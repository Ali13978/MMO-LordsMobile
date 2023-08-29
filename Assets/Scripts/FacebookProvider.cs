using AudienceNetwork;
using AudienceNetwork.Utility;
using System;
using UnityEngine;

public class FacebookProvider : IBanners, IInterstitial
{
	private string interstitialID;

	private string bannerID;

	private InterstitialAd interstitialAd;

	private AdView bannerAd;

	private GameObject recipientObject;

	private bool isLoaded;

	private bool isBeingShown;

	private bool isLoadingBanner;

	private bool isLoadedBanner;

	private bool isBeingShownBanner;

	private bool isDebugEnabled;

	private StationEngine stationEngine;

	private StationEngineAds stationEngineAds;

	private StationEngineFirebase.AnalyticsAdsPosition lastInterstitialPosition;

	private StationEngineFirebase.AnalyticsAdsPosition lastBannerPosition;

	private void OnDestroy()
	{
		if (interstitialAd != null)
		{
			interstitialAd.Dispose();
		}
	}

	public void InitializeBanner(StationEngine _stationEngine, string _bannerID, bool isDebugEnabled, GameObject _recipientObject)
	{
		bannerID = _bannerID;
		stationEngine = _stationEngine;
		this.isDebugEnabled = isDebugEnabled;
		isLoadingBanner = false;
		isLoadedBanner = false;
		recipientObject = _recipientObject;
		RequestBanner();
	}

	public bool CheckReadyBanner()
	{
		return isLoadedBanner;
	}

	public void CheckRoutineBanner()
	{
		bool flag = CheckReadyBanner();
		if (!flag)
		{
			RequestBanner();
		}
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Routine check BANNER: " + flag.ToString());
		}
	}

	private void RequestBanner()
	{
		if (!isBeingShownBanner && !isLoadingBanner)
		{
			isLoadingBanner = true;
			bannerAd = new AdView(bannerID, AdSize.BANNER_HEIGHT_50);
			bannerAd.Register(recipientObject);
			bannerAd.AdViewDidLoad = delegate
			{
				isLoadedBanner = true;
				isLoadingBanner = false;
				if (isDebugEnabled)
				{
					stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Banner loaded");
				}
			};
			bannerAd.AdViewDidFailWithError = delegate(string error)
			{
				isLoadingBanner = false;
				if (isDebugEnabled)
				{
					stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Banner error: " + error);
				}
			};
			bannerAd.AdViewWillLogImpression = delegate
			{
				if (isDebugEnabled)
				{
					stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Banner shown");
				}
			};
			bannerAd.AdViewDidClick = delegate
			{
				if (isDebugEnabled)
				{
					stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Banner clicked");
				}
				stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.Banner, StationEngineFirebase.AnalyticsAdsAction.Click, lastBannerPosition, StationEngineFirebase.AnalyticsAdsProvider.Facebook);
			};
			bannerAd.LoadAd();
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Requesting banner");
			}
		}
	}

	public void ShowBanner(StationEngineFirebase.AnalyticsAdsPosition _position, AdsBannerPosition _bannerPosition)
	{
		if (isLoadedBanner)
		{
			if (_bannerPosition == AdsBannerPosition.Top)
			{
				bannerAd.Show(0.0);
			}
			else
			{
				int num = (int)AdUtility.height() - 50;
				bannerAd.Show(num);
			}
			isLoadedBanner = false;
			isBeingShownBanner = true;
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Showing BANNER");
			}
			lastBannerPosition = _position;
			stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.Banner, StationEngineFirebase.AnalyticsAdsAction.Impression, lastBannerPosition, StationEngineFirebase.AnalyticsAdsProvider.Facebook);
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Trying to show BANNER but no BANNER could be found");
		}
	}

	public void DestroyBanner(bool _requestBanner)
	{
		if (bannerAd != null)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Destroying Banner");
			}
			bannerAd.Dispose();
			isLoadingBanner = false;
			isLoadedBanner = false;
			isBeingShownBanner = false;
			bannerAd = null;
			if (_requestBanner)
			{
				RequestBanner();
			}
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Trying to destroy Banner but no Banner could be found...");
		}
	}

	public void HideBanner()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Trying to hide but function not available");
		}
		if (bannerAd != null && isBeingShownBanner)
		{
			DestroyBanner(_requestBanner: true);
		}
	}

	public void HandleBannerAdOpened(object sender, EventArgs args)
	{
		throw new NotImplementedException();
	}

	public void HandleBannerAdLeavingApplication(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}

	public void InitializeInterstitial(StationEngine _stationEngine, StationEngineAds _stationEngineAds, string _interstitialID, bool isDebugEnabled, GameObject _recipientObject, string appID = null)
	{
		interstitialID = _interstitialID;
		stationEngine = _stationEngine;
		stationEngineAds = _stationEngineAds;
		this.isDebugEnabled = isDebugEnabled;
		isLoaded = false;
		recipientObject = _recipientObject;
		if (interstitialID != null)
		{
			if (this.isDebugEnabled)
			{
				stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Initializing Interstitial");
			}
			interstitialAd = new InterstitialAd(interstitialID);
			interstitialAd.Register(recipientObject);
			interstitialAd.InterstitialAdDidLoad = delegate
			{
				if (this.isDebugEnabled)
				{
					stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Interstitial loaded");
				}
				isLoaded = true;
			};
			interstitialAd.InterstitialAdDidFailWithError = delegate(string error)
			{
				if (this.isDebugEnabled)
				{
					stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Interstitial failed to load, error: " + error);
				}
			};
			interstitialAd.InterstitialAdWillLogImpression = delegate
			{
				if (this.isDebugEnabled)
				{
					stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Interstitial logged impression");
				}
			};
			interstitialAd.InterstitialAdDidClick = delegate
			{
				if (this.isDebugEnabled)
				{
					stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Interstitial clicked");
				}
				isBeingShown = false;
				stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.Interstitial, StationEngineFirebase.AnalyticsAdsAction.Click, lastInterstitialPosition, StationEngineFirebase.AnalyticsAdsProvider.Facebook);
				RequestInterstitial();
			};
			interstitialAd.InterstitialAdDidClose = delegate
			{
				if (this.isDebugEnabled)
				{
					stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Interstitial closed");
				}
				isBeingShown = false;
				RequestInterstitial();
			};
			RequestInterstitial();
		}
	}

	public bool CheckReadyInterstitial()
	{
		return isLoaded;
	}

	public void ShowInterstitial(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		if (CheckReadyInterstitial())
		{
			stationEngineAds.SetInterstitialStatus(StationEngineAds.InterstitialStatus.PLAYING);
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Showing Interstitial");
			}
			interstitialAd.Show();
			isLoaded = false;
			isBeingShown = true;
			lastInterstitialPosition = _position;
			stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.Interstitial, StationEngineFirebase.AnalyticsAdsAction.Impression, lastInterstitialPosition, StationEngineFirebase.AnalyticsAdsProvider.Facebook);
			RequestInterstitial();
		}
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
			stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Routine check INTERSTITIAL: " + flag.ToString());
		}
	}

	public void RequestInterstitial()
	{
		if (!isBeingShown)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Requesting Interstitial");
			}
			interstitialAd.LoadAd();
		}
	}

	public void DestroyInterstitial()
	{
		if (interstitialAd != null)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Destroying Interstitial");
			}
			interstitialAd.Dispose();
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("FB AUDIENCE NETWORK - Trying to destroy Interstitial but no Interstitial could be found...");
		}
	}
}
