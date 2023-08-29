using UnityEngine;

public class AnimationLevelUp : MonoBehaviour
{
	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
		LeanTween.delayedCall(3f, DestroyMe).setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void DestroyMe()
	{
		anim.SetTrigger("Destroy");
		UnityEngine.Object.Destroy(base.gameObject, 1f);
	}
}
