using UnityEngine;

namespace AudienceNetwork
{
	internal class InterstitialAdBridgeListenerProxy : AndroidJavaProxy
	{
		private InterstitialAd interstitialAd;

		private AndroidJavaObject bridgedInterstitialAd;

		public InterstitialAdBridgeListenerProxy(InterstitialAd interstitialAd, AndroidJavaObject bridgedInterstitialAd)
			: base("com.facebook.ads.InterstitialAdListener")
		{
			this.interstitialAd = interstitialAd;
			this.bridgedInterstitialAd = bridgedInterstitialAd;
		}

		private void onError(AndroidJavaObject ad, AndroidJavaObject error)
		{
			string errorMessage = error.Call<string>("getErrorMessage", new object[0]);
			interstitialAd.executeOnMainThread(delegate
			{
				if (interstitialAd.InterstitialAdDidFailWithError != null)
				{
					interstitialAd.InterstitialAdDidFailWithError(errorMessage);
				}
			});
		}

		private void onAdLoaded(AndroidJavaObject ad)
		{
			interstitialAd.executeOnMainThread(delegate
			{
				if (interstitialAd.InterstitialAdDidLoad != null)
				{
					interstitialAd.InterstitialAdDidLoad();
				}
			});
		}

		private void onAdClicked(AndroidJavaObject ad)
		{
			interstitialAd.executeOnMainThread(delegate
			{
				if (interstitialAd.InterstitialAdDidClick != null)
				{
					interstitialAd.InterstitialAdDidClick();
				}
			});
		}

		private void onInterstitialDisplayed(AndroidJavaObject ad)
		{
			interstitialAd.executeOnMainThread(delegate
			{
				if (interstitialAd.InterstitialAdWillLogImpression != null)
				{
					interstitialAd.InterstitialAdWillLogImpression();
				}
			});
		}

		private void onInterstitialDismissed(AndroidJavaObject ad)
		{
			interstitialAd.executeOnMainThread(delegate
			{
				if (interstitialAd.InterstitialAdDidClose != null)
				{
					interstitialAd.InterstitialAdDidClose();
				}
			});
		}

		private void onLoggingImpression(AndroidJavaObject ad)
		{
			interstitialAd.executeOnMainThread(delegate
			{
				if (interstitialAd.InterstitialAdWillLogImpression != null)
				{
					interstitialAd.InterstitialAdWillLogImpression();
				}
			});
		}
	}
}
