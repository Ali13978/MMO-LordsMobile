using System.Collections;
using System.Text;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public class POSTRequest : WebRequest
	{
		public POSTRequest(URL _URL, object _params, bool _isAsynchronous)
			: base(_URL, _params, _isAsynchronous)
		{
			base.WWWObject = CreateWWWObject();
		}

		protected override WWW CreateWWWObject()
		{
			string uRLString = base.URL.URLString;
			if (base.Parameters == null)
			{
				UnityEngine.Debug.LogWarning("[POSTRequest] Post data is missing");
				return new WWW(uRLString);
			}
			IDictionary dictionary = base.Parameters as IDictionary;
			if (dictionary == null)
			{
				UnityEngine.Debug.LogError("[POSTRequest] Invalid parameter");
				return null;
			}
			string s = dictionary.ToJSON();
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			return new WWW(uRLString, bytes);
		}

		public static POSTRequest CreateRequest(URL _URL, object _params)
		{
			return new POSTRequest(_URL, _params, _isAsynchronous: false);
		}

		public static POSTRequest CreateAsyncRequest(URL _URL, object _params)
		{
			return new POSTRequest(_URL, _params, _isAsynchronous: false);
		}
	}
}
