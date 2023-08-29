using Firebase.Analytics;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StationEngineFirebase : MonoBehaviour
{
	public enum AnalyticsAdsType
	{
		NativeBanner,
		Banner,
		Interstitial,
		VideoReward
	}

	public enum AnalyticsAdsAction
	{
		Impression,
		Click,
		Completed,
		Skip
	}

	public enum AnalyticsAdsPosition
	{
		Start,
		Exit,
		Restart,
		Continue,
		Extra_Coins,
		Extra_Life,
		Extra_Damage,
		Main_Menu,
		In_Game,
		Options,
		Select_Character,
		Testing,
		Map,
		Pause
	}

	public enum AnalyticsAdsProvider
	{
		Admob,
		AdColony,
		AppLovin,
		Vungle,
		Ogury,
		Facebook,
		UnityAds
	}

	public enum AnalyticsStore
	{
		IAP,
		Chest,
		Hero,
		Mercenary
	}

	public enum AnalyticsBehaviour
	{
		Share_All_Media,
		Rate_App,
		More_Games,
		Facebook_Invite,
		Facebook_Page,
		WebPage
	}

	public enum AnalyticsCustomDimension
	{
		IAP_FREE,
		IAP_VIP,
		LEGACY_OLD,
		LEGACY_NEW
	}

	private StationEngine stationEngine;

	private StationEngineConfiguration stationEngineConfiguration;

	private StationEngineGeoLocation stationEngineGeoLocation;

	private StationEngine.ComponentStatus actualStatusMessaging;

	private StationEngine.ComponentStatus actualStatusAnalytics;

	public void InitializeMessaging(StationEngine stationEngine, StationEngineConfiguration stationEngineConfiguration)
	{
		actualStatusMessaging = StationEngine.ComponentStatus.INITIALIZING;
		this.stationEngine = stationEngine;
		this.stationEngineConfiguration = stationEngineConfiguration;
		if (stationEngineConfiguration.debugFirebase)
		{
			stationEngine.PostDebugInfo("FIREBASE MESSAGING - INITIALIZING...");
		}
		try
		{
			FirebaseMessaging.MessageReceived += OnMessageReceived;
			FirebaseMessaging.TokenReceived += OnTokenReceived;
			actualStatusMessaging = StationEngine.ComponentStatus.INITIALIZED;
		}
		catch (Exception arg)
		{
			stationEngine.PostDebugError("FIREBASE MESSAGING - Error no Firebase: " + arg);
			actualStatusMessaging = StationEngine.ComponentStatus.ERROR;
		}
	}

	public void InitializeAnalytics(StationEngine stationEngine, StationEngineConfiguration stationEngineConfiguration, StationEngineGeoLocation stationEngineGeoLocation)
	{
		actualStatusAnalytics = StationEngine.ComponentStatus.INITIALIZING;
		this.stationEngine = stationEngine;
		this.stationEngineConfiguration = stationEngineConfiguration;
		this.stationEngineGeoLocation = stationEngineGeoLocation;
		if (stationEngineConfiguration.debugFirebase)
		{
			stationEngine.PostDebugInfo("FIREBASE ANALYTICS - INITIALIZING...");
		}
		FirebaseAnalytics.SetAnalyticsCollectionEnabled(enabled: true);
		FirebaseAnalytics.SetUserProperty(FirebaseAnalytics.UserPropertySignUpMethod, "Google");
		actualStatusAnalytics = StationEngine.ComponentStatus.INITIALIZED;
	}

	public StationEngine.ComponentStatus GetStatusAnalytics()
	{
		return actualStatusAnalytics;
	}

	public StationEngine.ComponentStatus GetStatusMessaging()
	{
		return actualStatusMessaging;
	}

	public void SetStatusTimeOutAnalytics()
	{
		actualStatusAnalytics = StationEngine.ComponentStatus.TIME_OUT;
	}

	public void SetStatusTimeOutMessaging()
	{
		actualStatusMessaging = StationEngine.ComponentStatus.TIME_OUT;
	}

	public void OnDestroy()
	{
		if (actualStatusMessaging == StationEngine.ComponentStatus.INITIALIZED)
		{
			FirebaseMessaging.MessageReceived -= OnMessageReceived;
			FirebaseMessaging.TokenReceived -= OnTokenReceived;
		}
	}

	public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
	{
		if (actualStatusMessaging != StationEngine.ComponentStatus.INITIALIZED)
		{
			return;
		}
		if (stationEngineConfiguration.debugFirebase)
		{
			stationEngine.PostDebugInfo("FIREBASE MESSAGING - RECEIVED MESSAGE");
		}
		FirebaseNotification notification = e.Message.Notification;
		if (notification != null)
		{
			if (stationEngineConfiguration.debugFirebase)
			{
				stationEngine.PostDebugInfo("FIREBASE MESSAGING - MESSAGE Title: " + notification.Title);
			}
			if (stationEngineConfiguration.debugFirebase)
			{
				stationEngine.PostDebugInfo("FIREBASE MESSAGING - MESSAGE Body: " + notification.Body);
			}
		}
		if (e.Message.From.Length > 0 && stationEngineConfiguration.debugFirebase)
		{
			stationEngine.PostDebugInfo("FIREBASE MESSAGING - MESSAGE Received From: " + e.Message.From);
		}
		if (e.Message.Data.Count > 0)
		{
			if (stationEngineConfiguration.debugFirebase)
			{
				stationEngine.PostDebugInfo("FIREBASE MESSAGING - MESSAGE Data: ");
			}
			foreach (KeyValuePair<string, string> datum in e.Message.Data)
			{
				if (stationEngineConfiguration.debugFirebase)
				{
					stationEngine.PostDebugInfo("  " + datum.Key + ": " + datum.Value);
				}
			}
		}
	}

	public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
	{
		if (actualStatusMessaging == StationEngine.ComponentStatus.INITIALIZED && stationEngineConfiguration.debugFirebase)
		{
			stationEngine.PostDebugInfo("FIREBASE MESSAGING - MESSAGE Received Registration Token - " + token.Token);
		}
	}

	public void SendAnalyticAd(AnalyticsAdsType _adType, AnalyticsAdsAction _adAction, AnalyticsAdsPosition _adPosition, AnalyticsAdsProvider _adProvider)
	{
		if (actualStatusAnalytics == StationEngine.ComponentStatus.INITIALIZED)
		{
			FirebaseAnalytics.LogEvent(_adType.ToString(), "Position_" + _adAction.ToString(), _adPosition.ToString());
			FirebaseAnalytics.LogEvent(_adType.ToString(), "Provider_" + _adAction.ToString(), _adProvider.ToString());
			FirebaseAnalytics.LogEvent(_adType.ToString(), "Country_" + _adProvider.ToString() + "_" + _adAction.ToString(), stationEngineGeoLocation.UserCountryCode);
		}
	}

	public void SendAnalyticBehaviour(AnalyticsBehaviour _behaviour)
	{
		if (actualStatusAnalytics == StationEngine.ComponentStatus.INITIALIZED)
		{
			FirebaseAnalytics.LogEvent("Behaviour", _behaviour.ToString(), stationEngineGeoLocation.UserCountryCode);
		}
	}

	public void SendAnalyticStore(AnalyticsStore storeGroup, int storeID)
	{
		if (actualStatusAnalytics == StationEngine.ComponentStatus.INITIALIZED)
		{
			FirebaseAnalytics.LogEvent("StoreBuys", storeGroup.ToString(), storeID.ToString());
		}
	}

	public void SendAnalyticCustom(string _name, string _parameter, string _value)
	{
		if (actualStatusAnalytics == StationEngine.ComponentStatus.INITIALIZED)
		{
			FirebaseAnalytics.LogEvent(_name, _parameter, _value);
		}
	}

	public void SendAnalyticSelectContent(string itemType, string itemName)
	{
		Parameter parameter = new Parameter(FirebaseAnalytics.ParameterContentType, itemType);
		Parameter parameter2 = new Parameter(FirebaseAnalytics.ParameterItemId, itemName);
		FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSelectContent, parameter, parameter2);
	}

	public void SendAnalyticSpendVirtualCurrency(string itemName, string virtualCurrencyName, string itemValue)
	{
		Parameter parameter = new Parameter(FirebaseAnalytics.ParameterItemName, itemName);
		Parameter parameter2 = new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, virtualCurrencyName);
		Parameter parameter3 = new Parameter(FirebaseAnalytics.ParameterValue, itemValue);
		FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSpendVirtualCurrency, parameter, parameter2, parameter3);
	}

	public void SendAnalyticTutorialBegin()
	{
		FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventTutorialBegin);
	}

	public void SendAnalyticTutorialComplete()
	{
		FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventTutorialComplete);
	}

	public void SendAnalyticTutorialWaves(string _name)
	{
		FirebaseAnalytics.LogEvent(_name);
	}

	public void SendExperimentID(string experimentID)
	{
	}
}
