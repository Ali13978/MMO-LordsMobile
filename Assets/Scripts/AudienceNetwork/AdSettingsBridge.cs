using UnityEngine;

namespace AudienceNetwork
{
	internal class AdSettingsBridge : IAdSettingsBridge
	{
		public static readonly IAdSettingsBridge Instance;

		internal AdSettingsBridge()
		{
		}

		static AdSettingsBridge()
		{
			Instance = createInstance();
		}

		private static IAdSettingsBridge createInstance()
		{
			if (Application.platform != 0)
			{
				return new AdSettingsBridgeAndroid();
			}
			return new AdSettingsBridge();
		}

		public virtual void addTestDevice(string deviceID)
		{
		}

		public virtual void setUrlPrefix(string urlPrefix)
		{
		}
	}
}
