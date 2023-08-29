using UnityEngine;

public class StationEngineSocial : MonoBehaviour
{
	private StationEngine stationEngine;

	private StationEngineConfiguration stationEngineConfiguration;

	public void Initialize(StationEngine stationEngine, StationEngineConfiguration stationEngineConfiguration)
	{
		this.stationEngine = stationEngine;
		this.stationEngineConfiguration = stationEngineConfiguration;
	}

	public void ShareAllMedia(string textTitle, string textBody)
	{
		stationEngine.SendAnalyticBehaviour(StationEngineFirebase.AnalyticsBehaviour.Share_All_Media);
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent");
		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[1]
		{
			androidJavaClass.GetStatic<string>("ACTION_SEND")
		});
		androidJavaObject.Call<AndroidJavaObject>("setType", new object[1]
		{
			"text/plain"
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
			textTitle
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_TITLE"),
			textTitle
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
			textBody
		});
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
		@static.Call("startActivity", androidJavaObject);
	}

	public void RateApp()
	{
		stationEngine.SendAnalyticBehaviour(StationEngineFirebase.AnalyticsBehaviour.Rate_App);
		//string url = "market://details?id=" + Application.bundleIdentifier;
		//Application.OpenURL(url);
	}

	public void MoreGames()
	{
		stationEngine.SendAnalyticBehaviour(StationEngineFirebase.AnalyticsBehaviour.More_Games);
		string url = "http://play.google.com/store/apps/dev?id=" + stationEngineConfiguration.googlePlayCompanyID;
		Application.OpenURL(url);
	}

	public void FacebookPage()
	{
		stationEngine.SendAnalyticBehaviour(StationEngineFirebase.AnalyticsBehaviour.Facebook_Page);
		string facebookPage = stationEngineConfiguration.facebookPage;
		Application.OpenURL(facebookPage);
	}

	public void WebPage()
	{
		stationEngine.SendAnalyticBehaviour(StationEngineFirebase.AnalyticsBehaviour.WebPage);
		string webPage = stationEngineConfiguration.webPage;
		Application.OpenURL(webPage);
	}
}
