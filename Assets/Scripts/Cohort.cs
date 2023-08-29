using UnityEngine;

public class Cohort : MonoBehaviour
{
	public Faction faction;

	public CohortStance stance;

	public CohortType type;

	public CohortSubType subType;

	public HeroeType perk;

	public int moneyKill;

	public bool isMounted;

	public bool isHeroe;

	public int heroeIndex;

	public float moveSpeed;

	public float javalineReloadSpeed;

	public int javalinesAmount;

	public float javalineRange;

	public float precisionRadiusError;

	public float statsHP;

	public float statsATK;

	public float statsRATK;

	public float statsDEF;

	public CohortSoldier[] soldiersArray = new CohortSoldier[8];

	public float safeDistance;

	public float findTargetDistance;

	private float syncTime = 0.4f;

	public float destroyTime;

	public GameObject[] elephantArchers;

	public AudioClip[] sfxAttackMelee;

	public AudioClip[] sfxAttackRanged;

	public AudioClip[] sfxHittedRanged;

	public AudioClip[] sfxDie;

	public AudioClip[] sfxHorseGallop;

	public AudioClip[] sfxElephant;

	private bool isElephant;

	private bool[] deadSoldier;

	private bool allUnitsDead;

	private GameObject gateObject;

	private GameObject warfareObject;

	private GameObject warfareStartObject;

	private Transform gateTransform;

	private Transform warfareTransform;

	private Transform warfareStartTransform;

	private Transform myTransform;

	private float probSound = 0.3f;

	public float SyncTime => syncTime;

	public bool IsElephant => isElephant;

	public bool[] DeadSoldier
	{
		get
		{
			return deadSoldier;
		}
		set
		{
			deadSoldier = value;
		}
	}

	public Transform GateTransform => gateTransform;

	public Transform WarfareTransform => warfareTransform;

	public Transform WarfareStartTransform => warfareStartTransform;

	private void Awake()
	{
		myTransform = base.gameObject.GetComponent<Transform>();
		if (elephantArchers != null && elephantArchers.Length > 0)
		{
			isElephant = true;
		}
		for (int i = 0; i < soldiersArray.Length; i++)
		{
			soldiersArray[i].MoveSpeed = moveSpeed;
			soldiersArray[i].JavalinesAmount = javalinesAmount;
			soldiersArray[i].PrecisionRadiusError = precisionRadiusError;
			soldiersArray[i].StatsHP = statsHP + statsDEF;
			soldiersArray[i].StatsATK = statsATK;
			soldiersArray[i].StatsRATK = statsRATK;
			soldiersArray[i].CohortParent = this;
		}
		gateObject = GameObject.FindGameObjectWithTag("Gate");
		gateTransform = gateObject.GetComponent<Transform>();
		warfareObject = GameObject.FindGameObjectWithTag("BattlefieldEnd");
		warfareTransform = warfareObject.GetComponent<Transform>();
		warfareStartObject = GameObject.FindGameObjectWithTag("BattlefieldStart");
		warfareStartTransform = warfareStartObject.GetComponent<Transform>();
	}

	private void Start()
	{
		if (stance == CohortStance.Attacker)
		{
			for (int i = 0; i < soldiersArray.Length; i++)
			{
				soldiersArray[i].tag = "CohortAttack";
				soldiersArray[i].arrowReceiverColider.gameObject.tag = "ArrowReceiverAttack";
				if (soldiersArray[i].extraArrowReceiverColider != null)
				{
					for (int j = 0; j < soldiersArray[i].extraArrowReceiverColider.Length; j++)
					{
						soldiersArray[i].extraArrowReceiverColider[j].gameObject.tag = "ArrowReceiverAttack";
					}
				}
			}
		}
		else
		{
			if (stance != 0)
			{
				return;
			}
			for (int k = 0; k < soldiersArray.Length; k++)
			{
				soldiersArray[k].tag = "CohortDefence";
				soldiersArray[k].arrowReceiverColider.gameObject.tag = "ArrowReceiverDefence";
				if (soldiersArray[k].extraArrowReceiverColider != null)
				{
					for (int l = 0; l < soldiersArray[k].extraArrowReceiverColider.Length; l++)
					{
						soldiersArray[k].extraArrowReceiverColider[l].gameObject.tag = "ArrowReceiverDefence";
					}
				}
			}
		}
	}

