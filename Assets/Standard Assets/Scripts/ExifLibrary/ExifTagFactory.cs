using System;

namespace ExifLibrary
{
	public static class ExifTagFactory
	{
		public static ExifTag GetExifTag(IFD ifd, ushort tagid)
		{
			return (ExifTag)(ifd + tagid);
		}

		public static ushort GetTagID(ExifTag exiftag)
		{
			IFD tagIFD = GetTagIFD(exiftag);
			return (ushort)((int)exiftag - (int)tagIFD);
		}

		public static IFD GetTagIFD(ExifTag tag)
		{
			return (IFD)((int)tag / 100000 * 100000);
		}

		public static string GetTagName(ExifTag tag)
		{
			string name = Enum.GetName(typeof(ExifTag), tag);
			if (name == null)
			{
				return "Unknown";
			}
			return name;
		}

		public static string GetTagName(IFD ifd, ushort tagid)
		{
			return GetTagName(GetExifTag(ifd, tagid));
		}

		public static string GetTagLongName(ExifTag tag)
		{
			string name = Enum.GetName(typeof(IFD), GetTagIFD(tag));
			string text = Enum.GetName(typeof(ExifTag), tag);
			if (text == null)
			{
				text = "Unknown";
			}
			string text2 = GetTagID(tag).ToString();
			return name + ": " + text + " (" + text2 + ")";
		}
	}
}
