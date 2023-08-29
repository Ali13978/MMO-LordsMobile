namespace ExifLibrary
{
	public class ExifByte : ExifProperty
	{
		protected byte mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (byte)value;
			}
		}

		public new byte Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 1, 1u, new byte[1]
		{
			mValue
		});

		public ExifByte(ExifTag tag, byte value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			return mValue.ToString();
		}

		public static implicit operator byte(ExifByte obj)
		{
			return obj.mValue;
		}
	}
}
