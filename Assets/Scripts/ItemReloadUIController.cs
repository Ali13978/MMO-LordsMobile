using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ItemReloadUIController : MonoBehaviour
{
	public Text priceText;

	public Text titleText;

	public Text boostDescriptionText;

	public int index;

	private SfxUIController sfxUiController;

	private UIController uiController;

	private StationEngine stationEngine;

	public void Initialize(SfxUIController sfxUiController, UIController uiController)
	{
		this.sfxUiController = sfxUiController;
		this.uiController = uiController;
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		Touch_Battle.IsWindowSmallOpen = true;
		SetWindow();
	}

	public void CloseWindow()
	{
		sfxUiController.PlaySound(SfxUI.ClickClose);
		Touch_Battle.IsWindowSmallOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void BuyBoosters()
	{
		if (PlayerPrefs.GetInt("playerRubies") >= ConfigPrefsController.boostsPrices[index])
		{
			switch (index)
			{
			case 0:
				PlayerPrefs.SetInt("xpBoost", PlayerPrefs.GetInt("xpBoost") + ConfigPrefsController.xpBoostWaveDuration);
				PlayerPrefs.SetInt("hasBoughtXpBoost", 1);
				break;
			case 1:
				PlayerPrefs.SetInt("moneyBoost", PlayerPrefs.GetInt("moneyBoost") + ConfigPrefsController.moneyBoostWaveDuration);
				PlayerPrefs.SetInt("hasBoughtMoneyBoost", 1);
				break;
			case 2:
				PlayerPrefs.SetInt("powerBoost", PlayerPrefs.GetInt("powerBoost") + ConfigPrefsController.powersBoostWaveDuration);
				PlayerPrefs.SetInt("hasBoughtPowersBoost", 1);
				break;
			case 3:
				PlayerPrefs.SetInt("xpBoost", PlayerPrefs.GetInt("xpBoost") + ConfigPrefsController.subscriptionBoostWaveDuration);
				PlayerPrefs.SetInt("moneyBoost", PlayerPrefs.GetInt("moneyBoost") + ConfigPrefsController.subscriptionBoostWaveDuration);
				PlayerPrefs.SetInt("powerBoost", PlayerPrefs.GetInt("powerBoost") + ConfigPrefsController.subscriptionBoostWaveDuration);
				PlayerPrefs.SetInt("hasBoughtVipBoost", 1);
				break;
			}
			PlayerPrefs.SetInt("playerRubies", PlayerPrefs.GetInt("playerRubies") - ConfigPrefsController.boostsPrices[index]);
			PlayerPrefs.SetInt("hasBoughtBoosters", 1);
			PlayerPrefs.Save();
			uiController.UpdateUIUpgrade();
		}
		else if (stationEngine.GetStatusIAPs() == StationEngine.ComponentStatus.INITIALIZED)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Windows/INAPPS/CanvasOutOfRubies")) as GameObject;
			NoRubiesUiController component = gameObject.GetComponent<NoRubiesUiController>();
			component.Initialize(stationEngine, sfxUiController, uiController);
			CloseWindow();
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			CloseWindow();
		}
	}

	private void SetWindow()
	{
		priceText.text = ConfigPrefsController.boostsPrices[index].ToString();
		switch (index)
		{
		case 0:
			boostDescriptionText.text = "x" + ConfigPrefsController.xpBoostWaveDuration + " " + ScriptLocalization.Get("NORMAL/waves");
			break;
		case 1:
			boostDescriptionText.text = "x" + ConfigPrefsController.moneyBoostWaveDuration + " " + ScriptLocalization.Get("NORMAL/waves");
			break;
		case 2:
			boostDescriptionText.text = "x" + ConfigPrefsController.powersBoostWaveDuration + " " + ScriptLocalization.Get("NORMAL/waves");
			break;
		case 3:
			boostDescriptionText.text = "x" + ConfigPrefsController.subscriptionBoostWaveDuration + " " + ScriptLocalization.Get("NORMAL/waves");
			break;
		}
	}
}
