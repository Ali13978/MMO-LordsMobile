using System;

namespace ExifLibrary
{
	public class IFD0IsEmptyException : Exception
	{
		public IFD0IsEmptyException()
			: base("0th IFD section cannot be empty.")
		{
		}

		public IFD0IsEmptyException(string message)
			: base(message)
		{
		}
	}
}
