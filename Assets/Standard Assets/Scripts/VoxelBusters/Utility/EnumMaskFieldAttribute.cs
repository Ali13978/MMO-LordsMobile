using System;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public class EnumMaskFieldAttribute : PropertyAttribute
	{
		private Type TargetType
		{
			get;
			set;
		}

		private EnumMaskFieldAttribute()
		{
		}

		public EnumMaskFieldAttribute(Type _targetType)
		{
			TargetType = _targetType;
		}

		public bool IsEnum()
		{
			return TargetType.IsEnum;
		}
	}
}
