using UnityEngine;

public class ScenariosSfx : MonoBehaviour
{
	public AudioSource[] audioSource;

	private void Start()
	{
	}

	public void TurnOffSfx()
	{
		for (int i = 0; i < audioSource.Length; i++)
		{
			if (audioSource[i].isPlaying)
			{
				audioSource[i].Stop();
			}
		}
	}

	public void TurnOnSfx()
	{
		for (int i = 0; i < audioSource.Length; i++)
		{
			if (!audioSource[i].isPlaying)
			{
				audioSource[i].Play();
			}
		}
	}
}
