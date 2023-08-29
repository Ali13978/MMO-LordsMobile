using I2.Loc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISelectLanguage : MonoBehaviour
{
	public static bool ChangedLanguage;

	public Button buttonClose;

	public Color colorTextSelected;

	public Color colorTextNormal;

	public Text[] flagsTextArray;

	public RectTransform[] transformButtons;

	public Text titleText;

	private GameObject gameObjectSelector;

	private int languageIndexSelected;

	private string initialLanguage;

	private string actualLanguage;

	public void Initialize()
	{
		Touch_Battle.IsWindowBigOpen = true;
	}

	private void Awake()
	{
		actualLanguage = PlayerPrefs.GetString("selectedLanguage", "en");
		initialLanguage = actualLanguage;
		switch (actualLanguage)
		{
		case "en":
			languageIndexSelected = 0;
			break;
		case "ru":
			languageIndexSelected = 1;
			break;
		case "es":
			languageIndexSelected = 2;
			break;
		case "it":
			languageIndexSelected = 3;
			break;
		case "de":
			languageIndexSelected = 4;
			break;
		case "fr":
			languageIndexSelected = 5;
			break;
		case "pt":
			languageIndexSelected = 6;
			break;
		case "nl":
			languageIndexSelected = 7;
			break;
		case "ca":
			languageIndexSelected = 8;
			break;
		case "pl":
			languageIndexSelected = 9;
			break;
		case "tr":
			languageIndexSelected = 10;
			break;
		case "hi":
			languageIndexSelected = 11;
			break;
		case "th":
			languageIndexSelected = 12;
			break;
		case "ko":
			languageIndexSelected = 13;
			break;
		}
		UpdateFlags();
		Initialize();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ButtonPressClose();
		}
	}

	public void ButtonPressLanguage(int languageIndex)
	{
		languageIndexSelected = languageIndex;
		switch (languageIndexSelected)
		{
		case 0:
			actualLanguage = "en";
			break;
		case 1:
			actualLanguage = "ru";
			break;
		case 2:
			actualLanguage = "es";
			break;
		case 3:
			actualLanguage = "it";
			break;
		case 4:
			actualLanguage = "de";
			break;
		case 5:
			actualLanguage = "fr";
			break;
		case 6:
			actualLanguage = "pt";
			break;
		case 7:
			actualLanguage = "nl";
			break;
		case 8:
			actualLanguage = "ca";
			break;
		case 9:
			actualLanguage = "pl";
			break;
		case 10:
			actualLanguage = "tr";
			break;
		case 11:
			actualLanguage = "hi";
			break;
		case 12:
			actualLanguage = "th";
			break;
		case 13:
			actualLanguage = "ko";
			break;
		}
		UpdateFlags();
	}

	public void ButtonPressClose()
	{
		bool flag = false;
		if (!PlayerPrefs.HasKey("selectedLanguage"))
		{
			flag = true;
		}
		PlayerPrefs.SetString("selectedLanguage", actualLanguage);
		PlayerPrefs.Save();
		LocalizationManager.CurrentLanguageCode = actualLanguage;
		if (initialLanguage != actualLanguage || flag)
		{
			StationEngine component = GameObject.Find("StationEngine").GetComponent<StationEngine>();
			component.HideBanner();
			ChangedLanguage = true;
			Touch_Battle.IsWindowBigOpen = false;
			Touch_Map.IsWindowOpen = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void UpdateFlags()
	{
		for (int i = 0; i < transformButtons.Length; i++)
		{
			if (i == languageIndexSelected)
			{
				if (gameObjectSelector != null)
				{
					UnityEngine.Object.Destroy(gameObjectSelector);
				}
				LocalizationManager.CurrentLanguageCode = actualLanguage;
				titleText.text = ScriptLocalization.Get("MAIN/language_title").ToUpper();
				gameObjectSelector = (UnityEngine.Object.Instantiate(Resources.Load("UI/WindowTechs/SelectorLanguage")) as GameObject);
				RectTransform component = gameObjectSelector.GetComponent<RectTransform>();
				component.SetParent(transformButtons[i]);
				component.anchoredPosition = Vector2.zero;
				component.localScale = new Vector3(1f, 1f, 1f);
				component.sizeDelta = new Vector2(16f, 16f);
			}
		}
	}
}
