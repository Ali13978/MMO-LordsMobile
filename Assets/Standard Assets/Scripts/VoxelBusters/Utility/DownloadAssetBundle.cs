using UnityEngine;

namespace VoxelBusters.Utility
{
	public class DownloadAssetBundle : Request
	{
		public delegate void Completion(AssetBundle _assetBundle, string _error);

		public Completion OnCompletion
		{
			get;
			set;
		}

		public DownloadAssetBundle(URL _URL, int _version, bool _isAsynchronous)
			: base(_URL, _isAsynchronous)
		{
			base.WWWObject = WWW.LoadFromCacheOrDownload(_URL.URLString, _version);
		}

		protected override void DidFailStartRequestWithError(string _error)
		{
			if (OnCompletion != null)
			{
				OnCompletion(null, _error);
			}
		}

		protected override void OnFetchingResponse()
		{
			if (string.IsNullOrEmpty(base.WWWObject.error))
			{
				if (OnCompletion != null)
				{
					OnCompletion(base.WWWObject.assetBundle, null);
				}
			}
			else if (OnCompletion != null)
			{
				OnCompletion(null, base.WWWObject.error);
			}
		}
	}
}
