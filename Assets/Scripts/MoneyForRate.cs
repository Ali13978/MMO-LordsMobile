using UnityEngine;
using UnityEngine.UI;

public class MoneyForRate : MonoBehaviour
{
	public Text textMoneyReward;

	public RectTransform mainTransform;

	private Vector2 positionShown = new Vector2(114f, 0f);

	private Vector2 positionHidden = new Vector2(-200f, 0f);

	private StationEngine stationEngine;

	private UIController uiController;

	private void Awake()
	{
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		uiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
		mainTransform.anchoredPosition = positionHidden;
	}

	public void SetData()
	{
		int num = (int)(ConfigPrefsController.rateMoneyPerWave * (float)PlayerPrefs.GetInt("playerWave"));
		textMoneyReward.text = num.ToString("###,###,###");
		LeanTween.move(mainTransform, positionShown, 1.5f).setEase(LeanTweenType.easeOutBounce).setIgnoreTimeScale(useUnScaledTime: true);
	}

	public void ButtonPressRateGame()
	{
		PlayerPrefs.SetInt("ratedGame", 1);
		PlayerPrefs.Save();
		stationEngine.RateApp();
		LeanTween.delayedCall(0.5f, GiveReward).setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void GiveReward()
	{
		int num = (int)(ConfigPrefsController.rateMoneyPerWave * (float)PlayerPrefs.GetInt("playerWave"));
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") + (float)num);
		if (MainController.worldScreen == WorldScreen.Upgrade)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("SubObjectUpgradeMenu");
			if (gameObject.activeInHierarchy)
			{
				uiController.UpdateUIUpgrade();
				uiController.UpdateExclamationUnits();
			}
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("Canvas/CanvasMoneyReward")) as GameObject;
		RewardAnimation component = gameObject2.GetComponent<RewardAnimation>();
		component.SetData(num);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void HideButton()
	{
		LeanTween.cancel(mainTransform.gameObject);
		LeanTween.move(mainTransform, positionHidden, 0.5f).setEase(LeanTweenType.easeOutBounce).setIgnoreTimeScale(useUnScaledTime: true);
	}

	public void UnhideButton()
	{
		LeanTween.cancel(mainTransform.gameObject);
		LeanTween.move(mainTransform, positionShown, 1.5f).setEase(LeanTweenType.easeOutBounce).setIgnoreTimeScale(useUnScaledTime: true);
	}
}
