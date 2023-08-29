namespace ExifLibrary
{
	public class ExifAscii : ExifProperty
	{
		protected string mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (string)value;
			}
		}

		public new string Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 2, (uint)(mValue.Length + 1), ExifBitConverter.GetBytes(mValue, addnull: true));

		public ExifAscii(ExifTag tag, string value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			return mValue;
		}

		public static implicit operator string(ExifAscii obj)
		{
			return obj.mValue;
		}
	}
}
