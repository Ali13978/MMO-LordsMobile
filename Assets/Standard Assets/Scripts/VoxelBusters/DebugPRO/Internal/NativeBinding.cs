using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.DebugPRO.Internal
{
	public class NativeBinding : MonoBehaviour
	{
		private class NativeInfo
		{
			public class Class
			{
				public const string NATIVE_BINDING_NAME = "com.voxelbusters.NativeBinding";
			}

			public class Methods
			{
				public const string LOG = "logMessage";
			}
		}

		private static AndroidJavaClass m_nativeBinding;

		private static Dictionary<eConsoleLogType, string> kLogTypeMap = new Dictionary<eConsoleLogType, string>
		{
			{
				eConsoleLogType.ERROR,
				"ERROR"
			},
			{
				eConsoleLogType.ASSERT,
				"ASSERT"
			},
			{
				eConsoleLogType.WARNING,
				"WARNING"
			},
			{
				eConsoleLogType.INFO,
				"INFO"
			},
			{
				eConsoleLogType.EXCEPTION,
				"EXCEPTION"
			}
		};

		private static AndroidJavaClass PluginNativeBinding
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

		public static void Log(ConsoleLog _log)
		{
			PluginNativeBinding.CallStatic("logMessage", _log.Message.ToBase64(), kLogTypeMap[_log.Type], _log.CallStack.ToBase64());
		}
	}
}
