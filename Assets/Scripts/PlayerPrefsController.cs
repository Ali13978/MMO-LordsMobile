using System;
using UnityEngine;

public static class PlayerPrefsController
{
	private static bool resetGame = false;

	public static bool forceTutorialDone = false;

	public static bool isSfx;

	public static bool isMusic;

	public static bool isFastForward;

	public static bool halfWave;

	public static bool halfLife;

	public static int playerLevel;

	public static float playerExp;

	public static int[] playerExpLevels;

	public static int WallLvl;

	public static int ArchersLvl;

	public static int BankLvl;

	public static int[] upgradeWallPrices;

	public static int[] upgradeArcherPrices;

	public static int[] upgradeTowerPrices;

	public static int[] upgradeCatapultPrices;

	public static int[] upgradeTowerAmmoSmallPrices;

	public static int[] upgradeTowerAmmoBigPrices;

	public static int[] upgradeCatapultAmmoSmallPrices;

	public static int[] upgradeCatapultAmmoBigPrices;

	public static int[] upgradeHeroPrices;

	public static HeroeType[] HeroeSelected = new HeroeType[2];

	public static int[] HeroeLvl = new int[4];

	public static TowerAmmoType[] TowerAmmo = new TowerAmmoType[6];

	public static int TowerLvl;

	public static int TowerAmmoSmallLvl;

	public static int TowerAmmoBigLvl;

	public static CatapultAmmoType[] CatapultAmmo = new CatapultAmmoType[2];

	public static int CatapultLvl;

	public static int CatapultAmmoSmallLvl;

	public static int CatapultAmmoBigLvl;

	public static int[] UnitsTechSpear = new int[7];

	public static int[] UnitsTechMelee = new int[10];

	public static int[] UnitsTechRanged = new int[5];

	public static int[] UnitsTechMounted = new int[4];

	public static int[] UnitsTechSiege = new int[4];

	public static bool[] UnitsTechMercenary = new bool[9];

	public static int GeneralTechBase_TowerDamage;

	public static int GeneralTechBase_CatapultDamage;

	public static int GeneralTechBase_ArchersRange;

	public static int GeneralTechBase_ArchersDamage;

	public static int GeneralTechBase_WallHp;

	public static int GeneralTechBase_IncomeColony;

	public static int GeneralTechBase_IncomeKill;

	public static int GeneralTechBase_ExperienceKill;

	public static int GeneralTechArmy_MeleeDamage;

	public static int GeneralTechArmy_RangedDamage;

	public static int GeneralTechArmy_SiegeDamage;

	public static int GeneralTechArmy_SiegeHealth;

	public static int GeneralTechArmy_SpawnCooldown;

	public static int GeneralTechArmy_HeroHealth;

	public static int GeneralTechArmy_HeroDamage;

	public static int GeneralTechArmy_MercenaryCost;

	public static int GeneralTechPowers_WarCry;

	public static int GeneralTechPowers_CatapultsShower;

	public static int GeneralTechPowers_ArchersShower;

	public static bool[] CitiesConquered = new bool[100];

	public static int[] CitiesLevel = new int[100];

	public static bool[] tutorialSteps = new bool[28];

	public static bool tutorialSpecialHero;

	public static bool tutorialSpecialMercenary;

	public static bool tutorialSpecialArmy;

	public static bool tutorialSpecialElephants;

	public static float timeIncomeCounter;

	public static float cameraX;

	public static float cameraY;

