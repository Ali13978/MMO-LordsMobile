using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class StoreUIController : MonoBehaviour
{
	public static StoreUIController instance;

	[Header("GENERAL")]
	public Text rubyText;

	[Header("Tabs Title")]
	public Image imageTabs;

	public Text[] textTabs;

	public Color32 colorOffTextTabs;

	public Color32 colorOnTextTabs;

	public Button buttonTabSubs;

	public Button buttonTabRubies;

	[Header("Subs Tab")]
	public Text[] wavesText;

	public Text[] priceText;

	public Button[] buttonBuyBoosts;

	public Text[] boostsDescriptionText;

	[Header("Gems Tab")]
	public GameObject[] tabsMenu;

	public Text[] packAmountText;

	public Text[] iapPriceText;

	public Text removeAdsText;

	public Text[] bonusGemsPerc;

	private StationEngine stationEngine;

	private UIController uiController;

	private SfxUIController sfxUiController;

	private MainController mainController;

	private void Awake()
	{
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			CloseStore();
		}
	}

	public void Initialize(UIController uiController, SfxUIController sfxUiController, MainController mainController)
	{
		this.uiController = uiController;
		this.sfxUiController = sfxUiController;
		this.mainController = mainController;
		instance = this;
		if (stationEngine.GetStatusIAPs() == StationEngine.ComponentStatus.INITIALIZED)
		{
			SetRubiesMenu();
		}
		else
		{
			SetSubsMenu();
		}
		Touch_Battle.IsWindowBigOpen = true;
	}

	public void SetRubiesMenu()
	{
		imageTabs.rectTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
		tabsMenu[0].SetActive(value: true);
		tabsMenu[1].SetActive(value: false);
		for (int i = 0; i < packAmountText.Length; i++)
		{
			packAmountText[i].text = "x" + ConfigPrefsController.storeRubyPackAmount[i].ToString();
			iapPriceText[i].text = stationEngine.GetIAPPrice(i);
			bonusGemsPerc[i].text.ToUpper();
		}
		removeAdsText.text.ToUpper();
		UpdateWindow();
	}

	public void SetSubsMenu()
	{
		UpdateSubsWindow();
		imageTabs.rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
		tabsMenu[1].SetActive(value: true);
		tabsMenu[0].SetActive(value: false);
		UpdateWindow();
	}

	public void CloseStore()
	{
		sfxUiController.PlaySound(SfxUI.ClickClose);
		uiController.BackFromUpgradeWindow();
		Touch_Battle.IsWindowBigOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void BuyBooster(int buttonIndex)
	{
		if (PlayerPrefs.GetInt("playerRubies") >= ConfigPrefsController.boostsPrices[buttonIndex])
		{
			switch (buttonIndex)
			{
			case 0:
				PlayerPrefs.SetInt("xpBoost", PlayerPrefs.GetInt("xpBoost") + ConfigPrefsController.xpBoostWaveDuration);
				PlayerPrefs.SetInt("hasBoughtXpBoost", 1);
				stationEngine.SendAnalyticSpendVirtualCurrency("XP_Booster", "[GEMS]", ConfigPrefsController.boostsPrices[buttonIndex].ToString());
				break;
			case 1:
				PlayerPrefs.SetInt("moneyBoost", PlayerPrefs.GetInt("moneyBoost") + ConfigPrefsController.moneyBoostWaveDuration);
				PlayerPrefs.SetInt("hasBoughtMoneyBoost", 1);
				stationEngine.SendAnalyticSpendVirtualCurrency("Coins_Booster", "[GEMS]", ConfigPrefsController.boostsPrices[buttonIndex].ToString());
				break;
			case 2:
				PlayerPrefs.SetInt("powerBoost", PlayerPrefs.GetInt("powerBoost") + ConfigPrefsController.powersBoostWaveDuration);
				PlayerPrefs.SetInt("hasBoughtPowersBoost", 1);
				stationEngine.SendAnalyticSpendVirtualCurrency("Skills_Booster", "[GEMS]", ConfigPrefsController.boostsPrices[buttonIndex].ToString());
				break;
			case 3:
				PlayerPrefs.SetInt("xpBoost", PlayerPrefs.GetInt("xpBoost") + ConfigPrefsController.subscriptionBoostWaveDuration);
				PlayerPrefs.SetInt("moneyBoost", PlayerPrefs.GetInt("moneyBoost") + ConfigPrefsController.subscriptionBoostWaveDuration);
				PlayerPrefs.SetInt("powerBoost", PlayerPrefs.GetInt("powerBoost") + ConfigPrefsController.subscriptionBoostWaveDuration);
				PlayerPrefs.SetInt("hasBoughtVipBoost", 1);
				stationEngine.SendAnalyticSpendVirtualCurrency("VIP", "[GEMS]", ConfigPrefsController.boostsPrices[buttonIndex].ToString());
				break;
			}
			sfxUiController.PlaySound(SfxUI.ClickBuy);
			PlayerPrefs.SetInt("playerRubies", PlayerPrefs.GetInt("playerRubies") - ConfigPrefsController.boostsPrices[buttonIndex]);
			PlayerPrefs.SetInt("hasBoughtBoosters", 1);
			PlayerPrefs.Save();
			UpdateSubsWindow();
			uiController.UpdateUIUpgrade();
		}
		else
		{
			sfxUiController.PlaySound(SfxUI.ClickDefault);
			if (stationEngine.GetStatusIAPs() == StationEngine.ComponentStatus.INITIALIZED)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Windows/INAPPS/CanvasOutOfRubies")) as GameObject;
				NoRubiesUiController component = gameObject.GetComponent<NoRubiesUiController>();
				component.Initialize(stationEngine, sfxUiController, uiController);
			}
		}
		UpdateWindow();
	}

	public void BuyRubies(int buttonIndex)
	{
		sfxUiController.PlaySound(SfxUI.ClickBuy);
		stationEngine.PurchaseIAP(buttonIndex);
		uiController.UpdateUIUpgrade();
		UpdateWindow();
	}

	public void RestorePurchases()
	{
	}

	private void SetSubsMenuText()
	{
		if (PlayerPrefs.GetInt("xpBoost") > 0)
		{
			wavesText[0].text = (ScriptLocalization.Get("STORE/duration") + " " + PlayerPrefs.GetInt("xpBoost").ToString() + " " + ScriptLocalization.Get("NORMAL/waves")).ToUpper();
			priceText[0].text = ConfigPrefsController.boostsPrices[0].ToString();
		}
		else
		{
			wavesText[0].text = string.Empty;
			priceText[0].text = ConfigPrefsController.boostsPrices[0].ToString();
		}
		if (PlayerPrefs.GetInt("moneyBoost") > 0)
		{
			wavesText[1].text = (ScriptLocalization.Get("STORE/duration") + " " + PlayerPrefs.GetInt("moneyBoost").ToString() + " " + ScriptLocalization.Get("NORMAL/waves")).ToUpper();
			priceText[1].text = ConfigPrefsController.boostsPrices[1].ToString();
		}
		else
		{
			wavesText[1].text = string.Empty;
			priceText[1].text = ConfigPrefsController.boostsPrices[1].ToString();
		}
		if (PlayerPrefs.GetInt("powerBoost") > 0)
		{
			wavesText[2].text = (ScriptLocalization.Get("STORE/duration") + " " + PlayerPrefs.GetInt("powerBoost").ToString() + " " + ScriptLocalization.Get("NORMAL/waves")).ToUpper();
			priceText[2].text = ConfigPrefsController.boostsPrices[2].ToString();
		}
		else
		{
			wavesText[2].text = string.Empty;
			priceText[2].text = ConfigPrefsController.boostsPrices[2].ToString();
		}
		priceText[3].text = ConfigPrefsController.boostsPrices[3].ToString();
		boostsDescriptionText[0].text = ScriptLocalization.Get("STORE/xp_boost_description") + " " + ConfigPrefsController.xpBoostWaveDuration + " " + ScriptLocalization.Get("NORMAL/waves").ToUpper();
		boostsDescriptionText[1].text = ScriptLocalization.Get("STORE/coins_boost_description") + " " + ConfigPrefsController.moneyBoostWaveDuration + " " + ScriptLocalization.Get("NORMAL/waves").ToUpper();
		boostsDescriptionText[2].text = ScriptLocalization.Get("STORE/preloaded_boost_description") + " " + ConfigPrefsController.powersBoostWaveDuration + " " + ScriptLocalization.Get("NORMAL/waves").ToUpper();
		boostsDescriptionText[3].text = ScriptLocalization.Get("STORE/vip_boost_description") + " " + ConfigPrefsController.subscriptionBoostWaveDuration + " " + ScriptLocalization.Get("NORMAL/waves").ToUpper();
	}

	private void UpdateSubsWindow()
	{
		SetSubsMenuText();
	}

	public void UpdateWindow()
	{
		rubyText.text = PlayerPrefs.GetInt("playerRubies").ToString();
		if (stationEngine.GetStatusIAPs() == StationEngine.ComponentStatus.INITIALIZED)
		{
			buttonTabRubies.enabled = true;
			textTabs[0].color = colorOnTextTabs;
		}
		else
		{
			buttonTabRubies.enabled = false;
			textTabs[0].color = colorOffTextTabs;
		}
	}
}
