using UnityEngine;

namespace VoxelBusters.NativePlugins.Internal
{
	public class Constants : MonoBehaviour
	{
		public const string kDebugTag = "Native Plugins";

		public const string kNotSupportedInEditor = "The operation could not be completed because the requested feature is not simulated in Unity Editor. Use your mobile device for testing this functionality.";

		public const string kiOSFeature = "The operation could not be completed because the requested feature is supported only on iOS platform.";

		public const string kAndroidFeature = "The operation could not be completed because the requested feature is supported only on Android platform.";

		public const string kNotSupported = "The operation could not be completed because the requested feature is not supported.";

		public const string kRootAssetsPath = "Assets";

		public const string kVBCodebasePath = "Assets/Standard Assets/VoxelBusters";

		public const string kVBExternalCodebasePath = "Assets/VoxelBusters";

		public const string kAndroidPluginsPath = "Assets/Plugins/Android";

		public const string kPluginAssetsPath = "Assets/Standard Assets/VoxelBusters/NativePlugins";

		public const string kEditorAssetsPath = "Assets/Standard Assets/VoxelBusters/NativePlugins/EditorResources";

		public const string kLogoPath = "Assets/Standard Assets/VoxelBusters/NativePlugins/EditorResources/Logo/NativePlugins.png";

		public const string kDefaultResourcesPath = "Default";

		public const string kDefaultContactImagePath = "Default/ContactImage";

		public const string kSampleUISkin = "AssetStoreProductUISkin";

		public const string kSubTitleStyle = "sub-title";

		public const string kButtonLeftStyle = "ButtonLeft";

		public const string kButtonMidStyle = "ButtonMid";

		public const string kButtonRightStyle = "ButtonRight";

		public const string kAssetStorePath = "http://bit.ly/1Fnpb5j";

		public const string kPurchaseFullVersionButton = "Purchase Full Version";

		public const string kFeatureNotSupportedInLiteVersion = "Feature not supported in Lite version. Please purchase full version of Native Plugins.";

		public const string kAndroidPluginsLibraryPath = "Assets/Plugins/Android/native_plugins_lib";

		public const string kAndroidPluginsJARPath = "Assets/Plugins/Android/native_plugins_lib/libs";

		public const string kGameServicesUserAuthMissingError = "The requested operation could not be completed because local player has not been authenticated.";

		public const string kGameServicesIdentifierNullError = "The requested operation could not be completed because identifier is null.";

		public const string kGameServicesIdentifierInfoNotFoundError = "The requested operation could not be completed because identifier records are not found.";

		public const string kPlayMakerDateTimeFormat = "yyyy-MM-dd HH:mm";

		public const string kAddressBookJARName = "feature.addressbook";

		public const string kBillingJARName = "feature.billing";

		public const string kBillingAmazonJARName = "feature.billing.amazon";

		public const string kCloudServicesJARName = "feature.cloudservices";

		public const string kGameServicesJARName = "feature.gameservices";

		public const string kMediaLibraryJARName = "feature.medialibrary";

		public const string kNotificationJARName = "feature.notification";

		public const string kNetworkConnectivityJARName = "feature.reachability";

		public const string kSharingJARName = "feature.sharing";

		public const string kSoomlaIntegrationJARName = "feature.sdk.soomla.integration";

		public const string kSocialNetworkTwitterJARName = "feature.socialnetwork.twitter";

		public const string kWebviewJARName = "feature.webview";

		public const string kExternalNotificationLibJARName = "feature.externallibrary.shortcutbadger";

		public const string kTwitterLibraryName = "twitter_lib";

		public const string kYoutubeLibraryName = "youtube_lib";

		public const string kPrefsKeyResolveAndroidDependencies = "np-resolve-android-dependencies";

		public const string kFullVersionProductURL = "http://bit.ly/1Fnpb5j";

		public const string kLiteVersionProductURL = "http://bit.ly/1KFEzdi";

		public const string kProductURL = "http://bit.ly/1KFEzdi";
	}
}
