using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationEngineAds : MonoBehaviour
{
	public enum Provider
	{
		ADMOB,
		FACEBOOK,
		OGURY,
		APPLOVIN,
		UNITYADS,
		VUNGLE,
		ADCOLONY
	}

	public enum VideoRewardStatus
	{
		IDLE,
		PLAYING,
		SKIPPED,
		COMPLETED,
		FAILED
	}

	public enum InterstitialStatus
	{
		IDLE,
		PLAYING
	}

	public enum BannerPosition
	{
		TOP,
		BOTTOM
	}

	private StationEngine stationEngine;

	private StationEngineAdsConfiguration stationEngineConfigurationAds;

	private AdmobProvider admobProvider;

	private UnityAdsProvider unityAdsProvider;

	private VungleProvider vungleProvider;

	private FacebookProvider facebookProvider;

	private List<Provider> adsListBanner = new List<Provider>();

	private List<Provider> adsListInterstitial = new List<Provider>();

	private List<Provider> adsListVideoReward = new List<Provider>();

	private float timeRoutineCheck;

	private float timeRoutineCheckFlag = 90f;

	private bool isDebugEnabled;

	private StationEngine.ComponentStatus actualStatus;

	private VideoRewardStatus videoRewardStatus;

	private InterstitialStatus interstitialStatus;

	public void Initialize(StationEngine stationEngine, StationEngineAdsConfiguration stationEngineConfigurationAds, bool isDebugEnabled)
	{
		actualStatus = StationEngine.ComponentStatus.INITIALIZING;
		this.stationEngine = stationEngine;
		this.stationEngineConfigurationAds = stationEngineConfigurationAds;
		this.isDebugEnabled = isDebugEnabled;
		timeRoutineCheck = timeRoutineCheckFlag;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("Initializing ADS system");
		}
		WWW www = new WWW(stationEngineConfigurationAds.adsCountriesServer);
		StartCoroutine(RequestServerList(www));
	}

	public StationEngine.ComponentStatus GetStatus()
	{
		return actualStatus;
	}

	public void SetStatusTimeOut()
	{
		actualStatus = StationEngine.ComponentStatus.TIME_OUT;
	}

	private void Update()
	{
		if (actualStatus == StationEngine.ComponentStatus.INITIALIZED)
		{
			timeRoutineCheck -= Time.unscaledDeltaTime;
			if (timeRoutineCheck <= 0f)
			{
				timeRoutineCheck = timeRoutineCheckFlag;
				RoutineCheckProviders();
			}
		}
	}

	private void RoutineCheckProviders()
	{
		for (int i = 0; i < adsListBanner.Count; i++)
		{
			Provider provider = adsListBanner[i];
			if (provider == Provider.FACEBOOK)
			{
				facebookProvider.CheckRoutineBanner();
			}
		}
		for (int j = 0; j < adsListInterstitial.Count; j++)
		{
			switch (adsListInterstitial[j])
			{
			case Provider.ADMOB:
				admobProvider.CheckRoutineInterstitial();
				break;
			case Provider.FACEBOOK:
				facebookProvider.CheckRoutineInterstitial();
				break;
			case Provider.VUNGLE:
				vungleProvider.CheckRoutineInterstitial();
				break;
			}
		}
		for (int k = 0; k < adsListVideoReward.Count; k++)
		{
			switch (adsListVideoReward[k])
			{
			case Provider.ADMOB:
				admobProvider.CheckRoutineRewardedVideo();
				break;
			case Provider.UNITYADS:
				unityAdsProvider.CheckRoutineRewardedVideo();
				break;
			case Provider.VUNGLE:
				vungleProvider.CheckRoutineRewardedVideo();
				break;
			}
		}
	}

	private IEnumerator RequestServerList(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("ADS COUNTRIES RETRIEVER - JSON Retrieved");
			}
			JSONObject i = new JSONObject(www.text);
			RetrieveInfoServer(i);
		}
		else
		{
			stationEngine.PostDebugError("ADS COUNTRIES RETRIEVER - Error: " + www.error);
			SetSavedQueue(couldReachServer: false);
		}
	}

	private void RetrieveInfoServer(JSONObject jsonMainObj)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		string text5 = string.Empty;
		string text6 = string.Empty;
		for (int i = 0; i < jsonMainObj.list.Count; i++)
		{
			string a = jsonMainObj.keys[i];
			if (!(a == "offerwall"))
			{
				continue;
			}
			JSONObject jSONObject = jsonMainObj.list[i];
			if (jSONObject.list.Count <= 0)
			{
				break;
			}
			for (int j = 0; j < jSONObject.list.Count; j++)
			{
				JSONObject jSONObject2 = jSONObject.list[j];
				bool flag = false;
				bool flag2 = false;
				for (int k = 0; k < jSONObject2.list.Count; k++)
				{
					a = jSONObject2.keys[k];
					JSONObject jSONObject3 = jSONObject2.list[k];
					if (a == "TITLE")
					{
						if (jSONObject3.str == "DEF")
						{
							flag2 = true;
						}
						else if (jSONObject3.str == stationEngine.GetCountryCode())
						{
							flag = true;
						}
						break;
					}
				}
				if (!flag && !flag2)
				{
					continue;
				}
				for (int l = 0; l < jSONObject2.list.Count; l++)
				{
					a = jSONObject2.keys[l];
					JSONObject jSONObject4 = jSONObject2.list[l];
					switch (a)
					{
					case "ICON":
						if (flag)
						{
							text5 = jSONObject4.str;
						}
						else if (flag2)
						{
							text2 = jSONObject4.str;
						}
						break;
					case "DESCRIPTION":
						if (flag)
						{
							text6 = jSONObject4.str;
						}
						else if (flag2)
						{
							text3 = jSONObject4.str;
						}
						break;
					case "ID":
						if (flag)
						{
							text4 = jSONObject4.str;
						}
						else if (flag2)
						{
							text = jSONObject4.str;
						}
						break;
					}
				}
			}
		}
		string[] array = new string[0];
		if (text4 != string.Empty)
		{
			array = text4.Split(new string[3]
			{
				",",
				";",
				"."
			}, StringSplitOptions.None);
		}
		else if (text != string.Empty)
		{
			array = text.Split(new string[3]
			{
				",",
				";",
				"."
			}, StringSplitOptions.None);
		}
		string[] array2 = new string[0];
		if (text5 != string.Empty)
		{
			array2 = text5.Split(new string[3]
			{
				",",
				";",
				"."
			}, StringSplitOptions.None);
		}
		else if (text2 != string.Empty)
		{
			array2 = text2.Split(new string[3]
			{
				",",
				";",
				"."
			}, StringSplitOptions.None);
		}
		string[] array3 = new string[0];
		if (text6 != string.Empty)
		{
			array3 = text6.Split(new string[3]
			{
				",",
				";",
				"."
			}, StringSplitOptions.None);
		}
		else if (text3 != string.Empty)
		{
			array3 = text3.Split(new string[3]
			{
				",",
				";",
				"."
			}, StringSplitOptions.None);
		}
		if (array.Length > 0)
		{
			for (int m = 0; m < array.Length; m++)
			{
				int result;
				if (array[m] != string.Empty && array[m] != " " && int.TryParse(array[m], out result) && Enum.IsDefined(typeof(Provider), result))
				{
					adsListBanner.Add((Provider)result);
				}
			}
		}
		if (array2.Length > 0)
		{
			for (int n = 0; n < array2.Length; n++)
			{
				int result2;
				if (array2[n] != string.Empty && array2[n] != " " && int.TryParse(array2[n], out result2) && Enum.IsDefined(typeof(Provider), result2))
				{
					adsListInterstitial.Add((Provider)result2);
				}
			}
		}
		if (array3.Length > 0)
		{
			for (int num = 0; num < array3.Length; num++)
			{
				int result3;
				if (array3[num] != string.Empty && array3[num] != " " && int.TryParse(array3[num], out result3) && Enum.IsDefined(typeof(Provider), result3))
				{
					adsListVideoReward.Add((Provider)result3);
				}
			}
		}
		SetSavedQueue(couldReachServer: true);
	}

	private void SetSavedQueue(bool couldReachServer)
	{
		stationEngine.SendAnalyticCustom("Behaviour", "InternetConnection", couldReachServer.ToString());
		string key = "StationEngine_BannerOK";
		string str = "StationEngine_Banner_";
		string key2 = "StationEngine_InterstitialOK";
		string str2 = "StationEngine_Interstitial_";
		string key3 = "StationEngine_VideoOK";
		string str3 = "StationEngine_Video_";
		bool loadedFromSave = false;
		if (couldReachServer)
		{
			PlayerPrefs.SetInt(key, adsListBanner.Count);
			for (int i = 0; i < adsListBanner.Count; i++)
			{
				PlayerPrefs.SetInt(str + i.ToString(), (int)adsListBanner[i]);
			}
			PlayerPrefs.SetInt(key2, adsListInterstitial.Count);
			for (int j = 0; j < adsListInterstitial.Count; j++)
			{
				PlayerPrefs.SetInt(str2 + j.ToString(), (int)adsListInterstitial[j]);
			}
			PlayerPrefs.SetInt(key3, adsListVideoReward.Count);
			for (int k = 0; k < adsListVideoReward.Count; k++)
			{
				PlayerPrefs.SetInt(str3 + k.ToString(), (int)adsListVideoReward[k]);
			}
			PlayerPrefs.Save();
		}
		else if (PlayerPrefs.HasKey(key) || PlayerPrefs.HasKey(key2) || PlayerPrefs.HasKey(key3))
		{
			loadedFromSave = true;
			if (PlayerPrefs.HasKey(key))
			{
				int @int = PlayerPrefs.GetInt(key, 0);
				if (@int > 0)
				{
					for (int l = 0; l < @int; l++)
					{
						int int2 = PlayerPrefs.GetInt(str + l.ToString(), 0);
						if (Enum.IsDefined(typeof(Provider), int2))
						{
							adsListBanner.Add((Provider)int2);
						}
					}
				}
			}
			if (PlayerPrefs.HasKey(key2))
			{
				int int3 = PlayerPrefs.GetInt(key2, 0);
				if (int3 > 0)
				{
					for (int m = 0; m < int3; m++)
					{
						int int4 = PlayerPrefs.GetInt(str2 + m.ToString(), 0);
						if (Enum.IsDefined(typeof(Provider), int4))
						{
							adsListInterstitial.Add((Provider)int4);
						}
					}
				}
			}
			if (PlayerPrefs.HasKey(key3))
			{
				int int5 = PlayerPrefs.GetInt(key3, 0);
				if (int5 > 0)
				{
					for (int n = 0; n < int5; n++)
					{
						int int6 = PlayerPrefs.GetInt(str3 + n.ToString(), 0);
						if (Enum.IsDefined(typeof(Provider), int6))
						{
							adsListVideoReward.Add((Provider)int6);
						}
					}
				}
			}
		}
		SetLocalQueue(couldReachServer, loadedFromSave);
	}

	private void SetLocalQueue(bool couldReachServer, bool loadedFromSave)
	{
		if (!couldReachServer && !loadedFromSave)
		{
			if (adsListBanner.Count <= 0)
			{
				for (int i = 0; i < stationEngineConfigurationAds.bannersProviders.Count; i++)
				{
					adsListBanner.Add(stationEngineConfigurationAds.bannersProviders[i]);
				}
			}
			if (adsListInterstitial.Count <= 0)
			{
				for (int j = 0; j < stationEngineConfigurationAds.interstitialsProviders.Count; j++)
				{
					adsListInterstitial.Add(stationEngineConfigurationAds.interstitialsProviders[j]);
				}
			}
			if (adsListVideoReward.Count <= 0)
			{
				for (int k = 0; k < stationEngineConfigurationAds.videoRewardsProviders.Count; k++)
				{
					adsListVideoReward.Add(stationEngineConfigurationAds.videoRewardsProviders[k]);
				}
			}
		}
		InitializeProviders();
	}

	private void InitializeProviders()
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = false;
		bool flag7 = false;
		bool flag8 = false;
		for (int i = 0; i < adsListBanner.Count; i++)
		{
			if (stationEngineConfigurationAds.enableBanners)
			{
				if (adsListBanner[i] == Provider.ADMOB)
				{
					flag = true;
				}
				if (adsListBanner[i] == Provider.FACEBOOK)
				{
					flag2 = true;
				}
			}
		}
		for (int j = 0; j < adsListInterstitial.Count; j++)
		{
			if (stationEngineConfigurationAds.enableInterstitials)
			{
				if (adsListInterstitial[j] == Provider.ADMOB)
				{
					flag3 = true;
				}
				if (adsListInterstitial[j] == Provider.FACEBOOK)
				{
					flag4 = true;
				}
				if (adsListInterstitial[j] == Provider.VUNGLE)
				{
					flag5 = true;
				}
			}
		}
		for (int k = 0; k < adsListVideoReward.Count; k++)
		{
			if (stationEngineConfigurationAds.enableVideoRewards)
			{
				if (adsListVideoReward[k] == Provider.ADMOB)
				{
					flag6 = true;
				}
				if (adsListVideoReward[k] == Provider.UNITYADS)
				{
					flag7 = true;
				}
				if (adsListVideoReward[k] == Provider.VUNGLE)
				{
					flag8 = true;
				}
			}
		}
		if (flag || flag3 || flag6)
		{
			admobProvider = new AdmobProvider();
			if (flag)
			{
				admobProvider.InitializeBanner(stationEngine, stationEngineConfigurationAds.bannerAdmobID, isDebugEnabled);
			}
			if (flag3)
			{
				admobProvider.InitializeInterstitial(stationEngine, this, stationEngineConfigurationAds.interstitialAdmobID, isDebugEnabled);
			}
			if (flag6)
			{
				admobProvider.InitializeRewardedVideo(stationEngine, this, stationEngineConfigurationAds.videoAdmobID, isDebugEnabled);
			}
		}
		if (flag2 || flag4)
		{
			facebookProvider = new FacebookProvider();
			if (flag2)
			{
				facebookProvider.InitializeBanner(stationEngine, stationEngineConfigurationAds.bannerFacebookID, isDebugEnabled, stationEngine.gameObject);
			}
			if (flag4)
			{
				facebookProvider.InitializeInterstitial(stationEngine, this, stationEngineConfigurationAds.interstitialFacebookID, isDebugEnabled, stationEngine.gameObject);
			}
		}
		if (flag7)
		{
			unityAdsProvider = new UnityAdsProvider();
			unityAdsProvider.InitializeRewardedVideo(stationEngine, this, stationEngineConfigurationAds.videoUnityadsID, isDebugEnabled);
		}
		if (flag5 || flag8)
		{
			vungleProvider = new VungleProvider();
			string interstitialID = stationEngineConfigurationAds.interstitialVunglePlacementID;
			string videoID = stationEngineConfigurationAds.videoVunglePlacementID;
			if (!flag5)
			{
				interstitialID = string.Empty;
			}
			if (!flag8)
			{
				videoID = string.Empty;
			}
			vungleProvider.InitializeVungle(stationEngine, this, interstitialID, stationEngineConfigurationAds.vungleAppID, videoID, isDebugEnabled);
		}
		actualStatus = StationEngine.ComponentStatus.INITIALIZED;
	}

	public void ShowBanner(StationEngineFirebase.AnalyticsAdsPosition _position, AdsBannerPosition _bannerPosition)
	{
		if (stationEngineConfigurationAds.enableBanners)
		{
			HideBanner();
			bool flag = false;
			for (int i = 0; i < adsListBanner.Count; i++)
			{
				switch (adsListBanner[i])
				{
				case Provider.ADMOB:
					admobProvider.ShowBanner(_position, _bannerPosition);
					flag = true;
					break;
				case Provider.FACEBOOK:
					if (facebookProvider.CheckReadyBanner())
					{
						facebookProvider.ShowBanner(_position, _bannerPosition);
						flag = true;
					}
					break;
				}
				if (flag)
				{
					break;
				}
			}
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("Trying to show BANNERS but disabled");
		}
	}

	public void HideBanner()
	{
		if (stationEngineConfigurationAds.enableBanners)
		{
			for (int i = 0; i < adsListBanner.Count; i++)
			{
				switch (adsListBanner[i])
				{
				case Provider.ADMOB:
					admobProvider.HideBanner();
					break;
				case Provider.FACEBOOK:
					facebookProvider.HideBanner();
					break;
				}
			}
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("BANNERS disabled");
		}
	}

	public bool IsEnableBanners()
	{
		return stationEngineConfigurationAds.enableBanners;
	}

	public void ShowInterstitial(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		if (stationEngineConfigurationAds.enableInterstitials)
		{
			bool flag = false;
			for (int i = 0; i < adsListInterstitial.Count; i++)
			{
				switch (adsListInterstitial[i])
				{
				case Provider.ADMOB:
					if (admobProvider.CheckReadyInterstitial())
					{
						flag = true;
						admobProvider.ShowInterstitial(_position);
					}
					break;
				case Provider.FACEBOOK:
					if (facebookProvider.CheckReadyInterstitial())
					{
						flag = true;
						facebookProvider.ShowInterstitial(_position);
					}
					break;
				case Provider.VUNGLE:
					if (vungleProvider.CheckReadyInterstitial())
					{
						flag = true;
						vungleProvider.ShowInterstitial(_position);
					}
					break;
				}
				if (flag)
				{
					break;
				}
			}
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("Trying to show INTERSTITIALS but disabled");
		}
	}

	public bool CheckInterstitial()
	{
		bool flag = false;
		if (stationEngineConfigurationAds.enableInterstitials)
		{
			for (int i = 0; i < adsListInterstitial.Count; i++)
			{
				switch (adsListInterstitial[i])
				{
				case Provider.ADMOB:
					flag = admobProvider.CheckReadyInterstitial();
					break;
				case Provider.FACEBOOK:
					flag = facebookProvider.CheckReadyInterstitial();
					break;
				case Provider.VUNGLE:
					flag = vungleProvider.CheckReadyInterstitial();
					break;
				}
				if (flag)
				{
					break;
				}
			}
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("INTERSTITIALS disabled");
		}
		return flag;
	}

	public InterstitialStatus GetInterstitialStatus()
	{
		return interstitialStatus;
	}

	public void SetInterstitialStatus(InterstitialStatus interstitialStatus)
	{
		this.interstitialStatus = interstitialStatus;
	}

	public bool IsEnableInterstitials()
	{
		return stationEngineConfigurationAds.enableInterstitials;
	}

	public bool ShowVideoReward(StationEngineFirebase.AnalyticsAdsPosition _position)
	{
		bool flag = false;
		if (stationEngineConfigurationAds.enableVideoRewards)
		{
			for (int i = 0; i < adsListVideoReward.Count; i++)
			{
				switch (adsListVideoReward[i])
				{
				case Provider.ADMOB:
					if (admobProvider.CheckVideoReady())
					{
						flag = true;
						admobProvider.ShowRewardedVideo(_position);
					}
					break;
				case Provider.UNITYADS:
					if (unityAdsProvider.CheckVideoReady())
					{
						flag = true;
						unityAdsProvider.ShowRewardedVideo(_position);
					}
					break;
				case Provider.VUNGLE:
					if (vungleProvider.CheckVideoReady())
					{
						flag = true;
						vungleProvider.ShowRewardedVideo(_position);
					}
					break;
				}
				if (flag)
				{
					break;
				}
			}
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("Trying to show VIDEO REWARD but disabled");
		}
		return flag;
	}

	public bool CheckVideoReward()
	{
		bool flag = false;
		if (stationEngineConfigurationAds.enableVideoRewards)
		{
			for (int i = 0; i < adsListVideoReward.Count; i++)
			{
				switch (adsListVideoReward[i])
				{
				case Provider.ADMOB:
					flag = admobProvider.CheckVideoReady();
					break;
				case Provider.UNITYADS:
					flag = unityAdsProvider.CheckVideoReady();
					break;
				case Provider.VUNGLE:
					flag = vungleProvider.CheckVideoReady();
					break;
				}
				if (flag)
				{
					break;
				}
			}
		}
		else if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("VIDEO REWARDS disabled");
		}
		return flag;
	}

	public VideoRewardStatus GetVideoRewardStatus()
	{
		return videoRewardStatus;
	}

	public void SetVideoRewardStatus(VideoRewardStatus videoRewardStatus)
	{
		this.videoRewardStatus = videoRewardStatus;
	}

	public bool IsEnableVideoRewards()
	{
		return stationEngineConfigurationAds.enableVideoRewards;
	}
}
