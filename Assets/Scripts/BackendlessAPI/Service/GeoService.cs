using BackendlessAPI.Async;
using BackendlessAPI.Data;
using BackendlessAPI.Engine;
using BackendlessAPI.Exception;
using BackendlessAPI.Geo;
using BackendlessAPI.LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BackendlessAPI.Service
{
	public class GeoService
	{
		private static string DEFAULT_CATEGORY_NAME = "Default";

		private static string RESULT = "result";

		private static string GEOPOINT = "geopoint";

		private static string COLLECTION = "collection";

		public GeoCategory AddCategory(string categoryName)
		{
			CheckCategoryName(categoryName);
			return Invoker.InvokeSync<GeoCategory>(Invoker.Api.GEOSERVICE_ADDCATEGORY, new object[2]
			{
				null,
				categoryName
			});
		}

		public void AddCategory(string categoryName, AsyncCallback<GeoCategory> callback)
		{
			try
			{
				CheckCategoryName(categoryName);
				Invoker.InvokeAsync(Invoker.Api.GEOSERVICE_ADDCATEGORY, new object[2]
				{
					null,
					categoryName
				}, callback);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex));
			}
		}

		public bool DeleteCategory(string categoryName)
		{
			bool result = false;
			CheckCategoryName(categoryName);
			Dictionary<string, object> dictionary = Invoker.InvokeSync<Dictionary<string, object>>(Invoker.Api.GEOSERVICE_DELETECATEGORY, new object[2]
			{
				null,
				categoryName
			});
			try
			{
				result = (bool)dictionary[RESULT];
				return result;
			}
			catch (System.Exception)
			{
				return result;
			}
		}

		public void DeleteCategory(string categoryName, AsyncCallback<bool> callback)
		{
			try
			{
				CheckCategoryName(categoryName);
				AsyncCallback<Dictionary<string, object>> callback2 = new AsyncCallback<Dictionary<string, object>>(delegate(Dictionary<string, object> r)
				{
					bool response = false;
					try
					{
						response = (bool)r[RESULT];
					}
					catch (System.Exception)
					{
					}
					if (callback != null)
					{
						callback.ResponseHandler(response);
					}
				}, delegate(BackendlessFault f)
				{
					if (callback != null)
					{
						callback.ErrorHandler(f);
						return;
					}
					throw new BackendlessException(f);
				});
				Invoker.InvokeAsync(Invoker.Api.GEOSERVICE_DELETECATEGORY, new object[2]
				{
					null,
					categoryName
				}, callback2);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex));
			}
		}

		public GeoPoint SavePoint(double latitude, double longitude, Dictionary<string, string> metadata)
		{
			return SavePoint(latitude, longitude, null, metadata);
		}

		public void SavePoint(double latitude, double longitude, Dictionary<string, string> metadata, AsyncCallback<GeoPoint> callback)
		{
			SavePoint(latitude, longitude, null, metadata, callback);
		}

		public GeoPoint SavePoint(double latitude, double longitude, Dictionary<string, object> metadata)
		{
			return SavePoint(latitude, longitude, null, metadata);
		}

		public void SavePoint(double latitude, double longitude, Dictionary<string, object> metadata, AsyncCallback<GeoPoint> callback)
		{
			SavePoint(latitude, longitude, null, metadata, callback);
		}

		public GeoPoint SavePoint(double latitude, double longitude, List<string> categoryNames, Dictionary<string, string> metadata)
		{
			return SavePoint(new GeoPoint(latitude, longitude, categoryNames, metadata));
		}

		public GeoPoint SavePoint(double latitude, double longitude, List<string> categoryNames, Dictionary<string, object> metadata)
		{
			return SavePoint(new GeoPoint(latitude, longitude, categoryNames, metadata));
		}

		public void SavePoint(double latitude, double longitude, List<string> categoryNames, Dictionary<string, object> metadata, AsyncCallback<GeoPoint> callback)
		{
			SavePoint(new GeoPoint(latitude, longitude, categoryNames, metadata), callback);
		}

		public void SavePoint(double latitude, double longitude, List<string> categoryNames, Dictionary<string, string> metadata, AsyncCallback<GeoPoint> callback)
		{
			SavePoint(new GeoPoint(latitude, longitude, categoryNames, metadata), callback);
		}

		public GeoPoint AddPoint(GeoPoint geoPoint)
		{
			return SavePoint(geoPoint);
		}

		public GeoPoint SavePoint(GeoPoint geoPoint)
		{
			if (geoPoint == null)
			{
				throw new ArgumentNullException("Geopoint cannot be null.");
			}
			CheckCoordinates(geoPoint.Latitude, geoPoint.Longitude);
			GeoPoint result = null;
			Dictionary<string, GeoPoint> dictionary = Invoker.InvokeSync<Dictionary<string, GeoPoint>>(Invoker.Api.GEOSERVICE_SAVEPOINT, new object[2]
			{
				null,
				GetSavePointQuery(geoPoint)
			});
			if (dictionary != null && dictionary.ContainsKey(GEOPOINT))
			{
				result = dictionary[GEOPOINT];
			}
			return result;
		}

		public void AddPoint(GeoPoint geoPoint, AsyncCallback<GeoPoint> callback)
		{
			SavePoint(geoPoint, callback);
		}

		public void SavePoint(GeoPoint geoPoint, AsyncCallback<GeoPoint> callback)
		{
			try
			{
				if (geoPoint == null)
				{
					throw new ArgumentNullException("Geopoint cannot be null.");
				}
				CheckCoordinates(geoPoint.Latitude, geoPoint.Longitude);
				AsyncCallback<Dictionary<string, GeoPoint>> callback2 = new AsyncCallback<Dictionary<string, GeoPoint>>(delegate(Dictionary<string, GeoPoint> r)
				{
					GeoPoint response = null;
					if (r != null && r.ContainsKey(GEOPOINT))
					{
						response = r[GEOPOINT];
					}
					if (callback != null)
					{
						callback.ResponseHandler(response);
					}
				}, delegate(BackendlessFault f)
				{
					if (callback != null)
					{
						callback.ErrorHandler(f);
						return;
					}
					throw new BackendlessException(f);
				});
				Invoker.Api api = Invoker.Api.GEOSERVICE_SAVEPOINT;
				if (!string.IsNullOrEmpty(geoPoint.ObjectId))
				{
					api = Invoker.Api.GEOSERVICE_UPDATEPOINT;
				}
				Invoker.InvokeAsync(api, new object[2]
				{
					null,
					GetSavePointQuery(geoPoint)
				}, callback2);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex));
			}
		}

		public BackendlessCollection<GeoPoint> GetPoints(BackendlessGeoQuery geoQuery)
		{
			checkGeoQuery(geoQuery);
			Invoker.Api api = Invoker.Api.UNKNOWN;
			string getPointsQuery = GetGetPointsQuery(geoQuery, out api);
			BackendlessCollection<GeoPoint> backendlessCollection = null;
			Dictionary<string, BackendlessCollection<GeoPoint>> dictionary = Invoker.InvokeSync<Dictionary<string, BackendlessCollection<GeoPoint>>>(api, new object[2]
			{
				null,
				getPointsQuery
			});
			if (dictionary != null && dictionary.ContainsKey(COLLECTION))
			{
				backendlessCollection = dictionary[COLLECTION];
				backendlessCollection.Query = geoQuery;
			}
			return backendlessCollection;
		}

		public void GetPoints(BackendlessGeoQuery geoQuery, AsyncCallback<BackendlessCollection<GeoPoint>> callback)
		{
			try
			{
				checkGeoQuery(geoQuery);
				Invoker.Api api = Invoker.Api.UNKNOWN;
				string getPointsQuery = GetGetPointsQuery(geoQuery, out api);
				AsyncCallback<Dictionary<string, BackendlessCollection<GeoPoint>>> callback2 = new AsyncCallback<Dictionary<string, BackendlessCollection<GeoPoint>>>(delegate(Dictionary<string, BackendlessCollection<GeoPoint>> r)
				{
					BackendlessCollection<GeoPoint> backendlessCollection = null;
					if (r != null && r.ContainsKey(COLLECTION))
					{
						backendlessCollection = r[COLLECTION];
						backendlessCollection.Query = geoQuery;
					}
					if (callback != null)
					{
						callback.ResponseHandler(backendlessCollection);
					}
				}, delegate(BackendlessFault f)
				{
					if (callback != null)
					{
						callback.ErrorHandler(f);
						return;
					}
					throw new BackendlessException(f);
				});
				Invoker.InvokeAsync(api, new object[2]
				{
					null,
					getPointsQuery
				}, callback2);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex));
			}
		}

		public BackendlessCollection<SearchMatchesResult> RelativeFind(BackendlessGeoQuery geoQuery)
		{
			if (geoQuery == null)
			{
				throw new ArgumentNullException("Geo query should not be null");
			}
			if (geoQuery.RelativeFindMetadata.Count == 0 || geoQuery.RelativeFindPercentThreshold == 0.0)
			{
				throw new ArgumentException("Geo query should contain relative metadata and a threshold for a relative search");
			}
			Invoker.Api api = Invoker.Api.UNKNOWN;
			string getPointsQuery = GetGetPointsQuery(geoQuery, out api);
			BackendlessCollection<SearchMatchesResult> backendlessCollection = null;
			Dictionary<string, BackendlessCollection<SearchMatchesResult>> dictionary = Invoker.InvokeSync<Dictionary<string, BackendlessCollection<SearchMatchesResult>>>(Invoker.Api.GEOSERVICE_RELATIVEFIND, new object[2]
			{
				null,
				getPointsQuery
			});
			if (dictionary != null && dictionary.ContainsKey(COLLECTION))
			{
				backendlessCollection = dictionary[COLLECTION];
				backendlessCollection.Query = geoQuery;
			}
			return backendlessCollection;
		}

		public void RelativeFind(BackendlessGeoQuery geoQuery, AsyncCallback<BackendlessCollection<SearchMatchesResult>> callback)
		{
			try
			{
				if (geoQuery == null)
				{
					throw new ArgumentNullException("Geo query should not be null");
				}
				if (geoQuery.RelativeFindMetadata.Count == 0 || geoQuery.RelativeFindPercentThreshold == 0.0)
				{
					throw new ArgumentException("Geo query should contain relative metadata and a threshold for a relative search");
				}
				Invoker.Api api = Invoker.Api.UNKNOWN;
				string getPointsQuery = GetGetPointsQuery(geoQuery, out api);
				AsyncCallback<Dictionary<string, BackendlessCollection<SearchMatchesResult>>> callback2 = new AsyncCallback<Dictionary<string, BackendlessCollection<SearchMatchesResult>>>(delegate(Dictionary<string, BackendlessCollection<SearchMatchesResult>> r)
				{
					BackendlessCollection<SearchMatchesResult> backendlessCollection = null;
					if (r != null && r.ContainsKey(COLLECTION))
					{
						backendlessCollection = r[COLLECTION];
						backendlessCollection.Query = geoQuery;
					}
					if (callback != null)
					{
						callback.ResponseHandler(backendlessCollection);
					}
				}, delegate(BackendlessFault f)
				{
					if (callback != null)
					{
						callback.ErrorHandler(f);
						return;
					}
					throw new BackendlessException(f);
				});
				Invoker.InvokeAsync(Invoker.Api.GEOSERVICE_RELATIVEFIND, new object[2]
				{
					null,
					getPointsQuery
				}, callback2);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex));
			}
		}

		public List<GeoCategory> GetCategories()
		{
			return Invoker.InvokeSync<List<GeoCategory>>(Invoker.Api.GEOSERVICE_GETCATEGORIES, new object[1]);
		}

		public void GetCategories(AsyncCallback<List<GeoCategory>> callback)
		{
			try
			{
				Invoker.InvokeAsync(Invoker.Api.GEOSERVICE_GETCATEGORIES, new object[1], callback);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex));
			}
		}

		private void CheckCoordinates(double? latitude, double? longitude)
		{
			if ((latitude.HasValue && latitude.Value > 90.0) || (latitude.HasValue && latitude.Value < -90.0))
			{
				throw new ArgumentException("Latitude value should be between -90 and 90.");
			}
			if ((longitude.HasValue && longitude.Value > 180.0) || (latitude.HasValue && latitude.Value < -180.0))
			{
				throw new ArgumentException("Longitude value should be between -180 and 180.");
			}
		}

		private void CheckCategoryName(string categoryName)
		{
			if (string.IsNullOrEmpty(categoryName))
			{
				throw new ArgumentNullException("Category name cannot be null or empty.");
			}
			if (categoryName.Equals(DEFAULT_CATEGORY_NAME))
			{
				throw new ArgumentException("cannot add or delete a default category name.");
			}
		}

		private void checkGeoQuery(BackendlessGeoQuery geoQuery)
		{
			if (geoQuery == null)
			{
				throw new ArgumentNullException("Geo query should not be null");
			}
			if (geoQuery.SearchRectangle != null)
			{
				if (geoQuery.SearchRectangle.Length != 4)
				{
					throw new ArgumentException("Wrong rectangle search query. It should contain four points.");
				}
				if (!double.IsNaN(geoQuery.Radius))
				{
					throw new ArgumentException("Inconsistent geo query. Query should not contain both rectangle and radius search parameters.");
				}
				if (!double.IsNaN(geoQuery.Latitude))
				{
					throw new ArgumentException("Inconsistent geo query. Query should not contain both rectangle and radius search parameters.");
				}
				if (!double.IsNaN(geoQuery.Longitude))
				{
					throw new ArgumentException("Inconsistent geo query. Query should not contain both rectangle and radius search parameters.");
				}
			}
			else if (!double.IsNaN(geoQuery.Radius))
			{
				if (geoQuery.Radius <= 0.0)
				{
					throw new ArgumentException("Wrong radius value.");
				}
				if (double.IsNaN(geoQuery.Latitude))
				{
					throw new ArgumentNullException("Latitude value should be between -90 and 90.");
				}
				if (double.IsNaN(geoQuery.Longitude))
				{
					throw new ArgumentNullException("Longitude value should be between -180 and 180.");
				}
				CheckCoordinates(geoQuery.Latitude, geoQuery.Longitude);
				if (!geoQuery.Units.HasValue)
				{
					throw new ArgumentNullException("Unit type cannot be null or empty.");
				}
			}
			else if (geoQuery.Categories == null && geoQuery.Metadata == null)
			{
				throw new ArgumentNullException("Could not understand Geo query options. Specify any.");
			}
			if (geoQuery.Categories != null)
			{
				foreach (string category in geoQuery.Categories)
				{
					CheckCategoryName(category);
				}
			}
			if (geoQuery.Offset < 0)
			{
				throw new ArgumentException("Offset cannot have a negative value.");
			}
			if (geoQuery.PageSize < 0)
			{
				throw new ArgumentException("Pagesize cannot have a negative value.");
			}
		}

		private static string GetSavePointQuery(GeoPoint geoPoint)
		{
			string text = null;
			if (geoPoint != null)
			{
				text = string.Empty;
				string objectId = geoPoint.ObjectId;
				if (!string.IsNullOrEmpty(objectId))
				{
					text = text + "/" + objectId;
				}
				text = text + "?lat=" + geoPoint.Latitude;
				text = text + "&lon=" + geoPoint.Longitude;
				List<string> categories = geoPoint.Categories;
				if (categories != null && categories.Count > 0)
				{
					string text2 = string.Empty;
					foreach (string item in categories)
					{
						if (!string.IsNullOrEmpty(text2))
						{
							text2 += ",";
						}
						text2 = ((item != null) ? (text2 + item) : (text2 + "null"));
					}
					if (!string.IsNullOrEmpty(text2))
					{
						text = text + "&categories=" + text2;
					}
				}
				Dictionary<string, object> metadata = geoPoint.Metadata;
				if (metadata != null && metadata.Count > 0)
				{
					string text3 = JsonMapper.ToJson(metadata);
					if (!string.IsNullOrEmpty(text3))
					{
						text = text + "&metadata=" + WWW.EscapeURL(text3);
					}
				}
			}
			return text;
		}

		private static void AddQuery(ref string query, string addQuery)
		{
			if (string.IsNullOrEmpty(query))
			{
				query = "?";
			}
			else
			{
				query += "&";
			}
			query += addQuery;
		}

		private static string GetGetPointsQuery(BackendlessGeoQuery geoQuery, out Invoker.Api api)
		{
			string query = null;
			if (geoQuery != null)
			{
				double[] searchRectangle = geoQuery.SearchRectangle;
				if (searchRectangle != null)
				{
					api = Invoker.Api.GEOSERVICE_GETRECT;
					if (searchRectangle.Length == 4)
					{
						AddQuery(ref query, "nwlat=" + searchRectangle[0]);
						AddQuery(ref query, "nwlon=" + searchRectangle[1]);
						AddQuery(ref query, "selat=" + searchRectangle[2]);
						AddQuery(ref query, "selon=" + searchRectangle[3]);
					}
				}
				else
				{
					api = Invoker.Api.GEOSERVICE_GETPOINTS;
					Dictionary<string, string> relativeFindMetadata = geoQuery.RelativeFindMetadata;
					if (relativeFindMetadata != null && relativeFindMetadata.Count > 0)
					{
						api = Invoker.Api.GEOSERVICE_RELATIVEFIND;
						string text = JsonMapper.ToJson(relativeFindMetadata);
						if (!string.IsNullOrEmpty(text))
						{
							AddQuery(ref query, "relativeFindMetadata=" + WWW.EscapeURL(text));
						}
						AddQuery(ref query, "relativeFindPercentThreshold=" + geoQuery.RelativeFindPercentThreshold);
					}
					if (!double.NaN.Equals(geoQuery.Latitude))
					{
						AddQuery(ref query, "lat=" + geoQuery.Latitude);
					}
					if (!double.NaN.Equals(geoQuery.Longitude))
					{
						AddQuery(ref query, "lon=" + geoQuery.Longitude);
					}
					if (!double.NaN.Equals(geoQuery.Radius))
					{
						AddQuery(ref query, "r=" + geoQuery.Radius);
					}
					Units? units = geoQuery.Units;
					if (units.HasValue)
					{
						AddQuery(ref query, "units=" + units.ToString());
					}
				}
				List<string> categories = geoQuery.Categories;
				if (categories != null && categories.Count > 0)
				{
					string text2 = string.Empty;
					foreach (string item in categories)
					{
						if (!string.IsNullOrEmpty(text2))
						{
							text2 += ",";
						}
						text2 += item;
					}
					if (!string.IsNullOrEmpty(text2))
					{
						AddQuery(ref query, "categories=" + text2);
					}
				}
				Dictionary<string, string> metadata = geoQuery.Metadata;
				if (metadata != null && metadata.Count > 0)
				{
					string text3 = JsonMapper.ToJson(metadata);
					if (!string.IsNullOrEmpty(text3))
					{
						AddQuery(ref query, "metadata=" + WWW.EscapeURL(text3));
					}
				}
				AddQuery(ref query, "includemetadata=" + geoQuery.IncludeMeta.ToString().ToLower());
				AddQuery(ref query, "pagesize=" + geoQuery.PageSize);
				AddQuery(ref query, "offset=" + geoQuery.Offset);
			}
			else
			{
				api = Invoker.Api.UNKNOWN;
			}
			return query;
		}
	}
}
