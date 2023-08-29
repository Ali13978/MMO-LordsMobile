using System;

namespace ExifLibrary
{
	public class ExifDateTime : ExifProperty
	{
		protected DateTime mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (DateTime)value;
			}
		}

		public new DateTime Value
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

		public override ExifInterOperability Interoperability => new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 2, 20u, ExifBitConverter.GetBytes(mValue, hastime: true));

		public ExifDateTime(ExifTag tag, DateTime value)
			: base(tag)
		{
			mValue = value;
		}

		public override string ToString()
		{
			return mValue.ToString("yyyy.MM.dd HH:mm:ss");
		}

		public static implicit operator DateTime(ExifDateTime obj)
		{
			return obj.mValue;
		}
	}
}
