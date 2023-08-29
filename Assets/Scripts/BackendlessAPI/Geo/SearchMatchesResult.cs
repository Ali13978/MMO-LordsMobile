using BackendlessAPI.LitJson;

namespace BackendlessAPI.Geo
{
	public class SearchMatchesResult
	{
		[JsonProperty("matches")]
		public double Matches
		{
			get;
			set;
		}

		[JsonProperty("geoPoint")]
		public GeoPoint GeoPoint
		{
			get;
			set;
		}
	}
}
