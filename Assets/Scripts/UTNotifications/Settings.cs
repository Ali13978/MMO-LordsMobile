using System;
using System.Collections.Generic;
using UnityEngine;

namespace UTNotifications
{
	public class Settings : ScriptableObject
	{
		[Serializable]
		public struct NotificationProfile
		{
			public string profileName;

			public string iosSound;

			public string androidIcon;

			public string androidLargeIcon;

			public string androidIcon5Plus;

			public string androidSound;
		}

		public enum ShowNotifications
		{
			WHEN_CLOSED_OR_IN_BACKGROUND,
			WHEN_CLOSED,
			ALWAYS
		}

		public enum TokenEncoding
		{
			Base64,
			HEX
		}

		public enum NotificationsGroupingMode
		{
			NONE,
			BY_NOTIFICATION_PROFILES,
			FROM_USER_DATA,
			ALL_IN_A_SINGLE_GROUP
		}

		private class UpdateMessage
		{
			public readonly string version;

			public readonly string text;

			public UpdateMessage(string version, string text)
			{
				this.version = version;
				this.text = text;
			}
		}

		public const string Version = "1.6.3";

		public const string DEFAULT_PROFILE_NAME = "default";

		public const string DEFAULT_PROFILE_NAME_INTERNAL = "__default_profile";

		private const string m_assetName = "UTNotificationsSettings";

		private const string m_settingsMenuItem = "Edit/Project Settings/UTNotifications";

		[SerializeField]
		private List<NotificationProfile> m_notificationProfiles = new List<NotificationProfile>();

		[SerializeField]
		private string m_pushPayloadTitleFieldName = "title";

		[SerializeField]
		private string m_pushPayloadTextFieldName = "text";

		[SerializeField]
		private string m_pushPayloadIdFieldName = "id";

		[SerializeField]
		private string m_pushPayloadUserDataParentFieldName = string.Empty;

		[SerializeField]
		private string m_pushPayloadNotificationProfileFieldName = "notification_profile";

		[SerializeField]
		private string m_pushPayloadBadgeFieldName = "badge_number";

		[SerializeField]
		private TokenEncoding m_iOSTokenEncoding = TokenEncoding.HEX;

		[SerializeField]
		private ShowNotifications m_androidShowNotificationsMode;

		[SerializeField]
		private bool m_android4CompatibilityMode;

		[SerializeField]
		private bool m_androidRestoreScheduledNotificationsAfterReboot = true;

		[SerializeField]
		private NotificationsGroupingMode m_androidNotificationsGrouping;

		[SerializeField]
		private bool m_androidShowLatestNotificationOnly;

		[SerializeField]
		private bool m_pushNotificationsEnabledIOS;

		[SerializeField]
		private bool m_pushNotificationsEnabledFirebase;

		[SerializeField]
		private bool m_pushNotificationsEnabledAmazon;

		[SerializeField]
		private bool m_pushNotificationsEnabledWindows;

		[SerializeField]
		private string m_firebaseSenderID = string.Empty;

		[SerializeField]
		private string m_assetVersionSaved = string.Empty;

		[SerializeField]
		private bool m_windowsDontShowWhenRunning = true;

		private static Settings m_instance;

		public static Settings Instance
		{
			get
			{
				if (m_instance == null)
				{
					m_instance = (Resources.Load("UTNotificationsSettings") as Settings);
					if (m_instance == null)
					{
						m_instance = ScriptableObject.CreateInstance<Settings>();
					}
				}
				return m_instance;
			}
		}

		public List<NotificationProfile> NotificationProfiles
		{
			get
			{
				if (m_notificationProfiles.Count != 0)
				{
					NotificationProfile notificationProfile = m_notificationProfiles[0];
					if (!(notificationProfile.profileName != "default"))
					{
						goto IL_0054;
					}
				}
				m_notificationProfiles.Insert(0, new NotificationProfile
				{
					profileName = "default"
				});
				goto IL_0054;
				IL_0054:
				return m_notificationProfiles;
			}
		}

