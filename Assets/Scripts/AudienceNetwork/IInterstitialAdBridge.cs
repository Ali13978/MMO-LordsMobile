namespace AudienceNetwork
{
	internal interface IInterstitialAdBridge
	{
		int Create(string placementId, InterstitialAd interstitialAd);

		int Load(int uniqueId);

		bool IsValid(int uniqueId);

		bool Show(int uniqueId);

		void Release(int uniqueId);

		void OnLoad(int uniqueId, FBInterstitialAdBridgeCallback callback);

		void OnImpression(int uniqueId, FBInterstitialAdBridgeCallback callback);

		void OnClick(int uniqueId, FBInterstitialAdBridgeCallback callback);

		void OnError(int uniqueId, FBInterstitialAdBridgeErrorCallback callback);

		void OnWillClose(int uniqueId, FBInterstitialAdBridgeCallback callback);

		void OnDidClose(int uniqueId, FBInterstitialAdBridgeCallback callback);
	}
}
