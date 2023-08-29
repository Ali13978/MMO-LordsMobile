using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
	public static WorldScreen worldScreen;

	public static bool ExpectingFinishGame;

	private float gateLife;

	private float gateLifeActual;

	private float[] timeReloadPower = new float[3];

	private float[] timeReloadPowerCounter = new float[3];

	private int playerLevelStart;

	private int playerLevelFinish;

	private float timer;

	private float timeToChange = 4.1f;

	private bool isPlayingAnimation;

	private int gemsToGive;

	private UpgradesController upgradesController;

	private UIController uiController;

	private WaveController waveController;

	private UIWaveController uiWaveController;

	private TutorialController tutorialController;

	private SfxUIController sfxUIController;

	private MusicController musicController;

	private LightingController lightingController;

	private BankController bankController;

	private StationEngine stationEngine;

	private AchievementsController achievementsController;

	private GameObject offerObject;

	private GameObject rateObject;

	public float GateLife
	{
		get
		{
			return gateLife;
		}
		set
		{
			gateLife = value;
		}
	}

	public float GateLifeActual
	{
		get
		{
			return gateLifeActual;
		}
		set
		{
			gateLifeActual = value;
		}
	}

	public float[] TimeReloadPower
	{
		get
		{
			return timeReloadPower;
		}
		set
		{
			timeReloadPower = value;
		}
	}

	public float[] TimeReloadPowerCounter
	{
		get
		{
			return timeReloadPowerCounter;
		}
		set
		{
			timeReloadPowerCounter = value;
		}
	}

	private void Awake()
	{
		Application.targetFrameRate = 30;
		Screen.sleepTimeout = -1;
		achievementsController = base.gameObject.GetComponent<AchievementsController>();
		upgradesController = base.gameObject.GetComponent<UpgradesController>();
		uiController = base.gameObject.GetComponent<UIController>();
		uiWaveController = base.gameObject.GetComponent<UIWaveController>();
		tutorialController = base.gameObject.GetComponent<TutorialController>();
		waveController = base.gameObject.GetComponent<WaveController>();
		sfxUIController = base.gameObject.GetComponent<SfxUIController>();
		musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
		lightingController = base.gameObject.GetComponent<LightingController>();
		bankController = base.gameObject.GetComponent<BankController>();
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		bool shouldReset = false;
		if (SceneManager.GetActiveScene().name == "MainScene" && (worldScreen == WorldScreen.Defence || worldScreen == WorldScreen.Upgrade) && PlayerPrefs.GetInt("tutorialStep_27", 0) == 0 && !PlayerPrefsController.forceTutorialDone)
		{
			if (!UISelectLanguage.ChangedLanguage)
			{
				shouldReset = true;
			}
			else
			{
				UISelectLanguage.ChangedLanguage = false;
			}
		}
		PlayerPrefsController.LoadInitial(shouldReset);
		upgradesController.SetInitialStructure(_setHeroes: true);
		uiController.SetInitialStructure();
		timeReloadPower[0] = ConfigPrefsController.generalPowerWarCryCooldown;
		timeReloadPower[1] = ConfigPrefsController.generalPowerBouldersCooldown;
		timeReloadPower[2] = ConfigPrefsController.generalPowerArrowsCooldown;
		ChangeScreen(worldScreen);
		lightingController.InitializeLighting();
		bankController.Initialize(stationEngine);
		Vector3 vector = new Vector3(36.35f, 0f, -0.8f);
		if (worldScreen == WorldScreen.Defence || worldScreen == WorldScreen.Upgrade)
		{
			Object.Instantiate(Resources.Load("Scenarios/Scenario_Humans"));
		}
		else
		{
			switch (EnemyPrefsController.TerrainSelected)
			{
			case CityTerrain.Orcs:
				Object.Instantiate(Resources.Load("Scenarios/Scenario_Orcs"));
				break;
			case CityTerrain.Goblins:
				Object.Instantiate(Resources.Load("Scenarios/Scenario_Goblins"));
				break;
			case CityTerrain.Wolves:
				Object.Instantiate(Resources.Load("Scenarios/Scenario_Wolves"));
				break;
			case CityTerrain.Skeletons:
				Object.Instantiate(Resources.Load("Scenarios/Scenario_Skeletons"));
				break;
			}
		}
		SetWallLife();
		sfxUIController.CheckScenarioSfx();
	}

	private void Start()
	{
		if (worldScreen == WorldScreen.Attack)
		{
			waveController.SetWaveRoman();
		}
		else if (worldScreen == WorldScreen.Upgrade)
		{
			uiWaveController.SetPhaseInitial();
		}
		Resources.UnloadUnusedAssets();
		achievementsController.CheckAchievementsWave();
		timer = 0f;
		isPlayingAnimation = false;
	}

	private void Update()
	{
		if (worldScreen == WorldScreen.Defence || worldScreen == WorldScreen.AttackStarted)
		{
			timeReloadPowerCounter[0] += Time.deltaTime * (ConfigPrefsController.generalPowerWarCryCooldownBase + ConfigPrefsController.generalPowerWarCryCooldownBase * ConfigPrefsController.generalPowerWarCryCooldownPerLevel * (float)PlayerPrefsController.GeneralTechPowers_WarCry);
			timeReloadPowerCounter[1] += Time.deltaTime * (ConfigPrefsController.generalPowerBouldersCooldownBase + ConfigPrefsController.generalPowerBouldersCooldownBase * ConfigPrefsController.generalPowerBouldersCooldownPerLevel * (float)PlayerPrefsController.GeneralTechPowers_CatapultsShower);
			timeReloadPowerCounter[2] += Time.deltaTime * (ConfigPrefsController.generalPowerArrowsCooldownBase + ConfigPrefsController.generalPowerArrowsCooldownBase * ConfigPrefsController.generalPowerArrowsCooldownPerLevel * (float)PlayerPrefsController.GeneralTechPowers_ArchersShower);
			for (int i = 0; i < timeReloadPower.Length; i++)
			{
				if (timeReloadPowerCounter[i] > timeReloadPower[i])
				{
					timeReloadPowerCounter[i] = timeReloadPower[i];
				}
			}
			if (worldScreen == WorldScreen.Defence)
			{
				upgradesController.TimeSpawn += Time.deltaTime * (ConfigPrefsController.spawnTimeIncrementBase + ConfigPrefsController.spawnTimeIncrementBase * ConfigPrefsController.generalArmySpawnCooldown * (float)PlayerPrefsController.GeneralTechArmy_SpawnCooldown);
			}
			else if (worldScreen == WorldScreen.AttackStarted)
			{
				upgradesController.EnemyTimeSpawn += Time.deltaTime;
			}
			if (upgradesController.TimeSpawn > 1f)
			{
				upgradesController.TimeSpawn = 1f;
			}
		}
		if (isPlayingAnimation)
		{
			timer += Time.unscaledDeltaTime;
		}
		if (timer >= timeToChange)
		{
			timer = 0f;
			isPlayingAnimation = false;
			ChangeScreen(WorldScreen.Upgrade);
			WinWaveTutorial();
		}
		if (worldScreen == WorldScreen.Defence && ExpectingFinishGame)
		{
			if (gateLifeActual <= 0f)
			{
				ExpectingFinishGame = false;
				uiController.LoseWave();
				isPlayingAnimation = true;
				Invoke("LoseWave", 0.5f);
			}
			else
			{
				if (waveController.IsWavePlaying || !(waveController.WaveTimeActual <= 0f))
				{
					return;
				}
				bool flag = false;
				for (int j = 0; j < upgradesController.CohortsEnemyArray.Count; j++)
				{
					if (upgradesController.CohortsEnemyArray[j].IsAnySoldierAlive())
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ExpectingFinishGame = false;
					uiController.buttonPauseObject.SetActive(value: false);
					uiController.WinWave();
					isPlayingAnimation = true;
					Invoke("WinWave", 0.5f);
				}
			}
		}
		else
		{
			if (worldScreen != WorldScreen.AttackStarted || !ExpectingFinishGame)
			{
				return;
			}
			if (gateLifeActual <= 0f)
			{
				ExpectingFinishGame = false;
				uiController.buttonPauseObject.SetActive(value: false);
				uiController.WinInvasion();
				Invoke("WinInvasion", 4f);
			}
			else
			{
				if (waveController.IsWavePlaying && waveController.WaveCohortFaction.Count != 0)
				{
					return;
				}
				bool flag2 = false;
				for (int k = 0; k < upgradesController.CohortsFriendArray.Count; k++)
				{
					if (upgradesController.CohortsFriendArray[k].IsAnySoldierAlive())
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					ExpectingFinishGame = false;
					uiController.LoseInvasion();
					Invoke("LoseInvasion", 4f);
				}
			}
		}
	}

	private void ChangeScreen(WorldScreen _screenToLoad)
	{
		worldScreen = _screenToLoad;
		switch (_screenToLoad)
		{
		case WorldScreen.Attack:
			musicController.ChangeTrack(2, _fadeOffChangingScreen: true);
			break;
		case WorldScreen.Defence:
			musicController.ChangeTrack(musicController.SetWaveMusic(), _fadeOffChangingScreen: true);
			break;
		case WorldScreen.Upgrade:
			musicController.ChangeTrack(0, _fadeOffChangingScreen: true);
			break;
		}
		uiController.ChangeScreen(_screenToLoad);
		Resources.UnloadUnusedAssets();
	}

	public void LoadMap()
	{
		DestroyOffer();
		musicController.ChangeTrack(1, _fadeOffChangingScreen: true);
		Resources.UnloadUnusedAssets();
		SceneManager.LoadScene("MapScene");
	}

	public void CancelWave()
	{
		ExpectingFinishGame = false;
		ConfigPrefsController.adsVideoCounter -= ConfigPrefsController.adsVideoCounterLoseWave;
		ConfigPrefsController.adsInterstitialCounter -= ConfigPrefsController.adsInterstitialCounterLoseWave;
		ConfigPrefsController.adsVideoDamageCounter--;
		uiController.LoseWave();
		Invoke("LoseWave", 0.5f);
		isPlayingAnimation = true;
	}

	public void StartWave(bool _isDefence)
	{
		DestroyOffer();
		playerLevelStart = PlayerPrefsController.playerLevel;
		PlayerPrefsController.halfWave = false;
		timeReloadPowerCounter[0] = 0f;
		timeReloadPowerCounter[1] = 0f;
		timeReloadPowerCounter[2] = 0f;
		upgradesController.TowerTimeReloadCounter = upgradesController.TowerTimeReload * 0.98f;
		for (int i = 0; i < upgradesController.CatapultsController.Length; i++)
		{
			if (upgradesController.CatapultsController[i] != null)
			{
				upgradesController.CatapultsController[i].TimeReloadCounter = upgradesController.CatapultsController[i].TimeReload * 0.98f;
			}
		}
		if (_isDefence)
		{
			waveController.StartWave();
		}
		else
		{
			waveController.StartWaveRoman();
		}
		upgradesController.StartWave();
		ExpectingFinishGame = true;
		if (_isDefence)
		{
			ChangeScreen(WorldScreen.Defence);
		}
		else
		{
			ChangeScreen(WorldScreen.AttackStarted);
		}
		if (worldScreen == WorldScreen.Defence || worldScreen == WorldScreen.Upgrade)
		{
			if (PlayerPrefs.GetInt("playerWave") == ConfigPrefsController.waveArmyPerWave && !PlayerPrefsController.tutorialSpecialArmy)
			{
				PlayerPrefsController.tutorialSpecialArmy = true;
				PlayerPrefsController.SaveTutorial();
				Time.timeScale = 0f;
				Object.Instantiate(Resources.Load("Canvas/Tutorial/CanvasTutorial_FirstWaveArmy"));
			}
			else if (PlayerPrefs.GetInt("playerWave") == ConfigPrefsController.waveElephantPerWave && !PlayerPrefsController.tutorialSpecialElephants)
			{
				PlayerPrefsController.tutorialSpecialElephants = true;
				PlayerPrefsController.SaveTutorial();
				Time.timeScale = 0f;
				Object.Instantiate(Resources.Load("Canvas/Tutorial/CanvasTutorial_FirstWaveElephants"));
			}
		}
	}

	public void LoseWave()
	{
		if (PlayerPrefs.GetInt("playerWave") <= 200)
		{
			string value = uiController.FormatString(PlayerPrefs.GetInt("playerWave"), tripleFormat: true);
			stationEngine.SendAnalyticCustom("Wave_Lose", "Waves", value);
			stationEngine.SendAnalyticCustom("Wave_Play", "Waves", value);
		}
		if (PlayerPrefs.GetInt("playerWave") > 200 && PlayerPrefs.GetInt("playerWave") % 25 == 0)
		{
			string value2 = uiController.FormatString(PlayerPrefs.GetInt("playerWave"), tripleFormat: true);
			stationEngine.SendAnalyticCustom("Wave_Lose", "Waves", value2);
			stationEngine.SendAnalyticCustom("Wave_Play", "Waves", value2);
		}
		if (PlayerPrefs.GetInt("playerWave") >= 1000 && PlayerPrefs.GetInt("playerWave") % 50 == 0)
		{
			string value3 = uiController.FormatString(PlayerPrefs.GetInt("playerWave"), tripleFormat: true);
			stationEngine.SendAnalyticCustom("Wave_Lose", "Waves", value3);
			stationEngine.SendAnalyticCustom("Wave_Play", "Waves", value3);
		}
		playerLevelFinish = PlayerPrefsController.playerLevel;
		SubtractBooster();
		waveController.FinishWave();
		SetWallLife();
		upgradesController.SetInitialStructure(_setHeroes: true);
		PlayerPrefsController.SaveFinishedWave(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
		uiWaveController.SetPhaseInitial();
		uiController.UpdateSpeedButton();
		ConfigPrefsController.adsVideoCounter += ConfigPrefsController.adsVideoCounterLoseWave;
		ConfigPrefsController.adsInterstitialCounter += ConfigPrefsController.adsInterstitialCounterLoseWave;
		ConfigPrefsController.adsVideoDamageCounter++;
		UpgradesController.isExtraDamageActive = false;
		ShowOfferVideo();
		ShowInterstitial();
		SubmitScore();
		achievementsController.CheckAchievementsWave();
	}

	public void WinWave()
	{
		if (PlayerPrefs.GetInt("playerWave") <= 200)
		{
			string value = uiController.FormatString(PlayerPrefs.GetInt("playerWave"), tripleFormat: true);
			stationEngine.SendAnalyticCustom("Wave_Won", "Waves", value);
			stationEngine.SendAnalyticCustom("Wave_Play", "Waves", value);
		}
		if (PlayerPrefs.GetInt("playerWave") > 200 && PlayerPrefs.GetInt("playerWave") % 25 == 0)
		{
			string value2 = uiController.FormatString(PlayerPrefs.GetInt("playerWave"), tripleFormat: true);
			stationEngine.SendAnalyticCustom("Wave_Won", "Waves", value2);
			stationEngine.SendAnalyticCustom("Wave_Play", "Waves", value2);
		}
		if (PlayerPrefs.GetInt("playerWave") >= 1000 && PlayerPrefs.GetInt("playerWave") % 50 == 0)
		{
			string value3 = uiController.FormatString(PlayerPrefs.GetInt("playerWave"), tripleFormat: true);
			stationEngine.SendAnalyticCustom("Wave_Won", "Waves", value3);
			stationEngine.SendAnalyticCustom("Wave_Play", "Waves", value3);
		}
		switch (PlayerPrefs.GetInt("playerWave"))
		{
		case 1:
			stationEngine.SendAnalyticTutorialWaves("tutorial_wave_01");
			break;
		case 3:
			stationEngine.SendAnalyticTutorialWaves("tutorial_wave_03");
			break;
		case 4:
			stationEngine.SendAnalyticTutorialWaves("tutorial_wave_04");
			break;
		case 50:
			stationEngine.SendAnalyticTutorialWaves("050_waves_won");
			break;
		case 200:
			stationEngine.SendAnalyticTutorialWaves("200_waves_won");
			break;
		case 400:
			stationEngine.SendAnalyticTutorialWaves("400_waves_won");
			break;
		}
		playerLevelFinish = PlayerPrefsController.playerLevel;
		SubtractBooster();
		PlayerPrefs.SetInt("playerWave", PlayerPrefs.GetInt("playerWave") + 1);
		PlayerPrefs.SetInt("playerLevel", PlayerPrefsController.playerLevel);
		PlayerPrefs.SetFloat("playerExp", PlayerPrefsController.playerExp);
		PlayerPrefs.Save();
		waveController.FinishWave();
		SetWallLife();
		upgradesController.SetInitialStructure(_setHeroes: true);
		PlayerPrefsController.SaveFinishedWave(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
		uiWaveController.SetPhaseInitial();
		uiController.UpdateSpeedButton();
		ConfigPrefsController.adsVideoCounter += ConfigPrefsController.adsVideoCounterWinWave;
		ConfigPrefsController.adsInterstitialCounter += ConfigPrefsController.adsInterstitialCounterWinWave;
		ConfigPrefsController.adsVideoDamageCounter = 0;
		ConfigPrefsController.adsVideoDamageCounterSeen = 0;
		UpgradesController.isExtraDamageActive = false;
		ShowOfferVideo();
		ShowInterstitial();
		SubmitScore();
		achievementsController.CheckAchievementsWave();
	}

	private void WinWaveTutorial()
	{
		if (tutorialController.GetActualIndex() == 3)
		{
			if (PlayerPrefs.GetFloat("playerMoney") >= (float)(PlayerPrefsController.upgradeWallPrices[1] + PlayerPrefsController.upgradeArcherPrices[0]))
			{
				tutorialController.ActivateStep(4);
			}
			else
			{
				tutorialController.SetGlow(0);
			}
		}
		else if (tutorialController.GetActualIndex() == 6)
		{
			tutorialController.ActivateStep(7);
		}
		else if (tutorialController.GetActualIndex() == 11)
		{
			if (PlayerPrefs.GetInt("playerExpPoints") >= ConfigPrefsController.generalPowerCostUpgrade_WarCry)
			{
				tutorialController.ActivateStep(12);
			}
			else
			{
				tutorialController.SetGlow(0);
			}
		}
		else if (tutorialController.GetActualIndex() == 18)
		{
			tutorialController.ActivateStep(19);
		}
	}

	public void LoseInvasion()
	{
		string value = uiController.FormatString(EnemyPrefsController.LevelIndexSelected, tripleFormat: false);
		stationEngine.SendAnalyticCustom("Map_Lose", "Colony", value);
		stationEngine.SendAnalyticCustom("Map_Play", "Colony", value);
		ConfigPrefsController.adsVideoCounter += ConfigPrefsController.adsVideoCounterLoseInvasion;
		ConfigPrefsController.adsInterstitialCounter += ConfigPrefsController.adsInterstitialCounterLoseInvasion;
		PlayerPrefs.SetInt("playerLevel", PlayerPrefsController.playerLevel);
		PlayerPrefs.SetFloat("playerExp", PlayerPrefsController.playerExp);
		LoadMap();
	}

	public void WinInvasion()
	{
		string value = uiController.FormatString(EnemyPrefsController.LevelIndexSelected, tripleFormat: false);
		stationEngine.SendAnalyticCustom("Map_Won", "Colony", value);
		stationEngine.SendAnalyticCustom("Map_Play", "Colony", value);
		if (EnemyPrefsController.cityCharacter == CityCharacter.Hero)
		{
			PlayerPrefsController.HeroeLvl[EnemyPrefsController.cityCharacterIndex] = 1;
			EnemyPrefsController.tempUnlockedChara = true;
		}
		else if (EnemyPrefsController.cityCharacter == CityCharacter.Mercenary)
		{
			PlayerPrefsController.UnitsTechMercenary[EnemyPrefsController.cityCharacterIndex] = true;
			EnemyPrefsController.tempUnlockedChara = true;
		}
		PlayerPrefsController.CitiesConquered[EnemyPrefsController.LevelIndexSelected] = true;
		PlayerPrefs.SetInt("playerLevel", PlayerPrefsController.playerLevel);
		PlayerPrefs.SetFloat("playerExp", PlayerPrefsController.playerExp);
		PlayerPrefsController.SaveCitiesConquered(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
		ConfigPrefsController.adsVideoCounter += ConfigPrefsController.adsVideoCounterWinInvasion;
		ConfigPrefsController.adsInterstitialCounter += ConfigPrefsController.adsInterstitialCounterWinInvasion;
		LoadMap();
		int num = 0;
		for (int i = 0; i < PlayerPrefsController.CitiesConquered.Length; i++)
		{
			int @int = PlayerPrefs.GetInt("citiesConquered_" + i.ToString(), 0);
			if (@int > 0)
			{
				num++;
			}
		}
		if (num == 50)
		{
			stationEngine.SendAnalyticTutorialWaves("050_colonies_conquered");
		}
		if (num == 100)
		{
			stationEngine.SendAnalyticTutorialWaves("100_colonies_conquered");
		}
	}

	public void SetWallLife()
	{
		int num = 0;
		num = ((worldScreen != WorldScreen.Attack) ? PlayerPrefsController.WallLvl : EnemyPrefsController.WallLvl);
		if (worldScreen == WorldScreen.Defence || worldScreen == WorldScreen.Upgrade)
		{
			gateLife = ConfigPrefsController.lifeWallBase + (float)num * ConfigPrefsController.lifePerWallLevel;
			gateLife += gateLife * ConfigPrefsController.generalBaseWallHpPerLevel * (float)PlayerPrefsController.GeneralTechBase_WallHp;
		}
		else
		{
			gateLife = ConfigPrefsController.lifeWallBase;
			float num2 = (float)EnemyPrefsController.WallLvl * ConfigPrefsController.lifePerWallLevel;
			gateLife += num2;
		}
		gateLifeActual = gateLife;
	}

	public void KilledEnemy(float _income, float _exp)
	{
		if (worldScreen == WorldScreen.Defence || worldScreen == WorldScreen.Upgrade)
		{
			if (PlayerPrefs.GetInt("moneyBoost") > 0)
			{
				PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") + _income * 2f);
			}
			else if (PlayerPrefs.GetInt("moneyBoost") <= 0)
			{
				PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") + _income);
			}
		}
		if (PlayerPrefs.GetInt("xpBoost") > 0 && worldScreen == WorldScreen.Defence)
		{
			PlayerPrefsController.playerExp += _exp * 2f;
		}
		else if (PlayerPrefs.GetInt("xpBoost") == 0)
		{
			PlayerPrefsController.playerExp += _exp;
		}
		if (PlayerPrefsController.playerExp >= (float)PlayerPrefsController.playerExpLevels[PlayerPrefsController.playerLevel])
		{
			LevelUp();
		}
	}

	private void LevelUp()
	{
		sfxUIController.PlaySound(SfxUI.LevelUp);
		Object.Instantiate(Resources.Load("Canvas/CanvasLevelUp"));
		PlayerPrefsController.playerLevel++;
		PlayerPrefs.SetInt("playerExpPoints", PlayerPrefs.GetInt("playerExpPoints") + ConfigPrefsController.playerExperiencePointsPerLevel);
		PlayerPrefsController.playerExp = 0f;
		PlayerPrefs.SetInt("playerLevel", PlayerPrefsController.playerLevel);
		PlayerPrefs.SetFloat("playerExp", PlayerPrefsController.playerExp);
		PlayerPrefs.Save();
	}

	private void ShowInterstitial()
	{
		if (PlayerPrefsController.tutorialSteps[27] && (float)PlayerPrefs.GetInt("playerWave") >= ConfigPrefsController.adsInterstitialMinWave && (worldScreen == WorldScreen.Upgrade || worldScreen == WorldScreen.Defence) && ConfigPrefsController.adsInterstitialCounter >= ConfigPrefsController.adsInterstitialCounterFlag)
		{
			ConfigPrefsController.adsInterstitialCounter = 0;
			if (!PlayerPrefsController.IsVipUser())
			{
				stationEngine.ShowInterstitial(StationEngineFirebase.AnalyticsAdsPosition.In_Game);
			}
		}
	}

	private void ShowOfferVideo()
	{
		bool flag = false;
		if (offerObject != null)
		{
			UnityEngine.Object.Destroy(offerObject);
		}
		if (PlayerPrefsController.tutorialSteps[27] && PlayerPrefs.GetInt("playerWave") >= ConfigPrefsController.adsVideoMinWave && (worldScreen == WorldScreen.Upgrade || worldScreen == WorldScreen.Defence) && ConfigPrefsController.adsVideoCounter >= ConfigPrefsController.adsVideoCounterFlag)
		{
			if (stationEngine.CheckVideoReward())
			{
				ConfigPrefsController.adsVideoCounter = 0;
				offerObject = (UnityEngine.Object.Instantiate(Resources.Load("Canvas/CanvasMoneyForVideo")) as GameObject);
				MoneyForVideo component = offerObject.GetComponent<MoneyForVideo>();
				component.SetData();
				flag = true;
			}
			else if (stationEngine.GetStatusGeoLocation() == StationEngine.ComponentStatus.INITIALIZED)
			{
				stationEngine.SendAnalyticCustom("VideoReward", "NoFillRate", stationEngine.GetCountryCode());
			}
		}
		if (flag || PlayerPrefs.GetInt("ratedGame", 0) != 0)
		{
			return;
		}
		float num = UnityEngine.Random.Range(0f, 1f);
		if (num < ConfigPrefsController.rateProbability && (worldScreen == WorldScreen.Upgrade || worldScreen == WorldScreen.Defence))
		{
			if (rateObject != null)
			{
				UnityEngine.Object.Destroy(rateObject);
			}
			if (PlayerPrefs.GetInt("playerWave") >= ConfigPrefsController.rateMinWave)
			{
				rateObject = (UnityEngine.Object.Instantiate(Resources.Load("Canvas/CanvasMoneyForRate")) as GameObject);
				MoneyForRate component2 = rateObject.GetComponent<MoneyForRate>();
				component2.SetData();
			}
		}
	}

	private void DestroyOffer()
	{
		if (offerObject != null)
		{
			stationEngine.SendAnalyticCustom("Behaviour_Videos", "Skipped", "Wave_" + PlayerPrefs.GetInt("playerWave", 0));
			UnityEngine.Object.Destroy(offerObject);
		}
		if (rateObject != null)
		{
			UnityEngine.Object.Destroy(rateObject);
		}
	}

	public void HideOffer()
	{
		if (offerObject != null)
		{
			MoneyForVideo component = offerObject.GetComponent<MoneyForVideo>();
			component.HideButton();
		}
		if (rateObject != null)
		{
			MoneyForRate component2 = rateObject.GetComponent<MoneyForRate>();
			component2.HideButton();
		}
	}

	public void UnhideOffer()
	{
		if (offerObject != null)
		{
			MoneyForVideo component = offerObject.GetComponent<MoneyForVideo>();
			component.UnhideButton();
		}
		if (rateObject != null)
		{
			MoneyForRate component2 = rateObject.GetComponent<MoneyForRate>();
			component2.UnhideButton();
		}
	}

	private void SubmitScore()
	{
		stationEngine.SubmitToLeaderboard(PlayerPrefs.GetInt("playerWave"), 0);
	}

	public void ShowWaveOffer()
	{
		if (PlayerPrefsController.tutorialSteps[27] && PlayerPrefs.GetInt("playerWave") >= ConfigPrefsController.adsVideoMinWave && (worldScreen == WorldScreen.Upgrade || worldScreen == WorldScreen.Defence) && ConfigPrefsController.adsVideoDamageCounter >= ConfigPrefsController.adsVideoDamageCounterFlag)
		{
			if (stationEngine.CheckVideoReward())
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Canvas/CanvasDamageForVideo")) as GameObject;
				DamageForVideo component = gameObject.GetComponent<DamageForVideo>();
				component.Initialize(stationEngine, uiController);
			}
			else if (stationEngine.GetStatusGeoLocation() == StationEngine.ComponentStatus.INITIALIZED)
			{
				stationEngine.SendAnalyticCustom("VideoReward", "NoFillRate", stationEngine.GetCountryCode());
			}
		}
	}

	private void SubtractBooster()
	{
		if (PlayerPrefs.GetInt("moneyBoost") > 0)
		{
			PlayerPrefs.SetInt("moneyBoost", PlayerPrefs.GetInt("moneyBoost") - 1);
		}
		if (PlayerPrefs.GetInt("xpBoost") > 0)
		{
			PlayerPrefs.SetInt("xpBoost", PlayerPrefs.GetInt("xpBoost") - 1);
		}
		if (PlayerPrefs.GetInt("powerBoost") > 0)
		{
			PlayerPrefs.SetInt("powerBoost", PlayerPrefs.GetInt("powerBoost") - 1);
		}
		PlayerPrefs.Save();
	}

	private void OnApplicationQuit()
	{
		if (worldScreen == WorldScreen.Defence)
		{
			PlayerPrefs.SetInt("playerLevel", PlayerPrefsController.playerLevel);
			PlayerPrefs.SetFloat("playerExp", PlayerPrefsController.playerExp);
			SubtractBooster();
		}
		PlayerPrefs.SetInt("playerLevel", PlayerPrefsController.playerLevel);
		PlayerPrefs.SetFloat("playerExp", PlayerPrefsController.playerExp);
		PlayerPrefs.Save();
	}
}
