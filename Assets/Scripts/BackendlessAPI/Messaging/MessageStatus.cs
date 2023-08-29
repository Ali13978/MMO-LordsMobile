using BackendlessAPI.LitJson;

namespace BackendlessAPI.Messaging
{
	public class MessageStatus
	{
		[JsonProperty("status")]
		public PublishStatusEnum Status
		{
			get;
			set;
		}

		[JsonProperty("messageId")]
		public string MessageId
		{
			get;
			set;
		}

		[JsonProperty("errorMessage")]
		public string ErrorMessage
		{
			get;
			set;
		}

		public MessageStatus()
		{
		}

		public MessageStatus(string messageId)
		{
			MessageId = messageId;
		}
	}
}
