using UnityEngine;
using UnityEngine.UI;

public class UIWindowPause : MonoBehaviour
{
	public GameObject buttonObjectExitGame;

	public GameObject buttonObjectExitBattle;

	public GameObject buttonObjectExitWave;

	public GameObject buttonObjectLanguage;

	public Button buttonAchievements;

	public Button buttonLeaderboard;

	public Button buttonCloud;

	public Image buttonImageGPG;

	public Image gpgIcon;

	public Image achievementsIcon;

	public Image leaderboardIcon;

	public Image cloudIcon;

	public Sprite spriteGPGOn;

	public Sprite spriteGPGOff;

	public Sprite spriteGPGIconOn;

	public Sprite spriteGPGIconOff;

	public Image imageLanguage;

	public Sprite[] spriteLanguages;

	public Image imageSfx;

	public Image imageSfxIcon;

	public Image imageMusic;

	public Image imageMusicIcon;

	public Text textVersion;

	private bool gpgEnabled;

	private SfxUIController sfxUiController;

	private MusicController musicController;

	private StationEngine stationEngine;

	private void Awake()
	{
		Time.timeScale = 0f;
		Touch_Battle.IsWindowBigOpen = true;
		Touch_Map.IsWindowOpen = true;
		sfxUiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SfxUIController>();
		musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		if (!PlayerPrefsController.tutorialSteps[26])
		{
			buttonObjectExitBattle.SetActive(value: false);
			buttonObjectExitWave.SetActive(value: false);
			buttonObjectLanguage.GetComponent<Button>().interactable = false;
		}
		if (MainController.worldScreen == WorldScreen.Upgrade)
		{
			buttonObjectExitBattle.SetActive(value: false);
			buttonObjectExitWave.SetActive(value: false);
		}
		else if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			buttonObjectExitGame.SetActive(value: false);
			buttonObjectExitWave.SetActive(value: false);
			buttonObjectLanguage.GetComponent<Button>().interactable = false;
		}
		else if (MainController.worldScreen == WorldScreen.Defence)
		{
			buttonObjectExitGame.SetActive(value: false);
			buttonObjectExitBattle.SetActive(value: false);
			buttonObjectLanguage.GetComponent<Button>().interactable = false;
		}
		textVersion.text = "v" + Application.version;
		SetButtonsImage();
		UpdateLanguageButton();
		UpdateGPG();
	}

	private void Start()
	{
		if (!PlayerPrefsController.IsVipUser())
		{
			stationEngine.ShowBanner(StationEngineFirebase.AnalyticsAdsPosition.Pause, AdsBannerPosition.Bottom);
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && !UIWindowExit.IsWindowOpen && !UIWindowCloud.IsWindowOpen && !PrivacyUI.IsWindowOpen)
		{
			ButtonPressResume();
		}
		if ((gpgEnabled && !stationEngine.GameServicesIsLogged()) || (!gpgEnabled && stationEngine.GameServicesIsLogged()))
		{
			UpdateGPG();
		}
	}

	private void UpdateGPG()
	{
		gpgEnabled = stationEngine.GameServicesIsLogged();
		buttonAchievements.interactable = gpgEnabled;
		buttonCloud.interactable = gpgEnabled;
		buttonLeaderboard.interactable = gpgEnabled;
		if (gpgEnabled)
		{
			buttonImageGPG.sprite = spriteGPGOn;
			if (Application.platform == RuntimePlatform.Android)
			{
				gpgIcon.sprite = spriteGPGIconOn;
			}
			achievementsIcon.color = new Color(1f, 1f, 1f, 1f);
			leaderboardIcon.color = new Color(1f, 1f, 1f, 1f);
			cloudIcon.color = new Color(1f, 1f, 1f, 1f);
		}
		else
		{
			buttonImageGPG.sprite = spriteGPGOff;
			if (Application.platform == RuntimePlatform.Android)
			{
				gpgIcon.sprite = spriteGPGIconOff;
			}
			achievementsIcon.color = new Color(1f, 1f, 1f, 0.4f);
			leaderboardIcon.color = new Color(1f, 1f, 1f, 0.4f);
			cloudIcon.color = new Color(1f, 1f, 1f, 0.4f);
		}
	}

	private void UpdateLanguageButton()
	{
		if (buttonObjectLanguage.GetComponent<Button>().IsInteractable())
		{
			imageLanguage.color = new Color(255f, 255f, 255f, 255f);
		}
		else
		{
			imageLanguage.color = new Color(255f, 255f, 255f, 0.4f);
		}
	}

	private void SetButtonsImage()
	{
		Color color = new Color32(156, 156, 156, byte.MaxValue);
		if (PlayerPrefsController.isMusic)
		{
			imageMusic.color = new Color(255f, 255f, 255f);
			imageMusicIcon.color = new Color(255f, 255f, 255f);
		}
		else
		{
			imageMusic.color = color;
			imageMusicIcon.color = color;
		}
		if (PlayerPrefsController.isSfx)
		{
			imageSfx.color = new Color(255f, 255f, 255f);
			imageSfxIcon.color = new Color(255f, 255f, 255f);
		}
		else
		{
			imageSfx.color = color;
			imageSfxIcon.color = color;
		}
	}

	public void ButtonPressResume()
	{
		sfxUiController.PlaySound(SfxUI.ClickClose);
		MainController component = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainController>();
		component.UnhideOffer();
		UIController component2 = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
		component2.UpdateSpeedButton();
		stationEngine.HideBanner();
		Touch_Battle.IsWindowBigOpen = false;
		Touch_Map.IsWindowOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void ButtonPressExitGame()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		Object.Instantiate(Resources.Load("Windows/CanvasExitGame"));
	}

	public void ButtonPressExitBattle()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		Object.Instantiate(Resources.Load("Windows/CanvasExitBattle"));
	}

	public void ButtonPressExitWave()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		Object.Instantiate(Resources.Load("Windows/CanvasExitWave"));
	}

	public void ButtonPressSfx()
	{
		if (PlayerPrefsController.isSfx)
		{
			PlayerPrefsController.isSfx = false;
		}
		else
		{
			PlayerPrefsController.isSfx = true;
		}
		PlayerPrefsController.SaveSoundPrefs();
		SetButtonsImage();
		sfxUiController.CheckScenarioSfx();
		sfxUiController.PlaySound(SfxUI.ClickDefault);
	}

	public void ButtonPressMusic()
	{
		if (PlayerPrefsController.isMusic)
		{
			PlayerPrefsController.isMusic = false;
		}
		else
		{
			PlayerPrefsController.isMusic = true;
		}
		PlayerPrefsController.SaveSoundPrefs();
		musicController.SetStatus(_forceVolume: true, _fadeOffChangingScreen: false);
		SetButtonsImage();
		sfxUiController.PlaySound(SfxUI.ClickDefault);
	}

	public void ButtonPressMoreGames()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		stationEngine.MoreGames();
	}

	public void ButtonPressShare()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		stationEngine.ShareAllMedia();
	}

	public void ButtonPressGameCenter()
	{
		if (stationEngine.GameServicesIsLogged())
		{
			stationEngine.ShowAchievement();
		}
		else
		{
			stationEngine.GameServicesLogIn();
		}
	}

	public void ButtonPressLogInOut()
	{
		stationEngine.GooglePlayGamesLogInOut();
	}

	public void ButtonPressLeaderboard()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		stationEngine.ShowLeaderboard(0, _logAnyway: true);
	}

	public void ButtonPressAchievements()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		stationEngine.ShowAchievement();
	}

	public void ButtonPressCloud()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		if (stationEngine.GameServicesIsLogged())
		{
			Object.Instantiate(Resources.Load("Windows/CanvasCloudSave"));
		}
		else
		{
			stationEngine.GameServicesLogIn();
		}
	}

	public void ButtonPressFacebookPage()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		stationEngine.FacebookPage();
	}

	public void ButtonPressPrivacyPolicy()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		stationEngine.ShowPostAgreement();
	}

	public void ButtonPressLanguage()
	{
		stationEngine.HideBanner();
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Windows/CanvasLanguage")) as GameObject;
		UISelectLanguage component = gameObject.GetComponent<UISelectLanguage>();
		component.Initialize();
	}
}
