using I2.Loc;
using System;
using UnityEngine;

public static class ConfigPrefsController
{
	public static int enableAnalytics = 0;

	public static int[] wallLvl = new int[99]
	{
		1,
		2,
		3,
		5,
		6,
		8,
		9,
		10,
		11,
		13,
		14,
		16,
		17,
		18,
		20,
		21,
		23,
		24,
		25,
		27,
		28,
		30,
		31,
		32,
		34,
		35,
		37,
		38,
		39,
		41,
		42,
		44,
		45,
		50,
		51,
		52,
		53,
		55,
		56,
		57,
		58,
		59,
		60,
		61,
		63,
		64,
		65,
		66,
		67,
		69,
		70,
		71,
		72,
		73,
		75,
		80,
		81,
		82,
		84,
		85,
		86,
		87,
		88,
		90,
		91,
		92,
		94,
		95,
		96,
		98,
		99,
		100,
		101,
		103,
		104,
		105,
		107,
		115,
		117,
		120,
		122,
		124,
		127,
		129,
		132,
		134,
		137,
		139,
		142,
		144,
		147,
		149,
		153,
		156,
		158,
		162,
		166,
		170,
		175
	};

	public static int[] archersLvl = new int[99]
	{
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		10,
		11,
		12,
		13,
		14,
		15,
		16,
		17,
		18,
		19,
		20,
		21,
		22,
		23,
		24,
		25,
		26,
		27,
		28,
		29,
		30,
		31,
		32,
		33,
		34,
		35,
		36,
		37,
		38,
		39,
		40,
		41,
		42,
		43,
		44,
		45,
		46,
		47,
		48,
		49,
		50,
		51,
		52,
		53,
		54,
		55,
		56,
		57,
		58,
		59,
		60,
		61,
		62,
		63,
		64,
		65,
		66,
		67,
		68,
		69,
		70,
		71,
		72,
		73,
		74,
		75,
		76,
		77,
		78,
		79,
		80,
		81,
		82,
		83,
		84,
		85,
		86,
		87,
		88,
		89,
		90,
		91,
		92,
		93,
		94,
		95,
		96,
		97,
		98,
		99
	};

	public static int[] archersStrength = new int[99]
	{
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		4,
		4,
		4,
		4,
		5,
		5,
		5,
		5,
		6,
		6,
		6,
		6,
		7,
		7,
		7,
		7,
		8,
		8,
		8,
		8,
		9,
		9,
		9,
		9,
		10,
		10,
		10,
		10,
		11,
		11,
		11,
		11,
		12,
		12,
		12,
		12,
		13,
		13,
		13,
		14,
		15,
		15,
		15,
		15,
		16,
		16,
		16,
		16,
		17,
		17,
		17,
		17,
		18,
		18,
		18,
		18,
		19,
		19,
		19,
		19,
		20,
		20,
		20,
		20,
		21,
		21,
		21,
		21,
		22,
		22,
		22,
		22,
		23,
		23,
		23,
		23,
		24,
		24,
		24,
		24,
		25,
		25,
		25
	};

	public static int[] towerLvl = new int[99]
	{
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		10,
		11,
		12,
		13,
		14,
		15,
		16,
		17,
		18,
		19,
		20,
		21,
		22,
		23,
		24,
		25,
		26,
		27,
		28,
		29,
		30,
		31,
		32,
		33,
		34,
		35,
		36,
		37,
		38,
		39,
		40,
		41,
		42,
		43,
		44,
		45,
		46,
		47,
		48,
		49,
		50,
		51,
		52,
		53,
		54,
		55,
		56,
		57,
		58,
		59,
		60,
		61,
		62,
		63,
		64,
		65,
		66,
		67,
		68,
		69,
		70,
		71,
		72,
		73,
		74,
		75,
		76,
		77,
		78,
		79,
		80,
		81,
		82,
		83,
		84,
		85,
		86,
		87,
		88,
		89,
		90,
		91,
		92,
		93,
		94,
		95,
		96,
		97,
		98
	};

	public static int[] catapultLvl = new int[99]
	{
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		10,
		11,
		12,
		13,
		14,
		15,
		16,
		17,
		18,
		19,
		20,
		21,
		22,
		23,
		24,
		25,
		26,
		27,
		28,
		29,
		30,
		31,
		32,
		33,
		34,
		35,
		36,
		37,
		38,
		39,
		40,
		41,
		42,
		43,
		44,
		45,
		46,
		47,
		48,
		49,
		50,
		51,
		52,
		53,
		54,
		55,
		56,
		57,
		58,
		59,
		60,
		61,
		62,
		63,
		64,
		65,
		66,
		67,
		68,
		69,
		70,
		71,
		72,
		73,
		74,
		75,
		76,
		77,
		78,
		79,
		80,
		81,
		82,
		83,
		84,
		85,
		86,
		87,
		88,
		89,
		90,
		91,
		92,
		93,
		94,
		95,
		96,
		97,
		98
	};

	public static int[] unitsLvl = new int[99]
	{
		7,
		7,
		14,
		14,
		21,
		21,
		28,
		28,
		35,
		35,
		42,
		42,
		49,
		49,
		56,
		56,
		63,
		63,
		70,
		70,
		77,
		77,
		84,
		84,
		91,
		91,
		98,
		98,
		105,
		105,
		112,
		112,
		119,
		130,
		130,
		137,
		137,
		144,
		144,
		151,
		151,
		158,
		158,
		165,
		165,
		172,
		172,
		179,
		179,
		186,
		186,
		193,
		193,
		200,
		200,
		220,
		220,
		225,
		225,
		230,
		230,
		235,
		235,
		240,
		240,
		245,
		245,
		250,
		250,
		255,
		255,
		260,
		260,
		265,
		265,
		265,
		270,
		290,
		290,
		295,
		295,
		300,
		300,
		305,
		305,
		310,
		310,
		315,
		315,
		320,
		320,
		325,
		325,
		330,
		330,
		335,
		335,
		340,
		340
	};

	public static int[] unitsSkin = new int[99]
	{
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1
	};

