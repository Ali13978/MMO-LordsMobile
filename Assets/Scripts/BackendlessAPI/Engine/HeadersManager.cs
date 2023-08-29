using System.Collections.Generic;

namespace BackendlessAPI.Engine
{
	internal class HeadersManager
	{
		private Dictionary<string, string> headers = new Dictionary<string, string>();

		private static object headersLock = new object();

		private static volatile HeadersManager _instance = null;

		public Dictionary<string, string> Headers
		{
			get
			{
				return headers;
			}
			set
			{
				lock (headersLock)
				{
					foreach (KeyValuePair<string, string> header in headers)
					{
						headers.Add(header.Key, header.Value);
					}
				}
			}
		}

		private HeadersManager()
		{
		}

		public static HeadersManager GetInstance()
		{
			if (_instance == null)
			{
				lock (headersLock)
				{
					if (_instance == null)
					{
						_instance = new HeadersManager();
						_instance.AddHeader(HeadersEnum.APP_ID_NAME, Backendless.AppId);
						_instance.AddHeader(HeadersEnum.SECRET_KEY_NAME, Backendless.SecretKey);
						_instance.AddHeader(HeadersEnum.APP_TYPE_NAME, "REST");
					}
				}
			}
			return _instance;
		}

		public void AddHeader(HeadersEnum headersEnum, string value)
		{
			lock (headersLock)
			{
				headers.Remove(headersEnum.Header);
				headers.Add(headersEnum.Header, value);
			}
		}

		public void RemoveHeader(HeadersEnum headersEnum)
		{
			lock (headersLock)
			{
				headers.Remove(headersEnum.Header);
			}
		}

		public static void CleanHeaders()
		{
			lock (typeof(HeadersManager))
			{
				_instance = null;
			}
		}
	}
}
