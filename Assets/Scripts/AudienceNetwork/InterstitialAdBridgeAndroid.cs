using AudienceNetwork.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AudienceNetwork
{
	internal class InterstitialAdBridgeAndroid : InterstitialAdBridge
	{
		private static Dictionary<int, InterstitialAdContainer> interstitialAds = new Dictionary<int, InterstitialAdContainer>();

		private static int lastKey = 0;

		private AndroidJavaObject interstitialAdForuniqueId(int uniqueId)
		{
			InterstitialAdContainer value = null;
			if (interstitialAds.TryGetValue(uniqueId, out value))
			{
				return value.bridgedInterstitialAd;
			}
			return null;
		}

		private string getStringForuniqueId(int uniqueId, string method)
		{
			return interstitialAdForuniqueId(uniqueId)?.Call<string>(method, new object[0]);
		}

		private string getImageURLForuniqueId(int uniqueId, string method)
		{
			AndroidJavaObject androidJavaObject = interstitialAdForuniqueId(uniqueId);
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

		public override int Create(string placementId, InterstitialAd interstitialAd)
		{
			AdUtility.prepare();
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
			AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("com.facebook.ads.InterstitialAd", androidJavaObject, placementId);
			InterstitialAdBridgeListenerProxy interstitialAdBridgeListenerProxy = new InterstitialAdBridgeListenerProxy(interstitialAd, androidJavaObject2);
			androidJavaObject2.Call("setAdListener", interstitialAdBridgeListenerProxy);
			InterstitialAdContainer interstitialAdContainer = new InterstitialAdContainer(interstitialAd);
			interstitialAdContainer.bridgedInterstitialAd = androidJavaObject2;
			interstitialAdContainer.listenerProxy = interstitialAdBridgeListenerProxy;
			int num = lastKey;
			interstitialAds.Add(num, interstitialAdContainer);
			lastKey++;
			return num;
		}

		public override int Load(int uniqueId)
		{
			AdUtility.prepare();
			interstitialAdForuniqueId(uniqueId)?.Call("loadAd");
			return uniqueId;
		}

		public override bool IsValid(int uniqueId)
		{
			return interstitialAdForuniqueId(uniqueId)?.Call<bool>("isAdLoaded", new object[0]) ?? false;
		}

		public override bool Show(int uniqueId)
		{
			return interstitialAdForuniqueId(uniqueId)?.Call<bool>("show", new object[0]) ?? false;
		}

		public override void Release(int uniqueId)
		{
			interstitialAdForuniqueId(uniqueId)?.Call("destroy");
			interstitialAds.Remove(uniqueId);
		}

		public override void OnLoad(int uniqueId, FBInterstitialAdBridgeCallback callback)
		{
		}

		public override void OnImpression(int uniqueId, FBInterstitialAdBridgeCallback callback)
		{
		}

		public override void OnClick(int uniqueId, FBInterstitialAdBridgeCallback callback)
		{
		}

		public override void OnError(int uniqueId, FBInterstitialAdBridgeErrorCallback callback)
		{
		}

		public override void OnWillClose(int uniqueId, FBInterstitialAdBridgeCallback callback)
		{
		}

		public override void OnDidClose(int uniqueId, FBInterstitialAdBridgeCallback callback)
		{
		}
	}
}