	public static int[] prespawnedUnits = new int[99]
	{
		5,
		6,
		8,
		9,
		10,
		11,
		12,
		13,
		14,
		15,
		16,
		17,
		18,
		19,
		20,
		21,
		22,
		23,
		24,
		25,
		26,
		27,
		28,
		29,
		30,
		31,
		32,
		33,
		34,
		35,
		36,
		37,
		38,
		39,
		40,
		41,
		42,
		43,
		44,
		45,
		46,
		47,
		48,
		49,
		50,
		51,
		52,
		53,
		54,
		55,
		56,
		57,
		58,
		59,
		60,
		61,
		62,
		63,
		64,
		65,
		66,
		67,
		68,
		69,
		70,
		71,
		72,
		73,
		74,
		75,
		76,
		77,
		78,
		79,
		80,
		81,
		82,
		83,
		84,
		85,
		86,
		87,
		88,
		89,
		90,
		91,
		92,
		93,
		94,
		95,
		96,
		97,
		98,
		99,
		100,
		100,
		100,
		100,
		100
	};

	public static float[] spawnTime = new float[99]
	{
		18f,
		17.95f,
		17.9f,
		17.85f,
		17.8f,
		17.75f,
		17.7f,
		17.65f,
		17.6f,
		17.55f,
		17.5f,
		17.45f,
		17.4f,
		17.35f,
		17.3f,
		17.25f,
		17.2f,
		17.15f,
		17.1f,
		17.05f,
		17f,
		16.95f,
		16.9f,
		16.85f,
		16.8f,
		16.75f,
		16.7f,
		16.65f,
		16.6f,
		16.55f,
		16.5f,
		16.45f,
		16.4f,
		14.35f,
		14.3f,
		14.25f,
		14.2f,
		14.15f,
		14.1f,
		14.05f,
		14f,
		13.95f,
		13.9f,
		13.85f,
		13.8f,
		13.75f,
		13.7f,
		13.65f,
		13.6f,
		13.55f,
		13.5f,
		13.45f,
		13.4f,
		13.35f,
		13.3f,
		12.25f,
		12.2f,
		12.15f,
		12.1f,
		12.05f,
		12f,
		11.95f,
		11.9f,
		11.85f,
		11.8f,
		11.75f,
		11.7f,
		11.65f,
		11.6f,
		11.55f,
		11.5f,
		11.45f,
		11.4f,
		11.35f,
		11.3f,
		11.25f,
		11.2f,
		11.15f,
		11.1f,
		11.05f,
		11f,
		10.95f,
		10.9f,
		10.85f,
		10.8f,
		10.75f,
		10.7f,
		10.65f,
		10.6f,
		10.55f,
		10.5f,
		10.45f,
		10.4f,
		10.35f,
		10.3f,
		10.25f,
		10.2f,
		10.15f,
		10.1f
	};

	public static int[] unitsPerCohort = new int[99]
	{
		12,
		13,
		14,
		15,
		16,
		17,
		18,
		19,
		20,
		21,
		22,
		23,
		24,
		25,
		26,
		27,
		28,
		29,
		30,
		31,
		32,
		33,
		34,
		35,
		36,
		37,
		38,
		39,
		40,
		41,
		42,
		43,
		44,
		45,
		46,
		47,
		48,
		49,
		50,
		51,
		52,
		53,
		54,
		55,
		56,
		57,
		58,
		59,
		60,
		61,
		62,
		63,
		64,
		65,
		66,
		67,
		68,
		69,
		70,
		71,
		72,
		73,
		74,
		75,
		76,
		77,
		78,
		79,
		80,
		81,
		82,
		83,
		84,
		85,
		86,
		87,
		88,
		89,
		90,
		91,
		92,
		93,
		94,
		95,
		96,
		97,
		98,
		99,
		100,
		100,
		100,
		100,
		100,
		100,
		100,
		100,
		100,
		100,
		100
	};

	public static float speedNormal = 1.1f;

	public static float speedFast = 2f;

	public static int jsonVersion = 0;

	public static int adsVideoMinWave = 10;

	public static float adsVideoMoneyPerWave = 8f;

	public static int adsVideoCounter;

	public static int adsVideoCounterFlag = 4;

	public static int adsVideoCounterWinWave = 1;

	public static int adsVideoCounterLoseWave = 2;

	public static int adsVideoCounterWinInvasion = 1;

	public static int adsVideoCounterLoseInvasion = 1;

	public static int adsVideoDamageCounter;

	public static int adsVideoDamageCounterSeen;

	public static int adsVideoDamageCounterFlag = 3;

	public static float adsVideoDamageBase = 20f;

	public static float adsVideoDamagePerTry = 5f;

	public static float adsVideoDamageMax = 50f;

	public static float adsInterstitialMinWave = 8f;

	public static int adsInterstitialCounter;

	public static int adsInterstitialCounterFlag = 3;

	public static int adsInterstitialCounterWinWave = 1;

	public static int adsInterstitialCounterLoseWave = 1;

	public static int adsInterstitialCounterWinInvasion = 1;

	public static int adsInterstitialCounterLoseInvasion = 1;

	public static int rateMinWave = 23;

	public static float rateMoneyPerWave = 14f;

	public static float rateProbability = 0f;

	public static int bankMinutesFull = 360;

	public static int bankMinutesFullRepeat = 720;

	public static float[] bankMultiplier = new float[21]
	{
		0.1f,
		0.125f,
		0.15f,
		0.2f,
		0.25f,
		0.3f,
		0.35f,
		0.4f,
		0.45f,
		0.5f,
		0.55f,
		0.6f,
		0.65f,
		0.7f,
		0.75f,
		0.8f,
		0.85f,
		0.9f,
		0.95f,
		1f,
		1.05f
	};

	public static int[] bankPrice = new int[21]
	{
		10,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5,
		5
	};

	public static int bankColoniesRequiredToUnlock = 2;

	public static float bankExtraMultiplier = 0.25f;

	public static float bankExtraQualifier = 0.4f;

	public static float[] storeRubyBonus = new float[4]
	{
		0f,
		10f,
		24f,
		40f
	};

	public static int[] storeRubyPackAmount = new int[4]
	{
		25,
		155,
		350,
		750
	};

	public static int xpBoostWaveDuration = 100;

	public static int moneyBoostWaveDuration = 100;

	public static int powersBoostWaveDuration = 100;

	public static int subscriptionBoostWaveDuration = 100;

	public static int[] boostsPrices = new int[4]
	{
		20,
		20,
		20,
		40
	};

	public static int freeMoneyBoostDuration = 10;

	public static int waveFreeItem = 7;

	public static int freeGems = 10;

	public static int gemDropPerWave = 5;

	public static int[] gemDropPerInvasion = new int[99]
	{
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1
	};

	public static int miniWavesPerWave = 3;

