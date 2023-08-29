using AudienceNetwork.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AudienceNetwork
{
	internal class NativeAdBridgeAndroid : NativeAdBridge
	{
		private static Dictionary<int, NativeAdContainer> nativeAds = new Dictionary<int, NativeAdContainer>();

		private static int lastKey = 0;

		private AndroidJavaObject nativeAdForNativeAdId(int uniqueId)
		{
			NativeAdContainer value = null;
			if (nativeAds.TryGetValue(uniqueId, out value))
			{
				return value.bridgedNativeAd;
			}
			return null;
		}

		private string getStringForNativeAdId(int uniqueId, string method)
		{
			return nativeAdForNativeAdId(uniqueId)?.Call<string>(method, new object[0]);
		}

		private string getImageURLForNativeAdId(int uniqueId, string method)
		{
			AndroidJavaObject androidJavaObject = nativeAdForNativeAdId(uniqueId);
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

		public override int Create(string placementId, NativeAd nativeAd)
		{
			AdUtility.prepare();
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
			AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("com.facebook.ads.NativeAd", androidJavaObject, placementId);
			NativeAdBridgeListenerProxy nativeAdBridgeListenerProxy = new NativeAdBridgeListenerProxy(nativeAd, androidJavaObject2);
			androidJavaObject2.Call("setAdListener", nativeAdBridgeListenerProxy);
			NativeAdContainer nativeAdContainer = new NativeAdContainer(nativeAd);
			nativeAdContainer.bridgedNativeAd = androidJavaObject2;
			nativeAdContainer.listenerProxy = nativeAdBridgeListenerProxy;
			int num = lastKey;
			nativeAds.Add(num, nativeAdContainer);
			lastKey++;
			return num;
		}

		public override int Load(int uniqueId)
		{
			AdUtility.prepare();
			AndroidJavaObject androidJavaObject = nativeAdForNativeAdId(uniqueId);
			if (androidJavaObject != null)
			{
				androidJavaObject.Call("registerExternalLogReceiver", NativeAdBridge.source);
				androidJavaObject.Call("loadAd");
			}
			return uniqueId;
		}

		public override bool IsValid(int uniqueId)
		{
			return nativeAdForNativeAdId(uniqueId)?.Call<bool>("isAdLoaded", new object[0]) ?? false;
		}

		public override string GetTitle(int uniqueId)
		{
			return getStringForNativeAdId(uniqueId, "getAdTitle");
		}

		public override string GetSubtitle(int uniqueId)
		{
			return getStringForNativeAdId(uniqueId, "getAdSubtitle");
		}

		public override string GetBody(int uniqueId)
		{
			return getStringForNativeAdId(uniqueId, "getAdBody");
		}

		public override string GetCallToAction(int uniqueId)
		{
			return getStringForNativeAdId(uniqueId, "getAdCallToAction");
		}

		public override string GetSocialContext(int uniqueId)
		{
			return getStringForNativeAdId(uniqueId, "getAdSocialContext");
		}

		public override string GetIconImageURL(int uniqueId)
		{
			return getImageURLForNativeAdId(uniqueId, "getAdIcon");
		}

		public override string GetCoverImageURL(int uniqueId)
		{
			return getImageURLForNativeAdId(uniqueId, "getAdCoverImage");
		}

		public override string GetAdChoicesImageURL(int uniqueId)
		{
			return getImageURLForNativeAdId(uniqueId, "getAdChoicesIcon");
		}

		public override string GetAdChoicesText(int uniqueId)
		{
			return getStringForNativeAdId(uniqueId, "getAdChoicesText");
		}

		public override string GetAdChoicesLinkURL(int uniqueId)
		{
			return getStringForNativeAdId(uniqueId, "getAdChoicesLinkUrl");
		}

		public override int GetMinViewabilityPercentage(int uniqueId)
		{
			return nativeAdForNativeAdId(uniqueId)?.Call<int>("getMinViewabilityPercentage", new object[0]) ?? 1;
		}

		private string getId(int uniqueId)
		{
			return nativeAdForNativeAdId(uniqueId)?.Call<string>("getId", new object[0]);
		}

		public override void ManualLogImpression(int uniqueId)
		{
			AndroidJavaObject androidJavaObject = nativeAdForNativeAdId(uniqueId);
			if (androidJavaObject != null)
			{
				sendIntentToBroadcastManager(uniqueId, "com.facebook.ads.native.impression");
			}
		}

		public override void ManualLogClick(int uniqueId)
		{
			AndroidJavaObject androidJavaObject = nativeAdForNativeAdId(uniqueId);
			if (androidJavaObject != null)
			{
				sendIntentToBroadcastManager(uniqueId, "com.facebook.ads.native.click");
			}
		}

		public override void ExternalLogImpression(int uniqueId)
		{
			nativeAdForNativeAdId(uniqueId)?.Call("logExternalImpression");
		}

		public override void ExternalLogClick(int uniqueId)
		{
			nativeAdForNativeAdId(uniqueId)?.Call("logExternalClick", NativeAdBridge.source);
		}

		private bool sendIntentToBroadcastManager(int uniqueId, string intent)
		{
			if (intent != null)
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", intent + ":" + getId(uniqueId));
				AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.support.v4.content.LocalBroadcastManager");
				AndroidJavaObject androidJavaObject2 = androidJavaClass2.CallStatic<AndroidJavaObject>("getInstance", new object[1]
				{
					@static
				});
				return androidJavaObject2.Call<bool>("sendBroadcast", new object[1]
				{
					androidJavaObject
				});
			}
			return false;
		}

		public override void Release(int uniqueId)
		{
			nativeAds.Remove(uniqueId);
		}

		public override void OnLoad(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public override void OnImpression(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public override void OnClick(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}

		public override void OnError(int uniqueId, FBNativeAdBridgeErrorCallback callback)
		{
		}

		public override void OnFinishedClick(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}
	}
}
