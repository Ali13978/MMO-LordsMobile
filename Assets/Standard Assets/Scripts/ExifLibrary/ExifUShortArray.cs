using System.Text;

namespace ExifLibrary
{
	public class ExifUShortArray : ExifProperty
	{
		protected ushort[] mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (ushort[])value;
			}
		}

		public new ushort[] Value
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

		public ExifUShortArray(ExifTag tag, ushort[] value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			ushort[] array = mValue;
			foreach (ushort value in array)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(' ');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		public static implicit operator ushort[](ExifUShortArray obj)
		{
			return obj.mValue;
		}
	}
}
