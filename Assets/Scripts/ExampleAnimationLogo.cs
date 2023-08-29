using UnityEngine;

public class ExampleAnimationLogo : MonoBehaviour
{
	private RectTransform myTransform;

	private void Awake()
	{
		myTransform = base.gameObject.GetComponent<RectTransform>();
	}

	private void Start()
	{
		LeanTween.scale(myTransform, new Vector3(1.15f, 1.15f, 1.15f), 0.95f).setLoopPingPong().setEase(LeanTweenType.easeInOutSine);
	}

	private void Update()
	{
	}
}
