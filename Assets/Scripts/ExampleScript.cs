using UnityEngine;
using UnityEngine.UI;

public class ExampleScript : MonoBehaviour
{
	private StationEngine stationEngine;

	public Color colorOFF;

	public Color colorINITIALIZING;

	public Color colorINITIALIZED;

	public Color colorERROR;

	public Color colorTIME_OUT;

	public Text dataTextGeoLocation;

	public Text dataTextRateServer;

	public Text dataTextNotifications;

	public Text dataTextTimeRetriever;

	public Text dataTextIAPS;

	public Text dataTextGooglePlay;

	public Text dataTextGameCenter;

	public Text dataTextFirebaseMessaging;

	public Text dataTextFirebaseAnalytics;

	public Text dataTextAds;

	public Text dataTextAdsInterstitial;

	public Text dataTextAdsVideoReward;

	public Text textVideoRewardStatus;

	public Text textCloudStatus;

	private bool isWaitingForVideo;

	private bool isWaitingForCloud;

	private void Awake()
	{
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		textVideoRewardStatus.text = "VIDEO NOT PLAYING";
		textCloudStatus.text = "CLOUD IDLE";
		SetStatus();
	}

	private void Update()
	{
		SetStatusAds();
	}

	private void LateUpdate()
	{
		if (isWaitingForVideo)
		{
			switch (stationEngine.GetVideoRewardStatus())
			{
			case StationEngineAds.VideoRewardStatus.COMPLETED:
				isWaitingForVideo = false;
				textVideoRewardStatus.text = "VIDEO COMPLETED";
				break;
			case StationEngineAds.VideoRewardStatus.FAILED:
				isWaitingForVideo = false;
				textVideoRewardStatus.text = "VIDEO FAILED";
				break;
			case StationEngineAds.VideoRewardStatus.SKIPPED:
				isWaitingForVideo = false;
				textVideoRewardStatus.text = "VIDEO SKIPPED";
				break;
			}
		}
	}

	public void ButtonLeaderboard()
	{
		stationEngine.ShowLeaderboard(0, _logAnyway: true);
	}

	public void ButtonAchievements()
	{
		stationEngine.ShowAchievement();
	}

	public void ButtonGPLogOut()
	{
		stationEngine.GooglePlayGamesLogOut();
	}

	public void ButtonGPLogIn()
	{
		stationEngine.GameServicesLogIn();
	}

	public void ButtonFacebookPage()
	{
		stationEngine.FacebookPage();
	}

	public void ButtonWebPage()
	{
		stationEngine.WebPage();
	}

	public void ButtonShareAllMedia()
	{
		stationEngine.ShareAllMedia();
	}

	public void ButtonRateApp()
	{
		stationEngine.RateApp();
	}

	public void ButtonMoreGames()
	{
		stationEngine.MoreGames();
	}

	public void ButtonShowBanner()
	{
		stationEngine.ShowBanner(StationEngineFirebase.AnalyticsAdsPosition.Testing, AdsBannerPosition.Bottom);
	}

	public void ButtonHideBanner()
	{
		stationEngine.HideBanner();
	}

	public void ButtonShowInterstitial()
	{
		stationEngine.ShowInterstitial(StationEngineFirebase.AnalyticsAdsPosition.Testing);
	}

	public void ButtonShowVideo()
	{
		isWaitingForVideo = stationEngine.ShowVideoReward(StationEngineFirebase.AnalyticsAdsPosition.Testing);
		if (isWaitingForVideo)
		{
			textVideoRewardStatus.text = "VIDEO PLAYING";
		}
	}

	public void ButtonSaveGame()
	{
		if (!isWaitingForCloud)
		{
			isWaitingForCloud = true;
			string savedGame = "Funca carajo!";
			stationEngine.CloudSaveGame(savedGame);
		}
	}

	public void ButtonLoadGame()
	{
		if (!isWaitingForCloud)
		{
			isWaitingForCloud = true;
			stationEngine.CloudLoadGame();
		}
	}

