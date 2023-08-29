using UnityEngine;

namespace AudienceNetwork
{
	internal class RewardedVideoAdBridgeListenerProxy : AndroidJavaProxy
	{
		private RewardedVideoAd rewardedVideoAd;

		private AndroidJavaObject bridgedRewardedVideoAd;

		public RewardedVideoAdBridgeListenerProxy(RewardedVideoAd rewardedVideoAd, AndroidJavaObject bridgedRewardedVideoAd)
			: base("com.facebook.ads.S2SRewardedVideoAdListener")
		{
			this.rewardedVideoAd = rewardedVideoAd;
			this.bridgedRewardedVideoAd = bridgedRewardedVideoAd;
		}

		private void onError(AndroidJavaObject ad, AndroidJavaObject error)
		{
			string errorMessage = error.Call<string>("getErrorMessage", new object[0]);
			rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd.RewardedVideoAdDidFailWithError != null)
				{
					rewardedVideoAd.RewardedVideoAdDidFailWithError(errorMessage);
				}
			});
		}

		private void onAdLoaded(AndroidJavaObject ad)
		{
			rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd.RewardedVideoAdDidLoad != null)
				{
					rewardedVideoAd.RewardedVideoAdDidLoad();
				}
			});
		}

		private void onAdClicked(AndroidJavaObject ad)
		{
			rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd.RewardedVideoAdDidClick != null)
				{
					rewardedVideoAd.RewardedVideoAdDidClick();
				}
			});
		}

		private void onRewardedVideoDisplayed(AndroidJavaObject ad)
		{
			rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd.RewardedVideoAdWillLogImpression != null)
				{
					rewardedVideoAd.RewardedVideoAdWillLogImpression();
				}
			});
		}

		private void onRewardedVideoClosed()
		{
			rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd.RewardedVideoAdDidClose != null)
				{
					rewardedVideoAd.RewardedVideoAdDidClose();
				}
			});
		}

		private void onRewardedVideoCompleted()
		{
			rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd.RewardedVideoAdComplete != null)
				{
					rewardedVideoAd.RewardedVideoAdComplete();
				}
			});
		}

		private void onRewardServerSuccess()
		{
			rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd.RewardedVideoAdDidSucceed != null)
				{
					rewardedVideoAd.RewardedVideoAdDidSucceed();
				}
			});
		}

		private void onRewardServerFailed()
		{
			rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd.RewardedVideoAdDidFail != null)
				{
					rewardedVideoAd.RewardedVideoAdDidFail();
				}
			});
		}

		private void onLoggingImpression(AndroidJavaObject ad)
		{
			rewardedVideoAd.executeOnMainThread(delegate
			{
				if (rewardedVideoAd.RewardedVideoAdWillLogImpression != null)
				{
					rewardedVideoAd.RewardedVideoAdWillLogImpression();
				}
			});
		}
	}
}
