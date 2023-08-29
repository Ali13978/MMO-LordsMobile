namespace BackendlessAPI.Data
{
	public interface IBackendlessQuery
	{
		int Offset
		{
			get;
			set;
		}

		int PageSize
		{
			get;
			set;
		}

		IBackendlessQuery NewInstance();
	}
}
