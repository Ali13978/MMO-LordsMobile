using System;
using System.Collections.Generic;
using System.Text;

public class Vungle
{
	private const string PLUGIN_VERSION = "4.1.1";

	private const string IOS_SDK_VERSION = "5.1.0";

	private const string WIN_SDK_VERSION = "1.3.16";

	private const string ANDROID_SDK_VERSION = "5.1.0";

	public static string VersionInfo
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder("unity-");
			return stringBuilder.Append("4.1.1").Append("/android-").Append("5.1.0")
				.ToString();
		}
	}

	public static event Action<string> onAdStartedEvent;

	public static event Action<string, AdFinishedEventArgs> onAdFinishedEvent;

	public static event Action<string, bool> adPlayableEvent;

	public static event Action onInitializeEvent;

	public static event Action<string> onLogEvent;

	static Vungle()
	{
		VungleManager.OnAdStartEvent += adStarted;
		VungleManager.OnAdFinishedEvent += adFinished;
		VungleManager.OnAdPlayableEvent += adPlayable;
		VungleManager.OnSDKLogEvent += onLog;
		VungleManager.OnSDKInitializeEvent += onInitialize;
	}

	private static void adStarted(string placementID)
	{
		if (Vungle.onAdStartedEvent != null)
		{
			Vungle.onAdStartedEvent(placementID);
		}
	}

	private static void adPlayable(string placementID, bool playable)
	{
		if (Vungle.adPlayableEvent != null)
		{
			Vungle.adPlayableEvent(placementID, playable);
		}
	}

	private static void onLog(string log)
	{
		if (Vungle.onLogEvent != null)
		{
			Vungle.onLogEvent(log);
		}
	}

	private static void adFinished(string placementID, AdFinishedEventArgs args)
	{
		if (Vungle.onAdFinishedEvent != null)
		{
			Vungle.onAdFinishedEvent(placementID, args);
		}
	}

	private static void onInitialize()
	{
		if (Vungle.onInitializeEvent != null)
		{
			Vungle.onInitializeEvent();
		}
	}

	public static void init(string appId, string[] placements)
	{
		VungleAndroid.init(appId, placements, "4.1.1");
	}

	public static void setSoundEnabled(bool isEnabled)
	{
		VungleAndroid.setSoundEnabled(isEnabled);
	}

	public static bool isAdvertAvailable(string placementID)
	{
		return VungleAndroid.isVideoAvailable(placementID);
	}

	public static void loadAd(string placementID)
	{
		VungleAndroid.loadAd(placementID);
	}

	public static void playAd(string placementID)
	{
		VungleAndroid.playAd(placementID);
	}

	public static void playAd(Dictionary<string, object> options, string placementID)
	{
		if (options == null)
		{
			options = new Dictionary<string, object>();
		}
		VungleAndroid.playAd(options, placementID);
	}

	public static void clearSleep()
	{
	}

	public static void setEndPoint(string endPoint)
	{
	}

	public static void setLogEnable(bool enable)
	{
	}

	public static string getEndPoint()
	{
		return string.Empty;
	}

	public static void onResume()
	{
		VungleAndroid.onResume();
	}

	public static void onPause()
	{
		VungleAndroid.onPause();
	}
}
