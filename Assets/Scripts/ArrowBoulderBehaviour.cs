using UnityEngine;

public class ArrowBoulderBehaviour : MonoBehaviour
{
	public float timeToDestroy;

	public GameObject prefabExplosion;

	private Vector3 middlePosition;

	private Vector3 finalPosition;

	private float arrowATK;

	private float timeMove;

	private CatapultAmmoType ammoType;

	private bool isStucked;

	private bool isShooted;

	private GameObject tempExplosion;

	public SphereCollider mySphereCollider;

	private UpgradesController upgradesController;

	public AudioClip[] sfxHit;

	private Transform myTransform;

	private SphereCollider myCollider;

	private AudioSource audioSource;

	public float ArrowATK
	{
		get
		{
			return arrowATK;
		}
		set
		{
			arrowATK = value;
		}
	}

	private void Awake()
	{
		upgradesController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UpgradesController>();
		audioSource = base.gameObject.GetComponent<AudioSource>();
		myTransform = base.gameObject.GetComponent<Transform>();
		myCollider = base.gameObject.GetComponent<SphereCollider>();
	}

	private void Update()
	{
		if (isStucked)
		{
			timeToDestroy -= Time.deltaTime;
			if (timeToDestroy <= 0f)
			{
				UnityEngine.Object.Destroy(tempExplosion);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (!isStucked && isShooted && col.CompareTag("Floor"))
		{
			bool isBonusSiege = false;
			if (ammoType == CatapultAmmoType.Big)
			{
				isBonusSiege = true;
			}
			CheckRadiusEffectCollision(isBonusSiege);
			isStucked = true;
			LeanTween.cancel(base.gameObject);
			myTransform.SetParent(col.transform);
			Transform transform = myTransform;
			Vector3 position = myTransform.position;
			float x = position.x;
			Vector3 position2 = myTransform.position;
			transform.position = new Vector3(x, 0f, position2.z);
			tempExplosion = (UnityEngine.Object.Instantiate(prefabExplosion, myTransform.position + new Vector3(0f, 0.025f, 0f) + new Vector3(0f, 0f, 0f), Quaternion.Euler(new Vector3(90f, 0f, 0f))) as GameObject);
			UnityEngine.Object.Destroy(myCollider);
		}
	}

	public void SetTarget(Vector3 _initialPosition, Vector3 _middlePosition, Vector3 _finalPosition, float _timeArrow, float _arrowATK, CatapultAmmoType _ammoType)
	{
		myTransform.SetParent(null);
		middlePosition = _middlePosition;
		finalPosition = _finalPosition;
		timeMove = _timeArrow;
		arrowATK = _arrowATK;
		ammoType = _ammoType;
		isShooted = true;
		Vector3[] to = new Vector3[4]
		{
			_initialPosition,
			_middlePosition - new Vector3(0f, 0f, 1f),
			_middlePosition + new Vector3(0f, 0f, 1f),
			_finalPosition
		};
		LeanTween.move(base.gameObject, to, _timeArrow);
	}

	private void CheckRadiusEffectCollision(bool _isBonusSiege)
	{
		if (MainController.worldScreen == WorldScreen.AttackStarted)
		{
			for (int i = 0; i < upgradesController.CohortsFriendArray.Count; i++)
			{
				upgradesController.CohortsFriendArray[i].CheckRadiusCollision(mySphereCollider.bounds, arrowATK, _isBonusSiege);
			}
		}
		else if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			for (int j = 0; j < upgradesController.CohortsEnemyArray.Count; j++)
			{
				upgradesController.CohortsEnemyArray[j].CheckRadiusCollision(mySphereCollider.bounds, arrowATK, _isBonusSiege);
			}
		}
		int num = UnityEngine.Random.Range(0, sfxHit.Length);
		if (PlayerPrefsController.isSfx)
		{
			audioSource.PlayOneShot(sfxHit[num], AudioPrefsController.volumeBattleBoulderHitGround * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}
}
