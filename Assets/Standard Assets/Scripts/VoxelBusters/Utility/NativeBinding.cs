using UnityEngine;

namespace VoxelBusters.Utility
{
	public class NativeBinding : MonoBehaviour
	{
		private class NativeInfo
		{
			public class Class
			{
				public const string NAME = "com.voxelbusters.utility.NativePlatformInfo";
			}

			public class Methods
			{
				public const string GET_BUILD_IDENTIFIER = "getPackageName";

				public const string GET_BUILD_VERSION = "getPackageVersionName";
			}
		}

		private static AndroidJavaClass m_nativeBinding;

		private static AndroidJavaClass PluginNativeBinding
		{
			get
			{
				if (m_nativeBinding == null)
				{
					m_nativeBinding = new AndroidJavaClass("com.voxelbusters.utility.NativePlatformInfo");
				}
				return m_nativeBinding;
			}
			set
			{
				m_nativeBinding = value;
			}
		}

		public static string GetBundleVersion()
		{
			return PluginNativeBinding.CallStatic<string>("getPackageVersionName", new object[0]);
		}

		public static string GetBundleIdentifier()
		{
			return PluginNativeBinding.CallStatic<string>("getPackageName", new object[0]);
		}
	}
}
