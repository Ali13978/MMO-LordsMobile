using UnityEngine;

public class Touch_Map : MonoBehaviour
{
	public static bool IsWindowOpen;

	public float moveSpeedMin;

	public float moveSpeedMax;

	public float zoomSpeed;

	public float _zoomMin;

	public float _zoomMax;

	private int touchIndex;

	private float touchAccumulatedMovement;

	private float startX;

	private float startY;

	public Transform cameraTransform;

	public Camera myCamera;

	private float cameraX;

	private float cameraY;

	private void Awake()
	{
		if (PlayerPrefs.HasKey("cameraX") && PlayerPrefs.HasKey("cameraY"))
		{
			UpdateCameraPosition();
		}
	}

	private void Update()
	{
		Vector3 localPosition = cameraTransform.localPosition;
		cameraX = localPosition.x;
		Vector3 localPosition2 = cameraTransform.localPosition;
		cameraY = localPosition2.y;
		if (IsWindowOpen || UIWindowTutorial.WindowOpen)
		{
			return;
		}
		if (UnityEngine.Input.touchCount == 1)
		{
			Touch touch = UnityEngine.Input.GetTouch(0);
			switch (touch.phase)
			{
			case TouchPhase.Began:
			{
				Vector2 position = touch.position;
				startX = position.x;
				Vector2 position2 = touch.position;
				startY = position2.y;
				touchIndex = touch.fingerId;
				touchAccumulatedMovement = 0f;
				break;
			}
			case TouchPhase.Moved:
			{
				if (touchIndex != touch.fingerId)
				{
					break;
				}
				float num = 0f;
				float num2 = 0f;
				float d = Mathf.Lerp(moveSpeedMax, moveSpeedMin, (_zoomMax - myCamera.orthographicSize) / (_zoomMax - _zoomMin));
				float num3 = startX;
				Vector2 position3 = touch.position;
				if (num3 > position3.x)
				{
					float num4 = startX;
					Vector2 position4 = touch.position;
					num = (num4 - position4.x) / (float)Screen.width;
					cameraTransform.localPosition += new Vector3(1f, 0f, 0f) * num * d * Time.deltaTime;
				}
				else
				{
					float num5 = startX;
					Vector2 position5 = touch.position;
					if (num5 < position5.x)
					{
						Vector2 position6 = touch.position;
						num = (position6.x - startX) / (float)Screen.width;
						cameraTransform.localPosition -= new Vector3(1f, 0f, 0f) * num * d * Time.deltaTime;
					}
				}
				float num6 = startY;
				Vector2 position7 = touch.position;
				if (num6 > position7.y)
				{
					float num7 = startY;
					Vector2 position8 = touch.position;
					num2 = (num7 - position8.y) / (float)Screen.height;
					cameraTransform.localPosition += new Vector3(0f, 1f, 0f) * num2 * d * Time.deltaTime;
				}
				else
				{
					float num8 = startY;
					Vector2 position9 = touch.position;
					if (num8 < position9.y)
					{
						Vector2 position10 = touch.position;
						num2 = (position10.y - startY) / (float)Screen.height;
						cameraTransform.localPosition -= new Vector3(0f, 1f, 0f) * num2 * d * Time.deltaTime;
					}
				}
				Vector2 position11 = touch.position;
				startX = position11.x;
				Vector2 position12 = touch.position;
				startY = position12.y;
				touchAccumulatedMovement += num + num2;
				break;
			}
			case TouchPhase.Canceled:
				touchIndex = -1;
				startX = 0f;
				startY = 0f;
				touchAccumulatedMovement = 0f;
				break;
			case TouchPhase.Ended:
				if (touchIndex != touch.fingerId || touchAccumulatedMovement <= 0.08f)
				{
				}
				touchIndex = -1;
				startX = 0f;
				startY = 0f;
				touchAccumulatedMovement = 0f;
				break;
			}
		}
		Vector3 vector = myCamera.ScreenToWorldPoint(new Vector3(0f, 0f, myCamera.nearClipPlane));
		Vector3 vector2 = myCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, myCamera.nearClipPlane));
		float num9 = -15.3f;
		float num10 = 15.3f;
		float num11 = 10.3f;
		float num12 = -10f;
		if (vector.x < num9)
		{
			float num13 = num9 - vector.x;
			Transform transform = cameraTransform;
			Vector3 position13 = cameraTransform.position;
			float x = position13.x + num13;
			Vector3 position14 = cameraTransform.position;
			float y = position14.y;
			Vector3 position15 = cameraTransform.position;
			transform.localPosition = new Vector3(x, y, position15.z);
		}
		else if (vector2.x > num10)
		{
			float num14 = num10 - vector2.x;
			Transform transform2 = cameraTransform;
			Vector3 position16 = cameraTransform.position;
			float x2 = position16.x + num14;
			Vector3 position17 = cameraTransform.position;
			float y2 = position17.y;
			Vector3 position18 = cameraTransform.position;
			transform2.localPosition = new Vector3(x2, y2, position18.z);
		}
		if (vector2.y > num11)
		{
			float num15 = num11 - vector2.y;
			Transform transform3 = cameraTransform;
			Vector3 position19 = cameraTransform.position;
			float x3 = position19.x;
			Vector3 position20 = cameraTransform.position;
			float y3 = position20.y + num15;
			Vector3 position21 = cameraTransform.position;
			transform3.localPosition = new Vector3(x3, y3, position21.z);
		}
		else if (vector.y < num12)
		{
			float num16 = num12 - vector.y;
			Transform transform4 = cameraTransform;
			Vector3 position22 = cameraTransform.position;
			float x4 = position22.x;
			Vector3 position23 = cameraTransform.position;
			float y4 = position23.y + num16;
			Vector3 position24 = cameraTransform.position;
			transform4.localPosition = new Vector3(x4, y4, position24.z);
		}
	}

	private void UpdateCameraPosition()
	{
		PlayerPrefsController.LoadMapCameraPosition(cameraTransform);
	}

	private void OnDestroy()
	{
		PlayerPrefsController.SaveMapCameraPosition(cameraX, cameraY);
	}
}
