using I2.Loc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMapController : MonoBehaviour
{
	public static int CitySelected;

	public GameObject boosted;

	public Image moneyBoosted;

	public Text textMoneyBoosted;

	public GameObject mainMenu;

	public GameObject attackMenu;

	public GameObject upgradeMenu;

	private UIWindowUpgradeColony uiWindowUpgradeColony;

	public CityIcon[] citiesArray;

	public Text textMoney;

	public Text textIncome;

	public Image imageIncome;

	private TutorialController tutorialController;

	private SfxUIController sfxUIController;

	private UIWindowAttack uiWindowAttack;

	private StationEngine stationEngine;

	private AchievementsController achievementsController;

	private void Awake()
	{
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		achievementsController = base.gameObject.GetComponent<AchievementsController>();
		tutorialController = base.gameObject.GetComponent<TutorialController>();
		sfxUIController = base.gameObject.GetComponent<SfxUIController>();
		uiWindowAttack = attackMenu.GetComponent<UIWindowAttack>();
		Touch_Map.IsWindowOpen = false;
		uiWindowUpgradeColony = upgradeMenu.GetComponent<UIWindowUpgradeColony>();
		mainMenu.SetActive(value: true);
		attackMenu.SetActive(value: false);
		upgradeMenu.SetActive(value: false);
		UpdateTopStats();
	}

	private void Start()
	{
		if (EnemyPrefsController.tempUnlockedChara)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Windows/CanvasUnlockedCharacter")) as GameObject;
			UIWindowInfo component = gameObject.GetComponent<UIWindowInfo>();
			component.SetCharacterData(EnemyPrefsController.cityCharacter, EnemyPrefsController.cityCharacterIndex);
			EnemyPrefsController.tempUnlockedChara = false;
		}
		if (PlayerPrefs.GetInt("tutorialStep_27") == 1 && !PlayerPrefsController.IsVipUser())
		{
			stationEngine.ShowBanner(StationEngineFirebase.AnalyticsAdsPosition.Map, AdsBannerPosition.Bottom);
		}
		CheckBoosts();
	}

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape) || !PlayerPrefsController.tutorialSteps[27])
		{
			return;
		}
		if (Touch_Map.IsWindowOpen)
		{
			if (attackMenu.activeSelf)
			{
				ButtonCloseMenu(_showBanner: true);
			}
			else
			{
				ButtonCloseMenu(_showBanner: false);
			}
		}
		else
		{
			ButtonMainMenu();
		}
	}

	public void CheckAchievements()
	{
		achievementsController.CheckAchievementsMap();
	}

	public void UpdateTopStats()
	{
		textMoney.text = Mathf.FloorToInt(PlayerPrefs.GetFloat("playerMoney")).ToString("###,###,##0");
	}

	public void ButtonCityUpgrade(CityIcon _cityIcon)
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		Touch_Map.IsWindowOpen = true;
		uiWindowUpgradeColony.CityIcon = _cityIcon;
		uiWindowUpgradeColony.UpdateWindow(_updateCityIcon: false);
		upgradeMenu.SetActive(value: true);
		mainMenu.SetActive(value: false);
	}

	public void ButtonCityAttack()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		Touch_Map.IsWindowOpen = true;
		attackMenu.SetActive(value: true);
		uiWindowAttack.StartWindow();
		mainMenu.SetActive(value: false);
		if (tutorialController.GetActualIndex() == 20)
		{
			tutorialController.ActivateStep(21);
		}
		stationEngine.HideBanner();
	}

	public void ButtonCloseMenu(bool _showBanner)
	{
		sfxUIController.PlaySound(SfxUI.ClickClose);
		Touch_Map.IsWindowOpen = false;
		if (attackMenu.activeSelf)
		{
			uiWindowAttack.ResetWindow();
		}
		attackMenu.SetActive(value: false);
		upgradeMenu.SetActive(value: false);
		mainMenu.SetActive(value: true);
		if (tutorialController.GetActualIndex() == 26)
		{
			LeanTween.cancel(citiesArray[1].gameObject);
			citiesArray[1].transform.localScale = new Vector3(1f, 1f, 1f);
			tutorialController.ActivateStep(27);
		}
		if (PlayerPrefs.GetInt("tutorialStep_27") == 1 && _showBanner && !PlayerPrefsController.IsVipUser())
		{
			stationEngine.ShowBanner(StationEngineFirebase.AnalyticsAdsPosition.Map, AdsBannerPosition.Bottom);
		}
	}

	public void ButtonMainMenu()
	{
		sfxUIController.PlaySound(SfxUI.ClickClose);
		GameObject gameObject = GameObject.Find("WaveRoman");
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		MainController.worldScreen = WorldScreen.Upgrade;
		MusicController component = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
		component.ChangeTrack(0, _fadeOffChangingScreen: true);
		stationEngine.HideBanner();
		SceneManager.LoadScene("MainScene");
	}

	private void CheckBoosts()
	{
		if (PlayerPrefs.GetInt("moneyBoost") > 0)
		{
			moneyBoosted.gameObject.SetActive(value: true);
			boosted.SetActive(value: true);
			textMoneyBoosted.text = PlayerPrefs.GetInt("moneyBoost").ToString() + " " + ScriptLocalization.Get("NORMAL/waves").ToUpper();
		}
		else if (PlayerPrefs.GetInt("moneyBoost") == 0)
		{
			moneyBoosted.gameObject.SetActive(value: false);
			boosted.SetActive(value: false);
		}
	}
}
