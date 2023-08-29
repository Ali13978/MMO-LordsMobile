using BackendlessAPI.LitJson;
using System;

namespace BackendlessAPI.Persistence
{
	public abstract class BackendlessEntity
	{
		public string ___class;

		public string __meta;

		[JsonProperty("objectId")]
		public string ObjectId
		{
			get;
			set;
		}

		[JsonProperty("created")]
		public DateTime? Created
		{
			get;
			set;
		}

		[JsonProperty("updated")]
		public DateTime? Updated
		{
			get;
			set;
		}

		public BackendlessEntity()
		{
			___class = GetType().Name;
		}
	}
}
