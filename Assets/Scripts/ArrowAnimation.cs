using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
	public Vector3 movePlusVector;

	public float moveTime;

	public LeanTweenType moveEase;

	private Transform myTransform;

	private void Awake()
	{
		myTransform = base.gameObject.GetComponent<Transform>();
	}

	private void Start()
	{
		LeanTween.move(base.gameObject, myTransform.position + movePlusVector, moveTime).setEase(moveEase).setLoopPingPong()
			.setIgnoreTimeScale(useUnScaledTime: true);
	}
}