	public static void LoadInitial(bool _shouldReset)
	{
		if (resetGame)
		{
			NukeData();
		}
		else if (_shouldReset)
		{
			ResetStats();
		}
		if (playerExpLevels == null)
		{
			playerExpLevels = new int[ConfigPrefsController.playerExperienceMaxLevel];
			playerExpLevels[0] = 0;
			playerExpLevels[1] = (int)ConfigPrefsController.playerExperienceBase;
			for (int i = 2; i < playerExpLevels.Length; i++)
			{
				playerExpLevels[i] = playerExpLevels[i - 1] + (int)((float)(i + 1) * ConfigPrefsController.playerExperienceMultiplier);
			}
			upgradeWallPrices = new int[ConfigPrefsController.upgradeWallMax];
			upgradeWallPrices[0] = 0;
			upgradeWallPrices[1] = ConfigPrefsController.upgradePriceWallBase;
			for (int j = 2; j < upgradeWallPrices.Length; j++)
			{
				upgradeWallPrices[j] = upgradeWallPrices[j - 1] + (int)((float)(j + 1) * ConfigPrefsController.upgradePriceWallMultiplier);
			}
			upgradeArcherPrices = new int[ConfigPrefsController.upgradeArchersMax];
			upgradeArcherPrices[0] = ConfigPrefsController.upgradePriceArchersBase;
			for (int k = 1; k < upgradeArcherPrices.Length; k++)
			{
				upgradeArcherPrices[k] = upgradeArcherPrices[k - 1] + (int)((float)(k + 1) * ConfigPrefsController.upgradePriceArchersMultiplier);
			}
			upgradeTowerPrices = new int[ConfigPrefsController.upgradeTowerMax];
			upgradeTowerPrices[0] = 0;
			upgradeTowerPrices[1] = ConfigPrefsController.upgradePriceTowerBase;
			for (int l = 2; l < upgradeTowerPrices.Length; l++)
			{
				upgradeTowerPrices[l] = upgradeTowerPrices[l - 1] + (int)((float)(l + 1) * ConfigPrefsController.upgradePriceTowerMultiplier);
			}
			upgradeCatapultPrices = new int[ConfigPrefsController.upgradeCatapultMax];
			upgradeCatapultPrices[0] = 0;
			upgradeCatapultPrices[1] = ConfigPrefsController.upgradePriceCatapultBase;
			for (int m = 2; m < upgradeCatapultPrices.Length; m++)
			{
				upgradeCatapultPrices[m] = upgradeCatapultPrices[m - 1] + (int)((float)(m + 1) * ConfigPrefsController.upgradePriceCatapultMultiplier);
			}
			upgradeTowerAmmoSmallPrices = new int[ConfigPrefsController.upgradeTowerAmmoSmallMax];
			upgradeTowerAmmoSmallPrices[0] = 0;
			upgradeTowerAmmoSmallPrices[1] = ConfigPrefsController.upgradePriceTowerAmmoSmallBase;
			for (int n = 2; n < upgradeTowerAmmoSmallPrices.Length; n++)
			{
				upgradeTowerAmmoSmallPrices[n] = upgradeTowerAmmoSmallPrices[n - 1] + (int)((float)(n + 1) * ConfigPrefsController.upgradePriceTowerAmmoSmallMultiplier);
			}
			upgradeTowerAmmoBigPrices = new int[ConfigPrefsController.upgradeTowerAmmoBigMax];
			upgradeTowerAmmoBigPrices[0] = 0;
			upgradeTowerAmmoBigPrices[1] = ConfigPrefsController.upgradePriceTowerAmmoBigBase;
			for (int num = 2; num < upgradeTowerAmmoBigPrices.Length; num++)
			{
				upgradeTowerAmmoBigPrices[num] = upgradeTowerAmmoBigPrices[num - 1] + (int)((float)(num + 1) * ConfigPrefsController.upgradePriceTowerAmmoBigMultiplier);
			}
			upgradeCatapultAmmoSmallPrices = new int[ConfigPrefsController.upgradeCatapultAmmoSmallMax];
			upgradeCatapultAmmoSmallPrices[0] = 0;
			upgradeCatapultAmmoSmallPrices[1] = ConfigPrefsController.upgradePriceCatapultAmmoSmallBase;
			for (int num2 = 2; num2 < upgradeCatapultAmmoSmallPrices.Length; num2++)
			{
				upgradeCatapultAmmoSmallPrices[num2] = upgradeCatapultAmmoSmallPrices[num2 - 1] + (int)((float)(num2 + 1) * ConfigPrefsController.upgradePriceCatapultAmmoSmallMultiplier);
			}
			upgradeCatapultAmmoBigPrices = new int[ConfigPrefsController.upgradeCatapultAmmoBigMax];
			upgradeCatapultAmmoBigPrices[0] = 0;
			upgradeCatapultAmmoBigPrices[1] = ConfigPrefsController.upgradePriceCatapultAmmoBigBase;
			for (int num3 = 2; num3 < upgradeCatapultAmmoBigPrices.Length; num3++)
			{
				upgradeCatapultAmmoBigPrices[num3] = upgradeCatapultAmmoBigPrices[num3 - 1] + (int)((float)(num3 + 1) * ConfigPrefsController.upgradePriceCatapultAmmoBigMultiplier);
			}
			upgradeHeroPrices = new int[ConfigPrefsController.upgradeHeroMax];
			upgradeHeroPrices[0] = 0;
			upgradeHeroPrices[1] = ConfigPrefsController.upgradePriceHeroBase;
			for (int num4 = 2; num4 < upgradeHeroPrices.Length; num4++)
			{
				upgradeHeroPrices[num4] = upgradeHeroPrices[num4 - 1] + (int)((float)(num4 + 1) * ConfigPrefsController.upgradePriceHeroMultiplier);
			}
		}
		for (int num5 = 0; num5 < tutorialSteps.Length; num5++)
		{
			if (PlayerPrefs.GetInt("tutorialStep_" + num5.ToString(), 0) == 0)
			{
				tutorialSteps[num5] = false;
				if (forceTutorialDone)
				{
					tutorialSteps[num5] = true;
				}
			}
			else
			{
				tutorialSteps[num5] = true;
			}
		}
		if (PlayerPrefs.GetInt("tutorialSpecialHero", 0) == 0)
		{
			tutorialSpecialHero = false;
		}
		else
		{
			tutorialSpecialHero = true;
		}
		if (PlayerPrefs.GetInt("tutorialSpecialMercenary", 0) == 0)
		{
			tutorialSpecialMercenary = false;
		}
		else
		{
			tutorialSpecialMercenary = true;
		}
		if (PlayerPrefs.GetInt("tutorialSpecialArmy", 0) == 0)
		{
			tutorialSpecialArmy = false;
		}
		else
		{
			tutorialSpecialArmy = true;
		}
		if (PlayerPrefs.GetInt("tutorialSpecialElephants", 0) == 0)
		{
			tutorialSpecialElephants = false;
		}
		else
		{
			tutorialSpecialElephants = true;
		}
		if (PlayerPrefs.GetInt("isSpeedFast", 0) == 0)
		{
			isFastForward = false;
		}
		else
		{
			isFastForward = true;
		}
		if (PlayerPrefs.GetInt("isSfx", 1) == 0)
		{
			isSfx = false;
		}
		else
		{
			isSfx = true;
		}
		if (PlayerPrefs.GetInt("isMusic", 1) == 0)
		{
			isMusic = false;
		}
		else
		{
			isMusic = true;
		}
		if (!PlayerPrefs.HasKey("playerMoney"))
		{
			PlayerPrefs.SetFloat("playerMoney", 14f);
			PlayerPrefs.SetInt("playerRubies", 10);
			PlayerPrefs.SetInt("playerWave", 1);
			PlayerPrefs.SetInt("vipUser", 0);
			PlayerPrefs.SetInt("hasBoughtMoneyBoost", 0);
			PlayerPrefs.SetInt("hasBoughtXpBoost", 0);
			PlayerPrefs.SetInt("hasBoughtPowersBoost", 0);
			PlayerPrefs.SetInt("hasBoughtVipBoost", 0);
			PlayerPrefs.SetInt("hasBoughtBoosters", 0);
			PlayerPrefs.SetInt("bankTutorialDone", 0);
			PlayerPrefs.SetInt("gotFreeGems", 0);
		}
		if (!PlayerPrefs.HasKey("firstServerRequest"))
		{
			PlayerPrefs.SetInt("firstServerRequest", 0);
		}
		playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
		playerExp = PlayerPrefs.GetFloat("playerExp", 5f);
		WallLvl = PlayerPrefs.GetInt("wallLvl", 0);
		ArchersLvl = PlayerPrefs.GetInt("archersLvl", 0);
		BankLvl = PlayerPrefs.GetInt("bankLvl", -1);
		HeroeSelected[0] = (HeroeType)PlayerPrefs.GetInt("heroSelected_0", 0);
		HeroeSelected[1] = (HeroeType)PlayerPrefs.GetInt("heroSelected_1", 0);
		HeroeLvl[0] = PlayerPrefs.GetInt("heroLevel_0", 0);
		HeroeLvl[1] = PlayerPrefs.GetInt("heroLevel_1", 0);
		HeroeLvl[2] = PlayerPrefs.GetInt("heroLevel_2", 0);
		HeroeLvl[3] = PlayerPrefs.GetInt("heroLevel_3", 0);
		TowerAmmo[0] = (TowerAmmoType)PlayerPrefs.GetInt("towerAmmo_0", 0);
		TowerAmmo[1] = (TowerAmmoType)PlayerPrefs.GetInt("towerAmmo_1", 0);
		TowerAmmo[2] = (TowerAmmoType)PlayerPrefs.GetInt("towerAmmo_2", 0);
		TowerAmmo[3] = (TowerAmmoType)PlayerPrefs.GetInt("towerAmmo_3", 0);
		TowerAmmo[4] = (TowerAmmoType)PlayerPrefs.GetInt("towerAmmo_4", 0);
		TowerAmmo[5] = (TowerAmmoType)PlayerPrefs.GetInt("towerAmmo_5", 0);
		TowerLvl = PlayerPrefs.GetInt("towerLvl", 1);
		TowerAmmoSmallLvl = PlayerPrefs.GetInt("towerAmmoSmallLvl", 1);
		TowerAmmoBigLvl = PlayerPrefs.GetInt("towerAmmoBigLvl", 1);
		CatapultAmmo[0] = (CatapultAmmoType)PlayerPrefs.GetInt("catapultAmmo_0", 0);
		CatapultAmmo[1] = (CatapultAmmoType)PlayerPrefs.GetInt("catapultAmmo_1", 0);
		CatapultLvl = PlayerPrefs.GetInt("catapultLvl", 1);
		CatapultAmmoSmallLvl = PlayerPrefs.GetInt("catapultAmmoSmallLvl", 1);
		CatapultAmmoBigLvl = PlayerPrefs.GetInt("catapultAmmoBigLvl", 1);
		GeneralTechBase_TowerDamage = PlayerPrefs.GetInt("generalTowerDamage", 0);
		GeneralTechBase_CatapultDamage = PlayerPrefs.GetInt("generalCatapultDamage", 0);
		GeneralTechBase_WallHp = PlayerPrefs.GetInt("generalWallHp", 0);
		GeneralTechBase_ArchersDamage = PlayerPrefs.GetInt("generalArchersDamage", 0);
		GeneralTechBase_ArchersRange = PlayerPrefs.GetInt("generalArchersRange", 0);
		GeneralTechBase_IncomeColony = PlayerPrefs.GetInt("generalIncomeColony", 0);
		GeneralTechBase_IncomeKill = PlayerPrefs.GetInt("generalIncomeKill", 0);
		GeneralTechBase_ExperienceKill = PlayerPrefs.GetInt("generalExperienceKill", 0);
		GeneralTechArmy_MeleeDamage = PlayerPrefs.GetInt("generalMeleeDamage", 0);
		GeneralTechArmy_RangedDamage = PlayerPrefs.GetInt("generalRangedDamage", 0);
		GeneralTechArmy_SiegeDamage = PlayerPrefs.GetInt("generalSiegeDamage", 0);
		GeneralTechArmy_SiegeHealth = PlayerPrefs.GetInt("generalSiegeHealth", 0);
		GeneralTechArmy_SpawnCooldown = PlayerPrefs.GetInt("generalSpawnCooldown", 0);
		GeneralTechArmy_HeroHealth = PlayerPrefs.GetInt("generalHeroHealth", 0);
		GeneralTechArmy_HeroDamage = PlayerPrefs.GetInt("generalHeroDamage", 0);
		GeneralTechArmy_MercenaryCost = PlayerPrefs.GetInt("generalMercenaryCost", 0);
		GeneralTechPowers_WarCry = PlayerPrefs.GetInt("generalPowersWarCry", 0);
		GeneralTechPowers_CatapultsShower = PlayerPrefs.GetInt("generalPowersCatapultsShower", 0);
		GeneralTechPowers_ArchersShower = PlayerPrefs.GetInt("generalPowersArchersShower", 0);
		for (int num6 = 0; num6 < UnitsTechSpear.Length; num6++)
		{
			UnitsTechSpear[num6] = PlayerPrefs.GetInt("unitsTechSpear_" + num6.ToString(), 0);
		}
		for (int num7 = 0; num7 < UnitsTechMelee.Length; num7++)
		{
			UnitsTechMelee[num7] = PlayerPrefs.GetInt("unitsTechMelee_" + num7.ToString(), 0);
		}
		for (int num8 = 0; num8 < UnitsTechMounted.Length; num8++)
		{
			UnitsTechMounted[num8] = PlayerPrefs.GetInt("unitsTechMounted_" + num8.ToString(), 0);
		}
		for (int num9 = 0; num9 < UnitsTechRanged.Length; num9++)
		{
			UnitsTechRanged[num9] = PlayerPrefs.GetInt("unitsTechRanged_" + num9.ToString(), 0);
		}
		for (int num10 = 0; num10 < UnitsTechSiege.Length; num10++)
		{
			UnitsTechSiege[num10] = PlayerPrefs.GetInt("unitsTechSiege_" + num10.ToString(), 0);
		}
		for (int num11 = 0; num11 < CitiesConquered.Length; num11++)
		{
			if (PlayerPrefs.GetInt("citiesConquered_" + num11.ToString(), 0) == 0)
			{
				CitiesConquered[num11] = false;
			}
			else
			{
				CitiesConquered[num11] = true;
			}
			CitiesLevel[num11] = PlayerPrefs.GetInt("citiesLevel_" + num11.ToString(), 0);
		}
		CitiesConquered[0] = true;
		for (int num12 = 0; num12 < UnitsTechMercenary.Length; num12++)
		{
			if (PlayerPrefs.GetInt("mercenary_" + num12.ToString(), 0) == 0)
			{
				UnitsTechMercenary[num12] = false;
			}
			else
			{
				UnitsTechMercenary[num12] = true;
			}
		}
	}

