using UnityEngine;

namespace AudienceNetwork
{
	internal class InterstitialAdContainer
	{
		internal AndroidJavaProxy listenerProxy;

		internal AndroidJavaObject bridgedInterstitialAd;

		internal InterstitialAd interstitialAd
		{
			get;
			set;
		}

		internal FBInterstitialAdBridgeCallback onLoad
		{
			get;
			set;
		}

		internal FBInterstitialAdBridgeCallback onImpression
		{
			get;
			set;
		}

		internal FBInterstitialAdBridgeCallback onClick
		{
			get;
			set;
		}

		internal FBInterstitialAdBridgeErrorCallback onError
		{
			get;
			set;
		}

		internal FBInterstitialAdBridgeCallback onDidClose
		{
			get;
			set;
		}

		internal FBInterstitialAdBridgeCallback onWillClose
		{
			get;
			set;
		}

		internal InterstitialAdContainer(InterstitialAd interstitialAd)
		{
			this.interstitialAd = interstitialAd;
		}

		public override string ToString()
		{
			return $"[InterstitialAdContainer: interstitialAd={interstitialAd}, onLoad={onLoad}]";
		}

		public static implicit operator bool(InterstitialAdContainer obj)
		{
			return !object.ReferenceEquals(obj, null);
		}
	}
}
