using UnityEngine;

public class StationEnginePrivacy : MonoBehaviour
{
	private const string keyAcceptance = "stationEnginePrivacyAccept";

	public bool CheckAcceptance()
	{
		bool acceptance = GetAcceptance();
		if (!acceptance)
		{
			Invoke("ShowAgreement", 2f);
		}
		return acceptance;
	}

	public void ShowPrivacyPolicy(StationEngineConfiguration stationEngineConfiguration)
	{
		string webPrivacyPolicy = stationEngineConfiguration.webPrivacyPolicy;
		Application.OpenURL(webPrivacyPolicy);
	}

	public void ShowPostAgreement()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("CanvasPrivacyPost")) as GameObject;
		PrivacyUI component = gameObject.GetComponent<PrivacyUI>();
		component.Initialize(this);
	}

	public void ChangeAcceptance(bool acceptance)
	{
		if (acceptance)
		{
			PlayerPrefs.SetInt("stationEnginePrivacyAccept", 1);
		}
		else
		{
			PlayerPrefs.SetInt("stationEnginePrivacyAccept", 0);
		}
		PlayerPrefs.Save();
	}

	private bool GetAcceptance()
	{
		bool result = false;
		if (PlayerPrefs.GetInt("stationEnginePrivacyAccept", 0) > 0)
		{
			result = true;
		}
		return result;
	}

	private void ShowAgreement()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("CanvasPrivacy")) as GameObject;
		PrivacyUI component = gameObject.GetComponent<PrivacyUI>();
		component.Initialize(this);
	}
}
