using BackendlessAPI.Async;

namespace BackendlessAPI.Service
{
	internal class EmptyUploadCallback : UploadCallback
	{
		public EmptyUploadCallback()
			: base(delegate
			{
			})
		{
		}
	}
}
