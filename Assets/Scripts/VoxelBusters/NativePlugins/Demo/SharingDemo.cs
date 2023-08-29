using UnityEngine;

namespace VoxelBusters.NativePlugins.Demo
{
	public class SharingDemo : NPDemoBase
	{
		[SerializeField]
		[Header("Message Sharing Properties")]
		private string m_smsBody = "SMS body holds text message that needs to be sent to recipients";

		[SerializeField]
		private string[] m_smsRecipients;

		[Header("Mail Sharing Properties")]
		[SerializeField]
		private string m_mailSubject = "Demo Mail";

		[SerializeField]
		private string m_plainMailBody = "This is plain text mail.";

		[SerializeField]
		private string m_htmlMailBody = "<html><body><h1>Hello</h1></body></html>";

		[SerializeField]
		private string[] m_mailToRecipients;

		[SerializeField]
		private string[] m_mailCCRecipients;

		[SerializeField]
		private string[] m_mailBCCRecipients;

		[Header("Share Sheet Properties")]
		[SerializeField]
		private eShareOptions[] m_excludedOptions = new eShareOptions[0];

		[Header("Share Properties ")]
		[SerializeField]
		private string m_shareMessage = "share message";

		[SerializeField]
		private string m_shareURL = "http://www.google.com";

		[SerializeField]
		[Tooltip("This demo consideres image relative to Application.persistentDataPath")]
		private string m_shareImageRelativePath;

		protected override void Start()
		{
			base.Start();
			AddExtraInfoTexts("When it comes to WhatsApp sharing, there is one major limitation on iOS platform i.e, you can either share only image or only text but not both. While sharing if both properties are set, then only image will be shared.");
		}

		protected override void DisplayFeatureFunctionalities()
		{
			base.DisplayFeatureFunctionalities();
			DrawMessageShareSection();
			DrawMailShareSection();
			DrawFBShareSection();
			DrawTwitterShareSection();
			DrawWhatsAppShareSection();
			DrawSocialShareSheetSection();
			DrawShareSheetSection();
		}

		private void DrawMessageShareSection()
		{
			GUILayout.Label("Message Share", "sub-title");
			if (GUILayout.Button("Is Messaging Available"))
			{
				AddNewResult("IsMessagingAvailable=" + IsMessagingServiceAvailable());
			}
			if (GUILayout.Button("Send Text Message"))
			{
				SendTextMessage();
			}
		}

		private void DrawMailShareSection()
		{
			GUILayout.Label("Mail Share", "sub-title");
			if (GUILayout.Button("Is Mail Available"))
			{
				AddNewResult("Can Send Mail = " + IsMailServiceAvailable());
			}
			if (GUILayout.Button("Send Plain Text Mail"))
			{
				SendPlainTextMail();
			}
			if (GUILayout.Button("Send HTML Text Mail"))
			{
				SendHTMLTextMail();
			}
			if (GUILayout.Button("Send Mail With Screenshot"))
			{
				SendMailWithScreenshot();
			}
			if (GUILayout.Button("Send Mail With Attachment : Path"))
			{
				SendMailWithAttachment();
			}
		}

		private void DrawFBShareSection()
		{
			GUILayout.Label("FB Share", "sub-title");
			if (GUILayout.Button("Is FB Share Service Available"))
			{
				AddNewResult("Can Share On FB = " + IsFBShareServiceAvailable());
			}
			if (GUILayout.Button("Share Text Message On FB"))
			{
				ShareTextMessageOnFB();
			}
			if (GUILayout.Button("Share URL On FB"))
			{
				ShareURLOnFB();
			}
			if (GUILayout.Button("Share Screenshot On FB"))
			{
				ShareScreenshotOnFB();
			}
			if (GUILayout.Button("Share Image On FB"))
			{
				ShareImageOnFB();
			}
		}

		private void DrawTwitterShareSection()
		{
			GUILayout.Label("Twitter Share", "sub-title");
			if (GUILayout.Button("Is Twitter Share Service Available"))
			{
				AddNewResult("Can Share On Twitter = " + IsTwitterShareServiceAvailable());
			}
			if (GUILayout.Button("Share Text Message On Twitter"))
			{
				ShareTextMessageOnTwitter();
			}
			if (GUILayout.Button("ShareURLOnTwitter"))
			{
				ShareURLOnTwitter();
			}
			if (GUILayout.Button("Share Screenshot On Twitter"))
			{
				ShareScreenshotOnTwitter();
			}
			if (GUILayout.Button("Share Image On Twitter"))
			{
				ShareImageOnTwitter();
			}
		}

