using UnityEngine;

public interface IInterstitial
{
	void InitializeInterstitial(StationEngine _stationEngine, StationEngineAds _stationEngineAds, string _interstitialID, bool isDebugEnabled, GameObject recipient = null, string appId = null);

	void ShowInterstitial(StationEngineFirebase.AnalyticsAdsPosition _position);

	void RequestInterstitial();

	bool CheckReadyInterstitial();

	void CheckRoutineInterstitial();

	void DestroyInterstitial();
}
