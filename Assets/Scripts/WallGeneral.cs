using UnityEngine;

public class WallGeneral : MonoBehaviour
{
	public Animator myAnimator;

	public Transform mySpriteTransform;

	private void Awake()
	{
		if (MainController.worldScreen == WorldScreen.Attack)
		{
			mySpriteTransform.localRotation = Quaternion.Euler(new Vector3(0f, 200f, 0f));
			SpriteRenderer component = mySpriteTransform.GetComponent<SpriteRenderer>();
			component.sortingOrder = 0;
		}
		else
		{
			mySpriteTransform.localRotation = Quaternion.Euler(new Vector3(0f, 340f, 0f));
		}
	}

	public void GiveOrder()
	{
		myAnimator.SetTrigger("Order");
	}
}
