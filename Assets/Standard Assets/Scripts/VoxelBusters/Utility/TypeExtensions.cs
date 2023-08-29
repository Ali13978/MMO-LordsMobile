using System;

namespace VoxelBusters.Utility
{
	public static class TypeExtensions
	{
		private static Type[] typeCodesToType;

		static TypeExtensions()
		{
			typeCodesToType = new Type[19];
			typeCodesToType[1] = typeof(object);
			typeCodesToType[2] = typeof(DBNull);
			typeCodesToType[3] = typeof(bool);
			typeCodesToType[4] = typeof(char);
			typeCodesToType[5] = typeof(sbyte);
			typeCodesToType[6] = typeof(byte);
			typeCodesToType[7] = typeof(short);
			typeCodesToType[8] = typeof(ushort);
			typeCodesToType[9] = typeof(int);
			typeCodesToType[10] = typeof(uint);
			typeCodesToType[11] = typeof(long);
			typeCodesToType[12] = typeof(ulong);
			typeCodesToType[13] = typeof(float);
			typeCodesToType[14] = typeof(double);
			typeCodesToType[15] = typeof(decimal);
			typeCodesToType[16] = typeof(DateTime);
			typeCodesToType[18] = typeof(string);
		}

		public static object DefaultValue(this Type _type)
		{
			if (_type.IsValueType)
			{
				return Activator.CreateInstance(_type);
			}
			return null;
		}

		public static Type GetTypeFromTypeCode(this TypeCode _typeCode)
		{
			return typeCodesToType[(int)_typeCode];
		}
	}
}
