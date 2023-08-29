using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class UICatapultMenu : MonoBehaviour
{
	public Image[] ammoLevel;

	public Text upgradeCatapultTextSmallLvl;

	public Text upgradeCatapultTextBigLvl;

	public Text upgradeCatapultTextSmallDamageOld;

	public Text upgradeCatapultTextSmallDamageNew;

	public Text upgradeCatapultTextBigDamageOld;

	public Text upgradeCatapultTextBigDamageNew;

	public Button upgradeCatapultButtonBigSelect;

	public Button upgradeCatapultButtonSmallSelect;

	public Button upgradeCatapultButtonBigLvl;

	public Button upgradeCatapultButtonSmallLvl;

	public Text upgradeCatapultTextSmallPrice;

	public Text upgradeCatapultTextBigPrice;

	private SfxUIController sfxUIController;

	private UpgradesController upgradesController;

	private UIController uiController;

	private int lastCatapultIndexSelected;

	private int catapultSlotSelected;

	public int LastCatapultIndexSelected => lastCatapultIndexSelected;

	private void Start()
	{
	}

	public void Initialize(int catapultSlotSelected, UIController uiController, SfxUIController sfxUIController, UpgradesController upgradesController)
	{
		this.catapultSlotSelected = catapultSlotSelected;
		this.uiController = uiController;
		this.upgradesController = upgradesController;
		this.sfxUIController = sfxUIController;
		UpdateBars();
		UpdateUIUpgradeCatapultAmmo(catapultSlotSelected);
	}

	public void ButtonClose()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		uiController.BackFromUpgradeWindow();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void ButtonPressUpgradeCatapultAmmoSelect(int _catapultAmmo)
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		PlayerPrefsController.CatapultAmmo[catapultSlotSelected] = (CatapultAmmoType)_catapultAmmo;
		UpdateUIUpgradeCatapultAmmo(catapultSlotSelected);
		upgradesController.SetInitialStructure(_setHeroes: false);
		uiController.buttonUpgradeCatapult[catapultSlotSelected].GetComponent<Image>().sprite = uiController.spriteCatapultAmmo[_catapultAmmo];
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
	}

	public void ButtonPressUpgradeCatapultAmmoBig()
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)PlayerPrefsController.upgradeCatapultAmmoBigPrices[PlayerPrefsController.CatapultAmmoBigLvl]);
		PlayerPrefsController.CatapultAmmoBigLvl++;
		UpdateUIUpgradeCatapultAmmo(catapultSlotSelected);
		uiController.UpdateLevelBars(ammoLevel[0], PlayerPrefsController.CatapultAmmoBigLvl, ConfigPrefsController.upgradeCatapultAmmoBigMax);
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
	}

	public void ButtonPressUpgradeCatapultAmmoSmall()
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)PlayerPrefsController.upgradeCatapultAmmoSmallPrices[PlayerPrefsController.CatapultAmmoSmallLvl]);
		PlayerPrefsController.CatapultAmmoSmallLvl++;
		UpdateUIUpgradeCatapultAmmo(catapultSlotSelected);
		uiController.UpdateLevelBars(ammoLevel[1], PlayerPrefsController.CatapultAmmoSmallLvl, ConfigPrefsController.upgradeCatapultAmmoSmallMax);
		PlayerPrefsController.SaveBoughtBuilding(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
	}

	public void UpdateUIUpgradeCatapultAmmo(int _catapultIndex)
	{
		lastCatapultIndexSelected = _catapultIndex;
		upgradeCatapultTextSmallLvl.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.CatapultAmmoSmallLvl.ToString();
		upgradeCatapultTextBigLvl.text = ScriptLocalization.Get("NORMAL/level").ToUpper() + " " + PlayerPrefsController.CatapultAmmoBigLvl.ToString();
		int num = 0;
		if (PlayerPrefsController.CatapultAmmoSmallLvl < ConfigPrefsController.upgradeCatapultAmmoSmallMax)
		{
			num = PlayerPrefsController.upgradeCatapultAmmoSmallPrices[PlayerPrefsController.CatapultAmmoSmallLvl];
		}
		int num2 = 0;
		if (PlayerPrefsController.CatapultAmmoBigLvl < ConfigPrefsController.upgradeCatapultAmmoBigMax)
		{
			num2 = PlayerPrefsController.upgradeCatapultAmmoBigPrices[PlayerPrefsController.CatapultAmmoBigLvl];
		}
		float num3 = ConfigPrefsController.damageCatapultAmmoSmallBase + (float)PlayerPrefsController.CatapultAmmoSmallLvl * ConfigPrefsController.damageCatapultAmmoSmallPerLevel;
		num3 += num3 * ConfigPrefsController.generalBaseCatapultDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_CatapultDamage;
		upgradeCatapultTextSmallDamageOld.text = num3.ToString("###,###,###.#");
		if (PlayerPrefsController.CatapultAmmoSmallLvl < ConfigPrefsController.upgradeCatapultAmmoSmallMax)
		{
			float num4 = ConfigPrefsController.damageCatapultAmmoSmallBase + (float)(PlayerPrefsController.CatapultAmmoSmallLvl + 1) * ConfigPrefsController.damageCatapultAmmoSmallPerLevel;
			num4 += num4 * ConfigPrefsController.generalBaseCatapultDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_CatapultDamage;
			upgradeCatapultTextSmallDamageNew.text = num4.ToString("###,###,###.#");
			upgradeCatapultTextSmallPrice.text = num.ToString("###,###,###");
		}
		else
		{
			upgradeCatapultTextSmallDamageNew.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
			upgradeCatapultTextSmallPrice.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
		}
		num3 = ConfigPrefsController.damageCatapultAmmoBigBase + (float)PlayerPrefsController.CatapultAmmoBigLvl * ConfigPrefsController.damageCatapultAmmoBigPerLevel;
		num3 += num3 * ConfigPrefsController.generalBaseCatapultDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_CatapultDamage;
		upgradeCatapultTextBigDamageOld.text = num3.ToString("###,###,###.#");
		if (PlayerPrefsController.CatapultAmmoBigLvl < ConfigPrefsController.upgradeCatapultAmmoBigMax)
		{
			float num5 = ConfigPrefsController.damageCatapultAmmoBigBase + (float)(PlayerPrefsController.CatapultAmmoBigLvl + 1) * ConfigPrefsController.damageCatapultAmmoBigPerLevel;
			num5 += num5 * ConfigPrefsController.generalBaseCatapultDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_CatapultDamage;
			upgradeCatapultTextBigDamageNew.text = num5.ToString("###,###,###.#");
			upgradeCatapultTextBigPrice.text = num2.ToString("###,###,###");
		}
		else
		{
			upgradeCatapultTextBigDamageNew.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
			upgradeCatapultTextBigPrice.text = ScriptLocalization.Get("NORMAL/max_big").ToUpper();
		}
		if (PlayerPrefsController.CatapultAmmoSmallLvl < ConfigPrefsController.upgradeCatapultAmmoBigMax && PlayerPrefs.GetFloat("playerMoney") >= (float)num)
		{
			upgradeCatapultButtonSmallLvl.interactable = true;
		}
		else
		{
			upgradeCatapultButtonSmallLvl.interactable = false;
		}
		if (PlayerPrefsController.CatapultAmmoBigLvl < ConfigPrefsController.upgradeCatapultAmmoSmallMax && PlayerPrefs.GetFloat("playerMoney") >= (float)num2)
		{
			upgradeCatapultButtonBigLvl.interactable = true;
		}
		else
		{
			upgradeCatapultButtonBigLvl.interactable = false;
		}
		if (PlayerPrefsController.CatapultAmmo[_catapultIndex] == CatapultAmmoType.Big)
		{
			upgradeCatapultButtonBigSelect.interactable = false;
			upgradeCatapultButtonSmallSelect.interactable = true;
		}
		else
		{
			upgradeCatapultButtonBigSelect.interactable = true;
			upgradeCatapultButtonSmallSelect.interactable = false;
		}
	}

	private void UpdateBars()
	{
		uiController.UpdateLevelBars(ammoLevel[0], PlayerPrefsController.CatapultAmmoBigLvl, ConfigPrefsController.upgradeCatapultAmmoBigMax);
		uiController.UpdateLevelBars(ammoLevel[1], PlayerPrefsController.CatapultAmmoSmallLvl, ConfigPrefsController.upgradeCatapultAmmoSmallMax);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ButtonClose();
		}
	}
}
