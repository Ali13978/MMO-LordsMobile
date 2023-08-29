using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StationEngine : MonoBehaviour
{
	public enum ComponentStatus
	{
		OFF,
		INITIALIZING,
		INITIALIZED,
		ERROR,
		TIME_OUT
	}

	public GameObject stationEngineConfigGeneral;

	public GameObject stationEngineConfigAndroid;

	public GameObject stationEngineConfigIos;

	private StationEngineConfiguration stationEngineConfiguration;

	private StationEngineGPGConfiguration stationEngineGPGConfiguration;

	private StationEngineGameCenterConfiguration stationEngineGameCenterConfiguration;

	private StationEngineAdsConfiguration stationEngineAdsConfiguration;

	private StationEngineIAPConfiguration stationEngineIAPConfiguration;

	private StationEngineRateServerConfiguration stationEngineRateServerConfiguration;

	private StationEngineShareConfiguration stationEngineShareConfiguration;

	private StationEngineAds stationEngineAds;

	private StationEngineGPG stationEngineGPG;

	private StationEngineGameCenter stationEngineGameCenter;

	private StationEngineLocalNotifications stationEngineLocalNotifications;

	private StationEngineFirebase stationEngineFirebase;

	private StationEngineSocial stationEngineSocial;

	private StationEngineGeoLocation stationEngineGeoLocation;

	private RateServerRetriever rateServerRetriever;

	private StationEngineTimeRetriever stationEngineTimeRetriever;

	private StationEngineIAP stationEngineIAP;

	private StationEnginePrivacy stationEnginePrivacy;

	private StationEngineConfigJsonRetriever stationEngineConfigJson;

	private void Awake()
	{
		List<GameObject> list = new List<GameObject>();
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = (GameObject)array[i];
			if (gameObject.name == "StationEngine")
			{
				list.Add(gameObject);
			}
		}
		if (list.Count > 1)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	private void Start()
	{
		ReferenceComponents();
		stationEngineConfiguration.objectLoadingBar.SetActive(value: false);
		if (stationEnginePrivacy.CheckAcceptance())
		{
			Initialize();
		}
	}

	private void ReferenceComponents()
	{
		stationEngineConfiguration = stationEngineConfigGeneral.GetComponent<StationEngineConfiguration>();
		stationEngineGPGConfiguration = stationEngineConfigAndroid.GetComponent<StationEngineGPGConfiguration>();
		stationEngineGameCenterConfiguration = stationEngineConfigIos.GetComponent<StationEngineGameCenterConfiguration>();
		GameObject gameObject = null;
		gameObject = ((Application.platform == RuntimePlatform.Android) ? stationEngineConfigAndroid : ((Application.platform != RuntimePlatform.IPhonePlayer) ? stationEngineConfigAndroid : stationEngineConfigIos));
		stationEngineAdsConfiguration = gameObject.GetComponent<StationEngineAdsConfiguration>();
		stationEngineIAPConfiguration = gameObject.GetComponent<StationEngineIAPConfiguration>();
		stationEngineRateServerConfiguration = gameObject.GetComponent<StationEngineRateServerConfiguration>();
		stationEngineShareConfiguration = gameObject.GetComponent<StationEngineShareConfiguration>();
		stationEngineAds = base.gameObject.GetComponent<StationEngineAds>();
		stationEngineGPG = base.gameObject.GetComponent<StationEngineGPG>();
		stationEngineGameCenter = base.gameObject.GetComponent<StationEngineGameCenter>();
		stationEngineLocalNotifications = base.gameObject.GetComponent<StationEngineLocalNotifications>();
		stationEngineFirebase = base.gameObject.GetComponent<StationEngineFirebase>();
		stationEngineSocial = base.gameObject.GetComponent<StationEngineSocial>();
		stationEngineGeoLocation = base.gameObject.GetComponent<StationEngineGeoLocation>();
		rateServerRetriever = base.gameObject.GetComponent<RateServerRetriever>();
		stationEngineTimeRetriever = base.gameObject.GetComponent<StationEngineTimeRetriever>();
		stationEngineIAP = base.gameObject.GetComponent<StationEngineIAP>();
		stationEnginePrivacy = base.gameObject.GetComponent<StationEnginePrivacy>();
		stationEngineConfigJson = base.gameObject.GetComponent<StationEngineConfigJsonRetriever>();
	}

	public void Initialize(GameObject privacyWindow = null)
	{
		if (privacyWindow != null)
		{
			UnityEngine.Object.Destroy(privacyWindow);
		}
		stationEngineConfiguration.objectLoadingBar.SetActive(value: true);
		stationEngineSocial.Initialize(this, stationEngineConfiguration);
		stationEngineConfigJson.Initialize(this);
		if (stationEngineConfiguration.enableGeoLocation)
		{
			stationEngineGeoLocation.Initialize(this, stationEngineConfiguration.debugGeoLocation);
		}
		if (stationEngineRateServerConfiguration.enableRateServerRetriever)
		{
			rateServerRetriever.Initialize(this, stationEngineRateServerConfiguration, stationEngineRateServerConfiguration.debugRateServerRetriever);
		}
		if (stationEngineConfiguration.enableLocalNotifications)
		{
			stationEngineLocalNotifications.Initialize(this, stationEngineConfiguration.debugLocalNotifications);
		}
		if (stationEngineConfiguration.enableTimeRetriever)
		{
			stationEngineTimeRetriever.Initialize(this, stationEngineConfiguration.debugTimeRetriever);
		}
		if (stationEngineIAPConfiguration.enableIAP)
		{
			stationEngineIAP.Initialize(this, stationEngineIAPConfiguration.pubKeyGooglePlay, stationEngineIAPConfiguration.nameIAP, stationEngineIAPConfiguration.skuIAP, stationEngineIAPConfiguration.debugIAP);
		}
		if (Application.platform == RuntimePlatform.Android)
		{
			if (stationEngineGPGConfiguration.enableGooglePlayGames)
			{
				stationEngineGPG.Initialize(this, stationEngineGPGConfiguration);
			}
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer && stationEngineGameCenterConfiguration.enableGameCenter)
		{
			stationEngineGameCenter.Initialize(this, stationEngineGameCenterConfiguration);
		}
		if (stationEngineConfiguration.enableFirebaseMessaging)
		{
			stationEngineFirebase.InitializeMessaging(this, stationEngineConfiguration);
		}
		if (stationEngineConfiguration.enableFirebaseAnalytics)
		{
			stationEngineFirebase.InitializeAnalytics(this, stationEngineConfiguration, stationEngineGeoLocation);
		}
		stationEngineAds.Initialize(this, stationEngineAdsConfiguration, stationEngineAdsConfiguration.debugAds);
		StartCoroutine(LoadNextLevelAsync());
	}

	private IEnumerator LoadNextLevelAsync()
	{
		AsyncOperation ao = SceneManager.LoadSceneAsync(stationEngineConfiguration.nameFirstGameScreen, LoadSceneMode.Single);
		ao.allowSceneActivation = false;
		bool componentsLoaded = false;
		float timeLoading = 0f;
		float timeToLoadScene = 0.89f;
		while (ao.progress <= timeToLoadScene || !componentsLoaded || timeLoading < stationEngineConfiguration.timeMin)
		{
			timeLoading += Time.unscaledDeltaTime;
			int totalComponentsPending = 0;
			int totalComponentsLoaded = 0;
			if (stationEngineConfigJson.shouldCheckServer)
			{
				totalComponentsPending++;
				if (GetStatusJsonRetriever() == ComponentStatus.INITIALIZED || GetStatusJsonRetriever() == ComponentStatus.ERROR || GetStatusJsonRetriever() == ComponentStatus.TIME_OUT)
				{
					totalComponentsLoaded++;
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					stationEngineConfigJson.SetStatusTimeOut();
				}
			}
			if (stationEngineConfiguration.enableGeoLocation)
			{
				totalComponentsPending++;
				if (GetStatusGeoLocation() == ComponentStatus.INITIALIZED || GetStatusGeoLocation() == ComponentStatus.ERROR || GetStatusGeoLocation() == ComponentStatus.TIME_OUT)
				{
					totalComponentsLoaded++;
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					stationEngineGeoLocation.SetStatusTimeOut();
				}
			}
			if (stationEngineConfiguration.enableLocalNotifications)
			{
				totalComponentsPending++;
				if (GetStatusLocalNotifications() == ComponentStatus.INITIALIZED || GetStatusLocalNotifications() == ComponentStatus.ERROR || GetStatusLocalNotifications() == ComponentStatus.TIME_OUT)
				{
					totalComponentsLoaded++;
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					stationEngineLocalNotifications.SetStatusTimeOut();
				}
			}
			if (stationEngineConfiguration.enableTimeRetriever)
			{
				totalComponentsPending++;
				if (GetStatusTimeRetriever() == ComponentStatus.INITIALIZED || GetStatusTimeRetriever() == ComponentStatus.ERROR || GetStatusTimeRetriever() == ComponentStatus.TIME_OUT)
				{
					totalComponentsLoaded++;
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					stationEngineTimeRetriever.SetStatusTimeOut();
				}
			}
			if (stationEngineRateServerConfiguration.enableRateServerRetriever)
			{
				totalComponentsPending++;
				if (GetStatusRateServer() == ComponentStatus.INITIALIZED || GetStatusRateServer() == ComponentStatus.ERROR || GetStatusRateServer() == ComponentStatus.TIME_OUT)
				{
					totalComponentsLoaded++;
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					rateServerRetriever.SetStatusTimeOut();
				}
			}
			if (stationEngineConfiguration.enableFirebaseAnalytics)
			{
				totalComponentsPending++;
				if (GetStatusAnalytics() == ComponentStatus.INITIALIZED || GetStatusAnalytics() == ComponentStatus.ERROR || GetStatusAnalytics() == ComponentStatus.TIME_OUT)
				{
					totalComponentsLoaded++;
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					stationEngineFirebase.SetStatusTimeOutAnalytics();
				}
			}
			if (stationEngineConfiguration.enableFirebaseMessaging)
			{
				totalComponentsPending++;
				if (GetStatusMessaging() == ComponentStatus.INITIALIZED || GetStatusMessaging() == ComponentStatus.ERROR || GetStatusMessaging() == ComponentStatus.TIME_OUT)
				{
					totalComponentsLoaded++;
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					stationEngineFirebase.SetStatusTimeOutMessaging();
				}
			}
			if (stationEngineIAPConfiguration.enableIAP)
			{
				totalComponentsPending++;
				if (GetStatusIAPs() == ComponentStatus.INITIALIZED || GetStatusIAPs() == ComponentStatus.ERROR || GetStatusIAPs() == ComponentStatus.TIME_OUT)
				{
					totalComponentsLoaded++;
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					stationEngineIAP.SetStatusTimeOut();
				}
			}
			if (Application.platform == RuntimePlatform.Android)
			{
				if (stationEngineGPGConfiguration.enableGooglePlayGames)
				{
					totalComponentsPending++;
					if (GetStatusGooglePlayGames() == ComponentStatus.INITIALIZED || GetStatusGooglePlayGames() == ComponentStatus.ERROR || GetStatusGooglePlayGames() == ComponentStatus.TIME_OUT)
					{
						totalComponentsLoaded++;
					}
					else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
					{
						stationEngineGPG.SetStatusTimeOut();
					}
				}
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer && stationEngineGameCenterConfiguration.enableGameCenter)
			{
				totalComponentsPending++;
				if (GetStatusGameCenter() == ComponentStatus.INITIALIZED || GetStatusGameCenter() == ComponentStatus.ERROR || GetStatusGameCenter() == ComponentStatus.TIME_OUT)
				{
					totalComponentsLoaded++;
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					stationEngineGameCenter.SetStatusTimeOut();
				}
			}
			if (stationEngineAdsConfiguration.enableBanners || stationEngineAdsConfiguration.enableInterstitials || stationEngineAdsConfiguration.enableVideoRewards)
			{
				totalComponentsPending++;
				if (GetStatusAds() == ComponentStatus.INITIALIZED || GetStatusAds() == ComponentStatus.ERROR || GetStatusAds() == ComponentStatus.TIME_OUT)
				{
					if (stationEngineAdsConfiguration.videoRewardLoadBeforeStartUp)
					{
						if (stationEngineAds.CheckVideoReward() || timeLoading >= stationEngineConfiguration.timeOut)
						{
							totalComponentsLoaded++;
						}
					}
					else
					{
						totalComponentsLoaded++;
					}
				}
				else if (timeLoading >= stationEngineConfiguration.timeOut && ao.progress >= timeToLoadScene)
				{
					stationEngineAds.SetStatusTimeOut();
				}
			}
			if (totalComponentsPending == totalComponentsLoaded)
			{
				componentsLoaded = true;
			}
			float percentageLoaded = 0.25f * ao.progress + 0.75f / (float)totalComponentsPending * (float)totalComponentsLoaded;
			UpdateSplashScreen(percentageLoaded);
			yield return null;
		}
		ao.allowSceneActivation = true;
	}

	private void UpdateSplashScreen(float percentageLoaded)
	{
		if (stationEngineConfiguration.imageLoadingSplashScreen != null)
		{
			stationEngineConfiguration.imageLoadingSplashScreen.fillAmount = percentageLoaded;
		}
		if (stationEngineConfiguration.textLoadingSplashScreen != null)
		{
			stationEngineConfiguration.textLoadingSplashScreen.text = (percentageLoaded * 100f).ToString("##0") + "%";
		}
	}

	public ComponentStatus GetStatusJsonRetriever()
	{
		return stationEngineConfigJson.GetStatus();
	}

	public void ShowPrivacyPolicy()
	{
		stationEnginePrivacy.ShowPrivacyPolicy(stationEngineConfiguration);
	}

	public void ShowPostAgreement()
	{
		stationEnginePrivacy.ShowPostAgreement();
	}

	public ComponentStatus GetStatusGeoLocation()
	{
		return stationEngineGeoLocation.GetStatus();
	}

	public string GetCountryCode()
	{
		string userCountryCode;
		return userCountryCode = stationEngineGeoLocation.UserCountryCode;
	}

	public ComponentStatus GetStatusRateServer()
	{
		return rateServerRetriever.GetStatus();
	}

	public bool IsRateActivated()
	{
		return rateServerRetriever.IsRateActivated();
	}

	public int GetWaveStart()
	{
		return rateServerRetriever.GetWaveStart();
	}

	public int GetWaveFlag()
	{
		return rateServerRetriever.GetWaveFlag();
	}

	public ComponentStatus GetStatusAds()
	{
		return stationEngineAds.GetStatus();
	}

	public void ShowBanner(StationEngineFirebase.AnalyticsAdsPosition _position, AdsBannerPosition _bannerPosition)
	{
		stationEngineAds.ShowBanner(_position, _bannerPosition);
	}

	public void HideBanner()
	{
		stationEngineAds.HideBanner();
	}

	public void ShowInterstitial(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		stationEngineAds.ShowInterstitial(_position);
	}

	public bool CheckInterstitial()
	{
		return stationEngineAds.CheckInterstitial();
	}

	public bool ShowVideoReward(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		bool result = false;
		if (GetVideoRewardStatus() != StationEngineAds.VideoRewardStatus.PLAYING)
		{
			result = stationEngineAds.ShowVideoReward(_position);
		}
		return result;
	}

	public bool CheckVideoReward()
	{
		return stationEngineAds.CheckVideoReward();
	}

	public StationEngineAds.VideoRewardStatus GetVideoRewardStatus()
	{
		return stationEngineAds.GetVideoRewardStatus();
	}

	public bool IsEnableBanners()
	{
		return stationEngineAds.IsEnableBanners();
	}

	public StationEngineAds.InterstitialStatus GetInterstitialStatus()
	{
		return stationEngineAds.GetInterstitialStatus();
	}

	public bool IsEnableInterstitials()
	{
		return stationEngineAds.IsEnableInterstitials();
	}

	public bool IsEnableVideoRewards()
	{
		return stationEngineAds.IsEnableVideoRewards();
	}

	public void ShareAllMedia()
	{
		stationEngineSocial.ShareAllMedia(stationEngineShareConfiguration.titleShare, stationEngineShareConfiguration.bodyShare);
	}

	public void RateApp()
	{
		stationEngineSocial.RateApp();
	}

	public void MoreGames()
	{
		stationEngineSocial.MoreGames();
	}

	public void FacebookPage()
	{
		stationEngineSocial.FacebookPage();
	}

	public void WebPage()
	{
		stationEngineSocial.WebPage();
	}

	public ComponentStatus GetStatusGooglePlayGames()
	{
		return stationEngineGPG.GetStatus();
	}

	public void GooglePlayGamesLogOut()
	{
		stationEngineGPG.LogOff();
	}

	public void GooglePlayGamesLogInOut()
	{
		stationEngineGPG.LogInOff(_isSilent: false);
	}

	public void CloudSaveGame(string _savedGame)
	{
		stationEngineGPG.SaveCloudGame(_savedGame);
	}

	public void CloudLoadGame()
	{
		stationEngineGPG.LoadCloudGame();
	}

	public CloudStatus GetCloudStatus()
	{
		return stationEngineGPG.StatusCloud;
	}

	public string GetCloudText()
	{
		return stationEngineGPG.SavedGameCloudString;
	}

	public ComponentStatus GetStatusGameCenter()
	{
		return stationEngineGameCenter.GetStatus();
	}

	public void GameServicesLogIn()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (stationEngineGPGConfiguration.enableGooglePlayGames)
			{
				stationEngineGPG.LogIn(stationEngineGPGConfiguration.googlePlayGamesSilentLogIn);
			}
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer && stationEngineGameCenterConfiguration.enableGameCenter)
		{
			stationEngineGameCenter.AuthenticateUser();
		}
	}

	public bool GameServicesIsLogged()
	{
		bool result = false;
		if (Application.platform == RuntimePlatform.Android)
		{
			if (stationEngineGPGConfiguration.enableGooglePlayGames)
			{
				result = stationEngineGPG.IsLogged();
			}
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer && stationEngineGameCenterConfiguration.enableGameCenter)
		{
			result = stationEngineGameCenter.IsAuthenticated();
		}
		return result;
	}

	public void ShowLeaderboard(int _leaderIndex, bool _logAnyway)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (stationEngineGPGConfiguration.enableGooglePlayGames)
			{
				stationEngineGPG.ShowLeaderboard(_leaderIndex, _logAnyway);
			}
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer && stationEngineGameCenterConfiguration.enableGameCenter)
		{
			stationEngineGameCenter.LoadLeaderboard(_leaderIndex);
		}
	}

	public void SubmitToLeaderboard(long _scoreToAdd, int _leaderIndex)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (stationEngineGPGConfiguration.enableGooglePlayGames)
			{
				stationEngineGPG.SubmitToLeaderboard(_scoreToAdd, _leaderIndex);
			}
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer && stationEngineGameCenterConfiguration.enableGameCenter)
		{
			stationEngineGameCenter.ReportScore(_scoreToAdd, _leaderIndex);
		}
	}

	public void ShowAchievement()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (stationEngineGPGConfiguration.enableGooglePlayGames)
			{
				stationEngineGPG.ShowAchievement(_logAnyWay: true);
			}
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer && stationEngineGameCenterConfiguration.enableGameCenter)
		{
			stationEngineGameCenter.ShowAchievements();
		}
	}

	public void UnlockAchievement(int _achievementIndex)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (stationEngineGPGConfiguration.enableGooglePlayGames)
			{
				stationEngineGPG.UnlockAchievement(_achievementIndex);
			}
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer && stationEngineGameCenterConfiguration.enableGameCenter)
		{
			stationEngineGameCenter.ReportAchievement(stationEngineGameCenterConfiguration.achievementsID[_achievementIndex], 100.0);
		}
	}

	public void IncrementAchievement(int _achievementIndex, int _amount)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (stationEngineGPGConfiguration.enableGooglePlayGames)
			{
				stationEngineGPG.IncrementAchievement(_achievementIndex, _amount);
			}
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer && stationEngineGameCenterConfiguration.enableGameCenter)
		{
			stationEngineGameCenter.ReportAchievement(stationEngineGameCenterConfiguration.achievementsID[_achievementIndex], _amount);
		}
	}

	public ComponentStatus GetStatusLocalNotifications()
	{
		return stationEngineLocalNotifications.GetStatus();
	}

	public void SetNotification(int timeInSeconds, string title, string body, int id)
	{
		stationEngineLocalNotifications.SetNotification(timeInSeconds, title, body, id);
	}

	public void SetRepeatNotification(int timeInSeconds, int timeRepeatInSeconds, string title, string body, int id)
	{
		stationEngineLocalNotifications.SetRepeatNotification(timeInSeconds, timeRepeatInSeconds, title, body, id);
	}

	public void CancelNotification(int id)
	{
		stationEngineLocalNotifications.CancelNotification(id);
	}

	public void CancelAllNotifications()
	{
		stationEngineLocalNotifications.CancelAllNotifications();
	}

	public ComponentStatus GetStatusIAPs()
	{
		return stationEngineIAP.GetStatus();
	}

	public void PurchaseIAP(int IAPIndex)
	{
		stationEngineIAP.PurchaseIAP(IAPIndex);
	}

	public string GetIAPPrice(int indexIAP)
	{
		return stationEngineIAP.GetPrice(indexIAP);
	}

	public void SetPrices()
	{
		stationEngineIAP.SetPrices();
	}

	public ComponentStatus GetStatusMessaging()
	{
		return stationEngineFirebase.GetStatusMessaging();
	}

	public ComponentStatus GetStatusAnalytics()
	{
		return stationEngineFirebase.GetStatusAnalytics();
	}

	public void SendAnalyticAd(StationEngineFirebase.AnalyticsAdsType _adType, StationEngineFirebase.AnalyticsAdsAction _adAction, StationEngineFirebase.AnalyticsAdsPosition _adPosition, StationEngineFirebase.AnalyticsAdsProvider _adProvider)
	{
		stationEngineFirebase.SendAnalyticAd(_adType, _adAction, _adPosition, _adProvider);
	}

	public void SendAnalyticBehaviour(StationEngineFirebase.AnalyticsBehaviour _behaviourItem)
	{
		stationEngineFirebase.SendAnalyticBehaviour(_behaviourItem);
	}

	public void SendAnalyticStore(StationEngineFirebase.AnalyticsStore storeGroup, int storeID)
	{
		stationEngineFirebase.SendAnalyticStore(storeGroup, storeID);
	}

	public void SendAnalyticCustom(string _name, string _parameter, string _value)
	{
		stationEngineFirebase.SendAnalyticCustom(_name, _parameter, _value);
	}

	public void SendAnalyticSelectContent(string itemType, string itemName)
	{
		stationEngineFirebase.SendAnalyticSelectContent(itemType, itemName);
	}

	public void SendAnalyticSpendVirtualCurrency(string itemName, string virtualCurrencyName, string itemValue)
	{
		stationEngineFirebase.SendAnalyticSpendVirtualCurrency(itemName, virtualCurrencyName, itemValue);
	}

	public void SendAnalyticTutorialBegin()
	{
		stationEngineFirebase.SendAnalyticTutorialBegin();
	}

	public void SendAnalyticTutorialComplete()
	{
		stationEngineFirebase.SendAnalyticTutorialComplete();
	}

	public void SendAnalyticTutorialWaves(string _name)
	{
		stationEngineFirebase.SendAnalyticTutorialWaves(_name);
	}

	public void SendExperimentID(string experimentID)
	{
		stationEngineFirebase.SendExperimentID(experimentID);
	}

	public ComponentStatus GetStatusTimeRetriever()
	{
		return stationEngineTimeRetriever.GetStatus();
	}

	public void UpdateTime(bool overwrite)
	{
		if (stationEngineConfiguration.enableTimeRetriever)
		{
			stationEngineTimeRetriever.RequestTime(overwrite);
		}
	}

	public double GetTimeStamp()
	{
		if (stationEngineConfiguration.enableTimeRetriever)
		{
			return stationEngineTimeRetriever.LastTimeStamp;
		}
		return -1.0;
	}

	public DateTime ParseTimeStamp(double timeStamp)
	{
		return stationEngineTimeRetriever.UnixTimeStampToDateTime(timeStamp);
	}

	public void PostDebugInfo(string _textToPost)
	{
		if (stationEngineConfiguration.enableDebug)
		{
			UnityEngine.Debug.Log(_textToPost);
		}
	}

	public void PostDebugError(string _textToPost)
	{
		if (stationEngineConfiguration.enableDebug)
		{
			UnityEngine.Debug.LogError(_textToPost);
		}
	}
}