	public static void LoadMapCameraPosition(Transform pos)
	{
		float @float = PlayerPrefs.GetFloat("cameraX");
		float float2 = PlayerPrefs.GetFloat("cameraY");
		Vector3 vector2 = pos.localPosition = new Vector3(@float, float2, -10f);
	}

	public static int LoadAttackFormation(int _dataIndex, int _arrayIndex)
	{
		int result = -1;
		switch (_dataIndex)
		{
		case 0:
			result = PlayerPrefs.GetInt("attackFormation_Type_" + _arrayIndex, 0);
			break;
		case 1:
			result = PlayerPrefs.GetInt("attackFormation_Index_" + _arrayIndex, -1);
			break;
		}
		return result;
	}

	public static void SaveFinishedWave(float _money, int _expPoints, int _playerWave)
	{
		SavePlayerStats(_money, _expPoints, _playerWave);
		PlayerPrefs.Save();
	}

	public static void SaveBoughtBuilding(float _money, int _expPoints, int _playerWave)
	{
		SaveDefenceStats();
		SavePlayerStats(_money, _expPoints, _playerWave);
		PlayerPrefs.Save();
	}

	public static void SaveBoughtGeneralTech(float _money, int _expPoints, int _playerWave)
	{
		SaveGeneralBonus();
		SavePlayerStats(_money, _expPoints, _playerWave);
		PlayerPrefs.Save();
	}

