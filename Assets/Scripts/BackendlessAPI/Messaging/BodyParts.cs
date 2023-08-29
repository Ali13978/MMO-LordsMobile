using BackendlessAPI.LitJson;

namespace BackendlessAPI.Messaging
{
	public class BodyParts
	{
		private string textMessage;

		private string htmlMessage;

		[JsonProperty("textmessage")]
		public string TextMessage => textMessage;

		[JsonProperty("htmlmessage")]
		public string HtmlMessage => htmlMessage;

		public BodyParts(string textMessage, string htmlMessage)
		{
			this.textMessage = textMessage;
			this.htmlMessage = htmlMessage;
		}
	}
}
