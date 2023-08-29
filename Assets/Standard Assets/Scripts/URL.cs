namespace VoxelBusters.Utility
{
	public struct URL
	{
		private const string kFileProtocol = "file://";

		private const string kHttpProtocol = "http://";

		private const string kProtocolSeperator = "://";

		public string URLString
		{
			get;
			private set;
		}

		public URL(string _URLString)
		{
			if (_URLString.IndexOf("://") == -1)
			{
				URLString = "file://" + _URLString;
			}
			else
			{
				URLString = _URLString;
			}
		}

		public static URL FileURLWithPath(string _rootPath, string _relativePath)
		{
			return FileURLWithPath(_rootPath + "/" + _relativePath);
		}

		public static URL FileURLWithPath(string _filePath)
		{
			string uRLString = _filePath;
			if (_filePath != null && _filePath.IndexOf("://") == -1)
			{
				uRLString = "file://" + _filePath;
			}
			URL result = default(URL);
			result.URLString = uRLString;
			return result;
		}

		public static URL URLWithString(string _rootURLString, string _relativePath)
		{
			return URLWithString(_rootURLString + "/" + _relativePath);
		}

		public static URL URLWithString(string _URLString)
		{
			string uRLString = _URLString;
			if (_URLString != null && _URLString.IndexOf("://") == -1)
			{
				uRLString = "file://" + _URLString;
			}
			URL result = default(URL);
			result.URLString = uRLString;
			return result;
		}

		public bool isFileReferenceURL()
		{
			return URLString.Contains("file://");
		}

		public override string ToString()
		{
			return $"[URL: {URLString}]";
		}
	}
}
