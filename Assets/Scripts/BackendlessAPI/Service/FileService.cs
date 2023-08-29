using BackendlessAPI.Async;
using BackendlessAPI.Engine;
using BackendlessAPI.Exception;
using BackendlessAPI.File;
using BackendlessAPI.LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace BackendlessAPI.Service
{
	public class FileService
	{
		private const int UPLOAD_BUFFER_DEFAULT_LENGTH = 8192;

		public void Upload(Stream stream, string remotePath, AsyncCallback<BackendlessFile> callback)
		{
			Upload(stream, remotePath, new EmptyUploadCallback(), callback);
		}

		public void Upload(Stream stream, string remotePath, UploadCallback uploadCallback, AsyncCallback<BackendlessFile> callback)
		{
			if (string.IsNullOrEmpty(remotePath))
			{
				throw new ArgumentNullException("File path cannot be null or empty.");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("File cannot be null.");
			}
			MakeFileUpload(stream, remotePath, uploadCallback, callback);
		}

		public void Remove(string fileUrl)
		{
			if (string.IsNullOrEmpty(fileUrl))
			{
				throw new ArgumentNullException("File path cannot be null or empty.");
			}
			Invoker.InvokeSync<object>(Invoker.Api.FILESERVICE_REMOVE, new object[2]
			{
				null,
				fileUrl
			});
		}

		public void Remove(string fileUrl, AsyncCallback<object> callback)
		{
			if (string.IsNullOrEmpty(fileUrl))
			{
				throw new ArgumentNullException("File path cannot be null or empty.");
			}
			Invoker.InvokeAsync(Invoker.Api.FILESERVICE_REMOVE, new object[2]
			{
				null,
				fileUrl
			}, callback);
		}

		public void RemoveDirectory(string directoryPath)
		{
			if (string.IsNullOrEmpty(directoryPath))
			{
				throw new ArgumentNullException("File path cannot be null or empty.");
			}
			Invoker.InvokeSync<object>(Invoker.Api.FILESERVICE_REMOVE, new object[2]
			{
				null,
				directoryPath
			});
		}

		public void RemoveDirectory(string directoryPath, AsyncCallback<object> callback)
		{
			if (string.IsNullOrEmpty(directoryPath))
			{
				throw new ArgumentNullException("File path cannot be null or empty.");
			}
			Invoker.InvokeAsync(Invoker.Api.FILESERVICE_REMOVE, new object[2]
			{
				null,
				directoryPath
			}, callback);
		}

		private void MakeFileUpload(Stream stream, string path, UploadCallback uploadCallback, AsyncCallback<BackendlessFile> callback)
		{
			string text = DateTime.Now.Ticks.ToString("x");
			byte[] bytes = Encoding.UTF8.GetBytes("\r\n--" + text + "--\r\n");
			string value = string.Empty;
			try
			{
				value = Path.GetFileName(path);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex.Message));
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("--");
			stringBuilder.Append(text);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Disposition: form-data; name=\"file\"; filename=\"");
			stringBuilder.Append(value);
			stringBuilder.Append("\"");
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Type: ");
			stringBuilder.Append("application/octet-stream");
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Transfer-Encoding: binary");
			stringBuilder.Append("\r\n");
			stringBuilder.Append("\r\n");
			string s = stringBuilder.ToString();
			byte[] bytes2 = Encoding.UTF8.GetBytes(s);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(Backendless.Url + "/" + Backendless.VersionNum + "/files/" + EncodeURL(path), UriKind.Absolute));
			httpWebRequest.ContentType = "multipart/form-data; boundary=" + text;
			httpWebRequest.Method = "POST";
			httpWebRequest.Headers["KeepAlive"] = "true";
			foreach (KeyValuePair<string, string> header in HeadersManager.GetInstance().Headers)
			{
				httpWebRequest.Headers[header.Key] = header.Value;
			}
			try
			{
				RequestStreamAsyncState<BackendlessFile> requestStreamAsyncState = new RequestStreamAsyncState<BackendlessFile>();
				requestStreamAsyncState.Callback = callback;
				requestStreamAsyncState.UploadCallback = uploadCallback;
				requestStreamAsyncState.HttpRequest = httpWebRequest;
				requestStreamAsyncState.HeaderBytes = bytes2;
				requestStreamAsyncState.BoundaryBytes = bytes;
				requestStreamAsyncState.Stream = stream;
				RequestStreamAsyncState<BackendlessFile> state = requestStreamAsyncState;
				httpWebRequest.BeginGetRequestStream(RequestStreamCallback, state);
			}
			catch (System.Exception ex2)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex2.Message));
			}
		}

		private static void RequestStreamCallback(IAsyncResult asyncResult)
		{
			RequestStreamAsyncState<BackendlessFile> requestStreamAsyncState = (RequestStreamAsyncState<BackendlessFile>)asyncResult.AsyncState;
			try
			{
				Stream stream = requestStreamAsyncState.Stream;
				HttpWebRequest httpRequest = requestStreamAsyncState.HttpRequest;
				UploadCallback uploadCallback = requestStreamAsyncState.UploadCallback;
				using (Stream stream2 = httpRequest.EndGetRequestStream(asyncResult))
				{
					byte[] headerBytes = requestStreamAsyncState.HeaderBytes;
					byte[] boundaryBytes = requestStreamAsyncState.BoundaryBytes;
					long length = stream.Length;
					long num = 0L;
					int num2 = (int)((length >= 8192) ? 8192 : length);
					byte[] buffer = new byte[num2];
					stream2.Write(headerBytes, 0, headerBytes.Length);
					int num3 = 0;
					stream.Seek(0L, SeekOrigin.Begin);
					for (int num4 = stream.Read(buffer, 0, num2); num4 > 0; num4 = stream.Read(buffer, 0, num2))
					{
						stream2.Write(buffer, 0, num4);
						num += num4;
						if (!(uploadCallback is EmptyUploadCallback))
						{
							int num5 = (int)((float)num / (float)length * 100f);
							if (num3 != num5)
							{
								num3 = num5;
								uploadCallback.ProgressHandler(num3);
							}
						}
					}
					if (!(uploadCallback is EmptyUploadCallback) && num3 != 100)
					{
						uploadCallback.ProgressHandler(100);
					}
					stream2.Write(boundaryBytes, 0, boundaryBytes.Length);
				}
				httpRequest.BeginGetResponse(ResponseCallback, requestStreamAsyncState);
			}
			catch (System.Exception ex)
			{
				if (requestStreamAsyncState.Callback == null)
				{
					throw;
				}
				requestStreamAsyncState.Callback.ErrorHandler(new BackendlessFault(ex.Message));
			}
		}

		private static void ResponseCallback(IAsyncResult asyncResult)
		{
			RequestStreamAsyncState<BackendlessFile> requestStreamAsyncState = (RequestStreamAsyncState<BackendlessFile>)asyncResult.AsyncState;
			using (requestStreamAsyncState.Stream)
			{
				try
				{
					using (Stream stream = requestStreamAsyncState.HttpRequest.EndGetResponse(asyncResult).GetResponseStream())
					{
						Encoding encoding = Encoding.GetEncoding("utf-8");
						string json = new StreamReader(stream, encoding).ReadToEnd();
						string fileURL = string.Empty;
						try
						{
							JsonData jsonData = JsonMapper.ToObject(json);
							fileURL = (string)jsonData["fileURL"];
						}
						catch (System.Exception)
						{
						}
						requestStreamAsyncState.Callback.ResponseHandler(new BackendlessFile(fileURL));
					}
				}
				catch (WebException ex2)
				{
					string json2 = new StreamReader(ex2.Response.GetResponseStream()).ReadToEnd();
					BackendlessFault backendlessFault = null;
					try
					{
						JsonData jsonData2 = JsonMapper.ToObject(json2);
						int num = (int)jsonData2["code"];
						string message = (string)jsonData2["message"];
						backendlessFault = new BackendlessFault(num.ToString(), message, null);
					}
					catch (System.Exception)
					{
						backendlessFault = new BackendlessFault(ex2);
					}
					if (requestStreamAsyncState.Callback == null)
					{
						throw new BackendlessException(backendlessFault);
					}
					requestStreamAsyncState.Callback.ErrorHandler(backendlessFault);
				}
			}
		}

		private string EncodeURL(string urlStr)
		{
			string[] array = urlStr.Split('/');
			string text = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				if (i != 0)
				{
					text += "/";
				}
				text += WWW.EscapeURL(array[i]);
			}
			return text;
		}
	}
}
