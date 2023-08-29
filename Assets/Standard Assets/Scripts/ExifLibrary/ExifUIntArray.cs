using System.Text;

namespace ExifLibrary
{
	public class ExifUIntArray : ExifProperty
	{
		protected uint[] mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (uint[])value;
			}
		}

		public new uint[] Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 3, (uint)mValue.Length, ExifBitConverter.GetBytes(mValue, BitConverterEx.ByteOrder.System));

		public ExifUIntArray(ExifTag tag, uint[] value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			uint[] array = mValue;
			foreach (uint value in array)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(' ');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		public static implicit operator uint[](ExifUIntArray obj)
		{
			return obj.mValue;
		}
	}
}
