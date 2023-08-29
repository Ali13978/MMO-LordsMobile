using BackendlessAPI.LitJson;

namespace BackendlessAPI.Messaging
{
	public class SubscriptionOptions
	{
		[JsonProperty("subscriberId")]
		public string SubscriberId
		{
			get;
			set;
		}

		[JsonProperty("subtopic")]
		public string Subtopic
		{
			get;
			set;
		}

		[JsonProperty("selector")]
		public string Selector
		{
			get;
			set;
		}

		public SubscriptionOptions()
		{
		}

		public SubscriptionOptions(string subscriberId)
		{
			SubscriberId = subscriberId;
		}

		public SubscriptionOptions(string subscriberId, string subtopic)
		{
			SubscriberId = subscriberId;
			Subtopic = subtopic;
		}

		public SubscriptionOptions(string subscriberId, string subtopic, string selector)
		{
			SubscriberId = subscriberId;
			Subtopic = subtopic;
			Selector = selector;
		}
	}
}
