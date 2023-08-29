namespace AudienceNetwork
{
	internal interface IAdSettingsBridge
	{
		void addTestDevice(string deviceID);

		void setUrlPrefix(string urlPrefix);
	}
}