	private void Update()
	{
		if (gateTransform == null)
		{
			gateObject = GameObject.FindGameObjectWithTag("Gate");
			gateTransform = gateObject.GetComponent<Transform>();
		}
		if (!allUnitsDead)
		{
			bool flag = true;
			for (int i = 0; i < deadSoldier.Length; i++)
			{
				if (!deadSoldier[i])
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				allUnitsDead = true;
				if (elephantArchers.Length > 0)
				{
					Invoke("DestroyElephantArchers", 1f);
				}
			}
		}
		else
		{
			destroyTime -= Time.deltaTime;
			if (destroyTime <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void DestroyElephantArchers()
	{
		if (elephantArchers.Length > 0)
		{
			for (int i = 0; i < elephantArchers.Length; i++)
			{
				UnityEngine.Object.Destroy(elephantArchers[i]);
			}
			elephantArchers = null;
		}
	}

	public bool IsAnySoldierAlive()
	{
		bool result = false;
		for (int i = 0; i < soldiersArray.Length; i++)
		{
			if (soldiersArray[i] != null && soldiersArray[i].StatsHP > 0f)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void CheckRadiusCollision(Bounds _boundsToCheck, float _damage, bool _isBonusSiege)
	{
		for (int i = 0; i < soldiersArray.Length; i++)
		{
			if (soldiersArray[i] != null && soldiersArray[i].Status != CohortSoldierStatus.Dead && soldiersArray[i].Status != CohortSoldierStatus.Flee && soldiersArray[i].MyColliderCapsule != null && _boundsToCheck.Intersects(soldiersArray[i].MyColliderCapsule.bounds))
			{
				float num = _damage;
				if (type == CohortType.Siege && _isBonusSiege)
				{
					num += _damage * ConfigPrefsController.bonusBigAmmoVsSiege;
				}
				else if (type != CohortType.Siege && !_isBonusSiege)
				{
					num += _damage * ConfigPrefsController.bonusBigAmmoVsSiege;
				}
				soldiersArray[i].Hitted(num, _isMelee: false);
			}
		}
	}

	public void SetCohortData(float _hp, float _atk, float _ratk, float _def)
	{
		statsHP = _hp;
		statsATK = _atk;
		statsRATK = _ratk;
		statsDEF = _def;
		for (int i = 0; i < soldiersArray.Length; i++)
		{
			soldiersArray[i].StatsHP = statsHP + statsDEF;
			soldiersArray[i].StatsATK = statsATK;
			soldiersArray[i].StatsRATK = statsRATK;
		}
	}

	public void InitializeCohort(int _unitsAmount)
	{
		deadSoldier = new bool[soldiersArray.Length];
		int num = soldiersArray.Length - _unitsAmount;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				UnityEngine.Object.Destroy(soldiersArray[soldiersArray.Length - 1 - i].gameObject);
				deadSoldier[soldiersArray.Length - 1 - i] = true;
			}
		}
		for (int j = 0; j < soldiersArray.Length; j++)
		{
			if (!deadSoldier[j])
			{
				soldiersArray[j].UnitIndex = j;
				if (perk != 0 && !isHeroe)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NewParticles/ParticlesHero_" + (int)(perk - 1)), soldiersArray[j].MyTransform.position + new Vector3(0f, 0.05f, 0f), Quaternion.Euler(new Vector3(90f, 0f, 0f))) as GameObject;
					gameObject.transform.SetParent(soldiersArray[j].MyTransform);
				}
				if (perk == HeroeType.RomanCommander && !isHeroe)
				{
					statsHP += (statsHP + statsDEF) * ConfigPrefsController.heroeRomanCommanderHealth;
					soldiersArray[j].StatsHP += (soldiersArray[j].StatsHP + statsDEF) * ConfigPrefsController.heroeRomanCommanderHealth;
				}
				soldiersArray[j].InitializeCohort();
			}
		}
	}

	public void PlaySoundAttackMelee(AudioSource _audioSource)
	{
		if (!PlayerPrefsController.isSfx || sfxAttackMelee.Length <= 0)
		{
			return;
		}
		float num = UnityEngine.Random.Range(0f, 1f);
		if (type == CohortType.Siege)
		{
			num = 0f;
		}
		if (num < probSound)
		{
			int num2 = UnityEngine.Random.Range(0, sfxAttackMelee.Length);
			if (type == CohortType.Siege)
			{
				_audioSource.PlayOneShot(sfxAttackMelee[num2], AudioPrefsController.volumeBattleSiegeAttack * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
			}
			else
			{
				_audioSource.PlayOneShot(sfxAttackMelee[num2], AudioPrefsController.volumeBattleMelee * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
			}
		}
	}

	public void PlaySoundAttackRanged(AudioSource _audioSource)
	{
		if (PlayerPrefsController.isSfx && sfxAttackRanged.Length > 0)
		{
			float num = UnityEngine.Random.Range(0f, 1f);
			if (num < probSound)
			{
				int num2 = UnityEngine.Random.Range(0, sfxAttackRanged.Length);
				_audioSource.PlayOneShot(sfxAttackRanged[num2], AudioPrefsController.volumeBattleRanged * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
			}
		}
	}

	public void PlaySoundDie(AudioSource _audioSource)
	{
		if (PlayerPrefsController.isSfx && sfxDie.Length > 0)
		{
			int num = UnityEngine.Random.Range(0, sfxDie.Length);
			_audioSource.PlayOneShot(sfxDie[num], AudioPrefsController.volumeBattleDie * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}

	public void PlaySoundHorseGallop(AudioSource _audioSource)
	{
		if (PlayerPrefsController.isSfx && sfxHorseGallop.Length > 0)
		{
			int num = UnityEngine.Random.Range(0, sfxHorseGallop.Length);
			_audioSource.PlayOneShot(sfxHorseGallop[num], AudioPrefsController.volumeBattleHorseGallop * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}

	public void PlaySoundElephantTrump(AudioSource _audioSource)
	{
		if (PlayerPrefsController.isSfx && sfxElephant.Length > 0)
		{
			int num = UnityEngine.Random.Range(0, sfxElephant.Length);
			_audioSource.PlayOneShot(sfxElephant[num], AudioPrefsController.volumeBattleElephantTrump * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}
}
