using System.Text;

namespace ExifLibrary
{
	public class ExifByteArray : ExifProperty
	{
		protected byte[] mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (byte[])value;
			}
		}

		public new byte[] Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 1, (uint)mValue.Length, mValue);

		public ExifByteArray(ExifTag tag, byte[] value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			byte[] array = mValue;
			foreach (byte value in array)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(' ');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		public static implicit operator byte[](ExifByteArray obj)
		{
			return obj.mValue;
		}
	}
}
