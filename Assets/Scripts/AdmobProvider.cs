using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdmobProvider : IBanners, IInterstitial, IRewardedVideo
{
	private string bannerID;

	private string interstitialID;

	private string videoRewardUnitID;

	private AdsBannerPosition oldPositionBanner;

	private bool isDebugEnabled;

	private BannerView bannerAdmob;

	private InterstitialAd interstitialAdmob;

	private RewardBasedVideoAd videoRewardAdmob;

	private StationEngine stationEngine;

	private StationEngineAds stationEngineAds;

	private StationEngineFirebase.AnalyticsAdsPosition lastPositionBanner;

	private StationEngineFirebase.AnalyticsAdsPosition lastPositionInterstitial;

	private StationEngineFirebase.AnalyticsAdsPosition lastPositionVideoReward;

	public void InitializeBanner(StationEngine _stationEngine, string _bannerID, bool isDebugEnabled, GameObject recipient = null)
	{
		bannerID = _bannerID;
		stationEngine = _stationEngine;
		this.isDebugEnabled = isDebugEnabled;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB BANNER - Initializing Banner");
		}
	}

	public void ShowBanner(StationEngineFirebase.AnalyticsAdsPosition _position, AdsBannerPosition _bannerPosition)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB BANNER - Requesting Banner");
		}
		if (bannerAdmob != null && _bannerPosition == oldPositionBanner)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("ADMOB BANNER - Unhiding existing Banner first");
			}
			bannerAdmob.Show();
			return;
		}
		if (bannerAdmob != null)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("ADMOB BANNER - Destroying existing Banner first");
			}
			DestroyBanner();
		}
		oldPositionBanner = _bannerPosition;
		switch (_bannerPosition)
		{
		case AdsBannerPosition.Bottom:
			bannerAdmob = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Bottom);
			break;
		case AdsBannerPosition.Top:
			bannerAdmob = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Top);
			break;
		default:
			bannerAdmob = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Bottom);
			break;
		}
		bannerAdmob.OnAdOpening += HandleBannerAdOpened;
		bannerAdmob.OnAdLeavingApplication += HandleBannerAdLeavingApplication;
		AdRequest request = new AdRequest.Builder().Build();
		bannerAdmob.LoadAd(request);
		lastPositionBanner = _position;
		stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.Banner, StationEngineFirebase.AnalyticsAdsAction.Impression, lastPositionBanner, StationEngineFirebase.AnalyticsAdsProvider.Admob);
	}

	public void DestroyBanner(bool requestBanner = false)
	{
		if (bannerAdmob != null)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("ADMOB BANNER - Destroying Banner");
			}
			bannerAdmob.Destroy();
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB BANNER - Trying to destroy Banner but no banner could be found...");
		}
	}

	public void HideBanner()
	{
		if (bannerAdmob != null)
		{
			bannerAdmob.Hide();
		}
	}

	public void HandleBannerAdOpened(object sender, EventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB BANNER - OPENED");
		}
	}

	public void HandleBannerAdLeavingApplication(object sender, EventArgs e)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB BANNER - CLICK");
		}
		stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.Banner, StationEngineFirebase.AnalyticsAdsAction.Click, lastPositionBanner, StationEngineFirebase.AnalyticsAdsProvider.Admob);
	}

	public void InitializeInterstitial(StationEngine _stationEngine, StationEngineAds _stationEngineAds, string _interstitialID, bool isDebugEnabled, GameObject recipient = null, string appID = null)
	{
		interstitialID = _interstitialID;
		stationEngine = _stationEngine;
		stationEngineAds = _stationEngineAds;
		this.isDebugEnabled = isDebugEnabled;
		if (interstitialID != null)
		{
			if (this.isDebugEnabled)
			{
				stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - Initializing Interstitial");
			}
			RequestInterstitial();
		}
	}

	public void ShowInterstitial(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		if (CheckReadyInterstitial())
		{
			stationEngineAds.SetInterstitialStatus(StationEngineAds.InterstitialStatus.PLAYING);
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - Showing Interstitial");
			}
			interstitialAdmob.Show();
			lastPositionInterstitial = _position;
			stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.Interstitial, StationEngineFirebase.AnalyticsAdsAction.Impression, lastPositionInterstitial, StationEngineFirebase.AnalyticsAdsProvider.Admob);
			RequestInterstitial();
		}
	}

	public void RequestInterstitial()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - Requesting Interstitial");
		}
		interstitialAdmob = new InterstitialAd(interstitialID);
		interstitialAdmob.OnAdLoaded += HandleAdLoaded;
		interstitialAdmob.OnAdFailedToLoad += HandleAdmobFailedToLoad;
		interstitialAdmob.OnAdOpening += HandleAdOpened;
		interstitialAdmob.OnAdClosed += HandleAdClosed;
		interstitialAdmob.OnAdLeavingApplication += HandleAdLeftApplication;
		AdRequest request = new AdRequest.Builder().Build();
		interstitialAdmob.LoadAd(request);
	}

	public bool CheckReadyInterstitial()
	{
		return interstitialAdmob.IsLoaded();
	}

	public void CheckRoutineInterstitial()
	{
		bool flag = CheckReadyInterstitial();
		if (!flag)
		{
			RequestInterstitial();
		}
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - Routine check: " + flag.ToString());
		}
	}

	public void DestroyInterstitial()
	{
		if (interstitialAdmob != null)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - Destroying Interstitial");
			}
			interstitialAdmob.Destroy();
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - Trying to destroy Interstitial but no Interstitial could be found...");
		}
	}

	public void HandleAdmobFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - FAILED");
		}
	}

	public void HandleAdLoaded(object sender, EventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - LOADED");
		}
	}

	public void HandleAdOpened(object sender, EventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - OPENED");
		}
	}

	public void HandleAdClosed(object sender, EventArgs args)
	{
		stationEngineAds.SetInterstitialStatus(StationEngineAds.InterstitialStatus.IDLE);
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - CLOSED");
		}
	}

	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB INTERSTITIAL - LEFT");
		}
		stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.Interstitial, StationEngineFirebase.AnalyticsAdsAction.Click, lastPositionInterstitial, StationEngineFirebase.AnalyticsAdsProvider.Admob);
	}

	public void InitializeRewardedVideo(StationEngine _stationEngine, StationEngineAds _stationEngineAds, string _videoRewardUnitID, bool isDebugEnabled, string appID = null)
	{
		videoRewardUnitID = _videoRewardUnitID;
		stationEngine = _stationEngine;
		stationEngineAds = _stationEngineAds;
		this.isDebugEnabled = isDebugEnabled;
		videoRewardAdmob = RewardBasedVideoAd.Instance;
		videoRewardAdmob.OnAdLoaded += HandleRewardBasedVideoLoaded;
		videoRewardAdmob.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		videoRewardAdmob.OnAdOpening += HandleRewardBasedVideoOpened;
		videoRewardAdmob.OnAdStarted += HandleRewardBasedVideoStarted;
		videoRewardAdmob.OnAdRewarded += HandleRewardBasedVideoRewarded;
		videoRewardAdmob.OnAdClosed += HandleRewardBasedVideoClosed;
		videoRewardAdmob.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VIDEO REWARD - Initialized VideoReward");
		}
		PreloadVideoReward();
	}

	public void PreloadVideoReward()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB - Requesting Video Reward");
		}
		AdRequest request = new AdRequest.Builder().Build();
		videoRewardAdmob.LoadAd(request, videoRewardUnitID);
	}

	public void ShowRewardedVideo(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		if (videoRewardAdmob.IsLoaded())
		{
			stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.PLAYING);
			videoRewardAdmob.Show();
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("ADMOB - Showing Video Reward");
			}
			lastPositionVideoReward = _position;
			stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.VideoReward, StationEngineFirebase.AnalyticsAdsAction.Impression, lastPositionVideoReward, StationEngineFirebase.AnalyticsAdsProvider.Admob);
		}
	}

	public bool CheckVideoReady()
	{
		return videoRewardAdmob.IsLoaded();
	}

	public void CheckRoutineRewardedVideo()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("ADMOB - Routine check VIDEO REWARD: " + videoRewardAdmob.IsLoaded());
		}
		if (!videoRewardAdmob.IsLoaded())
		{
			PreloadVideoReward();
		}
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VIDEO REWARD (event) - LOADED");
		}
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VIDEO REWARD (event) - FAILED: " + args.Message);
		}
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VIDEO REWARD (event) - OPENED");
		}
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VIDEO REWARD (event) - STARTED");
		}
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		PreloadVideoReward();
		if (stationEngineAds.GetVideoRewardStatus() != StationEngineAds.VideoRewardStatus.COMPLETED)
		{
			stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.SKIPPED);
			stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.VideoReward, StationEngineFirebase.AnalyticsAdsAction.Skip, lastPositionVideoReward, StationEngineFirebase.AnalyticsAdsProvider.Admob);
		}
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VIDEO REWARD (event) - CLOSED");
		}
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.COMPLETED);
		stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.VideoReward, StationEngineFirebase.AnalyticsAdsAction.Completed, lastPositionVideoReward, StationEngineFirebase.AnalyticsAdsProvider.Admob);
		string type = args.Type;
		double amount = args.Amount;
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VIDEO REWARD (event) - REWARDED");
		}
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VIDEO REWARD (event) - LEFT APPLICATION");
		}
	}
}
