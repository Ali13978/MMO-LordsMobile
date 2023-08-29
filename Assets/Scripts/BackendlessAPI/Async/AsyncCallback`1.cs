namespace BackendlessAPI.Async
{
	public class AsyncCallback<T>
	{
		internal ErrorHandler ErrorHandler;

		internal ResponseHandler<T> ResponseHandler;

		public AsyncCallback(ResponseHandler<T> responseHandler, ErrorHandler errorHandler)
		{
			ResponseHandler = responseHandler;
			ErrorHandler = errorHandler;
		}
	}
}
