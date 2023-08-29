using BackendlessAPI.LitJson;
using System.Collections.Generic;

namespace BackendlessAPI.Messaging
{
	public class Message
	{
		private Dictionary<string, string> _headers;

		[JsonProperty("messageId")]
		public string MessageId
		{
			get;
			set;
		}

		[JsonProperty("data")]
		public object Data
		{
			get;
			set;
		}

		[JsonProperty("headers")]
		public Dictionary<string, string> Headers
		{
			get
			{
				return _headers ?? (_headers = new Dictionary<string, string>());
			}
			set
			{
				_headers = value;
			}
		}

		[JsonProperty("publisherId")]
		public string PublisherId
		{
			get;
			set;
		}

		[JsonProperty("timestamp")]
		public long Timestamp
		{
			get;
			set;
		}
	}
}
