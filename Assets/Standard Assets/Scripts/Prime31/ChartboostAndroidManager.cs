using System;
using System.Collections.Generic;

namespace Prime31
{
	public class ChartboostAndroidManager : AbstractManager
	{
		public static event Action<string> didCacheInterstitialEvent;

		public static event Action<string, string> didFailToCacheInterstitialEvent;

		public static event Action<string, string> didFinishInterstitialEvent;

		public static event Action<string> didCacheMoreAppsEvent;

		public static event Action<string, string> didFailToCacheMoreAppsEvent;

		public static event Action<string, string> didFinishMoreAppsEvent;

		public static event Action<string> didCacheRewardedVideoEvent;

		public static event Action<string, string> didFailToLoadRewardedVideoEvent;

		public static event Action<string, string> didFinishRewardedVideoEvent;

		public static event Action<int> didCompleteRewardedVideoEvent;

		public static event Action<string> didFailToLoadUrlEvent;

		static ChartboostAndroidManager()
		{
			AbstractManager.initialize(typeof(ChartboostAndroidManager));
		}

		public void didCacheInterstitial(string location)
		{
			if (ChartboostAndroidManager.didCacheInterstitialEvent != null)
			{
				ChartboostAndroidManager.didCacheInterstitialEvent(location);
			}
		}

		public void didFailToLoadInterstitial(string json)
		{
			if (ChartboostAndroidManager.didFailToCacheInterstitialEvent != null)
			{
				Dictionary<string, string> dictionary = Json.decode<Dictionary<string, string>>(json);
				ChartboostAndroidManager.didFailToCacheInterstitialEvent(dictionary["location"], dictionary["error"]);
			}
		}

		public void didDismissInterstitial(string location)
		{
			if (ChartboostAndroidManager.didFinishInterstitialEvent != null)
			{
				ChartboostAndroidManager.didFinishInterstitialEvent(location, "dismiss");
			}
		}

		public void didClickInterstitial(string location)
		{
			if (ChartboostAndroidManager.didFinishInterstitialEvent != null)
			{
				ChartboostAndroidManager.didFinishInterstitialEvent(location, "click");
			}
		}

		public void didCloseInterstitial(string location)
		{
			if (ChartboostAndroidManager.didFinishInterstitialEvent != null)
			{
				ChartboostAndroidManager.didFinishInterstitialEvent(location, "close");
			}
		}

		public void didCacheMoreApps(string location)
		{
			if (ChartboostAndroidManager.didCacheMoreAppsEvent != null)
			{
				ChartboostAndroidManager.didCacheMoreAppsEvent(location);
			}
		}

		public void didFailToLoadMoreApps(string json)
		{
			if (ChartboostAndroidManager.didFailToCacheMoreAppsEvent != null)
			{
				Dictionary<string, string> dictionary = Json.decode<Dictionary<string, string>>(json);
				ChartboostAndroidManager.didFailToCacheMoreAppsEvent(dictionary["location"], dictionary["error"]);
			}
		}

		public void didDismissMoreApps(string location)
		{
			if (ChartboostAndroidManager.didFinishMoreAppsEvent != null)
			{
				ChartboostAndroidManager.didFinishMoreAppsEvent(location, "dismiss");
			}
		}

		public void didCloseMoreApps(string location)
		{
			if (ChartboostAndroidManager.didFinishMoreAppsEvent != null)
			{
				ChartboostAndroidManager.didFinishMoreAppsEvent(location, "close");
			}
		}

		public void didClickMoreApps(string location)
		{
			if (ChartboostAndroidManager.didFinishMoreAppsEvent != null)
			{
				ChartboostAndroidManager.didFinishMoreAppsEvent(location, "click");
			}
		}

		public void didCacheRewardedVideo(string location)
		{
			if (ChartboostAndroidManager.didCacheRewardedVideoEvent != null)
			{
				ChartboostAndroidManager.didCacheRewardedVideoEvent(location);
			}
		}

		public void didFailToLoadRewardedVideo(string json)
		{
			if (ChartboostAndroidManager.didFailToLoadRewardedVideoEvent != null)
			{
				Dictionary<string, string> dictionary = Json.decode<Dictionary<string, string>>(json);
				ChartboostAndroidManager.didFailToLoadRewardedVideoEvent(dictionary["location"], dictionary["error"]);
			}
		}

		public void didDismissRewardedVideo(string location)
		{
			if (ChartboostAndroidManager.didFinishRewardedVideoEvent != null)
			{
				ChartboostAndroidManager.didFinishRewardedVideoEvent(location, "dismiss");
			}
		}

		public void didCloseRewardedVideo(string location)
		{
			if (ChartboostAndroidManager.didFinishRewardedVideoEvent != null)
			{
				ChartboostAndroidManager.didFinishRewardedVideoEvent(location, "close");
			}
		}

		public void didClickRewardedVideo(string location)
		{
			if (ChartboostAndroidManager.didFinishRewardedVideoEvent != null)
			{
				ChartboostAndroidManager.didFinishRewardedVideoEvent(location, "click");
			}
		}

		public void didCompleteRewardedVideo(string reward)
		{
			if (ChartboostAndroidManager.didCompleteRewardedVideoEvent != null)
			{
				ChartboostAndroidManager.didCompleteRewardedVideoEvent(int.Parse(reward));
			}
		}

		public void didFailToLoadUrl(string url)
		{
			if (ChartboostAndroidManager.didFailToLoadUrlEvent != null)
			{
				ChartboostAndroidManager.didFailToLoadUrlEvent(url);
			}
		}
	}
}
