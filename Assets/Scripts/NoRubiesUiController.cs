using UnityEngine;
using UnityEngine.UI;

public class NoRubiesUiController : MonoBehaviour
{
	public Text packAmountText;

	public Text packCostText;

	private StationEngine stationEngine;

	private SfxUIController sfxUiController;

	private UIController uiController;

	public void Initialize(StationEngine stationEngine, SfxUIController sfxUiController, UIController uiController)
	{
		this.stationEngine = stationEngine;
		this.sfxUiController = sfxUiController;
		this.uiController = uiController;
		stationEngine.SetPrices();
		Touch_Battle.IsWindowSmallOpen = true;
		UpdateWindow();
	}

	public void CloseWindow()
	{
		sfxUiController.PlaySound(SfxUI.ClickClose);
		Touch_Battle.IsWindowSmallOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void BuyRubiesPack()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		stationEngine.PurchaseIAP(0);
	}

	private void UpdateWindow()
	{
		packAmountText.text = "x" + ConfigPrefsController.storeRubyPackAmount[0].ToString();
		packCostText.text = stationEngine.GetIAPPrice(0);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			CloseWindow();
		}
	}
}
