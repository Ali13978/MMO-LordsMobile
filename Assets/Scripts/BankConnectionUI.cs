using UnityEngine;
using UnityEngine.UI;

public class BankConnectionUI : MonoBehaviour
{
	private BankController bankController;

	private SfxUIController sfxUiController;

	private UIController uiController;

	public Text connectionText;

	public void Initialize(BankController bankController, SfxUIController sfxUiController, UIController uiController)
	{
		this.uiController = uiController;
		this.sfxUiController = sfxUiController;
		this.bankController = bankController;
		Touch_Battle.IsWindowSmallOpen = true;
		bankController.ForceStatus();
		connectionText.text.ToUpper();
		uiController.canvasUpgrade.SetActive(value: false);
	}

	public void CloseWindow()
	{
		Touch_Battle.IsWindowSmallOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
		uiController.BackFromUpgradeWindow();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKey(KeyCode.Escape))
		{
			CloseWindow();
		}
	}
}
