using System;
using UnityEngine;

namespace AudienceNetwork
{
	public sealed class InterstitialAd : IDisposable
	{
		private int uniqueId;

		private bool isLoaded;

		private AdHandler handler;

		public FBInterstitialAdBridgeCallback interstitialAdDidLoad;

		public FBInterstitialAdBridgeCallback interstitialAdWillLogImpression;

		public FBInterstitialAdBridgeErrorCallback interstitialAdDidFailWithError;

		public FBInterstitialAdBridgeCallback interstitialAdDidClick;

		public FBInterstitialAdBridgeCallback interstitialAdWillClose;

		public FBInterstitialAdBridgeCallback interstitialAdDidClose;

		public string PlacementId
		{
			get;
			private set;
		}

		public FBInterstitialAdBridgeCallback InterstitialAdDidLoad
		{
			internal get
			{
				return interstitialAdDidLoad;
			}
			set
			{
				interstitialAdDidLoad = value;
				InterstitialAdBridge.Instance.OnLoad(uniqueId, interstitialAdDidLoad);
			}
		}

		public FBInterstitialAdBridgeCallback InterstitialAdWillLogImpression
		{
			internal get
			{
				return interstitialAdWillLogImpression;
			}
			set
			{
				interstitialAdWillLogImpression = value;
				InterstitialAdBridge.Instance.OnImpression(uniqueId, interstitialAdWillLogImpression);
			}
		}

		public FBInterstitialAdBridgeErrorCallback InterstitialAdDidFailWithError
		{
			internal get
			{
				return interstitialAdDidFailWithError;
			}
			set
			{
				interstitialAdDidFailWithError = value;
				InterstitialAdBridge.Instance.OnError(uniqueId, interstitialAdDidFailWithError);
			}
		}

		public FBInterstitialAdBridgeCallback InterstitialAdDidClick
		{
			internal get
			{
				return interstitialAdDidClick;
			}
			set
			{
				interstitialAdDidClick = value;
				InterstitialAdBridge.Instance.OnClick(uniqueId, interstitialAdDidClick);
			}
		}

		public FBInterstitialAdBridgeCallback InterstitialAdWillClose
		{
			internal get
			{
				return interstitialAdWillClose;
			}
			set
			{
				interstitialAdWillClose = value;
				InterstitialAdBridge.Instance.OnWillClose(uniqueId, interstitialAdWillClose);
			}
		}

		public FBInterstitialAdBridgeCallback InterstitialAdDidClose
		{
			internal get
			{
				return interstitialAdDidClose;
			}
			set
			{
				interstitialAdDidClose = value;
				InterstitialAdBridge.Instance.OnDidClose(uniqueId, interstitialAdDidClose);
			}
		}

		public InterstitialAd(string placementId)
		{
			PlacementId = placementId;
			if (Application.platform != 0)
			{
				uniqueId = InterstitialAdBridge.Instance.Create(placementId, this);
				InterstitialAdBridge.Instance.OnLoad(uniqueId, InterstitialAdDidLoad);
				InterstitialAdBridge.Instance.OnImpression(uniqueId, InterstitialAdWillLogImpression);
				InterstitialAdBridge.Instance.OnClick(uniqueId, InterstitialAdDidClick);
				InterstitialAdBridge.Instance.OnError(uniqueId, InterstitialAdDidFailWithError);
				InterstitialAdBridge.Instance.OnWillClose(uniqueId, InterstitialAdWillClose);
				InterstitialAdBridge.Instance.OnDidClose(uniqueId, InterstitialAdDidClose);
			}
		}

		~InterstitialAd()
		{
			Dispose(iAmBeingCalledFromDisposeAndNotFinalize: false);
		}

		public void Dispose()
		{
			Dispose(iAmBeingCalledFromDisposeAndNotFinalize: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool iAmBeingCalledFromDisposeAndNotFinalize)
		{
			if ((bool)handler)
			{
				handler.removeFromParent();
			}
			UnityEngine.Debug.Log("Interstitial Ad Disposed.");
			InterstitialAdBridge.Instance.Release(uniqueId);
		}

		public override string ToString()
		{
			return $"[InterstitialAd: PlacementId={PlacementId}, InterstitialAdDidLoad={InterstitialAdDidLoad}, InterstitialAdWillLogImpression={InterstitialAdWillLogImpression}, InterstitialAdDidFailWithError={InterstitialAdDidFailWithError}, InterstitialAdDidClick={InterstitialAdDidClick}, InterstitialAdWillClose={InterstitialAdWillClose}, InterstitialAdDidClose={InterstitialAdDidClose}]";
		}

		public void Register(GameObject gameObject)
		{
			handler = gameObject.AddComponent<AdHandler>();
		}

		public void LoadAd()
		{
			if (Application.platform != 0)
			{
				InterstitialAdBridge.Instance.Load(uniqueId);
			}
			else
			{
				InterstitialAdDidLoad();
			}
		}

		public bool IsValid()
		{
			if (Application.platform != 0)
			{
				return isLoaded && InterstitialAdBridge.Instance.IsValid(uniqueId);
			}
			return true;
		}

		internal void loadAdFromData()
		{
			isLoaded = true;
		}

		public bool Show()
		{
			return InterstitialAdBridge.Instance.Show(uniqueId);
		}

		internal void executeOnMainThread(Action action)
		{
			if ((bool)handler)
			{
				handler.executeOnMainThread(action);
			}
		}

		public static implicit operator bool(InterstitialAd obj)
		{
			return !object.ReferenceEquals(obj, null);
		}
	}
}
