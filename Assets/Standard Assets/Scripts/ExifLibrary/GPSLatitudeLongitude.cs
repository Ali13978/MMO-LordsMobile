namespace ExifLibrary
{
	public class GPSLatitudeLongitude : ExifURationalArray
	{
		protected new MathEx.UFraction32[] Value
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

		public MathEx.UFraction32 Degrees
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

		public MathEx.UFraction32 Minutes
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

		public MathEx.UFraction32 Seconds
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

		public GPSLatitudeLongitude(ExifTag tag, MathEx.UFraction32[] value)
			: base(tag, value)
		{
		}

		public float ToFloat()
		{
			return (float)Degrees + (float)Minutes / 60f + (float)Seconds / 3600f;
		}

		public override string ToString()
		{
			return $"{(float)Degrees:F2}°{(float)Minutes:F2}'{(float)Seconds:F2}\"";
		}

		public static explicit operator float(GPSLatitudeLongitude obj)
		{
			return obj.ToFloat();
		}
	}
}
