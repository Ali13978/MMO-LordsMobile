using System.Collections.Generic;
using UnityEngine;

namespace UTNotifications
{
	public class ManagerImpl : Manager
	{
		private const float m_timeBetweenCheckingForIncomingNotifications = 0.5f;

		private bool m_willHandleReceivedNotifications;

		private float m_timeToCheckForIncomingNotifications;

		public override bool Initialize(bool willHandleReceivedNotifications, int startId = 0, bool incrementalId = false)
		{
			m_willHandleReceivedNotifications = willHandleReceivedNotifications;
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					return androidJavaClass.CallStatic<bool>("initialize", new object[16]
					{
						Settings.Instance.PushNotificationsEnabledFirebase,
						Settings.Instance.PushNotificationsEnabledAmazon,
						Settings.Instance.FirebaseSenderID,
						willHandleReceivedNotifications,
						startId,
						incrementalId,
						(int)Settings.Instance.AndroidShowNotificationsMode,
						Settings.Instance.AndroidRestoreScheduledNotificationsAfterReboot,
						(int)Settings.Instance.AndroidNotificationsGrouping,
						Settings.Instance.AndroidShowLatestNotificationOnly,
						Settings.Instance.PushPayloadTitleFieldName,
						Settings.Instance.PushPayloadTextFieldName,
						Settings.Instance.PushPayloadUserDataParentFieldName,
						Settings.Instance.PushPayloadNotificationProfileFieldName,
						Settings.Instance.PushPayloadIdFieldName,
						Settings.Instance.PushPayloadBadgeFieldName
					});
					IL_0113:
					bool result;
					return result;
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
				return false;
				IL_0138:
				bool result;
				return result;
			}
		}

