using System;
using UnityEngine;

namespace AudienceNetwork.Utility
{
	internal class AdUtilityBridgeAndroid : AdUtilityBridge
	{
		private T getPropertyOfDisplayMetrics<T>(string property)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
			AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getResources", new object[0]);
			AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getDisplayMetrics", new object[0]);
			return androidJavaObject3.Get<T>(property);
		}

		private double density()
		{
			return getPropertyOfDisplayMetrics<float>("density");
		}

		public override double deviceWidth()
		{
			return getPropertyOfDisplayMetrics<int>("widthPixels");
		}

		public override double deviceHeight()
		{
			return getPropertyOfDisplayMetrics<int>("heightPixels");
		}

		public override double width()
		{
			return convert(deviceWidth());
		}

		public override double height()
		{
			return convert(deviceHeight());
		}

		public override double convert(double deviceSize)
		{
			return deviceSize / density();
		}

		public override void prepare()
		{
			try
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.facebook.ads.internal.DisplayAdController");
				androidJavaClass.CallStatic("setMainThreadForced", true);
			}
			catch (Exception)
			{
			}
			try
			{
				AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.os.Looper");
				androidJavaClass2.CallStatic("prepare");
			}
			catch (Exception)
			{
			}
		}
	}
}
