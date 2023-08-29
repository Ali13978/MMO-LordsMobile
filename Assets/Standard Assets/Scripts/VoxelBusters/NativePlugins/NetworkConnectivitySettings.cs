using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class NetworkConnectivitySettings
	{
		[Serializable]
		public class AndroidSettings
		{
			[Tooltip("The connection port of the host. For DNS IP, it will be 53 or else 80.")]
			[SerializeField]
			private int m_port = 53;

			public int Port => m_port;
		}

		[SerializeField]
		[Tooltip("The host IP address in IPv4 format.")]
		private string m_hostAddressIPV4 = "8.8.8.8";

		[SerializeField]
		[Tooltip("The host IP address in IPv6 format.")]
		private string m_hostAddressIPV6 = "0:0:0:0:0:FFFF:0808:0808";

		[SerializeField]
		[Tooltip("The number of seconds to wait before the request times out.")]
		private int m_timeOutPeriod = 60;

		[SerializeField]
		[Tooltip("The number of retry attempts, when a response is not received from the host.")]
		private int m_maxRetryCount = 2;

		[Tooltip("The time interval between consecutive poll.")]
		[SerializeField]
		private float m_timeGapBetweenPolling = 2f;

		[SerializeField]
		private AndroidSettings m_android = new AndroidSettings();

		public string HostAddress => (Application.platform != RuntimePlatform.IPhonePlayer) ? m_hostAddressIPV4 : m_hostAddressIPV6;

		public int TimeOutPeriod => m_timeOutPeriod;

		public int MaxRetryCount => m_maxRetryCount;

		public float TimeGapBetweenPolling => m_timeGapBetweenPolling;

		public AndroidSettings Android => m_android;
	}
}
