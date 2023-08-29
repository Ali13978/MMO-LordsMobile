using UnityEngine.Advertisements;

public class UnityAdsProvider : IRewardedVideo
{
	private bool isDebugEnabled;

	private string interstitialID;

	private StationEngine stationEngine;

	private StationEngineAds stationEngineAds;

	private StationEngineFirebase.AnalyticsAdsPosition lastPosition;

	public void InitializeRewardedVideo(StationEngine _stationEngine, StationEngineAds _stationEngineAds, string _interstitialID, bool isDebugEnabled, string appID = null)
	{
		stationEngine = _stationEngine;
		stationEngineAds = _stationEngineAds;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("UNITY ADS - Initializing");
		}
		interstitialID = _interstitialID;
		this.isDebugEnabled = isDebugEnabled;
		MetaData metaData = new MetaData("gdpr");
		metaData.Set("consent", "true");
		Advertisement.SetMetaData(metaData);
		Advertisement.Initialize(interstitialID, testMode: false);
	}

	public bool CheckVideoReady()
	{
		return Advertisement.IsReady();
	}

	public void ShowRewardedVideo(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("UNITY ADS - Showing Video Reward");
		}
		if (Advertisement.IsReady())
		{
			stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.PLAYING);
			Advertisement.Show(null, new ShowOptions
			{
				resultCallback = delegate(ShowResult result)
				{
					if (isDebugEnabled)
					{
						stationEngine.PostDebugInfo(result.ToString());
					}
					switch (result)
					{
					case ShowResult.Failed:
						stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.FAILED);
						if (isDebugEnabled)
						{
							stationEngine.PostDebugInfo("UNITY ADS - Video Failed");
						}
						break;
					case ShowResult.Finished:
						stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.COMPLETED);
						stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.VideoReward, StationEngineFirebase.AnalyticsAdsAction.Completed, lastPosition, StationEngineFirebase.AnalyticsAdsProvider.UnityAds);
						if (isDebugEnabled)
						{
							stationEngine.PostDebugInfo("UNITY ADS - Video Finished");
						}
						break;
					case ShowResult.Skipped:
						stationEngineAds.SetVideoRewardStatus(StationEngineAds.VideoRewardStatus.SKIPPED);
						stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.VideoReward, StationEngineFirebase.AnalyticsAdsAction.Skip, lastPosition, StationEngineFirebase.AnalyticsAdsProvider.UnityAds);
						if (isDebugEnabled)
						{
							stationEngine.PostDebugInfo("UNITY ADS - Video Skipped");
						}
						break;
					}
				}
			});
			lastPosition = _position;
			stationEngine.SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType.VideoReward, StationEngineFirebase.AnalyticsAdsAction.Impression, lastPosition, StationEngineFirebase.AnalyticsAdsProvider.UnityAds);
		}
	}

	public void CheckRoutineRewardedVideo()
	{
	}
}