	public static int waveArmyPerWave = 10;

	public static int waveSiegePerWave = 20;

	public static int waveElephantPerWave = 30;

	public static int waveCohortQuantityMin = 1;

	public static float waveCohortQuantityIncrement = 0.09f;

	public static int[] waveBarbariansCost = new int[8]
	{
		1,
		2,
		3,
		6,
		7,
		14,
		15,
		30
	};

	public static float[] waveBarbariansMultiplier = new float[8]
	{
		1f,
		1.75f,
		2.25f,
		2.4f,
		2.5f,
		2.55f,
		2.6f,
		2.65f
	};

	public static float invasionIndexReferenceArchers = 35f;

	public static float invasionIndexReferenceSoldiers = 700f;

	public static float invasionIndexReferenceTowers = 50f;

	public static float invasionIndexReferenceCatapults = 50f;

	public static float invasionIndexReferenceWalls = 90f;

	public static float waveUnitsMelee_Hp_Base = 20f;

	public static float waveUnitsMelee_Attack_Base = 4f;

	public static float waveUnitsMelee_AttackRanged_Base = 2f;

	public static float waveUnitsMelee_Defence_Base = 0f;

	public static float waveUnitsMelee_Increment = 0.005f;

	public static float waveUnitsSpear_Hp_Base = 20f;

	public static float waveUnitsSpear_Attack_Base = 3f;

	public static float waveUnitsSpear_AttackRanged_Base = 3f;

	public static float waveUnitsSpear_Defence_Base = 0f;

	public static float waveUnitsSpear_Increment = 0.005f;

	public static float waveUnitsRanged_Hp_Base = 20f;

	public static float waveUnitsRanged_Attack_Base = 1.8f;

	public static float waveUnitsRanged_AttackRanged_Base = 1.8f;

	public static float waveUnitsRanged_Defence_Base = 0f;

	public static float waveUnitsRanged_Increment = 0.005f;

	public static float waveUnitsCavalry_Hp_Base = 20f;

	public static float waveUnitsCavalry_Attack_Base = 3f;

	public static float waveUnitsCavalry_AttackRanged_Base = 3f;

	public static float waveUnitsCavalry_Defence_Base = 0f;

	public static float waveUnitsCavalry_Increment = 0.005f;

	public static float waveUnitsSiege_Hp_Base = 1000f;

	public static float waveUnitsSiege_Attack_Base = 20f;

	public static float waveUnitsSiege_AttackRanged_Base = 20f;

	public static float waveUnitsSiege_Defence_Base = 0f;

	public static float waveUnitsSiege_Increment = 0.005f;

	public static float waveUnitsElephant_Hp_Base = 1400f;

	public static float waveUnitsElephant_Attack_Base = 36f;

	public static float waveUnitsElephant_AttackRanged_Base = 13f;

	public static float waveUnitsElephant_Defence_Base = 0f;

	public static float waveUnitsElephant_Increment = 0.005f;

	public static float waveRomanTime = 60f;

	public static float[] waveRomanEachTime = new float[3]
	{
		10f,
		35f,
		61f
	};

	public static float spawnTimeStartPlayer = 1f;

	public static float spawnTimeStartBarbarian = 0.85f;

	public static float spawnTimeIncrementBase = 0.015f;

	public static float spawnTimeRequiredPerUnit = 0.5f;

	public static float incomeMoneyPerEnemy = 2.25f;

	public static float incomeExperiencePerEnemy = 0.5f;

	public static int playerExperienceMaxLevel = 1000;

	public static float playerExperienceBase = 10f;

	public static float playerExperienceMultiplier = 6f;

	public static int playerExperiencePointsPerLevel = 3;

	public static float bonusDamageSpearVsMounted = 0.5f;

	public static float bonusDamageMountedVsJavaline = 0.5f;

	public static float bonusDamageJavalineVsSpear = 0.5f;

	public static float bonusSmallAmmoVsSoldier = 0.5f;

	public static float bonusBigAmmoVsSiege = 5f;

	public static int[] upgradeLvlCohort = new int[6]
	{
		2,
		5,
		8,
		11,
		14,
		17
	};

	public static int upgradeWallMax = 175;

	public static int upgradePriceWallBase = 10;

	public static float upgradePriceWallMultiplier = 2.15f;

	public static int upgradeArchersMax = 100;

	public static int upgradePriceArchersBase = 10;

	public static float upgradePriceArchersMultiplier = 8.25f;

	public static int upgradeTowerMax = 100;

	public static int upgradePriceTowerBase = 5;

	public static float upgradePriceTowerMultiplier = 3.85f;

	public static int upgradeCatapultMax = 100;

	public static int upgradePriceCatapultBase = 5;

	public static float upgradePriceCatapultMultiplier = 3.85f;

	public static int upgradeTowerAmmoSmallMax = 100;

	public static int upgradePriceTowerAmmoSmallBase = 2;

	public static float upgradePriceTowerAmmoSmallMultiplier = 2.2f;

	public static int upgradeTowerAmmoBigMax = 100;

	public static int upgradePriceTowerAmmoBigBase = 2;

	public static float upgradePriceTowerAmmoBigMultiplier = 2.2f;

	public static int upgradeCatapultAmmoSmallMax = 100;

	public static int upgradePriceCatapultAmmoSmallBase = 2;

	public static float upgradePriceCatapultAmmoSmallMultiplier = 2.2f;

	public static int upgradeCatapultAmmoBigMax = 100;

	public static int upgradePriceCatapultAmmoBigBase = 2;

	public static float upgradePriceCatapultAmmoBigMultiplier = 2.2f;

	public static int upgradeHeroMax = 100;

	public static int upgradePriceHeroBase = 2;

	public static float upgradePriceHeroMultiplier = 6.05f;

	public static int[] upgradeHeroWithRubiesPrice = new int[4]
	{
		1,
		1,
		1,
		1
	};

	public static int[] upgradePriceColonyMultiplier = new int[4]
	{
		10,
		15,
		20,
		25
	};

	public static float lifeWallBase = 100f;

	public static float lifePerWallLevel = 50f;

	public static float cooldownTower = 5f;

	public static float cooldownTowerBase = 0.6f;

	public static float cooldownTowerPerLevel = 0.01f;

	public static float cooldownCatapult = 5f;

	public static float cooldownCatapultBase = 0.25f;

	public static float cooldownCatapultPerLevel = 0.01f;

	public static float damageTowerAmmoSmallBase = 2f;

