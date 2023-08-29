using System;

namespace ExifLibrary
{
	public class ExifEnumProperty<T> : ExifProperty
	{
		protected T mValue;

		protected bool mIsBitField;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (T)value;
			}
		}

		public new T Value
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

		public bool IsBitField => mIsBitField;

		public override ExifInterOperability Interoperability
		{
			get
			{
				ushort tagID = ExifTagFactory.GetTagID(mTag);
				Type typeFromHandle = typeof(T);
				Type underlyingType = Enum.GetUnderlyingType(typeFromHandle);
				if (typeFromHandle == typeof(FileSource) || typeFromHandle == typeof(SceneType))
				{
					return new ExifInterOperability(tagID, 7, 1u, new byte[1]
					{
						(byte)(object)mValue
					});
				}
				if (typeFromHandle == typeof(GPSLatitudeRef) || typeFromHandle == typeof(GPSLongitudeRef) || typeFromHandle == typeof(GPSStatus) || typeFromHandle == typeof(GPSMeasureMode) || typeFromHandle == typeof(GPSSpeedRef) || typeFromHandle == typeof(GPSDirectionRef) || typeFromHandle == typeof(GPSDistanceRef))
				{
					return new ExifInterOperability(tagID, 2, 2u, new byte[2]
					{
						(byte)(object)mValue,
						0
					});
				}
				if (underlyingType == typeof(byte))
				{
					return new ExifInterOperability(tagID, 1, 1u, new byte[1]
					{
						(byte)(object)mValue
					});
				}
				if (underlyingType == typeof(ushort))
				{
					return new ExifInterOperability(tagID, 3, 1u, BitConverterEx.GetBytes((ushort)(object)mValue, BitConverterEx.ByteOrder.System, BitConverterEx.ByteOrder.System));
				}
				throw new UnknownEnumTypeException();
			}
		}

		public ExifEnumProperty(ExifTag tag, T value, bool isbitfield)
			: base(tag)
		{
			mValue = value;
			mIsBitField = isbitfield;
		}

		public ExifEnumProperty(ExifTag tag, T value)
			: this(tag, value, isbitfield: false)
		{
		}

		public override string ToString()
		{
			return mValue.ToString();
		}

		public static implicit operator T(ExifEnumProperty<T> obj)
		{
			return obj.mValue;
		}
	}
}
