using System;

namespace VoxelBusters.Utility
{
	public static class EnumerationExtensions
	{
		public static int GetValue(this Enum _enum)
		{
			int num = 0;
			Type type = _enum.GetType();
			foreach (int value in Enum.GetValues(type))
			{
				if (((int)(object)_enum & value) != 0)
				{
					num |= value;
				}
			}
			return num;
		}
	}
}
