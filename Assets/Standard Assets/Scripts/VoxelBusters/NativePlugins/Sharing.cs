using System;
using System.Collections;
using System.IO;
using UnityEngine;
using VoxelBusters.DebugPRO;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class Sharing : MonoBehaviour
	{
		public delegate void SharingCompletion(eShareResult _result);

		protected const string kSharingFeatureDeprecatedMethodInfo = "This method is deprecated. Instead of this use ShowView.";

		protected SharingCompletion OnSharingFinished;

		private eShareOptions[] m_socialNetworkExcludedList = new eShareOptions[3]
		{
			eShareOptions.MESSAGE,
			eShareOptions.MAIL,
			eShareOptions.WHATSAPP
		};

		protected void SharingFinished(string _reasonString)
		{
			this.ResumeUnity();
			ParseSharingFinishedData(_reasonString, out eShareResult _shareResult);
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing:Events] Sharing finished, Result=" + _shareResult);
			if (OnSharingFinished != null)
			{
				OnSharingFinished(_shareResult);
			}
		}

		protected virtual void ParseSharingFinishedData(string _resultString, out eShareResult _shareResult)
		{
			_shareResult = eShareResult.CLOSED;
		}

		protected virtual string SharingFailedResponse()
		{
			return string.Empty;
		}

		protected void MailShareFinished(string _reasonString)
		{
			this.ResumeUnity();
			ParseMailShareFinishedData(_reasonString, out eShareResult _shareResult);
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing:Events] Mail sharing finished, Result=" + _shareResult);
			if (OnSharingFinished != null)
			{
				OnSharingFinished(_shareResult);
			}
		}

		protected virtual void ParseMailShareFinishedData(string _resultString, out eShareResult _shareResult)
		{
			_shareResult = eShareResult.CLOSED;
		}

		protected virtual string MailShareFailedResponse()
		{
			return string.Empty;
		}

		public virtual bool IsMailServiceAvailable()
		{
			bool flag = false;
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing] Is service available=" + flag);
			return flag;
		}

		protected virtual void ShowMailShareComposer(MailShareComposer _composer)
		{
			if (!IsMailServiceAvailable())
			{
				MailShareFinished(MailShareFailedResponse());
			}
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void SendPlainTextMail(string _subject, string _body, string[] _recipients, SharingCompletion _onCompletion)
		{
			SendMail(_subject, _body, _isHTMLBody: false, null, string.Empty, string.Empty, _recipients, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void SendHTMLTextMail(string _subject, string _htmlBody, string[] _recipients, SharingCompletion _onCompletion)
		{
			SendMail(_subject, _htmlBody, _isHTMLBody: true, null, string.Empty, string.Empty, _recipients, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void SendMailWithScreenshot(string _subject, string _body, bool _isHTMLBody, string[] _recipients, SharingCompletion _onCompletion)
		{
			StartCoroutine(TextureExtensions.TakeScreenshot(delegate(Texture2D _texture)
			{
				byte[] attachmentByteArray = _texture.EncodeToPNG();
				SendMail(_subject, _body, _isHTMLBody, attachmentByteArray, "image/png", "Screenshot.png", _recipients, _onCompletion);
			}));
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void SendMailWithTexture(string _subject, string _body, bool _isHTMLBody, Texture2D _texture, string[] _recipients, SharingCompletion _onCompletion)
		{
			byte[] attachmentByteArray = null;
			string mimeType = null;
			string attachmentFileNameWithExtn = null;
			if (_texture != null)
			{
				attachmentByteArray = _texture.EncodeToPNG();
				attachmentFileNameWithExtn = "texture.png";
				mimeType = "image/png";
			}
			else
			{
				VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "[Sharing] Sending mail with no attachments, attachment is null");
			}
			SendMail(_subject, _body, _isHTMLBody, attachmentByteArray, mimeType, attachmentFileNameWithExtn, _recipients, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void SendMailWithAttachment(string _subject, string _body, bool _isHTMLBody, string _attachmentPath, string _mimeType, string[] _recipients, SharingCompletion _onCompletion)
		{
			DownloadAsset downloadAsset = new DownloadAsset(URL.FileURLWithPath(_attachmentPath), _isAsynchronous: true);
			downloadAsset.OnCompletion = delegate(WWW _www, string _error)
			{
				byte[] attachmentByteArray = null;
				string attachmentFileNameWithExtn = null;
				if (string.IsNullOrEmpty(_error))
				{
					attachmentByteArray = _www.bytes;
					attachmentFileNameWithExtn = Path.GetFileName(_attachmentPath);
				}
				else
				{
					VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "[Sharing] The operation could not be completed. Error=" + _error);
				}
				SendMail(_subject, _body, _isHTMLBody, attachmentByteArray, _mimeType, attachmentFileNameWithExtn, _recipients, _onCompletion);
			};
			downloadAsset.StartRequest();
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public virtual void SendMail(string _subject, string _body, bool _isHTMLBody, byte[] _attachmentByteArray, string _mimeType, string _attachmentFileNameWithExtn, string[] _recipients, SharingCompletion _onCompletion)
		{
			this.PauseUnity();
			OnSharingFinished = _onCompletion;
			if (!IsMailServiceAvailable())
			{
				MailShareFinished(MailShareFailedResponse());
			}
		}

		protected void MessagingShareFinished(string _reasonString)
		{
			this.ResumeUnity();
			ParseMessagingShareFinishedData(_reasonString, out eShareResult _shareResult);
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing:Events] Message sharing finished, Result=" + _shareResult);
			if (OnSharingFinished != null)
			{
				OnSharingFinished(_shareResult);
			}
		}

		protected virtual void ParseMessagingShareFinishedData(string _resultString, out eShareResult _shareResult)
		{
			_shareResult = eShareResult.CLOSED;
		}

		protected virtual string MessagingShareFailedResponse()
		{
			return string.Empty;
		}

		public virtual bool IsMessagingServiceAvailable()
		{
			bool flag = false;
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing] Is service available=" + flag);
			return flag;
		}

		protected virtual void ShowMessageShareComposer(MessageShareComposer _composer)
		{
			if (!IsMessagingServiceAvailable())
			{
				MessagingShareFinished(MessagingShareFailedResponse());
			}
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public virtual void SendTextMessage(string _body, string[] _recipients, SharingCompletion _onCompletion)
		{
			this.PauseUnity();
			OnSharingFinished = _onCompletion;
			if (!IsMessagingServiceAvailable())
			{
				MessagingShareFinished(MessagingShareFailedResponse());
			}
		}

		protected void FBShareFinished(string _reasonString)
		{
			this.ResumeUnity();
			ParseFBShareFinishedResponse(_reasonString, out eShareResult _shareResult);
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing:Events] FB share finished, Result=" + _shareResult);
			if (OnSharingFinished != null)
			{
				OnSharingFinished(_shareResult);
			}
		}

		protected void TwitterShareFinished(string _reasonString)
		{
			this.ResumeUnity();
			ParseTwitterShareFinishedResponse(_reasonString, out eShareResult _shareResult);
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing:Events] Twitter share finished, Result=" + _shareResult);
			if (OnSharingFinished != null)
			{
				OnSharingFinished(_shareResult);
			}
		}

		protected virtual void ParseFBShareFinishedResponse(string _resultString, out eShareResult _shareResult)
		{
			_shareResult = eShareResult.CLOSED;
		}

		protected virtual void ParseTwitterShareFinishedResponse(string _resultString, out eShareResult _shareResult)
		{
			_shareResult = eShareResult.CLOSED;
		}

		protected virtual string FBShareFailedResponse()
		{
			return string.Empty;
		}

		protected virtual string TwitterShareFailedResponse()
		{
			return string.Empty;
		}

		public virtual bool IsFBShareServiceAvailable()
		{
			bool flag = false;
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing] Is service available=" + flag);
			return flag;
		}

		public virtual bool IsTwitterShareServiceAvailable()
		{
			bool flag = false;
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing] Is service available=" + flag);
			return flag;
		}

		protected virtual void ShowFBShareComposer(FBShareComposer _composer)
		{
			if (!IsFBShareServiceAvailable())
			{
				FBShareFinished(FBShareFailedResponse());
			}
		}

		protected virtual void ShowTwitterShareComposer(TwitterShareComposer _composer)
		{
			if (!IsTwitterShareServiceAvailable())
			{
				TwitterShareFinished(TwitterShareFailedResponse());
			}
		}

		protected void WhatsAppShareFinished(string _reasonString)
		{
			this.ResumeUnity();
			ParseWhatsAppShareFinishedData(_reasonString, out eShareResult _shareResult);
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing:Events] WhatsApp sharing finished, Result=" + _shareResult);
			if (OnSharingFinished != null)
			{
				OnSharingFinished(_shareResult);
			}
		}

		protected virtual void ParseWhatsAppShareFinishedData(string _resultString, out eShareResult _shareResult)
		{
			_shareResult = eShareResult.CLOSED;
		}

		protected virtual string WhatsAppShareFailedResponse()
		{
			return string.Empty;
		}

		public virtual bool IsWhatsAppServiceAvailable()
		{
			bool flag = false;
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing] Is service available=" + flag);
			return flag;
		}

		protected virtual void ShowWhatsAppShareComposer(WhatsAppShareComposer _composer)
		{
			if (!IsWhatsAppServiceAvailable())
			{
				WhatsAppShareFinished(WhatsAppShareFailedResponse());
			}
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public virtual void ShareTextMessageOnWhatsApp(string _message, SharingCompletion _onCompletion)
		{
			this.PauseUnity();
			OnSharingFinished = _onCompletion;
			if (string.IsNullOrEmpty(_message) || !IsWhatsAppServiceAvailable())
			{
				VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing] Failed to share text");
				WhatsAppShareFinished(WhatsAppShareFailedResponse());
			}
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareScreenshotOnWhatsApp(SharingCompletion _onCompletion)
		{
			StartCoroutine(TextureExtensions.TakeScreenshot(delegate(Texture2D _texture)
			{
				byte[] imageByteArray = _texture.EncodeToPNG();
				ShareImageOnWhatsApp(imageByteArray, _onCompletion);
			}));
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareImageOnWhatsApp(string _imagePath, SharingCompletion _onCompletion)
		{
			DownloadAsset downloadAsset = new DownloadAsset(new URL(_imagePath), _isAsynchronous: true);
			downloadAsset.OnCompletion = delegate(WWW _www, string _error)
			{
				if (string.IsNullOrEmpty(_error))
				{
					ShareImageOnWhatsApp(_www.bytes, _onCompletion);
				}
				else
				{
					VoxelBusters.DebugPRO.Console.LogError("Native Plugins", "[Sharing] The operation could not be completed. Error=" + _error);
					ShareImageOnWhatsApp((byte[])null, _onCompletion);
				}
			};
			downloadAsset.StartRequest();
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareImageOnWhatsApp(Texture2D _texture, SharingCompletion _onCompletion)
		{
			if (_texture == null)
			{
				VoxelBusters.DebugPRO.Console.LogError("Native Plugins", "[Sharing] Texture is null");
				ShareImageOnWhatsApp((byte[])null, _onCompletion);
			}
			else
			{
				byte[] imageByteArray = _texture.EncodeToPNG();
				ShareImageOnWhatsApp(imageByteArray, _onCompletion);
			}
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public virtual void ShareImageOnWhatsApp(byte[] _imageByteArray, SharingCompletion _onCompletion)
		{
			this.PauseUnity();
			OnSharingFinished = _onCompletion;
			if (_imageByteArray == null || !IsWhatsAppServiceAvailable())
			{
				VoxelBusters.DebugPRO.Console.LogError("Native Plugins", "[Sharing] Failed to share image");
				WhatsAppShareFinished(WhatsAppShareFailedResponse());
			}
		}

		public void ShowView(IShareView _shareView, SharingCompletion _onCompletion)
		{
			StartCoroutine(ShowViewCoroutine(_shareView, _onCompletion));
		}

		private IEnumerator ShowViewCoroutine(IShareView _shareView, SharingCompletion _onCompletion)
		{
			while (!_shareView.IsReadyToShowView)
			{
				yield return null;
			}
			this.PauseUnity();
			OnSharingFinished = _onCompletion;
			if (_shareView is MailShareComposer)
			{
				ShowMailShareComposer((MailShareComposer)_shareView);
			}
			else if (_shareView is MessageShareComposer)
			{
				ShowMessageShareComposer((MessageShareComposer)_shareView);
			}
			else if (_shareView is WhatsAppShareComposer)
			{
				ShowWhatsAppShareComposer((WhatsAppShareComposer)_shareView);
			}
			else if (_shareView is FBShareComposer)
			{
				ShowFBShareComposer((FBShareComposer)_shareView);
			}
			else if (_shareView is TwitterShareComposer)
			{
				ShowTwitterShareComposer((TwitterShareComposer)_shareView);
			}
			else
			{
				ShowShareSheet((ShareSheet)_shareView);
			}
		}

		protected virtual void ShowShareSheet(ShareSheet _shareSheet)
		{
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareTextMessageOnSocialNetwork(string _message, SharingCompletion _onCompletion)
		{
			ShareMessage(_message, m_socialNetworkExcludedList, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareURLOnSocialNetwork(string _message, string _URLString, SharingCompletion _onCompletion)
		{
			ShareURL(_message, _URLString, m_socialNetworkExcludedList, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareScreenShotOnSocialNetwork(string _message, SharingCompletion _onCompletion)
		{
			ShareScreenShot(_message, m_socialNetworkExcludedList, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareImageOnSocialNetwork(string _message, Texture2D _texture, SharingCompletion _onCompletion)
		{
			ShareImage(_message, _texture, m_socialNetworkExcludedList, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareImageOnSocialNetwork(string _message, string _imagePath, SharingCompletion _onCompletion)
		{
			ShareImageAtPath(_message, _imagePath, m_socialNetworkExcludedList, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareImageOnSocialNetwork(string _message, byte[] _imageByteArray, SharingCompletion _onCompletion)
		{
			Share(_message, null, _imageByteArray, m_socialNetworkExcludedList, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareMessage(string _message, eShareOptions[] _excludedOptions, SharingCompletion _onCompletion)
		{
			Share(_message, null, null, _excludedOptions, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareURL(string _message, string _URLString, eShareOptions[] _excludedOptions, SharingCompletion _onCompletion)
		{
			if (string.IsNullOrEmpty(_URLString))
			{
				VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "[Sharing] ShareURL, URL is null/empty");
			}
			Share(_message, _URLString, null, _excludedOptions, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareScreenShot(string _message, eShareOptions[] _excludedOptions, SharingCompletion _onCompletion)
		{
			StartCoroutine(TextureExtensions.TakeScreenshot(delegate(Texture2D _texture)
			{
				ShareImage(_message, _texture, _excludedOptions, _onCompletion);
			}));
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareImage(string _message, Texture2D _texture, eShareOptions[] _excludedOptions, SharingCompletion _onCompletion)
		{
			byte[] imageByteArray = null;
			if (_texture != null)
			{
				imageByteArray = _texture.EncodeToPNG();
			}
			else
			{
				VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "[Sharing] ShareImage, texure is null");
			}
			Share(_message, null, imageByteArray, _excludedOptions, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void ShareImageAtPath(string _message, string _imagePath, eShareOptions[] _excludedOptions, SharingCompletion _onCompletion)
		{
			URL uRL = URL.FileURLWithPath(_imagePath);
			DownloadAsset downloadAsset = new DownloadAsset(uRL, _isAsynchronous: true);
			downloadAsset.OnCompletion = delegate(WWW _www, string _error)
			{
				byte[] imageByteArray = null;
				if (string.IsNullOrEmpty(_error))
				{
					imageByteArray = _www.bytes;
				}
				else
				{
					VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "[Sharing] The operation could not be completed. Error=" + _error);
				}
				Share(_message, null, imageByteArray, _excludedOptions, _onCompletion);
			};
			downloadAsset.StartRequest();
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public void Share(string _message, string _URLString, byte[] _imageByteArray, eShareOptions[] _excludedOptions, SharingCompletion _onCompletion)
		{
			string excludedOptionsJsonString = null;
			if (_excludedOptions != null)
			{
				excludedOptionsJsonString = _excludedOptions.ToJSON();
			}
			Share(_message, _URLString, _imageByteArray, excludedOptionsJsonString, _onCompletion);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		protected virtual void Share(string _message, string _URLString, byte[] _imageByteArray, string _excludedOptionsJsonString, SharingCompletion _onCompletion)
		{
			this.PauseUnity();
			OnSharingFinished = _onCompletion;
		}
	}
}
