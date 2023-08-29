using UnityEngine;

namespace AudienceNetwork
{
	internal class AdSettingsBridgeAndroid : AdSettingsBridge
	{
		public override void addTestDevice(string deviceID)
		{
			AndroidJavaClass adSettingsObject = getAdSettingsObject();
			adSettingsObject.CallStatic("addTestDevice", deviceID);
		}

		public override void setUrlPrefix(string urlPrefix)
		{
			AndroidJavaClass adSettingsObject = getAdSettingsObject();
			adSettingsObject.CallStatic("setUrlPrefix", urlPrefix);
		}

		private AndroidJavaClass getAdSettingsObject()
		{
			return new AndroidJavaClass("com.facebook.ads.AdSettings");
		}
	}
}
