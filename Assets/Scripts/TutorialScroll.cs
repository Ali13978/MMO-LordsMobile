using UnityEngine;
using UnityEngine.UI;

public class TutorialScroll : MonoBehaviour
{
	public RectTransform transformHand;

	public RectTransform transformCircle;

	public Image imageCircle;

	public Image imageHand;

	private Color colorOff = new Color(1f, 1f, 1f, 0f);

	private Color colorHandOn = new Color(1f, 1f, 1f, 1f);

	private Color colorCircleOn = new Color(1f, 1f, 0f, 1f);

	private Vector3 vectorInitialScaleHand;

	private Vector2 vectorInitialPositionHand;

	private Vector3 vectorInitialRotationHand;

	private Vector3 vectorInitialScaleCircle;

	private void Awake()
	{
		vectorInitialScaleHand = transformHand.localScale;
		vectorInitialPositionHand = transformHand.anchoredPosition;
		vectorInitialRotationHand = transformHand.localRotation.eulerAngles;
		vectorInitialScaleCircle = transformCircle.localScale;
		StepFirst();
	}

	private void StepFirst()
	{
		imageHand.color = colorOff;
		imageCircle.color = colorOff;
		transformHand.anchoredPosition = vectorInitialPositionHand;
		transformHand.localRotation = Quaternion.Euler(vectorInitialRotationHand);
		transformHand.localScale = vectorInitialScaleHand * 1.2f;
		transformCircle.localScale = vectorInitialScaleCircle;
		StepSecond();
	}

	private void StepSecond()
	{
		LeanTween.color(transformHand, colorHandOn, 0.4f).setOnComplete(StepThird).setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void StepThird()
	{
		LeanTween.scale(transformHand.gameObject, vectorInitialScaleHand, 0.5f).setEase(LeanTweenType.easeInOutSine).setOnComplete(StepFourth)
			.setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void StepFourth()
	{
		float num = 0.75f;
		LeanTween.scale(transformCircle, new Vector3(1f, 1f, 1f), num).setEase(LeanTweenType.easeInOutSine).setOnComplete(StepFifth)
			.setIgnoreTimeScale(useUnScaledTime: true);
		LeanTween.color(transformCircle, colorCircleOn, num / 2f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong(1)
			.setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void StepFifth()
	{
		LeanTween.move(transformHand, vectorInitialPositionHand + new Vector2(-400f, 0f), 1.5f).setEase(LeanTweenType.easeOutSine).setOnComplete(StepSixth)
			.setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void StepSixth()
	{
		LeanTween.color(transformHand, colorOff, 0.5f).setEase(LeanTweenType.easeInOutSine).setOnComplete(StepRepeat)
			.setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void StepRepeat()
	{
		StepFirst();
	}
}
