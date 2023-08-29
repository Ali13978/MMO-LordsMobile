using System;
using UnityEngine;
using VoxelBusters.DebugPRO;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class SharingAndroid : Sharing
	{
		private class Native
		{
			internal class Class
			{
				internal const string NAME = "com.voxelbusters.nativeplugins.features.sharing.SharingHandler";
			}

			internal class Methods
			{
				internal const string SHARE = "share";

				internal const string SEND_MAIL = "sendMail";

				internal const string SEND_SMS = "sendSms";

				internal const string IS_SERVICE_AVAILABLE = "isServiceAvailable";

				internal const string SHARE_ON_WHATS_APP = "shareOnWhatsApp";

				internal const string SET_ALLOWED_ORIENTATION = "setAllowedOrientation";
			}
		}

		private enum eShareOptionsAndroid
		{
			UNDEFINED,
			MESSAGE,
			MAIL,
			FB,
			TWITTER,
			WHATSAPP,
			GOOGLE_PLUS,
			INSTAGRAM
		}

		private const string kClosed = "closed";

		private const string kFailed = "failed";

		private AndroidJavaObject Plugin
		{
			get;
			set;
		}

		private SharingAndroid()
		{
			Plugin = AndroidPluginUtility.GetSingletonInstance("com.voxelbusters.nativeplugins.features.sharing.SharingHandler");
		}

		protected override void ParseSharingFinishedData(string _resultString, out eShareResult _shareResult)
		{
			if (_resultString.Equals("closed") || _resultString.Equals("failed"))
			{
				_shareResult = eShareResult.CLOSED;
				return;
			}
			VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "This status not implemented. sending closed event. [Fix this] " + _resultString);
			_shareResult = eShareResult.CLOSED;
		}

		protected override string SharingFailedResponse()
		{
			return "failed";
		}

		protected override void ParseMailShareFinishedData(string _resultString, out eShareResult _shareResult)
		{
			if (_resultString.Equals("closed") || _resultString.Equals("failed"))
			{
				_shareResult = eShareResult.CLOSED;
				return;
			}
			VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "This status not implemented. sending closed event. [Fix this] " + _resultString);
			_shareResult = eShareResult.CLOSED;
		}

		protected override string MailShareFailedResponse()
		{
			return "failed";
		}

		public override bool IsMailServiceAvailable()
		{
			bool flag = Plugin.Call<bool>("isServiceAvailable", new object[1]
			{
				2
			});
			if (!flag)
			{
				VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "[Sharing:Mail] CanSendMail=" + flag);
			}
			return flag;
		}

		protected override void ShowMailShareComposer(MailShareComposer _composer)
		{
			base.ShowMailShareComposer(_composer);
			if (IsMailServiceAvailable())
			{
				int num = (_composer.AttachmentData != null) ? _composer.AttachmentData.Length : 0;
				string text = (_composer.ToRecipients != null) ? _composer.ToRecipients.ToJSON() : null;
				string text2 = (_composer.CCRecipients != null) ? _composer.CCRecipients.ToJSON() : null;
				string text3 = (_composer.BCCRecipients != null) ? _composer.BCCRecipients.ToJSON() : null;
				Plugin.Call("sendMail", _composer.Subject, _composer.Body, _composer.IsHTMLBody, _composer.AttachmentData, num, _composer.MimeType, _composer.AttachmentFileName, text, text2, text3);
			}
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public override void SendMail(string _subject, string _body, bool _isHTMLBody, byte[] _attachmentByteArray, string _mimeType, string _attachmentFileNameWithExtn, string[] _recipients, SharingCompletion _onCompletion)
		{
			base.SendMail(_subject, _body, _isHTMLBody, _attachmentByteArray, _mimeType, _attachmentFileNameWithExtn, _recipients, _onCompletion);
			if (IsMailServiceAvailable())
			{
				int num = (_attachmentByteArray != null) ? _attachmentByteArray.Length : 0;
				string text = _recipients?.ToJSON();
				Plugin.Call("sendMail", _subject, _body, _isHTMLBody, _attachmentByteArray, num, _mimeType, _attachmentFileNameWithExtn, text, null, null);
			}
		}

		protected override void ParseMessagingShareFinishedData(string _resultString, out eShareResult _shareResult)
		{
			if (_resultString.Equals("closed") || _resultString.Equals("failed"))
			{
				_shareResult = eShareResult.CLOSED;
				return;
			}
			VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "This status not implemented. sending closed event. [Fix this] " + _resultString);
			_shareResult = eShareResult.CLOSED;
		}

		protected override string MessagingShareFailedResponse()
		{
			return "failed";
		}

		public override bool IsMessagingServiceAvailable()
		{
			bool flag = Plugin.Call<bool>("isServiceAvailable", new object[1]
			{
				1
			});
			if (!flag)
			{
				VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "[Sharing:Messaging] IsMessagingServiceAvailable=" + flag);
			}
			return flag;
		}

		protected override void ShowMessageShareComposer(MessageShareComposer _composer)
		{
			base.ShowMessageShareComposer(_composer);
			if (IsMessagingServiceAvailable())
			{
				string text = (_composer.ToRecipients != null) ? _composer.ToRecipients.ToJSON() : null;
				Plugin.Call("sendSms", _composer.Body, text);
			}
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public override void SendTextMessage(string _body, string[] _recipients, SharingCompletion _onCompletion)
		{
			base.SendTextMessage(_body, _recipients, _onCompletion);
			if (IsMessagingServiceAvailable())
			{
				string text = _recipients?.ToJSON();
				Plugin.Call("sendSms", _body, text);
			}
		}

		public override bool IsFBShareServiceAvailable()
		{
			bool flag = Plugin.Call<bool>("isServiceAvailable", new object[1]
			{
				3
			});
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing:FB] Is service available=" + flag);
			return flag;
		}

		public override bool IsTwitterShareServiceAvailable()
		{
			bool flag = Plugin.Call<bool>("isServiceAvailable", new object[1]
			{
				4
			});
			VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing:Twitter] Is service available=" + flag);
			return flag;
		}

		protected override void ShowFBShareComposer(FBShareComposer _composer)
		{
			base.ShowFBShareComposer(_composer);
			if (IsFBShareServiceAvailable())
			{
				int num = (_composer.ImageData != null) ? _composer.ImageData.Length : 0;
				eShareOptionsAndroid[] list = new eShareOptionsAndroid[6]
				{
					eShareOptionsAndroid.MAIL,
					eShareOptionsAndroid.MESSAGE,
					eShareOptionsAndroid.WHATSAPP,
					eShareOptionsAndroid.TWITTER,
					eShareOptionsAndroid.GOOGLE_PLUS,
					eShareOptionsAndroid.INSTAGRAM
				};
				Plugin.Call("share", _composer.Text, _composer.URL, _composer.ImageData, num, list.ToJSON());
			}
		}

		protected override void ShowTwitterShareComposer(TwitterShareComposer _composer)
		{
			base.ShowTwitterShareComposer(_composer);
			if (IsTwitterShareServiceAvailable())
			{
				int num = (_composer.ImageData != null) ? _composer.ImageData.Length : 0;
				eShareOptionsAndroid[] list = new eShareOptionsAndroid[6]
				{
					eShareOptionsAndroid.MAIL,
					eShareOptionsAndroid.MESSAGE,
					eShareOptionsAndroid.WHATSAPP,
					eShareOptionsAndroid.FB,
					eShareOptionsAndroid.GOOGLE_PLUS,
					eShareOptionsAndroid.INSTAGRAM
				};
				Plugin.Call("share", _composer.Text, _composer.URL, _composer.ImageData, num, list.ToJSON());
			}
		}

		protected override void ParseWhatsAppShareFinishedData(string _resultString, out eShareResult _shareResult)
		{
			if (_resultString.Equals("closed") || _resultString.Equals("failed"))
			{
				_shareResult = eShareResult.CLOSED;
				return;
			}
			VoxelBusters.DebugPRO.Console.LogWarning("Native Plugins", "This status not implemented. sending closed event. [Fix ] " + _resultString);
			_shareResult = eShareResult.CLOSED;
		}

		protected override string WhatsAppShareFailedResponse()
		{
			return "failed";
		}

		public override bool IsWhatsAppServiceAvailable()
		{
			bool flag = Plugin.Call<bool>("isServiceAvailable", new object[1]
			{
				5
			});
			if (!flag)
			{
				VoxelBusters.DebugPRO.Console.Log("Native Plugins", "[Sharing:WhatsApp] CanShare=" + flag);
			}
			return flag;
		}

		protected override void ShowWhatsAppShareComposer(WhatsAppShareComposer _composer)
		{
			base.ShowWhatsAppShareComposer(_composer);
			if (IsWhatsAppServiceAvailable())
			{
				byte[] imageData = _composer.ImageData;
				int num = (imageData != null) ? imageData.Length : 0;
				Plugin.Call("shareOnWhatsApp", _composer.Text, imageData, num);
			}
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public override void ShareTextMessageOnWhatsApp(string _message, SharingCompletion _onCompletion)
		{
			base.ShareTextMessageOnWhatsApp(_message, _onCompletion);
			if (!string.IsNullOrEmpty(_message) && IsWhatsAppServiceAvailable())
			{
				Plugin.Call("shareOnWhatsApp", _message, null, 0);
			}
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		public override void ShareImageOnWhatsApp(byte[] _imageByteArray, SharingCompletion _onCompletion)
		{
			base.ShareImageOnWhatsApp(_imageByteArray, _onCompletion);
			if (_imageByteArray != null && IsWhatsAppServiceAvailable())
			{
				Plugin.Call("shareOnWhatsApp", null, _imageByteArray, _imageByteArray.Length);
			}
		}

		protected override void ShowShareSheet(ShareSheet _shareSheet)
		{
			base.ShowShareSheet(_shareSheet);
			int num = (_shareSheet.ImageData != null) ? _shareSheet.ImageData.Length : 0;
			SetAllowedOrientation();
			Plugin.Call("share", _shareSheet.Text, _shareSheet.URL, _shareSheet.ImageData, num, _shareSheet.ExcludedShareOptions.ToJSON());
		}

		private void SetAllowedOrientation()
		{
			UnityEngine.Debug.Log("orientation : " + Screen.orientation);
			Plugin.Call("setAllowedOrientation", (int)Screen.orientation);
		}

		[Obsolete("This method is deprecated. Instead of this use ShowView.")]
		protected override void Share(string _message, string _URLString, byte[] _imageByteArray, string _excludedOptionsJsonString, SharingCompletion _onCompletion)
		{
			base.Share(_message, _URLString, _imageByteArray, _excludedOptionsJsonString, _onCompletion);
			int num = (_imageByteArray != null) ? _imageByteArray.Length : 0;
			Plugin.Call("share", _message, _URLString, _imageByteArray, num, _excludedOptionsJsonString);
		}
	}
}