	private void SetStatus()
	{
		dataTextGeoLocation.text = stationEngine.GetStatusGeoLocation().ToString();
		switch (stationEngine.GetStatusGeoLocation())
		{
		case StationEngine.ComponentStatus.INITIALIZED:
			dataTextGeoLocation.text = stationEngine.GetCountryCode().ToString();
			dataTextGeoLocation.color = colorINITIALIZED;
			break;
		case StationEngine.ComponentStatus.ERROR:
			dataTextGeoLocation.color = colorERROR;
			break;
		case StationEngine.ComponentStatus.INITIALIZING:
			dataTextGeoLocation.color = colorINITIALIZING;
			break;
		case StationEngine.ComponentStatus.OFF:
			dataTextGeoLocation.color = colorOFF;
			break;
		case StationEngine.ComponentStatus.TIME_OUT:
			dataTextGeoLocation.color = colorTIME_OUT;
			break;
		}
		dataTextRateServer.text = stationEngine.GetStatusRateServer().ToString();
		switch (stationEngine.GetStatusRateServer())
		{
		case StationEngine.ComponentStatus.INITIALIZED:
			dataTextRateServer.color = colorINITIALIZED;
			break;
		case StationEngine.ComponentStatus.ERROR:
			dataTextRateServer.color = colorERROR;
			break;
		case StationEngine.ComponentStatus.INITIALIZING:
			dataTextRateServer.color = colorINITIALIZING;
			break;
		case StationEngine.ComponentStatus.OFF:
			dataTextRateServer.color = colorOFF;
			break;
		case StationEngine.ComponentStatus.TIME_OUT:
			dataTextRateServer.color = colorTIME_OUT;
			break;
		}
		dataTextNotifications.text = stationEngine.GetStatusLocalNotifications().ToString();
		switch (stationEngine.GetStatusLocalNotifications())
		{
		case StationEngine.ComponentStatus.INITIALIZED:
			dataTextNotifications.color = colorINITIALIZED;
			break;
		case StationEngine.ComponentStatus.ERROR:
			dataTextNotifications.color = colorERROR;
			break;
		case StationEngine.ComponentStatus.INITIALIZING:
			dataTextNotifications.color = colorINITIALIZING;
			break;
		case StationEngine.ComponentStatus.OFF:
			dataTextNotifications.color = colorOFF;
			break;
		case StationEngine.ComponentStatus.TIME_OUT:
			dataTextNotifications.color = colorTIME_OUT;
			break;
		}
		dataTextTimeRetriever.text = stationEngine.GetStatusTimeRetriever().ToString();
		switch (stationEngine.GetStatusTimeRetriever())
		{
		case StationEngine.ComponentStatus.INITIALIZED:
			dataTextTimeRetriever.text = stationEngine.ParseTimeStamp(stationEngine.GetTimeStamp()).ToString();
			dataTextTimeRetriever.color = colorINITIALIZED;
			break;
		case StationEngine.ComponentStatus.ERROR:
			dataTextTimeRetriever.color = colorERROR;
			break;
		case StationEngine.ComponentStatus.INITIALIZING:
			dataTextTimeRetriever.color = colorINITIALIZING;
			break;
		case StationEngine.ComponentStatus.OFF:
			dataTextTimeRetriever.color = colorOFF;
			break;
		case StationEngine.ComponentStatus.TIME_OUT:
			dataTextTimeRetriever.color = colorTIME_OUT;
			break;
		}
		dataTextFirebaseAnalytics.text = stationEngine.GetStatusAnalytics().ToString();
		switch (stationEngine.GetStatusAnalytics())
		{
		case StationEngine.ComponentStatus.INITIALIZED:
			dataTextFirebaseAnalytics.color = colorINITIALIZED;
			break;
		case StationEngine.ComponentStatus.ERROR:
			dataTextFirebaseAnalytics.color = colorERROR;
			break;
		case StationEngine.ComponentStatus.INITIALIZING:
			dataTextFirebaseAnalytics.color = colorINITIALIZING;
			break;
		case StationEngine.ComponentStatus.OFF:
			dataTextFirebaseAnalytics.color = colorOFF;
			break;
		case StationEngine.ComponentStatus.TIME_OUT:
			dataTextFirebaseAnalytics.color = colorTIME_OUT;
			break;
		}
		dataTextFirebaseMessaging.text = stationEngine.GetStatusMessaging().ToString();
		switch (stationEngine.GetStatusMessaging())
		{
		case StationEngine.ComponentStatus.INITIALIZED:
			dataTextFirebaseMessaging.color = colorINITIALIZED;
			break;
		case StationEngine.ComponentStatus.ERROR:
			dataTextFirebaseMessaging.color = colorERROR;
			break;
		case StationEngine.ComponentStatus.INITIALIZING:
			dataTextFirebaseMessaging.color = colorINITIALIZING;
			break;
		case StationEngine.ComponentStatus.OFF:
			dataTextFirebaseMessaging.color = colorOFF;
			break;
		case StationEngine.ComponentStatus.TIME_OUT:
			dataTextFirebaseMessaging.color = colorTIME_OUT;
			break;
		}
		dataTextIAPS.text = stationEngine.GetStatusIAPs().ToString();
		switch (stationEngine.GetStatusIAPs())
		{
		case StationEngine.ComponentStatus.INITIALIZED:
			dataTextIAPS.color = colorINITIALIZED;
			break;
		case StationEngine.ComponentStatus.ERROR:
			dataTextIAPS.color = colorERROR;
			break;
		case StationEngine.ComponentStatus.INITIALIZING:
			dataTextIAPS.color = colorINITIALIZING;
			break;
		case StationEngine.ComponentStatus.OFF:
			dataTextIAPS.color = colorOFF;
			break;
		case StationEngine.ComponentStatus.TIME_OUT:
			dataTextIAPS.color = colorTIME_OUT;
			break;
		}
		dataTextGooglePlay.text = stationEngine.GetStatusGooglePlayGames().ToString();
		switch (stationEngine.GetStatusGooglePlayGames())
		{
		case StationEngine.ComponentStatus.INITIALIZED:
			dataTextGooglePlay.color = colorINITIALIZED;
			break;
		case StationEngine.ComponentStatus.ERROR:
			dataTextGooglePlay.color = colorERROR;
			break;
		case StationEngine.ComponentStatus.INITIALIZING:
			dataTextGooglePlay.color = colorINITIALIZING;
			break;
		case StationEngine.ComponentStatus.OFF:
			dataTextGooglePlay.color = colorOFF;
			break;
		case StationEngine.ComponentStatus.TIME_OUT:
			dataTextGooglePlay.color = colorTIME_OUT;
			break;
		}
		dataTextAds.text = stationEngine.GetStatusAds().ToString();
		switch (stationEngine.GetStatusAds())
		{
		case StationEngine.ComponentStatus.INITIALIZED:
			dataTextAds.color = colorINITIALIZED;
			break;
		case StationEngine.ComponentStatus.ERROR:
			dataTextAds.color = colorERROR;
			break;
		case StationEngine.ComponentStatus.INITIALIZING:
			dataTextAds.color = colorINITIALIZING;
			break;
		case StationEngine.ComponentStatus.OFF:
			dataTextAds.color = colorOFF;
			break;
		case StationEngine.ComponentStatus.TIME_OUT:
			dataTextAds.color = colorTIME_OUT;
			break;
		}
		SetStatusAds();
	}

