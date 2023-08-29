using UnityEngine;

public class Touch_Battle : MonoBehaviour
{
	public static bool IsWindowBigOpen;

	public static bool IsWindowSmallOpen;

	public float moveSpeedMin;

	public float moveSpeedMax;

	public float zoomSpeed;

	public float _zoomMin;

	public float _zoomMax;

	private bool isTouchingBackground;

	private Transform cameraTransform;

	private Camera myCamera;

	private UIController uiController;

	public Canvas[] canvasCheck;

	private float accumulatedMovementX;

	private float accumulatedThreshold = 0.001f;

	private float speedScroll = 7.5f;

	private float speedResistance = 5.5f;

	private float amountMove;

	private void Awake()
	{
		uiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
		if (MainController.worldScreen != WorldScreen.Attack)
		{
			myCamera = GameObject.FindGameObjectWithTag("CameraDefence").GetComponent<Camera>();
			GameObject gameObject = GameObject.FindGameObjectWithTag("CameraAttack");
			gameObject.SetActive(value: false);
		}
		else
		{
			myCamera = GameObject.FindGameObjectWithTag("CameraAttack").GetComponent<Camera>();
			GameObject gameObject2 = GameObject.FindGameObjectWithTag("CameraDefence");
			gameObject2.SetActive(value: false);
		}
		for (int i = 0; i < canvasCheck.Length; i++)
		{
			canvasCheck[i].worldCamera = myCamera;
		}
		cameraTransform = myCamera.GetComponent<Transform>();
	}

	private void Start()
	{
	}

	public void TouchingBackground(bool _isTouchingBackground)
	{
		isTouchingBackground = _isTouchingBackground;
	}

	private void Update()
	{
		if (!IsWindowBigOpen && isTouchingBackground)
		{
			if (UnityEngine.Input.touchCount == 1)
			{
				if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
				{
					accumulatedMovementX = 0f;
				}
				Vector2 vector = Input.touches[0].deltaPosition / Screen.width;
				float num = vector.x / Input.touches[0].deltaTime;
				if (!float.IsNaN(num) && !float.IsInfinity(num) && num != 0f)
				{
					if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
					{
						if ((accumulatedMovementX > 0f && num > 0f) || (accumulatedMovementX < 0f && num < 0f))
						{
							accumulatedMovementX = 0f;
						}
						accumulatedMovementX -= num;
						if (uiController.TutorialScrollLabel != null)
						{
							UnityEngine.Object.Destroy(uiController.TutorialScrollLabel);
						}
					}
					else
					{
						if ((accumulatedMovementX > 0f && num < 0f) || (accumulatedMovementX < 0f && num > 0f))
						{
							accumulatedMovementX = 0f;
						}
						accumulatedMovementX += num;
					}
				}
			}
			if (UnityEngine.Input.touchCount == 2 && Application.platform == RuntimePlatform.Android)
			{
				Touch touch = UnityEngine.Input.GetTouch(0);
				Touch touch2 = UnityEngine.Input.GetTouch(1);
				Vector2 a = touch.position - touch.deltaPosition;
				Vector2 b = touch2.position - touch2.deltaPosition;
				float magnitude = (a - b).magnitude;
				float magnitude2 = (touch.position - touch2.position).magnitude;
				float num2 = magnitude - magnitude2;
				if (myCamera.fieldOfView < _zoomMax && num2 > 0f)
				{
					myCamera.fieldOfView += zoomSpeed * Time.unscaledDeltaTime;
				}
				if (myCamera.fieldOfView > _zoomMin && num2 < 0f)
				{
					myCamera.fieldOfView -= zoomSpeed * Time.unscaledDeltaTime;
				}
				if (myCamera.fieldOfView > _zoomMax)
				{
					myCamera.fieldOfView = _zoomMax;
				}
				if (myCamera.fieldOfView < _zoomMin)
				{
					myCamera.fieldOfView = _zoomMin;
				}
			}
		}
		if (accumulatedMovementX > accumulatedThreshold || accumulatedMovementX < 0f - accumulatedThreshold)
		{
			amountMove = accumulatedMovementX * Time.unscaledDeltaTime;
			accumulatedMovementX -= amountMove * speedResistance;
		}
		else
		{
			amountMove = 0f;
			accumulatedMovementX = 0f;
		}
	}

	private void LateUpdate()
	{
		if (amountMove != 0f)
		{
			cameraTransform.localPosition += new Vector3(1f, 0f, 0f) * amountMove * speedScroll;
		}
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			float num = Mathf.Lerp(85f, 88.8f, (_zoomMax - myCamera.fieldOfView) / (_zoomMax - _zoomMin));
			float num2 = Mathf.Lerp(56f, 53f, (_zoomMax - myCamera.fieldOfView) / (_zoomMax - _zoomMin));
			Vector3 localPosition = cameraTransform.localPosition;
			if (localPosition.x > num)
			{
				Transform transform = cameraTransform;
				float x = num;
				Vector3 position = cameraTransform.position;
				float y = position.y;
				Vector3 position2 = cameraTransform.position;
				transform.localPosition = new Vector3(x, y, position2.z);
				return;
			}
			Vector3 localPosition2 = cameraTransform.localPosition;
			if (localPosition2.x < num2)
			{
				Transform transform2 = cameraTransform;
				float x2 = num2;
				Vector3 position3 = cameraTransform.position;
				float y2 = position3.y;
				Vector3 position4 = cameraTransform.position;
				transform2.localPosition = new Vector3(x2, y2, position4.z);
			}
		}
		else
		{
			if (MainController.worldScreen != WorldScreen.Defence && MainController.worldScreen != 0)
			{
				return;
			}
			float num3 = Mathf.Lerp(72f, 76f, (_zoomMax - myCamera.fieldOfView) / (_zoomMax - _zoomMin));
			float num4 = Mathf.Lerp(56f, 52.5f, (_zoomMax - myCamera.fieldOfView) / (_zoomMax - _zoomMin));
			Vector3 localPosition3 = cameraTransform.localPosition;
			if (localPosition3.x > num3)
			{
				Transform transform3 = cameraTransform;
				float x3 = num3;
				Vector3 position5 = cameraTransform.position;
				float y3 = position5.y;
				Vector3 position6 = cameraTransform.position;
				transform3.localPosition = new Vector3(x3, y3, position6.z);
				return;
			}
			Vector3 localPosition4 = cameraTransform.localPosition;
			if (localPosition4.x < num4)
			{
				Transform transform4 = cameraTransform;
				float x4 = num4;
				Vector3 position7 = cameraTransform.position;
				float y4 = position7.y;
				Vector3 position8 = cameraTransform.position;
				transform4.localPosition = new Vector3(x4, y4, position8.z);
			}
		}
	}

	public void MoveCameraToSpawnPoint()
	{
		Vector3 position = cameraTransform.position;
		float num = (position.x - 56f) * 0.85f;
		accumulatedMovementX = 0f - num;
	}

	public void MoveCameraToWalls()
	{
		Vector3 position = cameraTransform.position;
		float num = (position.x - 61f) * 0.55f;
		accumulatedMovementX = 0f - num;
	}
}
