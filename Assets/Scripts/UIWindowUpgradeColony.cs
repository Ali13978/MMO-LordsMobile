using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowUpgradeColony : MonoBehaviour
{
	public Text[] textCost;

	public Text[] textIncomeOld;

	public Text[] textIncomeNew;

	public Button[] buttonUpgrade;

	public Sprite[] spriteCity;

	public GameObject[] cityOff;

	public GameObject[] upgradeCity;

	public GameObject[] cityUnlocked;

	public GameObject[] unlockedEffect;

	private CityIcon cityIcon;

	private int upgradeCost;

	private UIMapController uiMapController;

	public UIIncomeColonies uiIncomeColonies;

	private SfxUIController sfxUIController;

	private bool[] hasUpgraded = new bool[5]
	{
		true,
		false,
		false,
		false,
		false
	};

	private float timer;

	public CityIcon CityIcon
	{
		get
		{
			return cityIcon;
		}
		set
		{
			cityIcon = value;
		}
	}

	private void Awake()
	{
		uiMapController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIMapController>();
		sfxUIController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SfxUIController>();
	}

	public void ButtonUpgradeCity()
	{
		hasUpgraded[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex]] = true;
		UnlockAnimation();
		if (PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] < 4)
		{
			sfxUIController.PlaySound(SfxUI.ClickBuy);
			PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)upgradeCost);
			uiMapController.UpdateTopStats();
			PlayerPrefsController.CitiesLevel[cityIcon.CityIndex]++;
			uiIncomeColonies.CalculateIncome();
			PlayerPrefsController.SaveCitiesConquered(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
			UpdateWindow(_updateCityIcon: true);
			uiMapController.CheckAchievements();
		}
	}

	public void UpdateWindow(bool _updateCityIcon)
	{
		Cities();
		for (int i = 0; i < textIncomeOld.Length; i++)
		{
			if (i <= PlayerPrefsController.CitiesLevel[cityIcon.CityIndex])
			{
				float num = (float)(i + 1) * ((5f + (float)cityIcon.CityIndex * ConfigPrefsController.colonyIncomeMultiplier) / 5f);
				int num2 = Convert.ToInt32(Math.Floor(num));
				if (num2 < 1)
				{
					num2 = 1;
				}
				textIncomeOld[i].text = "+ " + num2.ToString("###,###,##0") + " / " + ScriptLocalization.Get("NORMAL/minute").ToUpper();
			}
		}
		if (PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] < 4)
		{
			float num3 = (float)(PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] + 2) * ((5f + (float)cityIcon.CityIndex * ConfigPrefsController.colonyIncomeMultiplier) / 5f);
			int num4 = Convert.ToInt32(Math.Floor(num3));
			if (num4 < 1)
			{
				num4 = 1;
			}
			textIncomeNew[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] + 1].text = "+ " + num4.ToString("###,###,##0") + " / " + ScriptLocalization.Get("NORMAL/minute").ToUpper();
			upgradeCost = cityIcon.CityIndex * ConfigPrefsController.upgradePriceColonyMultiplier[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex]];
			textCost[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] + 1].text = upgradeCost.ToString();
			if (PlayerPrefs.GetFloat("playerMoney") >= (float)upgradeCost)
			{
				buttonUpgrade[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] + 1].interactable = true;
			}
			else if (PlayerPrefs.GetFloat("playerMoney") < (float)upgradeCost)
			{
				buttonUpgrade[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] + 1].interactable = false;
			}
		}
		else
		{
			upgradeCost = -1;
			buttonUpgrade[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex]].interactable = false;
		}
		if (_updateCityIcon)
		{
			cityIcon.SetStatus();
		}
	}

	public void Cities()
	{
		for (int i = 0; i < cityOff.Length; i++)
		{
			upgradeCity[i].SetActive(value: false);
			if (PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] + 1 < i)
			{
				cityOff[i].SetActive(value: true);
			}
			if (PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] >= i)
			{
				cityUnlocked[i].SetActive(value: true);
				cityOff[i].SetActive(value: false);
			}
			else
			{
				cityUnlocked[i].SetActive(value: false);
			}
		}
		if (PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] < 4)
		{
			upgradeCity[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] + 1].SetActive(value: true);
			cityOff[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] + 1].SetActive(value: true);
		}
	}

	private void UnlockAnimation()
	{
		unlockedEffect[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex] + 1].SetActive(value: true);
		LeanTween.delayedCall(unlockedEffect[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex]], 0.5f, (Action)delegate
		{
			unlockedEffect[PlayerPrefsController.CitiesLevel[cityIcon.CityIndex]].SetActive(value: false);
		});
	}
}
