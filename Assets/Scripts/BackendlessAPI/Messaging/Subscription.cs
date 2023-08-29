using BackendlessAPI.Async;
using BackendlessAPI.LitJson;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BackendlessAPI.Messaging
{
	public class Subscription
	{
		private Timer _timer;

		private int _pollingInterval = 1000;

		[JsonProperty("subscriptionId")]
		public string SubscriptionId
		{
			get;
			set;
		}

		[JsonProperty("channelName")]
		public string ChannelName
		{
			get;
			set;
		}

		[JsonProperty("pollingInterval")]
		public int PollingInterval
		{
			get
			{
				return _pollingInterval;
			}
			set
			{
				_pollingInterval = value;
			}
		}

		public Subscription()
		{
		}

		public Subscription(int pollingInterval)
		{
			_pollingInterval = pollingInterval;
		}

		public bool CancelSubscription()
		{
			if (_timer != null)
			{
				_timer.Change(-1, -1);
				_timer = null;
			}
			SubscriptionId = null;
			return true;
		}

		public void PauseSubscription()
		{
			if (_timer != null)
			{
				_timer.Change(-1, -1);
			}
		}

		public void ResumeSubscription()
		{
			if (SubscriptionId == null || ChannelName == null || _timer == null)
			{
				throw new ArgumentNullException("cannot resume a subscription, which is not paused.");
			}
			_timer.Change(0, _pollingInterval);
		}

		public void OnSubscribe(AsyncCallback<List<Message>> callback)
		{
			_timer = new Timer(delegate(object c)
			{
				List<Message> list = Backendless.Messaging.PollMessages(ChannelName, SubscriptionId);
				if (list.Count != 0)
				{
					AsyncCallback<List<Message>> asyncCallback = (AsyncCallback<List<Message>>)c;
					asyncCallback.ResponseHandler(list);
				}
			}, callback, 0, _pollingInterval);
		}
	}
}
