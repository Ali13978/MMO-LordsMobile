using BackendlessAPI.Engine;
using BackendlessAPI.LitJson;
using System.Collections.Generic;

namespace BackendlessAPI
{
	public class BackendlessUser
	{
		public const string PASSWORD_KEY = "password";

		public const string EMAIL_KEY = "email";

		private const string ID_KEY = "objectId";

		private Dictionary<string, object> _properties = new Dictionary<string, object>();

		[JsonProperty("properties")]
		public Dictionary<string, object> Properties
		{
			get
			{
				return _properties;
			}
			set
			{
				_properties = value;
			}
		}

		public string Password
		{
			get
			{
				return (!Properties.ContainsKey("password")) ? null : ((string)Properties["password"]);
			}
			set
			{
				if (Properties.ContainsKey("password"))
				{
					SetProperty("password", value);
				}
				else
				{
					AddProperty("password", value);
				}
			}
		}

		public string Email
		{
			get
			{
				return (!Properties.ContainsKey("email")) ? null : ((string)Properties["email"]);
			}
			set
			{
				if (Properties.ContainsKey("email"))
				{
					SetProperty("email", value);
				}
				else
				{
					AddProperty("email", value);
				}
			}
		}

		public string UserId
		{
			get
			{
				return (!Properties.ContainsKey("objectId")) ? null : ((string)Properties["objectId"]);
			}
			set
			{
				if (Properties.ContainsKey("objectId"))
				{
					SetProperty("objectId", value);
				}
				else
				{
					AddProperty("objectId", value);
				}
			}
		}

		public BackendlessUser()
		{
		}

		internal BackendlessUser(Dictionary<string, object> properties)
		{
			_properties = properties;
		}

		public void PutProperties(Dictionary<string, object> dictionary)
		{
			foreach (KeyValuePair<string, object> item in dictionary)
			{
				if (!item.Key.Equals(HeadersEnum.USER_TOKEN_KEY.ToString()))
				{
					if (Properties.ContainsKey(item.Key))
					{
						SetProperty(item.Key, item.Value);
					}
					else
					{
						AddProperty(item.Key, item.Value);
					}
				}
			}
		}

		public void AddProperty(string key, object value)
		{
			Properties.Add(key, value);
		}

		public void SetProperty(string key, object value)
		{
			Properties[key] = value;
		}

		public object GetProperty(string key)
		{
			return Properties[key];
		}
	}
}
