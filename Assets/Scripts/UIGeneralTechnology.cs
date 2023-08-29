using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class UIGeneralTechnology : MonoBehaviour
{
	private enum Window
	{
		Powers,
		Base,
		Army
	}

	private enum UpgradeSelected
	{
		None,
		Power,
		Base,
		Army
	}

	private Window windowOpened;

	private UpgradeSelected upgradeGroupSelected;

	private int upgradeIndexSelected;

	public GameObject windowPowers;

	public GameObject windowBase;

	public GameObject windowArmy;

	public GameObject powerDescription;

	public GameObject armydescription;

	public GameObject statsPowers;

	public GameObject statsSkills;

	public Button buttonPowers;

	public Button buttonBase;

	public Button buttonArmy;

	public Button buttonUpgrade;

	public RectTransform[] powerTransform;

	public RectTransform[] baseTransform;

	public RectTransform[] armyTransform;

	public Text[] textUpgradeLevelPower;

	public GameObject[] levelContainerPower;

	public Text[] textUpgradeLevelBase;

	public GameObject[] levelContainerBase;

	public Text[] textUpgradeLevelArmy;

	public GameObject[] levelContainerArmy;

	public Text textExperiencePoints;

	public Text textDescriptionTitle;

	public Text textDescriptionCost;

	public Text textArmyCost;

	public Text textPowerLevel;

	public Text textArmyLevel;

	public Text textEffectPowerDescription;

	public Text textEffectPowerA;

	public Text textEffectPowerDataAOld;

	public Text textEffectPowerDataANew;

	public Text textEffectPowerB;

	public Text textEffectPowerDataBOld;

	public Text textEffectPowerDataBNew;

	public Text textEffectBonusDataOld;

	public Text textEffectBonusDataNew;

	public Image[] imageTab;

	public Image[] arrowsPower;

	public Image arrowBonus;

	public Image[] powerBars;

	public Image armyBar;

	public Button[] upgradeButtonsBase;

	public Button[] upgradeButtonsArmy;

	public GameObject tutorialButtonClose;

	public Image tutorialGlowClose;

	public Image tutorialGlowUpgrade;

	public Button[] tutorialButtonsArray;

	public Image[] tutorialImagesArray;

	private string[] titleUpgradePower = new string[3]
	{
		"WAR CRY",
		"CATAPULTS SUPPORT",
		"ARCHERS SUPPORT"
	};

	private string[] descriptionUpgradePower = new string[3]
	{
		"Temporary increase units damage",
		"300% bonus vs siege weapons",
		"50% bonus vs units"
	};

	private string[] titleUpgradeBase = new string[8]
	{
		"TOWERS DAMAGE",
		"CATAPULTS DAMAGE",
		"WALL ARCHERS RANGE",
		"WALL ARCHERS DAMAGE",
		"WALL HEALTH",
		"GOLD PER COLONY",
		"GOLD PER KILL",
		"EXPERIENCE PER KILL"
	};

	private string[] titleUpgradeArmy = new string[8]
	{
		"MELEE SOLDIERS DAMAGE",
		"RANGED SOLDIERS DAMAGE",
		"SIEGE DAMAGE",
		"SIEGE HEALTH",
		"UNITS COOLDOWN",
		"HEROES HEALTH",
		"HEROES DAMAGE",
		"MERCENARIES COST"
	};

	private GameObject selectedObject;

	private TutorialController tutorialController;

	private SfxUIController sfxUiController;

	private MainController mainController;

	private UIController uiController;

	private UIIncomeColonies uiIncomeColonies;

	private StationEngine stationEngine;

	private bool anySettlementColonized;

	private bool anyHeroUnlocked;

	private bool anyMercenaryUnlocked;

	private void Awake()
	{
		buttonBase.image.color = new Color(255f, 255f, 255f, 0f);
		buttonPowers.image.color = new Color(255f, 255f, 255f, 0f);
		buttonArmy.image.color = new Color(255f, 255f, 255f, 0f);
	}

	public void Initialize(StationEngine stationEngine, MainController mainController, UIController uiController, SfxUIController sfxUiController, TutorialController tutorialController, UIIncomeColonies uiIncomeColonies)
	{
		this.stationEngine = stationEngine;
		this.tutorialController = tutorialController;
		this.sfxUiController = sfxUiController;
		this.mainController = mainController;
		this.uiController = uiController;
		this.uiIncomeColonies = uiIncomeColonies;
		Touch_Battle.IsWindowBigOpen = true;
		UpdatePowersBars(PlayerPrefsController.GeneralTechPowers_ArchersShower, ConfigPrefsController.generalArmyMaxUpgrade);
		UpdatePowersBars(PlayerPrefsController.GeneralTechPowers_ArchersShower, ConfigPrefsController.generalArmyMaxUpgrade);
		UpdatePowersBars(PlayerPrefsController.GeneralTechPowers_ArchersShower, ConfigPrefsController.generalArmyMaxUpgrade);
		titleUpgradePower[0] = ScriptLocalization.Get("GENERAL/skill0_title").ToUpper();
		titleUpgradePower[1] = ScriptLocalization.Get("GENERAL/skill2_title").ToUpper();
		titleUpgradePower[2] = ScriptLocalization.Get("GENERAL/skill1_title").ToUpper();
		descriptionUpgradePower[0] = ScriptLocalization.Get("GENERAL/skill0_description").ToUpper();
		descriptionUpgradePower[1] = ScriptLocalization.Get("GENERAL/skill2_description").ToUpper();
		descriptionUpgradePower[2] = ScriptLocalization.Get("GENERAL/skill1_description").ToUpper();
		titleUpgradeBase[0] = ScriptLocalization.Get("GENERAL/base0_title").ToUpper();
		titleUpgradeBase[1] = ScriptLocalization.Get("GENERAL/base1_title").ToUpper();
		titleUpgradeBase[2] = ScriptLocalization.Get("GENERAL/base2_title").ToUpper();
		titleUpgradeBase[3] = ScriptLocalization.Get("GENERAL/base3_title").ToUpper();
		titleUpgradeBase[4] = ScriptLocalization.Get("GENERAL/base4_title").ToUpper();
		titleUpgradeBase[5] = ScriptLocalization.Get("GENERAL/base5_title").ToUpper();
		titleUpgradeBase[6] = ScriptLocalization.Get("GENERAL/base6_title").ToUpper();
		titleUpgradeBase[7] = ScriptLocalization.Get("GENERAL/base7_title").ToUpper();
		titleUpgradeArmy[0] = ScriptLocalization.Get("GENERAL/army0_title").ToUpper();
		titleUpgradeArmy[1] = ScriptLocalization.Get("GENERAL/army1_title").ToUpper();
		titleUpgradeArmy[2] = ScriptLocalization.Get("GENERAL/army2_title").ToUpper();
		titleUpgradeArmy[3] = ScriptLocalization.Get("GENERAL/army3_title").ToUpper();
		titleUpgradeArmy[4] = ScriptLocalization.Get("GENERAL/army4_title").ToUpper();
		titleUpgradeArmy[5] = ScriptLocalization.Get("GENERAL/army5_title").ToUpper();
		titleUpgradeArmy[6] = ScriptLocalization.Get("GENERAL/army6_title").ToUpper();
		titleUpgradeArmy[7] = ScriptLocalization.Get("GENERAL/army7_title").ToUpper();
		for (int i = 0; i < PlayerPrefsController.CitiesConquered.Length; i++)
		{
			if (PlayerPrefsController.CitiesConquered[i] && i != 0)
			{
				anySettlementColonized = true;
				break;
			}
		}
		for (int j = 0; j < PlayerPrefsController.HeroeLvl.Length; j++)
		{
			if (PlayerPrefsController.HeroeLvl[j] > 0)
			{
				anyHeroUnlocked = true;
				break;
			}
		}
		for (int k = 0; k < PlayerPrefsController.UnitsTechMercenary.Length; k++)
		{
			if (PlayerPrefsController.UnitsTechMercenary[k])
			{
				anyMercenaryUnlocked = true;
				break;
			}
		}
		windowOpened = Window.Powers;
		upgradeGroupSelected = UpgradeSelected.Power;
		UpdateWindow();
		if (PlayerPrefsController.GeneralTechPowers_WarCry >= 0)
		{
			SelectPower(0);
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ButtonClose();
		}
	}

	public void UpdateWindow()
	{
		if (windowOpened == Window.Army)
		{
			if (PlayerPrefsController.UnitsTechSpear[1] >= 3)
			{
				if (!upgradeButtonsArmy[1].IsInteractable())
				{
					upgradeButtonsArmy[1].interactable = true;
				}
			}
			else
			{
				upgradeButtonsArmy[1].interactable = false;
			}
			if (PlayerPrefsController.UnitsTechRanged[1] >= 3)
			{
				if (!upgradeButtonsArmy[2].IsInteractable())
				{
					upgradeButtonsArmy[2].interactable = true;
				}
				if (!upgradeButtonsArmy[3].IsInteractable())
				{
					upgradeButtonsArmy[3].interactable = true;
				}
			}
			else
			{
				upgradeButtonsArmy[2].interactable = false;
				upgradeButtonsArmy[3].interactable = false;
			}
			if (anyHeroUnlocked)
			{
				if (!upgradeButtonsArmy[5].IsInteractable())
				{
					upgradeButtonsArmy[5].interactable = true;
				}
				if (!upgradeButtonsArmy[6].IsInteractable())
				{
					upgradeButtonsArmy[6].interactable = true;
				}
			}
			else
			{
				upgradeButtonsArmy[5].interactable = false;
				upgradeButtonsArmy[6].interactable = false;
			}
			if (anyMercenaryUnlocked)
			{
				if (!upgradeButtonsArmy[7].IsInteractable())
				{
					upgradeButtonsArmy[7].interactable = true;
				}
			}
			else
			{
				upgradeButtonsArmy[7].interactable = false;
			}
			if (PlayerPrefsController.GeneralTechArmy_MeleeDamage > 0)
			{
				textUpgradeLevelArmy[0].text = PlayerPrefsController.GeneralTechArmy_MeleeDamage.ToString();
				levelContainerArmy[0].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelArmy[0].text = string.Empty;
				levelContainerArmy[0].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechArmy_RangedDamage > 0)
			{
				textUpgradeLevelArmy[1].text = PlayerPrefsController.GeneralTechArmy_RangedDamage.ToString();
				levelContainerArmy[1].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelArmy[1].text = string.Empty;
				levelContainerArmy[1].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechArmy_SiegeDamage > 0)
			{
				textUpgradeLevelArmy[2].text = PlayerPrefsController.GeneralTechArmy_SiegeDamage.ToString();
				levelContainerArmy[2].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelArmy[2].text = string.Empty;
				levelContainerArmy[2].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechArmy_SiegeHealth > 0)
			{
				textUpgradeLevelArmy[3].text = PlayerPrefsController.GeneralTechArmy_SiegeHealth.ToString();
				levelContainerArmy[3].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelArmy[3].text = string.Empty;
				levelContainerArmy[3].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechArmy_SpawnCooldown > 0)
			{
				textUpgradeLevelArmy[4].text = PlayerPrefsController.GeneralTechArmy_SpawnCooldown.ToString();
				levelContainerArmy[4].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelArmy[4].text = string.Empty;
				levelContainerArmy[4].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechArmy_HeroHealth > 0)
			{
				textUpgradeLevelArmy[5].text = PlayerPrefsController.GeneralTechArmy_HeroHealth.ToString();
				levelContainerArmy[5].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelArmy[5].text = string.Empty;
				levelContainerArmy[5].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechArmy_HeroDamage > 0)
			{
				textUpgradeLevelArmy[6].text = PlayerPrefsController.GeneralTechArmy_HeroDamage.ToString();
				levelContainerArmy[6].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelArmy[6].text = string.Empty;
				levelContainerArmy[6].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechArmy_MercenaryCost > 0)
			{
				textUpgradeLevelArmy[7].text = PlayerPrefsController.GeneralTechArmy_MercenaryCost.ToString();
				levelContainerArmy[7].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelArmy[7].text = string.Empty;
				levelContainerArmy[7].SetActive(value: false);
			}
			buttonBase.interactable = true;
			buttonPowers.interactable = true;
			buttonArmy.interactable = false;
			windowBase.SetActive(value: false);
			windowPowers.SetActive(value: false);
			powerDescription.SetActive(value: false);
			armydescription.SetActive(value: true);
			windowArmy.SetActive(value: true);
			imageTab[0].enabled = false;
			imageTab[1].enabled = false;
			imageTab[2].enabled = true;
		}
		else if (windowOpened == Window.Base)
		{
			if (PlayerPrefsController.WallLvl >= 3)
			{
				if (!upgradeButtonsBase[0].IsInteractable())
				{
					upgradeButtonsBase[0].interactable = true;
				}
			}
			else
			{
				upgradeButtonsBase[0].interactable = false;
			}
			if (PlayerPrefsController.WallLvl >= 21)
			{
				if (!upgradeButtonsBase[1].IsInteractable())
				{
					upgradeButtonsBase[1].interactable = true;
				}
			}
			else
			{
				upgradeButtonsBase[1].interactable = false;
			}
			if (anySettlementColonized)
			{
				if (!upgradeButtonsBase[5].IsInteractable())
				{
					upgradeButtonsBase[5].interactable = true;
				}
			}
			else
			{
				upgradeButtonsBase[5].interactable = false;
			}
			if (PlayerPrefsController.GeneralTechBase_TowerDamage > 0)
			{
				textUpgradeLevelBase[0].text = PlayerPrefsController.GeneralTechBase_TowerDamage.ToString();
				levelContainerBase[0].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelBase[0].text = string.Empty;
				levelContainerBase[0].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechBase_CatapultDamage > 0)
			{
				textUpgradeLevelBase[1].text = PlayerPrefsController.GeneralTechBase_CatapultDamage.ToString();
				levelContainerBase[1].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelBase[1].text = string.Empty;
				levelContainerBase[1].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechBase_ArchersRange > 0)
			{
				textUpgradeLevelBase[2].text = PlayerPrefsController.GeneralTechBase_ArchersRange.ToString();
				levelContainerBase[2].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelBase[2].text = string.Empty;
				levelContainerBase[2].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechBase_ArchersDamage > 0)
			{
				textUpgradeLevelBase[3].text = PlayerPrefsController.GeneralTechBase_ArchersDamage.ToString();
				levelContainerBase[3].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelBase[3].text = string.Empty;
				levelContainerBase[3].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechBase_WallHp > 0)
			{
				textUpgradeLevelBase[4].text = PlayerPrefsController.GeneralTechBase_WallHp.ToString();
				levelContainerBase[4].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelBase[4].text = string.Empty;
				levelContainerBase[4].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechBase_IncomeColony > 0)
			{
				textUpgradeLevelBase[5].text = PlayerPrefsController.GeneralTechBase_IncomeColony.ToString();
				levelContainerBase[5].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelBase[5].text = string.Empty;
				levelContainerBase[5].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechBase_IncomeKill > 0)
			{
				textUpgradeLevelBase[6].text = PlayerPrefsController.GeneralTechBase_IncomeKill.ToString();
				levelContainerBase[6].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelBase[6].text = string.Empty;
				levelContainerBase[6].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechBase_ExperienceKill > 0)
			{
				textUpgradeLevelBase[7].text = PlayerPrefsController.GeneralTechBase_ExperienceKill.ToString();
				levelContainerBase[7].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelBase[7].text = string.Empty;
				levelContainerBase[7].SetActive(value: false);
			}
			buttonBase.interactable = false;
			buttonPowers.interactable = true;
			buttonArmy.interactable = true;
			windowBase.SetActive(value: true);
			windowPowers.SetActive(value: false);
			windowArmy.SetActive(value: false);
			powerDescription.SetActive(value: false);
			armydescription.SetActive(value: true);
			imageTab[0].enabled = false;
			imageTab[1].enabled = true;
			imageTab[2].enabled = false;
		}
		else if (windowOpened == Window.Powers)
		{
			if (PlayerPrefsController.GeneralTechPowers_WarCry > 0)
			{
				textUpgradeLevelPower[0].text = PlayerPrefsController.GeneralTechPowers_WarCry.ToString();
				levelContainerPower[0].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelPower[0].text = string.Empty;
				levelContainerPower[0].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechPowers_CatapultsShower > 0)
			{
				textUpgradeLevelPower[1].text = PlayerPrefsController.GeneralTechPowers_CatapultsShower.ToString();
				levelContainerPower[2].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelPower[1].text = string.Empty;
				levelContainerPower[2].SetActive(value: false);
			}
			if (PlayerPrefsController.GeneralTechPowers_ArchersShower > 0)
			{
				textUpgradeLevelPower[2].text = PlayerPrefsController.GeneralTechPowers_ArchersShower.ToString();
				levelContainerPower[1].SetActive(value: true);
			}
			else
			{
				textUpgradeLevelPower[2].text = string.Empty;
				levelContainerPower[1].SetActive(value: false);
			}
			buttonBase.interactable = true;
			buttonPowers.interactable = false;
			buttonArmy.interactable = true;
			windowBase.SetActive(value: false);
			windowPowers.SetActive(value: true);
			windowArmy.SetActive(value: false);
			powerDescription.SetActive(value: true);
			armydescription.SetActive(value: false);
			imageTab[0].enabled = true;
			imageTab[1].enabled = false;
			imageTab[2].enabled = false;
		}
		textExperiencePoints.text = PlayerPrefs.GetInt("playerExpPoints").ToString() + " XP";
		UpdateDescription();
	}

	public void OpenWindow(int _windowOpen)
	{
		windowOpened = (Window)_windowOpen;
		switch (windowOpened)
		{
		case Window.Powers:
			SelectPower(0);
			break;
		case Window.Base:
			if (PlayerPrefsController.WallLvl >= 3)
			{
				SelectBase(0);
			}
			else
			{
				SelectBase(2);
			}
			break;
		case Window.Army:
			SelectArmy(0);
			break;
		}
		UpdateWindow();
	}

	private void SelectNone()
	{
		UpdatePowersBars(0, 0);
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		upgradeGroupSelected = UpgradeSelected.None;
		upgradeIndexSelected = 0;
		if (selectedObject != null)
		{
			UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
		}
	}

	public void SelectPower(int _buttonIndex)
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		upgradeGroupSelected = UpgradeSelected.Power;
		upgradeIndexSelected = _buttonIndex;
		UpdateDescription();
		if (selectedObject != null)
		{
			UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
		}
		selectedObject = (UnityEngine.Object.Instantiate(Resources.Load("UI/WindowTechs/SelectorPower"), powerTransform[_buttonIndex].position, Quaternion.Euler(Vector3.zero)) as GameObject);
		RectTransform component = selectedObject.GetComponent<RectTransform>();
		component.SetParent(powerTransform[_buttonIndex].parent);
		component.localScale = new Vector3(1f, 1f, 1f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = new Vector2(-4f, -5f);
		component.SetSiblingIndex(0);
		if (_buttonIndex == 0)
		{
			UpdatePowersBars(PlayerPrefsController.GeneralTechPowers_WarCry, ConfigPrefsController.generalArmyMaxUpgrade);
		}
		if (_buttonIndex == 1)
		{
			UpdatePowersBars(PlayerPrefsController.GeneralTechPowers_CatapultsShower, ConfigPrefsController.generalArmyMaxUpgrade);
		}
		if (_buttonIndex == 2)
		{
			UpdatePowersBars(PlayerPrefsController.GeneralTechPowers_ArchersShower, ConfigPrefsController.generalPowerMaxUpgrade);
		}
	}

	public void SelectBase(int _buttonIndex)
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		upgradeGroupSelected = UpgradeSelected.Base;
		upgradeIndexSelected = _buttonIndex;
		UpdateDescription();
		if (selectedObject != null)
		{
			UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
		}
		selectedObject = (UnityEngine.Object.Instantiate(Resources.Load("UI/WindowTechs/SelectorUpgrade"), baseTransform[_buttonIndex].position, Quaternion.Euler(Vector3.zero)) as GameObject);
		RectTransform component = selectedObject.GetComponent<RectTransform>();
		component.SetParent(baseTransform[_buttonIndex].parent);
		component.localScale = new Vector3(1f, 1f, 1f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = new Vector2(10f, 10f);
		component.SetSiblingIndex(0);
		UpdateBaseBars(_buttonIndex);
	}

	public void SelectArmy(int _buttonIndex)
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		upgradeGroupSelected = UpgradeSelected.Army;
		upgradeIndexSelected = _buttonIndex;
		UpdateDescription();
		if (selectedObject != null)
		{
			UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
		}
		selectedObject = (UnityEngine.Object.Instantiate(Resources.Load("UI/WindowTechs/SelectorUpgrade"), armyTransform[_buttonIndex].position, Quaternion.Euler(Vector3.zero)) as GameObject);
		RectTransform component = selectedObject.GetComponent<RectTransform>();
		component.SetParent(armyTransform[_buttonIndex].parent);
		component.localScale = new Vector3(1f, 1f, 1f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = new Vector2(10f, 10f);
		component.SetSiblingIndex(0);
		UpdateArmyBars(_buttonIndex);
	}

	public void ButtonClose()
	{
		if (PlayerPrefsController.tutorialSteps[13])
		{
			sfxUiController.PlaySound(SfxUI.ClickDefault);
			uiController.BackFromUpgradeWindow();
			Touch_Battle.IsWindowBigOpen = false;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void ButtonUpgrade()
	{
		sfxUiController.PlaySound(SfxUI.ClickBuy);
		string empty = string.Empty;
		string empty2 = string.Empty;
		switch (upgradeGroupSelected)
		{
		case UpgradeSelected.Power:
			switch (upgradeIndexSelected)
			{
			case 0:
				PlayerPrefsController.GeneralTechPowers_WarCry++;
				UpdatePowersBars(PlayerPrefsController.GeneralTechPowers_WarCry, ConfigPrefsController.generalPowerMaxUpgrade);
				PlayerPrefs.SetInt("playerExpPoints", PlayerPrefs.GetInt("playerExpPoints") - ConfigPrefsController.generalPowerCostUpgrade_WarCry);
				PlayerPrefs.Save();
				stationEngine.SendAnalyticSelectContent("General_Skills", "S_BattleHorn");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechPowers_WarCry, tripleFormat: false);
				empty2 = ConfigPrefsController.generalPowerCostUpgrade_WarCry.ToString();
				stationEngine.SendAnalyticSpendVirtualCurrency("S_BattleHorn " + empty, "[EXP]", empty2);
				break;
			case 1:
				PlayerPrefsController.GeneralTechPowers_CatapultsShower++;
				UpdatePowersBars(PlayerPrefsController.GeneralTechPowers_CatapultsShower, ConfigPrefsController.generalPowerMaxUpgrade);
				PlayerPrefs.SetInt("playerExpPoints", PlayerPrefs.GetInt("playerExpPoints") - ConfigPrefsController.generalPowerCostUpgrade_Arrows);
				PlayerPrefs.Save();
				stationEngine.SendAnalyticSelectContent("General_Skills", "S_CatapultsSupport");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechPowers_CatapultsShower, tripleFormat: false);
				empty2 = ConfigPrefsController.generalPowerCostUpgrade_Arrows.ToString();
				stationEngine.SendAnalyticSpendVirtualCurrency("S_CatapultsSupport " + empty, "[EXP]", empty2);
				break;
			case 2:
				PlayerPrefsController.GeneralTechPowers_ArchersShower++;
				UpdatePowersBars(PlayerPrefsController.GeneralTechPowers_ArchersShower, ConfigPrefsController.generalPowerMaxUpgrade);
				PlayerPrefs.SetInt("playerExpPoints", PlayerPrefs.GetInt("playerExpPoints") - ConfigPrefsController.generalPowerCostUpgrade_Arrows);
				PlayerPrefs.Save();
				stationEngine.SendAnalyticSelectContent("General_Skills", "S_ArchersSupport");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechPowers_ArchersShower, tripleFormat: false);
				empty2 = ConfigPrefsController.generalPowerCostUpgrade_Arrows.ToString();
				stationEngine.SendAnalyticSpendVirtualCurrency("S_ArchersSupport " + empty, "[EXP]", empty2);
				break;
			}
			break;
		case UpgradeSelected.Base:
			UpdateBaseBars(upgradeIndexSelected);
			empty2 = ConfigPrefsController.generalArmyCostUpgrade.ToString();
			switch (upgradeIndexSelected)
			{
			case 0:
				PlayerPrefsController.GeneralTechBase_TowerDamage++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "B_TowersDamage");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechBase_TowerDamage, tripleFormat: false);
				empty2 = ConfigPrefsController.generalPowerCostUpgrade_Arrows.ToString();
				stationEngine.SendAnalyticSpendVirtualCurrency("B_TowersDamage " + empty, "[EXP]", empty2);
				break;
			case 1:
				PlayerPrefsController.GeneralTechBase_CatapultDamage++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "B_CatapultsDamage");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechBase_CatapultDamage, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("B_CatapultsDamage " + empty, "[EXP]", empty2);
				break;
			case 2:
				PlayerPrefsController.GeneralTechBase_ArchersRange++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "B_WallArchersRange");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechBase_ArchersRange, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("B_WallArchersRange " + empty, "[EXP]", empty2);
				break;
			case 3:
				PlayerPrefsController.GeneralTechBase_ArchersDamage++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "B_WallArchersDamage");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechBase_ArchersDamage, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("B_WallArchersDamage " + empty, "[EXP]", empty2);
				break;
			case 4:
				PlayerPrefsController.GeneralTechBase_WallHp++;
				mainController.SetWallLife();
				stationEngine.SendAnalyticSelectContent("General_Skills", "B_WallHitPoints");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechBase_WallHp, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("B_WallHitPoints " + empty, "[EXP]", empty2);
				break;
			case 5:
				PlayerPrefsController.GeneralTechBase_IncomeColony++;
				uiIncomeColonies.CalculateIncome();
				stationEngine.SendAnalyticSelectContent("General_Skills", "B_GoldPerColony");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechBase_IncomeColony, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("B_GoldPerColony " + empty, "[EXP]", empty2);
				break;
			case 6:
				PlayerPrefsController.GeneralTechBase_IncomeKill++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "B_GoldPerKill");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechBase_IncomeKill, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("B_GoldPerKill " + empty, "[EXP]", empty2);
				break;
			case 7:
				PlayerPrefsController.GeneralTechBase_ExperienceKill++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "B_ExperiencePerKill");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechBase_ExperienceKill, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("B_ExperiencePerKill " + empty, "[EXP]", empty2);
				break;
			}
			PlayerPrefs.SetInt("playerExpPoints", PlayerPrefs.GetInt("playerExpPoints") - ConfigPrefsController.generalBaseCostUpgrade);
			PlayerPrefs.Save();
			break;
		case UpgradeSelected.Army:
			UpdateArmyBars(upgradeIndexSelected);
			empty2 = ConfigPrefsController.generalBaseCostUpgrade.ToString();
			switch (upgradeIndexSelected)
			{
			case 0:
				PlayerPrefsController.GeneralTechArmy_MeleeDamage++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "A_MeleeSoldiersDamage");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechArmy_MeleeDamage, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("A_MeleeSoldiersDamage " + empty, "[EXP]", empty2);
				break;
			case 1:
				PlayerPrefsController.GeneralTechArmy_RangedDamage++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "A_RangedSoldiersDamage");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechArmy_RangedDamage, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("A_RangedSoldiersDamage " + empty, "[EXP]", empty2);
				break;
			case 2:
				PlayerPrefsController.GeneralTechArmy_SiegeDamage++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "A_SiegeWeaponsDamage");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechArmy_SiegeDamage, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("A_SiegeWeaponsDamage " + empty, "[EXP]", empty2);
				break;
			case 3:
				PlayerPrefsController.GeneralTechArmy_SiegeHealth++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "A_SiegeWeaponsHealth");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechArmy_SiegeHealth, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("A_SiegeWeaponsHealth " + empty, "[EXP]", empty2);
				break;
			case 4:
				PlayerPrefsController.GeneralTechArmy_SpawnCooldown++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "A_UnitsCooldown");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechArmy_SpawnCooldown, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("A_UnitsCooldown " + empty, "[EXP]", empty2);
				break;
			case 5:
				PlayerPrefsController.GeneralTechArmy_HeroHealth++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "A_HeroesHealth");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechArmy_HeroHealth, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("A_HeroesHealth " + empty, "[EXP]", empty2);
				break;
			case 6:
				PlayerPrefsController.GeneralTechArmy_HeroDamage++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "A_HeroesDamage");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechArmy_HeroDamage, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("A_HeroesDamage " + empty, "[EXP]", empty2);
				break;
			case 7:
				PlayerPrefsController.GeneralTechArmy_MercenaryCost++;
				stationEngine.SendAnalyticSelectContent("General_Skills", "A_MerceneriesCost");
				empty = uiController.FormatString(PlayerPrefsController.GeneralTechArmy_MercenaryCost, tripleFormat: false);
				stationEngine.SendAnalyticSpendVirtualCurrency("A_MerceneriesCost " + empty, "[EXP]", empty2);
				break;
			}
			PlayerPrefs.SetInt("playerExpPoints", PlayerPrefs.GetInt("playerExpPoints") - ConfigPrefsController.generalArmyCostUpgrade);
			PlayerPrefs.Save();
			break;
		}
		PlayerPrefsController.SaveBoughtGeneralTech(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
		if (tutorialController.GetActualIndex() == 13)
		{
			windowOpened = Window.Powers;
			tutorialController.ActivateStep(14);
		}
		UpdateWindow();
	}

	private void UpdateDescription()
	{
		int num = 0;
		if (upgradeGroupSelected == UpgradeSelected.None)
		{
			statsPowers.SetActive(value: false);
			statsSkills.SetActive(value: false);
			arrowBonus.enabled = false;
			arrowsPower[0].enabled = false;
			arrowsPower[1].enabled = false;
		}
		else if (upgradeGroupSelected == UpgradeSelected.Power)
		{
			statsPowers.SetActive(value: true);
			statsSkills.SetActive(value: false);
			arrowBonus.enabled = false;
			arrowsPower[0].enabled = true;
			arrowsPower[1].enabled = true;
		}
		else
		{
			statsPowers.SetActive(value: false);
			statsSkills.SetActive(value: true);
			arrowBonus.enabled = true;
			arrowsPower[0].enabled = false;
			arrowsPower[1].enabled = false;
		}
		switch (upgradeGroupSelected)
		{
		case UpgradeSelected.None:
			textDescriptionTitle.text = string.Empty;
			textDescriptionCost.text = string.Empty;
			textArmyCost.text = string.Empty;
			textEffectPowerDescription.text = string.Empty;
			textEffectPowerA.text = string.Empty;
			textEffectPowerDataAOld.text = string.Empty;
			textEffectPowerDataANew.text = string.Empty;
			textEffectPowerB.text = string.Empty;
			textEffectPowerDataBOld.text = string.Empty;
			textEffectPowerDataBNew.text = string.Empty;
			textEffectBonusDataOld.text = string.Empty;
			textEffectBonusDataNew.text = string.Empty;
			armyBar.fillAmount = 0f;
			powerBars[0].fillAmount = 0f;
			powerBars[1].fillAmount = 0f;
			textPowerLevel.text = string.Empty;
			textArmyLevel.text = string.Empty;
			buttonUpgrade.interactable = false;
			break;
		case UpgradeSelected.Power:
			textDescriptionTitle.text = titleUpgradePower[upgradeIndexSelected];
			textEffectBonusDataOld.text = string.Empty;
			textEffectBonusDataNew.text = string.Empty;
			switch (upgradeIndexSelected)
			{
			case 0:
				textDescriptionCost.text = ConfigPrefsController.generalPowerCostUpgrade_WarCry.ToString("###,###,##0") + " XP";
				num = PlayerPrefsController.GeneralTechPowers_WarCry;
				textEffectPowerDescription.text = descriptionUpgradePower[0];
				textEffectPowerA.text = ScriptLocalization.Get("NORMAL/damage").ToUpper();
				textEffectPowerDataAOld.text = "+" + (ConfigPrefsController.generalPowerWarCryDamagePerLevel * (float)PlayerPrefsController.GeneralTechPowers_WarCry * 100f).ToString("###,###,##0.#") + "%";
				textEffectPowerDataANew.text = "+" + (ConfigPrefsController.generalPowerWarCryDamagePerLevel * (float)(PlayerPrefsController.GeneralTechPowers_WarCry + 1) * 100f).ToString("###,###,##0.#") + "%";
				textEffectPowerB.text = ScriptLocalization.Get("GENERAL/cooldown").ToUpper();
				textEffectPowerDataBOld.text = "-" + (ConfigPrefsController.generalPowerWarCryCooldownPerLevel * (float)PlayerPrefsController.GeneralTechPowers_WarCry).ToString("###,###,##0.0") + "s";
				textEffectPowerDataBNew.text = "-" + (ConfigPrefsController.generalPowerWarCryCooldownPerLevel * (float)(PlayerPrefsController.GeneralTechPowers_WarCry + 1)).ToString("###,###,##0.0") + "s";
				textPowerLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechPowers_WarCry.ToString();
				break;
			case 1:
				textDescriptionCost.text = ConfigPrefsController.generalPowerCostUpgrade_Arrows.ToString("###,###,##0") + " XP";
				num = PlayerPrefsController.GeneralTechPowers_CatapultsShower;
				textEffectPowerDescription.text = descriptionUpgradePower[1];
				textEffectPowerA.text = ScriptLocalization.Get("NORMAL/damage").ToUpper();
				textEffectPowerDataAOld.text = (ConfigPrefsController.generalPowerBouldersDamageBase * (float)PlayerPrefs.GetInt("playerLevel") + ConfigPrefsController.generalPowerBouldersDamagePerLevel * (float)PlayerPrefsController.GeneralTechPowers_CatapultsShower).ToString("###,###,##0.#");
				textEffectPowerDataANew.text = (ConfigPrefsController.generalPowerBouldersDamageBase * (float)PlayerPrefs.GetInt("playerLevel") + ConfigPrefsController.generalPowerBouldersDamagePerLevel * (float)(PlayerPrefsController.GeneralTechPowers_CatapultsShower + 1)).ToString("###,###,##0.#");
				textEffectPowerB.text = ScriptLocalization.Get("GENERAL/cooldown").ToUpper();
				textEffectPowerDataBOld.text = "-" + (ConfigPrefsController.generalPowerBouldersCooldownPerLevel * (float)PlayerPrefsController.GeneralTechPowers_CatapultsShower).ToString("###,###,##0.0") + "s";
				textEffectPowerDataBNew.text = "-" + (ConfigPrefsController.generalPowerBouldersCooldownPerLevel * (float)(PlayerPrefsController.GeneralTechPowers_CatapultsShower + 1)).ToString("###,###,##0.0") + "s";
				textPowerLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechPowers_CatapultsShower.ToString();
				break;
			case 2:
				textDescriptionCost.text = ConfigPrefsController.generalPowerCostUpgrade_Arrows.ToString("###,###,##0") + " XP";
				num = PlayerPrefsController.GeneralTechPowers_ArchersShower;
				textEffectPowerDescription.text = descriptionUpgradePower[2];
				textEffectPowerA.text = ScriptLocalization.Get("NORMAL/damage").ToUpper();
				textEffectPowerDataAOld.text = (ConfigPrefsController.generalPowerArrowsDamageBase * (float)PlayerPrefs.GetInt("playerLevel") + ConfigPrefsController.generalPowerArrowsDamagePerLevel * (float)PlayerPrefsController.GeneralTechPowers_ArchersShower).ToString("###,###,##0.#");
				textEffectPowerDataANew.text = (ConfigPrefsController.generalPowerArrowsDamageBase * (float)PlayerPrefs.GetInt("playerLevel") + ConfigPrefsController.generalPowerArrowsDamagePerLevel * (float)(PlayerPrefsController.GeneralTechPowers_ArchersShower + 1)).ToString("###,###,##0.#");
				textEffectPowerB.text = ScriptLocalization.Get("GENERAL/cooldown").ToUpper();
				textEffectPowerDataBOld.text = "-" + (ConfigPrefsController.generalPowerArrowsCooldownPerLevel * (float)PlayerPrefsController.GeneralTechPowers_ArchersShower).ToString("###,###,##0.0") + "s";
				textEffectPowerDataBNew.text = "-" + (ConfigPrefsController.generalPowerArrowsCooldownPerLevel * (float)(PlayerPrefsController.GeneralTechPowers_ArchersShower + 1)).ToString("###,###,##0.0") + "s";
				textPowerLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechPowers_ArchersShower.ToString();
				break;
			}
			if (num >= ConfigPrefsController.generalPowerMaxUpgrade)
			{
				textEffectPowerDataANew.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
				textEffectPowerDataBNew.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
			}
			if (num < ConfigPrefsController.generalPowerMaxUpgrade && ((PlayerPrefs.GetInt("playerExpPoints") >= ConfigPrefsController.generalPowerCostUpgrade_WarCry && upgradeIndexSelected == 0) || (PlayerPrefs.GetInt("playerExpPoints") >= ConfigPrefsController.generalPowerCostUpgrade_Arrows && upgradeIndexSelected == 1) || (PlayerPrefs.GetInt("playerExpPoints") >= ConfigPrefsController.generalPowerCostUpgrade_Arrows && upgradeIndexSelected == 2)))
			{
				buttonUpgrade.interactable = true;
			}
			else
			{
				buttonUpgrade.interactable = false;
			}
			break;
		case UpgradeSelected.Base:
			textDescriptionTitle.text = titleUpgradeBase[upgradeIndexSelected];
			textArmyCost.text = ConfigPrefsController.generalBaseCostUpgrade.ToString("###,###,##0") + " XP";
			textEffectPowerDescription.text = string.Empty;
			textEffectPowerA.text = string.Empty;
			textEffectPowerDataAOld.text = string.Empty;
			textEffectPowerDataANew.text = string.Empty;
			textEffectPowerB.text = string.Empty;
			textEffectPowerDataBOld.text = string.Empty;
			textEffectPowerDataBNew.text = string.Empty;
			switch (upgradeIndexSelected)
			{
			case 0:
				num = PlayerPrefsController.GeneralTechBase_TowerDamage;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalBaseTowerDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_TowerDamage * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalBaseTowerDamagePerLevel * (float)(PlayerPrefsController.GeneralTechBase_TowerDamage + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechBase_TowerDamage.ToString();
				break;
			case 1:
				num = PlayerPrefsController.GeneralTechBase_CatapultDamage;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalBaseCatapultDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_CatapultDamage * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalBaseCatapultDamagePerLevel * (float)(PlayerPrefsController.GeneralTechBase_CatapultDamage + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechBase_CatapultDamage.ToString();
				break;
			case 2:
				num = PlayerPrefsController.GeneralTechBase_ArchersRange;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalBaseArchersRangePerLevel * (float)PlayerPrefsController.GeneralTechBase_ArchersRange * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalBaseArchersRangePerLevel * (float)(PlayerPrefsController.GeneralTechBase_ArchersRange + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechBase_ArchersRange.ToString();
				break;
			case 3:
				num = PlayerPrefsController.GeneralTechBase_ArchersDamage;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalBaseArchersDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_ArchersDamage * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalBaseArchersDamagePerLevel * (float)(PlayerPrefsController.GeneralTechBase_ArchersDamage + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechBase_ArchersDamage.ToString();
				break;
			case 4:
				num = PlayerPrefsController.GeneralTechBase_WallHp;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalBaseWallHpPerLevel * (float)PlayerPrefsController.GeneralTechBase_WallHp * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalBaseWallHpPerLevel * (float)(PlayerPrefsController.GeneralTechBase_WallHp + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechBase_WallHp.ToString();
				break;
			case 5:
				num = PlayerPrefsController.GeneralTechBase_IncomeColony;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalBaseIncomeColonyPerLevel * (float)PlayerPrefsController.GeneralTechBase_IncomeColony * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalBaseIncomeColonyPerLevel * (float)(PlayerPrefsController.GeneralTechBase_IncomeColony + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechBase_IncomeColony.ToString();
				break;
			case 6:
				num = PlayerPrefsController.GeneralTechBase_IncomeKill;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalBaseIncomeKillPerLevel * (float)PlayerPrefsController.GeneralTechBase_IncomeKill * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalBaseIncomeKillPerLevel * (float)(PlayerPrefsController.GeneralTechBase_IncomeKill + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechBase_IncomeKill.ToString();
				break;
			case 7:
				num = PlayerPrefsController.GeneralTechBase_ExperienceKill;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalBaseExperienceKillPerLevel * (float)PlayerPrefsController.GeneralTechBase_ExperienceKill * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalBaseExperienceKillPerLevel * (float)(PlayerPrefsController.GeneralTechBase_ExperienceKill + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechBase_ExperienceKill.ToString();
				break;
			}
			if (num >= ConfigPrefsController.generalBaseMaxUpgrade)
			{
				textEffectBonusDataNew.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
			}
			if (num < ConfigPrefsController.generalBaseMaxUpgrade && PlayerPrefs.GetInt("playerExpPoints") >= ConfigPrefsController.generalBaseCostUpgrade)
			{
				buttonUpgrade.interactable = true;
			}
			else
			{
				buttonUpgrade.interactable = false;
			}
			break;
		case UpgradeSelected.Army:
			textDescriptionTitle.text = titleUpgradeArmy[upgradeIndexSelected];
			textArmyCost.text = ConfigPrefsController.generalArmyCostUpgrade.ToString("###,###,##0") + " XP";
			textEffectPowerDescription.text = string.Empty;
			textEffectPowerA.text = string.Empty;
			textEffectPowerDataAOld.text = string.Empty;
			textEffectPowerDataANew.text = string.Empty;
			textEffectPowerB.text = string.Empty;
			textEffectPowerDataBOld.text = string.Empty;
			textEffectPowerDataBNew.text = string.Empty;
			switch (upgradeIndexSelected)
			{
			case 0:
				num = PlayerPrefsController.GeneralTechArmy_MeleeDamage;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalArmyMeleeDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_MeleeDamage * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalArmyMeleeDamagePerLevel * (float)(PlayerPrefsController.GeneralTechArmy_MeleeDamage + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechArmy_MeleeDamage.ToString();
				break;
			case 1:
				num = PlayerPrefsController.GeneralTechArmy_RangedDamage;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalArmyRangedDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_RangedDamage * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalArmyRangedDamagePerLevel * (float)(PlayerPrefsController.GeneralTechArmy_RangedDamage + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechArmy_RangedDamage.ToString();
				break;
			case 2:
				num = PlayerPrefsController.GeneralTechArmy_SiegeDamage;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalArmySiegeDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_SiegeDamage * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalArmySiegeDamagePerLevel * (float)(PlayerPrefsController.GeneralTechArmy_SiegeDamage + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechArmy_SiegeDamage.ToString();
				break;
			case 3:
				num = PlayerPrefsController.GeneralTechArmy_SiegeHealth;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalArmySiegeHealthPerLevel * (float)PlayerPrefsController.GeneralTechArmy_SiegeHealth * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalArmySiegeHealthPerLevel * (float)(PlayerPrefsController.GeneralTechArmy_SiegeHealth + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechArmy_SiegeHealth.ToString();
				break;
			case 4:
				num = PlayerPrefsController.GeneralTechArmy_SpawnCooldown;
				textEffectBonusDataOld.text = "-" + (ConfigPrefsController.generalArmySpawnCooldown * (float)PlayerPrefsController.GeneralTechArmy_SpawnCooldown * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "-" + (ConfigPrefsController.generalArmySpawnCooldown * (float)(PlayerPrefsController.GeneralTechArmy_SpawnCooldown + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechArmy_SpawnCooldown.ToString();
				break;
			case 5:
				num = PlayerPrefsController.GeneralTechArmy_HeroHealth;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalArmyHeroHealthPerLevel * (float)PlayerPrefsController.GeneralTechArmy_HeroHealth * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalArmyHeroHealthPerLevel * (float)(PlayerPrefsController.GeneralTechArmy_HeroHealth + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechArmy_HeroHealth.ToString();
				break;
			case 6:
				num = PlayerPrefsController.GeneralTechArmy_HeroDamage;
				textEffectBonusDataOld.text = "+" + (ConfigPrefsController.generalArmyHeroDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_HeroDamage * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "+" + (ConfigPrefsController.generalArmyHeroDamagePerLevel * (float)(PlayerPrefsController.GeneralTechArmy_HeroDamage + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechArmy_HeroDamage.ToString();
				break;
			case 7:
				num = PlayerPrefsController.GeneralTechArmy_MercenaryCost;
				textEffectBonusDataOld.text = "-" + (ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost * 100f).ToString("###,###,##0.##") + "%";
				textEffectBonusDataNew.text = "-" + (ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)(PlayerPrefsController.GeneralTechArmy_MercenaryCost + 1) * 100f).ToString("###,###,##0.##") + "%";
				textArmyLevel.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.GeneralTechArmy_MercenaryCost.ToString();
				break;
			}
			switch (upgradeIndexSelected)
			{
			case 0:
				uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_MeleeDamage, ConfigPrefsController.generalArmyMaxUpgrade);
				break;
			case 1:
				uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_RangedDamage, ConfigPrefsController.generalArmyMaxUpgrade);
				break;
			case 2:
				uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_SiegeDamage, ConfigPrefsController.generalArmyMaxUpgrade);
				break;
			case 3:
				uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_SiegeHealth, ConfigPrefsController.generalArmyMaxUpgrade);
				break;
			case 4:
				uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_SpawnCooldown, ConfigPrefsController.generalArmyMaxUpgrade);
				break;
			case 5:
				uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_HeroHealth, ConfigPrefsController.generalArmyMaxUpgrade);
				break;
			case 6:
				uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_HeroDamage, ConfigPrefsController.generalArmyMaxUpgrade);
				break;
			case 7:
				uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_MercenaryCost, ConfigPrefsController.generalArmyMaxUpgrade);
				break;
			}
			if (num >= ConfigPrefsController.generalArmyMaxUpgrade)
			{
				textEffectBonusDataNew.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
			}
			if (num < ConfigPrefsController.generalArmyMaxUpgrade && PlayerPrefs.GetInt("playerExpPoints") >= ConfigPrefsController.generalArmyCostUpgrade)
			{
				buttonUpgrade.interactable = true;
			}
			else
			{
				buttonUpgrade.interactable = false;
			}
			break;
		}
	}

	private void UpdatePowersBars(int upgrade, int upgradable)
	{
		for (int i = 0; i < powerBars.Length; i++)
		{
			uiController.UpdateLevelBars(powerBars[i], upgrade, upgradable);
		}
	}

	private void UpdateArmyBars(int index)
	{
		switch (index)
		{
		case 0:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_MeleeDamage, ConfigPrefsController.generalArmyMaxUpgrade);
			break;
		case 1:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_RangedDamage, ConfigPrefsController.generalArmyMaxUpgrade);
			break;
		case 2:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_SiegeDamage, ConfigPrefsController.generalArmyMaxUpgrade);
			break;
		case 3:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_SiegeHealth, ConfigPrefsController.generalArmyMaxUpgrade);
			break;
		case 4:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_SpawnCooldown, ConfigPrefsController.generalArmyMaxUpgrade);
			break;
		case 5:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_HeroHealth, ConfigPrefsController.generalArmyMaxUpgrade);
			break;
		case 6:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_HeroDamage, ConfigPrefsController.generalArmyMaxUpgrade);
			break;
		case 7:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechArmy_MercenaryCost, ConfigPrefsController.generalArmyMaxUpgrade);
			break;
		}
	}

	private void UpdateBaseBars(int index)
	{
		switch (index)
		{
		case 0:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechBase_TowerDamage, ConfigPrefsController.generalBaseMaxUpgrade);
			break;
		case 1:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechBase_CatapultDamage, ConfigPrefsController.generalBaseMaxUpgrade);
			break;
		case 2:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechBase_ArchersRange, ConfigPrefsController.generalBaseMaxUpgrade);
			break;
		case 3:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechBase_ArchersDamage, ConfigPrefsController.generalBaseMaxUpgrade);
			break;
		case 4:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechBase_WallHp, ConfigPrefsController.generalBaseMaxUpgrade);
			break;
		case 5:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechBase_IncomeColony, ConfigPrefsController.generalBaseMaxUpgrade);
			break;
		case 6:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechBase_IncomeKill, ConfigPrefsController.generalBaseMaxUpgrade);
			break;
		case 7:
			uiController.UpdateLevelBars(armyBar, PlayerPrefsController.GeneralTechBase_ExperienceKill, ConfigPrefsController.generalBaseMaxUpgrade);
			break;
		}
	}
}
