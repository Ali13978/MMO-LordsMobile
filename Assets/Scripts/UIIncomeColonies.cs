using System;
using UnityEngine;
using UnityEngine.UI;

public class UIIncomeColonies : MonoBehaviour
{
	private float timeHackLimitFlag = 10800f;

	private float timeHackLimit;

	public Text textIncome;

	public Text textNoIncome;

	public Image imageIncome;

	private int incomeColonies;

	public bool isMapScene;

	private UIController uiController;

	private UIMapController uiMapController;

	public UIWindowUpgradeColony uiWindowUpgradeColony;

	public UIWindowAttack uiWindowAttack;

	public GameObject objectUpgradeMenu;

	public GameObject withIncomePanel;

	public GameObject noIncomePanel;

	private void Awake()
	{
		if (isMapScene)
		{
			uiMapController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIMapController>();
		}
		else
		{
			uiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
		}
	}

	private void Start()
	{
		CalculateIncome();
		if (incomeColonies <= 0)
		{
			base.gameObject.SetActive(value: false);
			withIncomePanel.SetActive(value: false);
			noIncomePanel.SetActive(value: true);
			textNoIncome.text = Mathf.FloorToInt(PlayerPrefs.GetFloat("playerMoney")).ToString("###,###,##0");
		}
		else
		{
			base.gameObject.SetActive(value: true);
			withIncomePanel.SetActive(value: true);
			noIncomePanel.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (timeHackLimit < timeHackLimitFlag)
		{
			PlayerPrefsController.timeIncomeCounter += Time.unscaledDeltaTime;
			if (PlayerPrefsController.timeIncomeCounter >= ConfigPrefsController.colonyIncomeTime)
			{
				PlayerPrefsController.timeIncomeCounter = 0f;
				CalculateIncome();
				PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") + (float)incomeColonies);
				if (isMapScene)
				{
					uiMapController.UpdateTopStats();
				}
				PlayerPrefsController.SaveIncomeColony(PlayerPrefs.GetFloat("playerMoney"));
				UpdateUINewIncome();
			}
		}
		if (Time.timeScale > 0f)
		{
			timeHackLimit += Time.unscaledDeltaTime / Time.timeScale;
		}
		imageIncome.fillAmount = PlayerPrefsController.timeIncomeCounter / ConfigPrefsController.colonyIncomeTime;
	}

	public void ResetHackControl()
	{
		timeHackLimit = 0f;
	}

	public void CalculateIncome()
	{
		float num = 0f;
		for (int i = 1; i < PlayerPrefsController.CitiesConquered.Length; i++)
		{
			if (PlayerPrefsController.CitiesConquered[i])
			{
				float num2 = (float)(PlayerPrefsController.CitiesLevel[i] + 1) * ((5f + (float)i * ConfigPrefsController.colonyIncomeMultiplier) / 5f);
				int num3 = Convert.ToInt32(Math.Floor(num2));
				if (num3 < 1)
				{
					num3 = 1;
				}
				num += (float)num3;
			}
		}
		incomeColonies = Mathf.FloorToInt(num);
		incomeColonies += Mathf.FloorToInt((float)incomeColonies * ConfigPrefsController.generalBaseIncomeColonyPerLevel * (float)PlayerPrefsController.GeneralTechBase_IncomeColony);
		if (incomeColonies > 0)
		{
			textIncome.text = "+ " + incomeColonies.ToString("###,###,##0");
		}
	}

	private void UpdateUINewIncome()
	{
		if (isMapScene)
		{
			if (uiWindowUpgradeColony.gameObject.activeInHierarchy)
			{
				uiWindowUpgradeColony.UpdateWindow(_updateCityIcon: false);
			}
			if (uiWindowAttack.gameObject.activeInHierarchy)
			{
				uiWindowAttack.UpdateJustAttackButton();
			}
		}
		else if (MainController.worldScreen == WorldScreen.Upgrade)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas_Ballista");
			UIBallistaMenu uIBallistaMenu = null;
			if (gameObject != null)
			{
				uIBallistaMenu = gameObject.GetComponent<UIBallistaMenu>();
			}
			GameObject gameObject2 = GameObject.FindGameObjectWithTag("Canvas_Catapult");
			UICatapultMenu uICatapultMenu = null;
			if (gameObject2 != null)
			{
				uICatapultMenu = gameObject2.GetComponent<UICatapultMenu>();
			}
			GameObject gameObject3 = GameObject.FindGameObjectWithTag("Canvas_Hero");
			UIHeroesMenu uIHeroesMenu = null;
			if (gameObject3 != null)
			{
				uIHeroesMenu = gameObject3.GetComponent<UIHeroesMenu>();
			}
			GameObject gameObject4 = GameObject.FindGameObjectWithTag("Canvas_Units");
			UIUnitsTechnology uIUnitsTechnology = null;
			if (gameObject4 != null)
			{
				uIUnitsTechnology = gameObject4.GetComponent<UIUnitsTechnology>();
			}
			if (objectUpgradeMenu.activeInHierarchy)
			{
				uiController.UpdateUIUpgrade();
			}
			else if (gameObject != null)
			{
				uIBallistaMenu.UpdateUIUpgradeTowerAmmo(uIBallistaMenu.LastTowerIndexSelected);
			}
			else if (gameObject2 != null)
			{
				uICatapultMenu.UpdateUIUpgradeCatapultAmmo(uICatapultMenu.LastCatapultIndexSelected);
			}
			else if (gameObject3 != null)
			{
				uIHeroesMenu.UpdateUIUpgradeHeroe(uIHeroesMenu.LastHeroIndexSelected);
			}
			else if (gameObject4 != null)
			{
				uIUnitsTechnology.UpdateDescription();
			}
		}
	}
}
