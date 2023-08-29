using I2.Loc;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public static bool IsShootingCatapult;

	public static int CatapultIndex;

	public static bool IsShootingPower;

	public static int PowerIndex;

	public static UIController instance;

	public GameObject warfareObject;

	public GameObject canvasStats;

	public GameObject canvasInvasion;

	public GameObject canvasAttack;

	public GameObject canvasDefence;

	public GameObject canvasUpgrade;

	public GameObject canvasUpgradeTower;

	public GameObject canvasUpgradeCatapult;

	public GameObject canvasUpgradeHeroes;

	public GameObject canvasUpgradeBank;

	public RectTransform[] canvasTowerTransform;

	public RectTransform[] canvasCatapultTransform;

	public GameObject subCanvasUpgradeGeneral;

	public GameObject subCanvasUpgradeUnits;

	public GameObject subCanvasUpgradeTurn;

	public Image wallLevel;

	public Image archersLevel;

	public Image towersLevel;

	public Image catapultsLevel;

	public RectTransform transformLife;

	public RectTransform transformMoney;

	public RectTransform transformExp;

	public RectTransform transformWarcry;

	public RectTransform transformOptions;

	public RectTransform transformWaveBar;

	public Text gateLifeText;

	public Image gateLifeImage;

	public Text playerLevelText;

	public Text playerExpCountText;

	public Image playerLevelImage;

	public Text playerMoneyText;

	public Image playerWaveImage;

	public GameObject playerWarcryBar;

	public Text textWarcryBar;

	public Image playerWarcryImage;

	public GameObject playerExtraDamage;

	public Text textExtraDamage;

	public Image playerSpawnImage;

	public Text playerSpawnText;

	public Sprite[] spriteCardsMelee;

	public Sprite[] spriteCardsSpear;

	public Sprite[] spriteCardsRanged;

	public Sprite[] spriteCardsMounted;

	public Sprite[] spriteTowerAmmo;

	public Sprite[] spriteCatapultAmmo;

	public Sprite[] spriteCatapultAmmoButton;

	public GameObject[] upgradeTower = new GameObject[6];

	public GameObject[] upgradeCatapult = new GameObject[2];

	public Button[] buttonUpgradeTower = new Button[6];

	public Button[] buttonUpgradeCatapult = new Button[2];

	public Button[] buttonUpgradeHeroe = new Button[2];

	public Image[] imageHeroIcon;

	public Sprite[] heroIcon;

	public Button[] buttonDefenceUnits = new Button[4];

	public Image[] buttonDefenceUnitsImage = new Image[4];

	public Button[] buttonDefencePowers = new Button[3];

	public Image[] buttonDefencePowersImage = new Image[3];

	public Image[] buttonDefenceIcon;

	public Sprite[] buttonDefencePowerOff;

	public Sprite[] buttonDefencePowerOn;

	public Sprite spriteButtonDefencePowersBoosted;

	public Sprite spriteButtonDefencePowersNormal;

	public GameObject[] powersBoosted;

	public Text[] textPowersBoostedWavesLeft;

	public Button buttonDefenceTower;

	public Image buttonTowerImage;

	public Image buttonTowerIcon;

	public Sprite[] buttonTowerOnOff;

	public Button[] buttonDefenceCatapult = new Button[2];

	public Image[] buttonCatapultImage = new Image[2];

	public Image[] buttonCatapultImageChange = new Image[2];

	public Sprite[] buttonCatapultOnOff;

	public Button[] buttonAttackPowers = new Button[3];

	public Image[] buttonAttackPowersImage = new Image[3];

	public Image[] buttonAttackPowersIcon;

	public Button[] upgradeButtons;

	public Text[] upgradeTextLvl;

	public Text[] upgradeTextPrice;

	public Text[] upgradeTextEffect;

	public RectTransform upgradeHeroEffect;

	public RectTransform upgradeUnitsEffect;

	public GameObject buttonPauseObject;

	public GameObject buttonSpeedObject;

	public Image buttonSpeed;

	public Sprite[] spriteSpeed;

	public GameObject subCanvasTowers;

	public GameObject subCanvasCatapults;

	public Image moneyBoosted;

	public Image xpBoosted;

	public GameObject[] boosted;

	public Text textXpBoosted;

	public Text textMoneyBoosted;

	public GameObject rubyPanel;

	public Text rubyAmountText;

	public UIIncomeColonies uiIncomeColonies;

	public WarfareZone warfareZone;

	private bool[] usedBoostedPower = new bool[3];

	private int citiesTotalConquered;

	private UIBankIcon uiBankIcon;

	private Touch_Battle touchBattle;

	private MainController mainController;

	private UpgradesController upgradesController;

	private WaveController waveController;

	private TutorialController tutorialController;

	private AchievementsController achievementsController;

	private BankController bankController;

	private StationEngine stationEngine;

	private MusicController musicScript;

	private SfxUIController sfxUIController;

	private float timeToEnableButtons = 1f;

	private GameObject tutorialSpeedLabel;

	private GameObject tutorialScrollLabel;

	private bool tutorialSpeedLabelShown;

	private bool tutorialScrollLabelShown;

	public GameObject TutorialScrollLabel
	{
		get
		{
			return tutorialScrollLabel;
		}
		set
		{
			tutorialScrollLabel = value;
		}
	}

	private void Awake()
	{
		touchBattle = base.gameObject.GetComponent<Touch_Battle>();
		tutorialController = base.gameObject.GetComponent<TutorialController>();
		musicScript = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
		sfxUIController = base.gameObject.GetComponent<SfxUIController>();
		achievementsController = base.gameObject.GetComponent<AchievementsController>();
		bankController = base.gameObject.GetComponent<BankController>();
		uiBankIcon = canvasUpgradeBank.GetComponent<UIBankIcon>();
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		warfareObject.SetActive(value: false);
		playerWarcryBar.SetActive(value: false);
		instance = this;
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			transformLife.anchoredPosition = new Vector2(164f, -12f);
			transformWaveBar.anchoredPosition = new Vector2(-79.2f, -12f);
			transformWaveBar.sizeDelta = new Vector2(196f, 45f);
			rubyPanel.SetActive(value: false);
			transformOptions.gameObject.SetActive(value: false);
		}
		else if (!PlayerPrefsController.tutorialSteps[27])
		{
			transformOptions.gameObject.SetActive(value: false);
		}
	}

	private void Start()
	{
		for (int i = 0; i < buttonUpgradeTower.Length; i++)
		{
			buttonUpgradeTower[i].GetComponent<Image>().sprite = spriteTowerAmmo[(int)PlayerPrefsController.TowerAmmo[i]];
		}
		for (int j = 0; j < buttonUpgradeCatapult.Length; j++)
		{
			buttonUpgradeCatapult[j].GetComponent<Image>().sprite = spriteCatapultAmmo[(int)PlayerPrefsController.CatapultAmmo[j]];
		}
		UpdateBoostsText();
		CheckBoosts();
		UpdateHeroIcon();
		UpdateSpeedButton();
		SetInitialBankStatus();
		UpdateExclamationGeneral();
		UpdateExclamationUnits();
	}

	private void Update()
	{
		if (timeToEnableButtons > 0f)
		{
			timeToEnableButtons -= Time.deltaTime;
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && tutorialController.GetActualIndex() != 7 && tutorialController.GetActualIndex() != 8 && tutorialController.GetActualIndex() != 12 && tutorialController.GetActualIndex() != 13 && tutorialController.GetActualIndex() != 16)
		{
			if (MainController.worldScreen == WorldScreen.Attack)
			{
				ButtonPressPause();
			}
			else if (MainController.worldScreen == WorldScreen.AttackStarted || MainController.worldScreen == WorldScreen.Defence)
			{
				if (MainController.ExpectingFinishGame)
				{
					ButtonPressPause();
				}
			}
			else if (MainController.worldScreen == WorldScreen.Upgrade && !Touch_Battle.IsWindowBigOpen && !Touch_Battle.IsWindowSmallOpen)
			{
				ButtonPressPause();
			}
		}
		if (upgradesController.GateController != null)
		{
			gateLifeText.text = mainController.GateLifeActual.ToString("###,###,##0") + " / " + mainController.GateLife.ToString("###,###,##0");
			gateLifeImage.fillAmount = mainController.GateLifeActual / mainController.GateLife;
		}
		playerLevelText.text = ScriptLocalization.Get("NORMAL/level_ab").ToUpper() + " " + PlayerPrefsController.playerLevel.ToString("###,###,##0");
		playerLevelImage.fillAmount = PlayerPrefsController.playerExp / (float)PlayerPrefsController.playerExpLevels[PlayerPrefsController.playerLevel];
		playerMoneyText.text = Mathf.FloorToInt(PlayerPrefs.GetFloat("playerMoney")).ToString("###,###,##0");
		uiIncomeColonies.textNoIncome.text = Mathf.FloorToInt(PlayerPrefs.GetFloat("playerMoney")).ToString("###,###,##0");
		int num = (int)PlayerPrefsController.playerExp;
		playerExpCountText.text = num.ToString();
		playerWaveImage.fillAmount = waveController.WaveTimeActual / waveController.WaveTime;
		if (upgradesController.TimeWarcryActual > 0f)
		{
			upgradesController.TimeWarcryActual -= Time.deltaTime;
			if (upgradesController.TimeWarcryActual < 0f)
			{
				upgradesController.TimeWarcryActual = 0f;
			}
			playerWarcryImage.fillAmount = upgradesController.TimeWarcryActual / upgradesController.TimeWarcry;
			if (upgradesController.TimeWarcryActual <= 0f)
			{
				upgradesController.FinishWarCry();
			}
		}
		if (upgradesController.TowersController[0] != null)
		{
			bool flag = false;
			if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
			{
				upgradesController.TowerTimeReloadCounter += Time.deltaTime * (ConfigPrefsController.cooldownTowerBase + ConfigPrefsController.cooldownTowerBase * ConfigPrefsController.cooldownTowerPerLevel * (float)PlayerPrefsController.TowerLvl);
			}
			else
			{
				upgradesController.TowerTimeReloadCounter += Time.deltaTime * (ConfigPrefsController.cooldownTowerBase + ConfigPrefsController.cooldownTowerBase * ConfigPrefsController.cooldownTowerPerLevel * (float)EnemyPrefsController.TowerLvl);
			}
			buttonTowerImage.fillAmount = upgradesController.TowerTimeReloadCounter / upgradesController.TowerTimeReload;
			if (upgradesController.TowerTimeReloadCounter >= upgradesController.TowerTimeReload)
			{
				for (int i = 0; i < upgradesController.TowersController.Length; i++)
				{
					if (upgradesController.TowersController[i] != null && upgradesController.TowersController[i].TargetObject != null)
					{
						flag = true;
						break;
					}
				}
			}
			if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
			{
				if (flag)
				{
					if (!buttonDefenceTower.IsInteractable())
					{
						buttonDefenceTower.interactable = true;
						buttonTowerIcon.sprite = buttonTowerOnOff[0];
					}
				}
				else if (buttonDefenceTower.IsInteractable())
				{
					buttonDefenceTower.interactable = false;
					buttonTowerIcon.sprite = buttonTowerOnOff[1];
				}
			}
			else if (MainController.worldScreen == WorldScreen.AttackStarted && flag)
			{
				ButtonPressShootTower();
			}
		}
		if (MainController.worldScreen == WorldScreen.AttackStarted || MainController.worldScreen == WorldScreen.Defence)
		{
			bool[] array = new bool[3];
			if (PlayerPrefsController.GeneralTechPowers_WarCry > 0)
			{
				array[0] = true;
			}
			if (PlayerPrefsController.GeneralTechPowers_CatapultsShower > 0)
			{
				array[1] = true;
			}
			if (PlayerPrefsController.GeneralTechPowers_ArchersShower > 0)
			{
				array[2] = true;
			}
			for (int j = 0; j < mainController.TimeReloadPower.Length; j++)
			{
				if (!array[j])
				{
					continue;
				}
				if (MainController.worldScreen == WorldScreen.Defence)
				{
					buttonDefencePowersImage[j].fillAmount = mainController.TimeReloadPowerCounter[j] / mainController.TimeReloadPower[j];
					if (mainController.TimeReloadPowerCounter[j] >= mainController.TimeReloadPower[j])
					{
						if (!buttonDefencePowers[j].IsInteractable())
						{
							buttonDefencePowers[j].interactable = true;
							buttonDefenceIcon[j].sprite = buttonDefencePowerOn[j];
						}
					}
					else if (buttonDefencePowers[j].IsInteractable())
					{
						buttonDefencePowers[j].interactable = false;
						buttonDefenceIcon[j].sprite = buttonDefencePowerOff[j];
					}
				}
				else
				{
					if (MainController.worldScreen != WorldScreen.AttackStarted)
					{
						continue;
					}
					buttonAttackPowersImage[j].fillAmount = mainController.TimeReloadPowerCounter[j] / mainController.TimeReloadPower[j];
					if (mainController.TimeReloadPowerCounter[j] >= mainController.TimeReloadPower[j])
					{
						if (!buttonAttackPowers[j].IsInteractable())
						{
							buttonAttackPowers[j].interactable = true;
							buttonAttackPowersIcon[j].sprite = buttonDefencePowerOn[j];
						}
					}
					else if (buttonAttackPowers[j].IsInteractable())
					{
						buttonAttackPowers[j].interactable = false;
						buttonAttackPowersIcon[j].sprite = buttonDefencePowerOff[j];
					}
				}
			}
		}
		for (int k = 0; k < upgradesController.CatapultsController.Length; k++)
		{
			if (!(upgradesController.CatapultsController[k] != null))
			{
				continue;
			}
			bool flag2 = false;
			buttonCatapultImage[k].fillAmount = upgradesController.CatapultsController[k].TimeReloadCounter / upgradesController.CatapultsController[k].TimeReload;
			if (upgradesController.CatapultsController[k].TimeReloadCounter >= upgradesController.CatapultsController[k].TimeReload)
			{
				flag2 = true;
			}
			if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
			{
				if (flag2)
				{
					if (!buttonDefenceCatapult[k].IsInteractable())
					{
						buttonDefenceCatapult[k].interactable = true;
						UpdateCatapultImage(k, on: true);
					}
				}
				else if (buttonDefenceCatapult[k].IsInteractable())
				{
					buttonDefenceCatapult[k].interactable = false;
					UpdateCatapultImage(k, on: false);
				}
			}
			else if (MainController.worldScreen == WorldScreen.AttackStarted && flag2)
			{
				ButtonPressShootCatapultAI(k);
			}
		}
		if (MainController.worldScreen != WorldScreen.Defence && MainController.worldScreen != WorldScreen.AttackStarted)
		{
			return;
		}
		bool flag3 = false;
		bool flag4 = false;
		playerSpawnImage.fillAmount = upgradesController.TimeSpawn;
		switch ((int)(upgradesController.TimeSpawn / ConfigPrefsController.spawnTimeRequiredPerUnit))
		{
		case 1:
			playerSpawnText.text = "1";
			break;
		case 2:
			playerSpawnText.text = "2";
			break;
		default:
			playerSpawnText.text = "0";
			break;
		}
		if (upgradesController.TimeSpawn >= ConfigPrefsController.spawnTimeRequiredPerUnit)
		{
			flag3 = true;
		}
		if (upgradesController.EnemyTimeSpawn >= EnemyPrefsController.SpawnTimeUnits)
		{
			flag4 = true;
		}
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			if (flag3)
			{
				for (int l = 0; l < buttonDefenceUnits.Length; l++)
				{
					buttonDefenceUnits[l].interactable = true;
				}
			}
			else
			{
				for (int m = 0; m < buttonDefenceUnits.Length; m++)
				{
					buttonDefenceUnits[m].interactable = false;
				}
			}
		}
		else if (MainController.worldScreen == WorldScreen.AttackStarted && flag4)
		{
			int unitType = UnityEngine.Random.Range(1, 4);
			int num2 = (int)Mathf.Lerp(1f, 8f, (float)EnemyPrefsController.UnitsPerCohort / 100f);
			if (num2 <= 0)
			{
				num2 = 1;
			}
			if (num2 > 8)
			{
				num2 = 8;
			}
			upgradesController.SpawnUnitBarbarian(CohortStance.Defender, EnemyPrefsController.FactionSelected, (UnitType)unitType, EnemyPrefsController.UnitsSkin, num2, 0);
		}
	}

	public void SetInitialStructure()
	{
		mainController = base.gameObject.GetComponent<MainController>();
		upgradesController = base.gameObject.GetComponent<UpgradesController>();
		waveController = base.gameObject.GetComponent<WaveController>();
	}

	public void SetUpgradeButtonsDependingWallHeight(int _levelWall)
	{
		float num = 0f;
		for (int i = 0; i < canvasTowerTransform.Length; i++)
		{
			switch (_levelWall)
			{
			case 0:
				num = 1.5f;
				break;
			case 1:
				num = ((i != 2) ? 1.7f : 1.5f);
				break;
			case 2:
				switch (i)
				{
				case 2:
					num = 1.6f;
					break;
				case 3:
					num = 2.6f;
					break;
				case 4:
					num = 1.6f;
					break;
				default:
					num = 2f;
					break;
				}
				break;
			case 3:
				switch (i)
				{
				case 0:
					num = 3.2f;
					break;
				case 3:
				case 5:
					num = 2.9f;
					break;
				default:
					num = 2.5f;
					break;
				}
				break;
			}
			RectTransform obj = canvasTowerTransform[i];
			Vector3 anchoredPosition3D = canvasTowerTransform[i].anchoredPosition3D;
			float x = anchoredPosition3D.x;
			float y = num;
			Vector3 anchoredPosition3D2 = canvasTowerTransform[i].anchoredPosition3D;
			obj.anchoredPosition3D = new Vector3(x, y, anchoredPosition3D2.z);
		}
		for (int j = 0; j < canvasCatapultTransform.Length; j++)
		{
			switch (_levelWall)
			{
			case 0:
				num = 1.5f;
				break;
			case 1:
				num = 1.7f;
				break;
			case 2:
				num = 2f;
				break;
			case 3:
				num = 2.5f;
				break;
			}
			RectTransform obj2 = canvasCatapultTransform[j];
			Vector3 anchoredPosition3D3 = canvasCatapultTransform[j].anchoredPosition3D;
			float x2 = anchoredPosition3D3.x;
			float y2 = num;
			Vector3 anchoredPosition3D4 = canvasCatapultTransform[j].anchoredPosition3D;
			obj2.anchoredPosition3D = new Vector3(x2, y2, anchoredPosition3D4.z);
		}
	}

	public void ChangeScreen(WorldScreen _screenToLoad)
	{
		warfareZone.DestroyTarget(_isCanceled: true, Vector3.zero);
		canvasDefence.SetActive(value: false);
		canvasUpgrade.SetActive(value: false);
		canvasUpgradeTower.SetActive(value: false);
		canvasUpgradeCatapult.SetActive(value: false);
		canvasUpgradeHeroes.SetActive(value: false);
		canvasUpgradeBank.SetActive(value: false);
		canvasInvasion.SetActive(value: false);
		canvasAttack.SetActive(value: false);
		switch (_screenToLoad)
		{
		case WorldScreen.Upgrade:
			buttonPauseObject.SetActive(value: false);
			buttonSpeedObject.SetActive(value: false);
			playerExtraDamage.SetActive(value: false);
			rubyPanel.SetActive(value: true);
			if (PlayerPrefsController.tutorialSteps[27])
			{
				transformOptions.gameObject.SetActive(value: true);
			}
			canvasStats.SetActive(value: true);
			UpdateUIUpgrade();
			UpdateAllBars();
			UpdateBoostsText();
			if (uiBankIcon != null)
			{
				SetInitialBankStatus();
			}
			transformWaveBar.anchoredPosition = new Vector2(47f, -12f);
			transformWaveBar.sizeDelta = new Vector2(140f, 45f);
			rubyPanel.SetActive(value: true);
			break;
		case WorldScreen.Defence:
			buttonPauseObject.SetActive(value: true);
			buttonSpeedObject.SetActive(value: true);
			rubyPanel.SetActive(value: false);
			UpdateBoostsText();
			if (PlayerPrefsController.tutorialSteps[27])
			{
				transformOptions.gameObject.SetActive(value: false);
			}
			UpdateUIDefence();
			transformWaveBar.anchoredPosition = new Vector2(78f, -12f);
			transformWaveBar.sizeDelta = new Vector2(196f, 45f);
			rubyPanel.SetActive(value: false);
			break;
		case WorldScreen.Attack:
			buttonPauseObject.SetActive(value: true);
			buttonSpeedObject.SetActive(value: false);
			playerExtraDamage.SetActive(value: false);
			UpdateUIInvasion();
			break;
		case WorldScreen.AttackStarted:
			buttonSpeedObject.SetActive(value: true);
			UpdateUIAttack();
			break;
		}
	}

	public void WinWave()
	{
		if (tutorialScrollLabel != null)
		{
			UnityEngine.Object.Destroy(tutorialScrollLabel);
		}
		if (tutorialSpeedLabel != null)
		{
			UnityEngine.Object.Destroy(tutorialSpeedLabel);
		}
		sfxUIController.PlaySound(SfxUI.WinWave);
		Object.Instantiate(Resources.Load("Canvas/Message/CanvasWaveWin"));
		touchBattle.MoveCameraToWalls();
		UpdateExclamationGeneral();
		UpdateExclamationUnits();
	}

	public void LoseWave()
	{
		if (tutorialScrollLabel != null)
		{
			UnityEngine.Object.Destroy(tutorialScrollLabel);
		}
		if (tutorialSpeedLabel != null)
		{
			UnityEngine.Object.Destroy(tutorialSpeedLabel);
		}
		sfxUIController.PlaySound(SfxUI.LoseWave);
		Object.Instantiate(Resources.Load("Canvas/Message/CanvasWaveLose"));
		touchBattle.MoveCameraToWalls();
		UpdateExclamationGeneral();
		UpdateExclamationUnits();
	}

	public void WinInvasion()
	{
		sfxUIController.PlaySound(SfxUI.WinInvasion);
		Object.Instantiate(Resources.Load("Canvas/Message/CanvasInvasionWin"));
	}

	public void LoseInvasion()
	{
		sfxUIController.PlaySound(SfxUI.LoseInvasion);
		Object.Instantiate(Resources.Load("Canvas/Message/CanvasInvasionLose"));
	}

	public void ButtonPressPause()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		mainController.HideOffer();
		if (Time.timeScale > 0f)
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
			{
				Object.Instantiate(Resources.Load("Windows/CanvasPause"));
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXEditor)
			{
				Object.Instantiate(Resources.Load("Windows/CanvasPauseiOS"));
			}
		}
	}

	public void ButtonPressSpeed()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		if (tutorialController.GetActualIndex() != 2)
		{
			if (PlayerPrefsController.isFastForward)
			{
				PlayerPrefsController.isFastForward = false;
			}
			else
			{
				PlayerPrefsController.isFastForward = true;
			}
			PlayerPrefsController.SaveSpeedPrefs();
			UpdateSpeedButton();
		}
		if (tutorialSpeedLabel != null)
		{
			UnityEngine.Object.Destroy(tutorialSpeedLabel);
		}
	}

	public void ButtonPressStore()
	{
		if (!Touch_Battle.IsWindowBigOpen)
		{
			Touch_Battle.IsWindowBigOpen = true;
			DestroySmallMenues();
			sfxUIController.PlaySound(SfxUI.ClickDefault);
			mainController.HideOffer();
			subCanvasUpgradeGeneral.SetActive(value: false);
			subCanvasUpgradeUnits.SetActive(value: false);
			subCanvasUpgradeTurn.SetActive(value: false);
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/CanvasStore")) as GameObject;
			StoreUIController component = gameObject.GetComponent<StoreUIController>();
			component.Initialize(this, sfxUIController, mainController);
		}
	}

	public void UpdateSpeedButton()
	{
		if (tutorialController.GetActualIndex() == 2)
		{
			return;
		}
		if (PlayerPrefsController.isFastForward)
		{
			if (MainController.worldScreen == WorldScreen.Upgrade || MainController.worldScreen == WorldScreen.Attack)
			{
				Time.timeScale = ConfigPrefsController.speedNormal;
			}
			else
			{
				Time.timeScale = ConfigPrefsController.speedFast;
			}
			buttonSpeed.sprite = spriteSpeed[1];
		}
		else
		{
			Time.timeScale = ConfigPrefsController.speedNormal;
			buttonSpeed.sprite = spriteSpeed[0];
		}
	}

	public void ButtonPressSpawnDefenceSpear()
	{
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			upgradesController.OrderGeneral();
		}
		upgradesController.SpawnUnitRoman(_isInitialArmy: false, 0, CohortStance.Defender, Faction.Humans, UnitType.Spear, _isHeroSpawning: false, 0, 0);
	}

	public void ButtonPressSpawnDefenceCavalry()
	{
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			upgradesController.OrderGeneral();
		}
		upgradesController.SpawnUnitRoman(_isInitialArmy: false, 0, CohortStance.Defender, Faction.Humans, UnitType.Mounted, _isHeroSpawning: false, 0, 0);
	}

	public void ButtonPressSpawnDefenceMeelee()
	{
		if (tutorialController.GetActualIndex() == 2)
		{
			tutorialController.ActivateStep(3);
			Invoke("ShowScrollHand", 3.5f);
		}
		else if (tutorialController.GetActualIndex() == 6)
		{
			tutorialController.ClearGlow();
			Invoke("ShowSpeedLabel", 5f);
		}
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			upgradesController.OrderGeneral();
		}
		upgradesController.SpawnUnitRoman(_isInitialArmy: false, 0, CohortStance.Defender, Faction.Humans, UnitType.Sword, _isHeroSpawning: false, 0, 0);
	}

	public void ButtonPressSpawnDefenceArrow()
	{
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			upgradesController.OrderGeneral();
		}
		upgradesController.SpawnUnitRoman(_isInitialArmy: false, 0, CohortStance.Defender, Faction.Humans, UnitType.Archer, _isHeroSpawning: false, 0, 0);
	}

	public void ButtonPressPower(int _powerIndex)
	{
		if (MainController.worldScreen == WorldScreen.Upgrade || MainController.worldScreen == WorldScreen.Defence)
		{
			buttonDefencePowers[_powerIndex].interactable = false;
			buttonDefenceIcon[_powerIndex].sprite = buttonDefencePowerOff[_powerIndex];
			UpdatePowersBoosted(used: true, _powerIndex);
		}
		else if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			buttonAttackPowers[_powerIndex].interactable = false;
			buttonAttackPowersIcon[_powerIndex].sprite = buttonDefencePowerOff[_powerIndex];
		}
		if (_powerIndex > 0)
		{
			if (MainController.worldScreen == WorldScreen.Upgrade || MainController.worldScreen == WorldScreen.Defence)
			{
				canvasDefence.SetActive(value: false);
			}
			else if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
			{
				canvasAttack.SetActive(value: false);
			}
			warfareObject.SetActive(value: true);
			IsShootingPower = true;
			PowerIndex = _powerIndex - 1;
		}
		else
		{
			upgradesController.OrderGeneral();
			upgradesController.OrderGeneralWarcry();
		}
	}

	public void ButtonPressShootCatapult(int _catapultIndex)
	{
		buttonDefenceCatapult[_catapultIndex].interactable = false;
		IsShootingCatapult = true;
		CatapultIndex = _catapultIndex;
		canvasDefence.SetActive(value: false);
		warfareObject.SetActive(value: true);
	}

	public void ButtonPressShootCatapultAI(int _catapultIndex)
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < upgradesController.CohortsFriendArray.Count; i++)
		{
			for (int j = 0; j < upgradesController.CohortsFriendArray[i].soldiersArray.Length; j++)
			{
				if (upgradesController.CohortsFriendArray[i].soldiersArray[j].StatsHP > 0f && upgradesController.CohortsFriendArray[i].soldiersArray[j].MyTransform != null)
				{
					Vector3 position = upgradesController.CohortsFriendArray[i].soldiersArray[j].MyTransform.position;
					if (position.x < 68f)
					{
						list.Add(upgradesController.CohortsFriendArray[i].soldiersArray[j].MyTransform.position);
					}
				}
			}
		}
		if (list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			upgradesController.CatapultsController[_catapultIndex].ShootBoulder(list[index]);
		}
	}

	public void ButtonPressShootTower()
	{
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
		}
		upgradesController.TowerTimeReloadCounter = 0f;
		buttonDefenceTower.interactable = false;
		buttonTowerIcon.sprite = buttonTowerOnOff[1];
		for (int i = 0; i < upgradesController.TowersController.Length; i++)
		{
			if (upgradesController.TowersController[i] != null && upgradesController.TowersController[i].TargetObject != null)
			{
				upgradesController.TowersController[i].ShootVolt();
			}
		}
		upgradesController.OrderGeneral();
	}

	public void FinishedPowerShoot(bool _isCanceled, Vector3 _positionTarget)
	{
		if (!_isCanceled)
		{
			if (MainController.worldScreen == WorldScreen.Upgrade || MainController.worldScreen == WorldScreen.Defence)
			{
				upgradesController.OrderGeneral();
			}
			upgradesController.ShootPower(_positionTarget);
		}
		if (MainController.worldScreen == WorldScreen.Upgrade || MainController.worldScreen == WorldScreen.Defence)
		{
			canvasDefence.SetActive(value: true);
		}
		else if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			canvasAttack.SetActive(value: true);
		}
		warfareObject.SetActive(value: false);
		IsShootingPower = false;
	}

	public void FinishedCatapultShoot(bool _isCanceled, Vector3 _positionTarget)
	{
		if (!_isCanceled)
		{
			upgradesController.OrderGeneral();
			upgradesController.CatapultsController[CatapultIndex].ShootBoulder(_positionTarget);
		}
		if (MainController.worldScreen == WorldScreen.Defence)
		{
			canvasDefence.SetActive(value: true);
		}
		warfareObject.SetActive(value: false);
		IsShootingCatapult = false;
	}

	public void ButtonPressUpgradeWall()
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		PlayerPrefsController.WallLvl++;
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)PlayerPrefsController.upgradeWallPrices[PlayerPrefsController.WallLvl]);
		upgradesController.SetInitialStructure(_setHeroes: false);
		UpdateUIUpgrade();
		UpdateLevelBars(wallLevel, PlayerPrefsController.WallLvl, ConfigPrefsController.upgradeWallMax);
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
		string text = FormatString(PlayerPrefsController.upgradeWallPrices[PlayerPrefsController.WallLvl], tripleFormat: false);
		stationEngine.SendAnalyticSelectContent("Walls", text);
		stationEngine.SendAnalyticSpendVirtualCurrency("Walls", "[GOLD]", text);
		UpdateExclamationUnits();
		switch (tutorialController.GetActualIndex())
		{
		case 5:
			if (PlayerPrefsController.WallLvl >= 1 && PlayerPrefsController.ArchersLvl >= 1)
			{
				tutorialController.ActivateStep(6);
				break;
			}
			tutorialController.ClearGlow();
			tutorialController.SetGlow(2);
			break;
		case 15:
			if (PlayerPrefsController.WallLvl >= 3)
			{
				tutorialController.ActivateStep(16);
			}
			break;
		}
	}

	public void ButtonPressUpgradeArcher()
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)PlayerPrefsController.upgradeArcherPrices[PlayerPrefsController.ArchersLvl]);
		PlayerPrefsController.ArchersLvl++;
		upgradesController.SetInitialStructure(_setHeroes: false);
		UpdateUIUpgrade();
		UpdateLevelBars(archersLevel, PlayerPrefsController.ArchersLvl, ConfigPrefsController.upgradeArchersMax);
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
		string text = FormatString(PlayerPrefsController.upgradeArcherPrices[PlayerPrefsController.ArchersLvl], tripleFormat: false);
		stationEngine.SendAnalyticSelectContent("Archers", text);
		stationEngine.SendAnalyticSpendVirtualCurrency("Archers", "[GOLD]", text);
		UpdateExclamationUnits();
		if (tutorialController.GetActualIndex() == 5)
		{
			if (PlayerPrefsController.WallLvl >= 1 && PlayerPrefsController.ArchersLvl >= 1)
			{
				tutorialController.ActivateStep(6);
				return;
			}
			tutorialController.ClearGlow();
			tutorialController.SetGlow(1);
		}
	}

	public void ButtonPressUpgradeBallista()
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)PlayerPrefsController.upgradeTowerPrices[PlayerPrefsController.TowerLvl]);
		PlayerPrefsController.TowerLvl++;
		upgradesController.SetInitialStructure(_setHeroes: false);
		UpdateUIUpgrade();
		UpdateLevelBars(towersLevel, PlayerPrefsController.TowerLvl, ConfigPrefsController.upgradeTowerMax);
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
		string text = FormatString(PlayerPrefsController.upgradeTowerPrices[PlayerPrefsController.TowerLvl], tripleFormat: false);
		stationEngine.SendAnalyticSelectContent("Towers", text);
		stationEngine.SendAnalyticSpendVirtualCurrency("Towers", "[GOLD]", text);
		UpdateExclamationUnits();
	}

	public void ButtonPressUpgradeCatapult()
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)PlayerPrefsController.upgradeCatapultPrices[PlayerPrefsController.CatapultLvl]);
		PlayerPrefsController.CatapultLvl++;
		upgradesController.SetInitialStructure(_setHeroes: false);
		UpdateUIUpgrade();
		UpdateLevelBars(catapultsLevel, PlayerPrefsController.CatapultLvl, ConfigPrefsController.upgradeCatapultMax);
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
		string text = FormatString(PlayerPrefsController.upgradeCatapultPrices[PlayerPrefsController.CatapultLvl], tripleFormat: false);
		stationEngine.SendAnalyticSelectContent("Catapults", text);
		stationEngine.SendAnalyticSpendVirtualCurrency("Catapults", "[GOLD]", text);
		UpdateExclamationUnits();
	}

	public void ButtonPressUpgradeCatapultAmmo(int _catapultIndex)
	{
		if (!Touch_Battle.IsWindowBigOpen)
		{
			DestroySmallMenues();
			sfxUIController.PlaySound(SfxUI.ClickDefault);
			mainController.HideOffer();
			Touch_Battle.IsWindowSmallOpen = true;
			subCanvasUpgradeGeneral.SetActive(value: false);
			subCanvasUpgradeUnits.SetActive(value: false);
			subCanvasUpgradeTurn.SetActive(value: false);
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/CanvasCatapult")) as GameObject;
			UICatapultMenu component = gameObject.GetComponent<UICatapultMenu>();
			component.Initialize(_catapultIndex, this, sfxUIController, upgradesController);
		}
	}

	public void ButtonPressUpgradeBallistaAmmo(int _towerIndex)
	{
		if (Touch_Battle.IsWindowBigOpen)
		{
			return;
		}
		DestroySmallMenues();
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		mainController.HideOffer();
		if (tutorialController.GetActualIndex() != 19)
		{
			Touch_Battle.IsWindowSmallOpen = true;
			subCanvasUpgradeGeneral.SetActive(value: false);
			subCanvasUpgradeUnits.SetActive(value: false);
			subCanvasUpgradeTurn.SetActive(value: false);
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/CanvasBallista")) as GameObject;
			UIBallistaMenu component = gameObject.GetComponent<UIBallistaMenu>();
			component.Initialize(_towerIndex, this, sfxUIController, upgradesController);
			if (tutorialController.GetActualIndex() == 16)
			{
				tutorialController.ActivateStep(17);
			}
		}
	}

	public void ButtonPressUpgradeHeroe(int _heroIndex)
	{
		if (!Touch_Battle.IsWindowBigOpen)
		{
			DestroySmallMenues();
			sfxUIController.PlaySound(SfxUI.ClickDefault);
			mainController.HideOffer();
			Touch_Battle.IsWindowSmallOpen = true;
			subCanvasUpgradeGeneral.SetActive(value: false);
			subCanvasUpgradeUnits.SetActive(value: false);
			subCanvasUpgradeTurn.SetActive(value: false);
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/CanvasHeroes")) as GameObject;
			UIHeroesMenu component = gameObject.GetComponent<UIHeroesMenu>();
			component.Initialize(stationEngine, _heroIndex, this, sfxUIController, upgradesController);
		}
	}

	public void ButtonPressUpgradeBank()
	{
		if (Touch_Battle.IsWindowBigOpen)
		{
			return;
		}
		if (bankController.IsReady() && bankController.GetRewardAccumulated() > 0)
		{
			if (stationEngine.CheckVideoReward() && (float)PlayerPrefs.GetInt("bankCheckPendingReward") >= (float)bankController.GetMaxReward(PlayerPrefsController.BankLvl) * ConfigPrefsController.bankExtraQualifier)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Canvas/CanvasExtraBank")) as GameObject;
				BankVideo component = gameObject.GetComponent<BankVideo>();
				component.Initialize(stationEngine, bankController, sfxUIController, uiBankIcon);
			}
			else
			{
				sfxUIController.PlaySound(SfxUI.ClickBuy);
				uiBankIcon.AnimationPayReward();
				bankController.PayReward(giveExtra: false);
			}
			return;
		}
		DestroySmallMenues();
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		mainController.HideOffer();
		if (bankController.IsReady())
		{
			if (MainController.worldScreen == WorldScreen.Upgrade)
			{
				subCanvasUpgradeGeneral.SetActive(value: false);
				subCanvasUpgradeUnits.SetActive(value: false);
				subCanvasUpgradeTurn.SetActive(value: false);
			}
			if (PlayerPrefs.GetInt("bankTutorialDone") == 0 && PlayerPrefs.GetInt("bankLvl") <= 0)
			{
				tutorialController.ClearGlow();
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("UI/CanvasBank")) as GameObject;
			UIBankMenu component2 = gameObject2.GetComponent<UIBankMenu>();
			component2.Initialize(stationEngine, bankController, this, sfxUIController, upgradesController, tutorialController);
		}
		else
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate(Resources.Load("Windows/CanvasBankConnection")) as GameObject;
			BankConnectionUI component3 = gameObject3.GetComponent<BankConnectionUI>();
			component3.Initialize(bankController, sfxUIController, this);
			bankController.ForceStatus();
		}
	}

	public void BackFromUpgradeWindow()
	{
		mainController.UnhideOffer();
		subCanvasUpgradeGeneral.SetActive(value: true);
		subCanvasUpgradeUnits.SetActive(value: true);
		subCanvasUpgradeTurn.SetActive(value: true);
		canvasUpgrade.SetActive(value: true);
		if (PlayerPrefsController.tutorialSteps[27])
		{
			transformOptions.gameObject.SetActive(value: true);
		}
		Touch_Battle.IsWindowBigOpen = false;
		Touch_Battle.IsWindowSmallOpen = false;
		upgradesController.SetHeroes();
		UpdateExclamationGeneral();
		UpdateExclamationUnits();
		if (tutorialController.GetActualIndex() == 10)
		{
			tutorialController.ActivateStep(10);
		}
		else if (tutorialController.GetActualIndex() == 14)
		{
			tutorialController.ActivateStep(15);
		}
		else if (tutorialController.GetActualIndex() == 17)
		{
			tutorialController.ActivateStep(18);
		}
		else if (PlayerPrefsController.tutorialSteps[27])
		{
			UpdateUIUpgrade();
		}
		Resources.UnloadUnusedAssets();
	}

	public void ButtonPressUpgradeBack()
	{
		sfxUIController.PlaySound(SfxUI.ClickClose);
		BackFromUpgradeWindow();
	}

	public void ButtonPressUpgradeGeneral()
	{
		if (!Touch_Battle.IsWindowBigOpen && !Touch_Battle.IsWindowSmallOpen)
		{
			Touch_Battle.IsWindowBigOpen = true;
			uiIncomeColonies.ResetHackControl();
			mainController.HideOffer();
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/CanvasGeneralTree")) as GameObject;
			UIGeneralTechnology component = gameObject.GetComponent<UIGeneralTechnology>();
			component.Initialize(stationEngine, mainController, this, sfxUIController, tutorialController, uiIncomeColonies);
			if (PlayerPrefsController.tutorialSteps[27])
			{
				transformOptions.gameObject.SetActive(value: false);
			}
			if (tutorialController.GetActualIndex() == 12)
			{
				tutorialController.ActivateStep(13);
			}
		}
	}

	public void ButtonPressUpgradeUnits()
	{
		if (!Touch_Battle.IsWindowBigOpen && !Touch_Battle.IsWindowSmallOpen)
		{
			Touch_Battle.IsWindowBigOpen = true;
			uiIncomeColonies.ResetHackControl();
			mainController.HideOffer();
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/CanvasUnitsTree")) as GameObject;
			UIUnitsTechnology component = gameObject.GetComponent<UIUnitsTechnology>();
			component.Initialize(stationEngine, this, achievementsController, tutorialController, sfxUIController);
			if (PlayerPrefsController.tutorialSteps[27])
			{
				transformOptions.gameObject.SetActive(value: false);
			}
			if (tutorialController.GetActualIndex() == 7)
			{
				tutorialController.ActivateStep(8);
			}
		}
	}

	public void ButtonPressNextWave()
	{
		if (timeToEnableButtons <= 0f)
		{
			uiIncomeColonies.ResetHackControl();
			mainController.HideOffer();
			if (PlayerPrefsController.CatapultAmmo[0] == CatapultAmmoType.Big)
			{
				buttonCatapultImageChange[0].sprite = spriteCatapultAmmoButton[0];
			}
			else
			{
				buttonCatapultImageChange[0].sprite = spriteCatapultAmmoButton[1];
			}
			if (PlayerPrefsController.CatapultAmmo[1] == CatapultAmmoType.Big)
			{
				buttonCatapultImageChange[1].sprite = spriteCatapultAmmoButton[0];
			}
			else
			{
				buttonCatapultImageChange[1].sprite = spriteCatapultAmmoButton[1];
			}
			if (tutorialController.GetActualIndex() < 20)
			{
				tutorialController.ClearGlow();
			}
			if (tutorialController.GetActualIndex() == 0)
			{
				tutorialController.ActivateStep(2);
			}
			else if (tutorialController.GetActualIndex() == 6)
			{
				tutorialController.SetGlow(11);
			}
			mainController.StartWave(_isDefence: true);
			UpdateSpeedButton();
			mainController.ShowWaveOffer();
		}
	}

	public void ButtonPressStartInvasion()
	{
		upgradesController.OrderGeneral();
		mainController.StartWave(_isDefence: false);
		UpdateSpeedButton();
	}

	public void ButtonPressMap()
	{
		uiIncomeColonies.ResetHackControl();
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		mainController.HideOffer();
		mainController.LoadMap();
	}

	public void UpdateUIUpgrade()
	{
		upgradeTextLvl[0].text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + (PlayerPrefsController.WallLvl + 1).ToString("###,###,##0");
		upgradeTextLvl[1].text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.ArchersLvl.ToString("###,###,##0");
		upgradeTextLvl[2].text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.TowerLvl.ToString("###,###,##0");
		upgradeTextLvl[3].text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.CatapultLvl.ToString("###,###,##0");
		CheckBoosts();
		rubyAmountText.text = PlayerPrefs.GetInt("playerRubies").ToString();
		if (PlayerPrefsController.WallLvl < ConfigPrefsController.upgradeWallMax - 1 && PlayerPrefs.GetFloat("playerMoney") >= (float)PlayerPrefsController.upgradeWallPrices[PlayerPrefsController.WallLvl + 1])
		{
			upgradeButtons[0].interactable = true;
		}
		else
		{
			upgradeButtons[0].interactable = false;
		}
		if (PlayerPrefsController.WallLvl < ConfigPrefsController.upgradeWallMax - 1)
		{
			upgradeTextPrice[0].text = PlayerPrefsController.upgradeWallPrices[PlayerPrefsController.WallLvl + 1].ToString("###,###,##0");
		}
		else
		{
			upgradeTextPrice[0].text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
		}
		if (PlayerPrefsController.ArchersLvl < ConfigPrefsController.upgradeArchersMax && PlayerPrefs.GetFloat("playerMoney") >= (float)PlayerPrefsController.upgradeArcherPrices[PlayerPrefsController.ArchersLvl])
		{
			if (tutorialController != null)
			{
				if (tutorialController.GetActualIndex() == 15 && PlayerPrefsController.WallLvl <= 4)
				{
					upgradeButtons[1].interactable = false;
				}
				else
				{
					upgradeButtons[1].interactable = true;
				}
			}
			else
			{
				upgradeButtons[1].interactable = true;
			}
		}
		else
		{
			upgradeButtons[1].interactable = false;
		}
		if (PlayerPrefsController.ArchersLvl < ConfigPrefsController.upgradeArchersMax)
		{
			upgradeTextPrice[1].text = PlayerPrefsController.upgradeArcherPrices[PlayerPrefsController.ArchersLvl].ToString("###,###,##0");
		}
		else
		{
			upgradeTextPrice[1].text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
		}
		if (PlayerPrefsController.WallLvl >= 3)
		{
			if (!subCanvasTowers.activeInHierarchy)
			{
				subCanvasTowers.SetActive(value: true);
			}
			if (PlayerPrefsController.TowerLvl < ConfigPrefsController.upgradeTowerMax && upgradesController.TowersController[0] != null && PlayerPrefs.GetFloat("playerMoney") >= (float)PlayerPrefsController.upgradeTowerPrices[PlayerPrefsController.TowerLvl])
			{
				upgradeButtons[2].interactable = true;
			}
			else
			{
				upgradeButtons[2].interactable = false;
			}
			if (PlayerPrefsController.TowerLvl < ConfigPrefsController.upgradeTowerMax)
			{
				upgradeTextPrice[2].text = PlayerPrefsController.upgradeTowerPrices[PlayerPrefsController.TowerLvl].ToString("###,###,##0");
			}
			else
			{
				upgradeTextPrice[2].text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
			}
		}
		else
		{
			subCanvasTowers.SetActive(value: false);
		}
		if (PlayerPrefsController.WallLvl >= 21)
		{
			if (!subCanvasCatapults.activeInHierarchy)
			{
				subCanvasCatapults.SetActive(value: true);
			}
			if (PlayerPrefsController.CatapultLvl < ConfigPrefsController.upgradeCatapultMax && upgradesController.CatapultsController[0] != null && PlayerPrefs.GetFloat("playerMoney") >= (float)PlayerPrefsController.upgradeCatapultPrices[PlayerPrefsController.CatapultLvl])
			{
				upgradeButtons[3].interactable = true;
			}
			else
			{
				upgradeButtons[3].interactable = false;
			}
			if (PlayerPrefsController.CatapultLvl < ConfigPrefsController.upgradeCatapultMax)
			{
				upgradeTextPrice[3].text = PlayerPrefsController.upgradeCatapultPrices[PlayerPrefsController.CatapultLvl].ToString("###,###,##0");
			}
			else
			{
				upgradeTextPrice[3].text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
			}
		}
		else
		{
			subCanvasCatapults.SetActive(value: false);
		}
		canvasUpgrade.SetActive(value: true);
		for (int i = 0; i < upgradesController.CatapultsController.Length; i++)
		{
			if (upgradesController.CatapultsController[i] != null)
			{
				upgradeCatapult[i].SetActive(value: true);
			}
			else
			{
				upgradeCatapult[i].SetActive(value: false);
			}
		}
		canvasUpgradeCatapult.SetActive(value: true);
		for (int j = 0; j < upgradesController.TowersController.Length; j++)
		{
			if (upgradesController.TowersController[j] != null)
			{
				upgradeTower[j].SetActive(value: true);
			}
			else
			{
				upgradeTower[j].SetActive(value: false);
			}
		}
		canvasUpgradeTower.SetActive(value: true);
		int num = 0;
		for (int k = 0; k < PlayerPrefsController.HeroeLvl.Length; k++)
		{
			if (PlayerPrefsController.HeroeLvl[k] > 0)
			{
				num++;
			}
		}
		if (num > 0)
		{
			buttonUpgradeHeroe[0].gameObject.SetActive(value: true);
		}
		else
		{
			buttonUpgradeHeroe[0].gameObject.SetActive(value: false);
		}
		if (num > 1)
		{
			buttonUpgradeHeroe[1].gameObject.SetActive(value: true);
		}
		else
		{
			buttonUpgradeHeroe[1].gameObject.SetActive(value: false);
		}
		canvasUpgradeHeroes.SetActive(value: true);
	}

	public void ShowBoosterReload()
	{
		if (PlayerPrefs.GetInt("hasBoughtBoosters") != 1)
		{
			return;
		}
		GameObject gameObject = null;
		bool flag = false;
		switch (UnityEngine.Random.Range(0, 3))
		{
		case 0:
			if (PlayerPrefs.GetInt("hasBoughtXpBoost") == 1 && PlayerPrefs.GetInt("xpBoost") <= 0 && !flag)
			{
				gameObject = (UnityEngine.Object.Instantiate(Resources.Load("Windows/INAPPS/CanvasXPReload")) as GameObject);
				flag = true;
				ItemReloadUIController component2 = gameObject.GetComponent<ItemReloadUIController>();
				component2.Initialize(sfxUIController, this);
			}
			break;
		case 1:
			if (PlayerPrefs.GetInt("hasBoughtMoneyBoost") == 1 && PlayerPrefs.GetInt("moneyBoost") <= 0 && !flag)
			{
				gameObject = (UnityEngine.Object.Instantiate(Resources.Load("Windows/INAPPS/CanvasCoinsReload")) as GameObject);
				flag = true;
				ItemReloadUIController component3 = gameObject.GetComponent<ItemReloadUIController>();
				component3.Initialize(sfxUIController, this);
			}
			break;
		case 2:
			if (PlayerPrefs.GetInt("hasBoughtPowersBoost") == 1 && PlayerPrefs.GetInt("powerBoost") <= 0 && !flag)
			{
				gameObject = (UnityEngine.Object.Instantiate(Resources.Load("Windows/INAPPS/CanvasPowerReload")) as GameObject);
				flag = true;
				ItemReloadUIController component4 = gameObject.GetComponent<ItemReloadUIController>();
				component4.Initialize(sfxUIController, this);
			}
			break;
		case 3:
			if (PlayerPrefs.GetInt("hasBoughtVipBoost") == 1 && PlayerPrefs.GetInt("moneyBoost") <= 0 && PlayerPrefs.GetInt("powerBoost") <= 0 && PlayerPrefs.GetInt("xpBoost") <= 0 && !flag)
			{
				gameObject = (UnityEngine.Object.Instantiate(Resources.Load("Windows/INAPPS/CanvasVIPReload")) as GameObject);
				flag = true;
				ItemReloadUIController component = gameObject.GetComponent<ItemReloadUIController>();
				component.Initialize(sfxUIController, this);
			}
			break;
		}
	}

	private void DestroySmallMenues()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas_Ballista");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		gameObject = GameObject.FindGameObjectWithTag("Canvas_Catapult");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		gameObject = GameObject.FindGameObjectWithTag("Canvas_Hero");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		gameObject = GameObject.FindGameObjectWithTag("Canvas_Bank");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		gameObject = GameObject.FindGameObjectWithTag("Canvas_NoRubies");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		gameObject = GameObject.FindGameObjectWithTag("Canvas_BankConnection");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	private void UpdateUIDefence()
	{
		for (int i = 0; i < upgradesController.CatapultsController.Length; i++)
		{
			if (upgradesController.CatapultsController[i] != null)
			{
				buttonDefenceCatapult[i].gameObject.SetActive(value: true);
			}
			else
			{
				buttonDefenceCatapult[i].gameObject.SetActive(value: false);
			}
		}
		canvasDefence.SetActive(value: true);
		if (upgradesController.TowersController[0] != null)
		{
			buttonDefenceTower.gameObject.SetActive(value: true);
		}
		else
		{
			buttonDefenceTower.gameObject.SetActive(value: false);
		}
		buttonDefenceUnits[0].gameObject.SetActive(value: true);
		buttonDefenceUnits[1].gameObject.SetActive(value: false);
		buttonDefenceUnits[2].gameObject.SetActive(value: false);
		buttonDefenceUnits[3].gameObject.SetActive(value: false);
		UpdateSpawnButtons(isOn: true);
		for (int j = 0; j < PlayerPrefsController.UnitsTechMelee.Length; j++)
		{
			if (PlayerPrefsController.UnitsTechMelee[j] < 3 || j == PlayerPrefsController.UnitsTechMelee.Length - 1)
			{
				buttonDefenceUnitsImage[0].GetComponent<Image>().sprite = spriteCardsMelee[j];
				break;
			}
		}
		for (int k = 0; k < PlayerPrefsController.UnitsTechSpear.Length; k++)
		{
			if (PlayerPrefsController.UnitsTechSpear[k] < 3 || k == PlayerPrefsController.UnitsTechSpear.Length - 1)
			{
				buttonDefenceUnitsImage[1].GetComponent<Image>().sprite = spriteCardsSpear[k];
				break;
			}
		}
		for (int l = 0; l < PlayerPrefsController.UnitsTechRanged.Length; l++)
		{
			if (PlayerPrefsController.UnitsTechRanged[l] < 3 || l == PlayerPrefsController.UnitsTechRanged.Length - 1)
			{
				buttonDefenceUnitsImage[2].GetComponent<Image>().sprite = spriteCardsRanged[l];
				break;
			}
		}
		for (int m = 0; m < PlayerPrefsController.UnitsTechMounted.Length; m++)
		{
			if (PlayerPrefsController.UnitsTechMounted[m] < 3 || m == PlayerPrefsController.UnitsTechMounted.Length - 1)
			{
				buttonDefenceUnitsImage[3].GetComponent<Image>().sprite = spriteCardsMounted[m];
				break;
			}
		}
		buttonDefencePowers[0].gameObject.SetActive(value: false);
		buttonDefencePowers[1].gameObject.SetActive(value: false);
		buttonDefencePowers[2].gameObject.SetActive(value: false);
		if (PlayerPrefsController.GeneralTechPowers_WarCry > 0)
		{
			buttonDefencePowers[0].gameObject.SetActive(value: true);
		}
		if (PlayerPrefsController.GeneralTechPowers_CatapultsShower > 0)
		{
			buttonDefencePowers[1].gameObject.SetActive(value: true);
		}
		if (PlayerPrefsController.GeneralTechPowers_ArchersShower > 0)
		{
			buttonDefencePowers[2].gameObject.SetActive(value: true);
		}
		for (int n = 0; n < usedBoostedPower.Length; n++)
		{
			usedBoostedPower[n] = false;
			if (PlayerPrefs.GetInt("powerBoost") == 0)
			{
				UpdatePowersBoosted(used: true, n);
			}
			if (PlayerPrefs.GetInt("powerBoost") > 0 && !usedBoostedPower[n])
			{
				UpdatePowersBoosted(used: false, n);
			}
		}
	}

	private void UpdateUIAttack()
	{
		canvasAttack.SetActive(value: true);
		buttonAttackPowers[0].gameObject.SetActive(value: false);
		buttonAttackPowers[1].gameObject.SetActive(value: false);
		buttonAttackPowers[2].gameObject.SetActive(value: false);
		if (PlayerPrefsController.GeneralTechPowers_WarCry > 0)
		{
			buttonAttackPowers[0].gameObject.SetActive(value: true);
		}
		if (PlayerPrefsController.GeneralTechPowers_CatapultsShower > 0)
		{
			buttonAttackPowers[1].gameObject.SetActive(value: true);
		}
		if (PlayerPrefsController.GeneralTechPowers_ArchersShower > 0)
		{
			buttonAttackPowers[2].gameObject.SetActive(value: true);
		}
	}

	private void UpdateUIInvasion()
	{
		canvasInvasion.SetActive(value: true);
	}

	private void UpdateExclamationGeneral()
	{
		upgradeHeroEffect.localScale = new Vector3(1f, 1f, 1f);
		if (PlayerPrefs.GetInt("playerExpPoints") > 0)
		{
			upgradeHeroEffect.gameObject.SetActive(value: true);
		}
		else
		{
			upgradeHeroEffect.gameObject.SetActive(value: false);
		}
	}

	public void UpdateExclamationUnits()
	{
		bool flag = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		for (int i = 0; i < PlayerPrefsController.UnitsTechMelee.Length; i++)
		{
			if (PlayerPrefsController.UnitsTechMelee[i] >= 3)
			{
				num++;
			}
		}
		for (int j = 0; j < PlayerPrefsController.UnitsTechSpear.Length; j++)
		{
			if (PlayerPrefsController.UnitsTechSpear[j] >= 3)
			{
				num2++;
			}
		}
		for (int k = 0; k < PlayerPrefsController.UnitsTechRanged.Length; k++)
		{
			if (PlayerPrefsController.UnitsTechRanged[k] >= 3)
			{
				num3++;
			}
		}
		for (int l = 0; l < PlayerPrefsController.UnitsTechMounted.Length; l++)
		{
			if (PlayerPrefsController.UnitsTechMounted[l] >= 3)
			{
				num4++;
			}
		}
		for (int m = 0; m < PlayerPrefsController.UnitsTechSiege.Length; m++)
		{
			if (PlayerPrefsController.UnitsTechSiege[m] >= 3)
			{
				num5++;
			}
		}
		int num6 = 99999999;
		int num7 = 99999999;
		int num8 = 99999999;
		int num9 = 99999999;
		int num10 = 99999999;
		if (num < PlayerPrefsController.UnitsTechMelee.Length - 1)
		{
			num6 = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.None, num);
		}
		if (num2 < PlayerPrefsController.UnitsTechSpear.Length - 1)
		{
			num8 = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.Spear, num2);
		}
		if (num4 < PlayerPrefsController.UnitsTechMounted.Length - 1)
		{
			num9 = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.Mounted, num4);
		}
		if (num3 < PlayerPrefsController.UnitsTechRanged.Length - 1)
		{
			num7 = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.Javaline, num3);
		}
		if (num5 < PlayerPrefsController.UnitsTechSiege.Length - 1)
		{
			num10 = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Siege, CohortSubType.None, num5);
		}
		if (PlayerPrefs.GetFloat("playerMoney") >= (float)num6 || PlayerPrefs.GetFloat("playerMoney") >= (float)num8 || PlayerPrefs.GetFloat("playerMoney") >= (float)num7 || PlayerPrefs.GetFloat("playerMoney") >= (float)num9 || PlayerPrefs.GetFloat("playerMoney") >= (float)num10)
		{
			flag = true;
		}
		upgradeUnitsEffect.localScale = new Vector3(1f, 1f, 1f);
		if (flag)
		{
			upgradeUnitsEffect.gameObject.SetActive(value: true);
		}
		else
		{
			upgradeUnitsEffect.gameObject.SetActive(value: false);
		}
	}

	private void ShowSpeedLabel()
	{
		if (!tutorialSpeedLabelShown)
		{
			tutorialSpeedLabelShown = true;
			tutorialSpeedLabel = (UnityEngine.Object.Instantiate(Resources.Load("Canvas/Tutorial/CanvasTutorial_SpeedButton")) as GameObject);
		}
	}

	private void ShowScrollHand()
	{
		if (!tutorialScrollLabelShown)
		{
			tutorialScrollLabelShown = true;
			tutorialScrollLabel = (UnityEngine.Object.Instantiate(Resources.Load("Canvas/Tutorial/CanvasTutorial_Scroll")) as GameObject);
		}
	}

	public void UpdateLevelBars(Image fillImage, float upgradable, float upgradableMax)
	{
		fillImage.fillAmount = upgradable / upgradableMax;
	}

	public void UpdateHeroIcon()
	{
		for (int i = 0; i < imageHeroIcon.Length; i++)
		{
			if (PlayerPrefsController.HeroeSelected[i] != 0)
			{
				imageHeroIcon[i].sprite = heroIcon[(int)(PlayerPrefsController.HeroeSelected[i] - 1)];
			}
		}
	}

	private void UpdateAllBars()
	{
		UpdateLevelBars(wallLevel, PlayerPrefsController.WallLvl, ConfigPrefsController.upgradeWallMax);
		UpdateLevelBars(archersLevel, PlayerPrefsController.ArchersLvl, ConfigPrefsController.upgradeArchersMax);
		UpdateLevelBars(towersLevel, PlayerPrefsController.TowerLvl, ConfigPrefsController.upgradeTowerMax);
		UpdateLevelBars(catapultsLevel, PlayerPrefsController.CatapultLvl, ConfigPrefsController.upgradeCatapultMax);
	}

	private void UpdateCatapultImage(int catapultIndex, bool on)
	{
		if (PlayerPrefsController.CatapultAmmo[catapultIndex] == CatapultAmmoType.Big)
		{
			if (on)
			{
				buttonCatapultImageChange[catapultIndex].sprite = spriteCatapultAmmoButton[0];
			}
			else
			{
				buttonCatapultImageChange[catapultIndex].sprite = buttonCatapultOnOff[0];
			}
		}
		else if (on)
		{
			buttonCatapultImageChange[catapultIndex].sprite = spriteCatapultAmmoButton[1];
		}
		else
		{
			buttonCatapultImageChange[catapultIndex].sprite = buttonCatapultOnOff[1];
		}
	}

	private void CheckBoosts()
	{
		if (PlayerPrefs.GetInt("moneyBoost") > 0)
		{
			moneyBoosted.gameObject.SetActive(value: true);
			boosted[0].SetActive(value: true);
		}
		else if (PlayerPrefs.GetInt("moneyBoost") == 0)
		{
			moneyBoosted.gameObject.SetActive(value: false);
			boosted[0].SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("xpBoost") > 0)
		{
			xpBoosted.gameObject.SetActive(value: true);
			boosted[1].SetActive(value: true);
		}
		else if (PlayerPrefs.GetInt("xpBoost") == 0)
		{
			xpBoosted.gameObject.SetActive(value: false);
			boosted[1].SetActive(value: false);
		}
		UpdateBoostsText();
	}

	public void UpdateBoostsText()
	{
		if (MainController.worldScreen == WorldScreen.Defence)
		{
			textXpBoosted.text = PlayerPrefs.GetInt("xpBoost").ToString() + " " + ScriptLocalization.Get("NORMAL/waves").ToUpper();
			textMoneyBoosted.text = PlayerPrefs.GetInt("moneyBoost").ToString() + " " + ScriptLocalization.Get("NORMAL/waves").ToUpper();
		}
		if (MainController.worldScreen == WorldScreen.Upgrade || MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			textXpBoosted.text = PlayerPrefs.GetInt("xpBoost").ToString() + " " + ScriptLocalization.Get("NORMAL/waves").ToUpper();
			textMoneyBoosted.text = PlayerPrefs.GetInt("moneyBoost").ToString() + " " + ScriptLocalization.Get("NORMAL/waves").ToUpper();
		}
	}

	private void UpdatePowersBoosted(bool used, int index)
	{
		if (!used)
		{
			mainController.TimeReloadPowerCounter[index] = mainController.TimeReloadPower[index];
			buttonDefencePowersImage[index].sprite = spriteButtonDefencePowersBoosted;
			textPowersBoostedWavesLeft[index].text = PlayerPrefs.GetInt("powerBoost").ToString();
			powersBoosted[index].SetActive(value: true);
		}
		if (used)
		{
			mainController.TimeReloadPowerCounter[index] = 0f;
			buttonDefencePowersImage[index].sprite = spriteButtonDefencePowersNormal;
			powersBoosted[index].SetActive(value: false);
			usedBoostedPower[index] = true;
		}
	}

	private void SetInitialBankStatus()
	{
		citiesTotalConquered = 0;
		for (int i = 1; i < PlayerPrefsController.CitiesConquered.Length; i++)
		{
			if (PlayerPrefsController.CitiesConquered[i])
			{
				citiesTotalConquered++;
			}
			if (citiesTotalConquered >= ConfigPrefsController.bankColoniesRequiredToUnlock)
			{
				break;
			}
		}
		if (citiesTotalConquered >= ConfigPrefsController.bankColoniesRequiredToUnlock && (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade))
		{
			canvasUpgradeBank.SetActive(value: true);
			uiBankIcon.UpdateIconStatus();
		}
		else
		{
			canvasUpgradeBank.SetActive(value: false);
		}
		if (MainController.worldScreen == WorldScreen.Upgrade)
		{
			if (PlayerPrefsController.BankLvl < 0 && PlayerPrefs.GetInt("bankTutorialDone") == 0 && bankController.IsReady())
			{
				BankTutorial(citiesTotalConquered);
			}
			if (PlayerPrefsController.BankLvl >= 0)
			{
				PlayerPrefs.SetInt("bankTutorialDone", 1);
				PlayerPrefs.Save();
			}
		}
	}

	public void BankTutorial(int citiesConquered)
	{
		if (PlayerPrefsController.PlayBankTutorial(citiesConquered))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/BankTutorial/CanvasBankUnlocked")) as GameObject;
			UIBankUnlocked component = gameObject.GetComponent<UIBankUnlocked>();
			component.Initialize(mainController, this, sfxUIController, tutorialController);
			transformOptions.gameObject.SetActive(value: false);
			canvasUpgrade.SetActive(value: false);
			canvasUpgradeTower.SetActive(value: false);
		}
	}

	public void HalfWave()
	{
		if (playerWaveImage.fillAmount <= 0.5f)
		{
			PlayerPrefsController.halfWave = true;
		}
		else
		{
			PlayerPrefsController.halfWave = false;
		}
	}

	public void HittingGate()
	{
		if (gateLifeImage.fillAmount <= 0.5f)
		{
			PlayerPrefsController.halfLife = true;
		}
		else
		{
			PlayerPrefsController.halfLife = false;
		}
	}

	public void SpawnGems(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/CanvasGemReward"), new Vector3(40f, 1f, UnityEngine.Random.Range(-3, 3)), Quaternion.identity) as GameObject;
			GemRewardDropAnimation component = gameObject.GetComponent<GemRewardDropAnimation>();
			component.Initialize(i);
		}
	}

	private void UpdateUnitSpawnButtons(bool isOn)
	{
		if (PlayerPrefsController.UnitsTechMelee[0] >= 3)
		{
			buttonDefenceUnits[1].interactable = isOn;
		}
		if (PlayerPrefsController.UnitsTechSpear[1] >= 3)
		{
			buttonDefenceUnits[2].interactable = isOn;
		}
		if (PlayerPrefsController.UnitsTechSpear[2] >= 3)
		{
			buttonDefenceUnits[3].interactable = isOn;
		}
	}

	private void UpdateSpawnButtons(bool isOn)
	{
		if (PlayerPrefsController.UnitsTechMelee[0] >= 3)
		{
			buttonDefenceUnits[1].gameObject.SetActive(isOn);
		}
		if (PlayerPrefsController.UnitsTechSpear[1] >= 3)
		{
			buttonDefenceUnits[2].gameObject.SetActive(isOn);
		}
		if (PlayerPrefsController.UnitsTechSpear[2] >= 3)
		{
			buttonDefenceUnits[3].gameObject.SetActive(isOn);
		}
	}

	public string FormatString(int value, bool tripleFormat)
	{
		string result = string.Empty;
		if (value < 10)
		{
			result = ((!tripleFormat) ? ("[00" + value + "]") : ("[000" + value + "]"));
		}
		if (value < 100 && value >= 10)
		{
			result = ((!tripleFormat) ? ("[0" + value + "]") : ("[00" + value + "]"));
		}
		if (value < 1000 && value >= 100)
		{
			result = ((!tripleFormat) ? ("[" + value + "]") : ("[0" + value + "]"));
		}
		if (value >= 1000)
		{
			result = "[" + value + "]";
		}
		return result;
	}
}
