using UnityEngine;

public static class EnemyPrefsController
{
	public static int LevelIndexSelected;

	public static int LevelPowerIndexSelected;

	public static CityTerrain TerrainSelected;

	public static Faction FactionSelected;

	public static int WallLvl;

	public static int ArchersLvl;

	public static int ArchersStrength;

	public static TowerAmmoType[] TowerAmmo = new TowerAmmoType[6];

	public static int TowerLvl;

	public static CatapultAmmoType[] CatapultAmmo = new CatapultAmmoType[2];

	public static int CatapultLvl;

	public static int[] UnitsPreSpawned = new int[6];

	public static int UnitsPreSpawnedTotalSoldiers;

	public static int UnitsPerCohort;

	public static int UnitsTechLevel;

	public static int UnitsSkin;

	public static CityCharacter cityCharacter;

	public static int cityCharacterIndex;

	public static bool tempUnlockedChara;

	public static float SpawnTimeUnits;

	public static void SetLevel(int _levelIndex, Faction _faction, int _levelPowerIndex, CityCharacter _cityCharacter, int _characterIndex, CityTerrain _terrainSelected)
	{
		LevelIndexSelected = _levelIndex;
		LevelPowerIndexSelected = _levelPowerIndex;
		FactionSelected = _faction;
		TerrainSelected = _terrainSelected;
		int num = _levelPowerIndex - (int)(_faction - 1) * 100;
		WallLvl = ConfigPrefsController.wallLvl[_levelIndex - 1];
		ArchersLvl = ConfigPrefsController.archersLvl[_levelIndex - 1];
		ArchersStrength = ConfigPrefsController.archersStrength[_levelIndex - 1];
		TowerLvl = ConfigPrefsController.towerLvl[_levelIndex - 1];
		CatapultLvl = ConfigPrefsController.catapultLvl[_levelIndex - 1];
		UnitsTechLevel = ConfigPrefsController.unitsLvl[_levelIndex - 1];
		UnitsSkin = ConfigPrefsController.unitsSkin[_levelIndex - 1];
		UnitsPreSpawnedTotalSoldiers = (int)Mathf.Lerp(0f, 48f, (float)ConfigPrefsController.prespawnedUnits[_levelIndex - 1] / 100f);
		UnitsPerCohort = ConfigPrefsController.unitsPerCohort[_levelIndex - 1];
		int num2 = (int)Mathf.Lerp(0.8f, 12.99f, (float)ConfigPrefsController.prespawnedUnits[_levelIndex - 1] / 100f);
		for (int i = 0; i < UnitsPreSpawned.Length; i++)
		{
			if ((float)num2 >= 1f)
			{
				if ((float)(num2 - (UnitsPreSpawned.Length - i - 1)) >= 2f)
				{
					UnitsPreSpawned[i] = 1;
					num2 -= 2;
				}
				else
				{
					UnitsPreSpawned[i] = 0;
					num2--;
				}
			}
			else
			{
				UnitsPreSpawned[i] = -1;
			}
		}
		cityCharacter = _cityCharacter;
		cityCharacterIndex = _characterIndex;
		SpawnTimeUnits = ConfigPrefsController.spawnTime[_levelIndex - 1];
		for (int j = 0; j < CatapultAmmo.Length; j++)
		{
			CatapultAmmo[j] = (CatapultAmmoType)Random.Range(0, 2);
		}
		for (int k = 0; k < TowerAmmo.Length; k++)
		{
			TowerAmmo[k] = (TowerAmmoType)Random.Range(0, 2);
		}
	}
}
