using System;
using UnityEngine;

public class CohortSoldier : MonoBehaviour
{
	public GameObject prefabJavaline;

	private int unitIndex;

	public float statsHP;

	private float statsATK;

	private float statsRATK;

	private float moveSpeed;

	private int javalinesAmount;

	private float precisionRadiusError;

	private float javalineReloadCounter;

	private GameObject targetObject;

	private Transform targetTransform;

	private CohortSoldier targetScript;

	private GateController gateScript;

	public CohortSoldierStatus status;

	private SpriteRenderer myRenderer;

	private SpriteRenderer horseRenderer;

	private Cohort cohortParent;

	public Animator myAnimator;

	public Animator horseAnimator;

	private Transform myTransform;

	private Rigidbody myRigidbody;

	private BoxCollider myColliderBox;

	private CapsuleCollider myColliderCapsule;

	public BoxCollider arrowReceiverColider;

	public BoxCollider[] extraArrowReceiverColider;

	public Transform rangedPositionTransform;

	public RuntimeAnimatorController[] animatorHorseRandom;

	public Sprite[] spriteHorseRandom;

	public Transform transformMainHealth;

	public Transform transformHealth;

	private SpriteRenderer rendererHealth;

	public SpriteRenderer rendererHealthBack;

	private MainController mainController;

	private AudioSource audioSource;

	private ParticleSystem[] myParticleSystem;

	private bool siegeAlreadyAdvancing;

	private float checkSituationTimeCounter;

	private bool obstacleFront;

	private bool obstacleLeft;

	private bool obstacleRight;

	public int UnitIndex
	{
		get
		{
			return unitIndex;
		}
		set
		{
			unitIndex = value;
		}
	}

	public float StatsHP
	{
		get
		{
			return statsHP;
		}
		set
		{
			statsHP = value;
		}
	}

	public float StatsATK
	{
		get
		{
			return statsATK;
		}
		set
		{
			statsATK = value;
		}
	}

	public float StatsRATK
	{
		get
		{
			return statsRATK;
		}
		set
		{
			statsRATK = value;
		}
	}

