using BackendlessAPI.Data;
using System.Collections.Generic;

namespace BackendlessAPI.Persistence
{
	public class BackendlessDataQuery : IBackendlessQuery
	{
		public string WhereClause
		{
			get;
			set;
		}

		public QueryOptions QueryOptions
		{
			get;
			set;
		}

		public List<string> Properties
		{
			get;
			set;
		}

		public int PageSize
		{
			get
			{
				return (QueryOptions != null) ? QueryOptions.PageSize : 10;
			}
			set
			{
				if (QueryOptions == null)
				{
					QueryOptions = new QueryOptions();
				}
				QueryOptions.PageSize = value;
			}
		}

		public int Offset
		{
			get
			{
				return (QueryOptions != null) ? QueryOptions.Offset : 0;
			}
			set
			{
				if (QueryOptions == null)
				{
					QueryOptions = new QueryOptions();
				}
				QueryOptions.Offset = value;
			}
		}

		public BackendlessDataQuery()
		{
		}

		public BackendlessDataQuery(List<string> properties)
		{
			Properties = properties;
		}

		public BackendlessDataQuery(string whereClause)
		{
			WhereClause = whereClause;
		}

		public BackendlessDataQuery(QueryOptions queryOptions)
		{
			QueryOptions = queryOptions;
		}

		public BackendlessDataQuery(List<string> properties, string whereClause, QueryOptions queryOptions)
		{
			Properties = properties;
			WhereClause = whereClause;
			QueryOptions = queryOptions;
		}

		public IBackendlessQuery NewInstance()
		{
			BackendlessDataQuery backendlessDataQuery = new BackendlessDataQuery();
			backendlessDataQuery.Properties = Properties;
			backendlessDataQuery.WhereClause = WhereClause;
			backendlessDataQuery.QueryOptions = QueryOptions;
			return backendlessDataQuery;
		}
	}
}