		public string PushPayloadTitleFieldName
		{
			get
			{
				return m_pushPayloadTitleFieldName;
			}
			set
			{
				if (m_pushPayloadTitleFieldName != value)
				{
					m_pushPayloadTitleFieldName = value;
				}
			}
		}

		public string PushPayloadTextFieldName
		{
			get
			{
				return m_pushPayloadTextFieldName;
			}
			set
			{
				if (m_pushPayloadTextFieldName != value)
				{
					m_pushPayloadTextFieldName = value;
				}
			}
		}

		public string PushPayloadIdFieldName
		{
			get
			{
				return m_pushPayloadIdFieldName;
			}
			set
			{
				if (m_pushPayloadIdFieldName != value)
				{
					m_pushPayloadIdFieldName = value;
				}
			}
		}

		public string PushPayloadUserDataParentFieldName
		{
			get
			{
				return m_pushPayloadUserDataParentFieldName;
			}
			set
			{
				if (m_pushPayloadUserDataParentFieldName != value)
				{
					m_pushPayloadUserDataParentFieldName = value;
				}
			}
		}

		public string PushPayloadNotificationProfileFieldName
		{
			get
			{
				return m_pushPayloadNotificationProfileFieldName;
			}
			set
			{
				if (m_pushPayloadNotificationProfileFieldName != value)
				{
					m_pushPayloadNotificationProfileFieldName = value;
				}
			}
		}

		public string PushPayloadBadgeFieldName
		{
			get
			{
				return m_pushPayloadBadgeFieldName;
			}
			set
			{
				if (m_pushPayloadBadgeFieldName != value)
				{
					m_pushPayloadBadgeFieldName = value;
				}
			}
		}

		public bool PushNotificationsEnabledIOS
		{
			get
			{
				return m_pushNotificationsEnabledIOS;
			}
			set
			{
				if (m_pushNotificationsEnabledIOS != value)
				{
					m_pushNotificationsEnabledIOS = value;
				}
			}
		}

		public bool PushNotificationsEnabledFirebase
		{
			get
			{
				return m_pushNotificationsEnabledFirebase;
			}
			set
			{
				if (m_pushNotificationsEnabledFirebase != value)
				{
					m_pushNotificationsEnabledFirebase = value;
				}
			}
		}

		public bool PushNotificationsEnabledAmazon
		{
			get
			{
				return m_pushNotificationsEnabledAmazon;
			}
			set
			{
				if (m_pushNotificationsEnabledAmazon != value)
				{
					m_pushNotificationsEnabledAmazon = value;
				}
			}
		}

		public bool PushNotificationsEnabledWindows
		{
			get
			{
				return m_pushNotificationsEnabledWindows;
			}
			set
			{
				if (m_pushNotificationsEnabledWindows != value)
				{
					m_pushNotificationsEnabledWindows = value;
				}
			}
		}

		public TokenEncoding IOSTokenEncoding
		{
			get
			{
				return m_iOSTokenEncoding;
			}
			set
			{
				if (m_iOSTokenEncoding != value)
				{
					m_iOSTokenEncoding = value;
				}
			}
		}

		public ShowNotifications AndroidShowNotificationsMode
		{
			get
			{
				return m_androidShowNotificationsMode;
			}
			set
			{
				if (m_androidShowNotificationsMode != value)
				{
					m_androidShowNotificationsMode = value;
				}
			}
		}

		public bool AndroidRestoreScheduledNotificationsAfterReboot => m_androidRestoreScheduledNotificationsAfterReboot;

		public NotificationsGroupingMode AndroidNotificationsGrouping
		{
			get
			{
				return m_androidNotificationsGrouping;
			}
			set
			{
				if (m_androidNotificationsGrouping != value)
				{
					m_androidNotificationsGrouping = value;
				}
			}
		}

		public bool AndroidShowLatestNotificationOnly => m_androidShowLatestNotificationOnly;

		public string FirebaseSenderID => m_firebaseSenderID;

		public bool WindowsDontShowWhenRunning => m_windowsDontShowWhenRunning;
	}
}
