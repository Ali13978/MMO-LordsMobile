using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class UIBankMenu : MonoBehaviour
{
	private const string keyBankNotification = "bankNotification";

	public Text textLevelActual;

	public Text textPercentageActual;

	public Text textPercentageNext;

	public Text textIncomeActual;

	public Text textIncomeNext;

	public Text textIncomeMaxActual;

	public Text textIncomeMaxNext;

	public Text textPrice;

	public GameObject objectTickNotification;

	public GameObject areaNotification;

	public Button buttonUpgrade;

	public Image levelBar;

	public Image glowUpgrade;

	private BankController bankController;

	private SfxUIController sfxUIController;

	private UIController uiController;

	private UpgradesController upgradesController;

	private TutorialController tutorialController;

	private StationEngine stationEngine;

	public void Initialize(StationEngine stationEngine, BankController bankController, UIController uiController, SfxUIController sfxUIController, UpgradesController upgradesController, TutorialController tutorialController)
	{
		this.bankController = bankController;
		this.uiController = uiController;
		this.sfxUIController = sfxUIController;
		this.upgradesController = upgradesController;
		this.tutorialController = tutorialController;
		this.stationEngine = stationEngine;
		if (!PlayerPrefs.HasKey("bankNotification"))
		{
			PlayerPrefs.SetInt("bankNotification", 1);
			PlayerPrefs.Save();
		}
		Touch_Battle.IsWindowSmallOpen = true;
		UpdateWindow();
		if (PlayerPrefs.GetInt("bankTutorialDone") == 0 && PlayerPrefsController.BankLvl < 0)
		{
			if (PlayerPrefs.GetInt("gotFreeGems") == 0)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/BankTutorial/CanvasFreeGems")) as GameObject;
				UIFreeGems component = gameObject.GetComponent<UIFreeGems>();
				component.Initialize(uiController, sfxUIController, tutorialController);
			}
			else
			{
				tutorialController.SetGlow(14);
			}
		}
	}

	public void ButtonClose()
	{
		if (PlayerPrefsController.BankLvl >= 0)
		{
			sfxUIController.PlaySound(SfxUI.ClickDefault);
			uiController.BackFromUpgradeWindow();
			Touch_Battle.IsWindowSmallOpen = false;
			UnityEngine.Object.Destroy(base.gameObject);
			if (PlayerPrefs.GetInt("bankTutorialDone") == 0)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/BankTutorial/CanvasCongratulations")) as GameObject;
				UIBankTutorialDone component = gameObject.GetComponent<UIBankTutorialDone>();
				component.Initialize(sfxUIController);
			}
		}
	}

	public void ButtonUpgrade()
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		int bankLvl = PlayerPrefsController.BankLvl;
		if (PlayerPrefs.GetInt("playerRubies") >= ConfigPrefsController.bankPrice[bankLvl + 1])
		{
			bankLvl++;
			PlayerPrefsController.BankLvl++;
			PlayerPrefs.SetInt("playerRubies", PlayerPrefs.GetInt("playerRubies") - ConfigPrefsController.bankPrice[bankLvl]);
			PlayerPrefs.SetInt("bankLvl", bankLvl);
			PlayerPrefs.Save();
			stationEngine.SendAnalyticSpendVirtualCurrency("Bank_Upgrade " + (bankLvl + 1).ToString(), "[GEMS]", ConfigPrefsController.bankPrice[bankLvl].ToString());
			UpdateWindow();
			bankController.ForceStatus();
			upgradesController.SetInitialStructure(_setHeroes: false);
		}
		else if (stationEngine.GetStatusIAPs() == StationEngine.ComponentStatus.INITIALIZED)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Windows/INAPPS/CanvasOutOfRubies")) as GameObject;
			NoRubiesUiController component = gameObject.GetComponent<NoRubiesUiController>();
			component.Initialize(stationEngine, sfxUIController, uiController);
		}
		if (PlayerPrefs.GetInt("bankTutorialDone") == 0 && PlayerPrefs.GetInt("bankLvl") >= 0)
		{
			tutorialController.ClearGlow();
		}
	}

	public void ButtonNotification()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		if (PlayerPrefs.GetInt("bankNotification") == 0)
		{
			PlayerPrefs.SetInt("bankNotification", 1);
		}
		else
		{
			PlayerPrefs.SetInt("bankNotification", 0);
		}
		PlayerPrefs.Save();
		UpdateWindow();
		bankController.ForceStatus();
	}

	private void UpdateWindow()
	{
		int bankLvl = PlayerPrefsController.BankLvl;
		levelBar.fillAmount = (float)(bankLvl + 1) / (float)ConfigPrefsController.bankPrice.Length;
		uiController.rubyAmountText.text = PlayerPrefs.GetInt("playerRubies").ToString();
		if (bankLvl >= 0)
		{
			textLevelActual.text = ScriptLocalization.Get("NORMAL/level_big").ToUpper() + " " + (bankLvl + 1).ToString();
			textPercentageActual.text = (ConfigPrefsController.bankMultiplier[bankLvl] * 100f).ToString("###.##") + "%";
			textIncomeActual.text = (bankController.GetRewardPerMinute(bankLvl) * 60f).ToString("###,###,##0");
			textIncomeMaxActual.text = bankController.GetMaxReward(bankLvl).ToString("###,###,##0");
		}
		else
		{
			textLevelActual.text = "-";
			textPercentageActual.text = "-";
			textIncomeActual.text = "-";
			textIncomeMaxActual.text = "-";
		}
		if (PlayerPrefsController.BankLvl < ConfigPrefsController.bankPrice.Length - 1)
		{
			textPercentageNext.text = (ConfigPrefsController.bankMultiplier[bankLvl + 1] * 100f).ToString("###.##") + "%";
			textIncomeNext.text = (bankController.GetRewardPerMinute(bankLvl + 1) * 60f).ToString("###,###,##0");
			textIncomeMaxNext.text = bankController.GetMaxReward(bankLvl + 1).ToString("###,###,##0");
			textPrice.text = ConfigPrefsController.bankPrice[bankLvl + 1].ToString("###,###,##0");
		}
		else
		{
			textLevelActual.text = ScriptLocalization.Get("NORMAL/max_big");
			textPercentageNext.text = ScriptLocalization.Get("NORMAL/max_big");
			textIncomeNext.text = ScriptLocalization.Get("NORMAL/max_big");
			textIncomeMaxNext.text = ScriptLocalization.Get("NORMAL/max_big");
			textPrice.text = ScriptLocalization.Get("NORMAL/max_big");
			buttonUpgrade.interactable = false;
		}
		if (bankLvl >= 0)
		{
			areaNotification.SetActive(value: true);
			if (PlayerPrefs.GetInt("bankNotification") == 0)
			{
				objectTickNotification.SetActive(value: false);
			}
			else
			{
				objectTickNotification.SetActive(value: true);
			}
		}
		else
		{
			areaNotification.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ButtonClose();
		}
	}
}
