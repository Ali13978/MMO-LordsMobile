using UnityEngine;

public class BouldersShower : MonoBehaviour
{
	public float timeToDestroy;

	public Rigidbody[] bouldersRigidbodyArray;

	private bool[] isStucked;

	private bool allStucked;

	public AudioClip sfxStart;

	public AudioClip sfxHit;

	private bool playedSfxHit;

	private AudioSource audioSource;

	public bool[] IsStucked
	{
		get
		{
			return isStucked;
		}
		set
		{
			isStucked = value;
		}
	}

	private void Awake()
	{
		audioSource = base.gameObject.GetComponent<AudioSource>();
		isStucked = new bool[bouldersRigidbodyArray.Length];
	}

	private void Start()
	{
		for (int i = 0; i < bouldersRigidbodyArray.Length; i++)
		{
			if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
			{
				bouldersRigidbodyArray[i].AddForce(new Vector3(UnityEngine.Random.Range(-100f, -300f), UnityEngine.Random.Range(-800f, -1100f), 0f));
			}
			else if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
			{
				bouldersRigidbodyArray[i].AddForce(new Vector3(UnityEngine.Random.Range(100f, 300f), UnityEngine.Random.Range(-800f, -1100f), 0f));
			}
		}
		if (PlayerPrefsController.isSfx)
		{
			audioSource.PlayOneShot(sfxStart, AudioPrefsController.volumeBattlePowerBoulderStart * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}

	private void Update()
	{
		if (!allStucked)
		{
			allStucked = true;
			int num = 0;
			while (true)
			{
				if (num < isStucked.Length)
				{
					if (!isStucked[num])
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			allStucked = false;
		}
		else
		{
			timeToDestroy -= Time.deltaTime;
			if (timeToDestroy <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	public void PlaySfxHit()
	{
		if (PlayerPrefsController.isSfx && !playedSfxHit)
		{
			playedSfxHit = true;
			audioSource.PlayOneShot(sfxHit, AudioPrefsController.volumeBattlePowerBoulderHit * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
	}
}
