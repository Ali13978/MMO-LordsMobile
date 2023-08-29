using System.Collections;
using System.Collections.Generic;

namespace VoxelBusters.Utility
{
	public struct WebResponse
	{
		public int Status
		{
			get;
			private set;
		}

		public string Message
		{
			get;
			private set;
		}

		public IDictionary Data
		{
			get;
			private set;
		}

		public List<string> Errors
		{
			get;
			private set;
		}

		public static WebResponse WebResponseOnSuccess(IDictionary _jsonResponse)
		{
			WebResponse result = default(WebResponse);
			result.Status = _jsonResponse.GetIfAvailable<int>("status");
			if (_jsonResponse.Contains("response"))
			{
				IDictionary dictionary = _jsonResponse["response"] as IDictionary;
				if (dictionary.Contains("data"))
				{
					result.Data = (dictionary["data"] as IDictionary);
					result.Message = dictionary.GetIfAvailable<string>("message");
					result.Errors = dictionary.GetIfAvailable<List<string>>("errors");
				}
			}
			return result;
		}

		public static WebResponse WebResponseOnFail(IDictionary _jsonResponse)
		{
			WebResponse result = default(WebResponse);
			result.Status = 0;
			result.Message = null;
			result.Data = null;
			result.Errors = new List<string>();
			result.Errors.Add(_jsonResponse["error"] as string);
			return result;
		}
	}
}
