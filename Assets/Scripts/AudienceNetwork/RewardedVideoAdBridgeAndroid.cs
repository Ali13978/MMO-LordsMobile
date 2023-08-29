using AudienceNetwork.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AudienceNetwork
{
	internal class RewardedVideoAdBridgeAndroid : RewardedVideoAdBridge
	{
		private static Dictionary<int, RewardedVideoAdContainer> rewardedVideoAds = new Dictionary<int, RewardedVideoAdContainer>();

		private static int lastKey = 0;

		private AndroidJavaObject rewardedVideoAdForUniqueId(int uniqueId)
		{
			RewardedVideoAdContainer value = null;
			if (rewardedVideoAds.TryGetValue(uniqueId, out value))
			{
				return value.bridgedRewardedVideoAd;
			}
			return null;
		}

		private RewardedVideoAdContainer rewardedVideoAdContainerForUniqueId(int uniqueId)
		{
			RewardedVideoAdContainer value = null;
			if (rewardedVideoAds.TryGetValue(uniqueId, out value))
			{
				return value;
			}
			return null;
		}

		private string getStringForuniqueId(int uniqueId, string method)
		{
			return rewardedVideoAdForUniqueId(uniqueId)?.Call<string>(method, new object[0]);
		}

		private string getImageURLForuniqueId(int uniqueId, string method)
		{
			AndroidJavaObject androidJavaObject = rewardedVideoAdForUniqueId(uniqueId);
			if (androidJavaObject != null)
			{
				AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>(method, new object[0]);
				if (androidJavaObject2 != null)
				{
					return androidJavaObject2.Call<string>("getUrl", new object[0]);
				}
			}
			return null;
		}

		public override int Create(string placementId, RewardedVideoAd rewardedVideoAd)
		{
			AdUtility.prepare();
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
			AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("com.facebook.ads.RewardedVideoAd", androidJavaObject, placementId);
			RewardedVideoAdBridgeListenerProxy rewardedVideoAdBridgeListenerProxy = new RewardedVideoAdBridgeListenerProxy(rewardedVideoAd, androidJavaObject2);
			androidJavaObject2.Call("setAdListener", rewardedVideoAdBridgeListenerProxy);
			RewardedVideoAdContainer rewardedVideoAdContainer = new RewardedVideoAdContainer(rewardedVideoAd);
			rewardedVideoAdContainer.bridgedRewardedVideoAd = androidJavaObject2;
			rewardedVideoAdContainer.listenerProxy = rewardedVideoAdBridgeListenerProxy;
			int num = lastKey;
			rewardedVideoAds.Add(num, rewardedVideoAdContainer);
			lastKey++;
			return num;
		}

		public override int Load(int uniqueId)
		{
			AdUtility.prepare();
			rewardedVideoAdForUniqueId(uniqueId)?.Call("loadAd");
			return uniqueId;
		}

		public override bool IsValid(int uniqueId)
		{
			return rewardedVideoAdForUniqueId(uniqueId)?.Call<bool>("isAdLoaded", new object[0]) ?? false;
		}

		public override bool Show(int uniqueId)
		{
			RewardedVideoAdContainer rewardedVideoAdContainer = rewardedVideoAdContainerForUniqueId(uniqueId);
			AndroidJavaObject rewardedVideoAd = rewardedVideoAdForUniqueId(uniqueId);
			rewardedVideoAdContainer.rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd != null)
				{
					rewardedVideoAd.Call<bool>("show", new object[0]);
				}
			});
			return true;
		}

		public override void Release(int uniqueId)
		{
			rewardedVideoAdForUniqueId(uniqueId)?.Call("destroy");
			rewardedVideoAds.Remove(uniqueId);
		}

		public override void OnLoad(int uniqueId, FBRewardedVideoAdBridgeCallback callback)
		{
		}

		public override void OnImpression(int uniqueId, FBRewardedVideoAdBridgeCallback callback)
		{
		}

		public override void OnClick(int uniqueId, FBRewardedVideoAdBridgeCallback callback)
		{
		}

		public override void OnError(int uniqueId, FBRewardedVideoAdBridgeErrorCallback callback)
		{
		}

		public override void OnWillClose(int uniqueId, FBRewardedVideoAdBridgeCallback callback)
		{
		}

		public override void OnDidClose(int uniqueId, FBRewardedVideoAdBridgeCallback callback)
		{
		}
	}
}
