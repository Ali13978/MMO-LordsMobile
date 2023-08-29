using System;

namespace ExifLibrary
{
	public class SectionExceeds64KBException : Exception
	{
		public SectionExceeds64KBException()
			: base("Section length exceeds 64 kB.")
		{
		}

		public SectionExceeds64KBException(string message)
			: base(message)
		{
		}
	}
}
