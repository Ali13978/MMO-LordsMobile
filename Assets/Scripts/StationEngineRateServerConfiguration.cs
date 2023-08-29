using UnityEngine;

public class StationEngineRateServerConfiguration : MonoBehaviour
{
	[Header("RATE SERVER")]
	public bool enableRateServerRetriever = true;

	public bool debugRateServerRetriever;

	public string rateServer = "http://www.streamliveon.com/manuapps/ow.aspx?uid=36";
}
