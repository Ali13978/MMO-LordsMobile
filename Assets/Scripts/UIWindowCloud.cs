using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowCloud : MonoBehaviour
{
	public static bool IsWindowOpen;

	private StationEngine stationEngine;

	private UIWindowCloud uiWindowParent;

	private GameObject loadingBarObject;

	public Button buttonSaveCloud;

	public bool isMainMenu;

	private bool isLoading;

	private bool isSaving;

	public UIWindowCloud UiWindowParent
	{
		get
		{
			return uiWindowParent;
		}
		set
		{
			uiWindowParent = value;
		}
	}

	private void Awake()
	{
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		IsWindowOpen = true;
		if (isMainMenu)
		{
			if (!PlayerPrefsController.tutorialSteps[26])
			{
				buttonSaveCloud.interactable = false;
			}
			else
			{
				buttonSaveCloud.interactable = true;
			}
		}
	}

	private void Update()
	{
		if (isLoading)
		{
			if (stationEngine.GetCloudStatus() == CloudStatus.Error)
			{
				isLoading = false;
				Object.Instantiate(Resources.Load("Windows/CanvasCloudSave_ResultError"));
				UnityEngine.Object.Destroy(loadingBarObject);
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (stationEngine.GetCloudStatus() == CloudStatus.NoSavedGame)
			{
				isLoading = false;
				Object.Instantiate(Resources.Load("Windows/CanvasCloudSave_ResultErrorExist"));
				UnityEngine.Object.Destroy(loadingBarObject);
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (stationEngine.GetCloudStatus() == CloudStatus.Success)
			{
				isLoading = false;
				string cloudText = stationEngine.GetCloudText();
				PlayerPrefsController.CloudSaveRead(cloudText);
				Object.Instantiate(Resources.Load("Windows/CanvasCloudSave_ResultLoadOK"));
				UnityEngine.Object.Destroy(loadingBarObject);
			}
		}
		if (isSaving)
		{
			if (stationEngine.GetCloudStatus() == CloudStatus.Error)
			{
				isSaving = false;
				Object.Instantiate(Resources.Load("Windows/CanvasCloudSave_ResultError"));
				UnityEngine.Object.Destroy(loadingBarObject);
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (stationEngine.GetCloudStatus() == CloudStatus.Success)
			{
				isSaving = false;
				Object.Instantiate(Resources.Load("Windows/CanvasCloudSave_ResultSaveOK"));
				UnityEngine.Object.Destroy(loadingBarObject);
				UnityEngine.Object.Destroy(uiWindowParent.gameObject);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	public void ButtonPressCloseMainMenu()
	{
		IsWindowOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void ButtonPressClose()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void ButtonPressLoadCloud()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Windows/CanvasCloudSave_PromptLoad")) as GameObject;
		UIWindowCloud component = gameObject.GetComponent<UIWindowCloud>();
		component.UiWindowParent = this;
	}

	public void ConfirmCloudLoad()
	{
		loadingBarObject = (UnityEngine.Object.Instantiate(Resources.Load("Windows/CanvasCloudSave_BarLoading")) as GameObject);
		isLoading = true;
		StartCoroutine(MakeLoad());
	}

	public IEnumerator MakeLoad()
	{
		yield return null;
		stationEngine.CloudLoadGame();
	}

	public void ButtonPressSaveCloud()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Windows/CanvasCloudSave_PromptSave")) as GameObject;
		UIWindowCloud component = gameObject.GetComponent<UIWindowCloud>();
		component.UiWindowParent = this;
	}

	public void ConfirmCloudSave()
	{
		loadingBarObject = (UnityEngine.Object.Instantiate(Resources.Load("Windows/CanvasCloudSave_BarSaving")) as GameObject);
		isSaving = true;
		StartCoroutine(MakeSave());
	}

	public IEnumerator MakeSave()
	{
		yield return null;
		string textSave = PlayerPrefsController.CloudSaveWrite();
		stationEngine.CloudSaveGame(textSave);
	}

	public void ButtonPressRestartGame()
	{
		Application.Quit();
	}
}
