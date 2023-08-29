using UnityEngine;

public class UIBankTutorialDone : MonoBehaviour
{
	private SfxUIController sfxUIController;

	public void Initialize(SfxUIController sfxUIController)
	{
		this.sfxUIController = sfxUIController;
		Touch_Battle.IsWindowBigOpen = true;
	}

	public void CloseWindow()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		PlayerPrefs.SetInt("bankTutorialDone", 1);
		PlayerPrefs.Save();
		Touch_Battle.IsWindowBigOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
