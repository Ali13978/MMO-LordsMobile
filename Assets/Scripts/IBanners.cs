using System;
using UnityEngine;

public interface IBanners
{
	void HandleBannerAdOpened(object sender, EventArgs args);

	void HandleBannerAdLeavingApplication(object sender, EventArgs e);

	void InitializeBanner(StationEngine _stationEngine, string _bannerID, bool isDebugEnabled, GameObject recipient = null);

	void ShowBanner(StationEngineFirebase.AnalyticsAdsPosition _position, AdsBannerPosition _bannerPosition);

	void DestroyBanner(bool _requestBanner = false);

	void HideBanner();
}
