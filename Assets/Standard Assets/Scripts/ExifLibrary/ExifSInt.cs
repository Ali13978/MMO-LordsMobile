namespace ExifLibrary
{
	public class ExifSInt : ExifProperty
	{
		protected int mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (int)value;
			}
		}

		public new int Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 9, 1u, BitConverterEx.GetBytes(mValue, BitConverterEx.ByteOrder.System, BitConverterEx.ByteOrder.System));

		public ExifSInt(ExifTag tag, int value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			return mValue.ToString();
		}

		public static implicit operator int(ExifSInt obj)
		{
			return obj.mValue;
		}
	}
}
