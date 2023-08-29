using UnityEngine;

namespace AudienceNetwork
{
	internal class NativeAdBridgeListenerProxy : AndroidJavaProxy
	{
		private NativeAd nativeAd;

		private AndroidJavaObject bridgedNativeAd;

		public NativeAdBridgeListenerProxy(NativeAd nativeAd, AndroidJavaObject bridgedNativeAd)
			: base("com.facebook.ads.AdListener")
		{
			this.nativeAd = nativeAd;
			this.bridgedNativeAd = bridgedNativeAd;
		}

		private void onError(AndroidJavaObject ad, AndroidJavaObject error)
		{
			string errorMessage = error.Call<string>("getErrorMessage", new object[0]);
			nativeAd.executeOnMainThread(delegate
			{
				if (nativeAd.NativeAdDidFailWithError != null)
				{
					nativeAd.NativeAdDidFailWithError(errorMessage);
				}
			});
		}

		private void onAdLoaded(AndroidJavaObject ad)
		{
			nativeAd.executeOnMainThread(delegate
			{
				nativeAd.loadAdFromData();
				if (nativeAd.NativeAdDidLoad != null)
				{
					nativeAd.NativeAdDidLoad();
				}
			});
		}

		private void onAdClicked(AndroidJavaObject ad)
		{
			nativeAd.executeOnMainThread(delegate
			{
				if (nativeAd.NativeAdDidClick != null)
				{
					nativeAd.NativeAdDidClick();
				}
			});
		}

		private void onLoggingImpression(AndroidJavaObject ad)
		{
			nativeAd.executeOnMainThread(delegate
			{
				if (nativeAd.NativeAdWillLogImpression != null)
				{
					nativeAd.NativeAdWillLogImpression();
				}
			});
		}
	}
}
