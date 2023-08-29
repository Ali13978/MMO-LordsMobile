namespace ExifLibrary
{
	public struct ExifInterOperability
	{
		private ushort mTagID;

		private ushort mTypeID;

		private uint mCount;

		private byte[] mData;

		public ushort TagID => mTagID;

		public ushort TypeID => mTypeID;

		public uint Count => mCount;

		public byte[] Data => mData;

		public ExifInterOperability(ushort tagid, ushort typeid, uint count, byte[] data)
		{
			mTagID = tagid;
			mTypeID = typeid;
			mCount = count;
			mData = data;
		}

		public override string ToString()
		{
			return $"Tag: {mTagID}, Type: {mTypeID}, Count: {mCount}, Data Length: {mData.Length}";
		}
	}
}
