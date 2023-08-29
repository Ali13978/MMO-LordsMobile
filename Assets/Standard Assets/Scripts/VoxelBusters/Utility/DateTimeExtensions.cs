using System;
using System.Globalization;

namespace VoxelBusters.Utility
{
	public static class DateTimeExtensions
	{
		private const string kZuluFormat = "yyyy-MM-dd HH:mm:ss zzz";

		public static DateTime ToDateTimeUTC(this string _string, string _format = null)
		{
			if (_string == null)
			{
				return default(DateTime);
			}
			return DateTime.ParseExact(_string, _format, CultureInfo.InvariantCulture).ToUniversalTime();
		}

		public static DateTime ToDateTimeLocal(this string _string, string _format = null)
		{
			if (_string == null)
			{
				return default(DateTime);
			}
			return DateTime.ParseExact(_string, _format, CultureInfo.InvariantCulture).ToLocalTime();
		}

		public static DateTime ToZuluFormatDateTimeUTC(this string _string)
		{
			if (_string == null)
			{
				return default(DateTime);
			}
			return DateTime.ParseExact(_string, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.InvariantCulture).ToUniversalTime();
		}

		public static DateTime ToZuluFormatDateTimeLocal(this string _string)
		{
			if (_string == null)
			{
				return default(DateTime);
			}
			return DateTime.ParseExact(_string, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.InvariantCulture).ToLocalTime();
		}

		public static string ToStringUsingZuluFormat(this DateTime _dateTime)
		{
			string text = _dateTime.ToString("yyyy-MM-dd HH:mm:ss zzz");
			int length = text.Length;
			return text.Remove(length - 3, 1);
		}

		public static DateTime ToDateTimeFromJavaTime(this long _time)
		{
			TimeSpan value = TimeSpan.FromMilliseconds(_time);
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(value);
		}

		public static long ToJavaTimeFromDateTime(this DateTime _dateTime)
		{
			DateTime value = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return (long)_dateTime.ToUniversalTime().Subtract(value).TotalMilliseconds;
		}
	}
}