		public override void PostLocalNotification(string title, string text, int id, IDictionary<string, string> userData, string notificationProfile, int badgeNumber)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("postNotification", WWW.EscapeURL(title), WWW.EscapeURL(text), id, PackUserData(userData), notificationProfile, badgeNumber);
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		public override void ScheduleNotification(int triggerInSeconds, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile, int badgeNumber)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("scheduleNotification", triggerInSeconds, WWW.EscapeURL(title), WWW.EscapeURL(text), id, PackUserData(userData), notificationProfile, badgeNumber);
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		public override void ScheduleNotificationRepeating(int firstTriggerInSeconds, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile, int badgeNumber)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("scheduleNotificationRepeating", firstTriggerInSeconds, intervalSeconds, WWW.EscapeURL(title), WWW.EscapeURL(text), id, PackUserData(userData), notificationProfile, badgeNumber);
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		public override bool NotificationsEnabled()
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					return androidJavaClass.CallStatic<bool>("notificationsEnabled", new object[0]);
					IL_0022:
					bool result;
					return result;
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
				return false;
				IL_0047:
				bool result;
				return result;
			}
		}

		public override bool NotificationsAllowed()
		{
			return true;
		}

		public override void SetNotificationsEnabled(bool enabled)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("setNotificationsEnabled", enabled);
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		public override bool PushNotificationsEnabled()
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					return androidJavaClass.CallStatic<bool>("pushNotificationsEnabled", new object[0]) && ((Settings.Instance.PushNotificationsEnabledFirebase && androidJavaClass.CallStatic<bool>("fcmProviderAvailable", new object[0])) || (Settings.Instance.PushNotificationsEnabledAmazon && androidJavaClass.CallStatic<bool>("admProviderAvailable", new object[0])));
					IL_0075:
					bool result;
					return result;
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
				return false;
				IL_009a:
				bool result;
				return result;
			}
		}

		public override bool SetPushNotificationsEnabled(bool enabled)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("setPushNotificationsEnabled", enabled);
					return PushNotificationsEnabled();
					IL_0031:
					bool result;
					return result;
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
				return false;
				IL_0056:
				bool result;
				return result;
			}
		}

		public override void CancelNotification(int id)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("cancelNotification", id);
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
			HideNotification(id);
		}

		public override void HideNotification(int id)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("hideNotification", id);
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		public override void HideAllNotifications()
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("hideAllNotifications");
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		public override void CancelAllNotifications()
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("cancelAllNotifications");
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		public override int GetBadge()
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					return androidJavaClass.CallStatic<int>("getBadge", new object[0]);
					IL_0022:
					int result;
					return result;
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
				return 0;
				IL_0047:
				int result;
				return result;
			}
		}

		public override void SetBadge(int bandgeNumber)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("setBadge", bandgeNumber);
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		public void _OnAndroidIdReceived(string providerAndId)
		{
			JSONNode jSONNode = JSON.Parse(providerAndId);
			if (OnSendRegistrationIdHasSubscribers())
			{
				_OnSendRegistrationId(jSONNode[0], jSONNode[1]);
			}
		}

		protected void LateUpdate()
		{
			m_timeToCheckForIncomingNotifications -= Time.unscaledDeltaTime;
			if (!(m_timeToCheckForIncomingNotifications > 0f))
			{
				m_timeToCheckForIncomingNotifications = 0.5f;
				if (OnNotificationClickedHasSubscribers())
				{
					try
					{
						using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
						{
							HandleClickedNotification(androidJavaClass.CallStatic<string>("getClickedNotificationPacked", new object[0]));
						}
					}
					catch (AndroidJavaException exception)
					{
						UnityEngine.Debug.LogException(exception);
					}
				}
				if (m_willHandleReceivedNotifications && OnNotificationsReceivedHasSubscribers())
				{
					try
					{
						using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("universal.tools.notifications.Manager"))
						{
							HandleReceivedNotifications(androidJavaClass2.CallStatic<string>("getReceivedNotificationsPacked", new object[0]));
						}
					}
					catch (AndroidJavaException exception2)
					{
						UnityEngine.Debug.LogException(exception2);
					}
				}
			}
		}

		protected void OnApplicationPause(bool paused)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("universal.tools.notifications.Manager"))
				{
					androidJavaClass.CallStatic("setBackgroundMode", paused);
				}
			}
			catch (AndroidJavaException exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		private static string[] PackUserData(IDictionary<string, string> userData)
		{
			if (userData == null || userData.Count == 0)
			{
				return null;
			}
			string[] array = new string[userData.Count * 2];
			int num = 0;
			foreach (KeyValuePair<string, string> userDatum in userData)
			{
				array[num++] = userDatum.Key;
				array[num++] = userDatum.Value;
			}
			return array;
		}

		private void HandleClickedNotification(string receivedNotificationPacked)
		{
			if (!string.IsNullOrEmpty(receivedNotificationPacked))
			{
				_OnNotificationClicked(ParseReceivedNotification(JSON.Parse(receivedNotificationPacked)));
			}
		}

		private void HandleReceivedNotifications(string receivedNotificationsPacked)
		{
			if (string.IsNullOrEmpty(receivedNotificationsPacked) || receivedNotificationsPacked == "[]")
			{
				return;
			}
			List<ReceivedNotification> list = new List<ReceivedNotification>();
			JSONNode jSONNode = JSON.Parse(receivedNotificationsPacked);
			for (int i = 0; i < jSONNode.Count; i++)
			{
				JSONNode json = jSONNode[i];
				ReceivedNotification receivedNotification = ParseReceivedNotification(json);
				bool flag = false;
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].id == receivedNotification.id)
					{
						list[j] = receivedNotification;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(receivedNotification);
				}
			}
			_OnNotificationsReceived(list);
		}

		private ReceivedNotification ParseReceivedNotification(JSONNode json)
		{
			string title = WWW.UnEscapeURL(json["title"].Value);
			string text = WWW.UnEscapeURL(json["text"].Value);
			int asInt = json["id"].AsInt;
			string value = json["notification_profile"].Value;
			int asInt2 = json["badgeNumber"].AsInt;
			JSONNode jSONNode = json["user_data"];
			Dictionary<string, string> dictionary;
			if (jSONNode != null && jSONNode.Count > 0)
			{
				dictionary = new Dictionary<string, string>();
				foreach (KeyValuePair<string, JSONNode> item in (JSONClass)jSONNode)
				{
					dictionary.Add(WWW.UnEscapeURL(item.Key), WWW.UnEscapeURL(item.Value.Value));
				}
			}
			else
			{
				dictionary = null;
			}
			return new ReceivedNotification(title, text, asInt, dictionary, value, asInt2);
		}
	}
}
