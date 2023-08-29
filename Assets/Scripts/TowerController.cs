using UnityEngine;

public class TowerController : MonoBehaviour
{
	public GameObject[] upgradesObject;

	public TowerAmmoType ballistaSelected;

	public float findTargetDistance;

	public GameObject prefabVoltBig;

	public GameObject prefabVoltSmall;

	public Transform voltPosition;

	private float syncTime = 0.5f;

	private GameObject targetObject;

	private Transform targetTransform;

	private CohortSoldier targetScript;

	private Transform myTransform;

	public Transform pivotYTransform;

	public Transform pivotXTransform;

	public AudioClip[] sfxShoot;

	private Animator myAnimator;

	private AudioSource audioSource;

	private float speedRotationX = 15f;

	private float speedRotationY = 30f;

	private float checkSituationTimeCounter;

	public GameObject TargetObject => targetObject;

	private void Awake()
	{
		myTransform = base.gameObject.GetComponent<Transform>();
		myAnimator = base.gameObject.GetComponent<Animator>();
		audioSource = base.gameObject.GetComponent<AudioSource>();
	}

	private void Update()
	{
		CheckSituation();
		AimTarget();
	}

	public void SetInitialStatus()
	{
		if (PlayerPrefsController.TowerLvl < 75)
		{
			UnityEngine.Object.Destroy(upgradesObject[3]);
		}
		if (PlayerPrefsController.TowerLvl < 50)
		{
			UnityEngine.Object.Destroy(upgradesObject[2]);
		}
		if (PlayerPrefsController.TowerLvl < 25)
		{
			UnityEngine.Object.Destroy(upgradesObject[1]);
		}
		if (PlayerPrefsController.TowerLvl < 10)
		{
			UnityEngine.Object.Destroy(upgradesObject[0]);
		}
	}

