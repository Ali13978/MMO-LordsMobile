using AudienceNetwork.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AudienceNetwork
{
	internal class AdViewBridgeAndroid : AdViewBridge
	{
		private static Dictionary<int, AdViewContainer> adViews = new Dictionary<int, AdViewContainer>();

		private static int lastKey = 0;

		private AndroidJavaObject adViewForAdViewId(int uniqueId)
		{
			AdViewContainer value = null;
			if (adViews.TryGetValue(uniqueId, out value))
			{
				return value.bridgedAdView;
			}
			return null;
		}

		private string getStringForAdViewId(int uniqueId, string method)
		{
			return adViewForAdViewId(uniqueId)?.Call<string>(method, new object[0]);
		}

		private string getImageURLForAdViewId(int uniqueId, string method)
		{
			AndroidJavaObject androidJavaObject = adViewForAdViewId(uniqueId);
			if (androidJavaObject != null)
			{
				AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>(method, new object[0]);
				if (androidJavaObject2 != null)
				{
					return androidJavaObject2.Call<string>("getUrl", new object[0]);
				}
			}
			return null;
		}

		private AndroidJavaObject javaAdSizeFromAdSize(AdSize size)
		{
			AndroidJavaObject result = null;
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.facebook.ads.AdSize");
			switch (size)
			{
			case AdSize.BANNER_HEIGHT_50:
				result = androidJavaClass.GetStatic<AndroidJavaObject>("BANNER_HEIGHT_50");
				break;
			case AdSize.BANNER_HEIGHT_90:
				result = androidJavaClass.GetStatic<AndroidJavaObject>("BANNER_HEIGHT_90");
				break;
			case AdSize.RECTANGLE_HEIGHT_250:
				result = androidJavaClass.GetStatic<AndroidJavaObject>("RECTANGLE_HEIGHT_250");
				break;
			}
			return result;
		}

		public override int Create(string placementId, AdView adView, AdSize size)
		{
			AdUtility.prepare();
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
			AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("com.facebook.ads.AdView", androidJavaObject, placementId, javaAdSizeFromAdSize(size));
			AdViewBridgeListenerProxy adViewBridgeListenerProxy = new AdViewBridgeListenerProxy(adView, androidJavaObject2);
			androidJavaObject2.Call("setAdListener", adViewBridgeListenerProxy);
			AdViewContainer adViewContainer = new AdViewContainer(adView);
			adViewContainer.bridgedAdView = androidJavaObject2;
			adViewContainer.listenerProxy = adViewBridgeListenerProxy;
			int num = lastKey;
			adViews.Add(num, adViewContainer);
			lastKey++;
			return num;
		}

		public override int Load(int uniqueId)
		{
			AdUtility.prepare();
			adViewForAdViewId(uniqueId)?.Call("loadAd");
			return uniqueId;
		}

		public override bool Show(int uniqueId, double x, double y, double width, double height)
		{
			AndroidJavaObject adView = adViewForAdViewId(uniqueId);
			if (adView == null)
			{
				return false;
			}
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
			{
				AndroidJavaObject androidJavaObject = activity.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
				AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getResources", new object[0]);
				AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getDisplayMetrics", new object[0]);
				float num = androidJavaObject3.Get<float>("density");
				AndroidJavaObject androidJavaObject4 = new AndroidJavaObject("android.widget.LinearLayout$LayoutParams", (int)(width * (double)num), (int)(height * (double)num));
				AndroidJavaObject androidJavaObject5 = new AndroidJavaObject("android.widget.LinearLayout", activity);
				AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.R$id");
				AndroidJavaObject androidJavaObject6 = activity.Call<AndroidJavaObject>("findViewById", new object[1]
				{
					androidJavaClass2.GetStatic<int>("content")
				});
				androidJavaObject4.Call("setMargins", (int)(x * (double)num), (int)(y * (double)num), 0, 0);
				androidJavaObject5.Call("addView", adView, androidJavaObject4);
				androidJavaObject6.Call("addView", androidJavaObject5);
			});
			return true;
		}

		public override void DisableAutoRefresh(int uniqueId)
		{
			adViewForAdViewId(uniqueId)?.Call("disableAutoRefresh");
		}

		public override void Release(int uniqueId)
		{
			AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject adView = adViewForAdViewId(uniqueId);
			adViews.Remove(uniqueId);
			if (adView != null)
			{
				@static.Call("runOnUiThread", (AndroidJavaRunnable)delegate
				{
					adView.Call("destroy");
					AndroidJavaObject androidJavaObject = adView.Call<AndroidJavaObject>("getParent", new object[0]);
					androidJavaObject.Call("removeView", adView);
				});
			}
		}

		public override void OnLoad(int uniqueId, FBAdViewBridgeCallback callback)
		{
		}

		public override void OnImpression(int uniqueId, FBAdViewBridgeCallback callback)
		{
		}

		public override void OnClick(int uniqueId, FBAdViewBridgeCallback callback)
		{
		}

		public override void OnError(int uniqueId, FBAdViewBridgeErrorCallback callback)
		{
		}

		public override void OnFinishedClick(int uniqueId, FBAdViewBridgeCallback callback)
		{
		}
	}
}
