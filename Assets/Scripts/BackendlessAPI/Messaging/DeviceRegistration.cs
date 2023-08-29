using BackendlessAPI.LitJson;
using System;
using System.Collections.Generic;

namespace BackendlessAPI.Messaging
{
	public class DeviceRegistration
	{
		private DateTime? expiration;

		private long? timestamp;

		[JsonProperty("id")]
		public string Id
		{
			get;
			set;
		}

		[JsonProperty("channels")]
		public List<string> Channels
		{
			get;
			set;
		}

		public DateTime? Expiration
		{
			get
			{
				return expiration;
			}
			set
			{
				expiration = value;
				DateTime? dateTime = expiration;
				if (!dateTime.HasValue)
				{
					timestamp = 0L;
					return;
				}
				DateTime? dateTime2 = expiration;
				timestamp = Convert.ToInt64((dateTime2.Value - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);
			}
		}

		[JsonProperty("expiration")]
		public long? Timestamp
		{
			get
			{
				return timestamp;
			}
			set
			{
				timestamp = value;
				DateTime? dateTime = expiration;
				if (!dateTime.HasValue)
				{
					expiration = null;
					return;
				}
				double value2 = value.Value;
				expiration = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(value2);
			}
		}

		[JsonProperty("os")]
		public string Os
		{
			get;
			set;
		}

		[JsonProperty("osVersion")]
		public string OsVersion
		{
			get;
			set;
		}

		[JsonProperty("deviceToken")]
		public string DeviceToken
		{
			get;
			set;
		}

		[JsonProperty("registrationId")]
		public string RegistrationId
		{
			get;
			set;
		}

		[JsonProperty("deviceId")]
		public string DeviceId
		{
			get;
			set;
		}

		public void AddChannel(string channel)
		{
			if (Channels == null)
			{
				Channels = new List<string>();
			}
			Channels.Add(channel);
		}

		public void ClearRegistration()
		{
			Id = null;
			Channels = null;
			RegistrationId = null;
			DeviceToken = null;
		}
	}
}
