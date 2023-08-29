using System.Collections.Generic;
using UnityEngine;

public class StationEngineAdsConfiguration : MonoBehaviour
{
	[Header("ADS - GENERAL OPTIONS")]
	public bool enableBanners;

	public bool enableInterstitials;

	public bool enableVideoRewards;

	public bool debugAds;

	public string adsCountriesServer = "http://www.streamliveon.com/manuapps/ow.aspx?uid=34";

	public string vungleAppID = string.Empty;

	public bool videoRewardLoadBeforeStartUp = true;

	[Header("ADS - BANNER")]
	public string bannerAdmobID = string.Empty;

	public string bannerFacebookID = string.Empty;

	public List<StationEngineAds.Provider> bannersProviders = new List<StationEngineAds.Provider>();

	[Header("ADS - INTERSTITIAL")]
	public string interstitialAdmobID = string.Empty;

	public string interstitialFacebookID = string.Empty;

	public string interstitialVunglePlacementID = string.Empty;

	public List<StationEngineAds.Provider> interstitialsProviders = new List<StationEngineAds.Provider>();

	[Header("ADS - VIDEO REWARDS")]
	public string videoAdmobID = string.Empty;

	public string videoVunglePlacementID = string.Empty;

	public string videoUnityadsID = string.Empty;

	public List<StationEngineAds.Provider> videoRewardsProviders = new List<StationEngineAds.Provider>();
}
