using System;
using System.Text;

namespace VoxelBusters.Utility
{
	public static class StringExtensions
	{
		public static string GetPrintableString(this string _string)
		{
			return (_string != null) ? _string : "NULL";
		}

		public static bool Contains(this string _string, string _stringToCheck, bool _ignoreCase)
		{
			if (!_ignoreCase)
			{
				return _string.Contains(_stringToCheck);
			}
			return _string.ToLower().Contains(_stringToCheck.ToLower());
		}

		public static string ToBase64(this string _string)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(_string);
			return Convert.ToBase64String(bytes);
		}

		public static string FromBase64(this string _string)
		{
			byte[] array = Convert.FromBase64String(_string);
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}

		public static string StringBetween(this string _string, string _startString, string _endString, bool _ignoreCase)
		{
			StringComparison comparisonType = (!_ignoreCase) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			int num = _startString?.Length ?? 0;
			int num2 = _string.IndexOf(_startString, comparisonType);
			int num3 = _string.IndexOf(_endString, num2 + num, comparisonType);
			if (num2 == -1 || num3 == -1)
			{
				return string.Empty;
			}
			int length = num3 - (num2 + num);
			return _string.Substring(num2 + num, length);
		}
	}
}
