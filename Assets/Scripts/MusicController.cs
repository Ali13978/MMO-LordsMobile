using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
	public AudioClip[] musicClip;

	public AudioClip[] wavesClips;

	private float fadeSpeed;

	private float volumeTarget;

	private int nextTrackIndex;

	private bool isChangingTrack;

	private AudioSource audioSource;

	private void Awake()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("MusicController");
		if (array.Length > 1)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		Object.DontDestroyOnLoad(base.gameObject);
		audioSource = base.gameObject.GetComponent<AudioSource>();
		audioSource.clip = musicClip[0];
		audioSource.volume = 0f;
		audioSource.Play();
		SetStatus(_forceVolume: false, _fadeOffChangingScreen: false);
	}

	private void Update()
	{
		if (PlayerPrefsController.isMusic)
		{
			if (isChangingTrack)
			{
				if (audioSource.volume > 0f)
				{
					float volume = audioSource.volume;
					volume -= Time.unscaledDeltaTime * fadeSpeed;
					if (volume < 0f)
					{
						volume = 0f;
					}
					audioSource.volume = volume;
				}
				if (audioSource.volume <= 0f)
				{
					if (audioSource.isPlaying)
					{
						audioSource.Stop();
					}
					if (MainController.worldScreen == WorldScreen.Defence)
					{
						audioSource.clip = wavesClips[nextTrackIndex];
					}
					else
					{
						audioSource.clip = musicClip[nextTrackIndex];
					}
					audioSource.Play();
					isChangingTrack = false;
					SetStatus(_forceVolume: false, _fadeOffChangingScreen: false);
				}
				return;
			}
			float num = audioSource.volume;
			if (num < volumeTarget)
			{
				num += Time.unscaledDeltaTime * fadeSpeed;
				if (num > volumeTarget)
				{
					num = volumeTarget;
				}
			}
			else if (num > volumeTarget)
			{
				num -= Time.unscaledDeltaTime * fadeSpeed;
				if (num < volumeTarget)
				{
					num = volumeTarget;
				}
			}
			audioSource.volume = num;
		}
		else
		{
			audioSource.volume = 0f;
		}
	}

	public void SetStatus(bool _forceVolume, bool _fadeOffChangingScreen)
	{
		if (_fadeOffChangingScreen)
		{
			fadeSpeed = AudioPrefsController.volumeMusicFadeOffScreenSpeed;
		}
		else if (SceneManager.GetActiveScene().name == "MapScene")
		{
			fadeSpeed = AudioPrefsController.volumeMusicFadeOnMapSpeed;
			volumeTarget = AudioPrefsController.volumeMusicMap * AudioPrefsController.volumeMusic * AudioPrefsController.volumeMaster;
		}
		else
		{
			switch (MainController.worldScreen)
			{
			case WorldScreen.Upgrade:
				fadeSpeed = AudioPrefsController.volumeMusicFadeOnSpeed;
				volumeTarget = AudioPrefsController.volumeMusicUpgrade * AudioPrefsController.volumeMusic * AudioPrefsController.volumeMaster;
				break;
			case WorldScreen.Defence:
				fadeSpeed = AudioPrefsController.volumeMusicFadeOffBattleSpeed;
				volumeTarget = AudioPrefsController.volumeMusicDefence[nextTrackIndex] * AudioPrefsController.volumeMusic * AudioPrefsController.volumeMaster;
				break;
			case WorldScreen.Attack:
				fadeSpeed = AudioPrefsController.volumeMusicFadeOnSpeed;
				volumeTarget = AudioPrefsController.volumeMusicAttack * AudioPrefsController.volumeMusic * AudioPrefsController.volumeMaster;
				break;
			case WorldScreen.AttackStarted:
				fadeSpeed = AudioPrefsController.volumeMusicFadeOffBattleSpeed;
				volumeTarget = AudioPrefsController.volumeMusicAttackStarted * AudioPrefsController.volumeMusic * AudioPrefsController.volumeMaster;
				break;
			}
		}
		if (!PlayerPrefsController.isMusic)
		{
			volumeTarget = 0f;
		}
		else if (_forceVolume)
		{
			audioSource.volume = volumeTarget;
		}
	}

	public void ChangeTrack(int _trackIndex, bool _fadeOffChangingScreen)
	{
		SetStatus(_forceVolume: false, _fadeOffChangingScreen);
		isChangingTrack = true;
		nextTrackIndex = _trackIndex;
	}

	public int SetWaveMusic()
	{
		return UnityEngine.Random.Range(0, wavesClips.Length);
	}
}
