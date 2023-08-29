using System;

namespace BackendlessAPI.Logging
{
	internal class LogMessage
	{
		public DateTime timestamp
		{
			get;
			set;
		}

		public string message
		{
			get;
			set;
		}

		public string exception
		{
			get;
			set;
		}

		internal LogMessage(DateTime timestamp, string message, string exception)
		{
			this.timestamp = timestamp;
			this.message = message;
			this.exception = exception;
		}
	}
}
