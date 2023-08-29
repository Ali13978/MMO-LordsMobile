using System;

namespace ExifLibrary
{
	public class UnknownIFDSectionException : Exception
	{
		public UnknownIFDSectionException()
			: base("Unknown IFD section.")
		{
		}

		public UnknownIFDSectionException(string message)
			: base(message)
		{
		}
	}
}
