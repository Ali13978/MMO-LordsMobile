using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler, IPointerUpHandler
{
	public float smoothing;

	private Vector2 origin;

	private Vector2 direction;

	private Vector2 smoothDirection;

	private bool touched;

	private int pointerId;

	private void Awake()
	{
		direction = Vector2.zero;
		touched = false;
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (!touched)
		{
			touched = true;
			pointerId = data.pointerId;
			origin = data.position;
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		if (pointerId == data.pointerId)
		{
			direction = Vector2.zero;
			touched = false;
		}
	}

	public void OnDrag(PointerEventData data)
	{
		if (pointerId == data.pointerId)
		{
			Vector2 position = data.position;
			direction = (position - origin).normalized;
			UnityEngine.Debug.Log(direction);
		}
	}

	public Vector2 GetDirection()
	{
		smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}
