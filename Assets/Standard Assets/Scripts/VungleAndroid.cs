using MiniJSONV;
using System.Collections.Generic;
using UnityEngine;

public class VungleAndroid
{
	private static AndroidJavaObject _plugin;

	static VungleAndroid()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			VungleManager.noop();
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.vungle.VunglePlugin"))
			{
				_plugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
			}
		}
	}

	public static void init(string appId, string[] placements, string pluginVersion)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("init", appId, placements, pluginVersion);
		}
	}

	public static void onPause()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("onPause");
		}
	}

	public static void onResume()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("onResume");
		}
	}

	public static bool isVideoAvailable(string placementID)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return false;
		}
		return _plugin.Call<bool>("isVideoAvailable", new object[1]
		{
			placementID
		});
	}

	public static void setSoundEnabled(bool isEnabled)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("setSoundEnabled", isEnabled);
		}
	}

	public static void setAdOrientation(VungleAdOrientation orientation)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("setAdOrientation", (int)orientation);
		}
	}

	public static bool isSoundEnabled()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return true;
		}
		return _plugin.Call<bool>("isSoundEnabled", new object[0]);
	}

	public static void loadAd(string placementID)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("loadAd", placementID);
		}
	}

	public static void playAd(string placementID)
	{
		Dictionary<string, object> options = new Dictionary<string, object>();
		playAd(options, placementID);
	}

	public static void playAd(Dictionary<string, object> options, string placementID)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("playAd", Json.Serialize(options), placementID);
		}
	}
}
