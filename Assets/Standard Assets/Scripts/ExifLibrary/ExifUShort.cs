namespace ExifLibrary
{
	public class ExifUShort : ExifProperty
	{
		protected ushort mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (ushort)value;
			}
		}

		public new ushort Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 3, 1u, BitConverterEx.GetBytes(mValue, BitConverterEx.ByteOrder.System, BitConverterEx.ByteOrder.System));

		public ExifUShort(ExifTag tag, ushort value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			return mValue.ToString();
		}

		public static implicit operator ushort(ExifUShort obj)
		{
			return obj.mValue;
		}
	}
}
