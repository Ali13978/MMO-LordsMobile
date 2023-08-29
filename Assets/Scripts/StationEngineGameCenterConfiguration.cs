using System.Collections.Generic;
using UnityEngine;

public class StationEngineGameCenterConfiguration : MonoBehaviour
{
	[Header("GAME CENTER")]
	public bool enableGameCenter = true;

	public bool debugGameCenter;

	public List<string> leaderboardID = new List<string>();

	public List<string> achievementsID = new List<string>();
}
