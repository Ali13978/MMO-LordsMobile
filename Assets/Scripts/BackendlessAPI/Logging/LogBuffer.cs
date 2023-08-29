using System;
using System.Collections.Generic;
using System.Threading;

namespace BackendlessAPI.Logging
{
	internal class LogBuffer
	{
		private int numOfMessages;

		private int timeFrequency;

		private Dictionary<string, Dictionary<string, LinkedList<LogMessage>>> logBatches;

		private int messageCount;

		private Mutex mutex;

		private Timer timer;

		private TimerCallback timerCallback;

		internal LogBuffer()
		{
			mutex = new Mutex(initiallyOwned: false, "LogBufferMutex");
			numOfMessages = 100;
			timeFrequency = 300000;
			logBatches = new Dictionary<string, Dictionary<string, LinkedList<LogMessage>>>();
			messageCount = 0;
			setupTimer();
		}

		private void setupTimer()
		{
			if (timer != null)
			{
				timer.Dispose();
				timer = null;
			}
			if (timerCallback == null)
			{
				timerCallback = FlushMessages;
			}
			if (numOfMessages > 1)
			{
				timer = new Timer(timerCallback, null, 0, timeFrequency);
			}
		}

		public void FlushMessages(object stateInfo)
		{
			mutex.WaitOne();
			Flush();
			mutex.ReleaseMutex();
		}

		public void SetLogReportingPolicy(int numOfMessages, int timeFrequency)
		{
			if (numOfMessages > 1 && timeFrequency <= 0)
			{
				throw new System.Exception("the time frequency argument must be greater than zero");
			}
			this.numOfMessages = numOfMessages;
			this.timeFrequency = timeFrequency;
			setupTimer();
		}

		internal void Enqueue(string logger, string logLevel, string message, System.Exception error)
		{
			if (numOfMessages == 1)
			{
				Backendless.Logging.ReportSingleLogMessage(logger, logLevel, message, error);
				return;
			}
			mutex.WaitOne();
			Dictionary<string, LinkedList<LogMessage>> dictionary;
			if (logBatches.ContainsKey(logger))
			{
				dictionary = logBatches[logger];
			}
			else
			{
				dictionary = new Dictionary<string, LinkedList<LogMessage>>();
				logBatches[logger] = dictionary;
			}
			LinkedList<LogMessage> linkedList2 = dictionary.ContainsKey(logLevel) ? dictionary[logLevel] : (dictionary[logLevel] = new LinkedList<LogMessage>());
			linkedList2.AddLast(new LogMessage(DateTime.Now, message, error?.StackTrace));
			messageCount++;
			if (messageCount == numOfMessages)
			{
				Flush();
			}
			mutex.ReleaseMutex();
		}

		private void Flush()
		{
			LinkedList<LogBatch> linkedList = new LinkedList<LogBatch>();
			foreach (string key in logBatches.Keys)
			{
				Dictionary<string, LinkedList<LogMessage>> dictionary = logBatches[key];
				foreach (string key2 in dictionary.Keys)
				{
					LogBatch logBatch = new LogBatch();
					logBatch.logger = key;
					logBatch.logLevel = key2;
					logBatch.messages = dictionary[key2];
					linkedList.AddLast(logBatch);
				}
			}
			logBatches.Clear();
			LogBatch[] array = new LogBatch[linkedList.Count];
			linkedList.CopyTo(array, 0);
			Backendless.Logging.ReportBatch(array);
		}
	}
}
