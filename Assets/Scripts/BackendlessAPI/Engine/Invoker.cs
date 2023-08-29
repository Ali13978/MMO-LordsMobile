using BackendlessAPI.Async;
using BackendlessAPI.Exception;
using BackendlessAPI.LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BackendlessAPI.Engine
{
	internal static class Invoker
	{
		public enum Api
		{
			USERSERVICE_REGISTER,
			USERSERVICE_UPDATE,
			USERSERVICE_LOGIN,
			USERSERVICE_LOGOUT,
			USERSERVICE_RESTOREPASSWORD,
			USERSERVICE_GETUSERROLES,
			USERSERVICE_DESCRIBEUSERCLASS,
			PERSISTENCESERVICE_CREATE,
			PERSISTENCESERVICE_UPDATE,
			PERSISTENCESERVICE_REMOVE,
			PERSISTENCESERVICE_FIND,
			PERSISTENCESERVICE_DESCRIBE,
			FILESERVICE_REMOVE,
			GEOSERVICE_ADDCATEGORY,
			GEOSERVICE_DELETECATEGORY,
			GEOSERVICE_GETCATEGORIES,
			GEOSERVICE_SAVEPOINT,
			GEOSERVICE_UPDATEPOINT,
			GEOSERVICE_GETPOINTS,
			GEOSERVICE_GETRECT,
			GEOSERVICE_RELATIVEFIND,
			MESSAGINGSERVICE_PUBLISH,
			MESSAGINGSERVICE_CANCEL,
			MESSAGINGSERVICE_SUBSCRIBE,
			MESSAGINGSERVICE_POLLMESSAGES,
			MESSAGINGSERVICE_SENDEMAIL,
			MESSAGINGSERVICE_REGISTERDEVICEONSERVER,
			MESSAGINGSERVICE_UNREGISTERDEVICEONSERVER,
			MESSAGINGSERVICE_GETREGISTRATION,
			EVENTS_DISPATCH,
			COUNTERSERVICE_RESET,
			COUNTERSERVICE_GET,
			COUNTERSERVICE_GET_INC,
			COUNTERSERVICE_INC_GET,
			COUNTERSERVICE_GET_DEC,
			COUNTERSERVICE_DEC_GET,
			COUNTERSERVICE_ADD_GET,
			COUNTERSERVICE_GET_ADD,
			COUNTERSERVICE_COM_SET,
			CACHESERVICE_PUT,
			CACHESERVICE_GET,
			CACHESERVICE_CONTAINS,
			CACHESERVICE_EXPIREIN,
			CACHESERVICE_EXPIREAT,
			CACHESERVICE_DELETE,
			LOGGERSERVICE_PUT,
			UNKNOWN
		}

		public enum Method
		{
			GET,
			POST,
			PUT,
			DELETE,
			PATCH,
			UNKNOWN
		}

		private class RequestState<T>
		{
			public HttpWebRequest Request
			{
				get;
				set;
			}

			public string RequestJsonString
			{
				get;
				set;
			}

			public AsyncCallback<T> Callback
			{
				get;
				set;
			}

			public Api Api
			{
				get;
				set;
			}

			public RequestState()
			{
				Request = null;
				RequestJsonString = null;
				Callback = null;
				Api = Api.UNKNOWN;
			}
		}

		private static void GetRestApiRequestCommand(Api api, object[] args, out Method method, out string url, out Dictionary<string, string> headers)
		{
			headers = HeadersManager.GetInstance().Headers;
			url = Backendless.Url + "/" + Backendless.VersionNum + "/";
			switch (api)
			{
			case Api.USERSERVICE_REGISTER:
				method = Method.POST;
				url += "users/register";
				break;
			case Api.USERSERVICE_UPDATE:
				method = Method.PUT;
				url += "users/";
				url += args[1];
				break;
			case Api.USERSERVICE_LOGIN:
				method = Method.POST;
				url += "users/login";
				break;
			case Api.USERSERVICE_LOGOUT:
				method = Method.GET;
				url += "users/logout";
				break;
			case Api.USERSERVICE_RESTOREPASSWORD:
				method = Method.GET;
				url += "users/restorepassword/";
				url += args[1];
				break;
			case Api.USERSERVICE_GETUSERROLES:
				method = Method.GET;
				url += "users/userroles";
				break;
			case Api.USERSERVICE_DESCRIBEUSERCLASS:
				method = Method.GET;
				url += "users/userclassprops";
				break;
			case Api.PERSISTENCESERVICE_CREATE:
				method = Method.POST;
				url += "data/";
				url += args[1];
				break;
			case Api.PERSISTENCESERVICE_UPDATE:
			{
				method = Method.PUT;
				url += "data/";
				string text = url;
				url = text + args[1] + "/" + args[2];
				break;
			}
			case Api.PERSISTENCESERVICE_REMOVE:
			{
				method = Method.DELETE;
				url += "data/";
				string text = url;
				url = text + args[1] + "/" + args[2];
				break;
			}
			case Api.PERSISTENCESERVICE_FIND:
				method = Method.GET;
				url += "data/";
				url += args[1];
				if (args[2] != null)
				{
					url = url + "/" + args[2];
				}
				if (args[3] != null)
				{
					url = url + "?" + args[3];
				}
				break;
			case Api.PERSISTENCESERVICE_DESCRIBE:
				method = Method.GET;
				url += "data/";
				url = url + args[1] + "/properties";
				break;
			case Api.FILESERVICE_REMOVE:
				method = Method.DELETE;
				url += "files/";
				url += args[1];
				break;
			case Api.GEOSERVICE_ADDCATEGORY:
				method = Method.PUT;
				url += "geo/categories/";
				url += args[1];
				break;
			case Api.GEOSERVICE_DELETECATEGORY:
				method = Method.DELETE;
				url += "geo/categories/";
				url += args[1];
				break;
			case Api.GEOSERVICE_GETCATEGORIES:
				method = Method.GET;
				url += "geo/categories";
				break;
			case Api.GEOSERVICE_SAVEPOINT:
				method = Method.PUT;
				url += "geo/points";
				url += args[1];
				break;
			case Api.GEOSERVICE_UPDATEPOINT:
				method = Method.PATCH;
				url += "geo/points";
				url += args[1];
				break;
			case Api.GEOSERVICE_GETPOINTS:
				method = Method.GET;
				url += "geo/points";
				url += args[1];
				break;
			case Api.GEOSERVICE_GETRECT:
				method = Method.GET;
				url += "geo/rect";
				url += args[1];
				break;
			case Api.GEOSERVICE_RELATIVEFIND:
				method = Method.GET;
				url += "geo/relative/points";
				url += args[1];
				break;
			case Api.MESSAGINGSERVICE_PUBLISH:
				method = Method.POST;
				url += "messaging/";
				url += args[1];
				break;
			case Api.MESSAGINGSERVICE_CANCEL:
				method = Method.DELETE;
				url += "messaging/";
				url += args[1];
				break;
			case Api.MESSAGINGSERVICE_SUBSCRIBE:
				method = Method.POST;
				url += "messaging/";
				url = url + args[1] + "/subscribe";
				break;
			case Api.MESSAGINGSERVICE_POLLMESSAGES:
			{
				method = Method.GET;
				url += "messaging/";
				string text = url;
				url = text + args[1] + "/" + args[2];
				break;
			}
			case Api.MESSAGINGSERVICE_SENDEMAIL:
				method = Method.POST;
				url += "messaging/email";
				break;
			case Api.MESSAGINGSERVICE_REGISTERDEVICEONSERVER:
				method = Method.POST;
				url += "messaging/registrations";
				break;
			case Api.MESSAGINGSERVICE_GETREGISTRATION:
				method = Method.GET;
				url += "messaging/registrations/";
				url += args[1];
				break;
			case Api.MESSAGINGSERVICE_UNREGISTERDEVICEONSERVER:
				method = Method.DELETE;
				url += "messaging/registrations/";
				url += args[1];
				break;
			case Api.EVENTS_DISPATCH:
				method = Method.POST;
				url += "servercode/events/";
				url += args[1];
				break;
			case Api.COUNTERSERVICE_RESET:
				method = Method.PUT;
				url += "counters/";
				url = url + args[1] + "/reset";
				break;
			case Api.COUNTERSERVICE_GET:
				method = Method.GET;
				url += "counters/";
				url += args[1];
				break;
			case Api.COUNTERSERVICE_GET_INC:
				method = Method.PUT;
				url += "counters/";
				url = url + args[1] + "/get/increment";
				break;
			case Api.COUNTERSERVICE_INC_GET:
				method = Method.PUT;
				url += "counters/";
				url = url + args[1] + "/increment/get";
				break;
			case Api.COUNTERSERVICE_GET_DEC:
				method = Method.PUT;
				url += "counters/";
				url = url + args[1] + "/get/decrement";
				break;
			case Api.COUNTERSERVICE_DEC_GET:
				method = Method.PUT;
				url += "counters/";
				url = url + args[1] + "/decrement/get";
				break;
			case Api.COUNTERSERVICE_ADD_GET:
			{
				method = Method.PUT;
				url += "counters/";
				string text = url;
				url = text + args[1] + "/incrementby/get?value=" + args[2];
				break;
			}
			case Api.COUNTERSERVICE_GET_ADD:
			{
				method = Method.PUT;
				url += "counters/";
				string text = url;
				url = text + args[1] + "/get/incrementby?value=" + args[2];
				break;
			}
			case Api.COUNTERSERVICE_COM_SET:
			{
				method = Method.PUT;
				url += "counters/";
				string text = url;
				url = text + args[1] + "/get/compareandset?expected=" + args[2] + "&updatedvalue=" + args[3];
				break;
			}
			case Api.CACHESERVICE_PUT:
				method = Method.PUT;
				url += "cache/";
				url += args[1];
				if ((int)args[2] != 0)
				{
					url = url + "?timeout=" + args[2];
				}
				break;
			case Api.CACHESERVICE_GET:
				method = Method.GET;
				url += "cache/";
				url += args[1];
				break;
			case Api.CACHESERVICE_CONTAINS:
				method = Method.GET;
				url += "cache/";
				url = url + args[1] + "/check";
				break;
			case Api.CACHESERVICE_EXPIREIN:
			{
				method = Method.PUT;
				url += "cache/";
				string text = url;
				url = text + args[1] + "/expireIn?timeout=" + args[2];
				break;
			}
			case Api.CACHESERVICE_EXPIREAT:
			{
				method = Method.PUT;
				url += "cache/";
				string text = url;
				url = text + args[1] + "/expireAt?timestamp=" + args[2];
				break;
			}
			case Api.CACHESERVICE_DELETE:
				method = Method.DELETE;
				url += "cache/";
				url += args[1];
				break;
			case Api.LOGGERSERVICE_PUT:
				method = Method.PUT;
				url += "log";
				break;
			default:
				throw new BackendlessException("GetRequestData() bad parameter 'api'");
			}
		}

		public static T InvokeSync<T>(Api api, object[] args)
		{
			Method method = Method.UNKNOWN;
			string url = null;
			Dictionary<string, string> headers = null;
			object data = null;
			if (args != null && args.Length > 0)
			{
				data = args[0];
			}
			try
			{
				GetRestApiRequestCommand(api, args, out method, out url, out headers);
				return InvokeSync<T>(api, method, url, headers, data);
				IL_0039:
				T result;
				return result;
			}
			catch (BackendlessException ex)
			{
				throw new BackendlessException(ex.BackendlessFault);
				IL_004d:
				T result;
				return result;
			}
		}

		public static T InvokeSync<T>(Api api, Method method, string url, Dictionary<string, string> headers, object data)
		{
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Method = method.ToString();
				foreach (KeyValuePair<string, string> header in headers)
				{
					httpWebRequest.Headers.Add(header.Key, header.Value);
				}
				if (method != 0 && data != null)
				{
					string value = JsonMapper.ToJson(data);
					httpWebRequest.ContentType = "application/json";
					using (Stream stream = httpWebRequest.GetRequestStream())
					{
						using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
						{
							streamWriter.Write(value);
						}
					}
				}
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (Stream stream2 = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(stream2, Encoding.UTF8))
					{
						T result = default(T);
						string text = streamReader.ReadToEnd();
						if ((api >= Api.COUNTERSERVICE_GET && api <= Api.COUNTERSERVICE_COM_SET) || api == Api.CACHESERVICE_CONTAINS)
						{
							result = (T)Convert.ChangeType(text, typeof(T));
						}
						else if (!string.IsNullOrEmpty(text))
						{
							result = JsonMapper.ToObject<T>(text);
						}
						return result;
						IL_015e:
						T result2;
						return result2;
					}
				}
			}
			catch (WebException ex)
			{
				using (Stream stream3 = ex.Response.GetResponseStream())
				{
					using (StreamReader streamReader2 = new StreamReader(stream3, Encoding.UTF8))
					{
						string json = streamReader2.ReadToEnd();
						BackendlessFault backendlessFault = null;
						try
						{
							JsonData jsonData = JsonMapper.ToObject(json);
							int num = (int)jsonData["code"];
							string message = (string)jsonData["message"];
							backendlessFault = new BackendlessFault(num.ToString(), message, null);
						}
						catch (System.Exception)
						{
							backendlessFault = new BackendlessFault(ex);
						}
						throw new BackendlessException(backendlessFault);
						IL_0211:
						T result2;
						return result2;
					}
				}
			}
			catch (System.Exception ex3)
			{
				BackendlessFault backendlessFault2 = new BackendlessFault(ex3);
				throw new BackendlessException(backendlessFault2);
				IL_0251:
				T result2;
				return result2;
			}
		}

		public static void InvokeAsync<T>(Api api, object[] args, AsyncCallback<T> callback)
		{
			Method method = Method.UNKNOWN;
			string url = null;
			Dictionary<string, string> headers = null;
			object data = null;
			if (args != null && args.Length > 0)
			{
				data = args[0];
			}
			try
			{
				GetRestApiRequestCommand(api, args, out method, out url, out headers);
				InvokeAsync(api, method, url, headers, data, callback);
			}
			catch (BackendlessException backendlessException)
			{
				BackendlessFault backendlessFault = new BackendlessFault(backendlessException);
				if (callback == null)
				{
					throw new BackendlessException(backendlessFault);
				}
				callback.ErrorHandler(backendlessFault);
			}
		}

		public static void InvokeAsync<T>(Api api, Method method, string url, Dictionary<string, string> headers, object data, AsyncCallback<T> callback)
		{
			RequestState<T> requestState = new RequestState<T>();
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Method = method.ToString();
				foreach (KeyValuePair<string, string> header in headers)
				{
					httpWebRequest.Headers.Add(header.Key, header.Value);
				}
				requestState.Request = httpWebRequest;
				requestState.Callback = callback;
				requestState.Api = api;
				if (method == Method.GET || data == null)
				{
					httpWebRequest.BeginGetResponse(Invoker.GetResponseCallback<T>, requestState);
				}
				else
				{
					string requestJsonString = JsonMapper.ToJson(data);
					httpWebRequest.ContentType = "application/json";
					requestState.RequestJsonString = requestJsonString;
					httpWebRequest.BeginGetRequestStream(Invoker.GetRequestStreamCallback<T>, requestState);
				}
			}
			catch (System.Exception ex)
			{
				BackendlessFault backendlessFault = new BackendlessFault(ex);
				if (requestState.Callback == null)
				{
					throw new BackendlessException(backendlessFault);
				}
				requestState.Callback.ErrorHandler(backendlessFault);
			}
		}

		private static void GetRequestStreamCallback<T>(IAsyncResult asynchronousResult)
		{
			RequestState<T> requestState = (RequestState<T>)asynchronousResult.AsyncState;
			try
			{
				HttpWebRequest request = requestState.Request;
				string requestJsonString = requestState.RequestJsonString;
				using (Stream stream = request.EndGetRequestStream(asynchronousResult))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
					{
						streamWriter.Write(requestJsonString);
					}
				}
				request.BeginGetResponse(Invoker.GetResponseCallback<T>, requestState);
			}
			catch (System.Exception ex)
			{
				BackendlessFault backendlessFault = new BackendlessFault(ex.Message);
				if (requestState.Callback != null)
				{
					requestState.Callback.ErrorHandler(backendlessFault);
				}
			}
		}

		private static void GetResponseCallback<T>(IAsyncResult asynchronousResult)
		{
			RequestState<T> requestState = (RequestState<T>)asynchronousResult.AsyncState;
			T response = default(T);
			try
			{
				HttpWebRequest request = requestState.Request;
				HttpWebResponse httpWebResponse = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
				using (Stream stream = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
					{
						string text = streamReader.ReadToEnd();
						if ((requestState.Api >= Api.COUNTERSERVICE_GET && requestState.Api <= Api.COUNTERSERVICE_COM_SET) || requestState.Api == Api.CACHESERVICE_CONTAINS)
						{
							response = (T)Convert.ChangeType(text, typeof(T));
						}
						else if (!string.IsNullOrEmpty(text))
						{
							response = JsonMapper.ToObject<T>(text);
						}
					}
				}
				httpWebResponse.Close();
				if (requestState.Callback != null)
				{
					requestState.Callback.ResponseHandler(response);
				}
			}
			catch (WebException ex)
			{
				using (Stream stream2 = ex.Response.GetResponseStream())
				{
					using (StreamReader streamReader2 = new StreamReader(stream2, Encoding.UTF8))
					{
						string json = streamReader2.ReadToEnd();
						BackendlessFault backendlessFault = null;
						try
						{
							JsonData jsonData = JsonMapper.ToObject(json);
							int num = (int)jsonData["code"];
							string message = (string)jsonData["message"];
							backendlessFault = new BackendlessFault(num.ToString(), message, null);
						}
						catch (System.Exception)
						{
							backendlessFault = new BackendlessFault(ex);
						}
						if (requestState.Callback != null)
						{
							requestState.Callback.ErrorHandler(backendlessFault);
						}
					}
				}
			}
			catch (System.Exception ex3)
			{
				BackendlessFault backendlessFault2 = new BackendlessFault(ex3);
				if (requestState.Callback != null)
				{
					requestState.Callback.ErrorHandler(backendlessFault2);
				}
			}
		}
	}
}
