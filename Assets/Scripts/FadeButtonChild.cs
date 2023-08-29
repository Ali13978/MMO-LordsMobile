using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FadeButtonChild : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerExitHandler
{
	public Text child;

	public Image childImage;

	private bool pressed;

	private Button myButton;

	private Color off = new Color32(156, 156, 156, byte.MaxValue);

	private void Awake()
	{
		myButton = GetComponent<Button>();
	}

	private void Update()
	{
		if (!myButton.interactable || pressed)
		{
			SetFade();
		}
		else
		{
			SetNormal();
		}
	}

	private void SetNormal()
	{
		if (child != null)
		{
			child.color = Color.white;
		}
		if (childImage != null)
		{
			childImage.color = Color.white;
		}
	}

	private void SetFade()
	{
		if (child != null)
		{
			child.color = off;
		}
		if (childImage != null)
		{
			childImage.color = off;
		}
	}

	public void OnPointerDown(PointerEventData e)
	{
		pressed = true;
	}

	public void OnPointerUp(PointerEventData e)
	{
		pressed = false;
	}

	public void OnPointerExit(PointerEventData e)
	{
		pressed = false;
	}
}
