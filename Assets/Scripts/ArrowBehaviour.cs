using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
	public float timeToDestroy;

	private Vector3 middlePosition;

	private Vector3 finalPosition;

	private float arrowATK;

	private float timeMove;

	private bool isStucked;

	public LineRenderer myLineRenderer;

	private Vector3[] lastPositionTrail = new Vector3[3];

	private float timeTrail;

	private float timeTrailFlag = 0.05f;

	private CohortSoldier wallArcherTarget;

	private Transform myTransform;

	private BoxCollider myCollider;

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
	}

	private void Start()
	{
		lastPositionTrail[0] = myTransform.position;
		lastPositionTrail[1] = myTransform.position;
		lastPositionTrail[2] = myTransform.position;
	}

	private void Update()
	{
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.Upgrade)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (isStucked)
		{
			timeToDestroy -= Time.deltaTime;
			if (timeToDestroy <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (myLineRenderer != null)
		{
			myLineRenderer.SetPosition(0, myTransform.position - myTransform.up * 0.25f);
			myLineRenderer.SetPosition(1, lastPositionTrail[0]);
			myLineRenderer.SetPosition(2, lastPositionTrail[1]);
			myLineRenderer.SetPosition(3, lastPositionTrail[2]);
			UpdateTrailPosition();
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
		if (!isStucked && !col.CompareTag("ArrowDefence") && !col.CompareTag("JavalineDefence") && !col.CompareTag("JavalineAttack") && !col.CompareTag("JavalineGate") && ((base.gameObject.CompareTag("JavalineAttack") && col.CompareTag("ArrowReceiverDefence")) || (base.gameObject.CompareTag("JavalineDefence") && col.CompareTag("ArrowReceiverAttack")) || (base.gameObject.CompareTag("ArrowDefence") && col.CompareTag("ArrowReceiverAttack")) || (base.gameObject.CompareTag("JavalineGate") && col.CompareTag("Gate")) || col.CompareTag("Floor")))
		{
			isStucked = true;
			LeanTween.cancel(base.gameObject);
			myTransform.SetParent(col.transform);
			if (col.CompareTag("Floor") || !col.CompareTag("Gate"))
			{
			}
			UnityEngine.Object.Destroy(myCollider);
			Invoke("DestroyLineRenderer", 1f);
			if (col.CompareTag("Gate"))
			{
				GateController component = col.gameObject.GetComponent<GateController>();
				component.Hitted(arrowATK);
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (col.CompareTag("ArrowReceiverDefence") || col.CompareTag("ArrowReceiverAttack"))
			{
				CohortSoldierArrowReceiver component2 = col.GetComponent<CohortSoldierArrowReceiver>();
				component2.Hitted(arrowATK);
			}
			else if (wallArcherTarget != null)
			{
				wallArcherTarget.Hitted(arrowATK, _isMelee: false);
			}
		}
	}

	private void DestroyLineRenderer()
	{
		UnityEngine.Object.Destroy(myLineRenderer);
	}

	public void SetTarget(Vector3 _middlePosition, Vector3 _finalPosition, float _timeArrow, float _arrowATK, CohortSoldier _wallArcherTarget)
	{
		wallArcherTarget = _wallArcherTarget;
		middlePosition = _middlePosition;
		finalPosition = _finalPosition;
		Vector3[] to = new Vector3[4]
		{
			myTransform.position,
			_middlePosition,
			_middlePosition,
			_finalPosition
		};
		timeMove = _timeArrow;
		arrowATK = _arrowATK;
		LeanTween.move(base.gameObject, to, timeMove);
		float add = 0f;
		if (finalPosition.x > middlePosition.x)
		{
			add = -30f;
		}
		else if (finalPosition.x < middlePosition.x)
		{
			add = -30f;
		}
		LeanTween.rotateAroundLocal(base.gameObject, Vector3.forward, add, timeMove * 2f);
	}
}