	public static float damageTowerAmmoSmallPerLevel = 0.3f;

	public static float damageTowerAmmoBigBase = 2f;

	public static float damageTowerAmmoBigPerLevel = 0.3f;

	public static float damageCatapultAmmoSmallBase = 2f;

	public static float damageCatapultAmmoSmallPerLevel = 0.3f;

	public static float damageCatapultAmmoBigBase = 2f;

	public static float damageCatapultAmmoBigPerLevel = 0.3f;

	public static float colonyIncomeTime = 60f;

	public static float colonyIncomeMultiplier = 0.6f;

	public static int generalPowerMaxUpgrade = 50;

	public static int generalPowerCostUpgrade_WarCry = 2;

	public static int generalPowerCostUpgrade_Arrows = 4;

	public static int generalPowerCostUpgrade_Boulders = 8;

	public static float generalPowerWarCryDamageBase = 0f;

	public static float generalPowerWarCryDamagePerLevel = 0.02f;

	public static float generalPowerWarCryDurationBase = 4f;

	public static float generalPowerWarCryDurationPerLevel = 0.2f;

	public static float generalPowerWarCryCooldown = 5f;

	public static float generalPowerWarCryCooldownBase = 0.25f;

	public static float generalPowerWarCryCooldownPerLevel = 0.025f;

	public static float generalPowerBouldersDamageBase = 0.6f;

	public static float generalPowerBouldersDamagePerLevel = 1f;

	public static float generalPowerBouldersCooldown = 15f;

	public static float generalPowerBouldersCooldownBase = 0.25f;

	public static float generalPowerBouldersCooldownPerLevel = 0.025f;

	public static float generalPowerArrowsDamageBase = 0.6f;

	public static float generalPowerArrowsDamagePerLevel = 1f;

	public static float generalPowerArrowsCooldown = 10f;

	public static float generalPowerArrowsCooldownBase = 0.25f;

	public static float generalPowerArrowsCooldownPerLevel = 0.025f;

	public static int generalBaseMaxUpgrade = 50;

	public static int generalBaseCostUpgrade = 1;

	public static float generalBaseTowerDamagePerLevel = 0.005f;

	public static float generalBaseCatapultDamagePerLevel = 0.005f;

	public static float generalBaseWallHpPerLevel = 0.01f;

	public static float generalBaseArchersDamagePerLevel = 0.005f;

	public static float generalBaseArchersRangePerLevel = 0.005f;

	public static float generalBaseIncomeColonyPerLevel = 0.01f;

	public static float generalBaseIncomeKillPerLevel = 0.01f;

	public static float generalBaseExperienceKillPerLevel = 0.01f;

	public static int generalArmyMaxUpgrade = 50;

	public static int generalArmyCostUpgrade = 1;

	public static float generalArmyMeleeDamagePerLevel = 0.005f;

	public static float generalArmyRangedDamagePerLevel = 0.005f;

	public static float generalArmySiegeDamagePerLevel = 0.005f;

	public static float generalArmySiegeHealthPerLevel = 0.005f;

	public static float generalArmySpawnCooldown = 0.005f;

	public static float generalArmyHeroHealthPerLevel = 0.005f;

	public static float generalArmyHeroDamagePerLevel = 0.005f;

	public static float generalArmyMercenaryCostPerLevel = 0.01f;

	public static float heroeRomanCommanderAttack = 0.25f;

	public static float heroeRomanCommanderHealth = 0.25f;

	public static float heroeBarbarianCommanderFear = 0.05f;

	public static float heroeGladiatorCriticalHitChance = 0.05f;

	public static float heroeGladiatorCriticalHitDamage = 15f;

	public static float heroeArcaniExtraMoney = 0.15f;

	public static int[] unitStats_Hero_Hp = new int[4]
	{
		130,
		120,
		100,
		120
	};

	public static int[] unitStats_Hero_Attack = new int[4]
	{
		10,
		7,
		9,
		11
	};

	public static int[] unitStats_Hero_RangedAttack = new int[4]
	{
		0,
		7,
		0,
		0
	};

	public static int[] unitStats_Hero_Defence = new int[4];

	public static float unitStats_Hero_Cost = 1f;

	public static float unitStats_Hero_Hp_PerLevel = 3.5f;

	public static float unitStats_Hero_Attack_PerLevel = 0.75f;

	public static float unitStats_Hero_RangedAttack_PerLevel = 0.75f;

	public static float unitStats_Hero_Defence_PerLevel = 0.25f;

	public static float unitStats_Roman_Archer_AttackBase = 8f;

	public static float unitStats_Roman_Archer_AttackPerLevel = 0.5f;

	public static int unitStats_Roman_Melee_Hp = 20;

	public static int unitStats_Roman_Melee_Attack = 3;

	public static int unitStats_Roman_Melee_RangedAttack = 1;

	public static int unitStats_Roman_Melee_Defence = 0;

	public static CohortSubType unitStats_Roman_Melee_Type = CohortSubType.None;

	public static int unitStats_Roman_Melee_Hp_Upgrade = 7;

	public static int unitStats_Roman_Melee_Attack_Upgrade = 1;

	public static int unitStats_Roman_Melee_RangedAttack_Upgrade = 1;

	public static int unitStats_Roman_Melee_Defence_Upgrade = 1;

	public static int[] unitStats_Roman_Melee_Price_Base = new int[10]
	{
		40,
		120,
		360,
		1080,
		3240,
		8748,
		24786,
		69984,
		196830,
		551124
	};

	public static float[] unitStats_Roman_Melee_Price_Multiplier = new float[3]
	{
		1f,
		1.35f,
		1.7f
	};

	public static float unitStats_Roman_Melee_Cost = 0.5f;

	public static int unitStats_Roman_Spear_Hp = 41;

	public static int unitStats_Roman_Spear_Attack = 4;

	public static int unitStats_Roman_Spear_RangedAttack = 0;

	public static int unitStats_Roman_Spear_Defence = 3;

	public static CohortSubType unitStats_Roman_Spear_Type = CohortSubType.Spear;

	public static int unitStats_Roman_Spear_Hp_Upgrade = 9;

	public static int unitStats_Roman_Spear_Attack_Upgrade = 1;

	public static int unitStats_Roman_Spear_RangedAttack_Upgrade = 1;

	public static int unitStats_Roman_Spear_Defence_Upgrade = 1;

	public static int[] unitStats_Roman_Spear_Price_Base = new int[7]
	{
		120,
		516,
		2219,
		8587,
		34872,
		141128,
		568923
	};

