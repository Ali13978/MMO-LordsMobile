using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class PlatformValue
	{
		[SerializeField]
		private eRuntimePlatform m_platform;

		[SerializeField]
		private string m_value;

		public eRuntimePlatform Platform
		{
			get
			{
				return m_platform;
			}
			private set
			{
				m_platform = value;
			}
		}

		public string Value
		{
			get
			{
				return m_value;
			}
			private set
			{
				m_value = value;
			}
		}

		private PlatformValue()
		{
		}

		public static PlatformValue IOS(string _identifier)
		{
			PlatformValue platformValue = new PlatformValue();
			platformValue.Platform = eRuntimePlatform.IOS;
			platformValue.Value = _identifier;
			return platformValue;
		}

		public static PlatformValue Android(string _identifier)
		{
			PlatformValue platformValue = new PlatformValue();
			platformValue.Platform = eRuntimePlatform.ANDROID;
			platformValue.Value = _identifier;
			return platformValue;
		}

		public static PlatformValue Amazon(string _identifier)
		{
			PlatformValue platformValue = new PlatformValue();
			platformValue.Platform = eRuntimePlatform.AMAZON;
			platformValue.Value = _identifier;
			return platformValue;
		}
	}
}
