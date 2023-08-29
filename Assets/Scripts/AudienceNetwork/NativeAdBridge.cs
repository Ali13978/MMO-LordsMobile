using System.Collections.Generic;
using UnityEngine;

namespace AudienceNetwork
{
	internal class NativeAdBridge : INativeAdBridge
	{
		internal static readonly string source;

		public static readonly INativeAdBridge Instance;

		private FBNativeAdBridgeCallback onLoadCallback;

		private FBNativeAdBridgeCallback onImpressionCallback;

		private FBNativeAdBridgeCallback onClickCallback;

		private List<NativeAd> nativeAds = new List<NativeAd>();

		internal NativeAdBridge()
		{
		}

		static NativeAdBridge()
		{
			source = "AudienceNetworkUnityBridge " + SdkVersion.Build + " (Unity " + Application.unityVersion + ")";
			Instance = createInstance();
		}

		private static INativeAdBridge createInstance()
		{
			if (Application.platform != 0)
			{
				return new NativeAdBridgeAndroid();
			}
			return new NativeAdBridge();
		}

		public virtual int Create(string placementId, NativeAd nativeAd)
		{
			nativeAds.Add(nativeAd);
			return nativeAds.Count - 1;
		}

		public virtual int Load(int uniqueId)
		{
			NativeAd nativeAd = nativeAds[uniqueId];
			nativeAd.loadAdFromData();
			onLoadCallback?.Invoke();
			return uniqueId;
		}

		public virtual bool IsValid(int uniqueId)
		{
			return true;
		}

		public virtual string GetTitle(int uniqueId)
		{
			return "Facebook Test Ad";
		}

		public virtual string GetSubtitle(int uniqueId)
		{
			return "An ad for Facebook";
		}

		public virtual string GetBody(int uniqueId)
		{
			return "Your ad integration works. Woohoo!";
		}

		public virtual string GetCallToAction(int uniqueId)
		{
			return "Install Now";
		}

		public virtual string GetSocialContext(int uniqueId)
		{
			return "Available on the App Store";
		}

		public virtual string GetIconImageURL(int uniqueId)
		{
			return "https://www.facebook.com/images/ad_network/audience_network_icon.png";
		}

		public virtual string GetCoverImageURL(int uniqueId)
		{
			return "https://www.facebook.com/images/ad_network/audience_network_test_cover.png";
		}

		public virtual string GetAdChoicesImageURL(int uniqueId)
		{
			return string.Empty;
		}

		public virtual string GetAdChoicesText(int uniqueId)
		{
			return string.Empty;
		}

		public virtual string GetAdChoicesLinkURL(int uniqueId)
		{
			return string.Empty;
		}

		public virtual int GetMinViewabilityPercentage(int uniqueId)
		{
			return 1;
		}

		public virtual void ManualLogImpression(int uniqueId)
		{
			onImpressionCallback?.Invoke();
		}

		public virtual void ManualLogClick(int uniqueId)
		{
			onClickCallback?.Invoke();
		}

		public virtual void ExternalLogImpression(int uniqueId)
		{
			onImpressionCallback?.Invoke();
		}

		public virtual void ExternalLogClick(int uniqueId)
		{
			onClickCallback?.Invoke();
		}

		public virtual void Release(int uniqueId)
		{
		}

		public virtual void OnLoad(int uniqueId, FBNativeAdBridgeCallback callback)
		{
			onLoadCallback = callback;
		}

		public virtual void OnImpression(int uniqueId, FBNativeAdBridgeCallback callback)
		{
			onImpressionCallback = callback;
		}

		public virtual void OnClick(int uniqueId, FBNativeAdBridgeCallback callback)
		{
			onClickCallback = callback;
		}

		public virtual void OnError(int uniqueId, FBNativeAdBridgeErrorCallback callback)
		{
		}

		public virtual void OnFinishedClick(int uniqueId, FBNativeAdBridgeCallback callback)
		{
		}
	}
}
