using UnityEngine;

public class GemRewardDropAnimation : MonoBehaviour
{
	private Camera cam;

	public void Initialize(int index)
	{
		cam = Camera.current;
		Vector3 vector = cam.WorldToScreenPoint(new Vector3(0f, 0f, 0f));
		LeanTween.move(base.gameObject, new Vector2(vector.x + (float)index, 1.5f), 1.25f).setEaseInOutSine().setIgnoreTimeScale(useUnScaledTime: true);
		LeanTween.moveY(base.gameObject, 0.5f, 1.25f).setEaseOutBounce().setIgnoreTimeScale(useUnScaledTime: true);
	}

	public void Collect()
	{
	}
}
