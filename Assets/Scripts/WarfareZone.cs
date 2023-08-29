using UnityEngine;

public class WarfareZone : MonoBehaviour
{
	private bool touchStarted;

	public GameObject prefabTarget;

	private Vector3 positionTarget;

	private GameObject targetObject;

	private Transform targetTransform;

	public UIController uiController;

	private Camera enabledCamera;

	private void Start()
	{
		if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			enabledCamera = GameObject.FindGameObjectWithTag("CameraDefence").GetComponent<Camera>();
		}
		else if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			enabledCamera = GameObject.FindGameObjectWithTag("CameraAttack").GetComponent<Camera>();
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = enabledCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
			int mask = LayerMask.GetMask("Warfare");
			if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f, mask))
			{
				Vector3 point = hitInfo.point;
				float x = point.x;
				Vector3 point2 = hitInfo.point;
				positionTarget = new Vector3(x, 0.05f, point2.z);
				targetObject = (UnityEngine.Object.Instantiate(prefabTarget, positionTarget, Quaternion.Euler(new Vector3(90f, 0f, 0f))) as GameObject);
				targetTransform = targetObject.GetComponent<Transform>();
				touchStarted = true;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (touchStarted)
			{
				Ray ray2 = enabledCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
				int mask2 = LayerMask.GetMask("Warfare");
				if (Physics.Raycast(ray2, out RaycastHit hitInfo2, 500f, mask2))
				{
					Vector3 point3 = hitInfo2.point;
					float x2 = point3.x;
					Vector3 point4 = hitInfo2.point;
					positionTarget = new Vector3(x2, 0.05f, point4.z);
					DestroyTarget(_isCanceled: false, positionTarget);
				}
			}
			DestroyTarget(_isCanceled: true, Vector3.zero);
		}
		if (touchStarted)
		{
			Ray ray3 = enabledCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
			int mask3 = LayerMask.GetMask("Warfare");
			if (Physics.Raycast(ray3, out RaycastHit hitInfo3, 500f, mask3))
			{
				Vector3 point5 = hitInfo3.point;
				float x3 = point5.x;
				Vector3 point6 = hitInfo3.point;
				positionTarget = new Vector3(x3, 0.05f, point6.z);
				targetTransform.position = positionTarget;
			}
			else
			{
				DestroyTarget(_isCanceled: true, Vector3.zero);
			}
		}
	}

	public void DestroyTarget(bool _isCanceled, Vector3 _positionTarget)
	{
		UnityEngine.Object.Destroy(targetObject);
		touchStarted = false;
		if (UIController.IsShootingCatapult)
		{
			uiController.FinishedCatapultShoot(_isCanceled, _positionTarget);
		}
		else if (UIController.IsShootingPower)
		{
			uiController.FinishedPowerShoot(_isCanceled, _positionTarget);
		}
	}
}
