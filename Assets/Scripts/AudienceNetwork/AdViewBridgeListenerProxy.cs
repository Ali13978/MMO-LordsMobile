using UnityEngine;

namespace AudienceNetwork
{
	internal class AdViewBridgeListenerProxy : AndroidJavaProxy
	{
		private AdView adView;

		private AndroidJavaObject bridgedAdView;

		public AdViewBridgeListenerProxy(AdView adView, AndroidJavaObject bridgedAdView)
			: base("com.facebook.ads.AdListener")
		{
			this.adView = adView;
			this.bridgedAdView = bridgedAdView;
		}

		private void onError(AndroidJavaObject ad, AndroidJavaObject error)
		{
			string errorMessage = error.Call<string>("getErrorMessage", new object[0]);
			adView.executeOnMainThread(delegate
			{
				if (adView.AdViewDidFailWithError != null)
				{
					adView.AdViewDidFailWithError(errorMessage);
				}
			});
		}

		private void onAdLoaded(AndroidJavaObject ad)
		{
			adView.executeOnMainThread(delegate
			{
				if (adView.AdViewDidLoad != null)
				{
					adView.AdViewDidLoad();
				}
			});
		}

		private void onAdClicked(AndroidJavaObject ad)
		{
			adView.executeOnMainThread(delegate
			{
				if (adView.AdViewDidClick != null)
				{
					adView.AdViewDidClick();
				}
			});
		}

		private void onLoggingImpression(AndroidJavaObject ad)
		{
			adView.executeOnMainThread(delegate
			{
				if (adView.AdViewWillLogImpression != null)
				{
					adView.AdViewWillLogImpression();
				}
			});
		}
	}
}
