using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AudienceNetwork
{
	public sealed class NativeAd : IDisposable
	{
		internal const float MIN_ALPHA = 0.9f;

		internal const int MAX_ROTATION = 45;

		internal const int CHECK_VIEWABILITY_INTERVAL = 1;

		private int uniqueId;

		private bool isLoaded;

		private int minViewabilityPercentage;

		private NativeAdHandler handler;

		private FBNativeAdBridgeCallback nativeAdDidLoad;

		private FBNativeAdBridgeCallback nativeAdWillLogImpression;

		private FBNativeAdBridgeErrorCallback nativeAdDidFailWithError;

		private FBNativeAdBridgeCallback nativeAdDidClick;

		private FBNativeAdBridgeCallback nativeAdDidFinishHandlingClick;

		public string PlacementId
		{
			get;
			private set;
		}

		public string Title
		{
			get;
			private set;
		}

		public string Subtitle
		{
			get;
			private set;
		}

		public string Body
		{
			get;
			private set;
		}

		public string CallToAction
		{
			get;
			private set;
		}

		public string SocialContext
		{
			get;
			private set;
		}

		public string IconImageURL
		{
			get;
			private set;
		}

		public string CoverImageURL
		{
			get;
			private set;
		}

		public string AdChoicesImageURL
		{
			get;
			private set;
		}

		public Sprite IconImage
		{
			get;
			private set;
		}

		public Sprite CoverImage
		{
			get;
			private set;
		}

		public Sprite AdChoicesImage
		{
			get;
			private set;
		}

		public string AdChoicesText
		{
			get;
			private set;
		}

		public string AdChoicesLinkURL
		{
			get;
			private set;
		}

		public FBNativeAdBridgeCallback NativeAdDidLoad
		{
			internal get
			{
				return nativeAdDidLoad;
			}
			set
			{
				nativeAdDidLoad = value;
				NativeAdBridge.Instance.OnLoad(uniqueId, nativeAdDidLoad);
			}
		}

		public FBNativeAdBridgeCallback NativeAdWillLogImpression
		{
			internal get
			{
				return nativeAdWillLogImpression;
			}
			set
			{
				nativeAdWillLogImpression = value;
				NativeAdBridge.Instance.OnImpression(uniqueId, nativeAdWillLogImpression);
			}
		}

		public FBNativeAdBridgeErrorCallback NativeAdDidFailWithError
		{
			internal get
			{
				return nativeAdDidFailWithError;
			}
			set
			{
				nativeAdDidFailWithError = value;
				NativeAdBridge.Instance.OnError(uniqueId, nativeAdDidFailWithError);
			}
		}

		public FBNativeAdBridgeCallback NativeAdDidClick
		{
			internal get
			{
				return nativeAdDidClick;
			}
			set
			{
				nativeAdDidClick = value;
				NativeAdBridge.Instance.OnClick(uniqueId, nativeAdDidClick);
			}
		}

		public FBNativeAdBridgeCallback NativeAdDidFinishHandlingClick
		{
			internal get
			{
				return nativeAdDidFinishHandlingClick;
			}
			set
			{
				nativeAdDidFinishHandlingClick = value;
				NativeAdBridge.Instance.OnFinishedClick(uniqueId, nativeAdDidFinishHandlingClick);
			}
		}

		public NativeAd(string placementId)
		{
			PlacementId = placementId;
			uniqueId = NativeAdBridge.Instance.Create(placementId, this);
			NativeAdBridge.Instance.OnLoad(uniqueId, NativeAdDidLoad);
			NativeAdBridge.Instance.OnImpression(uniqueId, NativeAdWillLogImpression);
			NativeAdBridge.Instance.OnClick(uniqueId, NativeAdDidClick);
			NativeAdBridge.Instance.OnError(uniqueId, NativeAdDidFailWithError);
			NativeAdBridge.Instance.OnFinishedClick(uniqueId, NativeAdDidFinishHandlingClick);
		}

		~NativeAd()
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
				handler.stopImpressionValidation();
				handler.removeFromParent();
			}
			UnityEngine.Debug.Log("Native Ad Disposed.");
			NativeAdBridge.Instance.Release(uniqueId);
		}

		public override string ToString()
		{
			return $"[NativeAd: PlacementId={PlacementId}, Title={Title}, Subtitle={Subtitle}, Body={Body}, CallToAction={CallToAction}, SocialContext={SocialContext}, IconImageURL={IconImageURL}, CoverImageURL={CoverImageURL}, IconImage={IconImage}, CoverImage={CoverImage}, NativeAdDidLoad={NativeAdDidLoad}, NativeAdWillLogImpression={NativeAdWillLogImpression}, NativeAdDidFailWithError={NativeAdDidFailWithError}, NativeAdDidClick={NativeAdDidClick}, NativeAdDidFinishHandlingClick={NativeAdDidFinishHandlingClick}]";
		}

		private static TextureFormat imageFormat()
		{
			return TextureFormat.RGBA32;
		}

		public IEnumerator LoadIconImage(string url)
		{
			Texture2D texture = new Texture2D(4, 4, imageFormat(), mipChain: false);
			WWW www = new WWW(url);
			yield return www;
			www.LoadImageIntoTexture(texture);
			if ((bool)texture)
			{
				IconImage = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			}
		}

		public IEnumerator LoadCoverImage(string url)
		{
			Texture2D texture = new Texture2D(4, 4, imageFormat(), mipChain: false);
			WWW www = new WWW(url);
			yield return www;
			www.LoadImageIntoTexture(texture);
			if ((bool)texture)
			{
				CoverImage = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			}
		}

		public IEnumerator LoadAdChoicesImage(string url)
		{
			Texture2D texture = new Texture2D(4, 4, imageFormat(), mipChain: false);
			WWW www = new WWW(url);
			yield return www;
			www.LoadImageIntoTexture(texture);
			if ((bool)texture)
			{
				AdChoicesImage = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			}
		}

		public void LoadAd()
		{
			NativeAdBridge.Instance.Load(uniqueId);
		}

		public bool IsValid()
		{
			return isLoaded && NativeAdBridge.Instance.IsValid(uniqueId);
		}

		private void RegisterGameObjectForManualImpression(GameObject gameObject)
		{
			createHandler(gameObject);
		}

		public void RegisterGameObjectForImpression(GameObject gameObject, Button[] clickableButtons)
		{
			RegisterGameObjectForImpression(gameObject, clickableButtons, Camera.main);
		}

		public void RegisterGameObjectForImpression(GameObject gameObject, Button[] clickableButtons, Camera camera)
		{
			foreach (Button button in clickableButtons)
			{
				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(delegate
				{
					AdLogger.Log("Native ad with unique id " + uniqueId + " clicked!");
					ExternalClick();
				});
			}
			if ((bool)handler)
			{
				handler.stopImpressionValidation();
				handler.removeFromParent();
				createHandler(camera, gameObject);
				handler.startImpressionValidation();
			}
			else
			{
				createHandler(camera, gameObject);
			}
		}

		private void createHandler(GameObject gameObject)
		{
			createHandler(null, gameObject);
		}

		private void createHandler(Camera camera, GameObject gameObject)
		{
			handler = gameObject.AddComponent<NativeAdHandler>();
			handler.camera = camera;
			handler.minAlpha = 0.9f;
			handler.maxRotation = 45;
			handler.checkViewabilityInterval = 1;
			handler.validationCallback = delegate(bool success)
			{
				UnityEngine.Debug.Log("Native ad viewability check for unique id " + uniqueId + " returned success? " + success);
				if (success)
				{
					AdLogger.Log("Native ad with unique id " + uniqueId + " registered impression!");
					ExternalLogImpression();
					handler.stopImpressionValidation();
				}
			};
		}

		private void ManualLogImpression()
		{
			NativeAdBridge.Instance.ManualLogImpression(uniqueId);
		}

		private void ManualClick()
		{
			NativeAdBridge.Instance.ManualLogClick(uniqueId);
		}

		internal void ExternalLogImpression()
		{
			NativeAdBridge.Instance.ExternalLogImpression(uniqueId);
		}

		internal void ExternalClick()
		{
			NativeAdBridge.Instance.ExternalLogClick(uniqueId);
		}

		internal void loadAdFromData()
		{
			if (handler == null)
			{
				throw new InvalidOperationException("Native ad was loaded before it was registered. Ensure RegisterGameObjectForManualImpression () or RegisterGameObjectForImpression () are called.");
			}
			int num = uniqueId;
			Title = NativeAdBridge.Instance.GetTitle(num);
			Subtitle = NativeAdBridge.Instance.GetSubtitle(num);
			Body = NativeAdBridge.Instance.GetBody(num);
			CallToAction = NativeAdBridge.Instance.GetCallToAction(num);
			SocialContext = NativeAdBridge.Instance.GetSocialContext(num);
			CoverImageURL = NativeAdBridge.Instance.GetCoverImageURL(num);
			IconImageURL = NativeAdBridge.Instance.GetIconImageURL(num);
			AdChoicesImageURL = NativeAdBridge.Instance.GetAdChoicesImageURL(num);
			isLoaded = true;
			minViewabilityPercentage = NativeAdBridge.Instance.GetMinViewabilityPercentage(num);
			handler.minViewabilityPercentage = minViewabilityPercentage;
			if (NativeAdDidLoad != null)
			{
				handler.executeOnMainThread(delegate
				{
					NativeAdDidLoad();
				});
			}
			handler.executeOnMainThread(delegate
			{
				handler.startImpressionValidation();
			});
		}

		internal void executeOnMainThread(Action action)
		{
			if ((bool)handler)
			{
				handler.executeOnMainThread(action);
			}
		}

		public static implicit operator bool(NativeAd obj)
		{
			return !object.ReferenceEquals(obj, null);
		}
	}
}
