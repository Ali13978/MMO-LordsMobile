using System.Text;

namespace ExifLibrary
{
	public class ExifURationalArray : ExifProperty
	{
		protected MathEx.UFraction32[] mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (MathEx.UFraction32[])value;
			}
		}

		public new MathEx.UFraction32[] Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 5, (uint)mValue.Length, ExifBitConverter.GetBytes(mValue, BitConverterEx.ByteOrder.System));

		public ExifURationalArray(ExifTag tag, MathEx.UFraction32[] value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			MathEx.UFraction32[] array = mValue;
			for (int i = 0; i < array.Length; i++)
			{
				MathEx.UFraction32 uFraction = array[i];
				stringBuilder.Append(uFraction.ToString());
				stringBuilder.Append(' ');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		public static explicit operator float[](ExifURationalArray obj)
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
