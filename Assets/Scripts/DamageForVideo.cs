using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class DamageForVideo : MonoBehaviour
{
	public RectTransform mainTransform;

	public Text textDamage;

	public Text textPlay;

	public Image imageBackground;

	private Color colorBackgroundFinal;

	private Vector2 positionFinal;

	private float damageBonus;

	private bool waitingVideoReward;

	private StationEngine stationEngine;

	private UIController uiController;

	public void Initialize(StationEngine stationEngine, UIController uiController)
	{
		this.stationEngine = stationEngine;
		this.uiController = uiController;
		Time.timeScale = 0f;
		damageBonus = ConfigPrefsController.adsVideoDamageBase + (float)ConfigPrefsController.adsVideoDamageCounterSeen * ConfigPrefsController.adsVideoDamagePerTry;
		if (damageBonus >= ConfigPrefsController.adsVideoDamageMax)
		{
			damageBonus = ConfigPrefsController.adsVideoDamageMax;
		}
		textDamage.text = "+" + damageBonus.ToString() + "% " + ScriptLocalization.Get("NORMAL/damage").ToUpper();
		textPlay.text.ToUpper();
		AnimateMe();
	}

	public void ButtonPressClose()
	{
		ConfigPrefsController.adsVideoDamageCounter -= 2;
		stationEngine.SendAnalyticCustom("Behaviour_VideosDamage", "Skipped", "Wave_" + PlayerPrefs.GetInt("playerWave", 0));
		DestroyMe();
	}

	public void ButtonPressPlayVideo()
	{
		if (!waitingVideoReward && stationEngine.CheckVideoReward())
		{
			ConfigPrefsController.adsVideoDamageCounter--;
			ConfigPrefsController.adsVideoDamageCounterSeen++;
			stationEngine.SendAnalyticCustom("Behaviour_VideosDamage", "Played", "Wave_" + PlayerPrefs.GetInt("playerWave", 0));
			waitingVideoReward = true;
			stationEngine.ShowVideoReward(StationEngineFirebase.AnalyticsAdsPosition.Extra_Damage);
		}
	}

	private void Awake()
	{
		colorBackgroundFinal = imageBackground.color;
		positionFinal = mainTransform.anchoredPosition;
		imageBackground.color = new Color(0f, 0f, 0f, 0f);
		mainTransform.anchoredPosition = new Vector2(0f, -500f);
	}

	private void Update()
	{
		if (waitingVideoReward && stationEngine.GetVideoRewardStatus() != StationEngineAds.VideoRewardStatus.PLAYING)
		{
			waitingVideoReward = false;
			if (stationEngine.GetVideoRewardStatus() == StationEngineAds.VideoRewardStatus.COMPLETED)
			{
				RewardPlayer();
				stationEngine.SendAnalyticCustom("Video_Rewarded", "Damage", "1");
			}
			else
			{
				DestroyMe();
			}
		}
	}

	private void AnimateMe()
	{
		LeanTween.color(imageBackground.rectTransform, colorBackgroundFinal, 0.5f).setIgnoreTimeScale(useUnScaledTime: true);
		LeanTween.move(mainTransform, positionFinal, 1f).setEase(LeanTweenType.easeOutBounce).setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void RewardPlayer()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Canvas/CanvasDamageReward")) as GameObject;
		RewardAnimation component = gameObject.GetComponent<RewardAnimation>();
		string text = "+" + damageBonus.ToString() + "%";
		component.SetData(text);
		uiController.playerExtraDamage.SetActive(value: true);
		uiController.textExtraDamage.text = text;
		UpgradesController.extraDamageAmount = damageBonus / 100f;
		UpgradesController.isExtraDamageActive = true;
		DestroyMe();
	}

	private void DestroyMe()
	{
		if (PlayerPrefsController.isFastForward)
		{
			Time.timeScale = ConfigPrefsController.speedFast;
		}
		else
		{
			Time.timeScale = ConfigPrefsController.speedNormal;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
