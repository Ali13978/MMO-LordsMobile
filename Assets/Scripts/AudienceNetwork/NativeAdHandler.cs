using UnityEngine;

namespace AudienceNetwork
{
	[RequireComponent(typeof(RectTransform))]
	public class NativeAdHandler : AdHandler
	{
		public int minViewabilityPercentage;

		public float minAlpha;

		public int maxRotation;

		public int checkViewabilityInterval;

		public Camera camera;

		public FBNativeAdHandlerValidationCallback validationCallback;

		private float lastImpressionCheckTime;

		private bool impressionLogged;

		private bool shouldCheckImpression;

		public void startImpressionValidation()
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			shouldCheckImpression = true;
		}

		public void stopImpressionValidation()
		{
			shouldCheckImpression = false;
		}

		private void OnGUI()
		{
			checkImpression();
		}

		private bool checkImpression()
		{
			float time = Time.time;
			float num = time - lastImpressionCheckTime;
			if (shouldCheckImpression && !impressionLogged && num > (float)checkViewabilityInterval)
			{
				lastImpressionCheckTime = time;
				GameObject gameObject = base.gameObject;
				Camera x = camera;
				if (x == null)
				{
					x = GetComponent<Camera>();
				}
				if (x == null)
				{
					x = Camera.main;
				}
				while (gameObject != null)
				{
					Canvas component = gameObject.GetComponent<Canvas>();
					if (component != null && component.renderMode == RenderMode.WorldSpace)
					{
						break;
					}
					if (!checkGameObjectViewability(x, gameObject))
					{
						if (validationCallback != null)
						{
							validationCallback(success: false);
						}
						return false;
					}
					gameObject = null;
				}
				if (validationCallback != null)
				{
					validationCallback(success: true);
				}
				impressionLogged = true;
			}
			return impressionLogged;
		}

		private bool logViewability(bool success, string message)
		{
			if (!success)
			{
				UnityEngine.Debug.Log("Viewability validation failed: " + message);
			}
			else
			{
				UnityEngine.Debug.Log("Viewability validation success! " + message);
			}
			return success;
		}

		private bool checkGameObjectViewability(Camera camera, GameObject gameObject)
		{
			if (gameObject == null)
			{
				return logViewability(success: false, "GameObject is null.");
			}
			if (camera == null)
			{
				return logViewability(success: false, "Camera is null.");
			}
			if (!gameObject.activeInHierarchy)
			{
				return logViewability(success: false, "GameObject is not active in hierarchy.");
			}
			CanvasGroup[] components = gameObject.GetComponents<CanvasGroup>();
			CanvasGroup[] array = components;
			foreach (CanvasGroup canvasGroup in array)
			{
				if (canvasGroup.alpha < minAlpha)
				{
					return logViewability(success: false, "GameObject has a CanvasGroup with less than the minimum alpha required.");
				}
			}
			RectTransform rectTransform = gameObject.transform as RectTransform;
			Vector3 position = rectTransform.position;
			float width = rectTransform.rect.width;
			float height = rectTransform.rect.height;
			Vector3 position2 = position;
			position2.x -= width / 2f;
			position2.y -= height / 2f;
			Vector3 position3 = position;
			position3.x += width / 2f;
			position3.y += height / 2f;
			Vector3 lowerLeft = camera.WorldToScreenPoint(position2);
			Vector3 upperRight = camera.WorldToScreenPoint(position3);
			float num = upperRight.x - lowerLeft.x;
			float num2 = upperRight.y - lowerLeft.y;
			Rect pixelRect = camera.pixelRect;
			Rect screen = new Rect(pixelRect.x * Screen.dpi, pixelRect.y * Screen.dpi, pixelRect.width * Screen.dpi, pixelRect.height * Screen.dpi);
			if (num <= 0f && num2 <= 0f)
			{
				return logViewability(success: false, "GameObject's height/width is less than or equal to zero.");
			}
			if (!CheckScreenPosition(lowerLeft, upperRight, screen))
			{
				return logViewability(success: false, "Not enough of the GameObject is inside the viewport.");
			}
			if (num / width < (float)minViewabilityPercentage || num2 / height < (float)minViewabilityPercentage)
			{
				return logViewability(success: false, "The GameObject is too small to count as an impression.");
			}
			Vector3 eulerAngles = rectTransform.eulerAngles;
			int num3 = Mathf.FloorToInt(eulerAngles.x);
			int num4 = Mathf.FloorToInt(eulerAngles.y);
			int num5 = Mathf.FloorToInt(eulerAngles.z);
			int num6 = 360 - maxRotation;
			int num7 = maxRotation;
			if (num3 < num6 && num3 > num7)
			{
				return logViewability(success: false, "GameObject is rotated too much. (x axis)");
			}
			if (num4 < num6 && num4 > num7)
			{
				return logViewability(success: false, "GameObject is rotated too much. (y axis)");
			}
			if (num5 < num6 && num5 > num7)
			{
				return logViewability(success: false, "GameObject is rotated too much. (z axis)");
			}
			return logViewability(success: true, "--------------- VALID IMPRESSION REGISTERED! ----------------------");
		}

		private bool CheckScreenPosition(Vector3 lowerLeft, Vector3 upperRight, Rect screen)
		{
			float num = 0f;
			float num2 = 0f;
			if (lowerLeft.x < screen.xMin)
			{
				num += Mathf.Abs(lowerLeft.x - screen.xMin);
			}
			if (upperRight.x > screen.xMax)
			{
				num += Mathf.Abs(upperRight.x - screen.xMax);
			}
			float num3 = 1f - num / (upperRight.x - lowerLeft.x);
			if (num3 < (float)minViewabilityPercentage)
			{
				return false;
			}
			if (lowerLeft.y < screen.yMin)
			{
				num2 += Mathf.Abs(lowerLeft.y - screen.yMin);
			}
			if (upperRight.y > screen.yMax)
			{
				num2 += Mathf.Abs(upperRight.y - screen.yMax);
			}
			float num4 = 1f - num2 / (upperRight.y - lowerLeft.y);
			if (num4 < (float)minViewabilityPercentage)
			{
				return false;
			}
			return true;
		}
	}
}
