using UnityEngine;
using UnityEngine.UI;

public class StationEngineConfiguration : MonoBehaviour
{
	public bool enableDebug;

	public float timeOut = 10f;

	public float timeMin = 1f;

	public string webPrivacyPolicy = "https://www.gstationstudio.com/privacypolicy";

	[Header("SOCIAL")]
	public string facebookPage = "https://web.facebook.com/station1.games/";

	public string webPage = "https://www.gstationstudio.com";

	public string googlePlayCompanyID = "6738272163199307082";

	[Header("GEO LOCATION")]
	public bool enableGeoLocation = true;

	public bool debugGeoLocation;

	[Header("LOCAL NOTIFICATIONS")]
	public bool enableLocalNotifications = true;

	public bool debugLocalNotifications;

	[Header("TIME RETRIEVER")]
	public bool enableTimeRetriever = true;

	public bool debugTimeRetriever;

	[Header("FIREBASE")]
	public bool enableFirebaseAnalytics = true;

	public bool enableFirebaseMessaging = true;

	public bool debugFirebase;

	[Header("SPLASH SCREEN")]
	public string nameFirstGameScreen = string.Empty;

	public Image imageLoadingSplashScreen;

	public Text textLoadingSplashScreen;

	public GameObject objectLoadingBar;
}