	public static void SaveBoughtUnitTech(float _money, int _expPoints, int _playerWave)
	{
		SaveUnitsTech();
		SavePlayerStats(_money, _expPoints, _playerWave);
		PlayerPrefs.Save();
	}

	public static void SaveCitiesConquered(float _money, int _expPoints, int _playerWave)
	{
		SaveMap();
		SaveCharacters();
		SavePlayerStats(_money, _expPoints, _playerWave);
		PlayerPrefs.Save();
	}

	public static void SaveStartInvasion(float _money, int _expPoints, int _playerWave, ArmyType[] _unitTypeSelected, int[] _unitIndexSelected)
	{
		SavePlayerStats(_money, _expPoints, _playerWave);
		SaveAttackFormation(_unitTypeSelected, _unitIndexSelected);
		PlayerPrefs.Save();
	}

	public static void SaveIncomeColony(float _money)
	{
		SaveJustMoneyLocal(_money);
		PlayerPrefs.Save();
	}

	public static void SaveTutorial()
	{
		SaveJustTutorial();
		PlayerPrefs.Save();
	}

	public static void SaveSoundPrefs()
	{
		SaveAudio();
		PlayerPrefs.Save();
	}

	public static void SaveSpeedPrefs()
	{
		SaveSpeed();
		PlayerPrefs.Save();
	}

	public static void SaveJustMoney(float _money)
	{
		SaveJustMoneyLocal(_money);
		PlayerPrefs.Save();
	}

	public static void SaveMapCameraPosition(float x, float y)
	{
		PlayerPrefs.SetFloat("cameraX", x);
		PlayerPrefs.SetFloat("cameraY", y);
	}

	private static void ResetStats()
	{
		PlayerPrefs.DeleteKey("vipUser");
		PlayerPrefs.DeleteKey("playerRubies");
		PlayerPrefs.DeleteKey("bankLvl");
		PlayerPrefs.DeleteKey("moneyBoost");
		PlayerPrefs.DeleteKey("xpBoost");
		PlayerPrefs.DeleteKey("powerBoost");
		PlayerPrefs.DeleteKey("playerMoney");
		PlayerPrefs.DeleteKey("playerWave");
		PlayerPrefs.DeleteKey("playerExpPoints");
		PlayerPrefs.DeleteKey("ratedGame");
		PlayerPrefs.DeleteKey("playerLevel");
		PlayerPrefs.DeleteKey("playerExp");
		PlayerPrefs.DeleteKey("wallLvl");
		PlayerPrefs.DeleteKey("archersLvl");
		PlayerPrefs.DeleteKey("heroLevel_0");
		PlayerPrefs.DeleteKey("heroLevel_1");
		PlayerPrefs.DeleteKey("heroLevel_2");
		PlayerPrefs.DeleteKey("heroLevel_3");
		PlayerPrefs.DeleteKey("towerLvl");
		PlayerPrefs.DeleteKey("towerAmmoSmallLvl");
		PlayerPrefs.DeleteKey("towerAmmoBigLvl");
		PlayerPrefs.DeleteKey("catapultLvl");
		PlayerPrefs.DeleteKey("catapultAmmoSmallLvl");
		PlayerPrefs.DeleteKey("catapultAmmoBigLvl");
		for (int i = 0; i < TowerAmmo.Length; i++)
		{
			PlayerPrefs.DeleteKey("towerAmmo_" + i.ToString());
		}
		PlayerPrefs.DeleteKey("catapultAmmo_0");
		PlayerPrefs.DeleteKey("catapultAmmo_1");
		PlayerPrefs.DeleteKey("generalTowerDamage");
		PlayerPrefs.DeleteKey("generalCatapultDamage");
		PlayerPrefs.DeleteKey("generalArchersRange");
		PlayerPrefs.DeleteKey("generalArchersDamage");
		PlayerPrefs.DeleteKey("generalIncomeColony");
		PlayerPrefs.DeleteKey("generalIncomeKill");
		PlayerPrefs.DeleteKey("generalExperienceKill");
		PlayerPrefs.DeleteKey("generalMeleeDamage");
		PlayerPrefs.DeleteKey("generalRangedDamage");
		PlayerPrefs.DeleteKey("generalSiegeDamage");
		PlayerPrefs.DeleteKey("generalSiegeHealth");
		PlayerPrefs.DeleteKey("generalSpawnCooldown");
		PlayerPrefs.DeleteKey("generalHeroHealth");
		PlayerPrefs.DeleteKey("generalHeroDamage");
		PlayerPrefs.DeleteKey("generalMercenaryCost");
		PlayerPrefs.DeleteKey("generalPowersWarCry");
		PlayerPrefs.DeleteKey("generalPowersCatapultsShower");
		PlayerPrefs.DeleteKey("generalPowersArchersShower");
		for (int j = 0; j < UnitsTechSpear.Length; j++)
		{
			PlayerPrefs.DeleteKey("unitsTechSpear_" + j.ToString());
		}
		for (int k = 0; k < UnitsTechMelee.Length; k++)
		{
			PlayerPrefs.DeleteKey("unitsTechMelee_" + k.ToString());
		}
		for (int l = 0; l < UnitsTechMounted.Length; l++)
		{
			PlayerPrefs.DeleteKey("unitsTechMounted_" + l.ToString());
		}
		for (int m = 0; m < UnitsTechRanged.Length; m++)
		{
			PlayerPrefs.DeleteKey("unitsTechRanged_" + m.ToString());
		}
		for (int n = 0; n < UnitsTechSiege.Length; n++)
		{
			PlayerPrefs.DeleteKey("unitsTechSiege_" + n.ToString());
		}
		for (int num = 0; num < CitiesConquered.Length; num++)
		{
			PlayerPrefs.DeleteKey("citiesLevel_" + num.ToString());
			PlayerPrefs.DeleteKey("citiesConquered_" + num.ToString());
		}
		for (int num2 = 0; num2 < UnitsTechMercenary.Length; num2++)
		{
			PlayerPrefs.DeleteKey("mercenary_" + num2.ToString());
		}
		for (int num3 = 0; num3 < tutorialSteps.Length; num3++)
		{
			PlayerPrefs.DeleteKey("tutorialStep_" + num3.ToString());
		}
		PlayerPrefs.DeleteKey(" tutorialSpecialHero");
		PlayerPrefs.DeleteKey(" tutorialSpecialMercenary");
		PlayerPrefs.DeleteKey(" tutorialSpecialArmy");
		PlayerPrefs.DeleteKey(" tutorialSpecialElephants");
		PlayerPrefs.DeleteKey("isSpeedFast");
		PlayerPrefs.DeleteKey("isMusic");
		PlayerPrefs.DeleteKey("isSfx");
		PlayerPrefs.DeleteKey("cameraX");
		PlayerPrefs.DeleteKey("cameraY");
		PlayerPrefs.DeleteKey("cameraZ");
		for (int num4 = 0; num4 < 18; num4++)
		{
			PlayerPrefs.DeleteKey("attackFormation_Type_" + num4.ToString());
		}
	}