	private void SetStatusAds()
	{
		if (stationEngine.IsEnableInterstitials() && stationEngine.GetStatusAds() == StationEngine.ComponentStatus.INITIALIZED)
		{
			if (stationEngine.CheckInterstitial())
			{
				dataTextAdsInterstitial.text = "READY";
				dataTextAdsInterstitial.color = colorINITIALIZED;
			}
			else
			{
				dataTextAdsInterstitial.text = "NOT READY";
				dataTextAdsInterstitial.color = colorERROR;
			}
		}
		else
		{
			dataTextAdsInterstitial.text = "OFF";
			dataTextAdsInterstitial.color = colorOFF;
		}
		if (stationEngine.IsEnableVideoRewards() && stationEngine.GetStatusAds() == StationEngine.ComponentStatus.INITIALIZED)
		{
			if (stationEngine.CheckInterstitial())
			{
				dataTextAdsVideoReward.text = "READY";
				dataTextAdsVideoReward.color = colorINITIALIZED;
			}
			else
			{
				dataTextAdsVideoReward.text = "NOT READY";
				dataTextAdsVideoReward.color = colorERROR;
			}
		}
		else
		{
			dataTextAdsVideoReward.text = "OFF";
			dataTextAdsVideoReward.color = colorOFF;
		}
	}
}
