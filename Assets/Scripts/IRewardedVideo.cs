public interface IRewardedVideo
{
	void InitializeRewardedVideo(StationEngine _stationEngine, StationEngineAds _stationEngineAds, string _videoID, bool isDebugEnabled, string appID = null);

	void ShowRewardedVideo(StationEngineFirebase.AnalyticsAdsPosition _position);

	void CheckRoutineRewardedVideo();

	bool CheckVideoReady();
}
