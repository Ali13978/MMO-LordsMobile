using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExifLibrary
{
	public class ExifFile
	{
		private JPEGFile file;

		private JPEGSection app1;

		private uint makerNoteOffset;

		private long exifIFDFieldOffset;

		private long gpsIFDFieldOffset;

		private long interopIFDFieldOffset;

		private long firstIFDFieldOffset;

		private long thumbOffsetLocation;

		private long thumbSizeLocation;

		private uint thumbOffsetValue;

		private uint thumbSizeValue;

		private bool makerNoteProcessed;

		public Dictionary<ExifTag, ExifProperty> Properties
		{
			get;
			private set;
		}

		public BitConverterEx.ByteOrder ByteOrder
		{
			get;
			set;
		}

		public JPEGFile Thumbnail
		{
			get;
			set;
		}

		public ExifProperty this[ExifTag key]
		{
			get
			{
				return Properties[key];
			}
			set
			{
				Properties[key] = value;
			}
		}

		protected ExifFile()
		{
		}

		public void Save(string filename)
		{
			Save(filename, preserveMakerNote: true);
		}

		public void Save(string filename, bool preserveMakerNote)
		{
			WriteApp1(preserveMakerNote);
			file.Save(filename);
		}

		private void ReadAPP1()
		{
			app1 = file.Sections.Find((JPEGSection a) => a.Marker == JPEGMarker.APP1 && Encoding.ASCII.GetString(a.Header, 0, 6) == "Exif\0\0");
			if (app1 == null)
			{
				int num = file.Sections.FindLastIndex((JPEGSection a) => a.Marker == JPEGMarker.APP0);
				if (num == -1)
				{
					num = 0;
				}
				num++;
				ByteOrder = BitConverterEx.ByteOrder.BigEndian;
				app1 = new JPEGSection(JPEGMarker.APP1);
				file.Sections.Insert(num, app1);
				return;
			}
			byte[] header = app1.Header;
			SortedList<int, IFD> sortedList = new SortedList<int, IFD>();
			makerNoteOffset = 0u;
			int num2 = 6;
			if (header[num2] == 73)
			{
				ByteOrder = BitConverterEx.ByteOrder.LittleEndian;
			}
			else
			{
				ByteOrder = BitConverterEx.ByteOrder.BigEndian;
			}
			BitConverterEx bitConverterEx = new BitConverterEx(ByteOrder, BitConverterEx.ByteOrder.System);
			int key = (int)bitConverterEx.ToUInt32(header, num2 + 4);
			sortedList.Add(key, IFD.Zeroth);
			int num3 = -1;
			int num4 = 0;
			int num5 = -1;
			while (sortedList.Count != 0)
			{
				int num6 = num2 + sortedList.Keys[0];
				IFD iFD = sortedList.Values[0];
				sortedList.RemoveAt(0);
				ushort num7 = bitConverterEx.ToUInt16(header, num6);
				for (short num8 = 0; num8 < num7; num8 = (short)(num8 + 1))
				{
					int num9 = num6 + 2 + 12 * num8;
					ushort num10 = bitConverterEx.ToUInt16(header, num9);
					ushort num11 = bitConverterEx.ToUInt16(header, num9 + 2);
					uint num12 = bitConverterEx.ToUInt32(header, num9 + 4);
					byte[] array = new byte[4];
					Array.Copy(header, num9 + 8, array, 0, 4);
					if (iFD == IFD.Zeroth && num10 == 34665)
					{
						int key2 = (int)bitConverterEx.ToUInt32(array, 0);
						sortedList.Add(key2, IFD.EXIF);
					}
					else if (iFD == IFD.Zeroth && num10 == 34853)
					{
						int key3 = (int)bitConverterEx.ToUInt32(array, 0);
						sortedList.Add(key3, IFD.GPS);
					}
					else if (iFD == IFD.EXIF && num10 == 40965)
					{
						int key4 = (int)bitConverterEx.ToUInt32(array, 0);
						sortedList.Add(key4, IFD.Interop);
					}
					if (iFD == IFD.EXIF && num10 == 37500)
					{
						makerNoteOffset = bitConverterEx.ToUInt32(array, 0);
					}
					uint num13 = 0u;
					switch (num11)
					{
					case 1:
					case 2:
					case 7:
						num13 = 1u;
						break;
					case 3:
						num13 = 2u;
						break;
					case 4:
					case 9:
						num13 = 4u;
						break;
					case 5:
					case 10:
						num13 = 8u;
						break;
					}
					uint num14 = num12 * num13;
					int num15 = 0;
					if (num14 > 4)
					{
						num15 = num2 + (int)bitConverterEx.ToUInt32(array, 0);
						array = new byte[num14];
						Array.Copy(header, num15, array, 0L, num14);
					}
					if (iFD == IFD.First && num10 == 513)
					{
						num5 = 0;
						num3 = (int)bitConverterEx.ToUInt32(array, 0);
					}
					else if (iFD == IFD.First && num10 == 514)
					{
						num4 = (int)bitConverterEx.ToUInt32(array, 0);
					}
					if (iFD == IFD.First && num10 == 273)
					{
						num5 = 1;
						num3 = (int)((num11 != 3) ? bitConverterEx.ToUInt32(array, 0) : bitConverterEx.ToUInt16(array, 0));
					}
					else if (iFD == IFD.First && num10 == 279)
					{
						num4 = 0;
						for (int i = 0; i < num12; i++)
						{
							num4 = ((num11 != 3) ? (num4 + (int)bitConverterEx.ToUInt32(array, 0)) : (num4 + bitConverterEx.ToUInt16(array, 0)));
						}
					}
					ExifProperty exifProperty = ExifPropertyFactory.Get(num10, num11, num12, array, ByteOrder, iFD);
					if (!Properties.ContainsKey(exifProperty.Tag))
					{
						Properties.Add(exifProperty.Tag, exifProperty);
					}
					else
					{
						Properties[exifProperty.Tag] = exifProperty;
					}
				}
				int num16 = (int)bitConverterEx.ToUInt32(header, num6 + 2 + 12 * num7);
				if (num16 != 0)
				{
					sortedList.Add(num16, IFD.First);
				}
				if (num3 != -1 && num4 != 0 && Thumbnail == null && num5 == 0)
				{
					using (MemoryStream stream = new MemoryStream(header, num2 + num3, num4))
					{
						Thumbnail = new JPEGFile(stream);
					}
				}
			}
		}

		private void WriteApp1(bool preserveMakerNote)
		{
			exifIFDFieldOffset = 0L;
			gpsIFDFieldOffset = 0L;
			interopIFDFieldOffset = 0L;
			firstIFDFieldOffset = 0L;
			thumbOffsetLocation = 0L;
			thumbOffsetValue = 0u;
			thumbSizeLocation = 0L;
			thumbSizeValue = 0u;
			Dictionary<ExifTag, ExifProperty> dictionary = new Dictionary<ExifTag, ExifProperty>();
			Dictionary<ExifTag, ExifProperty> dictionary2 = new Dictionary<ExifTag, ExifProperty>();
			Dictionary<ExifTag, ExifProperty> dictionary3 = new Dictionary<ExifTag, ExifProperty>();
			Dictionary<ExifTag, ExifProperty> dictionary4 = new Dictionary<ExifTag, ExifProperty>();
			Dictionary<ExifTag, ExifProperty> dictionary5 = new Dictionary<ExifTag, ExifProperty>();
			foreach (KeyValuePair<ExifTag, ExifProperty> property in Properties)
			{
				switch (property.Value.IFD)
				{
				case IFD.Zeroth:
					dictionary.Add(property.Key, property.Value);
					break;
				case IFD.EXIF:
					dictionary2.Add(property.Key, property.Value);
					break;
				case IFD.GPS:
					dictionary3.Add(property.Key, property.Value);
					break;
				case IFD.Interop:
					dictionary4.Add(property.Key, property.Value);
					break;
				case IFD.First:
					dictionary5.Add(property.Key, property.Value);
					break;
				}
			}
			if (dictionary2.Count != 0 && !dictionary.ContainsKey(ExifTag.EXIFIFDPointer))
			{
				dictionary.Add(ExifTag.EXIFIFDPointer, new ExifUInt(ExifTag.EXIFIFDPointer, 0u));
			}
			if (dictionary3.Count != 0 && !dictionary.ContainsKey(ExifTag.GPSIFDPointer))
			{
				dictionary.Add(ExifTag.GPSIFDPointer, new ExifUInt(ExifTag.GPSIFDPointer, 0u));
			}
			if (dictionary4.Count != 0 && !dictionary2.ContainsKey(ExifTag.InteroperabilityIFDPointer))
			{
				dictionary2.Add(ExifTag.InteroperabilityIFDPointer, new ExifUInt(ExifTag.InteroperabilityIFDPointer, 0u));
			}
			if (dictionary2.Count == 0 && dictionary.ContainsKey(ExifTag.EXIFIFDPointer))
			{
				dictionary.Remove(ExifTag.EXIFIFDPointer);
			}
			if (dictionary3.Count == 0 && dictionary.ContainsKey(ExifTag.GPSIFDPointer))
			{
				dictionary.Remove(ExifTag.GPSIFDPointer);
			}
			if (dictionary4.Count == 0 && dictionary2.ContainsKey(ExifTag.InteroperabilityIFDPointer))
			{
				dictionary2.Remove(ExifTag.InteroperabilityIFDPointer);
			}
			if (dictionary.Count == 0)
			{
				throw new IFD0IsEmptyException();
			}
			BitConverterEx bitConverterEx = new BitConverterEx(BitConverterEx.ByteOrder.System, ByteOrder);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(Encoding.ASCII.GetBytes("Exif\0\0"), 0, 6);
			long position = memoryStream.Position;
			memoryStream.Write((ByteOrder == BitConverterEx.ByteOrder.LittleEndian) ? new byte[2]
			{
				73,
				73
			} : new byte[2]
			{
				77,
				77
			}, 0, 2);
			memoryStream.Write(bitConverterEx.GetBytes((ushort)42), 0, 2);
			memoryStream.Write(bitConverterEx.GetBytes(8u), 0, 4);
			WriteIFD(memoryStream, dictionary, IFD.Zeroth, position, preserveMakerNote);
			uint value = (uint)(memoryStream.Position - position);
			WriteIFD(memoryStream, dictionary2, IFD.EXIF, position, preserveMakerNote);
			uint value2 = (uint)(memoryStream.Position - position);
			WriteIFD(memoryStream, dictionary3, IFD.GPS, position, preserveMakerNote);
			uint value3 = (uint)(memoryStream.Position - position);
			WriteIFD(memoryStream, dictionary4, IFD.Interop, position, preserveMakerNote);
			uint value4 = (uint)(memoryStream.Position - position);
			WriteIFD(memoryStream, dictionary5, IFD.First, position, preserveMakerNote);
			if (exifIFDFieldOffset != 0L)
			{
				memoryStream.Seek(exifIFDFieldOffset, SeekOrigin.Begin);
				memoryStream.Write(bitConverterEx.GetBytes(value), 0, 4);
			}
			if (gpsIFDFieldOffset != 0L)
			{
				memoryStream.Seek(gpsIFDFieldOffset, SeekOrigin.Begin);
				memoryStream.Write(bitConverterEx.GetBytes(value2), 0, 4);
			}
			if (interopIFDFieldOffset != 0L)
			{
				memoryStream.Seek(interopIFDFieldOffset, SeekOrigin.Begin);
				memoryStream.Write(bitConverterEx.GetBytes(value3), 0, 4);
			}
			if (firstIFDFieldOffset != 0L)
			{
				memoryStream.Seek(firstIFDFieldOffset, SeekOrigin.Begin);
				memoryStream.Write(bitConverterEx.GetBytes(value4), 0, 4);
			}
			if (thumbOffsetLocation != 0L)
			{
				memoryStream.Seek(thumbOffsetLocation, SeekOrigin.Begin);
				memoryStream.Write(bitConverterEx.GetBytes(thumbOffsetValue), 0, 4);
			}
			if (thumbSizeLocation != 0L)
			{
				memoryStream.Seek(thumbSizeLocation, SeekOrigin.Begin);
				memoryStream.Write(bitConverterEx.GetBytes(thumbSizeValue), 0, 4);
			}
			memoryStream.Close();
			app1.Header = memoryStream.ToArray();
		}

		private void WriteIFD(MemoryStream stream, Dictionary<ExifTag, ExifProperty> ifd, IFD ifdtype, long tiffoffset, bool preserveMakerNote)
		{
			BitConverterEx bitConverterEx = new BitConverterEx(BitConverterEx.ByteOrder.System, ByteOrder);
			Queue<ExifProperty> queue = new Queue<ExifProperty>();
			foreach (ExifProperty value in ifd.Values)
			{
				if (value.Tag != ExifTag.MakerNote)
				{
					queue.Enqueue(value);
				}
			}
			if (ifd.ContainsKey(ExifTag.MakerNote))
			{
				queue.Enqueue(ifd[ExifTag.MakerNote]);
			}
			uint num = (uint)(2 + ifd.Count * 12 + 4 + stream.Position - tiffoffset);
			uint num2 = num;
			long num3 = stream.Position + (2 + ifd.Count * 12 + 4);
			bool flag = false;
			stream.Write(bitConverterEx.GetBytes((ushort)ifd.Count), 0, 2);
			while (queue.Count != 0)
			{
				ExifProperty exifProperty = queue.Dequeue();
				ExifInterOperability interoperability = exifProperty.Interoperability;
				uint num4 = 0u;
				if (!flag && !makerNoteProcessed && makerNoteOffset != 0 && ifdtype == IFD.EXIF && exifProperty.Tag != ExifTag.MakerNote && interoperability.Data.Length > 4 && num2 + interoperability.Data.Length > makerNoteOffset && ifd.ContainsKey(ExifTag.MakerNote))
				{
					queue.Enqueue(exifProperty);
					continue;
				}
				if (exifProperty.Tag == ExifTag.MakerNote)
				{
					flag = true;
					num4 = ((preserveMakerNote && !makerNoteProcessed) ? (makerNoteOffset - num2) : 0u);
				}
				stream.Write(bitConverterEx.GetBytes(interoperability.TagID), 0, 2);
				stream.Write(bitConverterEx.GetBytes(interoperability.TypeID), 0, 2);
				stream.Write(bitConverterEx.GetBytes(interoperability.Count), 0, 4);
				byte[] data = interoperability.Data;
				if (ByteOrder != BitConverterEx.SystemByteOrder)
				{
					if (interoperability.TypeID == 1 || interoperability.TypeID == 3 || interoperability.TypeID == 4 || interoperability.TypeID == 9)
					{
						Array.Reverse(data);
					}
					else if (interoperability.TypeID == 5 || interoperability.TypeID == 10)
					{
						Array.Reverse(data, 0, 4);
						Array.Reverse(data, 4, 4);
					}
				}
				if (ifdtype == IFD.Zeroth && interoperability.TagID == 34665)
				{
					exifIFDFieldOffset = stream.Position;
				}
				else if (ifdtype == IFD.Zeroth && interoperability.TagID == 34853)
				{
					gpsIFDFieldOffset = stream.Position;
				}
				else if (ifdtype == IFD.EXIF && interoperability.TagID == 40965)
				{
					interopIFDFieldOffset = stream.Position;
				}
				else if (ifdtype == IFD.First && interoperability.TagID == 513)
				{
					thumbOffsetLocation = stream.Position;
				}
				else if (ifdtype == IFD.First && interoperability.TagID == 514)
				{
					thumbSizeLocation = stream.Position;
				}
				if (data.Length <= 4)
				{
					stream.Write(data, 0, data.Length);
					for (int i = data.Length; i < 4; i++)
					{
						stream.WriteByte(0);
					}
					continue;
				}
				stream.Write(bitConverterEx.GetBytes(num2 + num4), 0, 4);
				long position = stream.Position;
				stream.Seek(num3, SeekOrigin.Begin);
				for (int j = 0; j < num4; j++)
				{
					stream.WriteByte(byte.MaxValue);
				}
				stream.Write(data, 0, data.Length);
				stream.Seek(position, SeekOrigin.Begin);
				num2 = (uint)((int)num2 + ((int)num4 + data.Length));
				num3 += num4 + data.Length;
			}
			if (ifdtype == IFD.Zeroth)
			{
				firstIFDFieldOffset = stream.Position;
			}
			stream.Write(new byte[4], 0, 4);
			stream.Seek(num3, SeekOrigin.Begin);
			if (ifdtype == IFD.First)
			{
				if (Thumbnail != null)
				{
					MemoryStream memoryStream = new MemoryStream();
					Thumbnail.Save(memoryStream);
					memoryStream.Close();
					byte[] array = memoryStream.ToArray();
					thumbOffsetValue = (uint)(stream.Position - tiffoffset);
					thumbSizeValue = (uint)array.Length;
					stream.Write(array, 0, array.Length);
					memoryStream.Dispose();
				}
				else
				{
					thumbOffsetValue = 0u;
					thumbSizeValue = 0u;
				}
			}
		}

		public static ExifFile Read(string filename)
		{
			ExifFile exifFile = new ExifFile();
			exifFile.Properties = new Dictionary<ExifTag, ExifProperty>();
			exifFile.file = new JPEGFile(filename);
			exifFile.ReadAPP1();
			exifFile.makerNoteProcessed = false;
			return exifFile;
		}

		public static ExifFile Read(Stream fileStream)
		{
			ExifFile exifFile = new ExifFile();
			exifFile.Properties = new Dictionary<ExifTag, ExifProperty>();
			exifFile.file = new JPEGFile();
			if (!exifFile.file.Read(fileStream))
			{
				exifFile.file = null;
				exifFile = null;
				return null;
			}
			exifFile.ReadAPP1();
			exifFile.makerNoteProcessed = false;
			return exifFile;
		}
	}
}
