using BackendlessAPI.Async;
using System.IO;
using System.Net;

namespace BackendlessAPI.Service
{
	internal class RequestStreamAsyncState<T>
	{
		public UploadCallback UploadCallback;

		public HttpWebRequest HttpRequest;

		public Stream Stream;

		public byte[] BoundaryBytes;

		public byte[] HeaderBytes;

		public AsyncCallback<T> Callback;
	}
}
