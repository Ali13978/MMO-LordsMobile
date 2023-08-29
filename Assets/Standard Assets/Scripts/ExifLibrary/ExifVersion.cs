using System.Text;

namespace ExifLibrary
{
	public class ExifVersion : ExifProperty
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
				mValue = value.Substring(0, 4);
			}
		}

		public override ExifInterOperability Interoperability
		{
			get
			{
				if (mTag == ExifTag.ExifVersion || mTag == ExifTag.FlashpixVersion || mTag == ExifTag.InteroperabilityVersion)
				{
					return new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 7, 4u, Encoding.ASCII.GetBytes(mValue));
				}
				byte[] array = new byte[4];
				for (int i = 0; i < 4; i++)
				{
					array[i] = byte.Parse(mValue[0].ToString());
				}
				return new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 7, 4u, array);
			}
		}

		public ExifVersion(ExifTag tag, string value)
			: base(tag)
		{
			if (value.Length > 4)
			{
				mValue = value.Substring(0, 4);
			}
			else if (value.Length < 4)
			{
				mValue = value + new string(' ', 4 - value.Length);
			}
			else
			{
				mValue = value;
			}
		}

		public override string ToString()
		{
			return mValue;
		}
	}
}
