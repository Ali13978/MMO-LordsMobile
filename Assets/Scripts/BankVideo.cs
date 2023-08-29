using UnityEngine;
using UnityEngine.UI;

public class BankVideo : MonoBehaviour
{
	public Text textReward;

	public Text textPlay;

	private StationEngine stationEngine;

	private SfxUIController sfxUIController;

	private BankController bankController;

	private UIBankIcon uiBankIcon;

	private bool waitingVideoReward;

	private float rewardAmountPercentage;

	public void Initialize(StationEngine stationEngine, BankController bankController, SfxUIController sfxUIController, UIBankIcon uiBankIcon)
	{
		this.stationEngine = stationEngine;
		this.bankController = bankController;
		this.sfxUIController = sfxUIController;
		this.uiBankIcon = uiBankIcon;
		rewardAmountPercentage = ConfigPrefsController.bankExtraMultiplier;
		Touch_Battle.IsWindowBigOpen = true;
		UpdateWindow();
	}

	public void ButtonVideo()
	{
		if (stationEngine.CheckVideoReward())
		{
			stationEngine.SendAnalyticCustom("Behaviour_Videos_Bank", "Played", "Wave_" + PlayerPrefs.GetInt("playerWave", 0));
			waitingVideoReward = true;
			stationEngine.ShowVideoReward(StationEngineFirebase.AnalyticsAdsPosition.Extra_Coins);
		}
	}

	public void ButtonClose()
	{
		GiveReward(giveExtra: false);
		CloseWindow();
	}

	private void CloseWindow()
	{
		Touch_Battle.IsWindowBigOpen = false;
		stationEngine.SendAnalyticCustom("Behaviour_Videos_Bank", "Skipped", "Wave_" + PlayerPrefs.GetInt("playerWave", 0));
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void GiveReward(bool giveExtra)
	{
		sfxUIController.PlaySound(SfxUI.ClickBuy);
		uiBankIcon.AnimationPayReward();
		bankController.PayReward(giveExtra);
	}

	private void UpdateWindow()
	{
		textReward.text = "+" + (rewardAmountPercentage * 100f).ToString("###,###.##") + "%";
		textPlay.text.ToUpper();
	}

	private void Update()
	{
		if (waitingVideoReward)
		{
			if (stationEngine.GetVideoRewardStatus() != StationEngineAds.VideoRewardStatus.PLAYING)
			{
				waitingVideoReward = false;
				if (stationEngine.GetVideoRewardStatus() == StationEngineAds.VideoRewardStatus.COMPLETED)
				{
					GiveReward(giveExtra: true);
					stationEngine.SendAnalyticCustom("Video_Rewarded", "Bank", "1");
				}
				CloseWindow();
			}
		}
		else if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			GiveReward(giveExtra: false);
			CloseWindow();
		}
	}
}
