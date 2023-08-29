using UnityEngine;
using UnityEngine.UI;

public class UIFreeGems : MonoBehaviour
{
	private UIController uiController;

	private SfxUIController sfxUiController;

	private TutorialController tutorialController;

	public Text gemAmountText;

	public void Initialize(UIController uiController, SfxUIController sfxUiController, TutorialController tutorialController)
	{
		this.uiController = uiController;
		this.sfxUiController = sfxUiController;
		this.tutorialController = tutorialController;
		Touch_Battle.IsWindowSmallOpen = true;
		gemAmountText.text = "X" + ConfigPrefsController.freeGems.ToString();
	}

	public void CloseWindow()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		PlayerPrefs.SetInt("playerRubies", PlayerPrefs.GetInt("playerRubies") + ConfigPrefsController.freeGems);
		PlayerPrefs.SetInt("gotFreeGems", 1);
		uiController.UpdateUIUpgrade();
		PlayerPrefs.Save();
		Touch_Battle.IsWindowSmallOpen = false;
		tutorialController.SetGlow(14);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
