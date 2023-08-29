using System;
using System.Collections;
using UnityEngine;

public class StationEngineTimeRetriever : MonoBehaviour
{
	private const string serverAdress = "http://api.timezonedb.com/v2/get-time-zone?key=VJX82T574QE1&format=json&by=zone&zone=Europe/London";

	private StationEngine stationEngine;

	private bool isDebugEnabled;

	private bool isEnabled;

	private double lastTimeStamp;

	private bool isBusy;

	private StationEngine.ComponentStatus actualStatus;

	public double LastTimeStamp => lastTimeStamp;

	public void Initialize(StationEngine stationEngine, bool isDebugEnabled)
	{
		actualStatus = StationEngine.ComponentStatus.INITIALIZING;
		this.stationEngine = stationEngine;
		this.isDebugEnabled = isDebugEnabled;
		isEnabled = true;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("TIME RETRIEVER - Initializing");
		}
		RequestTime(overwrite: false);
	}

	public StationEngine.ComponentStatus GetStatus()
	{
		return actualStatus;
	}

	public void SetStatusTimeOut()
	{
		actualStatus = StationEngine.ComponentStatus.TIME_OUT;
	}

	public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime().ToUniversalTime();
	}

	public void RequestTime(bool overwrite)
	{
		if (isEnabled)
		{
			if (!isBusy && (actualStatus != StationEngine.ComponentStatus.INITIALIZED || (actualStatus == StationEngine.ComponentStatus.INITIALIZED && overwrite)))
			{
				if (isDebugEnabled)
				{
					stationEngine.PostDebugInfo("TIME RETRIEVER - Requesting server");
				}
				isBusy = true;
				WWW www = new WWW("http://api.timezonedb.com/v2/get-time-zone?key=VJX82T574QE1&format=json&by=zone&zone=Europe/London");
				StartCoroutine(WaitForRequest(www));
			}
		}
		else
		{
			stationEngine.PostDebugError("TIME RETRIEVER - Service disabled");
		}
	}

	private void Update()
	{
		if (isEnabled && actualStatus == StationEngine.ComponentStatus.INITIALIZED)
		{
			lastTimeStamp += Time.unscaledDeltaTime;
		}
	}

	private IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("TIME RETRIEVER - Received data");
			}
			JSONObject i = new JSONObject(www.text);
			RetrieveTime(i);
		}
		else
		{
			stationEngine.PostDebugError("TIME RETRIEVER - Error: " + www.error);
			actualStatus = StationEngine.ComponentStatus.ERROR;
			isBusy = false;
		}
	}

	private void RetrieveTime(JSONObject jsonMainObj)
	{
		bool flag = true;
		double num = -1.0;
		for (int i = 0; i < jsonMainObj.list.Count; i++)
		{
			string a = jsonMainObj.keys[i];
			if (a == "timestamp")
			{
				JSONObject jSONObject = jsonMainObj.list[i];
				num = jSONObject.n;
				break;
			}
		}
		if (num != -1.0)
		{
			flag = true;
		}
		if (flag)
		{
			lastTimeStamp = num;
			DateTime dateTime = UnixTimeStampToDateTime(lastTimeStamp);
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("TIME RETRIEVER - Date/time retrieved: " + lastTimeStamp.ToString() + " /// " + dateTime.ToLongDateString() + " " + dateTime.ToLongTimeString());
			}
			actualStatus = StationEngine.ComponentStatus.INITIALIZED;
		}
		else
		{
			stationEngine.PostDebugError("TIME RETRIEVER - Invalid time retrieved");
			actualStatus = StationEngine.ComponentStatus.ERROR;
		}
		isBusy = false;
	}
}
