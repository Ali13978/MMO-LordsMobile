using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Text;
using UnityEngine;

public class StationEngineGPG : MonoBehaviour
{
	private StationEngine stationEngine;

	private StationEngineGPGConfiguration stationEngineGPGConfiguration;

	private int[] achivementsProgressPending = new int[1];

	private CloudStatus statusCloud;

	private string savedGameCloudString;

	private bool isDebugEnabled;

	private StationEngine.ComponentStatus actualStatus;

	public static bool cloudLegacyTry;

	public CloudStatus StatusCloud => statusCloud;

	public string SavedGameCloudString
	{
		get
		{
			return savedGameCloudString;
		}
		set
		{
			savedGameCloudString = value;
		}
	}

	public void Initialize(StationEngine stationEngine, StationEngineGPGConfiguration stationEngineGPGConfiguration)
	{
		actualStatus = StationEngine.ComponentStatus.INITIALIZING;
		this.stationEngine = stationEngine;
		this.stationEngineGPGConfiguration = stationEngineGPGConfiguration;
		isDebugEnabled = stationEngineGPGConfiguration.enableGooglePlayGames;
		achivementsProgressPending = new int[this.stationEngineGPGConfiguration.achievementsID.Count];
		for (int i = 0; i < achivementsProgressPending.Length; i++)
		{
			achivementsProgressPending[i] = PlayerPrefs.GetInt("StationEngine_achievProg_" + i.ToString(), 0);
		}
		if (this.stationEngineGPGConfiguration.enableCloudSaving)
		{
			PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
			PlayGamesPlatform.InitializeInstance(configuration);
		}
		PlayGamesPlatform.DebugLogEnabled = isDebugEnabled;
		PlayGamesPlatform.Activate();
		AuthenticateUser(this.stationEngineGPGConfiguration.googlePlayGamesSilentLogIn);
		actualStatus = StationEngine.ComponentStatus.INITIALIZED;
	}

	public StationEngine.ComponentStatus GetStatus()
	{
		return actualStatus;
	}

	public void SetStatusTimeOut()
	{
		actualStatus = StationEngine.ComponentStatus.TIME_OUT;
	}

	public void LogIn(bool _isSilent)
	{
		if (!IsLogged())
		{
			AuthenticateUser(_isSilent);
		}
	}

	public void LogOff()
	{
		if (IsLogged())
		{
			UnauthenticateUser();
		}
	}

	public void LogInOff(bool _isSilent)
	{
		if (IsLogged())
		{
			UnauthenticateUser();
		}
		else
		{
			AuthenticateUser(_isSilent);
		}
	}

	public bool IsLogged()
	{
		return PlayGamesPlatform.Instance.localUser.authenticated;
	}

	public void SubmitToLeaderboard(long _scoreToAdd, int _leaderIndex)
	{
		if (IsLogged())
		{
			Social.ReportScore(_scoreToAdd, stationEngineGPGConfiguration.leaderboardID[_leaderIndex], delegate
			{
			});
		}
	}

	public void ShowLeaderboard(int _leaderIndex, bool _logAnyway)
	{
		if (IsLogged())
		{
			PlayGamesPlatform.Instance.ShowLeaderboardUI(stationEngineGPGConfiguration.leaderboardID[_leaderIndex]);
		}
		else if (_logAnyway)
		{
			LogIn(_isSilent: false);
		}
	}

	public void ShowAchievement(bool _logAnyWay)
	{
		if (IsLogged())
		{
			PlayGamesPlatform.Instance.ShowAchievementsUI();
		}
		else if (_logAnyWay)
		{
			LogIn(_isSilent: false);
		}
	}

	public void UnlockAchievement(int _achievementIndex)
	{
		if (IsLogged())
		{
			Social.ReportProgress(stationEngineGPGConfiguration.achievementsID[_achievementIndex], 100.0, delegate
			{
			});
		}
	}

