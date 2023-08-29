using System.Collections;
using UnityEngine;

public class StationEngineGeoLocation : MonoBehaviour
{
	private const string serverAdress = "http://www.ip-api.com/json";

	private const string keySaveCountry = "stationEngineCountryCode";

	private StationEngine stationEngine;

	private bool isDebugEnabled;

	private string userCountryCode = "DEF";

	private StationEngine.ComponentStatus actualStatus;

	public string UserCountryCode => userCountryCode;

	public void Initialize(StationEngine stationEngine, bool isDebugEnabled)
	{
		actualStatus = StationEngine.ComponentStatus.INITIALIZING;
		this.stationEngine = stationEngine;
		this.isDebugEnabled = isDebugEnabled;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("GEO LOCATION - Initializing");
		}
		if (PlayerPrefs.HasKey("stationEngineCountryCode"))
		{
			actualStatus = StationEngine.ComponentStatus.INITIALIZED;
			LoadCountryCode();
		}
		else
		{
			RequestGeoLocation();
		}
	}

	public StationEngine.ComponentStatus GetStatus()
	{
		return actualStatus;
	}

	public void SetStatusTimeOut()
	{
		actualStatus = StationEngine.ComponentStatus.TIME_OUT;
	}

	private void RequestGeoLocation()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("GEO LOCATION - Requesting location");
		}
		WWW www = new WWW("http://www.ip-api.com/json");
		StartCoroutine(WaitForRequest(www));
	}

	private IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("GEO LOCATION - Data received");
			}
			JSONObject i = new JSONObject(www.text);
			RetrieveInfo(i);
		}
		else
		{
			stationEngine.PostDebugError("GEO LOCATION - Error: " + www.error);
			actualStatus = StationEngine.ComponentStatus.ERROR;
		}
	}

	private void RetrieveInfo(JSONObject jsonMainObj)
	{
		bool flag = false;
		for (int i = 0; i < jsonMainObj.list.Count; i++)
		{
			string a = jsonMainObj.keys[i];
			if (a == "countryCode")
			{
				JSONObject jSONObject = jsonMainObj.list[i];
				userCountryCode = jSONObject.str;
				if (isDebugEnabled)
				{
					stationEngine.PostDebugInfo("GEO LOCATION - Country code found: " + userCountryCode);
				}
				flag = true;
				SaveCountryCode();
				actualStatus = StationEngine.ComponentStatus.INITIALIZED;
				break;
			}
		}
		if (!flag)
		{
			stationEngine.PostDebugError("GEO LOCATION - No country code found at JSON");
			actualStatus = StationEngine.ComponentStatus.ERROR;
		}
	}

	private void SaveCountryCode()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("GEO LOCATION - Saved country code: " + userCountryCode);
		}
		PlayerPrefs.SetString("stationEngineCountryCode", userCountryCode);
	}

	private void LoadCountryCode()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("GEO LOCATION - Loaded country code: " + userCountryCode);
		}
		userCountryCode = PlayerPrefs.GetString("stationEngineCountryCode");
	}
}