		private void DrawWhatsAppShareSection()
		{
			GUILayout.Label("WhatsApp Share", "sub-title");
			if (GUILayout.Button("Is WhatsApp Service Available"))
			{
				AddNewResult("Can Share On WhatsApp = " + IsWhatsAppServiceAvailable());
			}
			if (GUILayout.Button("ShareTextMessageOnWhatsApp"))
			{
				ShareTextMessageOnWhatsApp();
			}
			if (GUILayout.Button("Share Screenshot On WhatsApp"))
			{
				ShareScreenshotOnWhatsApp();
			}
			if (GUILayout.Button("Share Image On WhatsApp"))
			{
				ShareImageOnWhatsApp();
			}
		}

		private void DrawSocialShareSheetSection()
		{
			GUILayout.Label("Social Share Sheet", "sub-title");
			if (GUILayout.Button("Share Text Message On SocialNetwork"))
			{
				ShareTextMessageOnSocialNetwork();
			}
			if (GUILayout.Button("Share URL On SocialNetwork"))
			{
				ShareURLOnSocialNetwork();
			}
			if (GUILayout.Button("Share ScreenShot On SocialNetwork"))
			{
				ShareScreenShotOnSocialNetwork();
			}
			if (GUILayout.Button("Share Image On SocialNetwork"))
			{
				ShareImageOnSocialNetwork();
			}
		}

		private void DrawShareSheetSection()
		{
			GUILayout.Label("Share Sheet", "sub-title");
			if (GUILayout.Button("Share Text Message Using ShareSheet"))
			{
				ShareTextMessageUsingShareSheet();
			}
			if (GUILayout.Button("Share URL Using ShareSheet"))
			{
				ShareURLUsingShareSheet();
			}
			if (GUILayout.Button("Share ScreenShot Using ShareSheet"))
			{
				ShareScreenShotUsingShareSheet();
			}
			if (GUILayout.Button("Share Image At Path Using ShareSheet"))
			{
				ShareImageAtPathUsingShareSheet();
			}
		}

		private bool IsMessagingServiceAvailable()
		{
			return NPBinding.Sharing.IsMessagingServiceAvailable();
		}

		private void SendTextMessage()
		{
			MessageShareComposer messageShareComposer = new MessageShareComposer();
			messageShareComposer.Body = m_smsBody;
			messageShareComposer.ToRecipients = m_smsRecipients;
			NPBinding.Sharing.ShowView(messageShareComposer, FinishedSharing);
		}

		private bool IsMailServiceAvailable()
		{
			return NPBinding.Sharing.IsMailServiceAvailable();
		}

		private void SendPlainTextMail()
		{
			MailShareComposer mailShareComposer = new MailShareComposer();
			mailShareComposer.Subject = m_mailSubject;
			mailShareComposer.Body = m_plainMailBody;
			mailShareComposer.IsHTMLBody = false;
			mailShareComposer.ToRecipients = m_mailToRecipients;
			mailShareComposer.CCRecipients = m_mailCCRecipients;
			mailShareComposer.BCCRecipients = m_mailBCCRecipients;
			NPBinding.Sharing.ShowView(mailShareComposer, FinishedSharing);
		}

		private void SendHTMLTextMail()
		{
			MailShareComposer mailShareComposer = new MailShareComposer();
			mailShareComposer.Subject = m_mailSubject;
			mailShareComposer.Body = m_htmlMailBody;
			mailShareComposer.IsHTMLBody = true;
			mailShareComposer.ToRecipients = m_mailToRecipients;
			mailShareComposer.CCRecipients = m_mailCCRecipients;
			mailShareComposer.BCCRecipients = m_mailBCCRecipients;
			NPBinding.Sharing.ShowView(mailShareComposer, FinishedSharing);
		}

