using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class FreeItemController : MonoBehaviour
{
	private UIController uiController;

	public Text durationText;

	public void Initialize(UIController uiController)
	{
		this.uiController = uiController;
		durationText.text = "x" + ConfigPrefsController.freeMoneyBoostDuration.ToString() + " " + ScriptLocalization.Get("NORMAL/waves");
	}

	public void GetFreeItem()
	{
		PlayerPrefs.SetInt("moneyBoost", PlayerPrefs.GetInt("moneyBoost") + ConfigPrefsController.freeMoneyBoostDuration);
		PlayerPrefs.Save();
		uiController.UpdateUIUpgrade();
		uiController.UpdateBoostsText();
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
