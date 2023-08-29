namespace ExifLibrary
{
	public class ExifSRational : ExifProperty
	{
		protected MathEx.Fraction32 mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (MathEx.Fraction32)value;
			}
		}

		public new MathEx.Fraction32 Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 10, 1u, ExifBitConverter.GetBytes(mValue, BitConverterEx.ByteOrder.System));

		public ExifSRational(ExifTag tag, int numerator, int denominator)
			: base(tag)
		{
			mValue = new MathEx.Fraction32(numerator, denominator);
		}

		public ExifSRational(ExifTag tag, MathEx.Fraction32 value)
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

		public int[] ToArray()
		{
			return new int[2]
			{
				mValue.Numerator,
				mValue.Denominator
			};
		}

		public static explicit operator float(ExifSRational obj)
		{
			return (float)obj.mValue;
		}
	}
}
