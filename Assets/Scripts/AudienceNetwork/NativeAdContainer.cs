using UnityEngine;

namespace AudienceNetwork
{
	internal class NativeAdContainer
	{
		internal AndroidJavaProxy listenerProxy;

		internal AndroidJavaObject bridgedNativeAd;

		internal NativeAd nativeAd
		{
			get;
			set;
		}

		internal FBNativeAdBridgeCallback onLoad
		{
			get;
			set;
		}

		internal FBNativeAdBridgeCallback onImpression
		{
			get;
			set;
		}

		internal FBNativeAdBridgeCallback onClick
		{
			get;
			set;
		}

		internal FBNativeAdBridgeErrorCallback onError
		{
			get;
			set;
		}

		internal FBNativeAdBridgeCallback onFinishedClick
		{
			get;
			set;
		}

		internal NativeAdContainer(NativeAd nativeAd)
		{
			this.nativeAd = nativeAd;
		}

		public static implicit operator bool(NativeAdContainer obj)
		{
			return !object.ReferenceEquals(obj, null);
		}
	}
}