	public void IncrementAchievement(int _achievementIndex, int _amount)
	{
		bool wasSuccess = false;
		int num = _amount + achivementsProgressPending[_achievementIndex];
		if (num > 0)
		{
			PlayGamesPlatform.Instance.IncrementAchievement(stationEngineGPGConfiguration.achievementsID[_achievementIndex], num, delegate(bool success)
			{
				wasSuccess = success;
			});
			if (!wasSuccess)
			{
				achivementsProgressPending[_achievementIndex] = num;
			}
			else
			{
				achivementsProgressPending[_achievementIndex] = 0;
			}
			PlayerPrefs.SetInt("StationEngine_achievProg_" + _achievementIndex.ToString(), achivementsProgressPending[_achievementIndex]);
		}
	}

	public void SaveCloudGame(string _savegameData)
	{
		//string filename = Application.bundleIdentifier + "_nv";
		//statusCloud = CloudStatus.Saving;
		//savedGameCloudString = _savegameData;
		//((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(filename, DataSource.ReadNetworkOnly, ConflictResolutionStrategy.UseUnmerged, SaveGameOpened);
	}

	private void SaveGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(savedGameCloudString);
			SavedGameMetadataUpdate updateForMetadata = default(SavedGameMetadataUpdate.Builder).WithUpdatedDescription("Saved game at " + DateTime.Now).Build();
			((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, updateForMetadata, bytes, SavedGameWritten);
		}
		else
		{
			statusCloud = CloudStatus.Error;
			stationEngine.PostDebugError("Error opening game: " + status);
		}
	}

	private void SavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			statusCloud = CloudStatus.Success;
			stationEngine.PostDebugInfo("Game " + game.Description + " written");
		}
		else
		{
			statusCloud = CloudStatus.Error;
			stationEngine.PostDebugError("Error saving game: " + status);
		}
	}

	public void LoadCloudGame()
	{
		//string filename = Application.bundleIdentifier + "_nv";
		//if (cloudLegacyTry)
		//{
		//	filename = Application.bundleIdentifier;
		//}
		//statusCloud = CloudStatus.Loading;
		//((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(filename, DataSource.ReadNetworkOnly, ConflictResolutionStrategy.UseUnmerged, LoadGameOpened);
	}

	private void LoadGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, SavedGameLoaded);
			return;
		}
		statusCloud = CloudStatus.Error;
		stationEngine.PostDebugError("Error opening game: " + status);
	}

	private void SavedGameLoaded(SavedGameRequestStatus status, byte[] data)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			stationEngine.PostDebugInfo("SaveGameLoaded, success=" + status);
			if (data == null)
			{
				if (cloudLegacyTry)
				{
					cloudLegacyTry = false;
					statusCloud = CloudStatus.NoSavedGame;
					stationEngine.PostDebugInfo("No data saved to the cloud yet...");
				}
				else
				{
					cloudLegacyTry = true;
					stationEngine.PostDebugInfo("Trying legacy saved gamed...");
					LoadCloudGame();
				}
				return;
			}
			savedGameCloudString = Encoding.UTF8.GetString(data);
			stationEngine.PostDebugInfo("Decoding cloud data from bytes.");
			stationEngine.PostDebugInfo("DATA RETRIEVED: " + savedGameCloudString);
			if (savedGameCloudString == string.Empty || savedGameCloudString == " ")
			{
				if (cloudLegacyTry)
				{
					cloudLegacyTry = false;
					statusCloud = CloudStatus.NoSavedGame;
					stationEngine.PostDebugInfo("No data saved to the cloud yet...");
				}
				else
				{
					cloudLegacyTry = true;
					stationEngine.PostDebugInfo("Trying legacy saved gamed...");
					LoadCloudGame();
				}
			}
			else
			{
				statusCloud = CloudStatus.Success;
			}
		}
		else
		{
			statusCloud = CloudStatus.Error;
			stationEngine.PostDebugError("Error reading game: " + status);
		}
	}

	private void AuthenticateUser(bool _isSilent)
	{
		PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication, _isSilent);
	}

	private void UnauthenticateUser()
	{
		PlayGamesPlatform.Instance.SignOut();
	}

	private void ProcessAuthentication(bool success)
	{
		if (success)
		{
			stationEngine.PostDebugInfo("SUCCESS AUTHENTICATION");
		}
		else
		{
			stationEngine.PostDebugError("FAILED AUTHENTICATION");
		}
	}
}
