using System;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class ApplicationSettings
	{
		[Serializable]
		public class AddonServices
		{
			public bool UsesSoomlaGrow => false;

			public bool UsesOneSignal => false;
		}

		[Serializable]
		public class AndroidSettings
		{
			[SerializeField]
			[Tooltip("The string that identifies your app in Google Play Store.")]
			private string m_storeIdentifier;

			public string StoreIdentifier
			{
				get
				{
					return m_storeIdentifier;
				}
				private set
				{
					m_storeIdentifier = value;
				}
			}
		}

		[Serializable]
		public class Features
		{
			[Serializable]
			public class MultiComponentFeature
			{
				public bool value = true;
			}

			[Serializable]
			public class MediaLibraryFeature : MultiComponentFeature
			{
				public bool usesCamera = true;

				public bool usesPhotoAlbum = true;
			}

			[Serializable]
			public class NotificationServiceFeature : MultiComponentFeature
			{
				public bool usesLocalNotification = true;

				public bool usesRemoteNotification = true;
			}

			[SerializeField]
			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, Address Book feature will be active within your application.")]
			private bool m_usesAddressBook = true;

			[NotifyNPSettingsOnValueChange]
			[Tooltip("If enabled, Network Connectivity feature will be active within your application.")]
			[SerializeField]
			private bool m_usesNetworkConnectivity = true;

			[Tooltip("If enabled, Sharing feature will be active within your application.")]
			[NotifyNPSettingsOnValueChange]
			[SerializeField]
			private bool m_usesSharing = true;

			public bool UsesAddressBook => m_usesAddressBook;

			public bool UsesBilling => false;

			public bool UsesCloudServices => false;

			public bool UsesGameServices => false;

			public bool UsesMediaLibrary => false;

			public MediaLibraryFeature MediaLibrary => null;

			public bool UsesNetworkConnectivity => m_usesNetworkConnectivity;

			public bool UsesNotificationService => false;

			public NotificationServiceFeature NotificationService => null;

			public bool UsesSharing => m_usesSharing;

			public bool UsesTwitter => false;

			public bool UsesWebView => false;
		}

		[Serializable]
		public class iOSSettings
		{
			[Tooltip("The string that identifies your app in iOS App Store.")]
			[SerializeField]
			private string m_storeIdentifier;

			[SerializeField]
			private string m_addressBookUsagePermissionDescription = "$(PRODUCT_NAME) uses contacts";

			[SerializeField]
			private string m_cameraUsagePermissionDescription = "$(PRODUCT_NAME) uses camera";

			[SerializeField]
			private string m_photoAlbumUsagePermissionDescription = "$(PRODUCT_NAME) uses photo library";

			[SerializeField]
			private string m_photoAlbumModifyUsagePermissionDescription = "$(PRODUCT_NAME) saves images to photo library";

			public string StoreIdentifier
			{
				get
				{
					return m_storeIdentifier;
				}
				private set
				{
					m_storeIdentifier = value;
				}
			}

			public string AddressBookUsagePermissionDescription => m_addressBookUsagePermissionDescription;

			public string CameraUsagePermissionDescription => m_cameraUsagePermissionDescription;

			public string PhotoAlbumUsagePermissionDescription => m_photoAlbumUsagePermissionDescription;

			public string PhotoAlbumModifyUsagePermissionDescription => m_photoAlbumModifyUsagePermissionDescription;
		}

		[Tooltip("Select the features that you would like to use.")]
		[SerializeField]
		private Features m_supportedFeatures = new Features();

		[SerializeField]
		private iOSSettings m_iOS = new iOSSettings();

		[SerializeField]
		private AndroidSettings m_android = new AndroidSettings();

		[SerializeField]
		[Tooltip("Select the Addon services that you would like to use.")]
		private AddonServices m_supportedAddonServices = new AddonServices();

		internal bool IsDebugMode => UnityEngine.Debug.isDebugBuild;

		internal iOSSettings IOS => m_iOS;

		internal AndroidSettings Android => m_android;

		internal Features SupportedFeatures => m_supportedFeatures;

		internal AddonServices SupportedAddonServices => m_supportedAddonServices;

		public string StoreIdentifier => m_android.StoreIdentifier;
	}
}
