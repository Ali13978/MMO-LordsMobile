using System.IO;
using UnityEngine;
using VoxelBusters.DebugPRO;
using VoxelBusters.NativePlugins.Internal;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class MailShareComposer : ShareImageUtility, IShareView
	{
		private bool m_attachmentDownloadInProgress;

		public string Subject
		{
			get;
			set;
		}

		public string Body
		{
			get;
			set;
		}

		public bool IsHTMLBody
		{
			get;
			set;
		}

		public string[] ToRecipients
		{
			get;
			set;
		}

		public string[] CCRecipients
		{
			get;
			set;
		}

		public string[] BCCRecipients
		{
			get;
			set;
		}

		public byte[] AttachmentData
		{
			get;
			private set;
		}

		public string AttachmentFileName
		{
			get;
			private set;
		}

		public string MimeType
		{
			get;
			private set;
		}

		public bool IsReadyToShowView => !base.ImageAsyncDownloadInProgress && !m_attachmentDownloadInProgress;

		public override void AttachImage(Texture2D _texture)
		{
			if (_texture != null)
			{
				AttachmentData = _texture.EncodeToPNG();
				AttachmentFileName = "ShareImage.png";
				MimeType = "image/png";
			}
			else
			{
				AttachmentData = null;
				AttachmentFileName = null;
				MimeType = null;
			}
		}

		public void AddAttachmentAtPath(string _attachmentPath, string _MIMEType)
		{
			m_attachmentDownloadInProgress = true;
			DownloadAsset downloadAsset = new DownloadAsset(URL.FileURLWithPath(_attachmentPath), _isAsynchronous: true);
			downloadAsset.OnCompletion = delegate(WWW _www, string _error)
			{
				m_attachmentDownloadInProgress = false;
				if (string.IsNullOrEmpty(_error))
				{
					AddAttachment(_www.bytes, Path.GetFileName(_attachmentPath), _MIMEType);
				}
				else
				{
					Console.LogWarning("Native Plugins", "[Sharing] The operation could not be completed. Error=" + _error);
				}
			};
			downloadAsset.StartRequest();
		}

		public void AddAttachment(byte[] _attachmentData, string _attachmentFileName, string _MIMEType)
		{
			AttachmentData = _attachmentData;
			AttachmentFileName = _attachmentFileName;
			MimeType = _MIMEType;
		}
	}
}
