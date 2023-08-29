using System;
using System.Collections;
using UnityEngine;

public class StationEngineConfigJsonRetriever : MonoBehaviour
{
	private const string serverAddress = "http://54.235.75.191/web_services/RequestInfo.aspx?package=com.strategygame.gameofwarriors";

	private const string localJsonKey = "localJsonId";

	private const string expIdKey = "experiment_ID";

	private const string countryCodeKey = "countryCode";

	private const string expIdDefault = "default";

	private const string localJsonVersion = "localJsonVersion";

	public bool shouldCheckServer;

	public bool isForTesting;

	public bool isForBalancing;

	private bool isServerReadCompleted;

	private bool isServerReadErrorConnection;

	private bool isServerReadErrorData;

	private StationEngine stationEngine;

	private StationEngine.ComponentStatus actualStatus;

	public bool IsServerReadCompleted => isServerReadCompleted;

	public bool IsServerReadErrorConnection => isServerReadErrorConnection;

	public bool IsServerReadErrorData => isServerReadErrorData;

	public void Initialize(StationEngine stationEngine)
	{
		this.stationEngine = stationEngine;
		actualStatus = StationEngine.ComponentStatus.INITIALIZING;
		if (!isForTesting && PlayerPrefs.HasKey("localJsonId") && PlayerPrefs.HasKey("localJsonVersion"))
		{
			string @string = PlayerPrefs.GetString("localJsonId");
			JSONObject data = new JSONObject(@string);
			SetData(data);
		}
		if (shouldCheckServer)
		{
			RequestJson();
		}
		else
		{
			isServerReadCompleted = true;
		}
	}

	public StationEngine.ComponentStatus GetStatus()
	{
		return actualStatus;
	}

	public void SetStatusTimeOut()
	{
		actualStatus = StationEngine.ComponentStatus.TIME_OUT;
	}

