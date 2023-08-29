using System.Text;

namespace ExifLibrary
{
	public class ExifSRationalArray : ExifProperty
	{
		protected MathEx.Fraction32[] mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (MathEx.Fraction32[])value;
			}
		}

		public new MathEx.Fraction32[] Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 10, (uint)mValue.Length, ExifBitConverter.GetBytes(mValue, BitConverterEx.ByteOrder.System));

		public ExifSRationalArray(ExifTag tag, MathEx.Fraction32[] value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			MathEx.Fraction32[] array = mValue;
			for (int i = 0; i < array.Length; i++)
			{
				MathEx.Fraction32 fraction = array[i];
				stringBuilder.Append(fraction.ToString());
				stringBuilder.Append(' ');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		public static explicit operator float[](ExifSRationalArray obj)
		{
			float[] array = new float[obj.mValue.Length];
			for (int i = 0; i < obj.mValue.Length; i++)
			{
				array[i] = (float)obj.mValue[i];
			}
			return array;
		}
	}
}
