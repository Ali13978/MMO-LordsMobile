using I2.Loc;
using System;
using UnityEngine;

public class BankController : MonoBehaviour
{
	private const string keyTimeStamp = "bankCheckTimeStamp";

	private const string keyBankLevel = "bankCheckLevel";

	private const string keyMoneyPerMinute = "bankCheckColoniesIncome";

	private const string keyPendingReward = "bankCheckPendingReward";

	public static bool RewardCalculated;

	private bool isInitialized;

	private bool isChecked;

	private float timeToCheckStatusFlag = 10f;

	private float timeToCheckStatus;

	private StationEngine stationEngine;

	public bool IsChecked
	{
		get
		{
			return isChecked;
		}
		set
		{
			isChecked = value;
		}
	}

	public void Initialize(StationEngine stationEngine)
	{
		this.stationEngine = stationEngine;
		isInitialized = true;
		CheckStatus();
	}

	public int GetRewardAccumulated()
	{
		int num = -1;
		if (RewardCalculated)
		{
			num = PlayerPrefs.GetInt("bankCheckPendingReward");
			if (num < 1)
			{
				num = -1;
			}
		}
		return num;
	}

	public bool IsReady()
	{
		return RewardCalculated;
	}

	public void ForceStatus()
	{
		CheckStatus();
	}

	public int GetMaxReward(int bankLevel = -1)
	{
		if (bankLevel == -1)
		{
			bankLevel = PlayerPrefs.GetInt("bankCheckLevel");
		}
		return (int)((float)ConfigPrefsController.bankMinutesFull * GetRewardPerMinute(bankLevel));
	}

	public float GetRewardPerMinute(int bankLevel)
	{
		return (float)ConfigPrefsController.GetColoniesIncome() * ConfigPrefsController.bankMultiplier[bankLevel];
	}

	public void PayReward(bool giveExtra)
	{
		if (stationEngine.GetStatusTimeRetriever() == StationEngine.ComponentStatus.INITIALIZED && PlayerPrefs.GetInt("bankCheckPendingReward") > 0 && RewardCalculated)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Canvas/CanvasMoneyReward")) as GameObject;
			RewardAnimation component = gameObject.GetComponent<RewardAnimation>();
			if (giveExtra)
			{
				float num = (float)PlayerPrefs.GetInt("bankCheckPendingReward") * ConfigPrefsController.bankExtraMultiplier;
				PlayerPrefs.SetFloat("playerMoney", PlayerPrefs.GetFloat("playerMoney") + ((float)PlayerPrefs.GetInt("bankCheckPendingReward") + num));
				component.SetData(PlayerPrefs.GetInt("bankCheckPendingReward") + (int)num);
			}
			else
			{
				PlayerPrefs.SetFloat("playerMoney", PlayerPrefs.GetFloat("playerMoney") + (float)PlayerPrefs.GetInt("bankCheckPendingReward"));
				component.SetData(PlayerPrefs.GetInt("bankCheckPendingReward"));
			}
			PlayerPrefs.SetInt("bankCheckPendingReward", 0);
			PlayerPrefs.Save();
		}
		CheckStatus();
	}

	private void Update()
	{
		if (isInitialized)
		{
			timeToCheckStatus += Time.deltaTime;
			if (timeToCheckStatus >= timeToCheckStatusFlag)
			{
				CheckStatus();
			}
		}
	}

	private void SetNotification()
	{
		int id = 3;
		stationEngine.CancelNotification(id);
		if (PlayerPrefs.GetInt("bankNotification") == 1 && PlayerPrefsController.BankLvl >= 0)
		{
			stationEngine.SetRepeatNotification(ConfigPrefsController.bankMinutesFull * 60, ConfigPrefsController.bankMinutesFullRepeat * 60, ScriptLocalization.Get("BANK/notification_title"), ScriptLocalization.Get("BANK/notification_body"), id);
		}
	}

	private void CheckStatus()
	{
		timeToCheckStatus = 0f;
		if (!isInitialized)
		{
			return;
		}
		if (stationEngine.GetStatusTimeRetriever() == StationEngine.ComponentStatus.INITIALIZED)
		{
			if (RewardCalculated)
			{
				SaveBankStamp();
			}
			else
			{
				CheckReward();
			}
		}
		else
		{
			stationEngine.UpdateTime(overwrite: false);
		}
	}

	private void CheckReward()
	{
		if (stationEngine.GetStatusTimeRetriever() != StationEngine.ComponentStatus.INITIALIZED || RewardCalculated)
		{
			return;
		}
		if (PlayerPrefs.HasKey("bankCheckTimeStamp") && PlayerPrefs.GetInt("bankCheckLevel") >= 0)
		{
			DateTime value = stationEngine.ParseTimeStamp(double.Parse(PlayerPrefs.GetString("bankCheckTimeStamp")));
			float num = (float)stationEngine.ParseTimeStamp(stationEngine.GetTimeStamp()).Subtract(value).TotalMinutes;
			if (num > (float)ConfigPrefsController.bankMinutesFull)
			{
				num = ConfigPrefsController.bankMinutesFull;
			}
			int @int = PlayerPrefs.GetInt("bankCheckLevel");
			float @float = PlayerPrefs.GetFloat("bankCheckColoniesIncome");
			int num2 = (int)(@float * ConfigPrefsController.bankMultiplier[@int] * num);
			int int2 = PlayerPrefs.GetInt("bankCheckPendingReward");
			num2 += int2;
			int maxReward = GetMaxReward(@int);
			if (num2 > maxReward)
			{
				num2 = maxReward;
			}
			PlayerPrefs.SetInt("bankCheckPendingReward", num2);
			PlayerPrefs.Save();
		}
		RewardCalculated = true;
		CheckStatus();
	}

	private void SaveBankStamp()
	{
		if (stationEngine.GetStatusTimeRetriever() == StationEngine.ComponentStatus.INITIALIZED)
		{
			PlayerPrefs.SetString("bankCheckTimeStamp", stationEngine.GetTimeStamp().ToString());
			PlayerPrefs.SetInt("bankCheckLevel", PlayerPrefsController.BankLvl);
			PlayerPrefs.SetFloat("bankCheckColoniesIncome", ConfigPrefsController.GetColoniesIncome());
			PlayerPrefs.Save();
			SetNotification();
		}
	}
}
