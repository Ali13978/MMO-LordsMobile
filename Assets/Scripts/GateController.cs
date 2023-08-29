using UnityEngine;

public class GateController : MonoBehaviour
{
	public int gateLevel;

	private bool isOpen;

	private bool isDestroyed;

	private float timeToCheckClose = 2f;

	private float timeToCheckCloseCounter;

	public GameObject[] upgradesArray;

	public GameObject[] doorUpgrades;

	public GameObject bridge;

	public GameObject bridgeUpgrade;

	public AudioClip sfxOpenGate;

	public AudioClip sfxDestroyGate;

	public Transform gateLeftTransform;

	public Transform gateRightTransform;

	private BoxCollider myBoxCollider;

	private Transform myTransform;

	private MainController mainController;

	private AudioSource audioSource;

	private void Awake()
	{
		myBoxCollider = base.gameObject.GetComponent<BoxCollider>();
		myTransform = base.gameObject.GetComponent<Transform>();
		mainController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainController>();
		audioSource = base.gameObject.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (!isOpen)
		{
			return;
		}
		timeToCheckCloseCounter += Time.deltaTime;
		if (!(timeToCheckCloseCounter >= timeToCheckClose))
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("CohortDefence");
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			Transform component = array[i].GetComponent<Transform>();
			CohortSoldier component2 = array[i].GetComponent<CohortSoldier>();
			Vector3 position = component.position;
			float x = position.x;
			Vector3 position2 = myTransform.position;
			if (x <= position2.x && component2.StatsHP > 0f)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			CloseGate();
		}
	}

	public void Hitted(float _damage)
	{
		mainController.GateLifeActual -= _damage;
		if (mainController.GateLifeActual <= 0f)
		{
			mainController.GateLifeActual = 0f;
			if (!isDestroyed)
			{
				DestroyDoor();
			}
		}
	}

	private void DestroyDoor()
	{
		isDestroyed = true;
		UnityEngine.Object.Destroy(gateLeftTransform.gameObject);
		UnityEngine.Object.Destroy(gateRightTransform.gameObject);
		GameObject gameObject = null;
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			gameObject = (UnityEngine.Object.Instantiate(Resources.Load("Walls/BrokenDoorLvl" + gateLevel.ToString()), myTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
			if (gateLevel == 3)
			{
				upgradesArray[0].SetActive(value: false);
			}
		}
		else
		{
			switch (EnemyPrefsController.FactionSelected)
			{
			case Faction.Goblins:
				gameObject = (UnityEngine.Object.Instantiate(Resources.Load("WallsEnemy/Broken_Door_Goblins"), myTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
				break;
			case Faction.Skeletons:
				gameObject = (UnityEngine.Object.Instantiate(Resources.Load("WallsEnemy/Broken_Door_Skeletons"), myTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
				break;
			case Faction.Wolves:
				gameObject = (UnityEngine.Object.Instantiate(Resources.Load("WallsEnemy/Broken_Door_Wolves"), myTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
				break;
			case Faction.Orcs:
				gameObject = (UnityEngine.Object.Instantiate(Resources.Load("WallsEnemy/Broken_Door_Orcs"), myTransform.position, Quaternion.Euler(Vector3.zero)) as GameObject);
				break;
			}
		}
		gameObject.transform.SetParent(myTransform);
		UnityEngine.Object.Destroy(myBoxCollider);
		if (PlayerPrefsController.isSfx)
		{
			audioSource.PlayOneShot(sfxDestroyGate, AudioPrefsController.volumeBattleGateDestroy * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}

	public void OpenGate()
	{
		if (!isDestroyed)
		{
			if (!isOpen && PlayerPrefsController.isSfx)
			{
				audioSource.PlayOneShot(sfxOpenGate, AudioPrefsController.volumeBattleGateOpen * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
			}
			isOpen = true;
			timeToCheckCloseCounter = 0f;
			LeanTween.cancel(base.gameObject);
			LeanTween.rotateLocal(gateLeftTransform.gameObject, new Vector3(0f, -90f, 0f), 2f);
			LeanTween.rotateLocal(gateRightTransform.gameObject, new Vector3(0f, 90f, 0f), 2f);
		}
	}

	private void CloseGate()
	{
		if (!isDestroyed)
		{
			isOpen = false;
			timeToCheckCloseCounter = 0f;
			LeanTween.cancel(base.gameObject);
			LeanTween.rotateLocal(gateLeftTransform.gameObject, new Vector3(0f, 0f, 0f), 2f);
			LeanTween.rotateLocal(gateRightTransform.gameObject, new Vector3(0f, 0f, 0f), 2f);
		}
	}

	public void SetWallUpgrades()
	{
		for (int i = 0; i < upgradesArray.Length; i++)
		{
			upgradesArray[i].SetActive(value: false);
		}
		for (int j = 0; j < doorUpgrades.Length; j++)
		{
			doorUpgrades[j].SetActive(value: false);
		}
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			switch (gateLevel)
			{
			case 0:
				bridgeUpgrade.SetActive(value: false);
				bridge.SetActive(value: true);
				if (PlayerPrefsController.WallLvl >= 1)
				{
					upgradesArray[0].SetActive(value: true);
					doorUpgrades[0].SetActive(value: true);
					doorUpgrades[2].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 5)
				{
					bridgeUpgrade.SetActive(value: true);
					bridge.SetActive(value: false);
					upgradesArray[1].SetActive(value: true);
					doorUpgrades[1].SetActive(value: true);
					doorUpgrades[3].SetActive(value: true);
				}
				break;
			case 1:
				if (PlayerPrefsController.WallLvl >= 10)
				{
					upgradesArray[0].SetActive(value: true);
					doorUpgrades[0].SetActive(value: true);
					doorUpgrades[1].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 20)
				{
					upgradesArray[1].SetActive(value: true);
					doorUpgrades[2].SetActive(value: true);
					doorUpgrades[3].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 24)
				{
					upgradesArray[2].SetActive(value: true);
					doorUpgrades[4].SetActive(value: true);
					doorUpgrades[5].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 28)
				{
					upgradesArray[3].SetActive(value: true);
					doorUpgrades[6].SetActive(value: true);
					doorUpgrades[7].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 32)
				{
					upgradesArray[4].SetActive(value: true);
				}
				break;
			case 2:
				if (PlayerPrefsController.WallLvl >= 56)
				{
					upgradesArray[0].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 64)
				{
					upgradesArray[1].SetActive(value: true);
					doorUpgrades[0].SetActive(value: true);
					doorUpgrades[1].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 76)
				{
					upgradesArray[2].SetActive(value: true);
					doorUpgrades[2].SetActive(value: true);
					doorUpgrades[3].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 92)
				{
					upgradesArray[3].SetActive(value: true);
				}
				break;
			case 3:
				if (PlayerPrefsController.WallLvl >= 132)
				{
					upgradesArray[0].SetActive(value: true);
					doorUpgrades[0].SetActive(value: true);
					doorUpgrades[1].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 150)
				{
					upgradesArray[1].SetActive(value: true);
					doorUpgrades[2].SetActive(value: true);
					doorUpgrades[3].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 156)
				{
					upgradesArray[2].SetActive(value: true);
				}
				if (PlayerPrefsController.WallLvl >= 168)
				{
					upgradesArray[3].SetActive(value: true);
				}
				break;
			}
		}
		else if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			int num = EnemyPrefsController.LevelPowerIndexSelected - (int)(EnemyPrefsController.FactionSelected - 1) * 100;
			int num2 = (int)(100f / (float)(upgradesArray.Length + 1));
			for (int k = 0; k < upgradesArray.Length && num >= k * num2; k++)
			{
				upgradesArray[k].SetActive(value: true);
			}
		}
	}
}
