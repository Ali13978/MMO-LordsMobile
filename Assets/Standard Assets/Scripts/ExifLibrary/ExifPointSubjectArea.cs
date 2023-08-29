using System.Text;

namespace ExifLibrary
{
	public class ExifPointSubjectArea : ExifUShortArray
	{
		protected new ushort[] Value
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

		public ushort X
		{
			get
			{
				return mValue[0];
			}
			set
			{
				mValue[0] = value;
			}
		}

		public ushort Y
		{
			get
			{
				return mValue[1];
			}
			set
			{
				mValue[1] = value;
			}
		}

		public ExifPointSubjectArea(ExifTag tag, ushort[] value)
			: base(tag, value)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("({0:d}, {1:d})", mValue[0], mValue[1]);
			return stringBuilder.ToString();
		}
	}
}
