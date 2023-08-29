using UnityEngine;
using UnityEngine.SceneManagement;

public class UIWindowTutorial : MonoBehaviour
{
	public static bool WindowOpen;

	public int _tutorialIndex;

	private TutorialController tutorialScript;

	private SfxUIController sfxUiController;

	private StationEngine stationEngine;

	private void Awake()
	{
		tutorialScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<TutorialController>();
		sfxUiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SfxUIController>();
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		WindowOpen = true;
	}

	private void Start()
	{
		if (_tutorialIndex == -1)
		{
			Time.timeScale = 0f;
		}
		sfxUiController.PlaySound(SfxUI.WindowOpen);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ButtonPressCloseWindow();
		}
	}

	public void ButtonPressCloseWindow()
	{
		WindowOpen = false;
		sfxUiController.PlaySound(SfxUI.ClickClose);
		if (_tutorialIndex == 0)
		{
			tutorialScript.ActivateStep(1);
			stationEngine.SendAnalyticTutorialBegin();
		}
		else if (_tutorialIndex == 4)
		{
			tutorialScript.ActivateStep(5);
		}
		else if (_tutorialIndex == 10)
		{
			tutorialScript.ActivateStep(11);
		}
		else if (_tutorialIndex == 24)
		{
			tutorialScript.ActivateStep(25);
		}
		else if (_tutorialIndex == 26)
		{
			PlayerPrefsController.tutorialSteps[27] = true;
			PlayerPrefsController.SaveTutorial();
			stationEngine.SendAnalyticTutorialComplete();
		}
		else if (_tutorialIndex == -1)
		{
			if (SceneManager.GetActiveScene().name == "MainScene")
			{
				UIController component = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
				component.UpdateSpeedButton();
			}
			else
			{
				Time.timeScale = 1f;
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void ButtonPressSkipTutorial()
	{
		WindowOpen = false;
		sfxUiController.PlaySound(SfxUI.ClickClose);
		for (int i = 0; i < PlayerPrefsController.tutorialSteps.Length; i++)
		{
			PlayerPrefsController.tutorialSteps[i] = true;
		}
		PlayerPrefsController.SaveTutorial();
		SceneManager.LoadScene("MainScene");
	}
}
