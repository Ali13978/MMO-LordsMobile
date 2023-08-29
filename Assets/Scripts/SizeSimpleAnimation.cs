using UnityEngine;

public class SizeSimpleAnimation : MonoBehaviour
{
	private void Awake()
	{
		LeanTween.scale(base.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong()
			.setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void Update()
	{
	}
}