	private static void NukeData()
	{
		PlayerPrefs.DeleteAll();
	}

	private static void SaveAudio()
	{
		int num = 0;
		num = (isSfx ? 1 : 0);
		PlayerPrefs.SetInt("isSfx", num);
		num = (isMusic ? 1 : 0);
		PlayerPrefs.SetInt("isMusic", num);
	}

	private static void SaveSpeed()
	{
		int num = 0;
		num = (isFastForward ? 1 : 0);
		PlayerPrefs.SetInt("isSpeedFast", num);
	}

	private static void SaveJustTutorial()
	{
		int num = 0;
		for (int i = 0; i < tutorialSteps.Length; i++)
		{
			num = 0;
			if (tutorialSteps[i])
			{
				num = 1;
			}
			PlayerPrefs.SetInt("tutorialStep_" + i.ToString(), num);
		}
		num = 0;
		if (tutorialSpecialHero)
		{
			num = 1;
		}
		PlayerPrefs.SetInt("tutorialSpecialHero", num);
		num = 0;
		if (tutorialSpecialMercenary)
		{
			num = 1;
		}
		PlayerPrefs.SetInt("tutorialSpecialMercenary", num);
		num = 0;
		if (tutorialSpecialArmy)
		{
			num = 1;
		}
		PlayerPrefs.SetInt("tutorialSpecialArmy", num);
		num = 0;
		if (tutorialSpecialElephants)
		{
			num = 1;
		}
		PlayerPrefs.SetInt("tutorialSpecialElephants", num);
	}

	private static void SaveJustMoneyLocal(float _money)
	{
		PlayerPrefs.SetFloat("playerMoney", _money);
	}

	private static void SavePlayerStats(float _money, int _expPoints, int _playerWave)
	{
		SaveJustMoneyLocal(_money);
		PlayerPrefs.SetInt("playerLevel", playerLevel);
		PlayerPrefs.SetFloat("playerExp", playerExp);
		PlayerPrefs.SetInt("playerExpPoints", _expPoints);
		PlayerPrefs.SetInt("playerWave", _playerWave);
	}

	private static void SaveDefenceStats()
	{
		PlayerPrefs.SetInt("wallLvl", WallLvl);
		PlayerPrefs.SetInt("archersLvl", ArchersLvl);
		PlayerPrefs.SetInt("heroSelected_0", (int)HeroeSelected[0]);
		PlayerPrefs.SetInt("heroSelected_1", (int)HeroeSelected[1]);
		for (int i = 0; i < HeroeLvl.Length; i++)
		{
			PlayerPrefs.SetInt("heroLevel_" + i.ToString(), HeroeLvl[i]);
		}
		PlayerPrefs.SetInt("towerAmmo_0", (int)TowerAmmo[0]);
		PlayerPrefs.SetInt("towerAmmo_1", (int)TowerAmmo[1]);
		PlayerPrefs.SetInt("towerAmmo_2", (int)TowerAmmo[2]);
		PlayerPrefs.SetInt("towerAmmo_3", (int)TowerAmmo[3]);
		PlayerPrefs.SetInt("towerAmmo_4", (int)TowerAmmo[4]);
		PlayerPrefs.SetInt("towerAmmo_5", (int)TowerAmmo[5]);
		PlayerPrefs.SetInt("towerLvl", TowerLvl);
		PlayerPrefs.SetInt("towerAmmoSmallLvl", TowerAmmoSmallLvl);
		PlayerPrefs.SetInt("towerAmmoBigLvl", TowerAmmoBigLvl);
		PlayerPrefs.SetInt("catapultAmmo_0", (int)CatapultAmmo[0]);
		PlayerPrefs.SetInt("catapultAmmo_1", (int)CatapultAmmo[1]);
		PlayerPrefs.SetInt("catapultLvl", CatapultLvl);
		PlayerPrefs.SetInt("catapultAmmoSmallLvl", CatapultAmmoSmallLvl);
		PlayerPrefs.SetInt("catapultAmmoBigLvl", CatapultAmmoBigLvl);
	}

	private static void SaveGeneralBonus()
	{
		PlayerPrefs.SetInt("generalTowerDamage", GeneralTechBase_TowerDamage);
		PlayerPrefs.SetInt("generalCatapultDamage", GeneralTechBase_CatapultDamage);
		PlayerPrefs.SetInt("generalWallHp", GeneralTechBase_WallHp);
		PlayerPrefs.SetInt("generalArchersDamage", GeneralTechBase_ArchersDamage);
		PlayerPrefs.SetInt("generalArchersRange", GeneralTechBase_ArchersRange);
		PlayerPrefs.SetInt("generalIncomeColony", GeneralTechBase_IncomeColony);
		PlayerPrefs.SetInt("generalIncomeKill", GeneralTechBase_IncomeKill);
		PlayerPrefs.SetInt("generalExperienceKill", GeneralTechBase_ExperienceKill);
		PlayerPrefs.SetInt("generalMeleeDamage", GeneralTechArmy_MeleeDamage);
		PlayerPrefs.SetInt("generalRangedDamage", GeneralTechArmy_RangedDamage);
		PlayerPrefs.SetInt("generalSiegeDamage", GeneralTechArmy_SiegeDamage);
		PlayerPrefs.SetInt("generalSiegeHealth", GeneralTechArmy_SiegeHealth);
		PlayerPrefs.SetInt("generalSpawnCooldown", GeneralTechArmy_SpawnCooldown);
		PlayerPrefs.SetInt("generalHeroHealth", GeneralTechArmy_HeroHealth);
		PlayerPrefs.SetInt("generalHeroDamage", GeneralTechArmy_HeroDamage);
		PlayerPrefs.SetInt("generalMercenaryCost", GeneralTechArmy_MercenaryCost);
		PlayerPrefs.SetInt("generalPowersWarCry", GeneralTechPowers_WarCry);
		PlayerPrefs.SetInt("generalPowersCatapultsShower", GeneralTechPowers_CatapultsShower);
		PlayerPrefs.SetInt("generalPowersArchersShower", GeneralTechPowers_ArchersShower);
	}

