using System;
using System.Collections.Generic;
using System.IO;

namespace ExifLibrary
{
	public class JPEGFile
	{
		public List<JPEGSection> Sections
		{
			get;
			set;
		}

		public byte[] TrailingData
		{
			get;
			set;
		}

		public JPEGFile()
		{
		}

		public JPEGFile(Stream stream)
		{
			Read(stream);
		}

		public JPEGFile(string filename)
		{
			using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
			{
				Read(stream);
			}
		}

		public void Save(Stream stream)
		{
			foreach (JPEGSection section in Sections)
			{
				if (section.Header.Length + 2 + 2 > 65536)
				{
					throw new SectionExceeds64KBException();
				}
				stream.Write(new byte[2]
				{
					255,
					(byte)section.Marker
				}, 0, 2);
				if (section.Header.Length != 0)
				{
					stream.Write(BitConverterEx.BigEndian.GetBytes((ushort)(section.Header.Length + 2)), 0, 2);
					stream.Write(section.Header, 0, section.Header.Length);
				}
				if (section.EntropyData.Length != 0)
				{
					stream.Write(section.EntropyData, 0, section.EntropyData.Length);
				}
			}
			if (TrailingData.Length != 0)
			{
				stream.Write(TrailingData, 0, TrailingData.Length);
			}
		}

		public void Save(string filename)
		{
			using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
			{
				Save(fileStream);
				fileStream.Close();
			}
		}

		public bool Read(Stream stream)
		{
			Sections = new List<JPEGSection>();
			using (stream)
			{
				byte[] array = new byte[2];
				if (stream.Read(array, 0, 2) != 2 || (array[0] != byte.MaxValue && array[1] != 216))
				{
					return false;
				}
				stream.Seek(0L, SeekOrigin.Begin);
				while (stream.Position != stream.Length)
				{
					if (stream.Read(array, 0, 2) != 2 || array[0] != byte.MaxValue || array[1] == 0 || array[1] == byte.MaxValue)
					{
						return false;
					}
					JPEGMarker jPEGMarker = (JPEGMarker)array[1];
					byte[] array2 = new byte[0];
					switch (jPEGMarker)
					{
					default:
					{
						byte[] array3 = new byte[2];
						if (stream.Read(array3, 0, 2) != 2)
						{
							return false;
						}
						long num = (int)BitConverterEx.BigEndian.ToUInt16(array3, 0);
						array2 = new byte[num - 2];
						int num3;
						for (int num2 = array2.Length; num2 > 0; num2 -= num3)
						{
							int count = Math.Min(num2, 4096);
							num3 = stream.Read(array2, array2.Length - num2, count);
							if (num3 == 0)
							{
								return false;
							}
						}
						break;
					}
					case JPEGMarker.RST0:
					case JPEGMarker.RST1:
					case JPEGMarker.RST2:
					case JPEGMarker.RST3:
					case JPEGMarker.RST4:
					case JPEGMarker.RST5:
					case JPEGMarker.RST6:
					case JPEGMarker.RST7:
					case JPEGMarker.SOI:
					case JPEGMarker.EOI:
						break;
					}
					byte[] array4 = new byte[0];
					switch (jPEGMarker)
					{
					case JPEGMarker.RST0:
					case JPEGMarker.RST1:
					case JPEGMarker.RST2:
					case JPEGMarker.RST3:
					case JPEGMarker.RST4:
					case JPEGMarker.RST5:
					case JPEGMarker.RST6:
					case JPEGMarker.RST7:
					case JPEGMarker.SOS:
					{
						long position = stream.Position;
						int num4;
						do
						{
							num4 = 0;
							do
							{
								num4 = stream.ReadByte();
								if (num4 == -1)
								{
									return false;
								}
							}
							while ((byte)num4 != byte.MaxValue);
							do
							{
								num4 = stream.ReadByte();
								if (num4 == -1)
								{
									return false;
								}
							}
							while ((byte)num4 == byte.MaxValue);
						}
						while ((byte)num4 == 0);
						stream.Seek(-2L, SeekOrigin.Current);
						long num5 = stream.Position - position;
						stream.Seek(-num5, SeekOrigin.Current);
						array4 = new byte[num5];
						int num7;
						for (int num6 = array4.Length; num6 > 0; num6 -= num7)
						{
							int count2 = Math.Min(num6, 4096);
							num7 = stream.Read(array4, array4.Length - num6, count2);
							if (num7 == 0)
							{
								return false;
							}
						}
						break;
					}
					}
					JPEGSection item = new JPEGSection(jPEGMarker, array2, array4);
					Sections.Add(item);
					if (jPEGMarker == JPEGMarker.EOI)
					{
						int num8 = (int)(stream.Length - stream.Position);
						TrailingData = new byte[num8];
						while (num8 > 0)
						{
							int count3 = Math.Min(num8, 4096);
							int num9 = stream.Read(TrailingData, TrailingData.Length - num8, count3);
							if (num9 == 0)
							{
								return false;
							}
							num8 -= num9;
						}
					}
				}
				stream.Close();
			}
			return true;
		}
	}
}
