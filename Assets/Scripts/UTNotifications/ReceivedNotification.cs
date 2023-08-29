using System.Collections.Generic;

namespace UTNotifications
{
	public class ReceivedNotification
	{
		public readonly string title;

		public readonly string text;

		public readonly int id;

		public readonly IDictionary<string, string> userData;

		public readonly string notificationProfile;

		public readonly int badgeNumber;

		public ReceivedNotification(string title, string text, int id, IDictionary<string, string> userData, string notificationProfile, int badgeNumber)
		{
			this.title = title;
			this.text = text;
			this.id = id;
			this.userData = userData;
			this.notificationProfile = notificationProfile;
			this.badgeNumber = badgeNumber;
		}
	}
}
