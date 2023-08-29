using System.Text;

namespace ExifLibrary
{
	public class ExifSIntArray : ExifProperty
	{
		protected int[] mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (int[])value;
			}
		}

		public new int[] Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 9, (uint)mValue.Length, ExifBitConverter.GetBytes(mValue, BitConverterEx.ByteOrder.System));

		public ExifSIntArray(ExifTag tag, int[] value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			int[] array = mValue;
			foreach (int value in array)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(' ');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		public static implicit operator int[](ExifSIntArray obj)
		{
			return obj.mValue;
		}
	}
}