		private void SendMailWithScreenshot()
		{
			MailShareComposer mailShareComposer = new MailShareComposer();
			mailShareComposer.Subject = m_mailSubject;
			mailShareComposer.Body = m_plainMailBody;
			mailShareComposer.IsHTMLBody = false;
			mailShareComposer.ToRecipients = m_mailToRecipients;
			mailShareComposer.CCRecipients = m_mailCCRecipients;
			mailShareComposer.BCCRecipients = m_mailBCCRecipients;
			mailShareComposer.AttachScreenShot();
			NPBinding.Sharing.ShowView(mailShareComposer, FinishedSharing);
		}

		private void SendMailWithAttachment()
		{
			MailShareComposer mailShareComposer = new MailShareComposer();
			mailShareComposer.Subject = m_mailSubject;
			mailShareComposer.Body = m_plainMailBody;
			mailShareComposer.IsHTMLBody = false;
			mailShareComposer.ToRecipients = m_mailToRecipients;
			mailShareComposer.CCRecipients = m_mailCCRecipients;
			mailShareComposer.BCCRecipients = m_mailBCCRecipients;
			mailShareComposer.AddAttachmentAtPath(GetImageFullPath(), "image/png");
			NPBinding.Sharing.ShowView(mailShareComposer, FinishedSharing);
		}

		private bool IsFBShareServiceAvailable()
		{
			return NPBinding.Sharing.IsFBShareServiceAvailable();
		}

		private void ShareTextMessageOnFB()
		{
			FBShareComposer fBShareComposer = new FBShareComposer();
			fBShareComposer.Text = m_shareMessage;
			NPBinding.Sharing.ShowView(fBShareComposer, FinishedSharing);
		}

		private void ShareURLOnFB()
		{
			FBShareComposer fBShareComposer = new FBShareComposer();
			fBShareComposer.Text = m_shareMessage;
			fBShareComposer.URL = m_shareURL;
			NPBinding.Sharing.ShowView(fBShareComposer, FinishedSharing);
		}

		private void ShareScreenshotOnFB()
		{
			FBShareComposer fBShareComposer = new FBShareComposer();
			fBShareComposer.Text = m_shareMessage;
			fBShareComposer.AttachScreenShot();
			NPBinding.Sharing.ShowView(fBShareComposer, FinishedSharing);
		}

		private void ShareImageOnFB()
		{
			FBShareComposer fBShareComposer = new FBShareComposer();
			fBShareComposer.Text = m_shareMessage;
			fBShareComposer.AttachImageAtPath(GetImageFullPath());
			NPBinding.Sharing.ShowView(fBShareComposer, FinishedSharing);
		}

		private bool IsTwitterShareServiceAvailable()
		{
			return NPBinding.Sharing.IsTwitterShareServiceAvailable();
		}

		private void ShareTextMessageOnTwitter()
		{
			TwitterShareComposer twitterShareComposer = new TwitterShareComposer();
			twitterShareComposer.Text = m_shareMessage;
			NPBinding.Sharing.ShowView(twitterShareComposer, FinishedSharing);
		}

		private void ShareURLOnTwitter()
		{
			TwitterShareComposer twitterShareComposer = new TwitterShareComposer();
			twitterShareComposer.Text = m_shareMessage;
			twitterShareComposer.URL = m_shareURL;
			NPBinding.Sharing.ShowView(twitterShareComposer, FinishedSharing);
		}

		private void ShareScreenshotOnTwitter()
		{
			TwitterShareComposer twitterShareComposer = new TwitterShareComposer();
			twitterShareComposer.Text = m_shareMessage;
			twitterShareComposer.AttachScreenShot();
			NPBinding.Sharing.ShowView(twitterShareComposer, FinishedSharing);
		}

		private void ShareImageOnTwitter()
		{
			TwitterShareComposer twitterShareComposer = new TwitterShareComposer();
			twitterShareComposer.Text = m_shareMessage;
			twitterShareComposer.AttachImageAtPath(GetImageFullPath());
			NPBinding.Sharing.ShowView(twitterShareComposer, FinishedSharing);
		}

		private bool IsWhatsAppServiceAvailable()
		{
			return NPBinding.Sharing.IsWhatsAppServiceAvailable();
		}

		private void ShareTextMessageOnWhatsApp()
		{
			WhatsAppShareComposer whatsAppShareComposer = new WhatsAppShareComposer();
			whatsAppShareComposer.Text = m_shareMessage;
			NPBinding.Sharing.ShowView(whatsAppShareComposer, FinishedSharing);
		}

