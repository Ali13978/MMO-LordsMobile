using System.Collections.Generic;

namespace BackendlessAPI.Persistence
{
	public class QueryOptions
	{
		public int PageSize
		{
			get;
			set;
		}

		public int Offset
		{
			get;
			set;
		}

		public List<string> SortBy
		{
			get;
			set;
		}

		public List<string> Related
		{
			get;
			set;
		}

		public QueryOptions()
		{
			PageSize = 10;
		}

		public QueryOptions(int pageSize, int offset)
		{
			PageSize = pageSize;
			Offset = offset;
		}

		public QueryOptions(int pageSize, int offset, string sortBy)
		{
			PageSize = pageSize;
			Offset = offset;
			SortBy = new List<string>
			{
				sortBy
			};
		}

		public QueryOptions NewInstance()
		{
			QueryOptions queryOptions = new QueryOptions();
			queryOptions.PageSize = PageSize;
			queryOptions.Offset = Offset;
			queryOptions.SortBy = SortBy;
			queryOptions.Related = Related;
			return queryOptions;
		}
	}
}
