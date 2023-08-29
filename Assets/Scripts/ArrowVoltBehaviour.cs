using UnityEngine;

public class ArrowVoltBehaviour : MonoBehaviour
{
	public float timeToDestroy;

	public float forceMove;

	public float timeToDestroyNotStucked;

	public bool stuckInSoldiers;

	private float arrowATK;

	private TowerAmmoType ammoType;

	private bool isStucked;

	public LineRenderer myLineRenderer;

	private Vector3[] lastPositionTrail = new Vector3[3];

	private float timeTrail;

	private float timeTrailFlag = 0.05f;

	private Transform myTransform;

	private BoxCollider myCollider;

	private Rigidbody myRigidbody;

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
		myTransform = base.gameObject.GetComponent<Transform>();
		myCollider = base.gameObject.GetComponent<BoxCollider>();
		myRigidbody = base.gameObject.GetComponent<Rigidbody>();
	}

	private void Start()
	{
		lastPositionTrail[0] = myTransform.position;
		lastPositionTrail[1] = myTransform.position;
		lastPositionTrail[2] = myTransform.position;
	}

	private void Update()
	{
		if (isStucked)
		{
			timeToDestroy -= Time.deltaTime;
			if (timeToDestroy <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		if (myLineRenderer != null)
		{
			myLineRenderer.SetPosition(0, myTransform.position);
			myLineRenderer.SetPosition(1, lastPositionTrail[0]);
			myLineRenderer.SetPosition(2, lastPositionTrail[1]);
			myLineRenderer.SetPosition(3, lastPositionTrail[2]);
			UpdateTrailPosition();
		}
		timeToDestroyNotStucked -= Time.deltaTime;
		if (timeToDestroyNotStucked <= 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void UpdateTrailPosition()
	{
		timeTrail += Time.deltaTime;
		if (timeTrail >= timeTrailFlag)
		{
			timeTrail = 0f;
			lastPositionTrail[2] = lastPositionTrail[1];
			lastPositionTrail[1] = lastPositionTrail[0];
			lastPositionTrail[0] = myTransform.position;
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (isStucked)
		{
			return;
		}
		if (col.CompareTag("ArrowReceiverAttack"))
		{
			CohortSoldierArrowReceiver component = col.GetComponent<CohortSoldierArrowReceiver>();
			float num = arrowATK;
			if (component.soldierScript.CohortParent.type == CohortType.Siege && ammoType == TowerAmmoType.Big)
			{
				num += arrowATK * ConfigPrefsController.bonusBigAmmoVsSiege;
			}
			else if (component.soldierScript.CohortParent.type != CohortType.Siege && ammoType == TowerAmmoType.Small)
			{
				num += arrowATK * ConfigPrefsController.bonusSmallAmmoVsSoldier;
			}
			component.Hitted(num);
		}
		if (col.CompareTag("Floor") || (col.CompareTag("ArrowReceiverAttack") && stuckInSoldiers))
		{
			isStucked = true;
			myRigidbody.velocity = Vector3.zero;
			UnityEngine.Object.Destroy(myLineRenderer);
			myTransform.SetParent(col.transform);
			if (!col.CompareTag("Floor"))
			{
				Transform transform = myTransform;
				Vector3 localPosition = myTransform.localPosition;
				transform.localPosition = new Vector3(0f, localPosition.y, 0f);
			}
			UnityEngine.Object.Destroy(myCollider);
			UnityEngine.Object.Destroy(myRigidbody);
		}
	}

	public void StartShoot(TowerAmmoType _ammoType)
	{
		ammoType = _ammoType;
		myTransform.SetParent(null);
		myRigidbody.AddForce(myTransform.right * forceMove);
	}
}
