using System.Collections;
using System.Text;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public class GETRequest : WebRequest
	{
		public GETRequest(URL _URL, object _params, bool _isAsynchronous)
			: base(_URL, _params, _isAsynchronous)
		{
			base.WWWObject = CreateWWWObject();
		}

		protected override WWW CreateWWWObject()
		{
			IDictionary dictionary = null;
			string uRLString = base.URL.URLString;
			if (base.Parameters == null)
			{
				return new WWW(uRLString);
			}
			if (base.Parameters.GetType() == typeof(string))
			{
				return new WWW(uRLString + "/" + (base.Parameters as string));
			}
			if ((dictionary = (base.Parameters as IDictionary)) != null)
			{
				if (dictionary.Count == 0)
				{
					return new WWW(uRLString);
				}
				StringBuilder stringBuilder = new StringBuilder(uRLString, uRLString.Length);
				int _paramAdded = 0;
				AppendParameters(null, dictionary, stringBuilder, ref _paramAdded);
				return new WWW(stringBuilder.ToString());
			}
			UnityEngine.Debug.LogError("[GETRequest] Invalid parameter");
			return null;
		}

		private void AppendParameters(string _key, object _value, StringBuilder _urlBuilder, ref int _paramAdded)
		{
			if (_value is IDictionary)
			{
				IDictionaryEnumerator enumerator = (_value as IDictionary).GetEnumerator();
				if (string.IsNullOrEmpty(_key))
				{
					while (enumerator.MoveNext())
					{
						object key = enumerator.Key;
						object value = enumerator.Value;
						AppendParameters(key.ToString(), value, _urlBuilder, ref _paramAdded);
					}
				}
				else
				{
					while (enumerator.MoveNext())
					{
						object key2 = enumerator.Key;
						object value2 = enumerator.Value;
						AppendParameters($"{_key}[{key2}]", value2, _urlBuilder, ref _paramAdded);
					}
				}
			}
			else if (_value is IEnumerable && _value.GetType() != typeof(string))
			{
				string key3 = _key + "[]";
				foreach (object item in _value as IEnumerable)
				{
					AppendParameters(key3, item, _urlBuilder, ref _paramAdded);
				}
			}
			else
			{
				if (_paramAdded == 0)
				{
					_urlBuilder.AppendFormat("?{0}={1}", _key, _value);
				}
				else
				{
					_urlBuilder.AppendFormat("&{0}={1}", _key, _value);
				}
				_paramAdded++;
			}
		}

		public static GETRequest CreateRequest(URL _URL, object _params)
		{
			return new GETRequest(_URL, _params, _isAsynchronous: false);
		}

		public static GETRequest CreateAsyncRequest(URL _URL, object _params)
		{
			return new GETRequest(_URL, _params, _isAsynchronous: true);
		}
	}
}
