using Firebase.Analytics;
using UnityEngine;

public class StationEngineAnalytics : MonoBehaviour
{
	private StationEngine stationEngine;

	public void Initialize(StationEngine _stationEngine)
	{
		stationEngine = _stationEngine;
		stationEngine.PostDebugInfo("ANALYTICS - INITIALIZING...");
		FirebaseAnalytics.SetAnalyticsCollectionEnabled(enabled: true);
		FirebaseAnalytics.SetUserProperty(FirebaseAnalytics.UserPropertySignUpMethod, "Google");
	}

	public void SendAnalyticAd(AnalyticsAdsType _adType, AnalyticsAdsAction _adAction, AnalyticsAdsPosition _adPosition, AnalyticsAdsProvider _adProvider)
	{
	}

	public void SendAnalyticBehaviour(AnalyticsBehaviour _behaviour)
	{
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

	public void SendAnalyticCustom(string _name, string _parameter, string _value)
	{
		FirebaseAnalytics.LogEvent(_name, _parameter, _value);
	}

	public void SendExperimentID(string experimentID)
	{
	}
}
