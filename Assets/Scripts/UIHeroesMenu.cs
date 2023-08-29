using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroesMenu : MonoBehaviour
{
	public RectTransform transform_ContentList;

	public RectTransform transform_Commander;

	public Text[] textHp_Commander;

	public Text[] textAtk_Commander;

	public Text[] textDef_Commander;

	public Text textPrice_Commander;

	public Button buttonUpgrade_Commander;

	public RectTransform transform_Gladiator;

	public Text[] textHp_Gladiator;

	public Text[] textAtk_Gladiator;

	public Text[] textDef_Gladiator;

	public Text textPrice_Gladiator;

	public Button buttonUpgrade_Gladiator;

	public RectTransform transform_Barbarian;

	public Text[] textHp_Barbarian;

	public Text[] textAtk_Barbarian;

	public Text[] textDef_Barbarian;

	public Text textPrice_Barbarian;

	public Button buttonUpgrade_Barbarian;

	public RectTransform transform_Arcani;

	public Text[] textHp_Arcani;

	public Text[] textAtk_Arcani;

	public Text[] textDef_Arcani;

	public Text textPrice_Arcani;

	public Button buttonUpgrade_Arcani;

	public Color colorTextNormal;

	public Color colorTextBetter;

	public Text[] upgradeHeroeTextLvl;

	public Button[] upgradeHeroeButtonLvl;

	public Button[] upgradeHeroeButtonLvlWithRubies;

	public Button[] upgradeHeroeButtonSelect;

	public Image[] attackBars;

	public Image[] healthBars;

	public Image[] defenceBars;

	public Button[] rubyUpgradeButton;

	public Text[] rubyUpgradeText;

	private int heroSlotSelected;

	private int lastHeroIndexSelected;

	private SfxUIController sfxUIController;

	private UIController uiController;

	private UpgradesController upgradesController;

	private StationEngine stationEngine;

	public int LastHeroIndexSelected => lastHeroIndexSelected;

	private void Start()
	{
	}

	public void Initialize(StationEngine stationEngine, int heroSlotSelected, UIController uiController, SfxUIController sfxUIController, UpgradesController upgradesController)
	{
		this.heroSlotSelected = heroSlotSelected;
		this.uiController = uiController;
		this.sfxUIController = sfxUIController;
		this.upgradesController = upgradesController;
		this.stationEngine = stationEngine;
		SetAvailableHeroes();
		SetStats();
		UpdateUIUpgradeHeroe(heroSlotSelected);
		SetRubiesPriceAndButtons();
	}

	public void ButtonClose()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		uiController.BackFromUpgradeWindow();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void ButtonPressUpgradeHeroBuyWithRubies(int _heroIndex)
	{
		if (PlayerPrefs.GetInt("playerRubies") >= ConfigPrefsController.upgradeHeroWithRubiesPrice[_heroIndex])
		{
			sfxUIController.PlaySound(SfxUI.ClickBuy);
			int num = ConfigPrefsController.upgradeHeroWithRubiesPrice[_heroIndex];
			PlayerPrefs.SetInt("playerRubies", PlayerPrefs.GetInt("playerRubies") - num);
			PlayerPrefsController.HeroeLvl[_heroIndex]++;
			UpdateUIUpgradeHeroe(heroSlotSelected);
			UpdateBars(_heroIndex);
			PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
			uiController.UpdateUIUpgrade();
			SetRubiesPriceAndButtons();
			upgradesController.SetHeroes();
			PlayerPrefs.Save();
			string text = uiController.FormatString(PlayerPrefsController.HeroeLvl[_heroIndex], tripleFormat: false);
			switch (_heroIndex)
			{
			case 0:
				stationEngine.SendAnalyticSelectContent("H_SteppeOutrider", text);
				stationEngine.SendAnalyticSpendVirtualCurrency("H_SteppeOutrider", "[GEMS]", text);
				break;
			case 1:
				stationEngine.SendAnalyticSelectContent("H_GoldenWorgenJavelin", text);
				stationEngine.SendAnalyticSpendVirtualCurrency("H_GoldenWorgenJavelin", "[GEMS]", text);
				break;
			case 2:
				stationEngine.SendAnalyticSelectContent("H_Gladiator", text);
				stationEngine.SendAnalyticSpendVirtualCurrency("H_Gladiator", "[GEMS]", text);
				break;
			case 3:
				stationEngine.SendAnalyticSelectContent("H_TheNightKing", text);
				stationEngine.SendAnalyticSpendVirtualCurrency("H_TheNightKing", "[GEMS]", text);
				break;
			}
		}
		else
		{
			StationEngine component = GameObject.Find("StationEngine").GetComponent<StationEngine>();
			if (component.GetStatusIAPs() == StationEngine.ComponentStatus.INITIALIZED)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Windows/INAPPS/CanvasOutOfRubies")) as GameObject;
				NoRubiesUiController component2 = gameObject.GetComponent<NoRubiesUiController>();
				component2.Initialize(component, sfxUIController, uiController);
			}
		}
	}

	public void ButtonPressUpgradeHeroeBuy(int _heroeIndex)
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		int num = PlayerPrefsController.upgradeHeroPrices[PlayerPrefsController.HeroeLvl[_heroeIndex]];
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)num);
		PlayerPrefsController.HeroeLvl[_heroeIndex]++;
		UpdateUIUpgradeHeroe(heroSlotSelected);
		string text = uiController.FormatString(PlayerPrefsController.HeroeLvl[_heroeIndex], tripleFormat: false);
		UpdateBars(_heroeIndex);
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
		uiController.UpdateUIUpgrade();
		upgradesController.SetHeroes();
		switch (_heroeIndex)
		{
		case 0:
			stationEngine.SendAnalyticSelectContent("H_SteppeOutrider", text);
			stationEngine.SendAnalyticSpendVirtualCurrency("H_SteppeOutrider", "[GOLD]", text);
			break;
		case 1:
			stationEngine.SendAnalyticSelectContent("H_GoldenWorgenJavelin", text);
			stationEngine.SendAnalyticSpendVirtualCurrency("H_GoldenWorgenJavelin", "[GOLD]", text);
			break;
		case 2:
			stationEngine.SendAnalyticSelectContent("H_Gladiator", text);
			stationEngine.SendAnalyticSpendVirtualCurrency("H_Gladiator", "[GOLD]", text);
			break;
		case 3:
			stationEngine.SendAnalyticSelectContent("H_TheNightKing", text);
			stationEngine.SendAnalyticSpendVirtualCurrency("H_TheNightKing", "[GOLD]", text);
			break;
		}
	}

	public void ButtonPressUpgradeHeroeSelect(int _heroeIndex)
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		PlayerPrefsController.HeroeSelected[heroSlotSelected] = (HeroeType)(_heroeIndex + 1);
		UpdateUIUpgradeHeroe(heroSlotSelected);
		SetRubiesPriceAndButtons();
		upgradesController.SetInitialStructure(_setHeroes: true);
		uiController.UpdateHeroIcon();
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
	}

	public void UpdateUIUpgradeHeroe(int _heroIndex)
	{
		lastHeroIndexSelected = _heroIndex;
		for (int i = 0; i < upgradeHeroeTextLvl.Length; i++)
		{
			upgradeHeroeTextLvl[i].text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.HeroeLvl[i].ToString();
			if (PlayerPrefsController.HeroeLvl[i] < ConfigPrefsController.upgradeHeroMax)
			{
				upgradeHeroeButtonLvl[i].interactable = true;
			}
			else
			{
				upgradeHeroeButtonLvl[i].interactable = false;
			}
		}
		for (int j = 0; j < upgradeHeroeButtonSelect.Length; j++)
		{
			if (PlayerPrefsController.HeroeSelected[_heroIndex] == (HeroeType)(j + 1))
			{
				upgradeHeroeButtonSelect[j].interactable = false;
			}
			else
			{
				upgradeHeroeButtonSelect[j].interactable = true;
			}
		}
		for (int k = 0; k < PlayerPrefsController.HeroeSelected.Length; k++)
		{
			if (k != _heroIndex && PlayerPrefsController.HeroeSelected[k] == PlayerPrefsController.HeroeSelected[_heroIndex])
			{
				PlayerPrefsController.HeroeSelected[k] = HeroeType.None;
			}
		}
		SetStats();
	}

	public void SetAvailableHeroes()
	{
		float num = -20f;
		float num2 = -190f;
		float num3 = num;
		if (PlayerPrefsController.HeroeLvl[0] > 0)
		{
			transform_Commander.anchoredPosition = new Vector2(0f, num3);
			num3 += num2;
			UpdateBars(0);
		}
		else
		{
			transform_Commander.gameObject.SetActive(value: false);
		}
		if (PlayerPrefsController.HeroeLvl[1] > 0)
		{
			transform_Gladiator.anchoredPosition = new Vector2(0f, num3);
			num3 += num2;
			UpdateBars(1);
		}
		else
		{
			transform_Gladiator.gameObject.SetActive(value: false);
		}
		if (PlayerPrefsController.HeroeLvl[2] > 0)
		{
			transform_Arcani.anchoredPosition = new Vector2(0f, num3);
			num3 += num2;
			UpdateBars(2);
		}
		else
		{
			transform_Arcani.gameObject.SetActive(value: false);
		}
		if (PlayerPrefsController.HeroeLvl[3] > 0)
		{
			transform_Barbarian.anchoredPosition = new Vector2(0f, num3);
			num3 += num2;
			UpdateBars(3);
		}
		else
		{
			transform_Barbarian.gameObject.SetActive(value: false);
		}
		RectTransform rectTransform = transform_ContentList;
		Vector2 sizeDelta = transform_ContentList.sizeDelta;
		rectTransform.sizeDelta = new Vector2(sizeDelta.x, 0f - num3);
	}

	public void SetStats()
	{
		for (int i = 0; i < PlayerPrefsController.HeroeLvl.Length; i++)
		{
			if (PlayerPrefsController.HeroeLvl[i] <= 0)
			{
				continue;
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = -1f;
			float num5 = -1f;
			float num6 = -1f;
			int num7 = 0;
			if (PlayerPrefsController.HeroeLvl[i] < ConfigPrefsController.upgradeHeroMax)
			{
				num7 = PlayerPrefsController.upgradeHeroPrices[PlayerPrefsController.HeroeLvl[i]];
			}
			num = ConfigPrefsController.GetUnitsRomanHeroHealth(i, PlayerPrefsController.HeroeLvl[i]);
			num2 = ((i != 1) ? ConfigPrefsController.GetUnitsRomanHeroAttack(i, PlayerPrefsController.HeroeLvl[i]) : ConfigPrefsController.GetUnitsRomanHeroRangedAttack(i, PlayerPrefsController.HeroeLvl[i]));
			num3 = ConfigPrefsController.GetUnitsRomanHeroDefence(i, PlayerPrefsController.HeroeLvl[i]);
			if (PlayerPrefsController.HeroeLvl[i] < ConfigPrefsController.upgradeHeroMax)
			{
				num4 = ConfigPrefsController.GetUnitsRomanHeroHealth(i, PlayerPrefsController.HeroeLvl[i] + 1);
				num5 = ((i != 1) ? ConfigPrefsController.GetUnitsRomanHeroAttack(i, PlayerPrefsController.HeroeLvl[i] + 1) : ConfigPrefsController.GetUnitsRomanHeroRangedAttack(i, PlayerPrefsController.HeroeLvl[i] + 1));
				num6 = ConfigPrefsController.GetUnitsRomanHeroDefence(i, PlayerPrefsController.HeroeLvl[i] + 1);
			}
			switch (i)
			{
			case 0:
				textHp_Commander[0].text = num.ToString("###,###,###.#");
				textAtk_Commander[0].text = num2.ToString("###,###,###.#");
				textDef_Commander[0].text = num3.ToString("###,###,###.#");
				if (num4 >= 0f)
				{
					textPrice_Commander.text = num7.ToString("###,###,###");
					textHp_Commander[1].text = num4.ToString("###,###,###.#");
					textAtk_Commander[1].text = num5.ToString("###,###,###.#");
					textDef_Commander[1].text = num6.ToString("###,###,###.#");
					if (num4 > num)
					{
						textHp_Commander[1].color = colorTextBetter;
					}
					else
					{
						textHp_Commander[1].color = colorTextNormal;
					}
					if (num5 > num2)
					{
						textAtk_Commander[1].color = colorTextBetter;
					}
					else
					{
						textAtk_Commander[1].color = colorTextNormal;
					}
					if (num6 > num3)
					{
						textDef_Commander[1].color = colorTextBetter;
					}
					else
					{
						textDef_Commander[1].color = colorTextNormal;
					}
					if (PlayerPrefs.GetFloat("playerMoney") >= (float)num7)
					{
						buttonUpgrade_Commander.interactable = true;
					}
					else
					{
						buttonUpgrade_Commander.interactable = false;
					}
				}
				else
				{
					textPrice_Commander.text = "MAX";
					textHp_Commander[1].text = "MAX";
					textAtk_Commander[1].text = "MAX";
					textDef_Commander[1].text = "MAX";
					textHp_Commander[1].color = colorTextNormal;
					textAtk_Commander[1].color = colorTextNormal;
					textDef_Commander[1].color = colorTextNormal;
					buttonUpgrade_Commander.interactable = false;
				}
				break;
			case 1:
				textHp_Gladiator[0].text = num.ToString("###,###,###.#");
				textAtk_Gladiator[0].text = num2.ToString("###,###,###.#");
				textDef_Gladiator[0].text = num3.ToString("###,###,###.#");
				if (num4 >= 0f)
				{
					textPrice_Gladiator.text = num7.ToString("###,###,###");
					textHp_Gladiator[1].text = num4.ToString("###,###,###.#");
					textAtk_Gladiator[1].text = num5.ToString("###,###,###.#");
					textDef_Gladiator[1].text = num6.ToString("###,###,###.#");
					if (num4 > num)
					{
						textHp_Gladiator[1].color = colorTextBetter;
					}
					else
					{
						textHp_Gladiator[1].color = colorTextNormal;
					}
					if (num5 > num2)
					{
						textAtk_Gladiator[1].color = colorTextBetter;
					}
					else
					{
						textAtk_Gladiator[1].color = colorTextNormal;
					}
					if (num6 > num3)
					{
						textDef_Gladiator[1].color = colorTextBetter;
					}
					else
					{
						textDef_Gladiator[1].color = colorTextNormal;
					}
					if (PlayerPrefs.GetFloat("playerMoney") >= (float)num7)
					{
						buttonUpgrade_Gladiator.interactable = true;
					}
					else
					{
						buttonUpgrade_Gladiator.interactable = false;
					}
				}
				else
				{
					textPrice_Gladiator.text = "MAX";
					textHp_Gladiator[1].text = "MAX";
					textAtk_Gladiator[1].text = "MAX";
					textDef_Gladiator[1].text = "MAX";
					textHp_Gladiator[1].color = colorTextNormal;
					textAtk_Gladiator[1].color = colorTextNormal;
					textDef_Gladiator[1].color = colorTextNormal;
					buttonUpgrade_Gladiator.interactable = false;
				}
				break;
			case 2:
				textHp_Arcani[0].text = num.ToString("###,###,###.#");
				textAtk_Arcani[0].text = num2.ToString("###,###,###.#");
				textDef_Arcani[0].text = num3.ToString("###,###,###.#");
				if (num4 >= 0f)
				{
					textPrice_Arcani.text = num7.ToString("###,###,###");
					textHp_Arcani[1].text = num4.ToString("###,###,###.#");
					textAtk_Arcani[1].text = num5.ToString("###,###,###.#");
					textDef_Arcani[1].text = num6.ToString("###,###,###.#");
					if (num4 > num)
					{
						textHp_Arcani[1].color = colorTextBetter;
					}
					else
					{
						textHp_Arcani[1].color = colorTextNormal;
					}
					if (num5 > num2)
					{
						textAtk_Arcani[1].color = colorTextBetter;
					}
					else
					{
						textAtk_Arcani[1].color = colorTextNormal;
					}
					if (num6 > num3)
					{
						textDef_Arcani[1].color = colorTextBetter;
					}
					else
					{
						textDef_Arcani[1].color = colorTextNormal;
					}
					if (PlayerPrefs.GetFloat("playerMoney") >= (float)num7)
					{
						buttonUpgrade_Arcani.interactable = true;
					}
					else
					{
						buttonUpgrade_Arcani.interactable = false;
					}
				}
				else
				{
					textPrice_Arcani.text = "MAX";
					textHp_Arcani[1].text = "MAX";
					textAtk_Arcani[1].text = "MAX";
					textDef_Arcani[1].text = "MAX";
					textHp_Arcani[1].color = colorTextNormal;
					textAtk_Arcani[1].color = colorTextNormal;
					textDef_Arcani[1].color = colorTextNormal;
					buttonUpgrade_Arcani.interactable = false;
				}
				break;
			case 3:
				textHp_Barbarian[0].text = num.ToString("###,###,###.#");
				textAtk_Barbarian[0].text = num2.ToString("###,###,###.#");
				textDef_Barbarian[0].text = num3.ToString("###,###,###.#");
				if (num4 >= 0f)
				{
					textPrice_Barbarian.text = num7.ToString("###,###,###");
					textHp_Barbarian[1].text = num4.ToString("###,###,###.#");
					textAtk_Barbarian[1].text = num5.ToString("###,###,###.#");
					textDef_Barbarian[1].text = num6.ToString("###,###,###.#");
					if (num4 > num)
					{
						textHp_Barbarian[1].color = colorTextBetter;
					}
					else
					{
						textHp_Barbarian[1].color = colorTextNormal;
					}
					if (num5 > num2)
					{
						textAtk_Barbarian[1].color = colorTextBetter;
					}
					else
					{
						textAtk_Barbarian[1].color = colorTextNormal;
					}
					if (num6 > num3)
					{
						textDef_Barbarian[1].color = colorTextBetter;
					}
					else
					{
						textDef_Barbarian[1].color = colorTextNormal;
					}
					if (PlayerPrefs.GetFloat("playerMoney") >= (float)num7)
					{
						buttonUpgrade_Barbarian.interactable = true;
					}
					else
					{
						buttonUpgrade_Barbarian.interactable = false;
					}
				}
				else
				{
					textPrice_Barbarian.text = "MAX";
					textHp_Barbarian[1].text = "MAX";
					textAtk_Barbarian[1].text = "MAX";
					textDef_Barbarian[1].text = "MAX";
					textHp_Barbarian[1].color = colorTextNormal;
					textAtk_Barbarian[1].color = colorTextNormal;
					textDef_Barbarian[1].color = colorTextNormal;
					buttonUpgrade_Barbarian.interactable = false;
				}
				break;
			}
		}
	}

	private void SetRubiesPriceAndButtons()
	{
		uiController.rubyAmountText.text = PlayerPrefs.GetInt("playerRubies").ToString();
		for (int i = 0; i < rubyUpgradeButton.Length; i++)
		{
			rubyUpgradeText[i].text = ConfigPrefsController.upgradeHeroWithRubiesPrice[i].ToString();
			if (PlayerPrefs.GetInt("playerRubies") >= ConfigPrefsController.upgradeHeroWithRubiesPrice[i])
			{
				rubyUpgradeButton[i].interactable = true;
			}
			else
			{
				rubyUpgradeButton[i].interactable = false;
			}
			if (PlayerPrefsController.HeroeLvl[i] < ConfigPrefsController.upgradeHeroMax)
			{
				upgradeHeroeButtonLvlWithRubies[i].interactable = true;
			}
			else
			{
				upgradeHeroeButtonLvlWithRubies[i].interactable = false;
			}
		}
	}

	private void UpdateBars(int _heroeIndex)
	{
		uiController.UpdateLevelBars(attackBars[_heroeIndex], PlayerPrefsController.HeroeLvl[_heroeIndex], ConfigPrefsController.upgradeHeroMax);
		uiController.UpdateLevelBars(defenceBars[_heroeIndex], PlayerPrefsController.HeroeLvl[_heroeIndex], ConfigPrefsController.upgradeHeroMax);
		uiController.UpdateLevelBars(healthBars[_heroeIndex], PlayerPrefsController.HeroeLvl[_heroeIndex], ConfigPrefsController.upgradeHeroMax);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ButtonClose();
		}
	}
}
