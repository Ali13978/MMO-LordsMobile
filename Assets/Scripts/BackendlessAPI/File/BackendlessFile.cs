using BackendlessAPI.Async;
using BackendlessAPI.LitJson;

namespace BackendlessAPI.File
{
	public class BackendlessFile
	{
		[JsonProperty("fileURL")]
		public string FileURL
		{
			get;
			set;
		}

		public BackendlessFile(string fileURL)
		{
			FileURL = fileURL;
		}

		public void Remove()
		{
			Backendless.Files.Remove(FileURL);
		}

		public void Remove(AsyncCallback<object> callback)
		{
			Backendless.Files.Remove(FileURL, callback);
		}
	}
}