	public static float[] unitStats_Roman_Spear_Price_Multiplier = new float[3]
	{
		1f,
		1.35f,
		1.7f
	};

	public static float unitStats_Roman_Spear_Cost = 0.5f;

	public static int unitStats_Roman_Ranged_Hp = 68;

	public static int unitStats_Roman_Ranged_Attack = 0;

	public static int unitStats_Roman_Ranged_RangedAttack = 3;

	public static int unitStats_Roman_Ranged_Defence = 6;

	public static CohortSubType unitStats_Roman_Ranged_Type = CohortSubType.Javaline;

	public static int unitStats_Roman_Ranged_Hp_Upgrade = 9;

	public static int unitStats_Roman_Ranged_Attack_Upgrade = 1;

	public static int unitStats_Roman_Ranged_RangedAttack_Upgrade = 1;

	public static int unitStats_Roman_Ranged_Defence_Upgrade = 1;

	public static int[] unitStats_Roman_Ranged_Price_Base = new int[5]
	{
		2219,
		8587,
		34872,
		141128,
		568293
	};

	public static float[] unitStats_Roman_Ranged_Price_Multiplier = new float[3]
	{
		1f,
		1.35f,
		1.75f
	};

	public static float unitStats_Roman_Ranged_Cost = 0.5f;

	public static int unitStats_Roman_Mounted_Hp = 122;

	public static int unitStats_Roman_Mounted_Attack = 13;

	public static int unitStats_Roman_Mounted_RangedAttack = 0;

	public static int unitStats_Roman_Mounted_Defence = 12;

	public static CohortSubType unitStats_Roman_Mounted_Type = CohortSubType.Mounted;

	public static int unitStats_Roman_Mounted_Hp_Upgrade = 9;

	public static int unitStats_Roman_Mounted_Attack_Upgrade = 1;

	public static int unitStats_Roman_Mounted_RangedAttack_Upgrade = 1;

	public static int unitStats_Roman_Mounted_Defence_Upgrade = 1;

	public static int[] unitStats_Roman_Mounted_Price_Base = new int[4]
	{
		8587,
		34872,
		141128,
		568293
	};

	public static float[] unitStats_Roman_Mounted_Price_Multiplier = new float[3]
	{
		1f,
		1.35f,
		1.75f
	};

	public static float unitStats_Roman_Mounted_Cost = 0.5f;

	public static int unitStats_Roman_Siege_Hp = 1000;

	public static int unitStats_Roman_Siege_Attack = 30;

	public static int unitStats_Roman_Siege_RangedAttack = 0;

	public static int unitStats_Roman_Siege_Defence = 50;

	public static CohortSubType unitStats_Roman_Siege_Type = CohortSubType.None;

	public static int unitStats_Roman_Siege_Hp_Upgrade = 150;

	public static int unitStats_Roman_Siege_Attack_Upgrade = 10;

	public static int unitStats_Roman_Siege_RangedAttack_Upgrade = 1;

	public static int unitStats_Roman_Siege_Defence_Upgrade = 5;

	public static int[] unitStats_Roman_Siege_Price_Base = new int[4]
	{
		34872,
		114872,
		376923,
		879486
	};

	public static float[] unitStats_Roman_Siege_Price_Multiplier = new float[3]
	{
		1f,
		1.35f,
		1.75f
	};

	public static float unitStats_Roman_Siege_Cost = 0.5f;

	public static float unitStats_Italian_Melee_Cost = 2f;

	public static float unitStats_Italian_Ranged_Cost = 2f;

	public static float unitStats_Italian_Mounted_Cost = 2f;

	public static float unitStats_Gaul_Melee_Cost = 2f;

	public static float unitStats_Gaul_Spear_Cost = 2f;

	public static float unitStats_Iberian_Ranged_Cost = 2f;

	public static float unitStats_Iberian_Mounted_Cost = 2f;

	public static float unitStats_Carthaginian_Melee_Cost = 2f;

	public static float unitStats_Carthaginian_Spear_Cost = 2f;

	public static string[] unitsStats_Hero_Name = new string[4]
	{
		"Roman Commander",
		"Gladiator",
		"Arcani",
		"Barbarian Commander"
	};

	public static string[] unitsStats_Soldier_Name = new string[5]
	{
		"Roman Skirmisher",
		"Roman Spearman",
		"Roman Melee",
		"Roman Cavalry",
		"Battering Ram"
	};

	public static string[] unitsStats_Mercenary_Name = new string[9]
	{
		"Italian Skirmisher",
		"Italian Cavalry",
		"Italian Swordman",
		"Gaulish Spearman",
		"Gaulish Axeman",
		"Iberian Skirmisher",
		"Iberian Cavalry",
		"Carthag. Spearman",
		"Carthag. Swordman"
	};

	public static void PrepareTranslations()
	{
		unitsStats_Hero_Name[0] = ScriptLocalization.Get("MAP/invasion_unit_hero0").ToUpper();
		unitsStats_Hero_Name[1] = ScriptLocalization.Get("MAP/invasion_unit_hero1").ToUpper();
		unitsStats_Hero_Name[2] = ScriptLocalization.Get("MAP/invasion_unit_hero2").ToUpper();
		unitsStats_Hero_Name[3] = ScriptLocalization.Get("MAP/invasion_unit_hero3").ToUpper();
		unitsStats_Soldier_Name[0] = ScriptLocalization.Get("MAP/invasion_unit_romanranged").ToUpper();
		unitsStats_Soldier_Name[1] = ScriptLocalization.Get("MAP/invasion_unit_romanspear").ToUpper();
		unitsStats_Soldier_Name[2] = ScriptLocalization.Get("MAP/invasion_unit_romanmelee").ToUpper();
		unitsStats_Soldier_Name[3] = ScriptLocalization.Get("MAP/invasion_unit_romancavalry").ToUpper();
		unitsStats_Soldier_Name[4] = ScriptLocalization.Get("MAP/invasion_unit_romansiege").ToUpper();
		unitsStats_Mercenary_Name[0] = ScriptLocalization.Get("MAP/invasion_unit_italian0").ToUpper();
		unitsStats_Mercenary_Name[1] = ScriptLocalization.Get("MAP/invasion_unit_italian1").ToUpper();
		unitsStats_Mercenary_Name[2] = ScriptLocalization.Get("MAP/invasion_unit_italian2").ToUpper();
		unitsStats_Mercenary_Name[3] = ScriptLocalization.Get("MAP/invasion_unit_gaul0").ToUpper();
		unitsStats_Mercenary_Name[4] = ScriptLocalization.Get("MAP/invasion_unit_gaul1").ToUpper();
		unitsStats_Mercenary_Name[5] = ScriptLocalization.Get("MAP/invasion_unit_iberian1").ToUpper();
		unitsStats_Mercenary_Name[6] = ScriptLocalization.Get("MAP/invasion_unit_iberian0").ToUpper();
		unitsStats_Mercenary_Name[7] = ScriptLocalization.Get("MAP/invasion_unit_cart0").ToUpper();
		unitsStats_Mercenary_Name[8] = ScriptLocalization.Get("MAP/invasion_unit_cart1").ToUpper();
	}