	public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
		set
		{
			moveSpeed = value;
		}
	}

	public int JavalinesAmount
	{
		get
		{
			return javalinesAmount;
		}
		set
		{
			javalinesAmount = value;
		}
	}

	public float PrecisionRadiusError
	{
		get
		{
			return precisionRadiusError;
		}
		set
		{
			precisionRadiusError = value;
		}
	}

	public CohortSoldierStatus Status
	{
		get
		{
			return status;
		}
		set
		{
			status = value;
		}
	}

	public Cohort CohortParent
	{
		get
		{
			return cohortParent;
		}
		set
		{
			cohortParent = value;
		}
	}

	public Transform MyTransform
	{
		get
		{
			return myTransform;
		}
		set
		{
			myTransform = value;
		}
	}

	public CapsuleCollider MyColliderCapsule
	{
		get
		{
			return myColliderCapsule;
		}
		set
		{
			myColliderCapsule = value;
		}
	}

	private void Awake()
	{
		mainController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainController>();
		audioSource = base.gameObject.GetComponent<AudioSource>();
		float value = 0f;
		if (cohortParent.type != CohortType.Siege)
		{
			value = UnityEngine.Random.Range(0f, 1f);
			myAnimator.SetFloat("RandomFrame", value);
		}
		myRenderer = myAnimator.gameObject.GetComponent<SpriteRenderer>();
		if (horseAnimator != null)
		{
			horseRenderer = horseAnimator.gameObject.GetComponent<SpriteRenderer>();
		}
		if (transformHealth != null)
		{
			rendererHealth = transformHealth.GetComponent<SpriteRenderer>();
		}
		Transform transform = null;
		if (myRenderer != null)
		{
			transform = myRenderer.GetComponent<Transform>();
		}
		Transform transform2 = null;
		if (horseAnimator != null)
		{
			transform2 = horseRenderer.GetComponent<Transform>();
		}
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			if (transform != null)
			{
				transform.localRotation = Quaternion.Euler(new Vector3(0f, 340f, 0f));
			}
			if (horseAnimator != null)
			{
				transform2.localRotation = Quaternion.Euler(new Vector3(0f, 340f, 0f));
			}
		}
		else
		{
			if (transform != null)
			{
				transform.localRotation = Quaternion.Euler(new Vector3(0f, 20f, 0f));
			}
			if (horseAnimator != null)
			{
				transform2.localRotation = Quaternion.Euler(new Vector3(0f, 20f, 0f));
			}
		}
		myTransform = base.gameObject.GetComponent<Transform>();
		myRigidbody = base.gameObject.GetComponent<Rigidbody>();
		myColliderBox = base.gameObject.GetComponent<BoxCollider>();
		myColliderCapsule = base.gameObject.GetComponent<CapsuleCollider>();
		if (cohortParent.type == CohortType.Mixed && cohortParent.subType == CohortSubType.Mounted && animatorHorseRandom != null && animatorHorseRandom.Length > 0)
		{
			int num = UnityEngine.Random.Range(0, animatorHorseRandom.Length);
			horseRenderer.sprite = spriteHorseRandom[num];
			horseAnimator.runtimeAnimatorController = animatorHorseRandom[num];
		}
		if (horseAnimator != null)
		{
			horseRenderer = horseAnimator.gameObject.GetComponent<SpriteRenderer>();
			horseAnimator.SetFloat("RandomFrame", value);
		}
	}

	private void Update()
	{
		if (status != CohortSoldierStatus.Dead && status != CohortSoldierStatus.Flee && cohortParent.type != CohortType.Siege)
		{
			if (javalineReloadCounter > 0f)
			{
				javalineReloadCounter -= Time.deltaTime;
			}
			if (MainController.worldScreen != WorldScreen.Attack)
			{
				CheckSituation();
			}
		}
		else if (status == CohortSoldierStatus.Flee)
		{
			Vector3 position = myTransform.position;
			if (position.x >= -10f)
			{
				cohortParent.DeadSoldier[unitIndex] = true;
			}
		}
	}

	private void FixedUpdate()
	{
		if (status != CohortSoldierStatus.Dead && status != CohortSoldierStatus.Flee)
		{
			if (cohortParent.type != CohortType.Siege || status == CohortSoldierStatus.AttackingGate || (MainController.worldScreen != WorldScreen.Defence && MainController.worldScreen != WorldScreen.AttackStarted))
			{
				return;
			}
			bool flag = false;
			if (cohortParent.IsElephant)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag("CohortAttack");
				CohortSoldier[] array2 = new CohortSoldier[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = array[i].GetComponent<CohortSoldier>();
					if (!array2[i].CohortParent.IsElephant || !(array2[i].statsHP > 0f) || !(array2[i] != this))
					{
						continue;
					}
					Vector3 localPosition = myTransform.localPosition;
					float x = localPosition.x;
					Vector3 localPosition2 = array2[i].MyTransform.localPosition;
					if (x < localPosition2.x)
					{
						Vector3 localPosition3 = myTransform.localPosition;
						float x2 = localPosition3.x;
						Vector3 localPosition4 = array2[i].MyTransform.localPosition;
						if (x2 > localPosition4.x - 5f)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (flag)
			{
				if (siegeAlreadyAdvancing)
				{
					cohortParent.PlaySoundElephantTrump(audioSource);
					siegeAlreadyAdvancing = false;
					myAnimator.SetTrigger("Idle");
				}
				return;
			}
			if (!siegeAlreadyAdvancing)
			{
				siegeAlreadyAdvancing = true;
				myAnimator.SetTrigger("Run");
			}
			if (!siegeAlreadyAdvancing)
			{
				return;
			}
			float z = 0f;
			Vector3 position = myRigidbody.position;
			if (position.z < -0.75f)
			{
				z = -0.5f * Time.deltaTime;
			}
			else
			{
				Vector3 position2 = myRigidbody.position;
				if (position2.z > 0.75f)
				{
					z = 0.5f * Time.deltaTime;
				}
			}
			myRigidbody.MovePosition(myRigidbody.position - new Vector3(moveSpeed * Time.fixedDeltaTime, 0f, z));
		}
		else if (status == CohortSoldierStatus.Flee)
		{
			myTransform.position += new Vector3(moveSpeed * Time.fixedDeltaTime, 0f, 0f);
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (status == CohortSoldierStatus.Dead || status == CohortSoldierStatus.Flee)
		{
			return;
		}
		if (cohortParent.type == CohortType.Mixed)
		{
			if ((col.gameObject.CompareTag("CohortAttack") && cohortParent.stance == CohortStance.Defender) || (col.gameObject.CompareTag("CohortDefence") && cohortParent.stance == CohortStance.Attacker))
			{
				SetMeeleeAttack(col.gameObject);
			}
			else if (status != CohortSoldierStatus.AttackingGate && col.gameObject.CompareTag("Gate") && cohortParent.stance == CohortStance.Attacker)
			{
				SetGateAttack(col.gameObject);
			}
		}
		else if (cohortParent.type == CohortType.Siege && status != CohortSoldierStatus.AttackingGate && col.gameObject.CompareTag("Gate"))
		{
			SetGateAttack(col.gameObject);
		}
	}

	private void OnTriggerStay(Collider col)
	{
		if (cohortParent.type != CohortType.Siege && cohortParent.stance == CohortStance.Attacker && status != CohortSoldierStatus.Dead && status != CohortSoldierStatus.AttackingGate && status != CohortSoldierStatus.Flee && cohortParent.type == CohortType.Mixed && status != CohortSoldierStatus.Fighting && cohortParent.GateTransform != null)
		{
			Vector3 position = myTransform.position;
			float x = position.x;
			Vector3 position2 = cohortParent.GateTransform.position;
			if (x <= position2.x + 2f && col.gameObject.CompareTag("Gate"))
			{
				SetGateAttack(col.gameObject);
			}
		}
	}

	public void InitializeCohort()
	{
		if (cohortParent.type != CohortType.Siege)
		{
			CheckSituation();
		}
		if (transformHealth != null)
		{
			transformHealth.localScale = new Vector3(0.94f, 0.6f, 1f);
			rendererHealth.enabled = false;
			rendererHealthBack.enabled = false;
			if ((cohortParent.stance == CohortStance.Attacker && (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)) || (cohortParent.stance == CohortStance.Defender && (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)))
			{
				rendererHealth.color = Color.green;
			}
			else
			{
				rendererHealth.color = Color.red;
			}
		}
	}

	private void RotationDamage()
	{
		if (!(transformHealth != null))
		{
			return;
		}
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			if (cohortParent.stance == CohortStance.Attacker)
			{
				Vector3 eulerAngles = myTransform.localRotation.eulerAngles;
				if (eulerAngles.y != 360f)
				{
					Vector3 eulerAngles2 = myTransform.localRotation.eulerAngles;
					if (eulerAngles2.y != 0f)
					{
						Vector3 eulerAngles3 = myTransform.localRotation.eulerAngles;
						if (eulerAngles3.y != -180f)
						{
							Vector3 eulerAngles4 = myTransform.localRotation.eulerAngles;
							if (eulerAngles4.y != 180f)
							{
								return;
							}
						}
						transformMainHealth.localRotation = Quaternion.Euler(new Vector3(0f, 220f, 0f));
						return;
					}
				}
				transformMainHealth.localRotation = Quaternion.Euler(new Vector3(0f, 35f, 0f));
			}
			else
			{
				if (cohortParent.stance != 0)
				{
					return;
				}
				Vector3 eulerAngles5 = myTransform.localRotation.eulerAngles;
				if (eulerAngles5.y != 360f)
				{
					Vector3 eulerAngles6 = myTransform.localRotation.eulerAngles;
					if (eulerAngles6.y != 0f)
					{
						Vector3 eulerAngles7 = myTransform.localRotation.eulerAngles;
						if (eulerAngles7.y != -180f)
						{
							Vector3 eulerAngles8 = myTransform.localRotation.eulerAngles;
							if (eulerAngles8.y != 180f)
							{
								return;
							}
						}
						transformMainHealth.localRotation = Quaternion.Euler(new Vector3(0f, 35f, 0f));
						return;
					}
				}
				transformMainHealth.localRotation = Quaternion.Euler(new Vector3(0f, 220f, 0f));
			}
		}
		else
		{
			if (MainController.worldScreen != WorldScreen.Defence && MainController.worldScreen != 0)
			{
				return;
			}
			if (cohortParent.stance == CohortStance.Attacker)
			{
				Vector3 eulerAngles9 = myTransform.localRotation.eulerAngles;
				if (eulerAngles9.y != 360f)
				{
					Vector3 eulerAngles10 = myTransform.localRotation.eulerAngles;
					if (eulerAngles10.y != 0f)
					{
						Vector3 eulerAngles11 = myTransform.localRotation.eulerAngles;
						if (eulerAngles11.y != -180f)
						{
							Vector3 eulerAngles12 = myTransform.localRotation.eulerAngles;
							if (eulerAngles12.y != 180f)
							{
								return;
							}
						}
						transformMainHealth.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
						return;
					}
				}
				transformMainHealth.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			}
			else
			{
				if (cohortParent.stance != 0)
				{
					return;
				}
				Vector3 eulerAngles13 = myTransform.localRotation.eulerAngles;
				if (eulerAngles13.y != 360f)
				{
					Vector3 eulerAngles14 = myTransform.localRotation.eulerAngles;
					if (eulerAngles14.y != 0f)
					{
						Vector3 eulerAngles15 = myTransform.localRotation.eulerAngles;
						if (eulerAngles15.y != -180f)
						{
							Vector3 eulerAngles16 = myTransform.localRotation.eulerAngles;
							if (eulerAngles16.y != 180f)
							{
								return;
							}
						}
						transformMainHealth.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
						return;
					}
				}
				transformMainHealth.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			}
		}
	}

	private void ShowDamage()
	{
		rendererHealth.enabled = true;
		rendererHealthBack.enabled = true;
		float num = statsHP / (cohortParent.statsHP + cohortParent.statsDEF);
		if (num < 0f)
		{
			num = 0f;
		}
		LeanTween.cancel(transformHealth.gameObject);
		LeanTween.scale(transformHealth.gameObject, new Vector3(Mathf.Lerp(0f, 0.94f, num / 1f), 0.6f, 1f), 0.5f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true);
		if (num <= 0f)
		{
			LeanTween.delayedCall(transformHealth.gameObject, 0.5f, FinishedShowingDamage).setIgnoreTimeScale(useUnScaledTime: true);
		}
		else
		{
			LeanTween.delayedCall(transformHealth.gameObject, 3f, FinishedShowingDamage).setIgnoreTimeScale(useUnScaledTime: true);
		}
	}

	private void FinishedShowingDamage()
	{
		if (transformHealth != null)
		{
			rendererHealth.enabled = false;
			rendererHealthBack.enabled = false;
		}
	}

	private void SetGateAttack(GameObject _gateObject)
	{
		UpdateSituation(CohortSoldierStatus.AttackingGate);
		targetObject = null;
		targetTransform = null;
		targetScript = null;
		gateScript = _gateObject.GetComponent<GateController>();
	}

	private void SetMeeleeAttack(GameObject _targetObject)
	{
		gateScript = null;
		targetObject = _targetObject;
		targetTransform = targetObject.GetComponent<Transform>();
		targetScript = targetObject.GetComponent<CohortSoldier>();
		UpdateSituation(CohortSoldierStatus.Fighting);
	}

	private void UpdateSituation(CohortSoldierStatus _status)
	{
		myAnimator.ResetTrigger("Run");
		myAnimator.ResetTrigger("Die");
		if (cohortParent.type != 0)
		{
			myAnimator.ResetTrigger("Attack");
		}
		if (!cohortParent.isMounted && cohortParent.type != CohortType.Siege)
		{
			myAnimator.ResetTrigger("Javaline");
		}
		myAnimator.ResetTrigger("Idle");
		if (cohortParent.isMounted)
		{
			horseAnimator.ResetTrigger("Run");
			horseAnimator.ResetTrigger("Die");
			horseAnimator.ResetTrigger("Idle");
		}
		if (targetTransform != null)
		{
			Vector3 position = targetTransform.position;
			float x = position.x;
			Vector3 position2 = myTransform.position;
			if (x > position2.x)
			{
				myTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				RotationDamage();
			}
			else
			{
				Vector3 position3 = targetTransform.position;
				float x2 = position3.x;
				Vector3 position4 = myTransform.position;
				if (x2 < position4.x)
				{
					myTransform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
					RotationDamage();
				}
			}
		}
		else if (cohortParent.stance == CohortStance.Attacker)
		{
			myTransform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			RotationDamage();
		}
		else
		{
			myTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			RotationDamage();
		}
		switch (_status)
		{
		case CohortSoldierStatus.Advancing:
		{
			status = CohortSoldierStatus.Advancing;
			bool flag = true;
			Vector3 vector = Vector3.zero;
			if (cohortParent.stance == CohortStance.Attacker)
			{
				vector = ((!(mainController.GateLifeActual > 0f)) ? (-Vector3.right) : (cohortParent.GateTransform.position - myTransform.position));
			}
			else if (cohortParent.stance == CohortStance.Defender)
			{
				vector = cohortParent.WarfareTransform.position - myTransform.position;
			}
			CheckObstaclesFront(vector);
			if (!obstacleFront)
			{
				myRigidbody.velocity = vector.normalized * moveSpeed;
			}
			else if (!obstacleLeft)
			{
				vector = Quaternion.Euler(0f, 70f, 0f) * vector;
				myRigidbody.velocity = vector.normalized * moveSpeed;
			}
			else if (!obstacleRight)
			{
				vector = Quaternion.Euler(0f, -70f, 0f) * vector;
				myRigidbody.velocity = vector.normalized * moveSpeed;
			}
			else
			{
				myRigidbody.velocity = Vector3.zero;
				flag = false;
			}
			if (flag)
			{
				cohortParent.PlaySoundHorseGallop(audioSource);
				myAnimator.SetTrigger("Run");
				if (cohortParent.isMounted)
				{
					horseAnimator.SetTrigger("Run");
				}
			}
			else
			{
				myAnimator.SetTrigger("Idle");
				if (cohortParent.isMounted)
				{
					horseAnimator.SetTrigger("Idle");
				}
			}
			break;
		}
		case CohortSoldierStatus.Dead:
			status = CohortSoldierStatus.Dead;
			myAnimator.SetTrigger("Die");
			if (cohortParent.isMounted)
			{
				horseAnimator.SetTrigger("Die");
			}
			if (myRigidbody != null)
			{
				myRigidbody.velocity = Vector3.zero;
			}
			try
			{
				myParticleSystem = GetComponentsInChildren<ParticleSystem>();
			}
			catch (Exception)
			{
				throw;
				IL_048a:;
			}
			for (int i = 0; i < myParticleSystem.Length; i++)
			{
				if (myParticleSystem != null)
				{
					myParticleSystem[i].gameObject.SetActive(value: false);
				}
			}
			break;
		case CohortSoldierStatus.Flee:
		{
			status = CohortSoldierStatus.Flee;
			myAnimator.SetTrigger("Run");
			if (cohortParent.isMounted)
			{
				horseAnimator.SetTrigger("Run");
			}
			myTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			RotationDamage();
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NewParticles/Surrender"), myTransform.position + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(new Vector3(-90f, 0f, 0f))) as GameObject;
			gameObject.transform.SetParent(myTransform);
			break;
		}
		case CohortSoldierStatus.Fighting:
			myRigidbody.isKinematic = true;
			status = CohortSoldierStatus.Fighting;
			myAnimator.SetTrigger("Attack");
			if (cohortParent.isMounted)
			{
				horseAnimator.SetTrigger("Idle");
			}
			break;
		case CohortSoldierStatus.AttackingGate:
			myRigidbody.isKinematic = true;
			status = CohortSoldierStatus.AttackingGate;
			myAnimator.SetTrigger("Attack");
			if (cohortParent.isMounted)
			{
				horseAnimator.SetTrigger("Idle");
			}
			break;
		case CohortSoldierStatus.Idle:
			if (myRigidbody != null)
			{
				myRigidbody.isKinematic = false;
				status = CohortSoldierStatus.Idle;
				myAnimator.SetTrigger("Idle");
				if (cohortParent.isMounted)
				{
					horseAnimator.SetTrigger("Idle");
				}
				myRigidbody.velocity = Vector3.zero;
			}
			break;
		case CohortSoldierStatus.Shooting:
			status = CohortSoldierStatus.Shooting;
			myAnimator.SetTrigger("Javaline");
			if (cohortParent.isMounted)
			{
				horseAnimator.SetTrigger("Idle");
			}
			break;
		case CohortSoldierStatus.ShootingGate:
			status = CohortSoldierStatus.ShootingGate;
			myAnimator.SetTrigger("Javaline");
			if (cohortParent.isMounted)
			{
				horseAnimator.SetTrigger("Idle");
			}
			break;
		case CohortSoldierStatus.Predating:
		{
			status = CohortSoldierStatus.Predating;
			bool flag = true;
			Vector3 vector = targetTransform.position - myTransform.position;
			CheckObstaclesFront(vector);
			if (!obstacleFront)
			{
				myRigidbody.velocity = vector.normalized * moveSpeed;
			}
			else if (!obstacleLeft)
			{
				vector = Quaternion.Euler(0f, 70f, 0f) * vector;
				myRigidbody.velocity = vector.normalized * moveSpeed;
			}
			else if (!obstacleRight)
			{
				vector = Quaternion.Euler(0f, -70f, 0f) * vector;
				myRigidbody.velocity = vector.normalized * moveSpeed;
			}
			else
			{
				myRigidbody.velocity = Vector3.zero;
				flag = false;
			}
			if (flag)
			{
				myAnimator.SetTrigger("Run");
				if (cohortParent.isMounted)
				{
					horseAnimator.SetTrigger("Run");
				}
			}
			else
			{
				myAnimator.SetTrigger("Idle");
				if (cohortParent.isMounted)
				{
					horseAnimator.SetTrigger("Idle");
				}
			}
			break;
		}
		case CohortSoldierStatus.Retreating:
			status = CohortSoldierStatus.Retreating;
			myAnimator.SetTrigger("Run");
			if (cohortParent.isMounted)
			{
				horseAnimator.SetTrigger("Run");
			}
			if (targetTransform != null)
			{
				Vector3 position5 = targetTransform.position;
				float x3 = position5.x;
				Vector3 position6 = myTransform.position;
				if (x3 > position6.x)
				{
					myTransform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
					RotationDamage();
				}
				else
				{
					Vector3 position7 = targetTransform.position;
					float x4 = position7.x;
					Vector3 position8 = myTransform.position;
					if (x4 < position8.x)
					{
						myTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
						RotationDamage();
					}
				}
			}
			else if (cohortParent.stance == CohortStance.Attacker)
			{
				myTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				RotationDamage();
			}
			else if (cohortParent.stance == CohortStance.Defender)
			{
				myTransform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
				RotationDamage();
			}
			myRigidbody.velocity = myTransform.right * moveSpeed;
			break;
		}
	}

	public void ShootJavaline()
	{
		if (status == CohortSoldierStatus.Dead || status == CohortSoldierStatus.Flee)
		{
			return;
		}
		if (targetObject != null || status == CohortSoldierStatus.ShootingGate)
		{
			if (cohortParent.type != 0)
			{
				javalinesAmount--;
			}
			javalineReloadCounter = cohortParent.javalineReloadSpeed;
			Vector3 vector = rangedPositionTransform.position + new Vector3(0.07f, 0.02f, 0f);
			Vector2 vector2 = UnityEngine.Random.insideUnitCircle * precisionRadiusError;
			Vector3 forward;
			Vector3 vector3;
			if (status == CohortSoldierStatus.ShootingGate)
			{
				forward = cohortParent.GateTransform.position - rangedPositionTransform.position;
				Vector3 position = cohortParent.GateTransform.position;
				float x = position.x;
				Vector3 position2 = cohortParent.GateTransform.position;
				vector3 = new Vector3(x, 0f, position2.z) + forward.normalized * 5f + new Vector3(vector2.x, 0f, vector2.y);
				vector3 = new Vector3(vector3.x, 0f, vector3.z);
			}
			else
			{
				forward = targetTransform.position - rangedPositionTransform.position;
				Vector3 position3 = targetTransform.position;
				float x2 = position3.x;
				Vector3 position4 = targetTransform.position;
				vector3 = new Vector3(x2, 0f, position4.z) + forward.normalized * 5f + new Vector3(vector2.x, 0f, vector2.y);
				vector3 = new Vector3(vector3.x, 0f, vector3.z);
			}
			Quaternion quaternion = Quaternion.LookRotation(forward);
			Vector3 eulerAngles = quaternion.eulerAngles;
			float x3 = eulerAngles.x;
			Vector3 eulerAngles2 = quaternion.eulerAngles;
			quaternion = Quaternion.Euler(x3, eulerAngles2.y - 90f, 270f);
			Vector3 middlePosition = rangedPositionTransform.position + (vector3 - rangedPositionTransform.position) * 0.5f;
			float x4 = middlePosition.x;
			Vector3 position5 = rangedPositionTransform.position;
			middlePosition = new Vector3(x4, position5.y, middlePosition.z);
			float timeArrow = Vector3.Distance(vector3, vector) * 0.1f;
			GameObject gameObject = UnityEngine.Object.Instantiate(prefabJavaline, vector, quaternion) as GameObject;
			if (status == CohortSoldierStatus.ShootingGate)
			{
				gameObject.tag = "JavalineGate";
			}
			else if (cohortParent.stance == CohortStance.Attacker)
			{
				gameObject.tag = "JavalineAttack";
			}
			else
			{
				gameObject.tag = "JavalineDefence";
			}
			ArrowBehaviour component = gameObject.GetComponent<ArrowBehaviour>();
			float num = 0f;
			if (targetScript != null && cohortParent.subType == CohortSubType.Javaline && targetScript.cohortParent.subType == CohortSubType.Spear)
			{
				num += statsRATK * ConfigPrefsController.bonusDamageJavalineVsSpear;
			}
			if (UpgradesController.isExtraDamageActive && cohortParent.faction == Faction.Humans)
			{
				num += (statsRATK + num) * UpgradesController.extraDamageAmount;
			}
			if (UpgradesController.isWarcryActive && cohortParent.faction == Faction.Humans)
			{
				num += (statsRATK + num) * (ConfigPrefsController.generalPowerWarCryDamageBase + ConfigPrefsController.generalPowerWarCryDamagePerLevel * (float)PlayerPrefsController.GeneralTechPowers_WarCry);
			}
			if (cohortParent.perk == HeroeType.Gladiator && UnityEngine.Random.Range(0f, 1f) < ConfigPrefsController.heroeGladiatorCriticalHitChance)
			{
				num += (statsRATK + num) * ConfigPrefsController.heroeGladiatorCriticalHitDamage;
				GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("NewParticles/CriticalHit"), MyTransform.position + new Vector3(0f, 1.5f, 0f), Quaternion.Euler(new Vector3(-90f, 0f, 0f))) as GameObject;
				gameObject2.transform.SetParent(myTransform);
				UnityEngine.Object.Destroy(gameObject2, 2f);
			}
			component.SetTarget(middlePosition, vector3, timeArrow, statsRATK + num, null);
			cohortParent.PlaySoundAttackRanged(audioSource);
		}
		UpdateSituation(CohortSoldierStatus.Idle);
	}

	public void HitEnemyMeelee()
	{
		float num = 0f;
		if (cohortParent.perk == HeroeType.RomanCommander && !cohortParent.isHeroe)
		{
			num += statsATK * ConfigPrefsController.heroeRomanCommanderAttack;
		}
		if (targetScript != null)
		{
			if (cohortParent.subType == CohortSubType.Spear && targetScript.cohortParent.subType == CohortSubType.Mounted)
			{
				num += statsATK * ConfigPrefsController.bonusDamageSpearVsMounted;
			}
			if (cohortParent.subType == CohortSubType.Mounted && targetScript.cohortParent.subType == CohortSubType.Javaline)
			{
				num += statsATK * ConfigPrefsController.bonusDamageMountedVsJavaline;
			}
		}
		if (UpgradesController.isExtraDamageActive && cohortParent.faction == Faction.Humans)
		{
			num += (statsATK + num) * UpgradesController.extraDamageAmount;
		}
		if (UpgradesController.isWarcryActive && cohortParent.faction == Faction.Humans)
		{
			num += (statsATK + num) * (ConfigPrefsController.generalPowerWarCryDamageBase + ConfigPrefsController.generalPowerWarCryDamagePerLevel * (float)PlayerPrefsController.GeneralTechPowers_WarCry);
		}
		if (targetScript != null)
		{
			if (targetScript.cohortParent.type != CohortType.Siege && !targetScript.cohortParent.isHeroe && cohortParent.perk == HeroeType.BarbarianCommander)
			{
				targetScript.TryFlee(ConfigPrefsController.heroeBarbarianCommanderFear);
			}
			targetScript.Hitted(statsATK + num, _isMelee: true, base.gameObject);
			cohortParent.PlaySoundAttackMelee(audioSource);
		}
		else if (gateScript != null)
		{
			gateScript.Hitted(statsATK + num);
			cohortParent.PlaySoundAttackMelee(audioSource);
		}
	}

	public void Hitted(float _damage, bool _isMelee, GameObject _targetMelee = null)
	{
		if (status != CohortSoldierStatus.Dead && status != CohortSoldierStatus.Flee)
		{
			if (cohortParent.type != CohortType.Siege && myAnimator.gameObject != null)
			{
				LeanTween.cancel(myAnimator.gameObject);
				myRenderer.color = Color.white;
				LeanTween.color(myAnimator.gameObject, Color.red, 0.15f).setLoopPingPong(1).setEase(LeanTweenType.easeInOutSine)
					.setIgnoreTimeScale(useUnScaledTime: true);
			}
			if (_damage < 1f)
			{
				_damage = 1f;
			}
			statsHP -= _damage;
			if (statsHP <= 0f)
			{
				cohortParent.PlaySoundDie(audioSource);
				KilledSoldier();
			}
			if (status == CohortSoldierStatus.AttackingGate && _isMelee && cohortParent.type != CohortType.Siege && _targetMelee != null)
			{
				SetMeeleeAttack(_targetMelee);
			}
			if (transformHealth != null)
			{
				RotationDamage();
				ShowDamage();
			}
		}
	}

	public void TryFlee(float _chanceFlee)
	{
		if (status != CohortSoldierStatus.Dead && status != CohortSoldierStatus.Flee && !cohortParent.isHeroe && cohortParent.type != CohortType.Siege)
		{
			float num = UnityEngine.Random.Range(0f, 1f);
			if (num < _chanceFlee)
			{
				Surrender();
			}
		}
	}

	private void Surrender()
	{
		UpdateSituation(CohortSoldierStatus.Flee);
		if ((cohortParent.stance == CohortStance.Attacker && (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)) || (cohortParent.stance == CohortStance.Defender && (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)))
		{
			float income = CalculateKillReward();
			float exp = CalculateKillExp();
			mainController.KilledEnemy(income, exp);
		}
		UnityEngine.Object.Destroy(myRigidbody);
		UnityEngine.Object.Destroy(myColliderBox);
		UnityEngine.Object.Destroy(myColliderCapsule);
		UnityEngine.Object.Destroy(arrowReceiverColider.gameObject);
		if (extraArrowReceiverColider != null)
		{
			for (int i = 0; i < extraArrowReceiverColider.Length; i++)
			{
				UnityEngine.Object.Destroy(extraArrowReceiverColider[i]);
			}
		}
	}

	private void KilledSoldier()
	{
		if (cohortParent.type != CohortType.Siege)
		{
			myRenderer.color = Color.gray;
			if (horseRenderer != null)
			{
				horseRenderer.color = Color.gray;
			}
		}
		UpdateSituation(CohortSoldierStatus.Dead);
		cohortParent.DeadSoldier[unitIndex] = true;
		if (cohortParent.perk == HeroeType.Arcani && cohortParent.isHeroe)
		{
			UpgradesController.isExtraMoneyActive = false;
		}
		if ((cohortParent.stance == CohortStance.Attacker && (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)) || (cohortParent.stance == CohortStance.Defender && (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)))
		{
			float income = CalculateKillReward();
			float exp = CalculateKillExp();
			mainController.KilledEnemy(income, exp);
		}
		UnityEngine.Object.Destroy(myRigidbody);
		UnityEngine.Object.Destroy(myColliderBox);
		UnityEngine.Object.Destroy(myColliderCapsule);
		if (arrowReceiverColider != null)
		{
			UnityEngine.Object.Destroy(arrowReceiverColider.gameObject);
		}
		if (extraArrowReceiverColider != null)
		{
			for (int i = 0; i < extraArrowReceiverColider.Length; i++)
			{
				UnityEngine.Object.Destroy(extraArrowReceiverColider[i]);
			}
		}
	}

	private void CheckSituation()
	{
		if (mainController.GateLifeActual <= 0f && status == CohortSoldierStatus.AttackingGate && cohortParent.type != CohortType.Siege)
		{
			myRigidbody.isKinematic = false;
			status = CohortSoldierStatus.Idle;
		}
		float num;
		float num2;
		bool flag;
		if (status != CohortSoldierStatus.Fighting && status != CohortSoldierStatus.Shooting && status != CohortSoldierStatus.Dead && status != CohortSoldierStatus.AttackingGate && status != CohortSoldierStatus.Flee)
		{
			checkSituationTimeCounter += Time.deltaTime;
			if (!(checkSituationTimeCounter >= cohortParent.SyncTime))
			{
				return;
			}
			checkSituationTimeCounter = 0f;
			SearchEnemies();
			if (targetObject != null)
			{
				num = Vector3.Distance(myTransform.position, targetTransform.position);
				num2 = Vector3.Distance(myTransform.position, cohortParent.WarfareStartTransform.position);
				flag = false;
				if (cohortParent.type == CohortType.Ranged)
				{
					if (cohortParent.stance == CohortStance.Defender)
					{
						Vector3 position = myTransform.position;
						float x = position.x;
						Vector3 position2 = cohortParent.GateTransform.position;
						if (x <= position2.x + 7f)
						{
							goto IL_01ab;
						}
					}
					if (cohortParent.stance == CohortStance.Attacker)
					{
						Vector3 position3 = myTransform.position;
						float x2 = position3.x;
						Vector3 position4 = cohortParent.WarfareTransform.position;
						if (x2 >= position4.x + 7f)
						{
							goto IL_01ab;
						}
					}
				}
				goto IL_01ad;
			}
			if (!(statsHP > 0f))
			{
				return;
			}
			if (cohortParent.stance == CohortStance.Defender)
			{
				Vector3 position5 = myTransform.position;
				float x3 = position5.x;
				Vector3 position6 = cohortParent.WarfareTransform.position;
				if (x3 < position6.x - 8f)
				{
					UpdateSituation(CohortSoldierStatus.Advancing);
					return;
				}
				Vector3 position7 = myTransform.position;
				float x4 = position7.x;
				Vector3 position8 = cohortParent.WarfareTransform.position;
				if (x4 > position8.x - 1f)
				{
					UpdateSituation(CohortSoldierStatus.Retreating);
				}
				else
				{
					UpdateSituation(CohortSoldierStatus.Idle);
				}
			}
			else if (cohortParent.type == CohortType.Ranged)
			{
				if (Vector3.Distance(myTransform.position, cohortParent.GateTransform.position) <= cohortParent.javalineRange)
				{
					if (javalineReloadCounter <= 0f)
					{
						UpdateSituation(CohortSoldierStatus.ShootingGate);
					}
				}
				else
				{
					UpdateSituation(CohortSoldierStatus.Advancing);
				}
			}
			else
			{
				UpdateSituation(CohortSoldierStatus.Advancing);
			}
			return;
		}
		if (status != CohortSoldierStatus.Fighting && status != CohortSoldierStatus.Shooting)
		{
			return;
		}
		if (targetScript != null)
		{
			if (targetScript.Status == CohortSoldierStatus.Dead || targetScript.status == CohortSoldierStatus.Flee)
			{
				UpdateSituation(CohortSoldierStatus.Idle);
			}
			else if (cohortParent.type == CohortType.Mixed && status == CohortSoldierStatus.Fighting && Vector3.Distance(myTransform.position, targetTransform.position) > 2f)
			{
				UpdateSituation(CohortSoldierStatus.Idle);
			}
		}
		else
		{
			UpdateSituation(CohortSoldierStatus.Idle);
		}
		return;
		IL_01ab:
		flag = true;
		goto IL_01ad;
		IL_01ad:
		if (cohortParent.type == CohortType.Ranged && num < cohortParent.safeDistance && ((cohortParent.stance == CohortStance.Attacker && num2 > 10f) || cohortParent.stance == CohortStance.Defender) && !flag)
		{
			UpdateSituation(CohortSoldierStatus.Retreating);
		}
		else if (num <= cohortParent.javalineRange && (cohortParent.type == CohortType.Ranged || (cohortParent.type == CohortType.Mixed && javalinesAmount > 0)))
		{
			if (javalineReloadCounter <= 0f)
			{
				UpdateSituation(CohortSoldierStatus.Shooting);
			}
		}
		else
		{
			UpdateSituation(CohortSoldierStatus.Predating);
		}
	}

	private void SearchEnemies()
	{
		if (cohortParent.stance == CohortStance.Defender && cohortParent.type == CohortType.Mixed)
		{
			Vector3 position = myTransform.position;
			float x = position.x;
			Vector3 position2 = cohortParent.GateTransform.position;
			if (x < position2.x)
			{
				targetObject = null;
				targetTransform = null;
				targetScript = null;
				return;
			}
		}
		if (cohortParent.stance == CohortStance.Attacker)
		{
			Vector3 position3 = myTransform.position;
			float x2 = position3.x;
			Vector3 position4 = cohortParent.WarfareStartTransform.position;
			if (x2 > position4.x)
			{
				targetObject = null;
				targetTransform = null;
				targetScript = null;
				return;
			}
		}
		GameObject[] array = (cohortParent.stance != CohortStance.Attacker) ? GameObject.FindGameObjectsWithTag("CohortAttack") : GameObject.FindGameObjectsWithTag("CohortDefence");
		if (array.Length > 0)
		{
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
				Vector3 position5 = component2.position;
				float x3 = position5.x;
				Vector3 position6 = cohortParent.WarfareStartTransform.position;
				if (!(x3 <= position6.x))
				{
					continue;
				}
				float num3 = Vector3.Distance(myTransform.position, component2.position);
				if (num3 <= cohortParent.findTargetDistance)
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

	private void CheckObstaclesFront(Vector3 _directionGo)
	{
		obstacleFront = false;
		obstacleLeft = false;
		obstacleRight = false;
		float maxDistance = 1.25f;
		if (Physics.Raycast(myTransform.position + new Vector3(0f, 0.5f, 0f), _directionGo.normalized, out RaycastHit hitInfo, maxDistance))
		{
			if (cohortParent.stance == CohortStance.Attacker)
			{
				if ((hitInfo.transform.CompareTag("CohortAttack") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
				{
					obstacleFront = true;
				}
			}
			else if ((hitInfo.transform.CompareTag("CohortDefence") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
			{
				obstacleFront = true;
			}
		}
		Vector3 a = Quaternion.Euler(0f, 90f, 0f) * _directionGo;
		a.Normalize();
		if (!obstacleFront && Physics.Raycast(myTransform.position + a * 0.2f + new Vector3(0f, 0.5f, 0f), _directionGo.normalized, out hitInfo, maxDistance))
		{
			if (cohortParent.stance == CohortStance.Attacker)
			{
				if ((hitInfo.transform.CompareTag("CohortAttack") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
				{
					obstacleFront = true;
				}
			}
			else if ((hitInfo.transform.CompareTag("CohortDefence") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
			{
				obstacleFront = true;
			}
		}
		if (!obstacleFront && Physics.Raycast(myTransform.position + a * -0.2f + new Vector3(0f, 0.5f, 0f), _directionGo.normalized, out hitInfo, maxDistance))
		{
			if (cohortParent.stance == CohortStance.Attacker)
			{
				if ((hitInfo.transform.CompareTag("CohortAttack") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
				{
					obstacleFront = true;
				}
			}
			else if ((hitInfo.transform.CompareTag("CohortDefence") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
			{
				obstacleFront = true;
			}
		}
		if (!obstacleFront)
		{
			return;
		}
		if (Physics.Raycast(myTransform.position + new Vector3(0f, 0.5f, 0f), (Quaternion.Euler(0f, 70f, 0f) * _directionGo).normalized, out hitInfo, maxDistance))
		{
			if (cohortParent.stance == CohortStance.Attacker)
			{
				if ((hitInfo.transform.CompareTag("CohortAttack") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
				{
					obstacleLeft = true;
				}
			}
			else if ((hitInfo.transform.CompareTag("CohortDefence") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
			{
				obstacleLeft = true;
			}
		}
		if (!obstacleLeft && Physics.Raycast(myTransform.position + new Vector3(0f, 0.5f, 0f), (Quaternion.Euler(0f, 35f, 0f) * _directionGo).normalized, out hitInfo, maxDistance))
		{
			if (cohortParent.stance == CohortStance.Attacker)
			{
				if (hitInfo.transform.CompareTag("CohortAttack") || hitInfo.transform.CompareTag("WallBoundarie"))
				{
					obstacleLeft = true;
				}
			}
			else if ((hitInfo.transform.CompareTag("CohortDefence") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
			{
				obstacleLeft = true;
			}
		}
		if (Physics.Raycast(myTransform.position + new Vector3(0f, 0.5f, 0f), (Quaternion.Euler(0f, -70f, 0f) * _directionGo).normalized, out hitInfo, maxDistance))
		{
			if (cohortParent.stance == CohortStance.Attacker)
			{
				if ((hitInfo.transform.CompareTag("CohortAttack") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
				{
					obstacleRight = true;
				}
			}
			else if ((hitInfo.transform.CompareTag("CohortDefence") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
			{
				obstacleRight = true;
			}
		}
		if (obstacleRight || !Physics.Raycast(myTransform.position + new Vector3(0f, 0.5f, 0f), (Quaternion.Euler(0f, -35f, 0f) * _directionGo).normalized, out hitInfo, maxDistance))
		{
			return;
		}
		if (cohortParent.stance == CohortStance.Attacker)
		{
			if ((hitInfo.transform.CompareTag("CohortAttack") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
			{
				obstacleRight = true;
			}
		}
		else if ((hitInfo.transform.CompareTag("CohortDefence") || hitInfo.transform.CompareTag("WallBoundarie")) && base.gameObject != hitInfo.transform.gameObject)
		{
			obstacleRight = true;
		}
	}

	private float CalculateKillReward()
	{
		float num = ConfigPrefsController.incomeMoneyPerEnemy * (float)cohortParent.moneyKill;
		float num2 = num + num * ConfigPrefsController.generalBaseIncomeKillPerLevel * (float)PlayerPrefsController.GeneralTechBase_IncomeKill;
		if (UpgradesController.isExtraMoneyActive)
		{
			num2 += num2 * ConfigPrefsController.heroeArcaniExtraMoney;
		}
		return num2;
	}

	private float CalculateKillExp()
	{
		float num = ConfigPrefsController.incomeExperiencePerEnemy * (float)cohortParent.moneyKill;
		return num + num * ConfigPrefsController.generalBaseExperienceKillPerLevel * (float)PlayerPrefsController.GeneralTechBase_ExperienceKill;
	}
}
