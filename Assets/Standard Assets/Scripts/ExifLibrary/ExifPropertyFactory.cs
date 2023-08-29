using System;
using System.Text;

namespace ExifLibrary
{
	public static class ExifPropertyFactory
	{
		public static ExifProperty Get(ExifInterOperability interOperability, BitConverterEx.ByteOrder byteOrder, IFD ifd)
		{
			return Get(interOperability.TagID, interOperability.TypeID, interOperability.Count, interOperability.Data, byteOrder, ifd);
		}

		public static ExifProperty Get(ushort tag, ushort type, uint count, byte[] value, BitConverterEx.ByteOrder byteOrder, IFD ifd)
		{
			BitConverterEx bitConverterEx = new BitConverterEx(byteOrder, BitConverterEx.ByteOrder.System);
			switch (ifd)
			{
			case IFD.Zeroth:
				switch (tag)
				{
				case 259:
					return new ExifEnumProperty<Compression>(ExifTag.Compression, (Compression)bitConverterEx.ToUInt16(value, 0));
				case 262:
					return new ExifEnumProperty<PhotometricInterpretation>(ExifTag.PhotometricInterpretation, (PhotometricInterpretation)bitConverterEx.ToUInt16(value, 0));
				case 274:
					return new ExifEnumProperty<Orientation>(ExifTag.Orientation, (Orientation)bitConverterEx.ToUInt16(value, 0));
				case 284:
					return new ExifEnumProperty<PlanarConfiguration>(ExifTag.PlanarConfiguration, (PlanarConfiguration)bitConverterEx.ToUInt16(value, 0));
				case 531:
					return new ExifEnumProperty<YCbCrPositioning>(ExifTag.YCbCrPositioning, (YCbCrPositioning)bitConverterEx.ToUInt16(value, 0));
				case 296:
					return new ExifEnumProperty<ResolutionUnit>(ExifTag.ResolutionUnit, (ResolutionUnit)bitConverterEx.ToUInt16(value, 0));
				case 306:
					return new ExifDateTime(ExifTag.DateTime, ExifBitConverter.ToDateTime(value));
				}
				break;
			case IFD.EXIF:
				switch (tag)
				{
				case 36864:
					return new ExifVersion(ExifTag.ExifVersion, ExifBitConverter.ToAscii(value));
				case 40960:
					return new ExifVersion(ExifTag.FlashpixVersion, ExifBitConverter.ToAscii(value));
				case 40961:
					return new ExifEnumProperty<ColorSpace>(ExifTag.ColorSpace, (ColorSpace)bitConverterEx.ToUInt16(value, 0));
				case 37510:
				{
					byte[] array = new byte[8];
					byte[] array2 = new byte[value.Length - 8];
					Array.Copy(value, array, 8);
					Array.Copy(value, 8, array2, 0, value.Length - 8);
					Encoding uTF = Encoding.UTF8;
					string @string = uTF.GetString(array, 0, array.Length);
					uTF = ((@string == "ASCII\0\0\0") ? Encoding.UTF8 : ((@string == "JIS\0\0\0\0\0") ? Encoding.GetEncoding("Japanese (JIS 0208-1990 and 0212-1990)") : ((!(@string == "Unicode\0")) ? null : Encoding.Unicode)));
					int num = Array.IndexOf(array2, (byte)0);
					if (num == -1)
					{
						num = array2.Length;
					}
					return new ExifEncodedString(ExifTag.UserComment, (uTF != null) ? uTF.GetString(array2, 0, num) : Encoding.UTF8.GetString(array2, 0, num), uTF);
				}
				case 36867:
					return new ExifDateTime(ExifTag.DateTimeOriginal, ExifBitConverter.ToDateTime(value));
				case 36868:
					return new ExifDateTime(ExifTag.DateTimeDigitized, ExifBitConverter.ToDateTime(value));
				case 34850:
					return new ExifEnumProperty<ExposureProgram>(ExifTag.ExposureProgram, (ExposureProgram)bitConverterEx.ToUInt16(value, 0));
				case 37383:
					return new ExifEnumProperty<MeteringMode>(ExifTag.MeteringMode, (MeteringMode)bitConverterEx.ToUInt16(value, 0));
				case 37384:
					return new ExifEnumProperty<LightSource>(ExifTag.LightSource, (LightSource)bitConverterEx.ToUInt16(value, 0));
				case 37385:
					return new ExifEnumProperty<Flash>(ExifTag.Flash, (Flash)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 37396:
					switch (count)
					{
					case 3u:
						return new ExifCircularSubjectArea(ExifTag.SubjectArea, ExifBitConverter.ToUShortArray(value, (int)count, byteOrder));
					case 4u:
						return new ExifRectangularSubjectArea(ExifTag.SubjectArea, ExifBitConverter.ToUShortArray(value, (int)count, byteOrder));
					default:
						return new ExifPointSubjectArea(ExifTag.SubjectArea, ExifBitConverter.ToUShortArray(value, (int)count, byteOrder));
					}
				case 41488:
					return new ExifEnumProperty<ResolutionUnit>(ExifTag.FocalPlaneResolutionUnit, (ResolutionUnit)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41492:
					return new ExifPointSubjectArea(ExifTag.SubjectLocation, ExifBitConverter.ToUShortArray(value, (int)count, byteOrder));
				case 41495:
					return new ExifEnumProperty<SensingMethod>(ExifTag.SensingMethod, (SensingMethod)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41728:
					return new ExifEnumProperty<FileSource>(ExifTag.FileSource, (FileSource)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41729:
					return new ExifEnumProperty<SceneType>(ExifTag.SceneType, (SceneType)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41985:
					return new ExifEnumProperty<CustomRendered>(ExifTag.CustomRendered, (CustomRendered)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41986:
					return new ExifEnumProperty<ExposureMode>(ExifTag.ExposureMode, (ExposureMode)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41987:
					return new ExifEnumProperty<WhiteBalance>(ExifTag.WhiteBalance, (WhiteBalance)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41990:
					return new ExifEnumProperty<SceneCaptureType>(ExifTag.SceneCaptureType, (SceneCaptureType)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41991:
					return new ExifEnumProperty<GainControl>(ExifTag.GainControl, (GainControl)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41992:
					return new ExifEnumProperty<Contrast>(ExifTag.Contrast, (Contrast)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41993:
					return new ExifEnumProperty<Saturation>(ExifTag.Saturation, (Saturation)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41994:
					return new ExifEnumProperty<Sharpness>(ExifTag.Sharpness, (Sharpness)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				case 41996:
					return new ExifEnumProperty<SubjectDistanceRange>(ExifTag.SubjectDistance, (SubjectDistanceRange)bitConverterEx.ToUInt16(value, 0), isbitfield: true);
				}
				break;
			case IFD.GPS:
				switch (tag)
				{
				case 0:
					return new ExifVersion(ExifTag.GPSVersionID, ExifBitConverter.ToString(value));
				case 1:
					return new ExifEnumProperty<GPSLatitudeRef>(ExifTag.GPSLatitudeRef, (GPSLatitudeRef)value[0]);
				case 2:
					return new GPSLatitudeLongitude(ExifTag.GPSLatitude, ExifBitConverter.ToURationalArray(value, (int)count, byteOrder));
				case 3:
					return new ExifEnumProperty<GPSLongitudeRef>(ExifTag.GPSLongitudeRef, (GPSLongitudeRef)value[0]);
				case 4:
					return new GPSLatitudeLongitude(ExifTag.GPSLongitude, ExifBitConverter.ToURationalArray(value, (int)count, byteOrder));
				case 5:
					return new ExifEnumProperty<GPSAltitudeRef>(ExifTag.GPSAltitudeRef, (GPSAltitudeRef)value[0]);
				case 7:
					return new GPSTimeStamp(ExifTag.GPSTimeStamp, ExifBitConverter.ToURationalArray(value, (int)count, byteOrder));
				case 9:
					return new ExifEnumProperty<GPSStatus>(ExifTag.GPSStatus, (GPSStatus)value[0]);
				case 10:
					return new ExifEnumProperty<GPSMeasureMode>(ExifTag.GPSMeasureMode, (GPSMeasureMode)value[0]);
				case 12:
					return new ExifEnumProperty<GPSSpeedRef>(ExifTag.GPSSpeedRef, (GPSSpeedRef)value[0]);
				case 14:
					return new ExifEnumProperty<GPSDirectionRef>(ExifTag.GPSTrackRef, (GPSDirectionRef)value[0]);
				case 16:
					return new ExifEnumProperty<GPSDirectionRef>(ExifTag.GPSImgDirectionRef, (GPSDirectionRef)value[0]);
				case 19:
					return new ExifEnumProperty<GPSLatitudeRef>(ExifTag.GPSDestLatitudeRef, (GPSLatitudeRef)value[0]);
				case 20:
					return new GPSLatitudeLongitude(ExifTag.GPSDestLatitude, ExifBitConverter.ToURationalArray(value, (int)count, byteOrder));
				case 21:
					return new ExifEnumProperty<GPSLongitudeRef>(ExifTag.GPSDestLongitudeRef, (GPSLongitudeRef)value[0]);
				case 22:
					return new GPSLatitudeLongitude(ExifTag.GPSDestLongitude, ExifBitConverter.ToURationalArray(value, (int)count, byteOrder));
				case 23:
					return new ExifEnumProperty<GPSDirectionRef>(ExifTag.GPSDestBearingRef, (GPSDirectionRef)value[0]);
				case 25:
					return new ExifEnumProperty<GPSDistanceRef>(ExifTag.GPSDestDistanceRef, (GPSDistanceRef)value[0]);
				case 29:
					return new ExifDateTime(ExifTag.GPSDateStamp, ExifBitConverter.ToDateTime(value, hastime: false));
				case 30:
					return new ExifEnumProperty<GPSDifferential>(ExifTag.GPSDifferential, (GPSDifferential)bitConverterEx.ToUInt16(value, 0));
				}
				break;
			case IFD.Interop:
				switch (tag)
				{
				case 1:
					return new ExifAscii(ExifTag.InteroperabilityIndex, ExifBitConverter.ToAscii(value));
				case 2:
					return new ExifVersion(ExifTag.InteroperabilityVersion, ExifBitConverter.ToAscii(value));
				}
				break;
			case IFD.First:
				switch (tag)
				{
				case 259:
					return new ExifEnumProperty<Compression>(ExifTag.ThumbnailCompression, (Compression)bitConverterEx.ToUInt16(value, 0));
				case 262:
					return new ExifEnumProperty<PhotometricInterpretation>(ExifTag.ThumbnailPhotometricInterpretation, (PhotometricInterpretation)bitConverterEx.ToUInt16(value, 0));
				case 274:
					return new ExifEnumProperty<Orientation>(ExifTag.ThumbnailOrientation, (Orientation)bitConverterEx.ToUInt16(value, 0));
				case 284:
					return new ExifEnumProperty<PlanarConfiguration>(ExifTag.ThumbnailPlanarConfiguration, (PlanarConfiguration)bitConverterEx.ToUInt16(value, 0));
				case 531:
					return new ExifEnumProperty<YCbCrPositioning>(ExifTag.ThumbnailYCbCrPositioning, (YCbCrPositioning)bitConverterEx.ToUInt16(value, 0));
				case 296:
					return new ExifEnumProperty<ResolutionUnit>(ExifTag.ThumbnailResolutionUnit, (ResolutionUnit)bitConverterEx.ToUInt16(value, 0));
				case 306:
					return new ExifDateTime(ExifTag.ThumbnailDateTime, ExifBitConverter.ToDateTime(value));
				}
				break;
			}
			ExifTag exifTag = ExifTagFactory.GetExifTag(ifd, tag);
			switch (type)
			{
			case 1:
				if (count == 1)
				{
					return new ExifByte(exifTag, value[0]);
				}
				return new ExifByteArray(exifTag, value);
			case 2:
				return new ExifAscii(exifTag, ExifBitConverter.ToAscii(value));
			case 3:
				if (count == 1)
				{
					return new ExifUShort(exifTag, bitConverterEx.ToUInt16(value, 0));
				}
				return new ExifUShortArray(exifTag, ExifBitConverter.ToUShortArray(value, (int)count, byteOrder));
			case 4:
				if (count == 1)
				{
					return new ExifUInt(exifTag, bitConverterEx.ToUInt32(value, 0));
				}
				return new ExifUIntArray(exifTag, ExifBitConverter.ToUIntArray(value, (int)count, byteOrder));
			case 5:
				if (count == 1)
				{
					return new ExifURational(exifTag, ExifBitConverter.ToURational(value, byteOrder));
				}
				return new ExifURationalArray(exifTag, ExifBitConverter.ToURationalArray(value, (int)count, byteOrder));
			case 7:
				return new ExifUndefined(exifTag, value);
			case 9:
				if (count == 1)
				{
					return new ExifSInt(exifTag, bitConverterEx.ToInt32(value, 0));
				}
				return new ExifSIntArray(exifTag, ExifBitConverter.ToSIntArray(value, (int)count, byteOrder));
			case 10:
				if (count == 1)
				{
					return new ExifSRational(exifTag, ExifBitConverter.ToSRational(value, byteOrder));
				}
				return new ExifSRationalArray(exifTag, ExifBitConverter.ToSRationalArray(value, (int)count, byteOrder));
			default:
				throw new ArgumentException("Unknown property type.");
			}
		}
	}
}
