using UnityEngine;
using VoxelBusters.AssetStoreProductUtility.Internal;
using VoxelBusters.Utility;

namespace VoxelBusters.AssetStoreProductUtility
{
	public class AssetStoreProduct
	{
		private const string kServerBaseAddress = "https://unity3dplugindashboard.herokuapp.com";

		private const string kProductDetailsPathFormat = "products/{0}/details";

		private const string kCheckUpdatesFailedMessage = "{0} update server could not be connected. Please try again after sometime.";

		private const string kAlreadyUptoDateMessage = "You are using latest version of {0}. Please check back for updates at a later time.";

		private const string kNewVersionAvailableMessage = "Newer version of {0} is available for download.";

		private const string kSkippedVersionPrefix = "version-skipped";

		private ProductUpdateInfo m_productUpdateInfo;

		private GETRequest m_updateGETRequest;

		public string ProductName
		{
			get;
			private set;
		}

		public string ProductVersion
		{
			get;
			private set;
		}

		public Texture2D LogoTexture
		{
			get;
			private set;
		}

		private AssetStoreProduct()
		{
		}

		public AssetStoreProduct(string _pName, string _pVersion, string _logoPath)
		{
			ProductName = _pName;
			ProductVersion = _pVersion;
		}

		~AssetStoreProduct()
		{
			LogoTexture = null;
		}
	}
}