		private void ShareScreenshotOnWhatsApp()
		{
			WhatsAppShareComposer whatsAppShareComposer = new WhatsAppShareComposer();
			whatsAppShareComposer.AttachScreenShot();
			NPBinding.Sharing.ShowView(whatsAppShareComposer, FinishedSharing);
		}

		private void ShareImageOnWhatsApp()
		{
			WhatsAppShareComposer whatsAppShareComposer = new WhatsAppShareComposer();
			whatsAppShareComposer.AttachImageAtPath(GetImageFullPath());
			NPBinding.Sharing.ShowView(whatsAppShareComposer, FinishedSharing);
		}

		private void ShareTextMessageOnSocialNetwork()
		{
			SocialShareSheet socialShareSheet = new SocialShareSheet();
			socialShareSheet.Text = m_shareMessage;
			NPBinding.UI.SetPopoverPointAtLastTouchPosition();
			NPBinding.Sharing.ShowView(socialShareSheet, FinishedSharing);
		}

		private void ShareURLOnSocialNetwork()
		{
			SocialShareSheet socialShareSheet = new SocialShareSheet();
			socialShareSheet.Text = m_shareMessage;
			socialShareSheet.URL = m_shareURL;
			NPBinding.UI.SetPopoverPointAtLastTouchPosition();
			NPBinding.Sharing.ShowView(socialShareSheet, FinishedSharing);
		}

		private void ShareScreenShotOnSocialNetwork()
		{
			SocialShareSheet socialShareSheet = new SocialShareSheet();
			socialShareSheet.Text = m_shareMessage;
			socialShareSheet.AttachScreenShot();
			NPBinding.UI.SetPopoverPointAtLastTouchPosition();
			NPBinding.Sharing.ShowView(socialShareSheet, FinishedSharing);
		}

		private void ShareImageOnSocialNetwork()
		{
			SocialShareSheet socialShareSheet = new SocialShareSheet();
			socialShareSheet.Text = m_shareMessage;
			socialShareSheet.AttachImageAtPath(GetImageFullPath());
			NPBinding.UI.SetPopoverPointAtLastTouchPosition();
			NPBinding.Sharing.ShowView(socialShareSheet, FinishedSharing);
		}

		private void ShareTextMessageUsingShareSheet()
		{
			ShareSheet shareSheet = new ShareSheet();
			shareSheet.Text = m_shareMessage;
			shareSheet.ExcludedShareOptions = m_excludedOptions;
			NPBinding.UI.SetPopoverPointAtLastTouchPosition();
			NPBinding.Sharing.ShowView(shareSheet, FinishedSharing);
		}

		private void ShareURLUsingShareSheet()
		{
			ShareSheet shareSheet = new ShareSheet();
			shareSheet.Text = m_shareMessage;
			shareSheet.URL = m_shareURL;
			shareSheet.ExcludedShareOptions = m_excludedOptions;
			NPBinding.UI.SetPopoverPointAtLastTouchPosition();
			NPBinding.Sharing.ShowView(shareSheet, FinishedSharing);
		}

		private void ShareScreenShotUsingShareSheet()
		{
			ShareSheet shareSheet = new ShareSheet();
			shareSheet.Text = m_shareMessage;
			shareSheet.ExcludedShareOptions = m_excludedOptions;
			shareSheet.AttachScreenShot();
			NPBinding.UI.SetPopoverPointAtLastTouchPosition();
			NPBinding.Sharing.ShowView(shareSheet, FinishedSharing);
		}

		private void ShareImageAtPathUsingShareSheet()
		{
			ShareSheet shareSheet = new ShareSheet();
			shareSheet.Text = m_shareMessage;
			shareSheet.ExcludedShareOptions = m_excludedOptions;
			shareSheet.AttachImageAtPath(GetImageFullPath());
			NPBinding.UI.SetPopoverPointAtLastTouchPosition();
			NPBinding.Sharing.ShowView(shareSheet, FinishedSharing);
		}

		private void FinishedSharing(eShareResult _result)
		{
			AddNewResult("Finished sharing");
			AppendResult("Share Result = " + _result);
		}

		private string GetImageFullPath()
		{
			return Application.persistentDataPath + "/" + m_shareImageRelativePath;
		}
	}
}
