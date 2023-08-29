using BackendlessAPI.LitJson;

namespace BackendlessAPI.Geo
{
	public class GeoCategory
	{
		[JsonProperty("objectId")]
		public string Id
		{
			get;
			set;
		}

		[JsonProperty("name")]
		public string Name
		{
			get;
			set;
		}

		[JsonProperty("size")]
		public int Size
		{
			get;
			set;
		}
	}
}
