using System;
using UnityEngine;

namespace AudienceNetwork
{
	public sealed class RewardedVideoAd : IDisposable
	{
		private int uniqueId;

		private bool isLoaded;

		private AdHandler handler;

		public FBRewardedVideoAdBridgeCallback rewardedVideoAdDidLoad;

		public FBRewardedVideoAdBridgeCallback rewardedVideoAdWillLogImpression;

		public FBRewardedVideoAdBridgeErrorCallback rewardedVideoAdDidFailWithError;

		public FBRewardedVideoAdBridgeCallback rewardedVideoAdDidClick;

		public FBRewardedVideoAdBridgeCallback rewardedVideoAdWillClose;

		public FBRewardedVideoAdBridgeCallback rewardedVideoAdDidClose;

		public FBRewardedVideoAdBridgeCallback rewardedVideoAdComplete;

		public FBRewardedVideoAdBridgeCallback rewardedVideoAdDidSucceed;

		public FBRewardedVideoAdBridgeCallback rewardedVideoAdDidFail;

		public string PlacementId
		{
			get;
			private set;
		}

		public FBRewardedVideoAdBridgeCallback RewardedVideoAdDidLoad
		{
			internal get
			{
				return rewardedVideoAdDidLoad;
			}
			set
			{
				rewardedVideoAdDidLoad = value;
				RewardedVideoAdBridge.Instance.OnLoad(uniqueId, rewardedVideoAdDidLoad);
			}
		}

		public FBRewardedVideoAdBridgeCallback RewardedVideoAdWillLogImpression
		{
			internal get
			{
				return rewardedVideoAdWillLogImpression;
			}
			set
			{
				rewardedVideoAdWillLogImpression = value;
				RewardedVideoAdBridge.Instance.OnImpression(uniqueId, rewardedVideoAdWillLogImpression);
			}
		}

		public FBRewardedVideoAdBridgeErrorCallback RewardedVideoAdDidFailWithError
		{
			internal get
			{
				return rewardedVideoAdDidFailWithError;
			}
			set
			{
				rewardedVideoAdDidFailWithError = value;
				RewardedVideoAdBridge.Instance.OnError(uniqueId, rewardedVideoAdDidFailWithError);
			}
		}

		public FBRewardedVideoAdBridgeCallback RewardedVideoAdDidClick
		{
			internal get
			{
				return rewardedVideoAdDidClick;
			}
			set
			{
				rewardedVideoAdDidClick = value;
				RewardedVideoAdBridge.Instance.OnClick(uniqueId, rewardedVideoAdDidClick);
			}
		}

		public FBRewardedVideoAdBridgeCallback RewardedVideoAdWillClose
		{
			internal get
			{
				return rewardedVideoAdWillClose;
			}
			set
			{
				rewardedVideoAdWillClose = value;
				RewardedVideoAdBridge.Instance.OnWillClose(uniqueId, rewardedVideoAdWillClose);
			}
		}

		public FBRewardedVideoAdBridgeCallback RewardedVideoAdDidClose
		{
			internal get
			{
				return rewardedVideoAdDidClose;
			}
			set
			{
				rewardedVideoAdDidClose = value;
				RewardedVideoAdBridge.Instance.OnDidClose(uniqueId, rewardedVideoAdDidClose);
			}
		}

		public FBRewardedVideoAdBridgeCallback RewardedVideoAdComplete
		{
			internal get
			{
				return rewardedVideoAdComplete;
			}
			set
			{
				rewardedVideoAdComplete = value;
				RewardedVideoAdBridge.Instance.OnComplete(uniqueId, rewardedVideoAdComplete);
			}
		}

		public FBRewardedVideoAdBridgeCallback RewardedVideoAdDidSucceed
		{
			internal get
			{
				return rewardedVideoAdDidSucceed;
			}
			set
			{
				rewardedVideoAdDidSucceed = value;
				RewardedVideoAdBridge.Instance.OnDidSucceed(uniqueId, rewardedVideoAdDidSucceed);
			}
		}

		public FBRewardedVideoAdBridgeCallback RewardedVideoAdDidFail
		{
			internal get
			{
				return rewardedVideoAdDidFail;
			}
			set
			{
				rewardedVideoAdDidFail = value;
				RewardedVideoAdBridge.Instance.OnDidFail(uniqueId, rewardedVideoAdDidFail);
			}
		}

		public RewardedVideoAd(string placementId)
		{
			PlacementId = placementId;
			if (Application.platform != 0)
			{
				uniqueId = RewardedVideoAdBridge.Instance.Create(placementId, this);
				RewardedVideoAdBridge.Instance.OnLoad(uniqueId, RewardedVideoAdDidLoad);
				RewardedVideoAdBridge.Instance.OnImpression(uniqueId, RewardedVideoAdWillLogImpression);
				RewardedVideoAdBridge.Instance.OnClick(uniqueId, RewardedVideoAdDidClick);
				RewardedVideoAdBridge.Instance.OnError(uniqueId, RewardedVideoAdDidFailWithError);
				RewardedVideoAdBridge.Instance.OnWillClose(uniqueId, RewardedVideoAdWillClose);
				RewardedVideoAdBridge.Instance.OnDidClose(uniqueId, RewardedVideoAdDidClose);
				RewardedVideoAdBridge.Instance.OnComplete(uniqueId, RewardedVideoAdComplete);
				RewardedVideoAdBridge.Instance.OnDidSucceed(uniqueId, RewardedVideoAdDidSucceed);
				RewardedVideoAdBridge.Instance.OnDidFail(uniqueId, RewardedVideoAdDidFail);
			}
		}

		~RewardedVideoAd()
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
			UnityEngine.Debug.Log("RewardedVideo Ad Disposed.");
			RewardedVideoAdBridge.Instance.Release(uniqueId);
		}

		public override string ToString()
		{
			return $"[RewardedVideoAd: PlacementId={PlacementId}, RewardedVideoAdDidLoad={RewardedVideoAdDidLoad}, RewardedVideoAdWillLogImpression={RewardedVideoAdWillLogImpression}, RewardedVideoAdDidFailWithError={RewardedVideoAdDidFailWithError}, RewardedVideoAdDidClick={RewardedVideoAdDidClick}, RewardedVideoAdWillClose={RewardedVideoAdWillClose}, RewardedVideoAdDidClose={RewardedVideoAdDidClose}, RewardedVideoAdComplete={RewardedVideoAdComplete}, RewardedVideoAdDidSucceed={RewardedVideoAdDidSucceed}, RewardedVideoAdDidFail={RewardedVideoAdDidFail}]";
		}

		public void Register(GameObject gameObject)
		{
			handler = gameObject.AddComponent<AdHandler>();
		}

		public void LoadAd()
		{
			if (Application.platform != 0)
			{
				RewardedVideoAdBridge.Instance.Load(uniqueId);
			}
			else
			{
				RewardedVideoAdDidLoad();
			}
		}

		public bool IsValid()
		{
			if (Application.platform != 0)
			{
				return isLoaded && RewardedVideoAdBridge.Instance.IsValid(uniqueId);
			}
			return true;
		}

		internal void loadAdFromData()
		{
			isLoaded = true;
		}

		public bool Show()
		{
			return RewardedVideoAdBridge.Instance.Show(uniqueId);
		}

		internal void executeOnMainThread(Action action)
		{
			if ((bool)handler)
			{
				handler.executeOnMainThread(action);
			}
		}

		public static implicit operator bool(RewardedVideoAd obj)
		{
			return !object.ReferenceEquals(obj, null);
		}
	}
}
