using UnityEngine;

public class WallArcher : MonoBehaviour
{
	public bool isSiegeArcher;

	public Faction faction;

	public float archerATK;

	public float timeArrowPerMeter;

	private float precisionRadiusError;

	public float rateOfFire;

	private float maxRange = 14f;

	public GameObject targetObject;

	private Transform targetTransform;

	private CohortSoldier targetScript;

	public GameObject prefabArrow;

	private WallArcherAction actualAction;

	private float rateOfFireCounter;

	private float timeToCheckSituationFlag = 0.5f;

	private float timeToCheckSituationCounter;

	public Animator myAnimator;

	public Transform rangedPositionTransform;

	public AudioClip[] sfxShoot;

	private Transform myTransform;

	private AudioSource audioSource;

	private float probSound = 0.8f;

	private void Awake()
	{
		myTransform = base.gameObject.GetComponent<Transform>();
		audioSource = base.gameObject.GetComponent<AudioSource>();
		myAnimator.SetFloat("RandomFrame", UnityEngine.Random.Range(0f, 1f));
		timeToCheckSituationCounter = timeToCheckSituationFlag;
		actualAction = WallArcherAction.Idle;
		if (MainController.worldScreen == WorldScreen.Attack)
		{
			myTransform.localRotation = Quaternion.Euler(new Vector3(0f, 40f, 0f));
		}
	}

	private void Start()
	{
		if (faction == Faction.Humans)
		{
			archerATK = ConfigPrefsController.unitStats_Roman_Archer_AttackBase + (float)PlayerPrefsController.ArchersLvl * ConfigPrefsController.unitStats_Roman_Archer_AttackPerLevel;
			archerATK += archerATK * ConfigPrefsController.generalBaseArchersDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_ArchersDamage;
			maxRange += maxRange * ConfigPrefsController.generalBaseArchersRangePerLevel * (float)PlayerPrefsController.GeneralTechBase_ArchersRange;
		}
		else if (isSiegeArcher)
		{
			archerATK = ConfigPrefsController.waveUnitsElephant_AttackRanged_Base + ConfigPrefsController.waveUnitsElephant_AttackRanged_Base * ConfigPrefsController.waveUnitsElephant_Increment * (float)PlayerPrefs.GetInt("playerWave");
		}
		else
		{
			archerATK = ConfigPrefsController.unitStats_Roman_Archer_AttackBase;
			float num = (float)EnemyPrefsController.ArchersStrength * ConfigPrefsController.unitStats_Roman_Archer_AttackPerLevel;
			archerATK += num;
		}
	}

	private void Update()
	{
		if (actualAction == WallArcherAction.Idle)
		{
			rateOfFireCounter -= Time.deltaTime;
			if (targetObject != null)
			{
				if (!(rateOfFireCounter <= 0f))
				{
					return;
				}
				if (targetScript.Status == CohortSoldierStatus.Dead || targetScript.Status == CohortSoldierStatus.Flee)
				{
					targetObject = null;
					targetTransform = null;
					targetScript = null;
					SetAction(WallArcherAction.Idle);
					FindTarget();
					return;
				}
				float num = Vector3.Distance(rangedPositionTransform.position, targetTransform.position);
				if (num <= maxRange)
				{
					SetAction(WallArcherAction.Attack);
					return;
				}
				targetObject = null;
				targetTransform = null;
				targetScript = null;
				SetAction(WallArcherAction.Idle);
				FindTarget();
			}
			else
			{
				timeToCheckSituationCounter -= Time.deltaTime;
				if (timeToCheckSituationCounter <= 0f)
				{
					timeToCheckSituationCounter = timeToCheckSituationFlag;
					FindTarget();
				}
			}
		}
		else if (actualAction == WallArcherAction.Attack && targetObject == null)
		{
			SetAction(WallArcherAction.Idle);
		}
	}

	private void SetAction(WallArcherAction _action)
	{
		actualAction = _action;
		switch (_action)
		{
		case WallArcherAction.Idle:
			rateOfFireCounter = rateOfFire;
			myAnimator.SetTrigger("Idle");
			break;
		case WallArcherAction.Attack:
			myAnimator.SetTrigger("Attack");
			if (isSiegeArcher)
			{
				Vector3 position = targetTransform.position;
				float x = position.x;
				Vector3 position2 = rangedPositionTransform.position;
				if (x > position2.x)
				{
					myTransform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
				}
				else
				{
					myTransform.localRotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
				}
			}
			break;
		}
	}

