using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.AssetStoreProductUtility.Internal
{
	internal struct ProductUpdateInfo
	{
		private const string kVersionNumberKey = "version_number";

		private const string kDownloadLinkKey = "download_link";

		private const string kAssetStoreLink = "asset_store_link";

		private const string kReleaseNoteKey = "release_notes";

		internal bool NewUpdateAvailable
		{
			get;
			private set;
		}

		internal string VersionNumber
		{
			get;
			private set;
		}

		internal string DownloadLink
		{
			get;
			private set;
		}

		internal string AssetStoreLink
		{
			get;
			private set;
		}

		internal string ReleaseNote
		{
			get;
			private set;
		}

		internal ProductUpdateInfo(bool _newUpdateAvailable)
		{
			NewUpdateAvailable = _newUpdateAvailable;
			VersionNumber = null;
			DownloadLink = null;
			AssetStoreLink = null;
			ReleaseNote = null;
		}

		internal ProductUpdateInfo(string _currentVersion, IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("version_number");
			string ifAvailable2 = _dataDict.GetIfAvailable<string>("download_link");
			string ifAvailable3 = _dataDict.GetIfAvailable<string>("asset_store_link");
			string ifAvailable4 = _dataDict.GetIfAvailable<string>("release_notes");
			NewUpdateAvailable = (ifAvailable.CompareTo(_currentVersion) > 0);
			VersionNumber = ifAvailable;
			DownloadLink = ifAvailable2;
			AssetStoreLink = ifAvailable3;
			ReleaseNote = ifAvailable4;
		}
	}
}
