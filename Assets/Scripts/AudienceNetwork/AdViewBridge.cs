using UnityEngine;

namespace AudienceNetwork
{
	internal class AdViewBridge : IAdViewBridge
	{
		public static readonly IAdViewBridge Instance;

		internal AdViewBridge()
		{
		}

		static AdViewBridge()
		{
			Instance = createInstance();
		}

		private static IAdViewBridge createInstance()
		{
			if (Application.platform != 0)
			{
				return new AdViewBridgeAndroid();
			}
			return new AdViewBridge();
		}

		public virtual int Create(string placementId, AdView AdView, AdSize size)
		{
			return 123;
		}

		public virtual int Load(int uniqueId)
		{
			return 123;
		}

		public virtual bool Show(int uniqueId, double x, double y, double width, double height)
		{
			return true;
		}

		public virtual void DisableAutoRefresh(int uniqueId)
		{
		}

		public virtual void ManualLogImpression(int uniqueId)
		{
		}

		public virtual void ManualLogClick(int uniqueId)
		{
		}

		public virtual void Release(int uniqueId)
		{
		}

		public virtual void OnLoad(int uniqueId, FBAdViewBridgeCallback callback)
		{
		}

		public virtual void OnImpression(int uniqueId, FBAdViewBridgeCallback callback)
		{
		}

		public virtual void OnClick(int uniqueId, FBAdViewBridgeCallback callback)
		{
		}

		public virtual void OnError(int uniqueId, FBAdViewBridgeErrorCallback callback)
		{
		}

		public virtual void OnFinishedClick(int uniqueId, FBAdViewBridgeCallback callback)
		{
		}
	}
}
