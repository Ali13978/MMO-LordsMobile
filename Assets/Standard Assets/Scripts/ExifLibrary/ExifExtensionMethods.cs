using System.IO;

namespace ExifLibrary
{
	public static class ExifExtensionMethods
	{
		public static byte[] ReadBytes(FileStream stream, int count)
		{
			byte[] array = new byte[count];
			int num = 0;
			int num2 = count;
			while (num2 > 0)
			{
				int num3 = stream.Read(array, num, num2);
				if (num3 <= 0)
				{
					return null;
				}
				num2 -= num3;
				num += num3;
			}
			return array;
		}

		public static void WriteBytes(FileStream stream, byte[] buffer)
		{
			stream.Write(buffer, 0, buffer.Length);
		}
	}
}
