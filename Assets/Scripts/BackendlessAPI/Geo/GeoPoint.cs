using BackendlessAPI.LitJson;
using System.Collections.Generic;

namespace BackendlessAPI.Geo
{
	public class GeoPoint
	{
		private List<string> _categories;

		private Dictionary<string, object> _metadata;

		[JsonProperty("objectId")]
		public string ObjectId
		{
			get;
			set;
		}

		[JsonProperty("latitude")]
		public double Latitude
		{
			get;
			set;
		}

		[JsonProperty("longitude")]
		public double Longitude
		{
			get;
			set;
		}

		[JsonProperty("distance")]
		public double Distance
		{
			get;
			set;
		}

		[JsonProperty("categories")]
		public List<string> Categories
		{
			get
			{
				return _categories ?? (_categories = new List<string>());
			}
			set
			{
				_categories = value;
			}
		}

		[JsonProperty("metadata")]
		public Dictionary<string, object> Metadata
		{
			get
			{
				return _metadata ?? (_metadata = new Dictionary<string, object>());
			}
			set
			{
				_metadata = value;
			}
		}

		public GeoPoint()
		{
		}

		public GeoPoint(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public GeoPoint(double latitude, double longitude, List<string> categories, Dictionary<string, string> metadata)
		{
			Latitude = latitude;
			Longitude = longitude;
			Categories = categories;
			foreach (KeyValuePair<string, string> metadatum in metadata)
			{
				Metadata.Add(metadatum.Key, metadatum.Value);
			}
		}

		public GeoPoint(double latitude, double longitude, List<string> categories, Dictionary<string, object> metadata)
		{
			Latitude = latitude;
			Longitude = longitude;
			Categories = categories;
			Metadata = metadata;
		}
	}
}
