using System;
using System.Collections.Generic;

namespace BackendlessAPI.Messaging
{
	public class DeliveryOptions
	{
		public const int IOS = 1;

		public const int ANDROID = 2;

		public const int WP = 4;

		public const int ALL = 7;

		private PushPolicyEnum _pushPolicy = PushPolicyEnum.ALSO;

		public int PushBroadcast
		{
			get;
			set;
		}

		public List<string> PushSinglecast
		{
			get;
			set;
		}

		public DateTime? PublishAt
		{
			get;
			set;
		}

		public long RepeatEvery
		{
			get;
			set;
		}

		public DateTime? RepeatExpiresAt
		{
			get;
			set;
		}

		public PushPolicyEnum PushPolicy
		{
			get
			{
				return _pushPolicy;
			}
			set
			{
				_pushPolicy = value;
			}
		}
	}
}
