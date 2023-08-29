using System.Collections.Generic;
using UnityEngine;

public class StationEngineGPGConfiguration : MonoBehaviour
{
	[Header("GOOGLE PLAY GAMES")]
	public bool enableGooglePlayGames = true;

	public bool debugGooglePlayGames;

	public bool googlePlayGamesSilentLogIn = true;

	public List<string> leaderboardID = new List<string>();

	public List<string> achievementsID = new List<string>();

	public bool enableCloudSaving = true;
}
