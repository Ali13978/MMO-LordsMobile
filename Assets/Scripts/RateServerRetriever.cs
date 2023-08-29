using System.Collections;
using UnityEngine;

public class RateServerRetriever : MonoBehaviour
{
	private StationEngine stationEngine;

	private StationEngineRateServerConfiguration stationEngineRateServerConfiguration;

	private bool isDebugEnabled;

	private bool isRateActivated;

	private int adsWaveStart = 7;

	private int adsWaveFlag = 3;

	private StationEngine.ComponentStatus actualStatus;

	public void Initialize(StationEngine stationEngine, StationEngineRateServerConfiguration stationEngineRateServerConfiguration, bool isDebugEnabled)
	{
		actualStatus = StationEngine.ComponentStatus.INITIALIZING;
		this.stationEngine = stationEngine;
		this.stationEngineRateServerConfiguration = stationEngineRateServerConfiguration;
		this.isDebugEnabled = isDebugEnabled;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("RATE SERVER - Initializing");
		}
		RequestRateServer();
	}

	public StationEngine.ComponentStatus GetStatus()
	{
		return actualStatus;
	}

	public void SetStatusTimeOut()
	{
		actualStatus = StationEngine.ComponentStatus.TIME_OUT;
	}

	public bool IsRateActivated()
	{
		return isRateActivated;
	}

	public int GetWaveStart()
	{
		return adsWaveStart;
	}

	public int GetWaveFlag()
	{
		return adsWaveFlag;
	}

	private void RequestRateServer()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("RATE SERVER - Requesting server");
		}
		WWW www = new WWW(stationEngineRateServerConfiguration.rateServer);
		StartCoroutine(WaitForRequest(www));
	}

	private IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("RATE SERVER - Data received");
			}
			JSONObject i = new JSONObject(www.text);
			RetrieveInfo(i);
		}
		else
		{
			stationEngine.PostDebugError("RATE SERVER - Error: " + www.error);
			actualStatus = StationEngine.ComponentStatus.ERROR;
		}
	}

	private void RetrieveInfo(JSONObject jsonMainObj)
	{
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
				for (int k = 0; k < jSONObject2.list.Count; k++)
				{
					a = jSONObject2.keys[k];
					JSONObject jSONObject3 = jSONObject2.list[k];
					switch (a)
					{
					case "ID":
						switch (int.Parse(jSONObject3.str))
						{
						case 0:
							isRateActivated = false;
							break;
						case 1:
							isRateActivated = true;
							break;
						}
						actualStatus = StationEngine.ComponentStatus.INITIALIZED;
						if (isDebugEnabled)
						{
							stationEngine.PostDebugInfo("RATE SERVER - Result RATE: " + isRateActivated.ToString());
						}
						break;
					case "ICON":
						adsWaveStart = int.Parse(jSONObject3.str);
						if (isDebugEnabled)
						{
							stationEngine.PostDebugInfo("RATE SERVER - Result WAVE START: " + adsWaveStart.ToString());
						}
						break;
					case "DESCRIPTION":
						adsWaveFlag = int.Parse(jSONObject3.str);
						if (isDebugEnabled)
						{
							stationEngine.PostDebugInfo("RATE SERVER - Result WAVE FLAG: " + adsWaveFlag.ToString());
						}
						break;
					}
				}
			}
		}
	}
}
