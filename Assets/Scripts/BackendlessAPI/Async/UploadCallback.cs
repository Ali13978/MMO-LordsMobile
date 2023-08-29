namespace BackendlessAPI.Async
{
	public class UploadCallback
	{
		internal readonly ProgressHandler ProgressHandler;

		public UploadCallback(ProgressHandler progressHandler)
		{
			ProgressHandler = progressHandler;
		}
	}
}
