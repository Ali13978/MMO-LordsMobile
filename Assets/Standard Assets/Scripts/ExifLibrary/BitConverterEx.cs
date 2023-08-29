using System;

namespace ExifLibrary
{
	public class BitConverterEx
	{
		public enum ByteOrder
		{
			System,
			LittleEndian,
			BigEndian
		}

		private ByteOrder mFrom;

		private ByteOrder mTo;

		public static ByteOrder SystemByteOrder => BitConverter.IsLittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian;

		public static BitConverterEx LittleEndian => new BitConverterEx(ByteOrder.LittleEndian, ByteOrder.System);

		public static BitConverterEx BigEndian => new BitConverterEx(ByteOrder.BigEndian, ByteOrder.System);

		public static BitConverterEx SystemEndian => new BitConverterEx(ByteOrder.System, ByteOrder.System);

		public BitConverterEx(ByteOrder from, ByteOrder to)
		{
			mFrom = from;
			mTo = to;
		}

		public static char ToChar(byte[] value, int startIndex, ByteOrder from, ByteOrder to)
		{
			byte[] value2 = CheckData(value, startIndex, 2, from, to);
			return BitConverter.ToChar(value2, 0);
		}

		public static ushort ToUInt16(byte[] value, int startIndex, ByteOrder from, ByteOrder to)
		{
			byte[] value2 = CheckData(value, startIndex, 2, from, to);
			return BitConverter.ToUInt16(value2, 0);
		}

		public static uint ToUInt32(byte[] value, int startIndex, ByteOrder from, ByteOrder to)
		{
			byte[] value2 = CheckData(value, startIndex, 4, from, to);
			return BitConverter.ToUInt32(value2, 0);
		}

		public static ulong ToUInt64(byte[] value, int startIndex, ByteOrder from, ByteOrder to)
		{
			byte[] value2 = CheckData(value, startIndex, 8, from, to);
			return BitConverter.ToUInt64(value2, 0);
		}

		public static short ToInt16(byte[] value, int startIndex, ByteOrder from, ByteOrder to)
		{
			byte[] value2 = CheckData(value, startIndex, 2, from, to);
			return BitConverter.ToInt16(value2, 0);
		}

		public static int ToInt32(byte[] value, int startIndex, ByteOrder from, ByteOrder to)
		{
			byte[] value2 = CheckData(value, startIndex, 4, from, to);
			return BitConverter.ToInt32(value2, 0);
		}

		public static long ToInt64(byte[] value, int startIndex, ByteOrder from, ByteOrder to)
		{
			byte[] value2 = CheckData(value, startIndex, 8, from, to);
			return BitConverter.ToInt64(value2, 0);
		}

		public static float ToSingle(byte[] value, int startIndex, ByteOrder from, ByteOrder to)
		{
			byte[] value2 = CheckData(value, startIndex, 4, from, to);
			return BitConverter.ToSingle(value2, 0);
		}

		public static double ToDouble(byte[] value, int startIndex, ByteOrder from, ByteOrder to)
		{
			byte[] value2 = CheckData(value, startIndex, 8, from, to);
			return BitConverter.ToDouble(value2, 0);
		}

		public static byte[] GetBytes(ushort value, ByteOrder from, ByteOrder to)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return CheckData(bytes, from, to);
		}

		public static byte[] GetBytes(uint value, ByteOrder from, ByteOrder to)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return CheckData(bytes, from, to);
		}

		public static byte[] GetBytes(ulong value, ByteOrder from, ByteOrder to)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return CheckData(bytes, from, to);
		}

		public static byte[] GetBytes(short value, ByteOrder from, ByteOrder to)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return CheckData(bytes, from, to);
		}

		public static byte[] GetBytes(int value, ByteOrder from, ByteOrder to)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return CheckData(bytes, from, to);
		}

		public static byte[] GetBytes(long value, ByteOrder from, ByteOrder to)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return CheckData(bytes, from, to);
		}

		public static byte[] GetBytes(float value, ByteOrder from, ByteOrder to)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return CheckData(bytes, from, to);
		}

		public static byte[] GetBytes(double value, ByteOrder from, ByteOrder to)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return CheckData(bytes, from, to);
		}

		public char ToChar(byte[] value, int startIndex)
		{
			return ToChar(value, startIndex, mFrom, mTo);
		}

		public ushort ToUInt16(byte[] value, int startIndex)
		{
			return ToUInt16(value, startIndex, mFrom, mTo);
		}

		public uint ToUInt32(byte[] value, int startIndex)
		{
			return ToUInt32(value, startIndex, mFrom, mTo);
		}

		public ulong ToUInt64(byte[] value, int startIndex)
		{
			return ToUInt64(value, startIndex, mFrom, mTo);
		}

		public short ToInt16(byte[] value, int startIndex)
		{
			return ToInt16(value, startIndex, mFrom, mTo);
		}

		public int ToInt32(byte[] value, int startIndex)
		{
			return ToInt32(value, startIndex, mFrom, mTo);
		}

		public long ToInt64(byte[] value, int startIndex)
		{
			return ToInt64(value, startIndex, mFrom, mTo);
		}

		public float ToSingle(byte[] value, int startIndex)
		{
			return ToSingle(value, startIndex, mFrom, mTo);
		}

		public double ToDouble(byte[] value, int startIndex)
		{
			return ToDouble(value, startIndex, mFrom, mTo);
		}

		public byte[] GetBytes(ushort value)
		{
			return GetBytes(value, mFrom, mTo);
		}

		public byte[] GetBytes(uint value)
		{
			return GetBytes(value, mFrom, mTo);
		}

		public byte[] GetBytes(ulong value)
		{
			return GetBytes(value, mFrom, mTo);
		}

		public byte[] GetBytes(short value)
		{
			return GetBytes(value, mFrom, mTo);
		}

		public byte[] GetBytes(int value)
		{
			return GetBytes(value, mFrom, mTo);
		}

		public byte[] GetBytes(long value)
		{
			return GetBytes(value, mFrom, mTo);
		}

		public byte[] GetBytes(float value)
		{
			return GetBytes(value, mFrom, mTo);
		}

		public byte[] GetBytes(double value)
		{
			return GetBytes(value, mFrom, mTo);
		}

		private static byte[] CheckData(byte[] value, int startIndex, int length, ByteOrder from, ByteOrder to)
		{
			from = CheckByteOrder(from);
			to = CheckByteOrder(to);
			byte[] array = new byte[length];
			Array.Copy(value, startIndex, array, 0, length);
			if (from != to)
			{
				Array.Reverse(array);
			}
			return array;
		}

		private static byte[] CheckData(byte[] value, ByteOrder from, ByteOrder to)
		{
			return CheckData(value, 0, value.Length, from, to);
		}

		private static ByteOrder CheckByteOrder(ByteOrder order)
		{
			if (order == ByteOrder.System)
			{
				if (BitConverter.IsLittleEndian)
				{
					return ByteOrder.LittleEndian;
				}
				return ByteOrder.BigEndian;
			}
			return order;
		}
	}
}
