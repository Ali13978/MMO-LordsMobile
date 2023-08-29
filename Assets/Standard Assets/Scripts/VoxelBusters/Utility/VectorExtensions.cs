using System;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public static class VectorExtensions
	{
		public static Vector2 Rotate(this Vector2 _vec, float _angleDeg)
		{
			float f = (float)Math.PI / 180f * _angleDeg;
			float num = Mathf.Cos(f);
			float num2 = Mathf.Sin(f);
			Vector2 zero = Vector2.zero;
			zero.x = _vec.x * num - _vec.y * num2;
			zero.y = _vec.x * num2 + _vec.y * num;
			return zero;
		}
	}
}
