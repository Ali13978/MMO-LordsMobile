using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	public struct EndpointDetails
	{
		private readonly string mEndpointId;

		private readonly string mDeviceId;

		private readonly string mName;

		private readonly string mServiceId;

		public string EndpointId => mEndpointId;

		public string DeviceId => mDeviceId;

		public string Name => mName;

		public string ServiceId => mServiceId;

		public EndpointDetails(string endpointId, string deviceId, string name, string serviceId)
		{
			mEndpointId = Misc.CheckNotNull(endpointId);
			mDeviceId = Misc.CheckNotNull(deviceId);
			mName = Misc.CheckNotNull(name);
			mServiceId = Misc.CheckNotNull(serviceId);
		}
	}
}
