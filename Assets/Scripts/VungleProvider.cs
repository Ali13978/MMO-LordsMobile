using System;
using System.Collections.Generic;
using UnityEngine;

public class VungleProvider : IInterstitial, IRewardedVideo
{
	private string appID;

	private string placementVideoID;

	private string placementInterstitialID;

	private bool isDebugEnabled;

	private StationEngine stationEngine;

	private StationEngineAds stationEngineAds;

	private StationEngineFirebase.AnalyticsAdsPosition lastPosition;

	private Dictionary<string, bool> placements = new Dictionary<string, bool>();

	private string[] placementsArray;

	public void InitializeVungle(StationEngine _stationEngine, StationEngineAds stationEngineAds, string _interstitialID, string appID, string videoID, bool isDebugEnabled)
	{
		stationEngine = _stationEngine;
		this.stationEngineAds = stationEngineAds;
		this.isDebugEnabled = isDebugEnabled;
		InitializeInterstitial(_stationEngine, stationEngineAds, _interstitialID, this.isDebugEnabled, null, appID);
		InitializeRewardedVideo(_stationEngine, stationEngineAds, appID, this.isDebugEnabled, videoID);
		placementsArray = new string[placements.Keys.Count];
		placements.Keys.CopyTo(placementsArray, 0);
		Vungle.init(this.appID, placementsArray);
		if (placementInterstitialID != string.Empty)
		{
			RequestInterstitial();
		}
	}

	private void InitializeEventHandlers()
	{
		Vungle.onAdStartedEvent += delegate
		{
		};
		Vungle.onAdFinishedEvent += delegate(string placementID, AdFinishedEventArgs args)
		{
			if (placementID == placementVideoID)
			{
				if (stationEngineAds.GetVideoRewardStatus() == StationEngineAds.VideoRewardStatus.PLAYING)
				{
					if (args.IsCompletedView)
					{
						stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.VideoReward, StationEngineFirebase.AnalyticsAdsAction.Completed, lastPosition, StationEngineFirebase.AnalyticsAdsProvider.Vungle);
						stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.COMPLETED);
					}
					else
					{
						stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.VideoReward, StationEngineFirebase.AnalyticsAdsAction.Skip, lastPosition, StationEngineFirebase.AnalyticsAdsProvider.Vungle);
						stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.SKIPPED);
					}
				}
			}
			else if (placementID == placementInterstitialID)
			{
				stationEngineAds.SetInterstitialStatus(StationEngineAds.InterstitialStatus.IDLE);
				RequestInterstitial();
			}
		};
		Vungle.adPlayableEvent += delegate(string placementID, bool adPlayable)
		{
			if (adPlayable)
			{
				if (isDebugEnabled)
				{
					stationEngine.PostDebugInfo("VUNGLE - An ad is ready to show!");
				}
			}
			else if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("VUNGLE - No ad is available at this moment.");
			}
		};
		Vungle.onLogEvent += delegate
		{
		};
		Vungle.onInitializeEvent += delegate
		{
		};
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			Vungle.onPause();
		}
		else
		{
			Vungle.onResume();
		}
	}

	public void InitializeInterstitial(StationEngine _stationEngine, StationEngineAds _stationEngineAds, string _interstitialID, bool isDebugEnabled, GameObject recipient = null, string appID = null)
	{
		this.appID = appID;
		placementInterstitialID = _interstitialID;
		this.isDebugEnabled = isDebugEnabled;
		stationEngine = _stationEngine;
		stationEngineAds = _stationEngineAds;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VUNGLE - Interstitials Initializing");
		}
		InitializeEventHandlers();
		if (placementInterstitialID != string.Empty)
		{
			placements.Add(placementInterstitialID, value: false);
		}
	}

	public void DestroyInterstitial()
	{
		throw new NotImplementedException();
	}

	public bool CheckReadyInterstitial()
	{
		return Vungle.isAdvertAvailable(placementInterstitialID);
	}

	public void ShowInterstitial(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		if (CheckReadyInterstitial())
		{
			stationEngineAds.SetInterstitialStatus(StationEngineAds.InterstitialStatus.PLAYING);
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("VUNGLE - Showing Interstitial");
			}
			Dictionary<string, object> options = new Dictionary<string, object>();
			stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.Interstitial, StationEngineFirebase.AnalyticsAdsAction.Impression, _position, StationEngineFirebase.AnalyticsAdsProvider.Vungle);
			Vungle.playAd(options, placementInterstitialID);
		}
	}

	public void CheckRoutineInterstitial()
	{
		bool flag = CheckReadyInterstitial();
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VUNGLE - Routine check: " + flag.ToString());
		}
		if (!flag)
		{
			RequestInterstitial();
		}
	}

	public void RequestInterstitial()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VUNGLE INTERSTITIAL - Requesting Interstitial");
		}
		Vungle.loadAd(placementInterstitialID);
	}

	public void InitializeRewardedVideo(StationEngine _stationEngine, StationEngineAds _stationEngineAds, string placementVideoID, bool isDebugEnabled, string appID)
	{
		this.appID = appID;
		this.placementVideoID = placementVideoID;
		stationEngine = _stationEngine;
		stationEngineAds = _stationEngineAds;
		this.isDebugEnabled = isDebugEnabled;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VUNGLE - Rewarded Video Initializing");
		}
		InitializeEventHandlers();
		if (this.placementVideoID != string.Empty)
		{
			placements.Add(this.placementVideoID, value: false);
		}
	}

	public bool CheckVideoReady()
	{
		return Vungle.isAdvertAvailable(placementVideoID);
	}

	public void ShowRewardedVideo(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VUNGLE - Showing Video Reward");
		}
		if (CheckVideoReady())
		{
			stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.PLAYING);
			Dictionary<string, object> options = new Dictionary<string, object>();
			lastPosition = _position;
			stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.VideoReward, StationEngineFirebase.AnalyticsAdsAction.Impression, lastPosition, StationEngineFirebase.AnalyticsAdsProvider.Vungle);
			Vungle.playAd(options, placementVideoID);
		}
	}

	public void CheckRoutineRewardedVideo()
	{
	}
}