	public static int GetColoniesIncome()
	{
		int num = 0;
		float num2 = 0f;
		for (int i = 1; i < PlayerPrefsController.CitiesConquered.Length; i++)
		{
			if (PlayerPrefsController.CitiesConquered[i])
			{
				float num3 = (float)(PlayerPrefsController.CitiesLevel[i] + 1) * ((5f + (float)i * colonyIncomeMultiplier) / 5f);
				int num4 = Convert.ToInt32(Math.Floor(num3));
				if (num4 < 1)
				{
					num4 = 1;
				}
				num2 += (float)num4;
			}
		}
		if (num2 > 0f)
		{
			num = Mathf.FloorToInt(num2);
			return num + Mathf.FloorToInt((float)num * generalBaseIncomeColonyPerLevel * (float)PlayerPrefsController.GeneralTechBase_IncomeColony);
		}
		return 0;
	}

	public static float GetUnitsRomanHealth(CohortType _type, CohortSubType _subType, int _unitIndex, int _upgradeLevel)
	{
		float num = 0f;
		switch (_subType)
		{
		case CohortSubType.None:
			num = ((_type != CohortType.Siege) ? ((float)(unitStats_Roman_Melee_Hp + unitStats_Roman_Melee_Hp_Upgrade * _unitIndex * 3 + unitStats_Roman_Melee_Hp_Upgrade * _upgradeLevel)) : ((float)(unitStats_Roman_Siege_Hp + unitStats_Roman_Siege_Hp_Upgrade * _unitIndex * 3 + unitStats_Roman_Siege_Hp_Upgrade * _upgradeLevel)));
			break;
		case CohortSubType.Spear:
			num = unitStats_Roman_Spear_Hp + unitStats_Roman_Spear_Hp_Upgrade * _unitIndex * 3 + unitStats_Roman_Spear_Hp_Upgrade * _upgradeLevel;
			break;
		case CohortSubType.Javaline:
			num = unitStats_Roman_Ranged_Hp + unitStats_Roman_Ranged_Hp_Upgrade * _unitIndex * 3 + unitStats_Roman_Ranged_Hp_Upgrade * _upgradeLevel;
			break;
		case CohortSubType.Mounted:
			num = unitStats_Roman_Mounted_Hp + unitStats_Roman_Mounted_Hp_Upgrade * _unitIndex * 3 + unitStats_Roman_Mounted_Hp_Upgrade * _upgradeLevel;
			break;
		}
		if (_subType == CohortSubType.None && _type == CohortType.Siege)
		{
			num += num * (generalArmySiegeHealthPerLevel * (float)PlayerPrefsController.GeneralTechArmy_SiegeHealth);
		}
		return num;
	}

	public static float GetUnitsRomanAttack(CohortType _type, CohortSubType _subType, int _unitIndex, int _upgradeLevel)
	{
		float num = 0f;
		switch (_subType)
		{
		case CohortSubType.None:
			num = ((_type != CohortType.Siege) ? ((float)(unitStats_Roman_Melee_Attack + unitStats_Roman_Melee_Attack_Upgrade * _unitIndex * 3 + unitStats_Roman_Melee_Attack_Upgrade * _upgradeLevel)) : ((float)(unitStats_Roman_Siege_Attack + unitStats_Roman_Siege_Attack_Upgrade * _unitIndex * 3 + unitStats_Roman_Siege_Attack_Upgrade * _upgradeLevel)));
			break;
		case CohortSubType.Spear:
			num = unitStats_Roman_Spear_Attack + unitStats_Roman_Spear_Attack_Upgrade * _unitIndex * 3 + unitStats_Roman_Spear_Attack_Upgrade * _upgradeLevel;
			break;
		case CohortSubType.Javaline:
			num = unitStats_Roman_Ranged_Attack + unitStats_Roman_Ranged_Attack_Upgrade * _unitIndex * 3 + unitStats_Roman_Ranged_Attack_Upgrade * _upgradeLevel;
			break;
		case CohortSubType.Mounted:
			num = unitStats_Roman_Mounted_Attack + unitStats_Roman_Mounted_Attack_Upgrade * _unitIndex * 3 + unitStats_Roman_Mounted_Attack_Upgrade * _upgradeLevel;
			break;
		}
		if (_subType == CohortSubType.None && _type == CohortType.Siege)
		{
			return num + num * (generalArmySiegeDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_SiegeDamage);
		}
		return num + num * (generalArmyMeleeDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_MeleeDamage);
	}

	public static float GetUnitsRomanRangedAttack(CohortType _type, CohortSubType _subType, int _unitIndex, int _upgradeLevel)
	{
		float num = 0f;
		switch (_subType)
		{
		case CohortSubType.None:
			num = ((_type != CohortType.Siege) ? ((float)(unitStats_Roman_Melee_RangedAttack + unitStats_Roman_Melee_RangedAttack_Upgrade * _unitIndex * 3 + unitStats_Roman_Melee_RangedAttack_Upgrade * _upgradeLevel)) : ((float)(unitStats_Roman_Siege_RangedAttack + unitStats_Roman_Siege_RangedAttack_Upgrade * _unitIndex * 3 + unitStats_Roman_Siege_RangedAttack_Upgrade * _upgradeLevel)));
			break;
		case CohortSubType.Spear:
			num = unitStats_Roman_Spear_RangedAttack + unitStats_Roman_Spear_RangedAttack_Upgrade * _unitIndex * 3 + unitStats_Roman_Spear_RangedAttack_Upgrade * _upgradeLevel;
			break;
		case CohortSubType.Javaline:
			num = unitStats_Roman_Ranged_RangedAttack + unitStats_Roman_Ranged_RangedAttack_Upgrade * _unitIndex * 3 + unitStats_Roman_Ranged_RangedAttack_Upgrade * _upgradeLevel;
			break;
		case CohortSubType.Mounted:
			num = unitStats_Roman_Mounted_RangedAttack + unitStats_Roman_Mounted_RangedAttack_Upgrade * _unitIndex * 3 + unitStats_Roman_Mounted_RangedAttack_Upgrade * _upgradeLevel;
			break;
		}
		if (_subType == CohortSubType.None && _type == CohortType.Siege)
		{
			return num + num * (generalArmySiegeDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_SiegeDamage);
		}
		return num + num * (generalArmyRangedDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_RangedDamage);
	}

