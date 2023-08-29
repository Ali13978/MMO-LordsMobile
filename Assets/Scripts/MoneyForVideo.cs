using UnityEngine;
using UnityEngine.UI;

public class MoneyForVideo : MonoBehaviour
{
	public Text textMoneyReward;

	public RectTransform mainTransform;

	private bool waitingVideoReward;

	private Vector2 positionShown = new Vector2(0f, 25f);

	private Vector2 positionHidden = new Vector2(0f, -150f);

	private StationEngine stationEngine;

	private UIController uiController;

	private void Awake()
	{
		stationEngine = GameObject.Find("StationEngine").GetComponent<StationEngine>();
		uiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
		mainTransform.anchoredPosition = positionHidden;
		SetData();
	}

	private void Update()
	{
		if (!waitingVideoReward || stationEngine.GetVideoRewardStatus() == StationEngineAds.VideoRewardStatus.PLAYING)
		{
			return;
		}
		waitingVideoReward = false;
		if (stationEngine.GetVideoRewardStatus() == StationEngineAds.VideoRewardStatus.COMPLETED)
		{
			stationEngine.SendAnalyticCustom("Video_Rewarded", "Coins", "1");
			int num = (int)(ConfigPrefsController.adsVideoMoneyPerWave * (float)PlayerPrefs.GetInt("playerWave"));
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
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void SetData()
	{
		int num = (int)(ConfigPrefsController.adsVideoMoneyPerWave * (float)PlayerPrefs.GetInt("playerWave"));
		textMoneyReward.text = num.ToString("###,###,###");
		LeanTween.move(mainTransform, positionShown, 1.5f).setEase(LeanTweenType.linear).setIgnoreTimeScale(useUnScaledTime: true)
			.setDelay(3.5f);
	}

	public void ButtonPressPlayVideo()
	{
		if (stationEngine.CheckVideoReward())
		{
			stationEngine.SendAnalyticCustom("Behaviour_Videos", "Played", "Wave_" + PlayerPrefs.GetInt("playerWave", 0));
			waitingVideoReward = true;
			stationEngine.ShowVideoReward(StationEngineFirebase.AnalyticsAdsPosition.Extra_Coins);
		}
	}

	public void HideButton()
	{
		LeanTween.cancel(mainTransform.gameObject);
		LeanTween.move(mainTransform, positionHidden, 0.5f).setEase(LeanTweenType.linear).setIgnoreTimeScale(useUnScaledTime: true);
	}

	public void UnhideButton()
	{
		LeanTween.cancel(mainTransform.gameObject);
		LeanTween.move(mainTransform, positionShown, 1.5f).setEase(LeanTweenType.linear).setIgnoreTimeScale(useUnScaledTime: true);
	}
}
