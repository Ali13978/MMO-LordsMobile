using I2.Loc;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
	public static bool isWarcryActive;

	public static bool isExtraMoneyActive;

	public static bool isExtraDamageActive;

	public static float extraDamageAmount;

	public Transform attackInvasionTransform;

	public Transform attackTransform;

	public Transform defenceTransform;

	public Transform bankTransform;

	public Transform wallTransform;

	private GameObject wallObject;

	private GateController gateController;

	public Transform wallArchersLeftParentTransform;

	public Transform wallArchersRightParentTransform;

	public Transform[] wallArchersTransform;

	private GameObject[] wallArchersObject;

	public Transform[] heroesTransform;

	private List<GameObject> heroesObject = new List<GameObject>();

	public Transform[] towersTransform;

	public Transform[] goblinsTowersTransform;

	public Transform[] skeletonsTowersTransform;

	public Transform[] orcsTowersTransform;

	public Transform[] wolvesTowersTransform;

	public Transform[] towersAttackTransform;

	private GameObject[] towersObject;

	private TowerController[] towersController;

	private float towerTimeReload;

	private float towerTimeReloadCounter;

	public Transform[] catapultsTransform;

	private GameObject[] catapultsObject;

	private CatapultController[] catapultsController;

	public Transform[] bannersTransform;

	private GameObject[] bannersObject;

	public Transform[] generalTransform;

	public Transform generalAttackTransform;

	private GameObject generalObject;

	private WallGeneral wallGeneral;

	private GameObject[] torchsObject;

	public Transform pitchTransform;

	public Transform[] preSpawnTransform;

	private GameObject backgroundObject;

	private GameObject pitchObject;

	private GameObject bridgeObject;

	private GameObject bridgeComplementObject;

	private GameObject bankObject;

	private GameObject[] stakesObject;

	private List<Cohort> cohortsEnemyArray = new List<Cohort>();

	private List<Cohort> cohortsFriendArray = new List<Cohort>();

	private float timeWarcry;

	private float timeWarcryActual;

	private float timeSpawn;

	private float enemyTimeSpawn;

	public AudioClip sfxPowerWarCry;

	private MainController mainController;

	private UIController uiController;

	private AudioSource audioSource;

	private int wallLvlToCheck;

	private int wallLvl;

	private float wallAltitude;

	private float cameraAltitude;

	private List<ParticleSystem> particlesWarCryArray = new List<ParticleSystem>();

	public GateController GateController => gateController;

	public TowerController[] TowersController => towersController;

	public float TowerTimeReload
	{
		get
		{
			return towerTimeReload;
		}
		set
		{
			towerTimeReload = value;
		}
	}

	public float TowerTimeReloadCounter
	{
		get
		{
			return towerTimeReloadCounter;
		}
		set
		{
			towerTimeReloadCounter = value;
		}
	}

	public CatapultController[] CatapultsController => catapultsController;

	public List<Cohort> CohortsEnemyArray
	{
		get
		{
			return cohortsEnemyArray;
		}
		set
		{
			cohortsEnemyArray = value;
		}
	}

	public List<Cohort> CohortsFriendArray
	{
		get
		{
			return cohortsFriendArray;
		}
		set
		{
			cohortsFriendArray = value;
		}
	}

	public float TimeWarcry => timeWarcry;

	public float TimeWarcryActual
	{
		get
		{
			return timeWarcryActual;
		}
		set
		{
			timeWarcryActual = value;
		}
	}

	public float TimeSpawn
	{
		get
		{
			return timeSpawn;
		}
		set
		{
			timeSpawn = value;
		}
	}

	public float EnemyTimeSpawn
	{
		get
		{
			return enemyTimeSpawn;
		}
		set
		{
			enemyTimeSpawn = value;
		}
	}

	private void Awake()
	{
		uiController = base.gameObject.GetComponent<UIController>();
		audioSource = base.gameObject.GetComponent<AudioSource>();
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			UnityEngine.Object.Destroy(attackTransform.gameObject);
			attackTransform = attackInvasionTransform;
		}
		else
		{
			UnityEngine.Object.Destroy(attackInvasionTransform.gameObject);
		}
	}

	private void Start()
	{
		if (uiController != null)
		{
			uiController.SetUpgradeButtonsDependingWallHeight(wallLvl);
		}
	}

	public void SetInitialStructure(bool _setHeroes)
	{
		if (mainController == null)
		{
			mainController = base.gameObject.GetComponent<MainController>();
		}
		if (MainController.worldScreen == WorldScreen.Attack)
		{
			wallLvlToCheck = EnemyPrefsController.WallLvl;
		}
		else
		{
			wallLvlToCheck = PlayerPrefsController.WallLvl;
		}
		SetWall();
		SetBackground();
		SetCameraHeight();
		SetArchers();
		SetTowers();
		SetBank();
		SetCatapults();
		SetGeneral();
		if (_setHeroes && (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade))
		{
			SetHeroes();
		}
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			SetPreSpawnedEnemies();
		}
		mainController.SetWallLife();
		if (uiController != null)
		{
			uiController.SetUpgradeButtonsDependingWallHeight(wallLvl);
		}
	}

	private void SetBank()
	{
		int num = 0;
		for (int i = 1; i < PlayerPrefsController.CitiesConquered.Length; i++)
		{
			if (PlayerPrefsController.CitiesConquered[i])
			{
				num++;
			}
			if (num >= ConfigPrefsController.bankColoniesRequiredToUnlock)
			{
				break;
			}
		}
		if (num >= ConfigPrefsController.bankColoniesRequiredToUnlock)
		{
			if (bankObject != null)
			{
				UnityEngine.Object.Destroy(bankObject);
			}
			if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
			{
				bankObject = (UnityEngine.Object.Instantiate(Resources.Load("Bank/Bank_"), bankTransform.position, Quaternion.Euler(0f, -45f, 0f)) as GameObject);
			}
		}
	}

	private void SetWall()
	{
		if (wallLvlToCheck <= 5)
		{
			wallLvl = 0;
			wallAltitude = 0f;
			cameraAltitude = 18.5f;
		}
		else if (wallLvlToCheck >= 6 && wallLvlToCheck <= 35)
		{
			wallLvl = 1;
			wallAltitude = 0.71f;
			cameraAltitude = 18.75f;
		}
		else if (wallLvlToCheck >= 36 && wallLvlToCheck <= 95)
		{
			wallLvl = 2;
			wallAltitude = 0.79f;
			cameraAltitude = 18.85f;
		}
		else if (wallLvlToCheck >= 96)
		{
			wallLvl = 3;
			wallAltitude = 1.89f;
			cameraAltitude = 19.75f;
		}
		if (wallObject != null)
		{
			UnityEngine.Object.Destroy(wallObject);
		}
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			wallObject = (UnityEngine.Object.Instantiate(Resources.Load("Walls/WallLvl" + wallLvl.ToString()), wallTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		else if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			wallAltitude = 1.04f;
			cameraAltitude = 18.9f;
			switch (EnemyPrefsController.FactionSelected)
			{
			case Faction.Goblins:
				wallAltitude = 1.37f;
				wallObject = (UnityEngine.Object.Instantiate(Resources.Load("WallsEnemy/Wall_Goblins_Lvl0"), wallTransform.position, Quaternion.Euler(new Vector3(0f, 180f, 0f))) as GameObject);
				break;
			case Faction.Skeletons:
				wallAltitude = 0.92f;
				wallObject = (UnityEngine.Object.Instantiate(Resources.Load("WallsEnemy/Wall_Skeletons_Lvl0"), wallTransform.position, Quaternion.Euler(new Vector3(0f, 180f, 0f))) as GameObject);
				break;
			case Faction.Wolves:
				wallAltitude = 0.92f;
				wallObject = (UnityEngine.Object.Instantiate(Resources.Load("WallsEnemy/Wall_Wolves_Lvl0"), wallTransform.position, Quaternion.Euler(new Vector3(0f, 180f, 0f))) as GameObject);
				break;
			case Faction.Orcs:
				wallAltitude = 0.98f;
				wallObject = (UnityEngine.Object.Instantiate(Resources.Load("WallsEnemy/Wall_Orcs_Lvl0"), wallTransform.position, Quaternion.Euler(new Vector3(0f, 180f, 0f))) as GameObject);
				break;
			}
		}
		wallObject.transform.SetParent(wallTransform);
		gateController = wallObject.GetComponent<GateController>();
		gateController.SetWallUpgrades();
	}

	private void SetBackground()
	{
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			int num = (int)Mathf.Lerp(0f, 17f, (float)PlayerPrefsController.WallLvl / 170f);
			if (backgroundObject != null)
			{
				UnityEngine.Object.Destroy(backgroundObject);
			}
			backgroundObject = (UnityEngine.Object.Instantiate(Resources.Load("Scenarios/Back_Human_" + num.ToString()), new Vector3(35f, 0f, 1.3f), Quaternion.Euler(Vector3.zero)) as GameObject);
		}
	}

	private void SetCameraHeight()
	{
		Transform transform = (MainController.worldScreen == WorldScreen.Attack) ? GameObject.FindGameObjectWithTag("CameraAttack").GetComponent<Transform>() : GameObject.FindGameObjectWithTag("CameraDefence").GetComponent<Transform>();
		Transform transform2 = transform;
		Vector3 position = transform.position;
		float x = position.x;
		float y = cameraAltitude;
		Vector3 position2 = transform.position;
		transform2.position = new Vector3(x, y, position2.z);
	}

	private void SetArchers()
	{
		int num = 0;
		num = ((MainController.worldScreen != WorldScreen.Attack) ? PlayerPrefsController.ArchersLvl : EnemyPrefsController.ArchersLvl);
		wallArchersLeftParentTransform.position = new Vector3(0f, wallAltitude, 0f);
		wallArchersRightParentTransform.position = new Vector3(0f, wallAltitude, 0f);
		int num2 = wallArchersTransform.Length;
		int num3 = 5;
		if (wallArchersObject == null)
		{
			wallArchersObject = new GameObject[num2];
		}
		if (MainController.worldScreen == WorldScreen.Attack)
		{
			num2 = (int)Mathf.Lerp(0f, 20f, (float)EnemyPrefsController.ArchersLvl / 100f);
			for (int i = 0; i < num2; i++)
			{
				int unitsSkin = EnemyPrefsController.UnitsSkin;
				if (wallArchersObject[i] != null)
				{
					UnityEngine.Object.Destroy(wallArchersObject[i]);
				}
				string str = "Soldiers/";
				switch (EnemyPrefsController.FactionSelected)
				{
				case Faction.Goblins:
					str += "Italian";
					break;
				case Faction.Skeletons:
					str += "Gaul";
					break;
				case Faction.Wolves:
					str += "Iberian";
					break;
				case Faction.Orcs:
					str += "Carthaginian";
					break;
				}
				str = str + "/WallArcher/WallArcher_" + unitsSkin.ToString();
				wallArchersObject[i] = (UnityEngine.Object.Instantiate(Resources.Load(str), wallArchersTransform[i].position, Quaternion.Euler(Vector3.zero)) as GameObject);
			}
			return;
		}
		for (int j = 0; j < num2; j++)
		{
			int num4 = -1;
			if (num > j)
			{
				for (int k = 0; k < num3; k++)
				{
					if (num < j + 1 + (k + 1) * num2)
					{
						num4 = k;
						break;
					}
				}
			}
			if (num4 >= 0)
			{
				if (wallArchersObject[j] != null)
				{
					UnityEngine.Object.Destroy(wallArchersObject[j]);
				}
				wallArchersObject[j] = (UnityEngine.Object.Instantiate(Resources.Load("Soldiers/Roman/WallArcher/WallArcher_" + num4.ToString()), wallArchersTransform[j].position, Quaternion.Euler(Vector3.zero)) as GameObject);
			}
		}
	}

	private void SetTowers()
	{
		towerTimeReload = ConfigPrefsController.cooldownTower;
		int[] array = new int[6];
		if (wallLvlToCheck >= 3 && wallLvlToCheck < 8)
		{
			array[0] = 0;
		}
		else if (wallLvlToCheck >= 8 && wallLvlToCheck < 40)
		{
			array[0] = 1;
		}
		else if (wallLvlToCheck >= 40 && wallLvlToCheck < 102)
		{
			array[0] = 2;
		}
		else if (wallLvlToCheck >= 102)
		{
			array[0] = 3;
		}
		else
		{
			array[0] = -1;
		}
		if (wallLvlToCheck >= 12 && wallLvlToCheck < 44)
		{
			array[1] = 1;
		}
		else if (wallLvlToCheck >= 44 && wallLvlToCheck < 108)
		{
			array[1] = 2;
		}
		else if (wallLvlToCheck >= 108)
		{
			array[1] = 3;
		}
		else
		{
			array[1] = -1;
		}
		if (wallLvlToCheck >= 30 && wallLvlToCheck < 48)
		{
			array[2] = 1;
		}
		else if (wallLvlToCheck >= 48 && wallLvlToCheck < 114)
		{
			array[2] = 2;
		}
		else if (wallLvlToCheck >= 114)
		{
			array[2] = 3;
		}
		else
		{
			array[2] = -1;
		}
		if (wallLvlToCheck >= 68 && wallLvlToCheck < 120)
		{
			array[3] = 2;
		}
		else if (wallLvlToCheck >= 120)
		{
			array[3] = 3;
		}
		else
		{
			array[3] = -1;
		}
		if (wallLvlToCheck >= 88 && wallLvlToCheck < 126)
		{
			array[4] = 2;
		}
		else if (wallLvlToCheck >= 126)
		{
			array[4] = 3;
		}
		else
		{
			array[4] = -1;
		}
		if (wallLvlToCheck >= 144)
		{
			array[5] = 3;
		}
		else
		{
			array[5] = -1;
		}
		if (towersObject == null)
		{
			towersObject = new GameObject[towersTransform.Length];
		}
		if (towersController == null)
		{
			towersController = new TowerController[towersTransform.Length];
		}
		for (int i = 0; i < towersTransform.Length; i++)
		{
			if (towersObject[i] != null)
			{
				UnityEngine.Object.Destroy(towersObject[i]);
			}
			if (array[i] < 0)
			{
				continue;
			}
			if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
			{
				string str = "TowersEnemy/Tower_";
				switch (EnemyPrefsController.FactionSelected)
				{
				case Faction.Goblins:
					str += "Goblins_0";
					towersTransform[i].position = goblinsTowersTransform[i].position;
					towersObject[i] = (UnityEngine.Object.Instantiate(Resources.Load(str), goblinsTowersTransform[i].position, Quaternion.Euler(new Vector3(0f, 90f, 0f))) as GameObject);
					break;
				case Faction.Skeletons:
					str += "Skeletons_0";
					towersTransform[i] = skeletonsTowersTransform[i];
					towersObject[i] = (UnityEngine.Object.Instantiate(Resources.Load(str), skeletonsTowersTransform[i].position, Quaternion.Euler(new Vector3(0f, 90f, 0f))) as GameObject);
					break;
				case Faction.Wolves:
					str += "Wolves_0";
					towersTransform[i] = wolvesTowersTransform[i];
					towersObject[i] = (UnityEngine.Object.Instantiate(Resources.Load(str), wolvesTowersTransform[i].position, Quaternion.Euler(new Vector3(0f, 90f, 0f))) as GameObject);
					break;
				case Faction.Orcs:
					str += "Orcs_0";
					towersTransform[i] = orcsTowersTransform[i];
					towersObject[i] = (UnityEngine.Object.Instantiate(Resources.Load(str), orcsTowersTransform[i].position, Quaternion.Euler(new Vector3(0f, 90f, 0f))) as GameObject);
					break;
				}
			}
			else
			{
				towersObject[i] = (UnityEngine.Object.Instantiate(Resources.Load("Towers/Tower_" + array[i].ToString()), towersTransform[i].position, Quaternion.Euler(new Vector3(0f, -90f, 0f))) as GameObject);
			}
			towersController[i] = towersObject[i].transform.Find("ballista").GetComponent<TowerController>();
			if (MainController.worldScreen == WorldScreen.Attack)
			{
				towersController[i].ballistaSelected = EnemyPrefsController.TowerAmmo[i];
			}
			else
			{
				towersController[i].ballistaSelected = PlayerPrefsController.TowerAmmo[i];
			}
			towersController[i].SetInitialStatus();
		}
	}

	private void SetCatapults()
	{
		if (catapultsObject == null)
		{
			catapultsObject = new GameObject[catapultsTransform.Length];
		}
		if (catapultsController == null)
		{
			catapultsController = new CatapultController[catapultsTransform.Length];
		}
		if (catapultsObject[0] != null)
		{
			UnityEngine.Object.Destroy(catapultsObject[0]);
		}
		if (catapultsObject[1] != null)
		{
			UnityEngine.Object.Destroy(catapultsObject[1]);
		}
		if (wallLvlToCheck >= 21)
		{
			catapultsObject[0] = (UnityEngine.Object.Instantiate(Resources.Load("Catapults/Catapult_0"), catapultsTransform[0].position, Quaternion.Euler(Vector3.zero)) as GameObject);
			catapultsController[0] = catapultsObject[0].GetComponent<CatapultController>();
			if (MainController.worldScreen == WorldScreen.Attack)
			{
				catapultsController[0].catapultSelected = EnemyPrefsController.CatapultAmmo[0];
			}
			else
			{
				catapultsController[0].catapultSelected = PlayerPrefsController.CatapultAmmo[0];
			}
			catapultsController[0].SetInitialStatus();
		}
		if (wallLvlToCheck >= 78)
		{
			catapultsObject[1] = (UnityEngine.Object.Instantiate(Resources.Load("Catapults/Catapult_0"), catapultsTransform[1].position, Quaternion.Euler(Vector3.zero)) as GameObject);
			catapultsController[1] = catapultsObject[1].GetComponent<CatapultController>();
			if (MainController.worldScreen == WorldScreen.Attack)
			{
				catapultsController[1].catapultSelected = EnemyPrefsController.CatapultAmmo[1];
			}
			else
			{
				catapultsController[1].catapultSelected = PlayerPrefsController.CatapultAmmo[1];
			}
			catapultsController[1].SetInitialStatus();
		}
	}

	private void SetTorchs()
	{
		if (torchsObject == null)
		{
			torchsObject = new GameObject[2];
		}
		if (torchsObject[0] != null)
		{
			UnityEngine.Object.Destroy(torchsObject[0]);
		}
		if (torchsObject[1] != null)
		{
			UnityEngine.Object.Destroy(torchsObject[1]);
		}
		if (wallLvlToCheck >= 14)
		{
			torchsObject[0] = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Torch_0"), Vector3.zero, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		if (wallLvlToCheck >= 60)
		{
			torchsObject[1] = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Torch_1"), Vector3.zero, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
	}

	private void SetDitch()
	{
		if (stakesObject == null)
		{
			stakesObject = new GameObject[3];
		}
		if (pitchObject != null)
		{
			UnityEngine.Object.Destroy(pitchObject);
		}
		if (bridgeObject != null)
		{
			UnityEngine.Object.Destroy(bridgeObject);
		}
		if (bridgeComplementObject != null)
		{
			UnityEngine.Object.Destroy(bridgeComplementObject);
		}
		if (stakesObject[0] != null)
		{
			UnityEngine.Object.Destroy(stakesObject[0]);
		}
		if (stakesObject[1] != null)
		{
			UnityEngine.Object.Destroy(stakesObject[1]);
		}
		if (stakesObject[2] != null)
		{
			UnityEngine.Object.Destroy(stakesObject[2]);
		}
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			int num = 0;
			switch (EnemyPrefsController.TerrainSelected)
			{
			case CityTerrain.Orcs:
				num = 1;
				break;
			case CityTerrain.Goblins:
				num = 0;
				break;
			case CityTerrain.Wolves:
				num = 0;
				break;
			case CityTerrain.Skeletons:
				num = 2;
				break;
			}
			if (wallLvlToCheck < 4)
			{
				pitchObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Trench_0_Inverted_" + num.ToString()), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
			}
			else if (wallLvlToCheck >= 4 && wallLvlToCheck < 22)
			{
				pitchObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Trench_1_Inverted_" + num.ToString()), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
			}
			else if (wallLvlToCheck >= 22)
			{
				pitchObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Trench_2_Inverted_" + num.ToString()), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
			}
		}
		else if (wallLvlToCheck < 4)
		{
			pitchObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Trench_0"), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		else if (wallLvlToCheck >= 4 && wallLvlToCheck < 22)
		{
			pitchObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Trench_1"), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		else if (wallLvlToCheck >= 22)
		{
			pitchObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Trench_2"), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		if (wallLvlToCheck >= 22 && wallLvlToCheck < 52)
		{
			bridgeObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Bridge_0"), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		else if (wallLvlToCheck >= 52 && wallLvlToCheck < 138)
		{
			bridgeObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Bridge_1"), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		else if (wallLvlToCheck >= 138)
		{
			bridgeObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Bridge_2"), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		if (wallLvlToCheck >= 26 && wallLvlToCheck < 52)
		{
			bridgeComplementObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Bridge_0_Complement"), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		else if (wallLvlToCheck >= 72 && wallLvlToCheck < 138)
		{
			bridgeComplementObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Bridge_1_Complement"), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		else if (wallLvlToCheck >= 162)
		{
			bridgeComplementObject = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Bridge_2_Complement"), pitchTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		if (wallLvlToCheck >= 16)
		{
			stakesObject[0] = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Stake_0"), pitchTransform.position + new Vector3(-0.2f, 0f, 0f), Quaternion.Euler(new Vector3(-90f, 0f, 0f))) as GameObject);
		}
		if (wallLvlToCheck >= 34)
		{
			stakesObject[1] = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Stake_1"), pitchTransform.position + new Vector3(-0.7f, 0f, 0f), Quaternion.Euler(new Vector3(-90f, 0f, 0f))) as GameObject);
		}
		if (wallLvlToCheck >= 84)
		{
			stakesObject[2] = (UnityEngine.Object.Instantiate(Resources.Load("Pitch/Stake_2"), pitchTransform.position + new Vector3(0.16f, 0f, 0f), Quaternion.Euler(Vector3.zero)) as GameObject);
		}
	}

	private void SetGeneral()
	{
		int num = wallLvl;
		if (wallLvlToCheck >= 150)
		{
			num++;
		}
		if (generalObject != null)
		{
			UnityEngine.Object.Destroy(generalObject);
		}
		int num2 = 0;
		if (PlayerPrefsController.playerLevel < 15)
		{
			num2 = 0;
		}
		else if (PlayerPrefsController.playerLevel >= 15 && PlayerPrefsController.playerLevel < 25)
		{
			num2 = 1;
		}
		else if (PlayerPrefsController.playerLevel >= 25 && PlayerPrefsController.playerLevel < 35)
		{
			num2 = 2;
		}
		else if (PlayerPrefsController.playerLevel >= 35)
		{
			num2 = 3;
		}
		if (MainController.worldScreen == WorldScreen.Attack)
		{
			generalObject = (UnityEngine.Object.Instantiate(Resources.Load("Soldiers/General/General_" + num2.ToString()), generalAttackTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		else
		{
			generalObject = (UnityEngine.Object.Instantiate(Resources.Load("Soldiers/General/General_" + num2.ToString()), generalTransform[num].position, Quaternion.Euler(Vector3.zero)) as GameObject);
		}
		wallGeneral = generalObject.GetComponent<WallGeneral>();
	}

	public void SetHeroes()
	{
		for (int i = 0; i < PlayerPrefsController.HeroeSelected.Length; i++)
		{
			if (PlayerPrefsController.HeroeSelected[i] != 0)
			{
				continue;
			}
			for (int j = 0; j < PlayerPrefsController.HeroeLvl.Length; j++)
			{
				if (PlayerPrefsController.HeroeLvl[j] > 0)
				{
					if (i == 0 && PlayerPrefsController.HeroeSelected[1] != (HeroeType)(j + 1))
					{
						PlayerPrefsController.HeroeSelected[0] = (HeroeType)(j + 1);
					}
					else if (i == 1 && PlayerPrefsController.HeroeSelected[0] != (HeroeType)(j + 1))
					{
						PlayerPrefsController.HeroeSelected[1] = (HeroeType)(j + 1);
					}
				}
			}
		}
		isExtraMoneyActive = false;
		if (PlayerPrefsController.HeroeSelected[0] == HeroeType.Arcani || PlayerPrefsController.HeroeSelected[1] == HeroeType.Arcani)
		{
			isExtraMoneyActive = true;
		}
		if (heroesObject.Count == 0)
		{
			heroesObject = new List<GameObject>();
		}
		for (int k = 0; k < cohortsFriendArray.Count; k++)
		{
			UnityEngine.Object.Destroy(cohortsFriendArray[k].gameObject);
		}
		cohortsFriendArray.Clear();
		if (heroesObject.Count > 0)
		{
			for (int l = 0; l < heroesObject.Count; l++)
			{
				UnityEngine.Object.Destroy(heroesObject[l]);
			}
			heroesObject.Clear();
		}
		for (int m = 0; m < PlayerPrefsController.HeroeSelected.Length; m++)
		{
			if (PlayerPrefsController.HeroeSelected[m] != 0)
			{
				SpawnHero(_isInitialArmy: false, 0, (int)(PlayerPrefsController.HeroeSelected[m] - 1), heroesTransform[m].position, CohortStance.Defender);
				switch (PlayerPrefsController.HeroeSelected[m])
				{
				case HeroeType.Arcani:
					SpawnUnitRoman(_isInitialArmy: false, 0, CohortStance.Defender, Faction.Humans, UnitType.Sword, _isHeroSpawning: true, m, 0);
					break;
				case HeroeType.BarbarianCommander:
					SpawnUnitRoman(_isInitialArmy: false, 0, CohortStance.Defender, Faction.Humans, UnitType.Spear, _isHeroSpawning: true, m, 0);
					break;
				case HeroeType.Gladiator:
					SpawnUnitRoman(_isInitialArmy: false, 0, CohortStance.Defender, Faction.Humans, UnitType.Archer, _isHeroSpawning: true, m, 0);
					break;
				case HeroeType.RomanCommander:
					SpawnUnitRoman(_isInitialArmy: false, 0, CohortStance.Defender, Faction.Humans, UnitType.Mounted, _isHeroSpawning: true, m, 0);
					break;
				}
			}
		}
	}

	private void SetPreSpawnedEnemies()
	{
		int num = 0;
		for (int i = 0; i < EnemyPrefsController.UnitsPreSpawned.Length; i++)
		{
			if (EnemyPrefsController.UnitsPreSpawned[i] >= 0)
			{
				num++;
			}
		}
		for (int j = 0; j < EnemyPrefsController.UnitsPreSpawned.Length; j++)
		{
			if (EnemyPrefsController.UnitsPreSpawned[j] >= 0)
			{
				Vector3 euler = new Vector3(0f, 0f, 0f);
				Vector3 position = preSpawnTransform[j].position;
				string str = "Soldiers/";
				switch (EnemyPrefsController.FactionSelected)
				{
				case Faction.Goblins:
					str += "Italian/";
					break;
				case Faction.Skeletons:
					str += "Gaul/";
					break;
				case Faction.Wolves:
					str += "Iberian/";
					break;
				case Faction.Orcs:
					str += "Carthaginian/";
					break;
				}
				int num2 = UnityEngine.Random.Range(0, 4);
				switch (num2)
				{
				case 0:
					str += "Ranged/Cohort_Ranged_";
					break;
				case 1:
					str += "Mounted/Cohort_Mounted_";
					break;
				case 2:
					str += "Spear/Cohort_Spear_";
					break;
				case 3:
					str += "Melee/Cohort_Melee_";
					break;
				}
				int unitsSkin = EnemyPrefsController.UnitsSkin;
				GameObject gameObject = null;
				gameObject = (UnityEngine.Object.Instantiate(Resources.Load(str + unitsSkin.ToString()), position, Quaternion.Euler(euler)) as GameObject);
				Cohort component = gameObject.GetComponent<Cohort>();
				float hp = 0f;
				float atk = 0f;
				float ratk = 0f;
				float def = 0f;
				int unitsTechLevel = EnemyPrefsController.UnitsTechLevel;
				switch (num2)
				{
				case 0:
					hp = ConfigPrefsController.GetUnitsBarbarianHealth(EnemyPrefsController.FactionSelected, CohortType.Ranged, CohortSubType.Javaline, unitsSkin, unitsTechLevel);
					atk = ConfigPrefsController.GetUnitsBarbarianAttack(EnemyPrefsController.FactionSelected, CohortType.Ranged, CohortSubType.Javaline, unitsSkin, unitsTechLevel);
					ratk = ConfigPrefsController.GetUnitsBarbarianRangedAttack(EnemyPrefsController.FactionSelected, CohortType.Ranged, CohortSubType.Javaline, unitsSkin, unitsTechLevel);
					def = ConfigPrefsController.GetUnitsBarbarianDefence(EnemyPrefsController.FactionSelected, CohortType.Ranged, CohortSubType.Javaline, unitsSkin, unitsTechLevel);
					break;
				case 1:
					hp = ConfigPrefsController.GetUnitsBarbarianHealth(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.Mounted, unitsSkin, unitsTechLevel);
					atk = ConfigPrefsController.GetUnitsBarbarianAttack(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.Mounted, unitsSkin, unitsTechLevel);
					ratk = ConfigPrefsController.GetUnitsBarbarianRangedAttack(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.Mounted, unitsSkin, unitsTechLevel);
					def = ConfigPrefsController.GetUnitsBarbarianDefence(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.Mounted, unitsSkin, unitsTechLevel);
					break;
				case 2:
					hp = ConfigPrefsController.GetUnitsBarbarianHealth(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.Spear, unitsSkin, unitsTechLevel);
					atk = ConfigPrefsController.GetUnitsBarbarianAttack(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.Spear, unitsSkin, unitsTechLevel);
					ratk = ConfigPrefsController.GetUnitsBarbarianRangedAttack(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.Spear, unitsSkin, unitsTechLevel);
					def = ConfigPrefsController.GetUnitsBarbarianDefence(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.Spear, unitsSkin, unitsTechLevel);
					break;
				case 3:
					hp = ConfigPrefsController.GetUnitsBarbarianHealth(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.None, unitsSkin, unitsTechLevel);
					atk = ConfigPrefsController.GetUnitsBarbarianAttack(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.None, unitsSkin, unitsTechLevel);
					ratk = ConfigPrefsController.GetUnitsBarbarianRangedAttack(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.None, unitsSkin, unitsTechLevel);
					def = ConfigPrefsController.GetUnitsBarbarianDefence(EnemyPrefsController.FactionSelected, CohortType.Mixed, CohortSubType.None, unitsSkin, unitsTechLevel);
					break;
				}
				component.SetCohortData(hp, atk, ratk, def);
				component.stance = CohortStance.Defender;
				int num3 = EnemyPrefsController.UnitsPreSpawnedTotalSoldiers / num;
				int num4 = EnemyPrefsController.UnitsPreSpawnedTotalSoldiers % num;
				if (j < num4)
				{
					num3++;
				}
				if (num3 > 8)
				{
					num3 = 8;
				}
				if (num3 < 0)
				{
					num3 = 0;
				}
				component.InitializeCohort(num3);
				cohortsEnemyArray.Add(component);
			}
		}
	}

	public void StartWave()
	{
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			enemyTimeSpawn = ConfigPrefsController.spawnTimeStartBarbarian;
		}
		else if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			timeSpawn = ConfigPrefsController.spawnTimeStartPlayer;
		}
	}

	public void SpawnHero(bool _isInitialArmy, int _positionAttack, int _heroeIndex, Vector3 _heroePosition, CohortStance _cohortStance)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Soldiers/Heroes/Cohort_Heroe_" + _heroeIndex.ToString())) as GameObject;
		Vector3 position = _heroePosition;
		if (_cohortStance == CohortStance.Attacker)
		{
			position = attackTransform.position + new Vector3(-1f, 0f, 0f);
			Vector3 a = new Vector3(4f, 0f, 0f);
			if (_positionAttack >= 0 && _positionAttack < 3)
			{
				position += 0f * a;
			}
			else if (_positionAttack >= 3 && _positionAttack < 6)
			{
				position += 1f * a;
			}
			switch (_positionAttack)
			{
			case 0:
			case 3:
				position -= new Vector3(0f, 0f, 4f);
				break;
			case 2:
			case 5:
				position += new Vector3(0f, 0f, 4f);
				break;
			}
			position = ((!_isInitialArmy) ? (position + new Vector3(0f, 0f, 0f)) : (position - new Vector3(10f, 0f, 0f)));
		}
		gameObject.transform.position = position;
		switch (_cohortStance)
		{
		case CohortStance.Attacker:
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			break;
		case CohortStance.Defender:
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			break;
		}
		Cohort component = gameObject.GetComponent<Cohort>();
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		num = ConfigPrefsController.GetUnitsRomanHeroHealth(_heroeIndex, PlayerPrefsController.HeroeLvl[_heroeIndex]);
		num2 = ConfigPrefsController.GetUnitsRomanHeroAttack(_heroeIndex, PlayerPrefsController.HeroeLvl[_heroeIndex]);
		num3 = ConfigPrefsController.GetUnitsRomanHeroRangedAttack(_heroeIndex, PlayerPrefsController.HeroeLvl[_heroeIndex]);
		num4 = ConfigPrefsController.GetUnitsRomanHeroDefence(_heroeIndex, PlayerPrefsController.HeroeLvl[_heroeIndex]);
		component.SetCohortData(num, num2, num3, num4);
		component.stance = _cohortStance;
		component.InitializeCohort(1);
		cohortsFriendArray.Add(component);
		heroesObject.Add(gameObject);
	}

	public void SpawnUnitRoman(bool _isInitialArmy, int _positionAttack, CohortStance _unitStance, Faction _unitFaction, UnitType _unitType, bool _isHeroSpawning, int _heroIndex, int _unitLevel)
	{
		if (MainController.worldScreen == WorldScreen.Defence && !_isHeroSpawning)
		{
			timeSpawn -= ConfigPrefsController.spawnTimeRequiredPerUnit;
		}
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 euler = new Vector3(0f, 0f, 0f);
		if (_unitStance != CohortStance.Attacker)
		{
			vector = ((!_isHeroSpawning) ? defenceTransform.position : (heroesTransform[_heroIndex].position + new Vector3(2.5f, 0f, 0f)));
		}
		else
		{
			vector = attackTransform.position;
			euler = new Vector3(0f, 180f, 0f);
			Vector3 a = new Vector3(4f, 0f, 0f);
			if (_positionAttack >= 0 && _positionAttack < 3)
			{
				vector += 0f * a;
			}
			else if (_positionAttack >= 3 && _positionAttack < 6)
			{
				vector += 1f * a;
			}
			switch (_positionAttack)
			{
			case 0:
			case 3:
				vector -= new Vector3(0f, 0f, 4f);
				break;
			case 2:
			case 5:
				vector += new Vector3(0f, 0f, 4f);
				break;
			}
			vector = ((!_isInitialArmy) ? (vector + new Vector3(0f, 0f, 0f)) : (vector - new Vector3(10f, 0f, 0f)));
		}
		string str = "Soldiers/";
		switch (_unitFaction)
		{
		case Faction.Humans:
			str += "Roman/";
			break;
		case Faction.Goblins:
			str += "Italian/";
			break;
		case Faction.Skeletons:
			str += "Gaul/";
			break;
		case Faction.Wolves:
			str += "Iberian/";
			break;
		case Faction.Orcs:
			str += "Carthaginian/";
			break;
		}
		int num = _unitLevel;
		switch (_unitType)
		{
		case UnitType.Archer:
			str += "Ranged/Cohort_Ranged_";
			if (_unitFaction != 0)
			{
				break;
			}
			for (int num4 = PlayerPrefsController.UnitsTechRanged.Length - 1; num4 >= 0; num4--)
			{
				if (PlayerPrefsController.UnitsTechRanged[num4] >= 3)
				{
					num = num4 + 1;
					break;
				}
			}
			if (num >= PlayerPrefsController.UnitsTechRanged.Length)
			{
				num = PlayerPrefsController.UnitsTechRanged.Length - 1;
			}
			break;
		case UnitType.Mounted:
			str += "Mounted/Cohort_Mounted_";
			if (_unitFaction != 0)
			{
				break;
			}
			for (int num6 = PlayerPrefsController.UnitsTechMounted.Length - 1; num6 >= 0; num6--)
			{
				if (PlayerPrefsController.UnitsTechMounted[num6] >= 3)
				{
					num = num6 + 1;
					break;
				}
			}
			if (num >= PlayerPrefsController.UnitsTechMounted.Length)
			{
				num = PlayerPrefsController.UnitsTechMounted.Length - 1;
			}
			break;
		case UnitType.Spear:
			str += "Spear/Cohort_Spear_";
			if (_unitFaction != 0)
			{
				break;
			}
			for (int num5 = PlayerPrefsController.UnitsTechSpear.Length - 1; num5 >= 0; num5--)
			{
				if (PlayerPrefsController.UnitsTechSpear[num5] >= 3)
				{
					num = num5 + 1;
					break;
				}
			}
			if (num >= PlayerPrefsController.UnitsTechSpear.Length)
			{
				num = PlayerPrefsController.UnitsTechSpear.Length - 1;
			}
			break;
		case UnitType.Sword:
			str += "Melee/Cohort_Melee_";
			if (_unitFaction != 0)
			{
				break;
			}
			for (int num3 = PlayerPrefsController.UnitsTechMelee.Length - 1; num3 >= 0; num3--)
			{
				if (PlayerPrefsController.UnitsTechMelee[num3] >= 3)
				{
					num = num3 + 1;
					break;
				}
			}
			if (num >= PlayerPrefsController.UnitsTechMelee.Length)
			{
				num = PlayerPrefsController.UnitsTechMelee.Length - 1;
			}
			break;
		case UnitType.Siege:
			str = "Soldiers/Siege/BatteringRam_";
			if (_unitFaction != 0)
			{
				break;
			}
			for (int num2 = PlayerPrefsController.UnitsTechSiege.Length - 1; num2 >= 0; num2--)
			{
				if (PlayerPrefsController.UnitsTechSiege[num2] >= 3)
				{
					num = num2 + 1;
					break;
				}
			}
			if (num >= PlayerPrefsController.UnitsTechSiege.Length)
			{
				num = PlayerPrefsController.UnitsTechSiege.Length - 1;
			}
			break;
		}
		GameObject gameObject = null;
		gameObject = (UnityEngine.Object.Instantiate(Resources.Load(str + num.ToString()), vector, Quaternion.Euler(euler)) as GameObject);
		Cohort component = gameObject.GetComponent<Cohort>();
		float hp = 0f;
		float atk = 0f;
		float ratk = 0f;
		float def = 0f;
		switch (_unitType)
		{
		case UnitType.Spear:
			hp = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Mixed, CohortSubType.Spear, num, PlayerPrefsController.UnitsTechSpear[num]);
			atk = ConfigPrefsController.GetUnitsRomanAttack(CohortType.Mixed, CohortSubType.Spear, num, PlayerPrefsController.UnitsTechSpear[num]);
			ratk = ConfigPrefsController.GetUnitsRomanRangedAttack(CohortType.Mixed, CohortSubType.Spear, num, PlayerPrefsController.UnitsTechSpear[num]);
			def = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Mixed, CohortSubType.Spear, num, PlayerPrefsController.UnitsTechSpear[num]);
			break;
		case UnitType.Sword:
			hp = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Mixed, CohortSubType.None, num, PlayerPrefsController.UnitsTechMelee[num]);
			atk = ConfigPrefsController.GetUnitsRomanAttack(CohortType.Mixed, CohortSubType.None, num, PlayerPrefsController.UnitsTechMelee[num]);
			ratk = ConfigPrefsController.GetUnitsRomanRangedAttack(CohortType.Mixed, CohortSubType.None, num, PlayerPrefsController.UnitsTechMelee[num]);
			def = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Mixed, CohortSubType.None, num, PlayerPrefsController.UnitsTechMelee[num]);
			break;
		case UnitType.Mounted:
			hp = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Mixed, CohortSubType.Mounted, num, PlayerPrefsController.UnitsTechMounted[num]);
			atk = ConfigPrefsController.GetUnitsRomanAttack(CohortType.Mixed, CohortSubType.Mounted, num, PlayerPrefsController.UnitsTechMounted[num]);
			ratk = ConfigPrefsController.GetUnitsRomanRangedAttack(CohortType.Mixed, CohortSubType.Mounted, num, PlayerPrefsController.UnitsTechMounted[num]);
			def = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Mixed, CohortSubType.Mounted, num, PlayerPrefsController.UnitsTechMounted[num]);
			break;
		case UnitType.Archer:
			hp = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Ranged, CohortSubType.Javaline, num, PlayerPrefsController.UnitsTechRanged[num]);
			atk = ConfigPrefsController.GetUnitsRomanAttack(CohortType.Ranged, CohortSubType.Javaline, num, PlayerPrefsController.UnitsTechRanged[num]);
			ratk = ConfigPrefsController.GetUnitsRomanRangedAttack(CohortType.Ranged, CohortSubType.Javaline, num, PlayerPrefsController.UnitsTechRanged[num]);
			def = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Ranged, CohortSubType.Javaline, num, PlayerPrefsController.UnitsTechRanged[num]);
			break;
		case UnitType.Siege:
			hp = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Siege, CohortSubType.None, num, PlayerPrefsController.UnitsTechSiege[num]);
			atk = ConfigPrefsController.GetUnitsRomanAttack(CohortType.Siege, CohortSubType.None, num, PlayerPrefsController.UnitsTechSiege[num]);
			ratk = ConfigPrefsController.GetUnitsRomanRangedAttack(CohortType.Siege, CohortSubType.None, num, PlayerPrefsController.UnitsTechSiege[num]);
			def = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Siege, CohortSubType.None, num, PlayerPrefsController.UnitsTechSiege[num]);
			break;
		}
		component.SetCohortData(hp, atk, ratk, def);
		if (_unitStance == CohortStance.Attacker)
		{
			component.stance = CohortStance.Attacker;
		}
		else
		{
			component.stance = CohortStance.Defender;
		}
		int num7 = 0;
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			num7 = _heroIndex;
		}
		else if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			num7 = (int)(PlayerPrefsController.HeroeSelected[_heroIndex] - 1);
		}
		int num8;
		if (_isHeroSpawning)
		{
			num8 = 1;
			if (PlayerPrefsController.HeroeLvl[num7] >= 10)
			{
				num8++;
			}
			if (PlayerPrefsController.HeroeLvl[num7] >= 20)
			{
				num8++;
			}
			if (PlayerPrefsController.HeroeLvl[num7] >= 30)
			{
				num8++;
			}
			if (PlayerPrefsController.HeroeLvl[num7] >= 40)
			{
				num8++;
			}
			if (PlayerPrefsController.HeroeLvl[num7] >= 50)
			{
				num8++;
			}
			if (PlayerPrefsController.HeroeLvl[num7] >= 60)
			{
				num8++;
			}
			if (PlayerPrefsController.HeroeLvl[num7] >= 70)
			{
				num8++;
			}
		}
		else
		{
			num8 = 2;
			if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[0] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[1])
			{
				num8 = 3;
			}
			else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[1] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[2])
			{
				num8 = 4;
			}
			else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[2] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[3])
			{
				num8 = 5;
			}
			else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[3] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[4])
			{
				num8 = 6;
			}
			else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[4] && PlayerPrefsController.playerLevel < ConfigPrefsController.upgradeLvlCohort[5])
			{
				num8 = 7;
			}
			else if (PlayerPrefsController.playerLevel >= ConfigPrefsController.upgradeLvlCohort[5])
			{
				num8 = 8;
			}
		}
		if (_isHeroSpawning)
		{
			if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
			{
				component.perk = (HeroeType)(num7 + 1);
			}
			else if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
			{
				component.perk = PlayerPrefsController.HeroeSelected[_heroIndex];
			}
		}
		else
		{
			component.perk = HeroeType.None;
		}
		component.InitializeCohort(num8);
		cohortsFriendArray.Add(component);
		if (_unitStance == CohortStance.Defender && !_isHeroSpawning)
		{
			gateController.OpenGate();
		}
	}

	public void SpawnUnitBarbarian(CohortStance _unitStance, Faction _faction, UnitType _unitType, int _levelIndex, int _amountSoldier, int _positionAttack)
	{
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 euler = new Vector3(0f, 0f, 0f);
		if (_unitStance == CohortStance.Attacker)
		{
			vector = attackTransform.position;
			switch (_positionAttack)
			{
			case 1:
				vector -= new Vector3(0f, 0f, 4f);
				break;
			case 2:
				vector += new Vector3(0f, 0f, 4f);
				break;
			}
			euler = new Vector3(0f, 180f, 0f);
		}
		else
		{
			vector = defenceTransform.position;
			enemyTimeSpawn -= EnemyPrefsController.SpawnTimeUnits;
		}
		string str = "Soldiers/";
		switch (_faction)
		{
		case Faction.Goblins:
			str += "Italian/";
			break;
		case Faction.Skeletons:
			str += "Gaul/";
			break;
		case Faction.Wolves:
			str += "Iberian/";
			break;
		case Faction.Orcs:
			str += "Carthaginian/";
			break;
		}
		int unitIndex = _levelIndex;
		switch (_unitType)
		{
		case UnitType.Archer:
			str += "Ranged/Cohort_Ranged_";
			break;
		case UnitType.Mounted:
			str += "Mounted/Cohort_Mounted_";
			break;
		case UnitType.Spear:
			str += "Spear/Cohort_Spear_";
			break;
		case UnitType.Sword:
			str += "Melee/Cohort_Melee_";
			break;
		}
		GameObject gameObject = null;
		gameObject = (UnityEngine.Object.Instantiate(Resources.Load(str + unitIndex.ToString()), vector, Quaternion.Euler(euler)) as GameObject);
		Cohort component = gameObject.GetComponent<Cohort>();
		float hp = 0f;
		float atk = 0f;
		float ratk = 0f;
		float def = 0f;
		int num = 0;
		num = ((MainController.worldScreen != WorldScreen.Defence && MainController.worldScreen != 0) ? EnemyPrefsController.UnitsTechLevel : PlayerPrefs.GetInt("playerWave"));
		switch (_unitType)
		{
		case UnitType.Spear:
			hp = ConfigPrefsController.GetUnitsBarbarianHealth(_faction, CohortType.Mixed, CohortSubType.Spear, unitIndex, num);
			atk = ConfigPrefsController.GetUnitsBarbarianAttack(_faction, CohortType.Mixed, CohortSubType.Spear, unitIndex, num);
			ratk = ConfigPrefsController.GetUnitsBarbarianRangedAttack(_faction, CohortType.Mixed, CohortSubType.Spear, unitIndex, num);
			def = ConfigPrefsController.GetUnitsBarbarianDefence(_faction, CohortType.Mixed, CohortSubType.Spear, unitIndex, num);
			break;
		case UnitType.Sword:
			hp = ConfigPrefsController.GetUnitsBarbarianHealth(_faction, CohortType.Mixed, CohortSubType.None, unitIndex, num);
			atk = ConfigPrefsController.GetUnitsBarbarianAttack(_faction, CohortType.Mixed, CohortSubType.None, unitIndex, num);
			ratk = ConfigPrefsController.GetUnitsBarbarianRangedAttack(_faction, CohortType.Mixed, CohortSubType.None, unitIndex, num);
			def = ConfigPrefsController.GetUnitsBarbarianDefence(_faction, CohortType.Mixed, CohortSubType.None, unitIndex, num);
			break;
		case UnitType.Mounted:
			hp = ConfigPrefsController.GetUnitsBarbarianHealth(_faction, CohortType.Mixed, CohortSubType.Mounted, unitIndex, num);
			atk = ConfigPrefsController.GetUnitsBarbarianAttack(_faction, CohortType.Mixed, CohortSubType.Mounted, unitIndex, num);
			ratk = ConfigPrefsController.GetUnitsBarbarianRangedAttack(_faction, CohortType.Mixed, CohortSubType.Mounted, unitIndex, num);
			def = ConfigPrefsController.GetUnitsBarbarianDefence(_faction, CohortType.Mixed, CohortSubType.Mounted, unitIndex, num);
			break;
		case UnitType.Archer:
			hp = ConfigPrefsController.GetUnitsBarbarianHealth(_faction, CohortType.Ranged, CohortSubType.Javaline, unitIndex, num);
			atk = ConfigPrefsController.GetUnitsBarbarianAttack(_faction, CohortType.Ranged, CohortSubType.Javaline, unitIndex, num);
			ratk = ConfigPrefsController.GetUnitsBarbarianRangedAttack(_faction, CohortType.Ranged, CohortSubType.Javaline, unitIndex, num);
			def = ConfigPrefsController.GetUnitsBarbarianDefence(_faction, CohortType.Ranged, CohortSubType.Javaline, unitIndex, num);
			break;
		}
		component.SetCohortData(hp, atk, ratk, def);
		if (_unitStance == CohortStance.Attacker)
		{
			component.stance = CohortStance.Attacker;
		}
		else
		{
			component.stance = CohortStance.Defender;
		}
		component.InitializeCohort(_amountSoldier);
		cohortsEnemyArray.Add(component);
		if (_unitStance == CohortStance.Defender)
		{
			gateController.OpenGate();
		}
	}

	public void SpawnBoss(Faction _faction, int _index, int _positionAttack, bool _isElephant)
	{
		Vector3 vector = attackTransform.position - new Vector3(0f, 0f, 1.3f);
		switch (_positionAttack)
		{
		case 1:
			vector -= new Vector3(0f, 0f, 4f);
			break;
		case 2:
			vector += new Vector3(0f, 0f, 4f);
			break;
		}
		Vector3 euler = new Vector3(0f, 180f, 0f);
		GameObject gameObject = null;
		string str = "Soldiers/Siege/";
		CohortSubType subType;
		if (_isElephant)
		{
			str += "WarElephant_";
			subType = CohortSubType.Mounted;
		}
		else
		{
			str += "BatteringRam_";
			subType = CohortSubType.None;
		}
		gameObject = (UnityEngine.Object.Instantiate(Resources.Load(str + _index.ToString()), vector, Quaternion.Euler(euler)) as GameObject);
		Cohort component = gameObject.GetComponent<Cohort>();
		float unitsBarbarianHealth = ConfigPrefsController.GetUnitsBarbarianHealth(_faction, CohortType.Siege, subType, _index, PlayerPrefs.GetInt("playerWave"));
		float unitsBarbarianAttack = ConfigPrefsController.GetUnitsBarbarianAttack(_faction, CohortType.Siege, subType, _index, PlayerPrefs.GetInt("playerWave"));
		float unitsBarbarianRangedAttack = ConfigPrefsController.GetUnitsBarbarianRangedAttack(_faction, CohortType.Siege, subType, _index, PlayerPrefs.GetInt("playerWave"));
		float unitsBarbarianDefence = ConfigPrefsController.GetUnitsBarbarianDefence(_faction, CohortType.Siege, subType, _index, PlayerPrefs.GetInt("playerWave"));
		component.SetCohortData(unitsBarbarianHealth, unitsBarbarianAttack, unitsBarbarianRangedAttack, unitsBarbarianDefence);
		component.stance = CohortStance.Attacker;
		component.InitializeCohort(1);
		cohortsEnemyArray.Add(component);
	}

	public void ShootPower(Vector3 _positionTarget)
	{
		mainController.TimeReloadPowerCounter[UIController.PowerIndex + 1] = 0f;
		if (UIController.PowerIndex == 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Powers/BouldersShower")) as GameObject;
			gameObject.transform.position = _positionTarget;
			if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
			{
				gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			}
		}
		else if (UIController.PowerIndex == 1)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("Powers/ArrowsShower")) as GameObject;
			gameObject2.transform.position = _positionTarget;
			if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
			{
				gameObject2.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			}
		}
	}

	public void OrderGeneral()
	{
		wallGeneral.GiveOrder();
	}

	public void OrderGeneralWarcry()
	{
		mainController.TimeReloadPowerCounter[0] = 0f;
		GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("NewParticles/WarCryGeneralSword"), generalObject.transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.Euler(Vector3.zero)) as GameObject;
		UnityEngine.Object.Destroy(obj, 5f);
		timeWarcry = ConfigPrefsController.generalPowerWarCryDurationBase + ConfigPrefsController.generalPowerWarCryDurationPerLevel * (float)PlayerPrefsController.GeneralTechPowers_WarCry;
		timeWarcryActual = timeWarcry;
		for (int i = 0; i < cohortsFriendArray.Count; i++)
		{
			for (int j = 0; j < cohortsFriendArray[i].soldiersArray.Length; j++)
			{
				if (!cohortsFriendArray[i].DeadSoldier[j])
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NewParticles/WarCryOnWarrior"), cohortsFriendArray[i].soldiersArray[j].MyTransform.position + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(Vector3.zero)) as GameObject;
					gameObject.transform.SetParent(cohortsFriendArray[i].soldiersArray[j].MyTransform);
					ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
					particlesWarCryArray.Add(component);
				}
			}
		}
		isWarcryActive = true;
		uiController.playerWarcryBar.SetActive(value: true);
		uiController.textWarcryBar.text = "+" + ((ConfigPrefsController.generalPowerWarCryDamageBase + ConfigPrefsController.generalPowerWarCryDamagePerLevel * (float)PlayerPrefsController.GeneralTechPowers_WarCry) * 100f).ToString() + "% " + ScriptLocalization.Get("NORMAL/damage").ToUpper();
		if (PlayerPrefsController.isSfx)
		{
			audioSource.PlayOneShot(sfxPowerWarCry, AudioPrefsController.volumeBattlePowerWarcry * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}

	public void FinishWarCry()
	{
		for (int i = 0; i < particlesWarCryArray.Count; i++)
		{
			if (particlesWarCryArray[i] != null)
			{
				particlesWarCryArray[i].Stop();
				UnityEngine.Object.Destroy(particlesWarCryArray[i], 2f);
			}
		}
		isWarcryActive = false;
		uiController.playerWarcryBar.SetActive(value: false);
	}
}
