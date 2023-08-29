using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	public class PlatformValueHelper
	{
		public static PlatformValue GetCurrentPlatformValue(PlatformValue[] _array)
		{
			if (_array == null)
			{
				return null;
			}
			eRuntimePlatform _platform = GetRuntimePlatform();
			return Array.Find(_array, (PlatformValue _entry) => _entry.Platform == _platform);
		}

		private static eRuntimePlatform GetRuntimePlatform()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.IPhonePlayer:
				return eRuntimePlatform.IOS;
			case RuntimePlatform.Android:
				return eRuntimePlatform.ANDROID;
			default:
				return eRuntimePlatform.UNKNOWN;
			}
		}
	}
}
