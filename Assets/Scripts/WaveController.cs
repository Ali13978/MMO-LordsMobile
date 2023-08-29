using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
	private float[] timeMiniWaves = new float[3]
	{
		48f,
		28f,
		8f
	};

	private float timeSeparationLines = 3f;

	private float timeSpawnBoss = 1f;

	private float timeComplement = 5f;

	private float timeWaveDurationPerCohort = 3.4f;

	private List<float> waveCohortTime = new List<float>();

	private List<Faction> waveCohortFaction = new List<Faction>();

	private List<int> waveCohortSize = new List<int>();

	private List<UnitType> waveCohortType = new List<UnitType>();

	private List<int> waveCohortPosition = new List<int>();

	private List<int> waveCohortLevel = new List<int>();

	private List<bool> waveCohortIsHero = new List<bool>();

	private bool isWavePlaying;

	private float waveTime;

	private float waveTimeActual;

	private UpgradesController upgradeController;

	private MainController mainController;

	private UIWaveController uiWaveController;

	private TutorialController tutorialController;

	public float[] TimeMiniWaves => timeMiniWaves;

	public List<Faction> WaveCohortFaction => waveCohortFaction;

	public bool IsWavePlaying => isWavePlaying;

	public float WaveTime => waveTime;

	public float WaveTimeActual => waveTimeActual;

	private void Awake()
	{
		mainController = base.gameObject.GetComponent<MainController>();
		upgradeController = base.gameObject.GetComponent<UpgradesController>();
		uiWaveController = base.gameObject.GetComponent<UIWaveController>();
		tutorialController = base.gameObject.GetComponent<TutorialController>();
		waveTime = 1f;
		waveTimeActual = 1f;
	}

	private void Update()
	{
		if (!isWavePlaying)
		{
			return;
		}
		waveTimeActual -= Time.deltaTime;
		if (waveCohortTime.Count > 0 && waveTimeActual <= waveCohortTime[0])
		{
			int num = 0;
			while (num < waveCohortTime.Count && waveTimeActual <= waveCohortTime[num])
			{
				if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
				{
					if (waveCohortIsHero[num])
					{
						int num2 = 0;
						switch (waveCohortType[num])
						{
						case UnitType.Archer:
							num2 = 1;
							break;
						case UnitType.Spear:
							num2 = 3;
							break;
						case UnitType.Sword:
							num2 = 2;
							break;
						case UnitType.Mounted:
							num2 = 0;
							break;
						}
						upgradeController.SpawnHero(_isInitialArmy: false, waveCohortPosition[num], num2, Vector3.zero, CohortStance.Attacker);
						upgradeController.SpawnUnitRoman(_isInitialArmy: false, waveCohortPosition[num], CohortStance.Attacker, waveCohortFaction[num], waveCohortType[num], _isHeroSpawning: true, num2, waveCohortLevel[num]);
					}
					else
					{
						upgradeController.SpawnUnitRoman(_isInitialArmy: false, waveCohortPosition[num], CohortStance.Attacker, waveCohortFaction[num], waveCohortType[num], _isHeroSpawning: false, 0, waveCohortLevel[num]);
					}
					waveCohortTime.RemoveAt(num);
					waveCohortFaction.RemoveAt(num);
					waveCohortSize.RemoveAt(num);
					waveCohortType.RemoveAt(num);
					waveCohortPosition.RemoveAt(num);
					waveCohortLevel.RemoveAt(num);
					waveCohortIsHero.RemoveAt(num);
				}
				else if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
				{
					if (waveCohortType[num] == UnitType.Siege)
					{
						upgradeController.SpawnBoss(waveCohortFaction[num], waveCohortLevel[num], waveCohortPosition[num], waveCohortIsHero[num]);
					}
					else
					{
						upgradeController.SpawnUnitBarbarian(CohortStance.Attacker, waveCohortFaction[num], waveCohortType[num], waveCohortLevel[num], waveCohortSize[num], waveCohortPosition[num]);
					}
					waveCohortTime.RemoveAt(num);
					waveCohortFaction.RemoveAt(num);
					waveCohortSize.RemoveAt(num);
					waveCohortType.RemoveAt(num);
					waveCohortPosition.RemoveAt(num);
					waveCohortLevel.RemoveAt(num);
					waveCohortIsHero.RemoveAt(num);
				}
				num--;
				if (waveCohortTime.Count == 0)
				{
					break;
				}
				num++;
			}
		}
		if (waveTimeActual <= 0f)
		{
			waveTimeActual = 0f;
			isWavePlaying = false;
		}
	}

	public void SetWaveRoman()
	{
		GameObject gameObject = GameObject.Find("WaveRoman");
		WaveRoman component = gameObject.GetComponent<WaveRoman>();
		waveCohortTime.Clear();
		waveCohortFaction.Clear();
		waveCohortSize.Clear();
		waveCohortType.Clear();
		waveCohortPosition.Clear();
		waveCohortLevel.Clear();
		waveCohortIsHero.Clear();
		int item = 2;
		if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[0] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[1])
		{
			item = 3;
		}
		else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[1] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[2])
		{
			item = 4;
		}
		else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[2] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[3])
		{
			item = 5;
		}
		else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[3] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[4])
		{
			item = 6;
		}
		else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[4] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[5])
		{
			item = 7;
		}
		else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[5])
		{
			item = 8;
		}
		int num = 0;
		for (int i = 0; i < component.UnitTypeSelected.Length; i++)
		{
			if (component.UnitTypeSelected[i] != 0)
			{
				if (i >= 0 && i <= 5)
				{
					waveCohortTime.Add(ConfigPrefsController.waveRomanEachTime[2]);
				}
				else if (i >= 6 && i <= 11)
				{
					waveCohortTime.Add(ConfigPrefsController.waveRomanEachTime[1]);
				}
				else if (i >= 12 && i <= 17)
				{
					waveCohortTime.Add(ConfigPrefsController.waveRomanEachTime[0]);
				}
				waveCohortFaction.Add(component.UnitsFactionSelected[i]);
				if (component.UnitTypeSelected[i] == ArmyType.Heroe)
				{
					waveCohortType.Add((UnitType)component.UnitIndexSelected[i]);
					waveCohortIsHero.Add(item: true);
				}
				else if (component.UnitTypeSelected[i] == ArmyType.Soldier)
				{
					waveCohortType.Add((UnitType)component.UnitIndexSelected[i]);
					waveCohortIsHero.Add(item: false);
				}
				else if (component.UnitTypeSelected[i] == ArmyType.Mercenary)
				{
					waveCohortType.Add((UnitType)component.UnitIndexSelected[i]);
					waveCohortIsHero.Add(item: false);
				}
				waveCohortSize.Add(item);
				waveCohortPosition.Add(num);
				waveCohortLevel.Add(component.UnitLevelSelected[i]);
			}
			num++;
			if (num > 5)
			{
				num = 0;
			}
		}
		UnityEngine.Object.Destroy(gameObject);
		for (int j = 0; j < waveCohortFaction.Count; j++)
		{
			if (!(waveCohortTime[j] >= ConfigPrefsController.waveRomanTime))
			{
				continue;
			}
			if (waveCohortIsHero[j])
			{
				int num2 = 0;
				switch (waveCohortType[j])
				{
				case UnitType.Archer:
					num2 = 1;
					break;
				case UnitType.Spear:
					num2 = 3;
					break;
				case UnitType.Sword:
					num2 = 2;
					break;
				case UnitType.Mounted:
					num2 = 0;
					break;
				}
				upgradeController.SpawnHero(_isInitialArmy: true, waveCohortPosition[j], num2, Vector3.zero, CohortStance.Attacker);
				upgradeController.SpawnUnitRoman(_isInitialArmy: true, waveCohortPosition[j], CohortStance.Attacker, waveCohortFaction[j], waveCohortType[j], _isHeroSpawning: true, num2, waveCohortLevel[j]);
			}
			else
			{
				upgradeController.SpawnUnitRoman(_isInitialArmy: true, waveCohortPosition[j], CohortStance.Attacker, waveCohortFaction[j], waveCohortType[j], _isHeroSpawning: false, 0, waveCohortLevel[j]);
			}
			waveCohortTime.RemoveAt(j);
			waveCohortFaction.RemoveAt(j);
			waveCohortSize.RemoveAt(j);
			waveCohortType.RemoveAt(j);
			waveCohortPosition.RemoveAt(j);
			waveCohortLevel.RemoveAt(j);
			waveCohortIsHero.RemoveAt(j);
			j--;
		}
		uiWaveController.SetPhaseAction(BarType.Invasion, Faction.Humans, null, null, null);
	}

	public void StartWaveRoman()
	{
		waveTime = ConfigPrefsController.waveRomanTime;
		waveTimeActual = waveTime;
		isWavePlaying = true;
	}

	public void StartWave()
	{
		int num = 1;
		bool flag = false;
		bool flag2 = false;
		if (PlayerPrefs.GetInt("playerWave") >= 5)
		{
			if (PlayerPrefs.GetInt("playerWave") % ConfigPrefsController.waveElephantPerWave == 0)
			{
				num = 2;
				flag2 = true;
			}
			else if (PlayerPrefs.GetInt("playerWave") % ConfigPrefsController.waveArmyPerWave == 0)
			{
				num = 1;
				flag2 = false;
				if (PlayerPrefs.GetInt("playerWave") % ConfigPrefsController.waveSiegePerWave == 0)
				{
					flag = true;
				}
			}
			else
			{
				num = 0;
				flag2 = true;
			}
		}
		else
		{
			num = 0;
			flag2 = true;
		}
		float num2 = (float)PlayerPrefs.GetInt("playerWave") * 3f;
		if (num == 1)
		{
			num2 *= 0.9f;
		}
		switch (num)
		{
		case 0:
		{
			CreateWaveArmy(flag2, num2, flag);
			UnitType[] array = new UnitType[2]
			{
				waveCohortType[0],
				UnitType.Archer
			};
			bool flag4 = false;
			for (int k = 0; k < waveCohortType.Count; k++)
			{
				if (waveCohortType[k] != array[0])
				{
					array[1] = waveCohortType[k];
					flag4 = true;
					break;
				}
			}
			if (!flag4)
			{
				array[1] = array[0];
			}
			if (PlayerPrefs.GetInt("playerWave") == 1)
			{
				for (int l = 0; l < waveCohortType.Count; l++)
				{
					waveCohortType[l] = UnitType.Spear;
				}
				array[0] = UnitType.Spear;
				array[1] = UnitType.Spear;
			}
			uiWaveController.SetPhaseAction(BarType.Horde, waveCohortFaction[0], array, null, null);
			break;
		}
		case 1:
		{
			CreateWaveArmy(flag2, num2, flag);
			UnitType[] array = new UnitType[2];
			UnitType[] array2 = new UnitType[2];
			UnitType[] array3 = (!flag) ? new UnitType[2] : new UnitType[3]
			{
				UnitType.Archer,
				UnitType.Archer,
				UnitType.Siege
			};
			for (int i = 0; i < 3; i++)
			{
				bool flag3 = false;
				for (int j = 0; j < waveCohortType.Count; j++)
				{
					if (waveCohortType[j] == UnitType.Siege || !(waveCohortTime[j] <= timeMiniWaves[i] + timeSeparationLines) || !(waveCohortTime[j] >= timeMiniWaves[i] - timeSeparationLines))
					{
						continue;
					}
					if (!flag3)
					{
						switch (i)
						{
						case 0:
							array[0] = waveCohortType[j];
							array[1] = waveCohortType[j];
							break;
						case 1:
							array2[0] = waveCohortType[j];
							array2[1] = waveCohortType[j];
							break;
						case 2:
							array3[0] = waveCohortType[j];
							array3[1] = waveCohortType[j];
							break;
						}
						flag3 = true;
						continue;
					}
					switch (i)
					{
					case 0:
						if (waveCohortType[j] != array[0])
						{
							array[1] = waveCohortType[j];
						}
						break;
					case 1:
						if (waveCohortType[j] != array2[0])
						{
							array2[1] = waveCohortType[j];
						}
						break;
					case 2:
						if (waveCohortType[j] != array3[0])
						{
							array2[1] = waveCohortType[j];
						}
						break;
					}
				}
			}
			uiWaveController.SetPhaseAction(BarType.Army, waveCohortFaction[0], array, array2, array3);
			break;
		}
		case 2:
			CreateWaveElephants();
			uiWaveController.SetPhaseAction(BarType.Elephants, Faction.Orcs, null, null, null);
			break;
		}
		isWavePlaying = true;
	}

	public void CreateWaveElephants()
	{
		waveCohortTime.Clear();
		waveCohortFaction.Clear();
		waveCohortSize.Clear();
		waveCohortType.Clear();
		waveCohortPosition.Clear();
		waveCohortLevel.Clear();
		waveCohortIsHero.Clear();
		Faction item = Faction.Orcs;
		int num = PlayerPrefs.GetInt("playerWave") / ConfigPrefsController.waveElephantPerWave;
		int num2 = 0;
		int num3 = num;
		do
		{
			num3 = ((num3 < 4) ? (num3 - 1) : (num3 - 4));
			num2++;
		}
		while (num3 > 0);
		float num4 = 12f;
		float num5 = 2f;
		waveTimeActual = (float)num2 * num4 + num5;
		int num6 = 0;
		for (int i = 0; i < num2; i++)
		{
			waveCohortTime.Add(waveTimeActual - (float)i * num4);
			waveCohortFaction.Add(item);
			waveCohortSize.Add(1);
			waveCohortType.Add(UnitType.Siege);
			waveCohortPosition.Add(num6);
			if (num >= 4)
			{
				waveCohortLevel.Add(1);
				num -= 4;
			}
			else
			{
				waveCohortLevel.Add(0);
				num--;
			}
			waveCohortIsHero.Add(item: true);
			num6++;
			if (num6 > 2)
			{
				num6 = 0;
			}
		}
		waveTime = waveTimeActual;
	}

	public void CreateWaveArmy(bool _isHorde, float _waveBudget, bool _spawnBoss)
	{
		waveCohortTime.Clear();
		waveCohortFaction.Clear();
		waveCohortSize.Clear();
		waveCohortType.Clear();
		waveCohortPosition.Clear();
		waveCohortLevel.Clear();
		waveCohortIsHero.Clear();
		int num = 0;
		for (int i = 1; i < ConfigPrefsController.waveBarbariansCost.Length - 1 && _waveBudget > (float)(ConfigPrefsController.waveBarbariansCost[i] * 144); i += 2)
		{
			num = i + 1;
		}
		Faction faction = Faction.Goblins;
		switch (num)
		{
		case 0:
			faction = Faction.Goblins;
			break;
		case 2:
			faction = Faction.Skeletons;
			break;
		case 4:
			faction = Faction.Wolves;
			break;
		case 6:
			faction = Faction.Orcs;
			break;
		}
		UnitType[] array = new UnitType[6];
		for (int j = 0; j < array.Length; j++)
		{
			int num2 = UnityEngine.Random.Range(0, 4);
			if (tutorialController.GetActualIndex() < 11)
			{
				switch (num2)
				{
				case 0:
					array[j] = UnitType.Archer;
					break;
				case 1:
					array[j] = UnitType.Archer;
					break;
				case 2:
					array[j] = UnitType.Spear;
					break;
				case 3:
					array[j] = UnitType.Spear;
					break;
				}
			}
			else
			{
				switch (num2)
				{
				case 0:
					array[j] = UnitType.Archer;
					break;
				case 1:
					array[j] = UnitType.Mounted;
					break;
				case 2:
					array[j] = UnitType.Spear;
					break;
				case 3:
					array[j] = UnitType.Sword;
					break;
				}
			}
		}
		for (int k = 0; k < 3; k++)
		{
			if (array[k * 2] != array[k * 2 + 1])
			{
				continue;
			}
			do
			{
				switch (UnityEngine.Random.Range(0, 4))
				{
				case 0:
					array[k * 2] = UnitType.Archer;
					break;
				case 1:
					array[k * 2] = UnitType.Mounted;
					break;
				case 2:
					array[k * 2] = UnitType.Spear;
					break;
				case 3:
					array[k * 2] = UnitType.Sword;
					break;
				}
			}
			while (array[k * 2] == array[k * 2 + 1]);
		}
		int num3 = 3;
		int num4 = 2;
		int num5 = 3;
		float[] array2 = new float[num3];
		array2[0] = _waveBudget / (float)num3;
		array2[1] = _waveBudget / (float)num3;
		array2[2] = _waveBudget / (float)num3;
		for (int l = 0; l < num3; l++)
		{
			if (l > 0 && array2[l - 1] > 0f)
			{
				array2[l] += array2[l - 1];
			}
			float[] array3 = new float[num4];
			array3[0] = array2[l] / (float)num4;
			array3[1] = array2[l] / (float)num4;
			for (int m = 0; m < num4; m++)
			{
				if (m > 0 && array3[m - 1] > 0f)
				{
					array3[m] += array2[m - 1];
				}
				float[] array4 = new float[num5];
				array4[0] = array3[m] / (float)num5;
				array4[1] = array3[m] / (float)num5;
				array4[2] = array3[m] / (float)num5;
				for (int n = 0; n < num5; n++)
				{
					if (n > 0 && array4[n - 1] > 0f)
					{
						array4[n] += array4[n - 1];
					}
					bool flag = false;
					if (array4[n] >= (float)(3 * ConfigPrefsController.waveBarbariansCost[num + 1]))
					{
						flag = true;
					}
					bool flag2 = false;
					if (flag && array4[n] >= (float)(8 * ConfigPrefsController.waveBarbariansCost[num]))
					{
						flag2 = true;
					}
					int num6 = 0;
					if (flag2)
					{
						num6 = 1;
					}
					else if (flag)
					{
						num6 = UnityEngine.Random.Range(0, 2);
					}
					int num7 = (int)array4[n] / ConfigPrefsController.waveBarbariansCost[num + num6];
					if (num7 > 8)
					{
						num7 = 8;
					}
					float num8 = num7 * ConfigPrefsController.waveBarbariansCost[num + num6];
					array2[l] -= num8;
					array3[m] -= num8;
					array4[n] -= num8;
					waveCohortTime.Add(timeMiniWaves[l] - timeSeparationLines * (float)m);
					waveCohortFaction.Add(faction);
					waveCohortSize.Add(num7);
					if (_isHorde)
					{
						waveCohortType.Add(array[Random.Range(0, 2)]);
					}
					else
					{
						waveCohortType.Add(array[l + m]);
					}
					waveCohortPosition.Add(n);
					waveCohortLevel.Add(num6);
					waveCohortIsHero.Add(item: false);
				}
			}
		}
		if (_spawnBoss)
		{
			waveCohortTime.Add(timeSpawnBoss);
			waveCohortFaction.Add(faction);
			waveCohortSize.Add(1);
			waveCohortType.Add(UnitType.Siege);
			waveCohortPosition.Add(0);
			switch (faction)
			{
			case Faction.Goblins:
				waveCohortLevel.Add(0);
				break;
			case Faction.Skeletons:
				waveCohortLevel.Add(1);
				break;
			case Faction.Wolves:
				waveCohortLevel.Add(2);
				break;
			case Faction.Orcs:
				waveCohortLevel.Add(3);
				break;
			}
			waveCohortIsHero.Add(item: false);
		}
		for (int num9 = 0; num9 < waveCohortSize.Count; num9++)
		{
			if (waveCohortSize[num9] <= 0)
			{
				waveCohortTime.RemoveAt(num9);
				waveCohortFaction.RemoveAt(num9);
				waveCohortSize.RemoveAt(num9);
				waveCohortType.RemoveAt(num9);
				waveCohortPosition.RemoveAt(num9);
				waveCohortLevel.RemoveAt(num9);
				waveCohortIsHero.RemoveAt(num9);
				num9--;
			}
		}
		if (_isHorde)
		{
			List<float> list = new List<float>();
			List<Faction> list2 = new List<Faction>();
			List<int> list3 = new List<int>();
			List<UnitType> list4 = new List<UnitType>();
			List<int> list5 = new List<int>();
			List<int> list6 = new List<int>();
			List<bool> list7 = new List<bool>();
			List<int> list8 = new List<int>();
			List<int> list9 = new List<int>();
			for (int num10 = 0; num10 < waveCohortTime.Count; num10++)
			{
				list8.Add(num10);
				list.Add(waveCohortTime[num10]);
				list2.Add(waveCohortFaction[num10]);
				list3.Add(waveCohortSize[num10]);
				list4.Add(waveCohortType[num10]);
				list5.Add(waveCohortPosition[num10]);
				list6.Add(waveCohortLevel[num10]);
				list7.Add(waveCohortIsHero[num10]);
			}
			waveCohortTime.Clear();
			waveCohortFaction.Clear();
			waveCohortSize.Clear();
			waveCohortType.Clear();
			waveCohortPosition.Clear();
			waveCohortLevel.Clear();
			waveCohortIsHero.Clear();
			list9 = Shuffle(list8);
			for (int num11 = 0; num11 < list9.Count; num11++)
			{
				waveCohortFaction.Add(list2[list9[num11]]);
				waveCohortSize.Add(list3[list9[num11]]);
				waveCohortType.Add(list4[list9[num11]]);
				waveCohortLevel.Add(list6[list9[num11]]);
				waveCohortIsHero.Add(list7[list9[num11]]);
			}
			waveTime = (float)list.Count * timeWaveDurationPerCohort;
			waveTimeActual = waveTime;
			float num12 = waveTime / (float)waveCohortFaction.Count;
			float num13 = waveTime;
			int num14 = -1;
			int num15 = waveCohortFaction.Count;
			do
			{
				num13 -= num12;
				num14++;
				if (num14 >= 3)
				{
					num14 = 0;
				}
				waveCohortTime.Add(num13);
				waveCohortPosition.Add(num14);
				num15--;
			}
			while (num13 > 0f && num15 > 0);
		}
		if (!_isHorde)
		{
			waveTimeActual = timeMiniWaves[0] + timeComplement;
			waveTime = waveTimeActual;
		}
	}

	private List<int> Shuffle(List<int> arrayToShuffle)
	{
		List<int> list = new List<int>();
		do
		{
			int index = UnityEngine.Random.Range(0, arrayToShuffle.Count);
			list.Add(arrayToShuffle[index]);
			arrayToShuffle.RemoveAt(index);
		}
		while (arrayToShuffle.Count > 0);
		return list;
	}

	public void FinishWave()
	{
		isWavePlaying = false;
		for (int i = 0; i < upgradeController.CohortsFriendArray.Count; i++)
		{
			if (upgradeController.CohortsFriendArray[i] != null)
			{
				UnityEngine.Object.Destroy(upgradeController.CohortsFriendArray[i].gameObject);
			}
		}
		upgradeController.CohortsFriendArray.Clear();
		for (int j = 0; j < upgradeController.CohortsEnemyArray.Count; j++)
		{
			if (upgradeController.CohortsEnemyArray[j] != null)
			{
				UnityEngine.Object.Destroy(upgradeController.CohortsEnemyArray[j].gameObject);
			}
		}
		upgradeController.CohortsEnemyArray.Clear();
		waveTime = 1f;
		waveTimeActual = 1f;
		UpdateWaveBarAtStart();
	}

	private void UpdateWaveBarAtStart()
	{
		float num = (float)PlayerPrefs.GetInt("playerWave") * 3f;
		if (PlayerPrefs.GetInt("playerWave") % ConfigPrefsController.waveArmyPerWave == 0)
		{
			num *= 0.9f;
		}
		int num2 = 0;
		for (int i = 1; i < ConfigPrefsController.waveBarbariansCost.Length - 1 && num > (float)(ConfigPrefsController.waveBarbariansCost[i] * 144); i += 2)
		{
			num2 = i + 1;
		}
		Faction enemyFactionToShow = Faction.Goblins;
		switch (num2)
		{
		case 0:
			enemyFactionToShow = Faction.Goblins;
			break;
		case 2:
			enemyFactionToShow = Faction.Skeletons;
			break;
		case 4:
			enemyFactionToShow = Faction.Wolves;
			break;
		case 6:
			enemyFactionToShow = Faction.Orcs;
			break;
		}
		uiWaveController.ShowEnemyIcon(enemyFactionToShow);
	}
}
