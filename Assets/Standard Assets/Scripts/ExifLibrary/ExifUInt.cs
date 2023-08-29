namespace ExifLibrary
{
	public class ExifUInt : ExifProperty
	{
		protected uint mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (uint)value;
			}
		}

		public new uint Value
		{
			get
			{
				return mValue;
			}
			set
			{
				mValue = value;
			}
		}

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 4, 1u, BitConverterEx.GetBytes(mValue, BitConverterEx.ByteOrder.System, BitConverterEx.ByteOrder.System));

		public ExifUInt(ExifTag tag, uint value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			return mValue.ToString();
		}

		public static implicit operator uint(ExifUInt obj)
		{
			return obj.mValue;
		}
	}
}
