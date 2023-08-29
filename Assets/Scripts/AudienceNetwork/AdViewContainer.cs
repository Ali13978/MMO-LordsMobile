using UnityEngine;

namespace AudienceNetwork
{
	internal class AdViewContainer
	{
		internal AndroidJavaProxy listenerProxy;

		internal AndroidJavaObject bridgedAdView;

		internal AdView adView
		{
			get;
			set;
		}

		internal FBAdViewBridgeCallback onLoad
		{
			get;
			set;
		}

		internal FBAdViewBridgeCallback onImpression
		{
			get;
			set;
		}

		internal FBAdViewBridgeCallback onClick
		{
			get;
			set;
		}

		internal FBAdViewBridgeErrorCallback onError
		{
			get;
			set;
		}

		internal FBAdViewBridgeCallback onFinishedClick
		{
			get;
			set;
		}

		internal AdViewContainer(AdView adView)
		{
			this.adView = adView;
		}

		public override string ToString()
		{
			return $"[AdViewContainer: adView={adView}, onLoad={onLoad}]";
		}

		public static implicit operator bool(AdViewContainer obj)
		{
			return !object.ReferenceEquals(obj, null);
		}
	}
}