	private static void SaveUnitsTech()
	{
		for (int i = 0; i < UnitsTechSpear.Length; i++)
		{
			PlayerPrefs.SetInt("unitsTechSpear_" + i.ToString(), UnitsTechSpear[i]);
		}
		for (int j = 0; j < UnitsTechMelee.Length; j++)
		{
			PlayerPrefs.SetInt("unitsTechMelee_" + j.ToString(), UnitsTechMelee[j]);
		}
		for (int k = 0; k < UnitsTechMounted.Length; k++)
		{
			PlayerPrefs.SetInt("unitsTechMounted_" + k.ToString(), UnitsTechMounted[k]);
		}
		for (int l = 0; l < UnitsTechRanged.Length; l++)
		{
			PlayerPrefs.SetInt("unitsTechRanged_" + l.ToString(), UnitsTechRanged[l]);
		}
		for (int m = 0; m < UnitsTechSiege.Length; m++)
		{
			PlayerPrefs.SetInt("unitsTechSiege_" + m.ToString(), UnitsTechSiege[m]);
		}
	}

	private static void SaveMap()
	{
		for (int i = 0; i < CitiesConquered.Length; i++)
		{
			int value = 0;
			if (CitiesConquered[i])
			{
				value = 1;
			}
			PlayerPrefs.SetInt("citiesConquered_" + i.ToString(), value);
			PlayerPrefs.SetInt("citiesLevel_" + i.ToString(), CitiesLevel[i]);
		}
	}

	private static void SaveCharacters()
	{
		for (int i = 0; i < UnitsTechMercenary.Length; i++)
		{
			PlayerPrefs.SetInt(value: UnitsTechMercenary[i] ? 1 : 0, key: "mercenary_" + i.ToString());
		}
		for (int j = 0; j < HeroeLvl.Length; j++)
		{
			PlayerPrefs.SetInt("heroLevel_" + j.ToString(), HeroeLvl[j]);
		}
	}

	private static void SaveAttackFormation(ArmyType[] _unitTypeSelected, int[] _unitIndexSelected)
	{
		for (int i = 0; i < _unitTypeSelected.Length; i++)
		{
			PlayerPrefs.SetInt("attackFormation_Type_" + i.ToString(), (int)_unitTypeSelected[i]);
			PlayerPrefs.SetInt("attackFormation_Index_" + i.ToString(), _unitIndexSelected[i]);
		}
	}

