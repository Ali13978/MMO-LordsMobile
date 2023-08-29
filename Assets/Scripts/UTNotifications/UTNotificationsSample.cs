using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace UTNotifications
{
	public class UTNotificationsSample : MonoBehaviour
	{
		protected const int LocalNotificationId = 1;

		protected const int ScheduledNotificationId = 2;

		protected const int RepeatingNotificationId = 3;

		protected const int PushNotificationId = 4;

		private const string m_title = "UTNotifications Sample";

		private const string m_notificationClickedTitle = "Notification Clicked";

		private const string m_notificationReceivedTitle = "Notification Received";

		private const string m_pleaseEnterWebServerAddress = "Please enter the running demo server address above. F.e. http://192.168.2.102:8080";

		private const string m_initializeTextOriginal = "Initialize";

		private const string m_notifyAllTextOriginal = "Notify all registered devices";

		private const string m_localNotificationText = "Create Local Notification";

		private const string m_incrementBadgeText = "Increment the badge number, when supported";

		private const string m_scheduledNotificationText = "Create Scheduled Notifications";

		private const string m_cancelRepeatingNotificationText = "Cancel Repeating Notification";

		private const string m_hideAllText = "Hide All Notifications";

		private const string m_cancelAllText = "Cancel All Notifications\n(Also resets the badge number on iOS)";

		private const string m_notificationsEnabledText = "Notifications Enabled";

		private const string m_webServerAddressOptionName = "_UT_NOTIFICATIONS_SAMPLE_SERVER_ADDRESS";

		protected string m_webServerAddress = string.Empty;

		private string m_initializeText = "Initialize";

		private string m_notifyAllText = "Notify all registered devices";

		private bool m_notificationsEnabled;

		private string m_clickToHide = "(Click to hide)";

		private ReceivedNotification m_clickedNotification;

		private List<ReceivedNotification> m_receivedNotifications = new List<ReceivedNotification>();

		public void Start()
		{
			Manager instance = Manager.Instance;
			instance.OnSendRegistrationId += SendRegistrationId;
			instance.OnNotificationClicked += OnNotificationClicked;
			instance.OnNotificationsReceived += OnNotificationsReceived;
			if (string.IsNullOrEmpty(m_webServerAddress))
			{
				m_webServerAddress = PlayerPrefs.GetString("_UT_NOTIFICATIONS_SAMPLE_SERVER_ADDRESS", string.Empty);
			}
			if (!string.IsNullOrEmpty(m_webServerAddress))
			{
				Initialize();
			}
		}

		public void OnGUI()
		{
			int num = Screen.height / 10;
			int offsetX = 8;
			int offsetY = num / 16;
			int offsetTitle = 32;
			if (!ShowReceivedNotification(clicked: true, num, offsetX, offsetY, offsetTitle) && !ShowReceivedNotification(clicked: false, num, offsetX, offsetY, offsetTitle))
			{
				ShowMenu(num, offsetX, offsetY, offsetTitle);
			}
		}

		protected bool ShowReceivedNotification(bool clicked, int height, int offsetX, int offsetY, int offsetTitle)
		{
			ReceivedNotification receivedNotification;
			if (clicked)
			{
				if (m_clickedNotification == null)
				{
					return false;
				}
				receivedNotification = m_clickedNotification;
			}
			else
			{
				if (m_receivedNotifications.Count <= 0)
				{
					return false;
				}
				receivedNotification = m_receivedNotifications[0];
			}
			GUI.Box(new Rect(offsetX, (Screen.height - ((height + offsetY) * 3 + offsetTitle)) / 2, Screen.width - offsetX * 2, (height + offsetY) * 3 + offsetTitle), ((!clicked) ? "Notification Received" : "Notification Clicked") + " [id=" + receivedNotification.id + "]");
			int num = (Screen.height - ((height + offsetY) * 3 + offsetTitle)) / 2 + offsetTitle;
			string text = string.Empty;
			if (receivedNotification.userData != null)
			{
				foreach (KeyValuePair<string, string> userDatum in receivedNotification.userData)
				{
					string text2 = text;
					text = text2 + userDatum.Key + "=" + userDatum.Value + " ";
				}
			}
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), ((!string.IsNullOrEmpty(receivedNotification.notificationProfile)) ? ("Profile: " + receivedNotification.notificationProfile + "\n") : string.Empty) + receivedNotification.title + "\n" + receivedNotification.text + ((!string.IsNullOrEmpty(text)) ? ("\n" + text) : string.Empty) + "\n" + m_clickToHide))
			{
				HideNotification(receivedNotification.id);
				if (clicked)
				{
					m_clickedNotification = null;
				}
				else
				{
					m_receivedNotifications.RemoveAt(0);
				}
			}
			num += height + offsetY;
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), "Hide All Notifications"))
			{
				m_clickedNotification = null;
				HideAll();
			}
			num += height + offsetY;
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), "Cancel All Notifications\n(Also resets the badge number on iOS)"))
			{
				m_clickedNotification = null;
				CancelAll();
			}
			num += height + offsetY;
			return true;
		}

		protected void ShowMenu(int height, int offsetX, int offsetY, int offsetTitle)
		{
			GUI.Box(new Rect(offsetX, offsetX, Screen.width - offsetX * 2, Screen.height - offsetX * 2), "UTNotifications Sample");
			int num = offsetY + offsetTitle;
			string text = GUI.TextField(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), m_webServerAddress);
			if (text != m_webServerAddress)
			{
				m_webServerAddress = text;
				PlayerPrefs.SetString("_UT_NOTIFICATIONS_SAMPLE_SERVER_ADDRESS", m_webServerAddress);
			}
			num += height + offsetY;
			if (string.IsNullOrEmpty(m_webServerAddress))
			{
				GUI.Label(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), "Please enter the running demo server address above. F.e. http://192.168.2.102:8080");
				return;
			}
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), m_notifyAllText))
			{
				NotifyAll();
			}
			num += height + offsetY;
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), m_initializeText))
			{
				Initialize();
			}
			num += height + offsetY;
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), "Create Local Notification"))
			{
				CreateLocalNotification();
			}
			num += height + offsetY;
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), "Create Scheduled Notifications"))
			{
				CreateScheduledNotifications();
			}
			num += height + offsetY;
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), "Cancel Repeating Notification"))
			{
				CancelRepeatingScheduledNotification();
			}
			num += height + offsetY;
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), "Increment the badge number, when supported"))
			{
				IncrementBadge();
			}
			num += height + offsetY;
			if (GUI.Button(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), "Cancel All Notifications\n(Also resets the badge number on iOS)"))
			{
				CancelAll();
			}
			num += height + offsetY;
			if (GUI.Toggle(new Rect(offsetX * 2, num, Screen.width - offsetX * 4, height), m_notificationsEnabled, "Notifications Enabled") != m_notificationsEnabled)
			{
				SetNotificationsEnabled(!m_notificationsEnabled);
			}
			num += height + offsetY;
		}

		protected void Initialize()
		{
			Manager instance = Manager.Instance;
			bool flag = instance.Initialize(willHandleReceivedNotifications: true);
			UnityEngine.Debug.Log("UTNotifications Initialize: " + flag);
			m_notificationsEnabled = instance.NotificationsEnabled();
			instance.SetBadge(0);
		}

		protected void CreateLocalNotification()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("user", "data");
			Manager.Instance.PostLocalNotification("Local", "Notification", 1, dictionary, "demo_notification_profile");
		}

		protected void CreateScheduledNotifications()
		{
			Manager.Instance.ScheduleNotification(15, "Scheduled", "Notification", 2, null, "demo_notification_profile", 1);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("image_url", "http://thecatapi.com/api/images/get?format=src&type=png&size=med");
			Manager.Instance.ScheduleNotificationRepeating(DateTime.Now.AddSeconds(5.0), 25, "Scheduled Repeating", "Notification", 3, dictionary, "demo_notification_profile", 1);
		}

		protected void CancelRepeatingScheduledNotification()
		{
			Manager.Instance.CancelNotification(3);
		}

		protected void IncrementBadge()
		{
			Manager.Instance.SetBadge(Manager.Instance.GetBadge() + 1);
		}

		protected void CancelAll()
		{
			Manager.Instance.CancelAllNotifications();
			Manager.Instance.SetBadge(0);
			m_receivedNotifications.Clear();
		}

		protected void SetNotificationsEnabled(bool enabled)
		{
			Manager.Instance.SetNotificationsEnabled(enabled);
			m_notificationsEnabled = Manager.Instance.NotificationsEnabled();
		}

		protected void HideNotification(int id)
		{
			Manager.Instance.HideNotification(id);
		}

		protected void HideAll()
		{
			Manager.Instance.HideAllNotifications();
			m_receivedNotifications.Clear();
		}

		protected void SendRegistrationId(string providerName, string registrationId)
		{
			string userId = GenerateDeviceUniqueIdentifier();
			StartCoroutine(SendRegistrationId(userId, providerName, registrationId));
		}

		protected IEnumerator SendRegistrationId(string userId, string providerName, string registrationId)
		{
			if (string.IsNullOrEmpty(m_webServerAddress))
			{
				m_initializeText = "Initialize\nUnable to send the registrationId: please fill the running demo server address";
				yield break;
			}
			m_initializeText = "Initialize\nSending registrationId...\nPlease make sure the example server is running as " + m_webServerAddress;
			WWWForm wwwForm = new WWWForm();
			wwwForm.AddField("uid", userId);
			wwwForm.AddField("provider", providerName);
			wwwForm.AddField("id", registrationId);
			WWW www = new WWW(m_webServerAddress + "/register", wwwForm);
			yield return www;
			if (www.error != null)
			{
				m_initializeText = "Initialize\n" + www.error + " " + www.text;
			}
			else
			{
				m_initializeText = "Initialize\n" + www.text;
			}
		}

		protected void NotifyAll()
		{
			StartCoroutine(NotifyAll(4, "Hello!", "From " + SystemInfo.deviceModel, "demo_notification_profile", 1));
		}

		protected IEnumerator NotifyAll(int id, string title, string text, string notificationProfile, int badgeNumber)
		{
			m_notifyAllText = "Notify all registered devices\nSending...\nPlease make sure the example server is running as " + m_webServerAddress;
			title = WWW.EscapeURL(title);
			text = WWW.EscapeURL(text);
			string noCache = "&_NO_CACHE=" + UnityEngine.Random.value;
			WWW www = new WWW(string.Format("{0}/notify?id={1}&title={2}&text={3}&badge={4}{5}{6}", m_webServerAddress, id, title, text, badgeNumber, (!string.IsNullOrEmpty(notificationProfile)) ? ("&notification_profile=" + notificationProfile) : string.Empty, noCache));
			yield return www;
			if (www.error != null)
			{
				m_notifyAllText = "Notify all registered devices\n" + www.error + " " + www.text;
			}
			else
			{
				m_notifyAllText = "Notify all registered devices\n" + www.text;
			}
		}

		protected void OnNotificationClicked(ReceivedNotification notification)
		{
			m_clickedNotification = notification;
		}

		protected void OnNotificationsReceived(IList<ReceivedNotification> receivedNotifications)
		{
			m_receivedNotifications.AddRange(receivedNotifications);
		}

		private static string GetMd5Hash(string input)
		{
			if (input == string.Empty)
			{
				return string.Empty;
			}
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] array = mD5CryptoServiceProvider.ComputeHash(Encoding.Default.GetBytes(input));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		private static string GenerateDeviceUniqueIdentifier()
		{
			try
			{
				string text = string.Empty;
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.content.Context");
				string static2 = androidJavaClass2.GetStatic<string>("TELEPHONY_SERVICE");
				AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getSystemService", new object[1]
				{
					static2
				});
				bool flag = false;
				try
				{
					text = androidJavaObject.Call<string>("getDeviceId", new object[0]);
				}
				catch
				{
					flag = true;
				}
				if (text == null)
				{
					text = string.Empty;
				}
				if (flag)
				{
					AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("android.provider.Settings$Secure");
					string static3 = androidJavaClass3.GetStatic<string>("ANDROID_ID");
					AndroidJavaObject androidJavaObject2 = @static.Call<AndroidJavaObject>("getContentResolver", new object[0]);
					text = androidJavaClass3.CallStatic<string>("getString", new object[2]
					{
						androidJavaObject2,
						static3
					});
					if (text == null)
					{
						text = string.Empty;
					}
				}
				if (text == string.Empty)
				{
					string text2 = "00000000000000000000000000000000";
					try
					{
						StreamReader streamReader = new StreamReader("/sys/class/net/wlan0/address");
						text2 = streamReader.ReadLine();
						streamReader.Close();
					}
					catch (Exception exception)
					{
						UnityEngine.Debug.LogException(exception);
					}
					text = text2.Replace(":", string.Empty);
				}
				return GetMd5Hash(text);
				IL_013f:
				string result;
				return result;
			}
			catch (Exception exception2)
			{
				UnityEngine.Debug.LogException(exception2);
				return GetMd5Hash("00000000000000000000000000000000");
				IL_015e:
				string result;
				return result;
			}
		}
	}
}