	public void ShootVolt()
	{
		GameObject[] array;
		if (ballistaSelected == TowerAmmoType.Big)
		{
			array = new GameObject[1]
			{
				Object.Instantiate(prefabVoltBig)
			};
			Transform component = array[0].GetComponent<Transform>();
			component.SetParent(voltPosition);
			component.localPosition = Vector3.zero;
			component.localRotation = Quaternion.Euler(Vector3.zero);
		}
		else
		{
			array = new GameObject[5];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = UnityEngine.Object.Instantiate(prefabVoltSmall);
				Transform component2 = array[i].GetComponent<Transform>();
				component2.SetParent(voltPosition);
				component2.localPosition = Vector3.zero;
				component2.localRotation = Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f)));
			}
		}
		float num = 0f;
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			if (ballistaSelected == TowerAmmoType.Big)
			{
				num = ConfigPrefsController.damageTowerAmmoBigBase + (float)PlayerPrefsController.TowerAmmoBigLvl * ConfigPrefsController.damageTowerAmmoBigPerLevel;
			}
			else if (ballistaSelected == TowerAmmoType.Small)
			{
				num = ConfigPrefsController.damageTowerAmmoSmallBase + (float)PlayerPrefsController.TowerAmmoSmallLvl * ConfigPrefsController.damageTowerAmmoSmallPerLevel;
			}
			num += num * ConfigPrefsController.generalBaseTowerDamagePerLevel * (float)PlayerPrefsController.GeneralTechBase_TowerDamage;
		}
		else
		{
			float b = 0f;
			if (ballistaSelected == TowerAmmoType.Big)
			{
				num = ConfigPrefsController.damageTowerAmmoBigBase;
				b = ConfigPrefsController.invasionIndexReferenceTowers * ConfigPrefsController.damageTowerAmmoBigPerLevel;
			}
			else if (ballistaSelected == TowerAmmoType.Small)
			{
				num = ConfigPrefsController.damageTowerAmmoSmallBase;
				b = ConfigPrefsController.invasionIndexReferenceTowers * ConfigPrefsController.damageTowerAmmoSmallPerLevel;
			}
			num += Mathf.Lerp(0f, b, (float)EnemyPrefsController.TowerLvl / 100f);
		}
		for (int j = 0; j < array.Length; j++)
		{
			ArrowVoltBehaviour component3 = array[j].GetComponent<ArrowVoltBehaviour>();
			component3.ArrowATK = num;
			component3.StartShoot(ballistaSelected);
			myAnimator.SetTrigger("Attack");
		}
		int num2 = UnityEngine.Random.Range(0, sfxShoot.Length);
		if (PlayerPrefsController.isSfx)
		{
			audioSource.PlayOneShot(sfxShoot[num2], AudioPrefsController.volumeBattleTowerShoot * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}

	private void AimTarget()
	{
		if (!(targetTransform != null))
		{
			return;
		}
		Vector3 b = new Vector3(0f, 0.1f, 0f);
		Vector3 forward = targetTransform.position + b - pivotYTransform.position;
		Quaternion quaternion = Quaternion.LookRotation(forward);
		Vector3 eulerAngles = quaternion.eulerAngles;
		Vector3 vector = new Vector3(0f, eulerAngles.y, 0f);
		Vector3 eulerAngles2 = pivotYTransform.rotation.eulerAngles;
		float y = eulerAngles2.y;
		float num = vector.y;
		if (y < 180f && num > y + 180f)
		{
			num = (360f - num) * -1f;
		}
		else if (y > 180f && num < y - 180f)
		{
			num = 360f + num;
		}
		float num2 = 0f;
		if (y > num)
		{
			num2 = y - num;
			if (num2 > speedRotationY * Time.deltaTime)
			{
				num2 = speedRotationY * Time.deltaTime;
			}
		}
		else if (y < num)
		{
			num2 = (num - y) * -1f;
			if (num2 < (0f - speedRotationY) * Time.deltaTime)
			{
				num2 = (0f - speedRotationY) * Time.deltaTime;
			}
		}
		pivotYTransform.rotation = Quaternion.Euler(new Vector3(-90f, y - num2, 0f));
		forward = targetTransform.position + b - pivotXTransform.position;
		Vector3 eulerAngles3 = Quaternion.LookRotation(forward).eulerAngles;
		float x = eulerAngles3.x;
		Vector3 eulerAngles4 = quaternion.eulerAngles;
		y = eulerAngles4.x;
		float num3 = 0f;
		if (y > x)
		{
			num3 = y - x;
			if (num3 > speedRotationX * Time.deltaTime)
			{
				num3 = speedRotationX * Time.deltaTime;
			}
		}
		else if (y < x)
		{
			num3 = (x - y) * -1f;
			if (num3 < (0f - speedRotationX) * Time.deltaTime)
			{
				num3 = (0f - speedRotationX) * Time.deltaTime;
			}
		}
		pivotXTransform.localRotation = Quaternion.Euler(new Vector3(y - num3, 0f, 0f));
	}

	private void CheckSituation()
	{
		checkSituationTimeCounter += Time.deltaTime;
		if (checkSituationTimeCounter >= syncTime)
		{
			checkSituationTimeCounter = 0f;
			SearchEnemies();
		}
	}

	private void SearchEnemies()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("CohortAttack");
		if (array.Length > 0)
		{
			int num = -1;
			float num2 = 0f;
			for (int i = 0; i < array.Length; i++)
			{
				CohortSoldier component = array[i].GetComponent<CohortSoldier>();
				if (component.Status == CohortSoldierStatus.Dead || component.status == CohortSoldierStatus.Flee)
				{
					continue;
				}
				Transform component2 = array[i].GetComponent<Transform>();
				float num3 = Vector3.Distance(myTransform.position, component2.position);
				if (num3 <= findTargetDistance)
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
			else
			{
				targetObject = null;
				targetTransform = null;
				targetScript = null;
			}
		}
		else
		{
			targetObject = null;
			targetTransform = null;
			targetScript = null;
		}
	}
}