	public static void CloudSaveRead(string cloudSave)
	{
		JSONObject jSONObject = new JSONObject(cloudSave);
		for (int i = 0; i < jSONObject.list.Count; i++)
		{
			JSONObject jSONObject2 = jSONObject.list[i];
			string text = jSONObject.keys[i];
			string s = string.Empty;
			if (text.IndexOf("_", StringComparison.CurrentCultureIgnoreCase) != -1)
			{
				string[] array = text.Split(new string[1]
				{
					"_"
				}, StringSplitOptions.RemoveEmptyEntries);
				text = array[0];
				s = array[1];
			}
			string str = jSONObject2.str;
			switch (text)
			{
			case "hasBoughtBoosters":
				PlayerPrefs.SetInt("hasBoughtBoosters", int.Parse(str));
				break;
			case "hasBoughtVipBoost":
				PlayerPrefs.SetInt("hasBoughtVipBoost", int.Parse(str));
				break;
			case "hasBoughtPowersBoost":
				PlayerPrefs.SetInt("hasBoughtPowersBoost", int.Parse(str));
				break;
			case "hasBoughtXpBoost":
				PlayerPrefs.SetInt("hasBoughtXpBoost", int.Parse(str));
				break;
			case "hasBoughtMoneyBoost":
				PlayerPrefs.SetInt("hasBoughtMoneyBoost", int.Parse(str));
				break;
			case "bankTutorialDone":
				PlayerPrefs.SetInt("bankTutorialDone", int.Parse(str));
				break;
			case "gotFreeGems":
				PlayerPrefs.SetInt("gotFreeGems", int.Parse(str));
				break;
			case "vipUser":
				PlayerPrefs.SetInt("vipUser", int.Parse(str));
				break;
			case "bankLvl":
				PlayerPrefs.SetInt("bankLvl", int.Parse(str));
				break;
			case "xpBooster":
				PlayerPrefs.SetInt("xpBoost", int.Parse(str));
				break;
			case "moneyBooster":
				PlayerPrefs.SetInt("moneyBoost", int.Parse(str));
				break;
			case "powerBooster":
				PlayerPrefs.SetInt("powerBoost", int.Parse(str));
				break;
			case "playerRubies":
				PlayerPrefs.SetInt("playerRubies", int.Parse(str));
				break;
			case "playerMoney":
				PlayerPrefs.SetFloat("playerMoney", float.Parse(str));
				break;
			case "playerWave":
				PlayerPrefs.SetInt("playerWave", int.Parse(str));
				break;
			case "playerExpPoints":
				PlayerPrefs.SetInt("playerExpPoints", int.Parse(str));
				break;
			case "playerLevel":
				playerLevel = int.Parse(str);
				break;
			case "playerExp":
				playerExp = float.Parse(str);
				break;
			case "wallLvl":
				WallLvl = int.Parse(str);
				break;
			case "archersLvl":
				ArchersLvl = int.Parse(str);
				break;
			case "ratedGame":
				PlayerPrefs.SetInt("ratedGame", int.Parse(str));
				break;
			case "heroSelected":
				HeroeSelected[int.Parse(s)] = (HeroeType)int.Parse(str);
				break;
			case "heroLvl":
				HeroeLvl[int.Parse(s)] = int.Parse(str);
				break;
			case "towerAmmo":
				TowerAmmo[int.Parse(s)] = (TowerAmmoType)int.Parse(str);
				break;
			case "towerLvl":
				TowerLvl = int.Parse(str);
				break;
			case "towerSmallLvl":
				TowerAmmoSmallLvl = int.Parse(str);
				break;
			case "towerBigLvl":
				TowerAmmoBigLvl = int.Parse(str);
				break;
			case "catapultAmmo":
				CatapultAmmo[int.Parse(s)] = (CatapultAmmoType)int.Parse(str);
				break;
			case "catapultLvl":
				CatapultLvl = int.Parse(str);
				break;
			case "catapultSmallLvl":
				CatapultAmmoSmallLvl = int.Parse(str);
				break;
			case "catapultBigLvl":
				CatapultAmmoBigLvl = int.Parse(str);
				break;
			case "techSpear":
				UnitsTechSpear[int.Parse(s)] = int.Parse(str);
				break;
			case "techMelee":
				UnitsTechMelee[int.Parse(s)] = int.Parse(str);
				break;
			case "techRanged":
				UnitsTechRanged[int.Parse(s)] = int.Parse(str);
				break;
			case "techMounted":
				UnitsTechMounted[int.Parse(s)] = int.Parse(str);
				break;
			case "techSiege":
				UnitsTechSiege[int.Parse(s)] = int.Parse(str);
				break;
			case "techMerc":
				if (int.Parse(str) == 0)
				{
					UnitsTechMercenary[int.Parse(s)] = false;
				}
				else
				{
					UnitsTechMercenary[int.Parse(s)] = true;
				}
				break;
			case "genBase":
				switch (int.Parse(s))
				{
				case 0:
					GeneralTechBase_TowerDamage = int.Parse(str);
					break;
				case 1:
					GeneralTechBase_CatapultDamage = int.Parse(str);
					break;
				case 2:
					GeneralTechBase_ArchersRange = int.Parse(str);
					break;
				case 3:
					GeneralTechBase_ArchersDamage = int.Parse(str);
					break;
				case 4:
					GeneralTechBase_WallHp = int.Parse(str);
					break;
				case 5:
					GeneralTechBase_IncomeColony = int.Parse(str);
					break;
				case 6:
					GeneralTechBase_IncomeKill = int.Parse(str);
					break;
				case 7:
					GeneralTechBase_ExperienceKill = int.Parse(str);
					break;
				}
				break;
			case "genArmy":
				switch (int.Parse(s))
				{
				case 0:
					GeneralTechArmy_MeleeDamage = int.Parse(str);
					break;
				case 1:
					GeneralTechArmy_RangedDamage = int.Parse(str);
					break;
				case 2:
					GeneralTechArmy_SiegeDamage = int.Parse(str);
					break;
				case 3:
					GeneralTechArmy_SiegeHealth = int.Parse(str);
					break;
				case 4:
					GeneralTechArmy_SpawnCooldown = int.Parse(str);
					break;
				case 5:
					GeneralTechArmy_HeroHealth = int.Parse(str);
					break;
				case 6:
					GeneralTechArmy_HeroDamage = int.Parse(str);
					break;
				case 7:
					GeneralTechArmy_MercenaryCost = int.Parse(str);
					break;
				}
				break;
			case "genPower":
				switch (int.Parse(s))
				{
				case 0:
					GeneralTechPowers_WarCry = int.Parse(str);
					break;
				case 1:
					GeneralTechPowers_CatapultsShower = int.Parse(str);
					break;
				case 2:
					GeneralTechPowers_ArchersShower = int.Parse(str);
					break;
				}
				break;
			case "cityConquered":
				for (int j = 0; j < CitiesConquered.Length; j++)
				{
					if (int.Parse(str) == 0)
					{
						CitiesConquered[int.Parse(s)] = false;
					}
					else
					{
						CitiesConquered[int.Parse(s)] = true;
					}
				}
				break;
			case "cityLvl":
				CitiesLevel[int.Parse(s)] = int.Parse(str);
				break;
			case "tutorialArmy":
				if (int.Parse(str) == 0)
				{
					tutorialSpecialArmy = false;
				}
				else
				{
					tutorialSpecialArmy = true;
				}
				break;
			case "tutorialElephant":
				if (int.Parse(str) == 0)
				{
					tutorialSpecialElephants = false;
				}
				else
				{
					tutorialSpecialElephants = true;
				}
				break;
			}
		}
		SavePlayerStats(PlayerPrefs.GetFloat("playerMoney", 0f), PlayerPrefs.GetInt("playerExpPoints", 0), PlayerPrefs.GetInt("playerWave", 0));
		SaveDefenceStats();
		SaveGeneralBonus();
		SaveUnitsTech();
		SaveMap();
		SaveCharacters();
		for (int k = 0; k < 25; k++)
		{
			if (PlayerPrefs.HasKey("attackFormation_Type_" + k.ToString()))
			{
				PlayerPrefs.DeleteKey("attackFormation_Type_" + k.ToString());
			}
			if (PlayerPrefs.HasKey("attackFormation_Index_" + k.ToString()))
			{
				PlayerPrefs.DeleteKey("attackFormation_Index_" + k.ToString());
			}
		}
		for (int l = 0; l < tutorialSteps.Length; l++)
		{
			tutorialSteps[l] = true;
		}
		SaveJustTutorial();
		PlayerPrefs.Save();
	}

