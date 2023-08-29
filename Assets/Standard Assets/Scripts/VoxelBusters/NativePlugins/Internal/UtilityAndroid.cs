using UnityEngine;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class UtilityAndroid : IUtilityPlatform
	{
		private class Native
		{
			internal class Class
			{
				internal const string NAME = "com.voxelbusters.nativeplugins.features.utility.UtilityHandler";
			}

			internal class Methods
			{
				internal const string SET_APPLICATION_ICON_BADGE_NUMBER = "setApplicationIconBadgeNumber";
			}
		}

		private AndroidJavaObject Plugin
		{
			get;
			set;
		}

		public UtilityAndroid()
		{
			Plugin = AndroidPluginUtility.GetSingletonInstance("com.voxelbusters.nativeplugins.features.utility.UtilityHandler");
		}

		public void OpenStoreLink(string _applicationID)
		{
			Application.OpenURL("http://play.google.com/store/apps/details?id=" + _applicationID);
		}

		public void SetApplicationIconBadgeNumber(int _badgeNumber)
		{
			Plugin.Call("setApplicationIconBadgeNumber", _badgeNumber);
		}
	}
}
