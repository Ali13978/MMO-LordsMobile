using System.Collections.Generic;

namespace BackendlessAPI.Logging
{
	internal class LogBatch
	{
		public string logLevel;

		public string logger;

		public LinkedList<LogMessage> messages;
	}
}
