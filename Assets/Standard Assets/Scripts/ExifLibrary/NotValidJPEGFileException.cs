using System;

namespace ExifLibrary
{
	public class NotValidJPEGFileException : Exception
	{
		public NotValidJPEGFileException()
			: base("Not a valid JPEG file.")
		{
		}

		public NotValidJPEGFileException(string message)
			: base(message)
		{
		}
	}
}
