using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class TranslationsController : MonoBehaviour
{
	private const string keySelectLanguage = "selectedLanguage";

	public Text textLoading;

	private string actualLanguage = string.Empty;

	private void Awake()
	{
		actualLanguage = "en";
		if (PlayerPrefs.HasKey("selectedLanguage"))
		{
			actualLanguage = PlayerPrefs.GetString("selectedLanguage", "en");
		}
		else
		{
			switch (Application.systemLanguage)
			{
			case SystemLanguage.English:
				actualLanguage = "en";
				break;
			case SystemLanguage.Catalan:
				actualLanguage = "ca";
				break;
			case SystemLanguage.Spanish:
				actualLanguage = "es";
				break;
			case SystemLanguage.Russian:
				actualLanguage = "ru";
				break;
			case SystemLanguage.Polish:
				actualLanguage = "pl";
				break;
			case SystemLanguage.Korean:
				actualLanguage = "ko";
				break;
			case SystemLanguage.Italian:
				actualLanguage = "it";
				break;
			case SystemLanguage.French:
				actualLanguage = "fr";
				break;
			case SystemLanguage.German:
				actualLanguage = "de";
				break;
			case SystemLanguage.Japanese:
				actualLanguage = "ja";
				break;
			case SystemLanguage.Portuguese:
				actualLanguage = "pt";
				break;
			case SystemLanguage.Thai:
				actualLanguage = "th";
				break;
			case SystemLanguage.Turkish:
				actualLanguage = "tr";
				break;
			case SystemLanguage.Dutch:
				actualLanguage = "nl";
				break;
			}
		}
		LocalizationManager.CurrentLanguageCode = actualLanguage;
		if (textLoading != null)
		{
			textLoading.text = ScriptLocalization.Get("NORMAL/loading") + "...";
		}
	}
}
