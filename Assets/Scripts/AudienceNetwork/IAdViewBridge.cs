namespace AudienceNetwork
{
	internal interface IAdViewBridge
	{
		int Create(string placementId, AdView adView, AdSize size);

		int Load(int uniqueId);

		bool Show(int uniqueId, double x, double y, double width, double height);

		void DisableAutoRefresh(int uniqueId);

		void Release(int uniqueId);

		void OnLoad(int uniqueId, FBAdViewBridgeCallback callback);

		void OnImpression(int uniqueId, FBAdViewBridgeCallback callback);

		void OnClick(int uniqueId, FBAdViewBridgeCallback callback);

		void OnError(int uniqueId, FBAdViewBridgeErrorCallback callback);

		void OnFinishedClick(int uniqueId, FBAdViewBridgeCallback callback);
	}
}
