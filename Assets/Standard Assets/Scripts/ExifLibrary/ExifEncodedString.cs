using System;
using System.Text;

namespace ExifLibrary
{
	public class ExifEncodedString : ExifProperty
	{
		protected string mValue;

		private Encoding mEncoding;

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

		public Encoding Encoding
		{
			get
			{
				return mEncoding;
			}
			set
			{
				mEncoding = value;
			}
		}

		public override ExifInterOperability Interoperability
		{
			get
			{
				string empty = string.Empty;
				empty = ((mEncoding == null) ? "\0\0\0\0\0\0\0\0" : ((mEncoding.EncodingName == "US-ASCII") ? "ASCII\0\0\0" : ((mEncoding.EncodingName == "Japanese (JIS 0208-1990 and 0212-1990)") ? "JIS\0\0\0\0\0" : ((!(mEncoding.EncodingName == "Unicode")) ? "\0\0\0\0\0\0\0\0" : "Unicode\0"))));
				byte[] bytes = Encoding.ASCII.GetBytes(empty);
				byte[] array = (mEncoding != null) ? mEncoding.GetBytes(mValue) : Encoding.ASCII.GetBytes(mValue);
				byte[] array2 = new byte[bytes.Length + array.Length];
				Array.Copy(bytes, 0, array2, 0, bytes.Length);
				Array.Copy(array, 0, array2, bytes.Length, array.Length);
				return new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 7, (uint)array2.Length, array2);
			}
		}

		public ExifEncodedString(ExifTag tag, string value, Encoding encoding)
			: base(tag)
		{
			mValue = value;
			mEncoding = encoding;
		}

		public override string ToString()
		{
			return mValue;
		}

		public static implicit operator string(ExifEncodedString obj)
		{
			return obj.mValue;
		}
	}
}
