using System;
using UnityEngine;
using VoxelBusters.AssetStoreProductUtility;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class NPSettings : AdvancedScriptableObject<NPSettings>, IAssetStoreProduct, IRateMyAppDelegate
	{
		private const bool kIsFullVersion = false;

		private const string kProductName = "Native Plugins";

		private const string kProductVersion = "1.5.4";

		private const string kPrefsKeyBuildIdentifier = "np-build-identifier";

		internal const string kPrefsKeyPropertyModified = "np-property-modified";

		internal const string kMethodPropertyChanged = "OnPropertyModified";

		internal const string kLiteVersionMacro = "NATIVE_PLUGINS_LITE_VERSION";

		private const string kMacroAddressBook = "USES_ADDRESS_BOOK";

		private const string kMacroBilling = "USES_BILLING";

		private const string kMacroCloudServices = "USES_CLOUD_SERVICES";

		private const string kMacroGameServices = "USES_GAME_SERVICES";

		private const string kMacroMediaLibrary = "USES_MEDIA_LIBRARY";

		private const string kMacroNetworkConnectivity = "USES_NETWORK_CONNECTIVITY";

		private const string kMacroNotificationService = "USES_NOTIFICATION_SERVICE";

		private const string kMacroSharin = "USES_SHARING";

		private const string kMacroTwitter = "USES_TWITTER";

		private const string kMacroWebView = "USES_WEBVIEW";

		private const string kMacroSoomlaGrowService = "USES_SOOMLA_GROW";

		private const string kMacroOneSignalService = "USES_ONE_SIGNAL";

		[NonSerialized]
		private AssetStoreProduct m_assetStoreProduct;

		private RateMyApp m_rateMyApp;

		[HideInInspector]
		[SerializeField]
		private string m_lastOpenedDateString;

		[SerializeField]
		private ApplicationSettings m_applicationSettings = new ApplicationSettings();

		[SerializeField]
		private UtilitySettings m_utilitySettings = new UtilitySettings();

		[SerializeField]
		private AddonServicesSettings m_addonServicesSettings = new AddonServicesSettings();

		public static ApplicationSettings Application => AdvancedScriptableObject<NPSettings>.Instance.m_applicationSettings;

		public static UtilitySettings Utility => AdvancedScriptableObject<NPSettings>.Instance.m_utilitySettings;

		public static AddonServicesSettings AddonServicesSettings => AdvancedScriptableObject<NPSettings>.Instance.m_addonServicesSettings;

		public AssetStoreProduct AssetStoreProduct => m_assetStoreProduct;

		protected override void Reset()
		{
			base.Reset();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		public bool CanShowRateMyAppDialog()
		{
			return false;
		}

		public void OnBeforeShowingRateMyAppDialog()
		{
		}
	}
}
