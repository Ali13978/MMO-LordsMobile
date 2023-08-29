using System.Text;

namespace ExifLibrary
{
	public class ExifCircularSubjectArea : ExifPointSubjectArea
	{
		public ushort Diamater
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

		public ExifCircularSubjectArea(ExifTag tag, ushort[] value)
			: base(tag, value)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("({0:d}, {1:d}) {2:d}", mValue[0], mValue[1], mValue[2]);
			return stringBuilder.ToString();
		}
	}
}
