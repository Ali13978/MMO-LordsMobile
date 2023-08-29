using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FPS : MonoBehaviour
{
	public float updateInterval;

	private float accum;

	private int frames;

	private float timeleft;

	public FPS()
	{
		updateInterval = 0.5f;
	}

	public void Start()
	{
		if (!GetComponent<Text>())
		{
			MonoBehaviour.print("FramesPerSecond needs a GUIText component!");
			enabled = false;
		}
		else
		{
			timeleft = updateInterval;
		}
	}

	public void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		if (!(timeleft > 0f))
		{
			GetComponent<Text>().text = "FPS - " + (accum / (float)frames).ToString("f2");
			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
	}

	public void Main()
	{
	}
}
