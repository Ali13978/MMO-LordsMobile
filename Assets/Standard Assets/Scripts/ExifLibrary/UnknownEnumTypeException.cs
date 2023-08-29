using System;

namespace ExifLibrary
{
	public class UnknownEnumTypeException : Exception
	{
		public UnknownEnumTypeException()
			: base("Unknown enum type.")
		{
		}

		public UnknownEnumTypeException(string message)
			: base(message)
		{
		}
	}
}
