using UnityEngine;
using UnityEngine.UI;

public class UIWindowExit : MonoBehaviour
{
	public static bool IsWindowOpen;

	public bool showNative;

	public CanvasScaler myScaler;

	public RectTransform transformButtons;

	private MainController mainController;

	private SfxUIController sfxUiController;

	private StationEngine stationEngine;

	private GameObject pauseMenuObject;

	private void Awake()
	{
		IsWindowOpen = true;
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		mainController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainController>();
		sfxUiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SfxUIController>();
		pauseMenuObject = GameObject.FindGameObjectWithTag("MenuPause");
		pauseMenuObject.SetActive(value: false);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ButtonPressNo();
		}
	}

	public void ButtonPressYesGame()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		Application.Quit();
	}

	public void ButtonPressYesBattle()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		stationEngine.HideBanner();
		Time.timeScale = 1f;
		Touch_Battle.IsWindowBigOpen = false;
		Touch_Battle.IsWindowSmallOpen = false;
		Touch_Map.IsWindowOpen = false;
		IsWindowOpen = false;
		mainController.LoadMap();
	}

	public void ButtonPressYesWave()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		stationEngine.HideBanner();
		Time.timeScale = 1f;
		Touch_Battle.IsWindowBigOpen = false;
		Touch_Battle.IsWindowSmallOpen = false;
		Touch_Map.IsWindowOpen = false;
		IsWindowOpen = false;
		mainController.CancelWave();
		UnityEngine.Object.Destroy(pauseMenuObject);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void ButtonPressNo()
	{
		sfxUiController.PlaySound(SfxUI.ClickClose);
		pauseMenuObject.SetActive(value: true);
		IsWindowOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
