using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public abstract class WebRequest : Request
	{
		public delegate void JSONResponse(IDictionary _response);

		public object Parameters
		{
			get;
			set;
		}

		public JSONResponse OnSuccess
		{
			get;
			set;
		}

		public JSONResponse OnFailure
		{
			get;
			set;
		}

		public WebRequest(URL _URL, object _params, bool _isAsynchronous)
			: base(_URL, _isAsynchronous)
		{
			Parameters = _params;
		}

		protected abstract WWW CreateWWWObject();

		protected override void DidFailStartRequestWithError(string _error)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("error", _error);
			IDictionary response = dictionary;
			if (OnFailure != null)
			{
				OnFailure(response);
			}
		}

		protected override void OnFetchingResponse()
		{
			if (string.IsNullOrEmpty(base.WWWObject.error))
			{
				IDictionary response = JSONUtility.FromJSON(base.WWWObject.text) as IDictionary;
				if (OnSuccess != null)
				{
					OnSuccess(response);
				}
				return;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("error", base.WWWObject.error);
			IDictionary response2 = dictionary;
			if (OnFailure != null)
			{
				OnFailure(response2);
			}
		}
	}
}
