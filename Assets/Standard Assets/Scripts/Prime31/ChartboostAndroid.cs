using UnityEngine;

namespace Prime31
{
	public class ChartboostAndroid
	{
		private static AndroidJavaObject _plugin;

		static ChartboostAndroid()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.prime31.ChartboostPlugin"))
				{
					_plugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
				}
			}
		}

		public static void setImpressionsUseActivities(bool impressionsUseActivities)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("setImpressionsUseActivities", impressionsUseActivities);
			}
		}

		public static void init(string appId, string appSignature, bool shouldRequestInterstitialsInFirstSession = true)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("init", appId, appSignature, shouldRequestInterstitialsInFirstSession);
			}
		}

		public static void cacheInterstitial(string location)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("cacheInterstitial", location);
			}
		}

		public static bool hasCachedInterstitial(string location)
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return false;
			}
			return _plugin.Call<bool>("hasCachedInterstitial", new object[1]
			{
				location
			});
		}

		public static void showInterstitial(string location)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("showInterstitial", location);
			}
		}

		public static void cacheMoreApps(string location = "None")
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("cacheMoreApps", location);
			}
		}

		public static bool hasCachedMoreApps(string location = "None")
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return false;
			}
			return _plugin.Call<bool>("hasCachedMoreApps", new object[1]
			{
				location
			});
		}

		public static void showMoreApps(string location = "None")
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("showMoreApps", location);
			}
		}

		public static void cacheRewardedVideo(string location = "None")
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("cacheRewardedVideo", location);
			}
		}

		public static bool hasCachedRewardedVideo(string location = "None")
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return false;
			}
			return _plugin.Call<bool>("hasCachedRewardedVideo", new object[1]
			{
				location
			});
		}

		public static void showRewardedVideo(string location = "None")
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("showRewardedVideo", location);
			}
		}
	}
}
