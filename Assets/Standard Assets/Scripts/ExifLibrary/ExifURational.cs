namespace ExifLibrary
{
	public class ExifURational : ExifProperty
	{
		protected MathEx.UFraction32 mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (MathEx.UFraction32)value;
			}
		}

		public new MathEx.UFraction32 Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 5, 1u, ExifBitConverter.GetBytes(mValue, BitConverterEx.ByteOrder.System));

		public ExifURational(ExifTag tag, uint numerator, uint denominator)
			: base(tag)
		{
			mValue = new MathEx.UFraction32(numerator, denominator);
		}

		public ExifURational(ExifTag tag, MathEx.UFraction32 value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			return mValue.ToString();
		}

		public float ToFloat()
		{
			return (float)mValue;
		}

		public uint[] ToArray()
		{
			return new uint[2]
			{
				mValue.Numerator,
				mValue.Denominator
			};
		}

		public static explicit operator float(ExifURational obj)
		{
			return (float)obj.mValue;
		}
	}
}
