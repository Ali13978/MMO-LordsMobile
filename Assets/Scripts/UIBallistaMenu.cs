using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class UIBallistaMenu : MonoBehaviour
{
	public Image[] ammoLevel;

	public Text upgradeTowerTextSmallLvl;

	public Text upgradeTowerTextBigLvl;

	public Text upgradeTowerTextSmallDamageOld;

	public Text upgradeTowerTextSmallDamageNew;

	public Text upgradeTowerTextBigDamageOld;

	public Text upgradeTowerTextBigDamageNew;

	public Button upgradeTowerButtonBigSelect;

	public Button upgradeTowerButtonSmallSelect;

	public Button upgradeTowerButtonBigLvl;

	public Button upgradeTowerButtonSmallLvl;

	public Text upgradeTowerTextSmallPrice;

	public Text upgradeTowerTextBigPrice;

	private int lastTowerIndexSelected;

	private int towerSlotSelected;

	private SfxUIController sfxUIController;

	private UpgradesController upgradesController;

	private UIController uiController;

	public int LastTowerIndexSelected => lastTowerIndexSelected;

	private void Start()
	{
	}

	public void Initialize(int towerSlotSelected, UIController uiController, SfxUIController sfxUIController, UpgradesController upgradesController)
	{
		this.towerSlotSelected = towerSlotSelected;
		this.uiController = uiController;
		this.upgradesController = upgradesController;
		this.sfxUIController = sfxUIController;
		UpdateBars();
		UpdateUIUpgradeTowerAmmo(towerSlotSelected);
	}

	public void ButtonClose()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		uiController.BackFromUpgradeWindow();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void ButtonPressUpgradeTowerAmmoSelect(int _towerAmmo)
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		PlayerPrefsController.TowerAmmo[towerSlotSelected] = (TowerAmmoType)_towerAmmo;
		UpdateUIUpgradeTowerAmmo(towerSlotSelected);
		upgradesController.SetInitialStructure(_setHeroes: false);
		uiController.buttonUpgradeTower[towerSlotSelected].GetComponent<Image>().sprite = uiController.spriteTowerAmmo[_towerAmmo];
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
	}

	public void ButtonPressUpgradeBallistaAmmoBig()
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)PlayerPrefsController.upgradeTowerAmmoBigPrices[PlayerPrefsController.TowerAmmoBigLvl]);
		PlayerPrefsController.TowerAmmoBigLvl++;
		UpdateUIUpgradeTowerAmmo(towerSlotSelected);
		uiController.UpdateLevelBars(ammoLevel[0], PlayerPrefsController.TowerAmmoBigLvl, ConfigPrefsController.upgradeTowerAmmoBigMax);
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
	}

	public void ButtonPressUpgradeBallistaAmmoSmall()
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		uiController.UpdateLevelBars(ammoLevel[1], PlayerPrefsController.TowerAmmoSmallLvl, ConfigPrefsController.upgradeTowerAmmoSmallMax);
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)PlayerPrefsController.upgradeTowerAmmoSmallPrices[PlayerPrefsController.TowerAmmoSmallLvl]);
		PlayerPrefsController.TowerAmmoSmallLvl++;
		UpdateUIUpgradeTowerAmmo(towerSlotSelected);
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
	}

	public void UpdateUIUpgradeTowerAmmo(int _towerIndex)
	{
		lastTowerIndexSelected = _towerIndex;
		upgradeTowerTextSmallLvl.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.TowerAmmoSmallLvl.ToString();
		upgradeTowerTextBigLvl.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.TowerAmmoBigLvl.ToString();
		int num = 0;
		if (PlayerPrefsController.TowerAmmoSmallLvl < ConfigPrefsController.upgradeTowerAmmoSmallMax)
		{
			num = PlayerPrefsController.upgradeTowerAmmoSmallPrices[PlayerPrefsController.TowerAmmoSmallLvl];
		}
		int num2 = 0;
		if (PlayerPrefsController.TowerAmmoBigLvl < ConfigPrefsController.upgradeTowerAmmoBigMax)
		{
			num2 = PlayerPrefsController.upgradeTowerAmmoBigPrices[PlayerPrefsController.TowerAmmoBigLvl];
		}
		float num3 = ConfigPrefsController.damageTowerAmmoSmallBase + (float)PlayerPrefsController.TowerAmmoSmallLvl * ConfigPrefsController.damageTowerAmmoSmallPerLevel;
		num3 += num3 * ConfigPrefsController.generalBaseTowerDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_TowerDamage;
		upgradeTowerTextSmallDamageOld.text = num3.ToString("###,###,###.#");
		if (PlayerPrefsController.TowerAmmoSmallLvl < ConfigPrefsController.upgradeTowerAmmoSmallMax)
		{
			float num4 = ConfigPrefsController.damageTowerAmmoSmallBase + (float)(PlayerPrefsController.TowerAmmoSmallLvl + 1) * ConfigPrefsController.damageTowerAmmoSmallPerLevel;
			num4 += num4 * ConfigPrefsController.generalBaseTowerDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_TowerDamage;
			upgradeTowerTextSmallDamageNew.text = num4.ToString("###,###,###.#");
			upgradeTowerTextSmallPrice.text = num.ToString("###,###,###");
		}
		else
		{
			upgradeTowerTextSmallDamageNew.text = "MAX";
			upgradeTowerTextSmallPrice.text = "MAX";
		}
		num3 = ConfigPrefsController.damageTowerAmmoBigBase + (float)PlayerPrefsController.TowerAmmoBigLvl * ConfigPrefsController.damageTowerAmmoBigPerLevel;
		num3 += num3 * ConfigPrefsController.generalBaseTowerDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_TowerDamage;
		upgradeTowerTextBigDamageOld.text = num3.ToString("###,###,###.##");
		if (PlayerPrefsController.TowerAmmoBigLvl < ConfigPrefsController.upgradeTowerAmmoBigMax)
		{
			float num5 = ConfigPrefsController.damageTowerAmmoBigBase + (float)(PlayerPrefsController.TowerAmmoBigLvl + 1) * ConfigPrefsController.damageTowerAmmoBigPerLevel;
			num5 += num5 * ConfigPrefsController.generalBaseTowerDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_TowerDamage;
			upgradeTowerTextBigDamageNew.text = num5.ToString("###,###,###.##");
			upgradeTowerTextBigPrice.text = num2.ToString("###,###,###");
		}
		else
		{
			upgradeTowerTextBigDamageNew.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
			upgradeTowerTextBigPrice.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
		}
		if (PlayerPrefsController.TowerAmmoSmallLvl < ConfigPrefsController.upgradeTowerAmmoSmallMax && PlayerPrefs.GetFloat("playerMoney") >= (float)num)
		{
			upgradeTowerButtonSmallLvl.interactable = true;
		}
		else
		{
			upgradeTowerButtonSmallLvl.interactable = false;
		}
		if (PlayerPrefsController.TowerAmmoBigLvl < ConfigPrefsController.upgradeTowerAmmoBigMax && PlayerPrefs.GetFloat("playerMoney") >= (float)num2)
		{
			upgradeTowerButtonBigLvl.interactable = true;
		}
		else
		{
			upgradeTowerButtonBigLvl.interactable = false;
		}
		if (PlayerPrefsController.TowerAmmo[_towerIndex] == TowerAmmoType.Big)
		{
			upgradeTowerButtonBigSelect.interactable = false;
			upgradeTowerButtonSmallSelect.interactable = true;
		}
		else
		{
			upgradeTowerButtonBigSelect.interactable = true;
			upgradeTowerButtonSmallSelect.interactable = false;
		}
	}

	private void UpdateBars()
	{
		uiController.UpdateLevelBars(ammoLevel[0], PlayerPrefsController.TowerAmmoBigLvl, ConfigPrefsController.upgradeTowerAmmoBigMax);
		uiController.UpdateLevelBars(ammoLevel[1], PlayerPrefsController.TowerAmmoSmallLvl, ConfigPrefsController.upgradeTowerAmmoSmallMax);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ButtonClose();
		}
	}
}