	public static float GetUnitsRomanDefence(CohortType _type, CohortSubType _subType, int _unitIndex, int _upgradeLevel)
	{
		float result = 0f;
		switch (_subType)
		{
		case CohortSubType.None:
			result = ((_type != CohortType.Siege) ? ((float)(unitStats_Roman_Melee_Defence + unitStats_Roman_Melee_Defence_Upgrade * _unitIndex * 3 + unitStats_Roman_Melee_Defence_Upgrade * _upgradeLevel)) : ((float)(unitStats_Roman_Siege_Defence + unitStats_Roman_Siege_Defence_Upgrade * _unitIndex * 3 + unitStats_Roman_Siege_Defence_Upgrade * _upgradeLevel)));
			break;
		case CohortSubType.Spear:
			result = unitStats_Roman_Spear_Defence + unitStats_Roman_Spear_Defence_Upgrade * _unitIndex * 3 + unitStats_Roman_Spear_Defence_Upgrade * _upgradeLevel;
			break;
		case CohortSubType.Javaline:
			result = unitStats_Roman_Ranged_Defence + unitStats_Roman_Ranged_Defence_Upgrade * _unitIndex * 3 + unitStats_Roman_Ranged_Defence_Upgrade * _upgradeLevel;
			break;
		case CohortSubType.Mounted:
			result = unitStats_Roman_Mounted_Defence + unitStats_Roman_Mounted_Defence_Upgrade * _unitIndex * 3 + unitStats_Roman_Mounted_Defence_Upgrade * _upgradeLevel;
			break;
		}
		return result;
	}

	public static int GetUpgradeUnitPrice(CohortType _type, CohortSubType _subType, int _unitIndex)
	{
		int result = 0;
		switch (_subType)
		{
		case CohortSubType.None:
			result = ((_type != CohortType.Siege) ? ((int)((float)unitStats_Roman_Melee_Price_Base[_unitIndex] * unitStats_Roman_Melee_Price_Multiplier[PlayerPrefsController.UnitsTechMelee[_unitIndex]])) : ((int)((float)unitStats_Roman_Siege_Price_Base[_unitIndex] * unitStats_Roman_Siege_Price_Multiplier[PlayerPrefsController.UnitsTechSiege[_unitIndex]])));
			break;
		case CohortSubType.Spear:
			result = (int)((float)unitStats_Roman_Spear_Price_Base[_unitIndex] * unitStats_Roman_Spear_Price_Multiplier[PlayerPrefsController.UnitsTechSpear[_unitIndex]]);
			break;
		case CohortSubType.Javaline:
			result = (int)((float)unitStats_Roman_Ranged_Price_Base[_unitIndex] * unitStats_Roman_Ranged_Price_Multiplier[PlayerPrefsController.UnitsTechRanged[_unitIndex]]);
			break;
		case CohortSubType.Mounted:
			result = (int)((float)unitStats_Roman_Mounted_Price_Base[_unitIndex] * unitStats_Roman_Mounted_Price_Multiplier[PlayerPrefsController.UnitsTechMounted[_unitIndex]]);
			break;
		}
		return result;
	}

	public static float GetUnitsBarbarianHealth(Faction _faction, CohortType _type, CohortSubType _subType, int _unitIndex, int _levelToApply)
	{
		float result = 0f;
		int num = (int)(_faction - 1) * 2 + _unitIndex;
		if (num > 7)
		{
			num = 7;
		}
		float num2 = waveBarbariansMultiplier[num];
		switch (_subType)
		{
		case CohortSubType.None:
			result = ((_type != CohortType.Siege) ? (num2 * (waveUnitsMelee_Hp_Base + waveUnitsMelee_Hp_Base * waveUnitsMelee_Increment * (float)_levelToApply)) : (num2 * (waveUnitsSiege_Hp_Base + waveUnitsSiege_Hp_Base * waveUnitsSiege_Increment * (float)_levelToApply)));
			break;
		case CohortSubType.Spear:
			result = num2 * (waveUnitsSpear_Hp_Base + waveUnitsSpear_Hp_Base * waveUnitsSpear_Increment * (float)_levelToApply);
			break;
		case CohortSubType.Javaline:
			result = num2 * (waveUnitsRanged_Hp_Base + waveUnitsRanged_Hp_Base * waveUnitsRanged_Increment * (float)_levelToApply);
			break;
		case CohortSubType.Mounted:
			if (_type == CohortType.Siege)
			{
				if (_unitIndex == 0)
				{
					num2 = 1f;
				}
				if (_unitIndex == 1)
				{
					num2 = 1.55f;
				}
				result = num2 * (waveUnitsElephant_Hp_Base + waveUnitsElephant_Hp_Base * waveUnitsElephant_Increment * (float)_levelToApply);
			}
			else
			{
				result = num2 * (waveUnitsCavalry_Hp_Base + waveUnitsCavalry_Hp_Base * waveUnitsCavalry_Increment * (float)_levelToApply);
			}
			break;
		}
		return result;
	}

