using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
	public CityIcon[] citiesArray;

	private TutorialController tutorialController;

	private StationEngine stationEngine;

	private AchievementsController achievementsController;

	private void Awake()
	{
		achievementsController = base.gameObject.GetComponent<AchievementsController>();
		tutorialController = base.gameObject.GetComponent<TutorialController>();
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		bool isTutorial = false;
		if (tutorialController.GetActualIndex() == 19)
		{
			tutorialController.ActivateStep(20);
			isTutorial = true;
		}
		Time.timeScale = 1f;
		ConfigPrefsController.PrepareTranslations();
		UpdateMapStatus(isTutorial);
		InitializeCities();
	}

	private void Start()
	{
		achievementsController.CheckAchievementsMap();
		ShowInterstitial();
	}

	private void InitializeCities()
	{
		for (int i = 0; i < citiesArray.Length; i++)
		{
			citiesArray[i].CityIndex = i;
			citiesArray[i].InitializeComponents();
			citiesArray[i].SetStatus();
		}
	}

	public void UpdateMapStatus(bool _isTutorial)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < citiesArray.Length; i++)
		{
			citiesArray[i].isConquered = PlayerPrefsController.CitiesConquered[i];
			if (citiesArray[i].cityCharacter == CityCharacter.Hero)
			{
				list.Add(i);
			}
			else if (citiesArray[i].cityCharacter == CityCharacter.Mercenary)
			{
				list2.Add(i);
			}
		}
		if (_isTutorial)
		{
			for (int j = 0; j < citiesArray.Length; j++)
			{
				citiesArray[j].isVisible = false;
			}
			citiesArray[0].isVisible = true;
			citiesArray[1].isVisible = true;
			LeanTween.scale(citiesArray[1].gameObject, new Vector3(0.8f, 0.8f, 0.8f), 1f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true)
				.setLoopPingPong();
		}
		else
		{
			for (int k = 0; k < citiesArray.Length; k++)
			{
				if (citiesArray[k].isConquered)
				{
					citiesArray[k].isVisible = true;
					for (int l = 0; l < citiesArray[k].citiesConnected.Length; l++)
					{
						citiesArray[k].citiesConnected[l].isVisible = true;
					}
				}
			}
			if (tutorialController.GetActualIndex() == 23)
			{
				citiesArray[1].isVisible = true;
				LeanTween.scale(citiesArray[1].gameObject, new Vector3(0.8f, 0.8f, 0.8f), 1f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true)
					.setLoopPingPong();
				tutorialController.ActivateStep(24);
			}
		}
		if (!PlayerPrefsController.tutorialSpecialHero)
		{
			bool flag = false;
			for (int m = 0; m < list.Count; m++)
			{
				if (PlayerPrefsController.CitiesConquered[list[m]])
				{
					flag = true;
					break;
				}
			}
			if (flag && !Touch_Map.IsWindowOpen)
			{
				PlayerPrefsController.tutorialSpecialHero = true;
				PlayerPrefsController.SaveTutorial();
				Object.Instantiate(Resources.Load("Canvas/Tutorial/CanvasTutorial_Heroes"));
			}
		}
		if (PlayerPrefsController.tutorialSpecialMercenary)
		{
			return;
		}
		bool flag2 = false;
		for (int n = 0; n < list2.Count; n++)
		{
			if (PlayerPrefsController.CitiesConquered[list2[n]])
			{
				flag2 = true;
				break;
			}
		}
		if (flag2 && !Touch_Map.IsWindowOpen)
		{
			PlayerPrefsController.tutorialSpecialMercenary = true;
			PlayerPrefsController.SaveTutorial();
			Object.Instantiate(Resources.Load("Canvas/Tutorial/CanvasTutorial_Mercenaries"));
		}
	}

	private void ShowInterstitial()
	{
		if (PlayerPrefsController.tutorialSteps[27] && (float)PlayerPrefs.GetInt("playerWave") >= ConfigPrefsController.adsInterstitialMinWave && ConfigPrefsController.adsInterstitialCounter >= ConfigPrefsController.adsInterstitialCounterFlag)
		{
			ConfigPrefsController.adsInterstitialCounter = 0;
			if (!PlayerPrefsController.IsVipUser())
			{
				stationEngine.ShowInterstitial(StationEngineFirebase.AnalyticsAdsPosition.Map);
			}
		}
	}
}
