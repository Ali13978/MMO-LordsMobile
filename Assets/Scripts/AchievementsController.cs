using UnityEngine;

public class AchievementsController : MonoBehaviour
{
	private StationEngine stationEngine;

	private void Awake()
	{
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
	}

	public void CheckAchievementsMap()
	{
		bool flag = false;
		for (int i = 1; i < PlayerPrefsController.CitiesLevel.Length; i++)
		{
			if (PlayerPrefsController.CitiesLevel[i] >= 4)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			stationEngine.UnlockAchievement(0);
		}
		bool flag2 = false;
		bool flag3 = true;
		for (int j = 0; j < PlayerPrefsController.HeroeLvl.Length; j++)
		{
			if (PlayerPrefsController.HeroeLvl[j] > 0)
			{
				flag2 = true;
			}
			if (PlayerPrefsController.HeroeLvl[j] <= 0)
			{
				flag3 = false;
			}
		}
		if (flag2)
		{
			stationEngine.UnlockAchievement(5);
		}
		if (flag3)
		{
			stationEngine.UnlockAchievement(6);
		}
		bool flag4 = false;
		bool flag5 = true;
		for (int k = 0; k < PlayerPrefsController.UnitsTechMercenary.Length; k++)
		{
			if (PlayerPrefsController.UnitsTechMercenary[k])
			{
				flag4 = true;
			}
			if (!PlayerPrefsController.UnitsTechMercenary[k])
			{
				flag5 = false;
			}
		}
		if (flag4)
		{
			stationEngine.UnlockAchievement(3);
		}
		if (flag5)
		{
			stationEngine.UnlockAchievement(4);
		}
		bool flag6 = true;
		bool flag7 = true;
		bool flag8 = true;
		bool flag9 = true;
		for (int l = 1; l < PlayerPrefsController.CitiesConquered.Length; l++)
		{
			if (!PlayerPrefsController.CitiesConquered[l])
			{
				if (l >= 1 && l <= 33)
				{
					flag6 = false;
				}
				if (l >= 34 && l <= 55)
				{
					flag7 = false;
				}
				if (l >= 56 && l <= 77)
				{
					flag8 = false;
				}
				if (l >= 78)
				{
					flag9 = false;
				}
			}
		}
		if (flag6)
		{
			stationEngine.UnlockAchievement(7);
		}
		if (flag7)
		{
			stationEngine.UnlockAchievement(8);
		}
		if (flag8)
		{
			stationEngine.UnlockAchievement(9);
		}
		if (flag9)
		{
			stationEngine.UnlockAchievement(10);
		}
	}

	public void CheckAchievementsWave()
	{
		int @int = PlayerPrefs.GetInt("playerWave");
		if (@int >= 31)
		{
			stationEngine.UnlockAchievement(1);
		}
		if (@int >= 11)
		{
			stationEngine.UnlockAchievement(2);
		}
		if (@int >= 100)
		{
			stationEngine.UnlockAchievement(14);
		}
		if (@int >= 250)
		{
			stationEngine.UnlockAchievement(15);
		}
		if (@int >= 500)
		{
			stationEngine.UnlockAchievement(16);
		}
		if (@int >= 750)
		{
			stationEngine.UnlockAchievement(17);
		}
		if (@int >= 1000)
		{
			stationEngine.UnlockAchievement(18);
		}
		if (PlayerPrefsController.UnitsTechRanged[1] >= 3)
		{
			stationEngine.UnlockAchievement(11);
		}
		if (PlayerPrefsController.UnitsTechSpear[2] >= 3)
		{
			stationEngine.UnlockAchievement(12);
		}
		if (PlayerPrefsController.UnitsTechSpear[1] >= 3)
		{
			stationEngine.UnlockAchievement(13);
		}
	}
}
