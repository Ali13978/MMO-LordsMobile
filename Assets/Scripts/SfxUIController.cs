using UnityEngine;

public class SfxUIController : MonoBehaviour
{
	private AudioSource audioSource;

	public AudioClip sfxClickDefault;

	public AudioClip sfxClickBuy;

	public AudioClip sfxClickClose;

	public AudioClip sfxWinWave;

	public AudioClip sfxLoseWave;

	public AudioClip sfxWinInvasion;

	public AudioClip sfxLoseInvasion;

	public AudioClip sfxStartInvasion;

	public AudioClip sfxLevelUp;

	public AudioClip sfxCharacterUnlock;

	public AudioClip sfxWindowOpen;

	private void Awake()
	{
		audioSource = base.gameObject.GetComponent<AudioSource>();
	}

	public void PlaySound(SfxUI _sfxIndex)
	{
		if (PlayerPrefsController.isSfx)
		{
			switch (_sfxIndex)
			{
			case SfxUI.ClickDefault:
				audioSource.PlayOneShot(sfxClickDefault, 1f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.ClickBuy:
				audioSource.PlayOneShot(sfxClickBuy, 1f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.ClickClose:
				audioSource.PlayOneShot(sfxClickClose, 1f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.LevelUp:
				audioSource.PlayOneShot(sfxLevelUp, 1f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.LoseInvasion:
				audioSource.PlayOneShot(sfxLoseInvasion, 0.8f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.LoseWave:
				audioSource.PlayOneShot(sfxLoseWave, 0.8f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.StartInvasion:
				audioSource.PlayOneShot(sfxStartInvasion, 0.7f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.WinInvasion:
				audioSource.PlayOneShot(sfxWinInvasion, 0.5f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.WinWave:
				audioSource.PlayOneShot(sfxWinWave, 0.5f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.CharacterUnlock:
				audioSource.PlayOneShot(sfxCharacterUnlock, 1f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			case SfxUI.WindowOpen:
				audioSource.PlayOneShot(sfxWindowOpen, 0.75f * AudioPrefsController.volumeUI * AudioPrefsController.volumeMaster);
				break;
			}
		}
	}

	public void CheckScenarioSfx()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("ScenarioObject");
		if (!(gameObject != null))
		{
			return;
		}
		ScenariosSfx component = gameObject.GetComponent<ScenariosSfx>();
		if (component != null)
		{
			if (PlayerPrefsController.isSfx)
			{
				component.TurnOnSfx();
			}
			else
			{
				component.TurnOffSfx();
			}
		}
	}
}
