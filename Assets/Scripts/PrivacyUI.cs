using UnityEngine;

public class PrivacyUI : MonoBehaviour
{
	public static bool IsWindowOpen;

	public GameObject windowInfo;

	private StationEngine stationEngine;

	private StationEnginePrivacy stationEnginePrivacy;

	public void Initialize(StationEnginePrivacy stationEnginePrivacy)
	{
		IsWindowOpen = true;
		this.stationEnginePrivacy = stationEnginePrivacy;
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
	}

	public void ButtonAccept()
	{
		IsWindowOpen = false;
		stationEnginePrivacy.ChangeAcceptance(acceptance: true);
		stationEngine.Initialize(base.gameObject);
	}

	public void ButtonDoNotAccept()
	{
		windowInfo.SetActive(value: true);
	}

	public void ButtonCloseInfo()
	{
		windowInfo.SetActive(value: false);
	}

	public void ShowPrivacy()
	{
		stationEngine.ShowPrivacyPolicy();
	}

	public void ButtonRevoke()
	{
		windowInfo.SetActive(value: true);
	}

	public void ButtonRevokeDefinitive()
	{
		stationEnginePrivacy.ChangeAcceptance(acceptance: false);
		Application.Quit();
	}

	public void ButtonClose()
	{
		IsWindowOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