	public void FindTarget()
	{
		GameObject[] array = null;
		array = ((!isSiegeArcher) ? GameObject.FindGameObjectsWithTag("CohortAttack") : GameObject.FindGameObjectsWithTag("CohortDefence"));
		if (array.Length <= 0)
		{
			return;
		}
		int num = -1;
		float num2 = 0f;
		for (int i = 0; i < array.Length; i++)
		{
			CohortSoldier component = array[i].GetComponent<CohortSoldier>();
			if (component.Status == CohortSoldierStatus.Dead || component.Status == CohortSoldierStatus.Flee)
			{
				continue;
			}
			Transform component2 = array[i].GetComponent<Transform>();
			float num3 = Vector3.Distance(rangedPositionTransform.position, component2.position);
			if (num3 <= maxRange)
			{
				if (num == -1)
				{
					num = i;
					num2 = num3;
				}
				else if (num2 > num3)
				{
					num = i;
					num2 = num3;
				}
			}
		}
		if (num != -1)
		{
			targetObject = array[num];
			targetTransform = targetObject.GetComponent<Transform>();
			targetScript = targetObject.GetComponent<CohortSoldier>();
		}
	}

	public void SpawnArrow()
	{
		Vector3 vector = rangedPositionTransform.position + new Vector3(0.07f, 0.02f, 0f);
		Vector3 vector2 = Vector3.zero;
		if (targetTransform != null)
		{
			vector2 = targetTransform.position;
			if (targetScript.Status == CohortSoldierStatus.Advancing || targetScript.Status == CohortSoldierStatus.Predating)
			{
				vector2 -= new Vector3(1f, 0f, 0f);
			}
			vector2 += new Vector3(UnityEngine.Random.Range(0f - precisionRadiusError, precisionRadiusError), 0f, UnityEngine.Random.Range(0f - precisionRadiusError, precisionRadiusError));
			Vector3 vector3 = vector2 - vector;
			Vector3 a = vector2;
			Vector3 normalized = vector3.normalized;
			float x = normalized.x;
			Vector3 normalized2 = vector3.normalized;
			vector2 = a + new Vector3(x, 0f, normalized2.z) * 1.5f;
		}
		Vector3 forward = vector2 - vector;
		Quaternion quaternion = Quaternion.LookRotation(forward);
		Vector3 eulerAngles = quaternion.eulerAngles;
		float x2 = eulerAngles.x;
		Vector3 eulerAngles2 = quaternion.eulerAngles;
		quaternion = Quaternion.Euler(x2, eulerAngles2.y - 90f, 270f);
		Vector3 vector4 = vector + (vector2 - vector) * 0.5f;
		vector4 = new Vector3(vector4.x, vector.y + 1f, vector4.z);
		float timeArrow = 0f;
		if (targetObject != null)
		{
			timeArrow = Vector3.Distance(targetObject.transform.position, vector) * timeArrowPerMeter;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(prefabArrow, vector, quaternion) as GameObject;
		if (isSiegeArcher)
		{
			gameObject.tag = "JavalineAttack";
		}
		else
		{
			gameObject.tag = "ArrowDefence";
		}
		float num = archerATK;
		if (UpgradesController.isExtraDamageActive && faction == Faction.Humans)
		{
			num += archerATK * UpgradesController.extraDamageAmount;
		}
		ArrowBehaviour component = gameObject.GetComponent<ArrowBehaviour>();
		component.SetTarget(vector4, vector2, timeArrow, num, targetScript);
		PlaySoundShoot();
		SetAction(WallArcherAction.Idle);
	}

	private void PlaySoundShoot()
	{
		if (sfxShoot.Length > 0 && PlayerPrefsController.isSfx)
		{
			float num = UnityEngine.Random.Range(0f, 1f);
			if (num < probSound)
			{
				int num2 = UnityEngine.Random.Range(0, sfxShoot.Length);
				audioSource.PlayOneShot(sfxShoot[num2], AudioPrefsController.volumeBattleWallArrow * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
			}
		}
	}
}
