using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class StationEngineGameCenter : MonoBehaviour
{
	private static ILeaderboard m_Leaderboard;

	private StationEngine stationEngine;

	private StationEngineGameCenterConfiguration stationEngineGameCenterConfig;

	private StationEngine.ComponentStatus actualStatus;

	public void Initialize(StationEngine stationEngine, StationEngineGameCenterConfiguration stationEngineGameCenterConfig)
	{
		this.stationEngine = stationEngine;
		this.stationEngineGameCenterConfig = stationEngineGameCenterConfig;
		actualStatus = StationEngine.ComponentStatus.INITIALIZING;
		AuthenticateUser();
	}

	public StationEngine.ComponentStatus GetStatus()
	{
		return actualStatus;
	}

	public void SetStatusTimeOut()
	{
		actualStatus = StationEngine.ComponentStatus.TIME_OUT;
	}

	public void ReportAchievement(string achievementId, double progress)
	{
		if (IsAuthenticated())
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(value: true);
			Social.ReportProgress(achievementId, progress, delegate(bool result)
			{
				MonoBehaviour.print((!result) ? $"FAILED TO REPORT ACHIEVEMENT {achievementId}" : $"SUCCESFULLY REPORTED ACHIEVEMENT {achievementId}");
			});
		}
		else
		{
			AuthenticateUser();
		}
	}

	public void ShowAchievements()
	{
		if (IsAuthenticated())
		{
			Social.ShowAchievementsUI();
		}
		else
		{
			AuthenticateUser();
		}
	}

	private void ProcessLoadedAchievements(IAchievement[] achievements)
	{
		if (achievements.Length == 0)
		{
			MonoBehaviour.print("NO ACHIEVEMENTS FOUND");
		}
		else
		{
			MonoBehaviour.print("NUMBER OF ACHIEVEMENTS: " + achievements.Length);
		}
	}

	public void ReportScore(long score, int _leaderIndex)
	{
		if (IsAuthenticated())
		{
			Social.ReportScore(score, stationEngineGameCenterConfig.leaderboardID[_leaderIndex], delegate
			{
			});
		}
		else
		{
			AuthenticateUser();
		}
	}

	public void LoadLeaderboard(int _leaderIndex)
	{
		if (IsAuthenticated())
		{
			m_Leaderboard = Social.CreateLeaderboard();
			m_Leaderboard.id = stationEngineGameCenterConfig.leaderboardID[_leaderIndex];
			m_Leaderboard.LoadScores(delegate(bool result)
			{
				ProcessLeaderboard(result);
			});
		}
		else
		{
			AuthenticateUser();
		}
	}

	private void ProcessLeaderboard(bool result)
	{
		if (result)
		{
			IScore[] scores = m_Leaderboard.scores;
			foreach (IScore message in scores)
			{
				MonoBehaviour.print(message);
			}
		}
		Social.ShowLeaderboardUI();
	}

	public bool IsAuthenticated()
	{
		return Social.localUser.authenticated;
	}

	public void AuthenticateUser()
	{
		if (!IsAuthenticated())
		{
			Social.localUser.Authenticate(ProcessAuthentication);
		}
	}

	private void ProcessAuthentication(bool success)
	{
		if (success)
		{
			MonoBehaviour.print("HAS LOGGED IN TO GAMECENTER!");
			actualStatus = StationEngine.ComponentStatus.INITIALIZED;
			Social.LoadAchievements(ProcessLoadedAchievements);
		}
		else
		{
			MonoBehaviour.print("FAILED TO AUTHENTICATE");
			actualStatus = StationEngine.ComponentStatus.ERROR;
		}
	}
}