	public static string CloudSaveWrite()
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.STRING);
		jSONObject.AddField("hasBoughtBoosters", PlayerPrefs.GetInt("hasBoughtBoosters").ToString());
		jSONObject.AddField("hasBoughtVipBoost", PlayerPrefs.GetInt("hasBoughtVipBoost").ToString());
		jSONObject.AddField("hasBoughtPowersBoost", PlayerPrefs.GetInt("hasBoughtPowersBoost").ToString());
		jSONObject.AddField("hasBoughtXpBoost", PlayerPrefs.GetInt("hasBoughtXpBoost").ToString());
		jSONObject.AddField("hasBoughtMoneyBoost", PlayerPrefs.GetInt("hasBoughtMoneyBoost").ToString());
		jSONObject.AddField("gotFreeGems", PlayerPrefs.GetInt("gotFreeGems").ToString());
		jSONObject.AddField("bankTutorialDone", PlayerPrefs.GetInt("bankTutorialDone").ToString());
		jSONObject.AddField("vipUser", PlayerPrefs.GetInt("vipUser").ToString());
		jSONObject.AddField("playerRubies", PlayerPrefs.GetInt("playerRubies").ToString());
		jSONObject.AddField("bankLvl", PlayerPrefs.GetInt("bankLvl").ToString());
		jSONObject.AddField("moneyBooster", PlayerPrefs.GetInt("moneyBoost").ToString());
		jSONObject.AddField("xpBooster", PlayerPrefs.GetInt("xpBoost").ToString());
		jSONObject.AddField("powerBooster", PlayerPrefs.GetInt("powerBoost").ToString());
		jSONObject.AddField("playerMoney", ((int)PlayerPrefs.GetFloat("playerMoney", 0f)).ToString());
		jSONObject.AddField("playerWave", PlayerPrefs.GetInt("playerWave", 1).ToString());
		jSONObject.AddField("playerExpPoints", PlayerPrefs.GetInt("playerExpPoints", 0).ToString());
		jSONObject.AddField("playerLevel", playerLevel.ToString());
		jSONObject.AddField("playerExp", ((int)playerExp).ToString());
		jSONObject.AddField("wallLvl", WallLvl.ToString());
		jSONObject.AddField("archersLvl", ArchersLvl.ToString());
		jSONObject.AddField("ratedGame", PlayerPrefs.GetInt("ratedGame", 0).ToString());
		for (int i = 0; i < HeroeSelected.Length; i++)
		{
			JSONObject jSONObject2 = jSONObject;
			string name = "heroSelected_" + i.ToString();
			int num = (int)HeroeSelected[i];
			jSONObject2.AddField(name, num.ToString());
		}
		for (int j = 0; j < HeroeLvl.Length; j++)
		{
			jSONObject.AddField("heroLvl_" + j.ToString(), HeroeLvl[j].ToString());
		}
		for (int k = 0; k < TowerAmmo.Length; k++)
		{
			JSONObject jSONObject3 = jSONObject;
			string name2 = "towerAmmo_" + k.ToString();
			int num2 = (int)TowerAmmo[k];
			jSONObject3.AddField(name2, num2.ToString());
		}
		jSONObject.AddField("towerLvl", TowerLvl.ToString());
		jSONObject.AddField("towerSmallLvl", TowerAmmoSmallLvl.ToString());
		jSONObject.AddField("towerBigLvl", TowerAmmoBigLvl.ToString());
		for (int l = 0; l < CatapultAmmo.Length; l++)
		{
			JSONObject jSONObject4 = jSONObject;
			string name3 = "catapultAmmo_" + l.ToString();
			int num3 = (int)CatapultAmmo[l];
			jSONObject4.AddField(name3, num3.ToString());
		}
		jSONObject.AddField("catapultLvl", CatapultLvl.ToString());
		jSONObject.AddField("catapultSmallLvl", CatapultAmmoSmallLvl.ToString());
		jSONObject.AddField("catapultBigLvl", CatapultAmmoBigLvl.ToString());
		for (int m = 0; m < UnitsTechSpear.Length; m++)
		{
			jSONObject.AddField("techSpear_" + m.ToString(), UnitsTechSpear[m].ToString());
		}
		for (int n = 0; n < UnitsTechMelee.Length; n++)
		{
			jSONObject.AddField("techMelee_" + n.ToString(), UnitsTechMelee[n].ToString());
		}
		for (int num4 = 0; num4 < UnitsTechRanged.Length; num4++)
		{
			jSONObject.AddField("techRanged_" + num4.ToString(), UnitsTechRanged[num4].ToString());
		}
		for (int num5 = 0; num5 < UnitsTechMounted.Length; num5++)
		{
			jSONObject.AddField("techMounted_" + num5.ToString(), UnitsTechMounted[num5].ToString());
		}
		for (int num6 = 0; num6 < UnitsTechSiege.Length; num6++)
		{
			jSONObject.AddField("techSiege_" + num6.ToString(), UnitsTechSiege[num6].ToString());
		}
		for (int num7 = 0; num7 < UnitsTechMercenary.Length; num7++)
		{
			if (UnitsTechMercenary[num7])
			{
				jSONObject.AddField("techMerc_" + num7.ToString(), "1");
			}
			else
			{
				jSONObject.AddField("techMerc_" + num7.ToString(), "0");
			}
		}
		jSONObject.AddField("genBase_0", GeneralTechBase_TowerDamage.ToString());
		jSONObject.AddField("genBase_1", GeneralTechBase_CatapultDamage.ToString());
		jSONObject.AddField("genBase_2", GeneralTechBase_ArchersRange.ToString());
		jSONObject.AddField("genBase_3", GeneralTechBase_ArchersDamage.ToString());
		jSONObject.AddField("genBase_4", GeneralTechBase_WallHp.ToString());
		jSONObject.AddField("genBase_5", GeneralTechBase_IncomeColony.ToString());
		jSONObject.AddField("genBase_6", GeneralTechBase_IncomeKill.ToString());
		jSONObject.AddField("genBase_7", GeneralTechBase_ExperienceKill.ToString());
		jSONObject.AddField("genArmy_0", GeneralTechArmy_MeleeDamage.ToString());
		jSONObject.AddField("genArmy_1", GeneralTechArmy_RangedDamage.ToString());
		jSONObject.AddField("genArmy_2", GeneralTechArmy_SiegeDamage.ToString());
		jSONObject.AddField("genArmy_3", GeneralTechArmy_SiegeHealth.ToString());
		jSONObject.AddField("genArmy_4", GeneralTechArmy_SpawnCooldown.ToString());
		jSONObject.AddField("genArmy_5", GeneralTechArmy_HeroHealth.ToString());
		jSONObject.AddField("genArmy_6", GeneralTechArmy_HeroDamage.ToString());
		jSONObject.AddField("genArmy_7", GeneralTechArmy_MercenaryCost.ToString());
		jSONObject.AddField("genPower_0", GeneralTechPowers_WarCry.ToString());
		jSONObject.AddField("genPower_1", GeneralTechPowers_CatapultsShower.ToString());
		jSONObject.AddField("genPower_2", GeneralTechPowers_ArchersShower.ToString());
		for (int num8 = 0; num8 < CitiesConquered.Length; num8++)
		{
			if (CitiesConquered[num8])
			{
				jSONObject.AddField("cityConquered_" + num8.ToString(), "1");
			}
			else
			{
				jSONObject.AddField("cityConquered_" + num8.ToString(), "0");
			}
		}
		for (int num9 = 0; num9 < CitiesLevel.Length; num9++)
		{
			jSONObject.AddField("cityLvl_" + num9.ToString(), CitiesLevel[num9].ToString());
		}
		if (tutorialSpecialArmy)
		{
			jSONObject.AddField("tutorialArmy", "1");
		}
		else
		{
			jSONObject.AddField("tutorialArmy", "0");
		}
		if (tutorialSpecialElephants)
		{
			jSONObject.AddField("tutorialElephant", "1");
		}
		else
		{
			jSONObject.AddField("tutorialElephant", "0");
		}
		return jSONObject.Print();
	}

	public static bool IsVipUser()
	{
		if (PlayerPrefs.GetInt("vipUser") == 1)
		{
			return true;
		}
		return false;
	}

	public static bool PlayBankTutorial(int citiesConquered)
	{
		if (PlayerPrefs.GetInt("bankTutorialDone") == 0 && citiesConquered >= ConfigPrefsController.bankColoniesRequiredToUnlock && PlayerPrefs.GetInt("bankLvl") <= 0)
		{
			return true;
		}
		return false;
	}

	public static bool GotFreeGems()
	{
		if (PlayerPrefs.GetInt("gotFreeGems") == 1)
		{
			return true;
		}
		return false;
	}
}
