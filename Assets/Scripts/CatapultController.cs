using UnityEngine;

public class CatapultController : MonoBehaviour
{
	public GameObject[] upgradesObject;

	public CatapultAmmoType catapultSelected;

	public float errorRadius;

	public float timeReload;

	private float timeReloadCounter;

	private Vector3 positionTarget;

	private GameObject[] bouldersArray;

	private Transform[] bouldersTransformArray;

	private ArrowBoulderBehaviour[] bouldersScriptArray;

	public GameObject prefabBoulderBig;

	public GameObject prefabBoulderSmall;

	public Transform transformParentBoulder;

	public Transform boulderPosition;

	public AudioClip sfxReload;

	public AudioClip sfxShoot;

	private AudioSource audioSource;

	private Animator myAnimator;

	public float TimeReload => timeReload;

	public float TimeReloadCounter
	{
		get
		{
			return timeReloadCounter;
		}
		set
		{
			timeReloadCounter = value;
		}
	}

	private void Awake()
	{
		audioSource = base.gameObject.GetComponent<AudioSource>();
		myAnimator = base.gameObject.GetComponent<Animator>();
		timeReload = ConfigPrefsController.cooldownCatapult;
	}

	private void Update()
	{
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			timeReloadCounter += Time.deltaTime * (ConfigPrefsController.cooldownCatapultBase + ConfigPrefsController.cooldownCatapultBase * ConfigPrefsController.cooldownCatapultPerLevel * (float)PlayerPrefsController.CatapultLvl);
		}
		else
		{
			timeReloadCounter += Time.deltaTime * (ConfigPrefsController.cooldownCatapultBase + ConfigPrefsController.cooldownCatapultBase * ConfigPrefsController.cooldownCatapultPerLevel * (float)EnemyPrefsController.CatapultLvl);
		}
		if (timeReloadCounter > timeReload)
		{
			timeReloadCounter = timeReload;
		}
	}

	public void SetInitialStatus()
	{
		if (PlayerPrefsController.CatapultLvl < 75)
		{
		}
		if (PlayerPrefsController.CatapultLvl < 50)
		{
		}
		if (PlayerPrefsController.CatapultLvl < 25)
		{
			UnityEngine.Object.Destroy(upgradesObject[1]);
		}
		if (PlayerPrefsController.CatapultLvl < 10)
		{
			UnityEngine.Object.Destroy(upgradesObject[0]);
		}
		Reload();
	}

	public void ShootBoulder(Vector3 _positionTarget)
	{
		timeReloadCounter = 0f;
		positionTarget = _positionTarget;
		myAnimator.SetTrigger("Shoot");
		if (PlayerPrefsController.isSfx)
		{
			audioSource.PlayOneShot(sfxShoot, AudioPrefsController.volumeBattleCatapultShoot * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}

	public void FireBoulder()
	{
		float num = 0f;
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			if (catapultSelected == CatapultAmmoType.Big)
			{
				num = ConfigPrefsController.damageCatapultAmmoBigBase + (float)PlayerPrefsController.CatapultAmmoBigLvl * ConfigPrefsController.damageCatapultAmmoBigPerLevel;
			}
			else if (catapultSelected == CatapultAmmoType.Small)
			{
				num = ConfigPrefsController.damageCatapultAmmoSmallBase + (float)PlayerPrefsController.CatapultAmmoSmallLvl * ConfigPrefsController.damageCatapultAmmoSmallPerLevel;
			}
			num += num * ConfigPrefsController.generalBaseCatapultDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_CatapultDamage;
		}
		else
		{
			float b = 0f;
			if (catapultSelected == CatapultAmmoType.Big)
			{
				num = ConfigPrefsController.damageCatapultAmmoBigBase;
				b = ConfigPrefsController.invasionIndexReferenceCatapults * ConfigPrefsController.damageCatapultAmmoBigPerLevel;
			}
			else if (catapultSelected == CatapultAmmoType.Small)
			{
				num = ConfigPrefsController.damageCatapultAmmoSmallBase;
				b = ConfigPrefsController.invasionIndexReferenceCatapults * ConfigPrefsController.damageCatapultAmmoSmallPerLevel;
			}
			num += Mathf.Lerp(0f, b, (float)EnemyPrefsController.CatapultLvl / 100f);
		}
		for (int i = 0; i < bouldersScriptArray.Length; i++)
		{
			Vector3 vector = positionTarget;
			if (i > 0)
			{
				vector += new Vector3(UnityEngine.Random.Range(0f - errorRadius, errorRadius), 0f, UnityEngine.Random.Range(0f - errorRadius, errorRadius));
			}
			Vector3 position = bouldersTransformArray[i].position;
			float timeArrow = Vector3.Distance(vector, position) * 0.035f;
			Vector3 vector2 = position + (vector - position) * 0.5f;
			vector2 = new Vector3(vector2.x, 4f, vector2.z);
			bouldersScriptArray[i].SetTarget(bouldersTransformArray[i].position, vector2, vector, timeArrow, num, catapultSelected);
		}
	}

	public void Reload()
	{
		if (PlayerPrefsController.isSfx)
		{
			audioSource.PlayOneShot(sfxReload, AudioPrefsController.volumeBattleCatapultReload * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
		if (catapultSelected == CatapultAmmoType.Big)
		{
			bouldersArray = new GameObject[1];
			bouldersScriptArray = new ArrowBoulderBehaviour[1];
			bouldersTransformArray = new Transform[1];
			bouldersArray[0] = UnityEngine.Object.Instantiate(prefabBoulderBig);
			bouldersTransformArray[0] = bouldersArray[0].GetComponent<Transform>();
			bouldersTransformArray[0].SetParent(transformParentBoulder);
		}
		else if (catapultSelected == CatapultAmmoType.Small)
		{
			bouldersArray = new GameObject[5];
			bouldersScriptArray = new ArrowBoulderBehaviour[5];
			bouldersTransformArray = new Transform[5];
			for (int i = 0; i < bouldersArray.Length; i++)
			{
				bouldersArray[i] = UnityEngine.Object.Instantiate(prefabBoulderSmall);
				bouldersTransformArray[i] = bouldersArray[i].GetComponent<Transform>();
				bouldersTransformArray[i].SetParent(transformParentBoulder);
			}
		}
		for (int j = 0; j < bouldersScriptArray.Length; j++)
		{
			if (j == 0)
			{
				bouldersTransformArray[j].position = boulderPosition.position;
			}
			else
			{
				bouldersTransformArray[j].position = boulderPosition.position + new Vector3(UnityEngine.Random.Range(-0.15f, 0.15f), UnityEngine.Random.Range(-0.08f, -0.16f), UnityEngine.Random.Range(-0.15f, 0.15f));
			}
			bouldersScriptArray[j] = bouldersArray[j].GetComponent<ArrowBoulderBehaviour>();
		}
	}
}
