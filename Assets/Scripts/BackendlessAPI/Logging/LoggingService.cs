using BackendlessAPI.Engine;
using BackendlessAPI.LitJson;
using System;
using System.Collections.Generic;

namespace BackendlessAPI.Logging
{
	public class LoggingService
	{
		private class RequestLog
		{
			[JsonProperty("log-level")]
			public string LogLevel
			{
				get;
				set;
			}

			[JsonProperty("logger")]
			public string Logger
			{
				get;
				set;
			}

			[JsonProperty("timestamp")]
			public DateTime Timestamp
			{
				get;
				set;
			}

			[JsonProperty("message")]
			public string Message
			{
				get;
				set;
			}

			[JsonProperty("exception")]
			public string Exception
			{
				get;
				set;
			}
		}

		private LogBuffer buffer;

		private Dictionary<string, Logger> loggers;

		internal LogBuffer Buffer => buffer;

		public LoggingService()
		{
			buffer = new LogBuffer();
			loggers = new Dictionary<string, Logger>();
		}

		public void SetLogReportingPolicy(int numOfMessages, int timeFrequencyMS)
		{
			buffer.SetLogReportingPolicy(numOfMessages, timeFrequencyMS);
		}

		public Logger GetLogger(Type loggerType)
		{
			return GetLogger(loggerType.Name);
		}

		public Logger GetLogger(string loggerName)
		{
			if (loggers.ContainsKey(loggerName))
			{
				return loggers[loggerName];
			}
			Logger logger = new Logger(loggerName);
			loggers[loggerName] = logger;
			return logger;
		}

		internal void ReportSingleLogMessage(string logger, string loglevel, string message, System.Exception error)
		{
			List<RequestLog> list = new List<RequestLog>();
			RequestLog requestLog = new RequestLog();
			requestLog.LogLevel = loglevel;
			requestLog.Logger = logger;
			requestLog.Timestamp = DateTime.Now;
			requestLog.Message = message;
			requestLog.Exception = error?.StackTrace;
			list.Add(requestLog);
			Invoker.InvokeSync<object>(Invoker.Api.LOGGERSERVICE_PUT, new object[1]
			{
				list
			});
		}

		internal void ReportBatch(LogBatch[] logBatchs)
		{
			if (logBatchs.Length != 0)
			{
				List<RequestLog> list = new List<RequestLog>();
				foreach (LogBatch logBatch in logBatchs)
				{
					foreach (LogMessage message in logBatch.messages)
					{
						RequestLog requestLog = new RequestLog();
						requestLog.LogLevel = logBatch.logLevel;
						requestLog.Logger = logBatch.logger;
						requestLog.Timestamp = message.timestamp;
						requestLog.Message = message.message;
						requestLog.Exception = message.exception;
						list.Add(requestLog);
					}
				}
				Invoker.InvokeSync<object>(Invoker.Api.LOGGERSERVICE_PUT, new object[1]
				{
					list
				});
			}
		}
	}
}
