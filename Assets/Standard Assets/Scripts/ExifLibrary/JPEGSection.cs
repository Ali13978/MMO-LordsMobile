namespace ExifLibrary
{
	public class JPEGSection
	{
		public JPEGMarker Marker
		{
			get;
			private set;
		}

		public byte[] Header
		{
			get;
			set;
		}

		public byte[] EntropyData
		{
			get;
			set;
		}

		private JPEGSection()
		{
			Header = new byte[0];
			EntropyData = new byte[0];
		}

		public JPEGSection(JPEGMarker marker, byte[] data, byte[] entropydata)
		{
			Marker = marker;
			Header = data;
			EntropyData = entropydata;
		}

		public JPEGSection(JPEGMarker marker)
		{
			Marker = marker;
		}

		public override string ToString()
		{
			return $"{Marker} => Header: {Header.Length} bytes, Entropy Data: {EntropyData.Length} bytes";
		}
	}
}
