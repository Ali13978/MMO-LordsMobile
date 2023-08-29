using System.Collections.Generic;
using UnityEngine;

public class ProgressArrowAnimation : MonoBehaviour
{
	public bool right;

	public bool up;

	public bool down;

	private Animator anim;

	public static ProgressArrowAnimation instance;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		instance = this;
	}

	public void OnEnable()
	{
		PlayAnimation();
	}

	private void PlayAnimation()
	{
		if (right)
		{
			anim.Play("ProgressArrowRight", 0, 0f);
		}
		if (up)
		{
			anim.Play("ProgressArrowUp", 0, 0f);
		}
		if (down)
		{
			anim.Play("ProgressArrowDown", 0, 0f);
		}
	}

	public void RefeshAnimation()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("ProgressArrow");
		List<Animator> list = new List<Animator>();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add(array[i].GetComponent<Animator>());
			list[i].GetComponent<ProgressArrowAnimation>().PlayAnimation();
		}
	}
}
