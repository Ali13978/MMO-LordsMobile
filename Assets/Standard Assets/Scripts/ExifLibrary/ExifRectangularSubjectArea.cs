using System.Text;

namespace ExifLibrary
{
	public class ExifRectangularSubjectArea : ExifPointSubjectArea
	{
		public ushort Width
		{
			get
			{
				return mValue[2];
			}
			set
			{
				mValue[2] = value;
			}
		}

		public ushort Height
		{
			get
			{
				return mValue[3];
			}
			set
			{
				mValue[3] = value;
			}
		}

		public ExifRectangularSubjectArea(ExifTag tag, ushort[] value)
			: base(tag, value)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("({0:d}, {1:d}) ({2:d} x {3:d})", mValue[0], mValue[1], mValue[2], mValue[3]);
			return stringBuilder.ToString();
		}
	}
}
