using System;
using System.Globalization;
using System.Text;

namespace ExifLibrary
{
	public class ExifBitConverter : BitConverterEx
	{
		public ExifBitConverter(ByteOrder from, ByteOrder to)
			: base(from, to)
		{
		}

		public static string ToAscii(byte[] data, bool endatfirstnull)
		{
			int num = data.Length;
			if (endatfirstnull)
			{
				num = Array.IndexOf(data, (byte)0);
				if (num == -1)
				{
					num = data.Length;
				}
			}
			return Encoding.UTF8.GetString(data, 0, num);
		}

		public static string ToAscii(byte[] data)
		{
			return ToAscii(data, endatfirstnull: true);
		}

		public static string ToString(byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte value in data)
			{
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		public static DateTime ToDateTime(byte[] data, bool hastime)
		{
			string s = ToAscii(data);
			if (hastime)
			{
				return DateTime.ParseExact(s, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);
			}
			return DateTime.ParseExact(s, "yyyy:MM:dd", CultureInfo.InvariantCulture);
		}

		public static DateTime ToDateTime(byte[] data)
		{
			return ToDateTime(data, hastime: true);
		}

		public static MathEx.UFraction32 ToURational(byte[] data, ByteOrder frombyteorder)
		{
			byte[] array = new byte[4];
			byte[] array2 = new byte[4];
			Array.Copy(data, 0, array, 0, 4);
			Array.Copy(data, 4, array2, 0, 4);
			return new MathEx.UFraction32(BitConverterEx.ToUInt32(array, 0, frombyteorder, ByteOrder.System), BitConverterEx.ToUInt32(array2, 0, frombyteorder, ByteOrder.System));
		}

		public static MathEx.Fraction32 ToSRational(byte[] data, ByteOrder frombyteorder)
		{
			byte[] array = new byte[4];
			byte[] array2 = new byte[4];
			Array.Copy(data, 0, array, 0, 4);
			Array.Copy(data, 4, array2, 0, 4);
			return new MathEx.Fraction32(BitConverterEx.ToInt32(array, 0, frombyteorder, ByteOrder.System), BitConverterEx.ToInt32(array2, 0, frombyteorder, ByteOrder.System));
		}

		public static ushort[] ToUShortArray(byte[] data, int count, ByteOrder frombyteorder)
		{
			ushort[] array = new ushort[count];
			for (uint num = 0u; num < count; num++)
			{
				byte[] array2 = new byte[2];
				Array.Copy(data, num * 2, array2, 0L, 2L);
				array[num] = BitConverterEx.ToUInt16(array2, 0, frombyteorder, ByteOrder.System);
			}
			return array;
		}

		public static uint[] ToUIntArray(byte[] data, int count, ByteOrder frombyteorder)
		{
			uint[] array = new uint[count];
			for (uint num = 0u; num < count; num++)
			{
				byte[] array2 = new byte[4];
				Array.Copy(data, num * 4, array2, 0L, 4L);
				array[num] = BitConverterEx.ToUInt32(array2, 0, frombyteorder, ByteOrder.System);
			}
			return array;
		}

		public static int[] ToSIntArray(byte[] data, int count, ByteOrder byteorder)
		{
			int[] array = new int[count];
			for (uint num = 0u; num < count; num++)
			{
				byte[] array2 = new byte[4];
				Array.Copy(data, num * 4, array2, 0L, 4L);
				array[num] = BitConverterEx.ToInt32(array2, 0, byteorder, ByteOrder.System);
			}
			return array;
		}

		public static MathEx.UFraction32[] ToURationalArray(byte[] data, int count, ByteOrder frombyteorder)
		{
			MathEx.UFraction32[] array = new MathEx.UFraction32[count];
			for (uint num = 0u; num < count; num++)
			{
				byte[] array2 = new byte[4];
				byte[] array3 = new byte[4];
				Array.Copy(data, num * 8, array2, 0L, 4L);
				Array.Copy(data, num * 8 + 4, array3, 0L, 4L);
				array[num].Set(BitConverterEx.ToUInt32(array2, 0, frombyteorder, ByteOrder.System), BitConverterEx.ToUInt32(array3, 0, frombyteorder, ByteOrder.System));
			}
			return array;
		}

		public static MathEx.Fraction32[] ToSRationalArray(byte[] data, int count, ByteOrder frombyteorder)
		{
			MathEx.Fraction32[] array = new MathEx.Fraction32[count];
			for (uint num = 0u; num < count; num++)
			{
				byte[] array2 = new byte[4];
				byte[] array3 = new byte[4];
				Array.Copy(data, num * 8, array2, 0L, 4L);
				Array.Copy(data, num * 8 + 4, array3, 0L, 4L);
				array[num].Set(BitConverterEx.ToInt32(array2, 0, frombyteorder, ByteOrder.System), BitConverterEx.ToInt32(array3, 0, frombyteorder, ByteOrder.System));
			}
			return array;
		}

		public static byte[] GetBytes(string value, bool addnull)
		{
			if (addnull)
			{
				value += '\0';
			}
			return Encoding.UTF8.GetBytes(value);
		}

		public static byte[] GetBytes(string value)
		{
			return GetBytes(value, addnull: false);
		}

		public static byte[] GetBytes(DateTime value, bool hastime)
		{
			string empty = string.Empty;
			empty = ((!hastime) ? value.ToString("yyyy:MM:dd", CultureInfo.InvariantCulture) : value.ToString("yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture));
			return GetBytes(empty, addnull: true);
		}

		public static byte[] GetBytes(MathEx.UFraction32 value, ByteOrder tobyteorder)
		{
			byte[] bytes = BitConverterEx.GetBytes(value.Numerator, ByteOrder.System, tobyteorder);
			byte[] bytes2 = BitConverterEx.GetBytes(value.Denominator, ByteOrder.System, tobyteorder);
			byte[] array = new byte[8];
			Array.Copy(bytes, 0, array, 0, 4);
			Array.Copy(bytes2, 0, array, 4, 4);
			return array;
		}

		public static byte[] GetBytes(MathEx.Fraction32 value, ByteOrder tobyteorder)
		{
			byte[] bytes = BitConverterEx.GetBytes(value.Numerator, ByteOrder.System, tobyteorder);
			byte[] bytes2 = BitConverterEx.GetBytes(value.Denominator, ByteOrder.System, tobyteorder);
			byte[] array = new byte[8];
			Array.Copy(bytes, 0, array, 0, 4);
			Array.Copy(bytes2, 0, array, 4, 4);
			return array;
		}

		public static byte[] GetBytes(ushort[] value, ByteOrder tobyteorder)
		{
			byte[] array = new byte[2 * value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				byte[] bytes = BitConverterEx.GetBytes(value[i], ByteOrder.System, tobyteorder);
				Array.Copy(bytes, 0, array, i * 2, 2);
			}
			return array;
		}

		public static byte[] GetBytes(uint[] value, ByteOrder tobyteorder)
		{
			byte[] array = new byte[4 * value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				byte[] bytes = BitConverterEx.GetBytes(value[i], ByteOrder.System, tobyteorder);
				Array.Copy(bytes, 0, array, i * 4, 4);
			}
			return array;
		}

		public static byte[] GetBytes(int[] value, ByteOrder tobyteorder)
		{
			byte[] array = new byte[4 * value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				byte[] bytes = BitConverterEx.GetBytes(value[i], ByteOrder.System, tobyteorder);
				Array.Copy(bytes, 0, array, i * 4, 4);
			}
			return array;
		}

		public static byte[] GetBytes(MathEx.UFraction32[] value, ByteOrder tobyteorder)
		{
			byte[] array = new byte[8 * value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				byte[] bytes = BitConverterEx.GetBytes(value[i].Numerator, ByteOrder.System, tobyteorder);
				byte[] bytes2 = BitConverterEx.GetBytes(value[i].Denominator, ByteOrder.System, tobyteorder);
				Array.Copy(bytes, 0, array, i * 8, 4);
				Array.Copy(bytes2, 0, array, i * 8 + 4, 4);
			}
			return array;
		}

		public static byte[] GetBytes(MathEx.Fraction32[] value, ByteOrder tobyteorder)
		{
			byte[] array = new byte[8 * value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				byte[] bytes = BitConverterEx.GetBytes(value[i].Numerator, ByteOrder.System, tobyteorder);
				byte[] bytes2 = BitConverterEx.GetBytes(value[i].Denominator, ByteOrder.System, tobyteorder);
				Array.Copy(bytes, 0, array, i * 8, 4);
				Array.Copy(bytes2, 0, array, i * 8 + 4, 4);
			}
			return array;
		}
	}
}