	public static float GetUnitsBarbarianAttack(Faction _faction, CohortType _type, CohortSubType _subType, int _unitIndex, int _levelToApply)
	{
		float result = 0f;
		int num = (int)(_faction - 1) * 2 + _unitIndex;
		if (num > 7)
		{
			num = 7;
		}
		float num2 = waveBarbariansMultiplier[num];
		switch (_subType)
		{
		case CohortSubType.None:
			result = ((_type != CohortType.Siege) ? (num2 * (waveUnitsMelee_Attack_Base + waveUnitsMelee_Attack_Base * waveUnitsMelee_Increment * (float)_levelToApply)) : (num2 * (waveUnitsSiege_Attack_Base + waveUnitsSiege_Attack_Base * waveUnitsSiege_Increment * (float)_levelToApply)));
			break;
		case CohortSubType.Spear:
			result = num2 * (waveUnitsSpear_Attack_Base + waveUnitsSpear_Attack_Base * waveUnitsSpear_Increment * (float)_levelToApply);
			break;
		case CohortSubType.Javaline:
			result = num2 * (waveUnitsRanged_Attack_Base + waveUnitsRanged_Attack_Base * waveUnitsRanged_Increment * (float)_levelToApply);
			break;
		case CohortSubType.Mounted:
			if (_type == CohortType.Siege)
			{
				if (_unitIndex == 0)
				{
					num2 = 1f;
				}
				if (_unitIndex == 1)
				{
					num2 = 1.55f;
				}
				result = num2 * (waveUnitsElephant_Attack_Base + waveUnitsElephant_Attack_Base * waveUnitsElephant_Increment * (float)_levelToApply);
			}
			else
			{
				result = num2 * (waveUnitsCavalry_Attack_Base + waveUnitsCavalry_Attack_Base * waveUnitsCavalry_Increment * (float)_levelToApply);
			}
			break;
		}
		return result;
	}

	public static float GetUnitsBarbarianRangedAttack(Faction _faction, CohortType _type, CohortSubType _subType, int _unitIndex, int _levelToApply)
	{
		float result = 0f;
		int num = (int)(_faction - 1) * 2 + _unitIndex;
		if (num > 7)
		{
			num = 7;
		}
		float num2 = waveBarbariansMultiplier[num];
		switch (_subType)
		{
		case CohortSubType.None:
			result = ((_type != CohortType.Siege) ? (num2 * (waveUnitsMelee_AttackRanged_Base + waveUnitsMelee_AttackRanged_Base * waveUnitsMelee_Increment * (float)_levelToApply)) : (num2 * (waveUnitsSiege_AttackRanged_Base + waveUnitsSiege_AttackRanged_Base * waveUnitsSiege_Increment * (float)_levelToApply)));
			break;
		case CohortSubType.Spear:
			result = num2 * (waveUnitsSpear_AttackRanged_Base + waveUnitsSpear_AttackRanged_Base * waveUnitsSpear_Increment * (float)_levelToApply);
			break;
		case CohortSubType.Javaline:
			result = num2 * (waveUnitsRanged_AttackRanged_Base + waveUnitsRanged_AttackRanged_Base * waveUnitsRanged_Increment * (float)_levelToApply);
			break;
		case CohortSubType.Mounted:
			if (_type == CohortType.Siege)
			{
				if (_unitIndex == 0)
				{
					num2 = 1f;
				}
				if (_unitIndex == 1)
				{
					num2 = 1.55f;
				}
				result = num2 * (waveUnitsElephant_AttackRanged_Base + waveUnitsElephant_AttackRanged_Base * waveUnitsElephant_Increment * (float)_levelToApply);
			}
			else
			{
				result = num2 * (waveUnitsCavalry_AttackRanged_Base + waveUnitsCavalry_AttackRanged_Base * waveUnitsCavalry_Increment * (float)_levelToApply);
			}
			break;
		}
		return result;
	}

	public static float GetUnitsBarbarianDefence(Faction _faction, CohortType _type, CohortSubType _subType, int _unitIndex, int _levelToApply)
	{
		float result = 0f;
		int num = (int)(_faction - 1) * 2 + _unitIndex;
		if (num > 7)
		{
			num = 7;
		}
		float num2 = waveBarbariansMultiplier[num];
		switch (_subType)
		{
		case CohortSubType.None:
			result = ((_type != CohortType.Siege) ? (num2 * (waveUnitsMelee_Defence_Base + waveUnitsMelee_Defence_Base * waveUnitsMelee_Increment * (float)_levelToApply)) : (num2 * (waveUnitsSiege_Defence_Base + waveUnitsSiege_Defence_Base * waveUnitsSiege_Increment * (float)_levelToApply)));
			break;
		case CohortSubType.Spear:
			result = num2 * (waveUnitsSpear_Defence_Base + waveUnitsSpear_Defence_Base * waveUnitsSpear_Increment * (float)_levelToApply);
			break;
		case CohortSubType.Javaline:
			result = num2 * (waveUnitsRanged_Defence_Base + waveUnitsRanged_Defence_Base * waveUnitsRanged_Increment * (float)_levelToApply);
			break;
		case CohortSubType.Mounted:
			if (_type == CohortType.Siege)
			{
				if (_unitIndex == 0)
				{
					num2 = 1f;
				}
				if (_unitIndex == 1)
				{
					num2 = 1.55f;
				}
				result = num2 * (waveUnitsElephant_Defence_Base + waveUnitsElephant_Defence_Base * waveUnitsElephant_Increment * (float)_levelToApply);
			}
			else
			{
				result = num2 * (waveUnitsCavalry_Defence_Base + waveUnitsCavalry_Defence_Base * waveUnitsCavalry_Increment * (float)_levelToApply);
			}
			break;
		}
		return result;
	}

	public static float GetUnitsRomanHeroHealth(int _heroIndex, int _heroLvl)
	{
		float num = 0f;
		num = unitStats_Hero_Hp[_heroIndex];
		num += unitStats_Hero_Hp_PerLevel * (float)_heroLvl;
		return num + num * (generalArmyHeroHealthPerLevel * (float)PlayerPrefsController.GeneralTechArmy_HeroHealth);
	}

	public static float GetUnitsRomanHeroAttack(int _heroIndex, int _heroLvl)
	{
		float num = 0f;
		num = unitStats_Hero_Attack[_heroIndex];
		num += unitStats_Hero_Attack_PerLevel * (float)_heroLvl;
		return num + num * (generalArmyHeroDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_HeroDamage);
	}

	public static float GetUnitsRomanHeroRangedAttack(int _heroIndex, int _heroLvl)
	{
		float num = 0f;
		num = unitStats_Hero_RangedAttack[_heroIndex];
		num += unitStats_Hero_RangedAttack_PerLevel * (float)_heroLvl;
		return num + num * (generalArmyHeroDamagePerLevel * (float)PlayerPrefsController.GeneralTechArmy_HeroDamage);
	}

	public static float GetUnitsRomanHeroDefence(int _heroIndex, int _heroLvl)
	{
		float num = 0f;
		num = unitStats_Hero_Defence[_heroIndex];
		return num + unitStats_Hero_Defence_PerLevel * (float)_heroLvl;
	}
}