	private void RequestJson()
	{
		string text = "http://54.235.75.191/web_services/RequestInfo.aspx?package=com.strategygame.gameofwarriors";
		if (!isForTesting)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				text += "&platform=android&market=googleplay";
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				text += "&platform=ios&market=appstore";
			}
			text = text + "&appversion=" + Application.version + "&request=json&language=EN";
			if (PlayerPrefs.HasKey("countryCode"))
			{
				text = text + "&countrycode=" + PlayerPrefs.GetString("countryCode");
			}
			if (isForBalancing)
			{
				text += "&experiment_id=test";
			}
			else if (PlayerPrefs.HasKey("experiment_ID"))
			{
				text = text + "&experiment_id=" + PlayerPrefs.GetString("experiment_ID");
			}
			else
			{
				PlayerPrefs.SetString("experiment_ID", "default");
				PlayerPrefs.Save();
			}
			text += "&config_version=2";
		}
		WWW www = new WWW(text);
		StartCoroutine(WaitForRequest(www));
	}

	private IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			try
			{
				JSONObject i = new JSONObject(www.text);
				int tempInt = 0;
				for (int j = 0; j < i.list.Count; j++)
				{
					string key = i.keys[j];
					if (key == "json_version")
					{
						JSONObject json = i.list[j];
						tempInt = int.Parse(json.str);
					}
				}
				if (tempInt >= ConfigPrefsController.jsonVersion)
				{
					SetData(i);
					SaveJson(i);
					actualStatus = StationEngine.ComponentStatus.INITIALIZED;
				}
			}
			catch (Exception ex)
			{
				Exception e = ex;
				stationEngine.PostDebugError("TEST A/B - Error DATA: " + e.Message);
				isServerReadErrorData = true;
				actualStatus = StationEngine.ComponentStatus.ERROR;
			}
		}
		else
		{
			stationEngine.PostDebugError("TEST A/B - Error CONNECTION: " + www.error);
			stationEngine.PostDebugError("TEST A/B - URL: " + www.url);
			isServerReadErrorConnection = true;
			actualStatus = StationEngine.ComponentStatus.ERROR;
		}
		isServerReadCompleted = true;
		stationEngine.SendExperimentID(PlayerPrefs.GetString("experiment_ID"));
	}

	private void SetData(JSONObject jsonMainObj)
	{
		JSONObject jSONObject = null;
		for (int i = 0; i < jsonMainObj.list.Count; i++)
		{
			switch (jsonMainObj.keys[i])
			{
			case "json_version":
				jSONObject = jsonMainObj.list[i];
				PlayerPrefs.SetInt("localJsonVersion", int.Parse(jSONObject.str));
				break;
			case "speed_normal":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.speedNormal = float.Parse(jSONObject.str);
				break;
			case "speed_fast":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.speedFast = float.Parse(jSONObject.str);
				break;
			case "ads_video_Min_Wave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoMinWave = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_video_money_wave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoMoneyPerWave = float.Parse(jSONObject.str);
				break;
			case "ads_Video_Counter_Flag":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoCounterFlag = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Video_Counter_WinWave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoCounterWinWave = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Video_Counter_LoseWave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoCounterLoseWave = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Video_Counter_WinInvasion":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoCounterWinInvasion = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Video_Counter_LoseInvasion":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoCounterLoseInvasion = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Video_Damage_Counter_Flag":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoDamageCounterFlag = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Video_Damage_Base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoDamageBase = float.Parse(jSONObject.str);
				break;
			case "ads_Video_Damage_PerTry":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoDamagePerTry = float.Parse(jSONObject.str);
				break;
			case "ads_Video_Damage_Max":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoDamageMax = float.Parse(jSONObject.str);
				break;
			case "ads_Interstitial_Min_Wave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsVideoMinWave = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Interstitial_Counter_Flag":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsInterstitialCounterFlag = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Interstitial_Counter_WinWave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsInterstitialCounterWinWave = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Interstitial_Counter_LoseWave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsInterstitialCounterLoseWave = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Interstitial_Counter_WinInvasion":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsInterstitialCounterWinInvasion = Convert.ToInt32(jSONObject.str);
				break;
			case "ads_Interstitial_Counter_LoseInvasion":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.adsInterstitialCounterLoseInvasion = Convert.ToInt32(jSONObject.str);
				break;
			case "rate_Min_Wave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.rateMinWave = Convert.ToInt32(jSONObject.str);
				break;
			case "rate_Money_Per_Wave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.rateMoneyPerWave = float.Parse(jSONObject.str);
				break;
			case "rate_Probability":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.rateProbability = float.Parse(jSONObject.str);
				break;
			case "mini_waves_per_wave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.miniWavesPerWave = Convert.ToInt32(jSONObject.str);
				break;
			case "wave_army_per_wave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveArmyPerWave = Convert.ToInt32(jSONObject.str);
				break;
			case "wave_siege_per_wave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveSiegePerWave = Convert.ToInt32(jSONObject.str);
				break;
			case "wave_elephant_per_wave":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveElephantPerWave = Convert.ToInt32(jSONObject.str);
				break;
			case "wave_cohort_quantity_min":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveCohortQuantityMin = Convert.ToInt32(jSONObject.str);
				break;
			case "wave_cohort_quantity_increment":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveCohortQuantityIncrement = float.Parse(jSONObject.str);
				break;
			case "wave_Barbarians_Cost":
				jSONObject = jsonMainObj.list[i];
				for (int num = 0; num < ConfigPrefsController.waveBarbariansCost.Length; num++)
				{
					ConfigPrefsController.waveBarbariansCost[num] = Convert.ToInt32(jSONObject[num].str);
				}
				break;
			case "wave_Barbarians_Multiplier":
				jSONObject = jsonMainObj.list[i];
				for (int l = 0; l < ConfigPrefsController.waveBarbariansMultiplier.Length; l++)
				{
					ConfigPrefsController.waveBarbariansMultiplier[l] = float.Parse(jSONObject[l].str);
				}
				break;
			case "invasion_archers_stats":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.invasionIndexReferenceArchers = float.Parse(jSONObject.str);
				break;
			case "invasion_soldiers_stats":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.invasionIndexReferenceSoldiers = float.Parse(jSONObject.str);
				break;
			case "invasion_towers_stats":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.invasionIndexReferenceTowers = float.Parse(jSONObject.str);
				break;
			case "invasion_catapults_stats":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.invasionIndexReferenceCatapults = float.Parse(jSONObject.str);
				break;
			case "invasion_walls_stats":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.invasionIndexReferenceWalls = float.Parse(jSONObject.str);
				break;
			case "wave_meleesoldier_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsMelee_Hp_Base = float.Parse(jSONObject.str);
				break;
			case "wave_meleesoldier_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsMelee_Attack_Base = float.Parse(jSONObject.str);
				break;
			case "wave_meleesoldier_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsMelee_AttackRanged_Base = float.Parse(jSONObject.str);
				break;
			case "wave_melee_soldier_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsMelee_Defence_Base = float.Parse(jSONObject.str);
				break;
			case "wave_melee_soldier_increment":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsMelee_Increment = float.Parse(jSONObject.str);
				break;
			case "wave_spearsoldier_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSpear_Hp_Base = float.Parse(jSONObject.str);
				break;
			case "wave_spearsoldier_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSpear_Attack_Base = float.Parse(jSONObject.str);
				break;
			case "wave_spearsoldier_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSpear_AttackRanged_Base = float.Parse(jSONObject.str);
				break;
			case "wave_spearsoldier_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSpear_Defence_Base = float.Parse(jSONObject.str);
				break;
			case "wave_spearsoldier_increment":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSpear_Increment = float.Parse(jSONObject.str);
				break;
			case "wave_rangedsoldier_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsRanged_Hp_Base = float.Parse(jSONObject.str);
				break;
			case "wave_rangedsoldier_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsRanged_Attack_Base = float.Parse(jSONObject.str);
				break;
			case "wave_rangedsoldier_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsRanged_Attack_Base = float.Parse(jSONObject.str);
				break;
			case "wave_rangedsoldier_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsRanged_Defence_Base = float.Parse(jSONObject.str);
				break;
			case "wave_rangedsoldier_increment":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsRanged_Increment = float.Parse(jSONObject.str);
				break;
			case "wave_cavalry_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsCavalry_Hp_Base = float.Parse(jSONObject.str);
				break;
			case "wave_cavalry_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsCavalry_Attack_Base = float.Parse(jSONObject.str);
				break;
			case "wave_cavalry_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsCavalry_AttackRanged_Base = float.Parse(jSONObject.str);
				break;
			case "wave_cavalry_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsRanged_Defence_Base = float.Parse(jSONObject.str);
				break;
			case "wave_cavalry_increment":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsRanged_Increment = float.Parse(jSONObject.str);
				break;
			case "wave_siege_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSiege_Hp_Base = float.Parse(jSONObject.str);
				break;
			case "wave_siege_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSiege_Attack_Base = float.Parse(jSONObject.str);
				break;
			case "wave_siege_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSiege_AttackRanged_Base = float.Parse(jSONObject.str);
				break;
			case "wave_siege_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSiege_Defence_Base = float.Parse(jSONObject.str);
				break;
			case "wave_siege_increment":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsSiege_Increment = float.Parse(jSONObject.str);
				break;
			case "wave_elephants_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsElephant_Hp_Base = float.Parse(jSONObject.str);
				break;
			case "wave_elephants_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsElephant_Attack_Base = float.Parse(jSONObject.str);
				break;
			case "wave_elephants_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsElephant_AttackRanged_Base = float.Parse(jSONObject.str);
				break;
			case "wave_elephants_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsElephant_Defence_Base = float.Parse(jSONObject.str);
				break;
			case "wave_elephants_increment":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveUnitsElephant_Increment = float.Parse(jSONObject.str);
				break;
			case "wave_roman_time":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveRomanTime = float.Parse(jSONObject.str);
				break;
			case "wave_roman_each_time":
				jSONObject = jsonMainObj.list[i];
				for (int num29 = 0; num29 < ConfigPrefsController.waveRomanEachTime.Length; num29++)
				{
					ConfigPrefsController.waveRomanEachTime[num29] = float.Parse(jSONObject[num29].str);
				}
				break;
			case "spawn_time_start_player":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.spawnTimeStartPlayer = float.Parse(jSONObject.str);
				break;
			case "spawn_time_start_barbarian":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.spawnTimeStartBarbarian = float.Parse(jSONObject.str);
				break;
			case "spawn_time_increment_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.spawnTimeIncrementBase = float.Parse(jSONObject.str);
				break;
			case "spawn_time_per_unit":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.spawnTimeRequiredPerUnit = float.Parse(jSONObject.str);
				break;
			case "income_money":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.incomeMoneyPerEnemy = float.Parse(jSONObject.str);
				break;
			case "income_experience":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.incomeExperiencePerEnemy = float.Parse(jSONObject.str);
				break;
			case "player_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.playerExperienceMaxLevel = Convert.ToInt32(jSONObject.str);
				break;
			case "player_xp_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.playerExperienceBase = float.Parse(jSONObject.str);
				break;
			case "player_xp_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.playerExperienceMultiplier = float.Parse(jSONObject.str);
				break;
			case "player_xp_points_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.playerExperiencePointsPerLevel = Convert.ToInt32(jSONObject.str);
				break;
			case "spear_vs_mounted":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.bonusDamageSpearVsMounted = float.Parse(jSONObject.str);
				break;
			case "mounted_vs_javaline":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.bonusDamageMountedVsJavaline = float.Parse(jSONObject.str);
				break;
			case "javaline_vs_spear":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.bonusDamageJavalineVsSpear = float.Parse(jSONObject.str);
				break;
			case "smallAmmo_vs_soldier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.bonusSmallAmmoVsSoldier = float.Parse(jSONObject.str);
				break;
			case "bigAmmo_vs_siege":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.bonusBigAmmoVsSiege = float.Parse(jSONObject.str);
				break;
			case "upgrade_level_cohort":
				jSONObject = jsonMainObj.list[i];
				for (int num28 = 0; num28 < ConfigPrefsController.upgradeLvlCohort.Length; num28++)
				{
					ConfigPrefsController.upgradeLvlCohort[num28] = Convert.ToInt32(jSONObject[num28].str);
				}
				break;
			case "wall_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradeWallMax = Convert.ToInt32(jSONObject.str);
				break;
			case "wall_base_price":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceWallBase = Convert.ToInt32(jSONObject.str);
				break;
			case "wall_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceWallMultiplier = float.Parse(jSONObject.str);
				break;
			case "archers_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradeArchersMax = Convert.ToInt32(jSONObject.str);
				break;
			case "archers_base_price":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceArchersBase = Convert.ToInt32(jSONObject.str);
				break;
			case "archers_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceArchersMultiplier = float.Parse(jSONObject.str);
				break;
			case "towers_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradeTowerMax = Convert.ToInt32(jSONObject.str);
				break;
			case "towers_base_price":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceTowerBase = Convert.ToInt32(jSONObject.str);
				break;
			case "towers_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceTowerMultiplier = float.Parse(jSONObject.str);
				break;
			case "catapult_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradeCatapultMax = Convert.ToInt32(jSONObject.str);
				break;
			case "catapult_base_price":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceCatapultBase = Convert.ToInt32(jSONObject.str);
				break;
			case "catapult_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceCatapultMultiplier = float.Parse(jSONObject.str);
				break;
			case "tower_smallAmmo_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradeTowerAmmoSmallMax = Convert.ToInt32(jSONObject.str);
				break;
			case "tower_smallAmmo_base_price":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceTowerAmmoSmallBase = Convert.ToInt32(jSONObject.str);
				break;
			case "tower_smallAmmo_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceTowerAmmoSmallMultiplier = float.Parse(jSONObject.str);
				break;
			case "tower_bigAmmo_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradeTowerAmmoBigMax = Convert.ToInt32(jSONObject.str);
				break;
			case "tower_bigAmmo_base_price":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceTowerAmmoBigBase = Convert.ToInt32(jSONObject.str);
				break;
			case "tower_bigAmmo_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceTowerAmmoBigMultiplier = float.Parse(jSONObject.str);
				break;
			case "catapult_smallAmmo_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradeCatapultAmmoSmallMax = Convert.ToInt32(jSONObject.str);
				break;
			case "catapult_smallAmmo_base_price":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceCatapultAmmoSmallBase = Convert.ToInt32(jSONObject.str);
				break;
			case "catapult_smallAmmo_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceCatapultAmmoSmallMultiplier = float.Parse(jSONObject.str);
				break;
			case "catapult_bigAmmo_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradeCatapultAmmoBigMax = Convert.ToInt32(jSONObject.str);
				break;
			case "catapult_bigAmmo_base_price":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceCatapultAmmoBigBase = Convert.ToInt32(jSONObject.str);
				break;
			case "catapult_bigAmmo_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceCatapultAmmoBigMultiplier = float.Parse(jSONObject.str);
				break;
			case "hero_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradeHeroMax = Convert.ToInt32(jSONObject.str);
				break;
			case "hero_base_price":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceHeroBase = Convert.ToInt32(jSONObject.str);
				break;
			case "hero_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.upgradePriceHeroMultiplier = float.Parse(jSONObject.str);
				break;
			case "colony_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				for (int num27 = 0; num27 < ConfigPrefsController.upgradePriceColonyMultiplier.Length; num27++)
				{
					ConfigPrefsController.upgradePriceColonyMultiplier[num27] = Convert.ToInt32(jSONObject[num27].str);
				}
				break;
			case "wall_life_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.lifeWallBase = float.Parse(jSONObject.str);
				break;
			case "wall_life_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.lifePerWallLevel = float.Parse(jSONObject.str);
				break;
			case "tower_cooldown":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.cooldownTower = float.Parse(jSONObject.str);
				break;
			case "tower_cooldown_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.cooldownTowerBase = float.Parse(jSONObject.str);
				break;
			case "tower_cooldown_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.cooldownTowerPerLevel = float.Parse(jSONObject.str);
				break;
			case "catapult_cooldown":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.cooldownCatapult = float.Parse(jSONObject.str);
				break;
			case "catapult_cooldown_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.cooldownCatapultBase = float.Parse(jSONObject.str);
				break;
			case "catapult_cooldown_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.cooldownCatapultPerLevel = float.Parse(jSONObject.str);
				break;
			case "tower_damage_smallAmmo_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.damageTowerAmmoSmallBase = float.Parse(jSONObject.str);
				break;
			case "tower_damage_smallAmmo_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.damageTowerAmmoSmallPerLevel = float.Parse(jSONObject.str);
				break;
			case "tower_damage_bigAmmo_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.damageTowerAmmoBigBase = float.Parse(jSONObject.str);
				break;
			case "tower_damage_bigAmmo_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.damageTowerAmmoBigPerLevel = float.Parse(jSONObject.str);
				break;
			case "catapult_damage_smallAmmo_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.damageCatapultAmmoSmallBase = float.Parse(jSONObject.str);
				break;
			case "catapult_damage_smallAmmo_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.damageCatapultAmmoSmallPerLevel = float.Parse(jSONObject.str);
				break;
			case "catapult_damage_bigAmmo_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.damageTowerAmmoBigBase = float.Parse(jSONObject.str);
				break;
			case "catapult_damage_bigAmmo_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.damageCatapultAmmoBigPerLevel = float.Parse(jSONObject.str);
				break;
			case "colony_income_time":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.colonyIncomeTime = float.Parse(jSONObject.str);
				break;
			case "colony_income_multiplier":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.colonyIncomeMultiplier = float.Parse(jSONObject.str);
				break;
			case "power_max_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerMaxUpgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "warcry_upgrade_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerCostUpgrade_WarCry = Convert.ToInt32(jSONObject.str);
				break;
			case "boulders_upgrade_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerCostUpgrade_Boulders = Convert.ToInt32(jSONObject.str);
				break;
			case "arrows_upgrade_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerCostUpgrade_Arrows = Convert.ToInt32(jSONObject.str);
				break;
			case "warcry_damage_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerWarCryDamageBase = float.Parse(jSONObject.str);
				break;
			case "warcry_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerWarCryDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "warcry_duration_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerWarCryDurationBase = float.Parse(jSONObject.str);
				break;
			case "warcry_duration_ per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerWarCryDurationPerLevel = float.Parse(jSONObject.str);
				break;
			case "warcry_cooldown":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerWarCryCooldown = float.Parse(jSONObject.str);
				break;
			case "warcry_cooldown_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerWarCryCooldownBase = float.Parse(jSONObject.str);
				break;
			case "warcry_cooldown_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerWarCryCooldownPerLevel = float.Parse(jSONObject.str);
				break;
			case "boulders_damage_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerBouldersDamageBase = float.Parse(jSONObject.str);
				break;
			case "boulders_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerBouldersDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "boulders_cooldown":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerBouldersCooldown = float.Parse(jSONObject.str);
				break;
			case "boulders_cooldown_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerBouldersCooldownBase = float.Parse(jSONObject.str);
				break;
			case "boulders_cooldown_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerBouldersCooldownPerLevel = float.Parse(jSONObject.str);
				break;
			case "arrows_damage_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerArrowsDamageBase = float.Parse(jSONObject.str);
				break;
			case "arrows_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerArrowsDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "arrows_cooldown":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerArrowsCooldown = float.Parse(jSONObject.str);
				break;
			case "arrows_cooldown_base":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerArrowsCooldownBase = float.Parse(jSONObject.str);
				break;
			case "arrows_cooldown_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalPowerArrowsCooldownPerLevel = float.Parse(jSONObject.str);
				break;
			case "base_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseMaxUpgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "base_upgrade_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseCostUpgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "tower_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseTowerDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "catapult_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseCatapultDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "wall_hp_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseWallHpPerLevel = float.Parse(jSONObject.str);
				break;
			case "archers_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseArchersDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "archers_range_per_level per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseArchersRangePerLevel = float.Parse(jSONObject.str);
				break;
			case "colony_income_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseIncomeColonyPerLevel = float.Parse(jSONObject.str);
				break;
			case "kill_income_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseIncomeKillPerLevel = float.Parse(jSONObject.str);
				break;
			case "kill_xp_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalBaseExperienceKillPerLevel = float.Parse(jSONObject.str);
				break;
			case "army_max_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmyMaxUpgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "army_upgrade_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmyCostUpgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "melee_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmyMeleeDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "ranged_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmyRangedDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "siege_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmySiegeDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "siege_health_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmySiegeHealthPerLevel = float.Parse(jSONObject.str);
				break;
			case "spawn_cooldown":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmySpawnCooldown = float.Parse(jSONObject.str);
				break;
			case "hero_health_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmyHeroHealthPerLevel = float.Parse(jSONObject.str);
				break;
			case "hero_damage_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmyHeroDamagePerLevel = float.Parse(jSONObject.str);
				break;
			case "mercenary_cost_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.generalArmyMercenaryCostPerLevel = float.Parse(jSONObject.str);
				break;
			case "hero_romanCommander_Attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.heroeRomanCommanderAttack = float.Parse(jSONObject.str);
				break;
			case "hero_romanCommander_Health":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.heroeRomanCommanderHealth = float.Parse(jSONObject.str);
				break;
			case "hero_barbarianCommander_fear":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.heroeBarbarianCommanderFear = float.Parse(jSONObject.str);
				break;
			case "hero_gladiator_criticalHit_chance":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.heroeGladiatorCriticalHitChance = float.Parse(jSONObject.str);
				break;
			case "hero_gladiator_criticalHit_damage":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.heroeGladiatorCriticalHitDamage = float.Parse(jSONObject.str);
				break;
			case "hero_arcani_extra_money":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.heroeArcaniExtraMoney = float.Parse(jSONObject.str);
				break;
			case "hero_hp":
				jSONObject = jsonMainObj.list[i];
				for (int num26 = 0; num26 < ConfigPrefsController.unitStats_Hero_Hp.Length; num26++)
				{
					ConfigPrefsController.unitStats_Hero_Hp[num26] = Convert.ToInt32(jSONObject[num26].str);
				}
				break;
			case "hero_attack":
				jSONObject = jsonMainObj.list[i];
				for (int num25 = 0; num25 < ConfigPrefsController.unitStats_Hero_Attack.Length; num25++)
				{
					ConfigPrefsController.unitStats_Hero_Attack[num25] = Convert.ToInt32(jSONObject[num25].str);
				}
				break;
			case "hero_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				for (int num24 = 0; num24 < ConfigPrefsController.unitStats_Hero_RangedAttack.Length; num24++)
				{
					ConfigPrefsController.unitStats_Hero_RangedAttack[num24] = Convert.ToInt32(jSONObject[num24].str);
				}
				break;
			case "hero_defence":
				jSONObject = jsonMainObj.list[i];
				for (int num23 = 0; num23 < ConfigPrefsController.unitStats_Hero_Defence.Length; num23++)
				{
					ConfigPrefsController.unitStats_Hero_Defence[num23] = Convert.ToInt32(jSONObject[num23].str);
				}
				break;
			case "hero_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Hero_Cost = float.Parse(jSONObject.str);
				break;
			case "hero_hp_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Hero_Hp_PerLevel = float.Parse(jSONObject.str);
				break;
			case "hero_attack_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Hero_Attack_PerLevel = float.Parse(jSONObject.str);
				break;
			case "hero_ranged_attack_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Hero_RangedAttack_PerLevel = float.Parse(jSONObject.str);
				break;
			case "hero_defence_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Hero_Defence_PerLevel = float.Parse(jSONObject.str);
				break;
			case "roman_archer_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Archer_AttackBase = float.Parse(jSONObject.str);
				break;
			case "roman_archer_attack_per_level":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Archer_AttackPerLevel = float.Parse(jSONObject.str);
				break;
			case "roman_melee_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Melee_Hp = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_melee_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Melee_Attack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_melee_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Melee_RangedAttack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_melee_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Melee_Defence = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_melee_hp_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Melee_Hp_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_melee_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Melee_Attack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_melee_ranged_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Melee_RangedAttack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_melee_defence_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Melee_Defence_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_melee_price":
				jSONObject = jsonMainObj.list[i];
				for (int num22 = 0; num22 < ConfigPrefsController.unitStats_Roman_Melee_Price_Base.Length; num22++)
				{
					ConfigPrefsController.unitStats_Roman_Melee_Price_Base[num22] = Convert.ToInt32(jSONObject[num22].str);
				}
				break;
			case "roman_melee_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				for (int num21 = 0; num21 < ConfigPrefsController.unitStats_Roman_Melee_Price_Multiplier.Length; num21++)
				{
					ConfigPrefsController.unitStats_Roman_Melee_Price_Multiplier[num21] = float.Parse(jSONObject[num21].str);
				}
				break;
			case "roman_melee_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Melee_Cost = float.Parse(jSONObject.str);
				break;
			case "roman_spear_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Spear_Hp = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_spear_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Spear_Attack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_spear_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Spear_RangedAttack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_spear_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Spear_Defence = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_spear_hp_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Spear_Hp_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_spear_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Spear_Attack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_spear_ranged_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Spear_RangedAttack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_spear_defence_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Spear_Defence = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_spear_price":
				jSONObject = jsonMainObj.list[i];
				for (int num20 = 0; num20 < ConfigPrefsController.unitStats_Roman_Spear_Price_Base.Length; num20++)
				{
					ConfigPrefsController.unitStats_Roman_Spear_Price_Base[num20] = Convert.ToInt32(jSONObject[num20].str);
				}
				break;
			case "roman_spear_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				for (int num19 = 0; num19 < ConfigPrefsController.unitStats_Roman_Spear_Price_Multiplier.Length; num19++)
				{
					ConfigPrefsController.unitStats_Roman_Spear_Price_Multiplier[num19] = float.Parse(jSONObject[num19].str);
				}
				break;
			case "roman_spear_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Spear_Cost = float.Parse(jSONObject.str);
				break;
			case "roman_ranged_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Ranged_Hp = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Ranged_Attack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_ranged_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Ranged_RangedAttack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_ranged_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Ranged_Defence = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_ranged_hp_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Ranged_Hp_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_ranged_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Ranged_Attack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_ranged_ranged_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Ranged_RangedAttack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_ranged_defence_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Ranged_Defence_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_ranged_price":
				jSONObject = jsonMainObj.list[i];
				for (int num18 = 0; num18 < ConfigPrefsController.unitStats_Roman_Ranged_Price_Base.Length; num18++)
				{
					ConfigPrefsController.unitStats_Roman_Ranged_Price_Base[num18] = Convert.ToInt32(jSONObject[num18].str);
				}
				break;
			case "roman_ranged_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				for (int num17 = 0; num17 < ConfigPrefsController.unitStats_Roman_Ranged_Price_Multiplier.Length; num17++)
				{
					ConfigPrefsController.unitStats_Roman_Ranged_Price_Multiplier[num17] = float.Parse(jSONObject[num17].str);
				}
				break;
			case "roman_ranged_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Ranged_Cost = float.Parse(jSONObject.str);
				break;
			case "roman_mounted_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Mounted_Hp = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_mounted_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Mounted_Attack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_mounted_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Mounted_RangedAttack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_mounted_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Mounted_Defence = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_mounted_hp_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Mounted_Hp_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_mounted_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Mounted_Attack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_mounted_ranged_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Mounted_RangedAttack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_mounted_defence_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Mounted_Defence = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_mounted_price":
				jSONObject = jsonMainObj.list[i];
				for (int num16 = 0; num16 < ConfigPrefsController.unitStats_Roman_Mounted_Price_Base.Length; num16++)
				{
					ConfigPrefsController.unitStats_Roman_Mounted_Price_Base[num16] = Convert.ToInt32(jSONObject[num16].str);
				}
				break;
			case "roman_mounted_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				for (int num15 = 0; num15 < ConfigPrefsController.unitStats_Roman_Mounted_Price_Multiplier.Length; num15++)
				{
					ConfigPrefsController.unitStats_Roman_Mounted_Price_Multiplier[num15] = float.Parse(jSONObject[num15].str);
				}
				break;
			case "roman_mounted_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Mounted_Cost = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_siege_hp":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Siege_Hp = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_siege_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Siege_Attack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_siege_ranged_attack":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Siege_RangedAttack = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_siege_defence":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Siege_Defence = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_siege_hp_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Siege_Hp_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_siege_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Siege_Attack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_siege_ranged_attack_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Siege_Attack_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_siege_defence_upgrade":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Siege_Defence_Upgrade = Convert.ToInt32(jSONObject.str);
				break;
			case "roman_siege_price":
				jSONObject = jsonMainObj.list[i];
				for (int num14 = 0; num14 < ConfigPrefsController.unitStats_Roman_Siege_Price_Base.Length; num14++)
				{
					ConfigPrefsController.unitStats_Roman_Siege_Price_Base[num14] = Convert.ToInt32(jSONObject[num14].str);
				}
				break;
			case "roman_siege_price_multiplier":
				jSONObject = jsonMainObj.list[i];
				for (int num13 = 0; num13 < ConfigPrefsController.unitStats_Roman_Siege_Price_Multiplier.Length; num13++)
				{
					ConfigPrefsController.unitStats_Roman_Siege_Price_Multiplier[num13] = float.Parse(jSONObject[num13].str);
				}
				break;
			case "roman_siege_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Roman_Siege_Cost = float.Parse(jSONObject.str);
				break;
			case "italian_melee_cost ":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Italian_Melee_Cost = float.Parse(jSONObject.str);
				break;
			case "italian_ranged_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Italian_Ranged_Cost = float.Parse(jSONObject.str);
				break;
			case "italian_mounted_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Italian_Mounted_Cost = float.Parse(jSONObject.str);
				break;
			case "gaul_melee_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Gaul_Melee_Cost = float.Parse(jSONObject.str);
				break;
			case "gaul_spear_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Gaul_Spear_Cost = float.Parse(jSONObject.str);
				break;
			case "iberian_ranged_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Iberian_Ranged_Cost = float.Parse(jSONObject.str);
				break;
			case "iberian_mounted_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Iberian_Mounted_Cost = float.Parse(jSONObject.str);
				break;
			case "carthaginian_melee_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Carthaginian_Melee_Cost = float.Parse(jSONObject.str);
				break;
			case "carthaginian_spear_cost":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.unitStats_Carthaginian_Spear_Cost = float.Parse(jSONObject.str);
				break;
			case "wave_free_item":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.waveFreeItem = Convert.ToInt32(jSONObject.str);
				break;
			case "store_ruby_pack_amount":
				jSONObject = jsonMainObj.list[i];
				for (int num12 = 0; num12 < ConfigPrefsController.storeRubyPackAmount.Length; num12++)
				{
					ConfigPrefsController.storeRubyPackAmount[num12] = Convert.ToInt32(jSONObject[num12].str);
				}
				break;
			case "xp_boost_wave_duration":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.xpBoostWaveDuration = Convert.ToInt32(jSONObject.str);
				break;
			case "money_boost_wave_duration":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.moneyBoostWaveDuration = Convert.ToInt32(jSONObject.str);
				break;
			case "powers_boost_wave_duration":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.powersBoostWaveDuration = Convert.ToInt32(jSONObject.str);
				break;
			case "vip_boost_wave_duration":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.subscriptionBoostWaveDuration = Convert.ToInt32(jSONObject.str);
				break;
			case "boosts_price":
				jSONObject = jsonMainObj.list[i];
				for (int num11 = 0; num11 < ConfigPrefsController.boostsPrices.Length; num11++)
				{
					ConfigPrefsController.boostsPrices[num11] = Convert.ToInt32(jSONObject[num11].str);
				}
				break;
			case "upgrade_hero_rubies_price":
				jSONObject = jsonMainObj.list[i];
				for (int num10 = 0; num10 < ConfigPrefsController.upgradeHeroWithRubiesPrice.Length; num10++)
				{
					ConfigPrefsController.upgradeHeroWithRubiesPrice[num10] = Convert.ToInt32(jSONObject[num10].str);
				}
				break;
			case "minutes_to_fill":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.bankMinutesFull = Convert.ToInt32(jSONObject.str);
				break;
			case "minutes_to_fill_repeat":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.bankMinutesFullRepeat = Convert.ToInt32(jSONObject.str);
				break;
			case "bank_upgrade_multiplier":
				jSONObject = jsonMainObj.list[i];
				for (int num9 = 0; num9 < ConfigPrefsController.bankMultiplier.Length; num9++)
				{
					ConfigPrefsController.bankMultiplier[num9] = float.Parse(jSONObject[num9].str);
				}
				break;
			case "bank_upgrade_price":
				jSONObject = jsonMainObj.list[i];
				for (int num8 = 0; num8 < ConfigPrefsController.bankPrice.Length; num8++)
				{
					ConfigPrefsController.bankPrice[num8] = Convert.ToInt32(jSONObject[num8].str);
				}
				break;
			case "colonies_required_to_unlock_bank":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.bankColoniesRequiredToUnlock = Convert.ToInt32(jSONObject.str);
				break;
			case "free_gems":
				jSONObject = jsonMainObj.list[i];
				ConfigPrefsController.freeGems = Convert.ToInt32(jSONObject.str);
				break;
			case "wall_Lvl":
				jSONObject = jsonMainObj.list[i];
				for (int num7 = 0; num7 < ConfigPrefsController.wallLvl.Length; num7++)
				{
					ConfigPrefsController.wallLvl[num7] = int.Parse(jSONObject[num7].str);
				}
				break;
			case "tower_Lvl":
				jSONObject = jsonMainObj.list[i];
				for (int num6 = 0; num6 < ConfigPrefsController.towerLvl.Length; num6++)
				{
					ConfigPrefsController.towerLvl[num6] = Convert.ToInt32(jSONObject[num6].str);
				}
				break;
			case "catapult_Lvl":
				jSONObject = jsonMainObj.list[i];
				for (int num5 = 0; num5 < ConfigPrefsController.catapultLvl.Length; num5++)
				{
					ConfigPrefsController.catapultLvl[num5] = Convert.ToInt32(jSONObject[num5].str);
				}
				break;
			case "archers_Lvl":
				jSONObject = jsonMainObj.list[i];
				for (int num4 = 0; num4 < ConfigPrefsController.archersLvl.Length; num4++)
				{
					ConfigPrefsController.archersLvl[num4] = Convert.ToInt32(jSONObject[num4].str);
				}
				break;
			case "archers_Atk":
				jSONObject = jsonMainObj.list[i];
				for (int num3 = 0; num3 < ConfigPrefsController.archersStrength.Length; num3++)
				{
					ConfigPrefsController.archersStrength[num3] = Convert.ToInt32(jSONObject[num3].str);
				}
				break;
			case "units_Lvl":
				jSONObject = jsonMainObj.list[i];
				for (int num2 = 0; num2 < ConfigPrefsController.unitsLvl.Length; num2++)
				{
					ConfigPrefsController.unitsLvl[num2] = Convert.ToInt32(jSONObject[num2].str);
				}
				break;
			case "units_Skin":
				jSONObject = jsonMainObj.list[i];
				for (int n = 0; n < ConfigPrefsController.unitsSkin.Length; n++)
				{
					ConfigPrefsController.unitsSkin[n] = Convert.ToInt32(jSONObject[n].str);
				}
				break;
			case "spawn_time":
				jSONObject = jsonMainObj.list[i];
				for (int m = 0; m < ConfigPrefsController.spawnTime.Length; m++)
				{
					ConfigPrefsController.spawnTime[m] = float.Parse(jSONObject[m].str);
				}
				break;
			case "prespawned_units":
				jSONObject = jsonMainObj.list[i];
				for (int k = 0; k < ConfigPrefsController.prespawnedUnits.Length; k++)
				{
					ConfigPrefsController.prespawnedUnits[k] = Convert.ToInt32(jSONObject[k].str);
				}
				break;
			case "units_per_cohort":
				jSONObject = jsonMainObj.list[i];
				for (int j = 0; j < ConfigPrefsController.unitsPerCohort.Length; j++)
				{
					ConfigPrefsController.unitsPerCohort[j] = Convert.ToInt32(jSONObject[j].str);
				}
				break;
			case "country_code":
				jSONObject = jsonMainObj.list[i];
				PlayerPrefs.SetString("countryCode", jSONObject.str);
				PlayerPrefs.Save();
				break;
			case "experiment_id":
				jSONObject = jsonMainObj.list[i];
				PlayerPrefs.SetString("experiment_ID", jSONObject.str);
				PlayerPrefs.Save();
				break;
			case "enable_analytics":
				jSONObject = jsonMainObj.list[i];
				UnityEngine.Debug.LogWarning(ConfigPrefsController.enableAnalytics);
				break;
			}
		}
	}

	private void SaveJson(JSONObject jsonMainObj)
	{
		string value = jsonMainObj.Print();
		PlayerPrefs.SetString("localJsonId", value);
		PlayerPrefs.Save();
	}
}
