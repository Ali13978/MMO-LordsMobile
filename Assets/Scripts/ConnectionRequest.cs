using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	public struct ConnectionRequest
	{
		private readonly EndpointDetails mRemoteEndpoint;

		private readonly byte[] mPayload;

		public EndpointDetails RemoteEndpoint => mRemoteEndpoint;

		public byte[] Payload => mPayload;

		public ConnectionRequest(string remoteEndpointId, string remoteDeviceId, string remoteEndpointName, string serviceId, byte[] payload)
		{
			Logger.d("Constructing ConnectionRequest");
			mRemoteEndpoint = new EndpointDetails(remoteEndpointId, remoteDeviceId, remoteEndpointName, serviceId);
			mPayload = Misc.CheckNotNull(payload);
		}
	}
}
