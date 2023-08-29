using System;

namespace BackendlessAPI.Logging
{
	public class Logger
	{
		private string loggerName;

		internal Logger(string loggerName)
		{
			this.loggerName = loggerName;
		}

		public void trace(string message)
		{
			Backendless.Logging.Buffer.Enqueue(loggerName, "TRACE", message, null);
		}

		public void debug(string message)
		{
			Backendless.Logging.Buffer.Enqueue(loggerName, "DEBUG", message, null);
		}

		public void info(string message)
		{
			Backendless.Logging.Buffer.Enqueue(loggerName, "INFO", message, null);
		}

		public void warn(string message)
		{
			Backendless.Logging.Buffer.Enqueue(loggerName, "WARN", message, null);
		}

		public void warn(string message, System.Exception e)
		{
			Backendless.Logging.Buffer.Enqueue(loggerName, "WARN", message, e);
		}

		public void error(string message)
		{
			Backendless.Logging.Buffer.Enqueue(loggerName, "ERROR", message, null);
		}

		public void error(string message, System.Exception e)
		{
			Backendless.Logging.Buffer.Enqueue(loggerName, "ERROR", message, e);
		}

		public void fatal(string message)
		{
			Backendless.Logging.Buffer.Enqueue(loggerName, "FATAL", message, null);
		}

		public void fatal(string message, System.Exception e)
		{
			Backendless.Logging.Buffer.Enqueue(loggerName, "FATAL", message, e);
		}
	}
}
