using UnityEngine;

public class AdCamera : MonoBehaviour
{
	public SimpleTouchPad touchPad;

	public float rotationSpeed = 10f;

	public float speed = 10f;

	public GameObject target;

	private float currentTranslation;

	private Vector3 point;

	private void Start()
	{
		point = target.transform.position;
		base.transform.LookAt(point);
	}

	private void FixedUpdate()
	{
		Vector2 direction = touchPad.GetDirection();
		float num = direction.y * speed;
		if (currentTranslation != num)
		{
			currentTranslation = num;
			base.transform.RotateAround(point, new Vector3(0f, 1f, 0f), 5f * ((!(direction.y >= 0f)) ? (-1f) : 1f));
		}
	}
}
