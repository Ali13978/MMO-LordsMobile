namespace AudienceNetwork
{
	internal interface INativeAdBridge
	{
		int Create(string placementId, NativeAd nativeAd);

		int Load(int uniqueId);

		bool IsValid(int uniqueId);

		string GetTitle(int uniqueId);

		string GetSubtitle(int uniqueId);

		string GetBody(int uniqueId);

		string GetCallToAction(int uniqueId);

		string GetSocialContext(int uniqueId);

		string GetIconImageURL(int uniqueId);

		string GetCoverImageURL(int uniqueId);

		string GetAdChoicesImageURL(int uniqueId);

		string GetAdChoicesText(int uniqueId);

		string GetAdChoicesLinkURL(int uniqueId);

		int GetMinViewabilityPercentage(int uniqueId);

		void ManualLogImpression(int uniqueId);

		void ManualLogClick(int uniqueId);

		void ExternalLogImpression(int uniqueId);

		void ExternalLogClick(int uniqueId);

		void Release(int uniqueId);

		void OnLoad(int uniqueId, FBNativeAdBridgeCallback callback);

		void OnImpression(int uniqueId, FBNativeAdBridgeCallback callback);

		void OnClick(int uniqueId, FBNativeAdBridgeCallback callback);

		void OnError(int uniqueId, FBNativeAdBridgeErrorCallback callback);

		void OnFinishedClick(int uniqueId, FBNativeAdBridgeCallback callback);
	}
}
