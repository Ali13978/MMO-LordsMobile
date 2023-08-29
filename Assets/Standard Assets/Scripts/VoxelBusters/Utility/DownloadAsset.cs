using UnityEngine;

namespace VoxelBusters.Utility
{
	public class DownloadAsset : Request
	{
		public delegate void Completion(WWW _www, string _error);

		public Completion OnCompletion
		{
			get;
			set;
		}

		public DownloadAsset(URL _URL, bool _isAsynchronous)
			: base(_URL, _isAsynchronous)
		{
			base.WWWObject = new WWW(_URL.URLString);
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
					OnCompletion(base.WWWObject, null);
				}
			}
			else if (OnCompletion != null)
			{
				OnCompletion(null, base.WWWObject.error);
			}
		}
	}
}
