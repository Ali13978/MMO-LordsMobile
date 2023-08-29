using UnityEngine;

namespace VoxelBusters.Utility
{
	public class PlayerSettings : MonoBehaviour
	{
		public static string GetBundleVersion()
		{
			return NativeBinding.GetBundleVersion();
		}

		public static string GetBundleIdentifier()
		{
			return NativeBinding.GetBundleIdentifier();
		}
	}
}
