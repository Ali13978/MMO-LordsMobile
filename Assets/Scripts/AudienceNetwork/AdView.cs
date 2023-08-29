using AudienceNetwork.Utility;
using System;
using UnityEngine;

namespace AudienceNetwork
{
	public sealed class AdView : IDisposable
	{
		private int uniqueId;

		private AdSize size;

		private AdHandler handler;

		public FBAdViewBridgeCallback adViewDidLoad;

		public FBAdViewBridgeCallback adViewWillLogImpression;

		public FBAdViewBridgeErrorCallback adViewDidFailWithError;

		public FBAdViewBridgeCallback adViewDidClick;

		public FBAdViewBridgeCallback adViewDidFinishClick;

		public string PlacementId
		{
			get;
			private set;
		}

		public FBAdViewBridgeCallback AdViewDidLoad
		{
			internal get
			{
				return adViewDidLoad;
			}
			set
			{
				adViewDidLoad = value;
				AdViewBridge.Instance.OnLoad(uniqueId, adViewDidLoad);
			}
		}

		public FBAdViewBridgeCallback AdViewWillLogImpression
		{
			internal get
			{
				return adViewWillLogImpression;
			}
			set
			{
				adViewWillLogImpression = value;
				AdViewBridge.Instance.OnImpression(uniqueId, adViewWillLogImpression);
			}
		}

		public FBAdViewBridgeErrorCallback AdViewDidFailWithError
		{
			internal get
			{
				return adViewDidFailWithError;
			}
			set
			{
				adViewDidFailWithError = value;
				AdViewBridge.Instance.OnError(uniqueId, adViewDidFailWithError);
			}
		}

		public FBAdViewBridgeCallback AdViewDidClick
		{
			internal get
			{
				return adViewDidClick;
			}
			set
			{
				adViewDidClick = value;
				AdViewBridge.Instance.OnClick(uniqueId, adViewDidClick);
			}
		}

		public FBAdViewBridgeCallback AdViewDidFinishClick
		{
			internal get
			{
				return adViewDidFinishClick;
			}
			set
			{
				adViewDidFinishClick = value;
				AdViewBridge.Instance.OnFinishedClick(uniqueId, adViewDidFinishClick);
			}
		}

		public AdView(string placementId, AdSize size)
		{
			PlacementId = placementId;
			this.size = size;
			if (Application.platform != 0)
			{
				uniqueId = AdViewBridge.Instance.Create(placementId, this, size);
				AdViewBridge.Instance.OnLoad(uniqueId, AdViewDidLoad);
				AdViewBridge.Instance.OnImpression(uniqueId, AdViewWillLogImpression);
				AdViewBridge.Instance.OnClick(uniqueId, AdViewDidClick);
				AdViewBridge.Instance.OnError(uniqueId, AdViewDidFailWithError);
				AdViewBridge.Instance.OnFinishedClick(uniqueId, AdViewDidFinishClick);
			}
		}

		~AdView()
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
			UnityEngine.Debug.Log("Banner Ad Disposed.");
			AdViewBridge.Instance.Release(uniqueId);
		}

		public override string ToString()
		{
			return $"[AdView: PlacementId={PlacementId}, AdViewDidLoad={AdViewDidLoad}, AdViewWillLogImpression={AdViewWillLogImpression}, AdViewDidFailWithError={AdViewDidFailWithError}, AdViewDidClick={AdViewDidClick}, adViewDidFinishClick={adViewDidFinishClick}]";
		}

		public void Register(GameObject gameObject)
		{
			handler = gameObject.AddComponent<AdHandler>();
		}

		public void LoadAd()
		{
			if (Application.platform != 0)
			{
				AdViewBridge.Instance.Load(uniqueId);
			}
			else
			{
				AdViewDidLoad();
			}
		}

		private double heightFromType(AdSize size)
		{
			switch (size)
			{
			case AdSize.BANNER_HEIGHT_50:
				return 50.0;
			case AdSize.BANNER_HEIGHT_90:
				return 90.0;
			case AdSize.RECTANGLE_HEIGHT_250:
				return 250.0;
			default:
				return 0.0;
			}
		}

		public bool Show(double y)
		{
			return AdViewBridge.Instance.Show(uniqueId, 0.0, y, AdUtility.width(), heightFromType(size));
		}

		public bool Show(double x, double y)
		{
			return AdViewBridge.Instance.Show(uniqueId, x, y, AdUtility.width(), heightFromType(size));
		}

		private bool Show(double x, double y, double width, double height)
		{
			return AdViewBridge.Instance.Show(uniqueId, x, y, width, height);
		}

		public void DisableAutoRefresh()
		{
			AdViewBridge.Instance.DisableAutoRefresh(uniqueId);
		}

		internal void executeOnMainThread(Action action)
		{
			if ((bool)handler)
			{
				handler.executeOnMainThread(action);
			}
		}

		public static implicit operator bool(AdView obj)
		{
			return !object.ReferenceEquals(obj, null);
		}
	}
}
