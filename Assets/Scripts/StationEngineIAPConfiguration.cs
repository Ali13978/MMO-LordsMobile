using UnityEngine;

public class StationEngineIAPConfiguration : MonoBehaviour
{
	[Header("IAPs")]
	public bool enableIAP = true;

	public bool debugIAP;

	public string pubKeyGooglePlay = string.Empty;

	public string[] nameIAP;

	public string[] skuIAP;
}
