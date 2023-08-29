using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	public class PlatformBindingHelper : MonoBehaviour
	{
		private class NativeInfo
		{
			public class Class
			{
				public const string NATIVE_BINDING_NAME = "com.voxelbusters.NativeBinding";
			}

			public class Methods
			{
				public const string ON_PAUSE = "onApplicationPause";

				public const string ON_RESUME = "onApplicationResume";

				public const string ON_QUIT = "onApplicationQuit";

				public const string ENABLE_DEBUG = "enableDebug";
			}
		}

		private AndroidJavaClass m_nativeBinding;

		private AndroidJavaClass PluginNativeBinding
		{
			get
			{
				if (m_nativeBinding == null)
				{
					m_nativeBinding = AndroidPluginUtility.CreateClassObject("com.voxelbusters.NativeBinding");
				}
				return m_nativeBinding;
			}
			set
			{
				m_nativeBinding = value;
			}
		}

		private void InitializeAndroidSettings()
		{
			bool isDebugMode = NPSettings.Application.IsDebugMode;
			PluginNativeBinding.CallStatic("enableDebug", isDebugMode);
		}

		private void OnApplicationPause(bool paused)
		{
			if (paused)
			{
				PluginNativeBinding.CallStatic("onApplicationPause");
			}
			else
			{
				PluginNativeBinding.CallStatic("onApplicationResume");
			}
		}

		private void OnApplicationQuit()
		{
			PluginNativeBinding.CallStatic("onApplicationQuit");
		}

		private void Awake()
		{
			InitializeAndroidSettings();
		}
	}
}
