using BackendlessAPI.Async;
using BackendlessAPI.Exception;
using BackendlessAPI.Geo;
using BackendlessAPI.LitJson;
using BackendlessAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BackendlessAPI.Data
{
	public class BackendlessCollection<T>
	{
		[JsonProperty("totalObjects")]
		public int TotalObjects
		{
			get;
			set;
		}

		[JsonProperty("data")]
		public List<T> Data
		{
			get;
			set;
		}

		public IBackendlessQuery Query
		{
			get;
			set;
		}

		public int PageSize
		{
			get
			{
				return (Query != null) ? Query.PageSize : 0;
			}
			set
			{
				Query.PageSize = value;
			}
		}

		public List<T> GetCurrentPage()
		{
			return Data;
		}

		public BackendlessCollection<T> NextPage()
		{
			int offset = Query.Offset;
			int pageSize = Query.PageSize;
			return GetPage(pageSize, offset + pageSize);
		}

		public BackendlessCollection<T> PreviousPage()
		{
			int offset = Query.Offset;
			int pageSize = Query.PageSize;
			return (offset - pageSize < 0) ? NewInstance() : GetPage(pageSize, offset - pageSize);
		}

		public BackendlessCollection<T> GetPage(int pageSize, int offset)
		{
			return (BackendlessCollection<T>)DownloadPage(pageSize, offset);
		}

		public void NextPage(AsyncCallback<BackendlessCollection<T>> responder)
		{
			int offset = Query.Offset;
			int pageSize = Query.PageSize;
			GetPage(pageSize, offset + pageSize, responder);
		}

		public void PreviousPage(AsyncCallback<BackendlessCollection<T>> responder)
		{
			int offset = Query.Offset;
			int pageSize = Query.PageSize;
			if (offset - pageSize >= 0)
			{
				GetPage(pageSize, offset - pageSize, responder);
			}
			else
			{
				responder.ResponseHandler(NewInstance());
			}
		}

		public void GetPage(int pageSize, int offset, AsyncCallback<BackendlessCollection<T>> responder)
		{
			DownloadPage(pageSize, offset, responder);
		}

		private object DownloadPage(int pageSize, int offset)
		{
			IBackendlessQuery backendlessQuery = Query.NewInstance();
			backendlessQuery.Offset = offset;
			backendlessQuery.PageSize = pageSize;
			if (typeof(T) == typeof(GeoPoint))
			{
				return Backendless.Geo.GetPoints((BackendlessGeoQuery)backendlessQuery);
			}
			return Backendless.Persistence.Find<T>((BackendlessDataQuery)backendlessQuery);
		}

		private void DownloadPage(int pageSize, int offset, AsyncCallback<BackendlessCollection<T>> responder)
		{
			IBackendlessQuery backendlessQuery = Query.NewInstance();
			backendlessQuery.Offset = offset;
			backendlessQuery.PageSize = pageSize;
			ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					responder.ResponseHandler((BackendlessCollection<T>)DownloadPage(pageSize, offset));
				}
				catch (BackendlessException ex)
				{
					responder.ErrorHandler(ex.BackendlessFault);
				}
				catch (System.Exception ex2)
				{
					responder.ErrorHandler(new BackendlessFault(ex2.Message));
				}
			});
		}

		private BackendlessCollection<T> NewInstance()
		{
			BackendlessCollection<T> backendlessCollection = new BackendlessCollection<T>();
			backendlessCollection.Data = Data;
			backendlessCollection.Query = Query;
			backendlessCollection.TotalObjects = TotalObjects;
			return backendlessCollection;
		}
	}
}
