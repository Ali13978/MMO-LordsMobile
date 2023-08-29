using BackendlessAPI.Async;
using BackendlessAPI.Engine;
using BackendlessAPI.Exception;
using BackendlessAPI.Messaging;
using System;
using System.Collections.Generic;

namespace BackendlessAPI.Service
{
	public class MessagingService
	{
		public delegate void UnityRegisterDevice(string GCMSenderID, long timestamp);

		public delegate void UnityUnregisterDevice();

		private static string DEFAULT_CHANNEL_NAME = "default";

		private static DeviceRegistration _deviceRegistration;

		private static AsyncCallback<string> _deviceRegisterCallback;

		private static AsyncCallback<bool> _deviceUnregisterCallback;

		private UnityRegisterDevice _unityRegisterDevice;

		private UnityUnregisterDevice _unityUnregisterDevice;

		public DeviceRegistration DeviceRegistration => _deviceRegistration;

		public MessagingService()
		{
			_deviceRegistration = new DeviceRegistration();
		}

		public void SetUnityRegisterDevice(UnityRegisterDevice unityRegisterDevice, UnityUnregisterDevice unityUnregisterDevice)
		{
			_unityRegisterDevice = unityRegisterDevice;
			_unityUnregisterDevice = unityUnregisterDevice;
		}

		public void RegisterDevice(string GCMSenderID)
		{
			RegisterDevice(GCMSenderID, (AsyncCallback<string>)null);
		}

		public void RegisterDevice(string GCMSenderID, AsyncCallback<string> callback)
		{
			RegisterDevice(GCMSenderID, DEFAULT_CHANNEL_NAME, callback);
		}

		public void RegisterDevice(string GCMSenderID, string channel)
		{
			RegisterDevice(GCMSenderID, channel, null);
		}

		public void RegisterDevice(string GCMSenderID, string channel, AsyncCallback<string> callback)
		{
			if (string.IsNullOrEmpty(channel))
			{
				throw new ArgumentNullException("Channel name cannot be null or empty.");
			}
			RegisterDevice(GCMSenderID, new List<string>
			{
				channel
			}, callback);
		}

		public void RegisterDevice(string GCMSenderID, List<string> channels)
		{
			RegisterDevice(GCMSenderID, channels, (AsyncCallback<string>)null);
		}

		public void RegisterDevice(string GCMSenderID, List<string> channels, AsyncCallback<string> callback)
		{
			RegisterDevice(GCMSenderID, channels, null, callback);
		}

		public void RegisterDevice(string GCMSenderID, DateTime expiration)
		{
			RegisterDevice(GCMSenderID, expiration, null);
		}

		public void RegisterDevice(string GCMSenderID, DateTime expiration, AsyncCallback<string> callback)
		{
			RegisterDevice(GCMSenderID, null, expiration, callback);
		}

		public void RegisterDevice(string GCMSenderID, List<string> channels, DateTime? expiration)
		{
			RegisterDevice(GCMSenderID, channels, expiration, null);
		}

		public void RegisterDevice(string GCMSenderID, List<string> channels, DateTime? expiration, AsyncCallback<string> callback)
		{
			if (channels == null)
			{
				throw new ArgumentNullException("Channel name cannot be null or empty.");
			}
			if (channels.Count == 0)
			{
				_deviceRegistration.AddChannel(DEFAULT_CHANNEL_NAME);
			}
			foreach (string channel in channels)
			{
				checkChannelName(channel);
				_deviceRegistration.AddChannel(channel);
			}
			_deviceRegistration.Expiration = expiration;
			_deviceRegisterCallback = callback;
			long timestamp = _deviceRegistration.Timestamp.HasValue ? _deviceRegistration.Timestamp.Value : 0;
			_unityRegisterDevice(GCMSenderID, timestamp);
		}

		public void RegisterDeviceOnServer()
		{
			AsyncCallback<string> callback = _deviceRegisterCallback;
			AsyncCallback<Dictionary<string, object>> callback2 = new AsyncCallback<Dictionary<string, object>>(delegate(Dictionary<string, object> response)
			{
				if (callback != null)
				{
					callback.ResponseHandler(GetRegisterDeviceOnServerResult(response));
				}
			}, delegate(BackendlessFault fault)
			{
				if (callback != null)
				{
					callback.ErrorHandler(fault);
					return;
				}
				throw new BackendlessException(fault);
			});
			Invoker.InvokeAsync(Invoker.Api.MESSAGINGSERVICE_REGISTERDEVICEONSERVER, new object[1]
			{
				GetRegisterDeviceOnServerRequestData(_deviceRegistration)
			}, callback2);
		}

		public List<DeviceRegistration> GetRegistrations()
		{
			return Invoker.InvokeSync<List<DeviceRegistration>>(Invoker.Api.MESSAGINGSERVICE_GETREGISTRATION, new object[2]
			{
				null,
				GetDeviceId()
			});
		}

		public void GetRegistrations(AsyncCallback<List<DeviceRegistration>> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.MESSAGINGSERVICE_GETREGISTRATION, new object[2]
			{
				null,
				GetDeviceId()
			}, callback);
		}

		public void UnregisterDevice(AsyncCallback<bool> callback)
		{
			_deviceUnregisterCallback = callback;
			_unityUnregisterDevice();
		}

		public void UnregisterDeviceOnServer()
		{
			AsyncCallback<bool> callback = _deviceUnregisterCallback;
			AsyncCallback<Dictionary<string, object>> callback2 = new AsyncCallback<Dictionary<string, object>>(delegate(Dictionary<string, object> response)
			{
				if (callback != null)
				{
					callback.ResponseHandler(GetUnregisterDeviceOnServerResult(response));
				}
			}, delegate(BackendlessFault fault)
			{
				if (callback != null)
				{
					callback.ErrorHandler(fault);
					return;
				}
				throw new BackendlessException(fault);
			});
			Invoker.InvokeAsync(Invoker.Api.MESSAGINGSERVICE_UNREGISTERDEVICEONSERVER, new object[2]
			{
				null,
				_deviceRegistration.DeviceId
			}, callback2);
		}

		public MessageStatus Publish(object message)
		{
			return Publish(message, DEFAULT_CHANNEL_NAME);
		}

		public MessageStatus Publish(object message, PublishOptions publishOptions)
		{
			return Publish(message, DEFAULT_CHANNEL_NAME, publishOptions);
		}

		public MessageStatus Publish(object message, DeliveryOptions deliveryOptions)
		{
			return Publish(message, DEFAULT_CHANNEL_NAME, null, deliveryOptions);
		}

		public MessageStatus Publish(object message, PublishOptions publishOptions, DeliveryOptions deliveryOptions)
		{
			return Publish(message, DEFAULT_CHANNEL_NAME, publishOptions, deliveryOptions);
		}

		public void Publish(object message, AsyncCallback<MessageStatus> callback)
		{
			Publish(message, DEFAULT_CHANNEL_NAME, callback);
		}

		public void Publish(object message, PublishOptions publishOptions, AsyncCallback<MessageStatus> callback)
		{
			Publish(message, DEFAULT_CHANNEL_NAME, publishOptions, callback);
		}

		public void Publish(object message, DeliveryOptions deliveryOptions, AsyncCallback<MessageStatus> callback)
		{
			Publish(message, DEFAULT_CHANNEL_NAME, null, deliveryOptions, callback);
		}

		public void Publish(object message, PublishOptions publishOptions, DeliveryOptions deliveryOptions, AsyncCallback<MessageStatus> callback)
		{
			Publish(message, DEFAULT_CHANNEL_NAME, publishOptions, deliveryOptions, callback);
		}

		public MessageStatus Publish(object message, string channelName)
		{
			return PublishSync(message, channelName, null, null);
		}

		public MessageStatus Publish(object message, string channelName, PublishOptions publishOptions)
		{
			return PublishSync(message, channelName, publishOptions, null);
		}

		public MessageStatus Publish(object message, string channelName, DeliveryOptions deliveryOptions)
		{
			return PublishSync(message, channelName, null, deliveryOptions);
		}

		public MessageStatus Publish(object message, string channelName, PublishOptions publishOptions, DeliveryOptions deliveryOptions)
		{
			return PublishSync(message, channelName, publishOptions, deliveryOptions);
		}

		private MessageStatus PublishSync(object message, string channelName, PublishOptions publishOptions, DeliveryOptions deliveryOptions)
		{
			checkChannelName(channelName);
			if (message == null)
			{
				throw new ArgumentNullException("Message cannot be null. Use an empty string instead.");
			}
			return Invoker.InvokeSync<MessageStatus>(Invoker.Api.MESSAGINGSERVICE_PUBLISH, new object[2]
			{
				GetPublishRequestData(message, publishOptions, deliveryOptions),
				channelName
			});
		}

		public void Publish(object message, string channelName, AsyncCallback<MessageStatus> callback)
		{
			Publish(message, channelName, null, null, callback);
		}

		public void Publish(object message, string channelName, PublishOptions publishOptions, AsyncCallback<MessageStatus> callback)
		{
			Publish(message, channelName, publishOptions, null, callback);
		}

		public void Publish(object message, string channelName, DeliveryOptions deliveryOptions, AsyncCallback<MessageStatus> callback)
		{
			Publish(message, channelName, null, deliveryOptions, callback);
		}

		public void Publish(object message, string channelName, PublishOptions publishOptions, DeliveryOptions deliveryOptions, AsyncCallback<MessageStatus> callback)
		{
			checkChannelName(channelName);
			if (message == null)
			{
				throw new ArgumentNullException("Message cannot be null. Use an empty string instead.");
			}
			Invoker.InvokeAsync(Invoker.Api.MESSAGINGSERVICE_PUBLISH, new object[2]
			{
				GetPublishRequestData(message, publishOptions, deliveryOptions),
				channelName
			}, callback);
		}

		public bool Cancel(string messageId)
		{
			if (string.IsNullOrEmpty(messageId))
			{
				throw new ArgumentNullException("Message id cannot be null or empty.");
			}
			Dictionary<string, object> response = Invoker.InvokeSync<Dictionary<string, object>>(Invoker.Api.MESSAGINGSERVICE_CANCEL, new object[2]
			{
				null,
				messageId
			});
			return GetCancelResult(response);
		}

		public void Cancel(string messageId, AsyncCallback<bool> callback)
		{
			if (string.IsNullOrEmpty(messageId))
			{
				throw new ArgumentNullException("Message id cannot be null or empty.");
			}
			AsyncCallback<Dictionary<string, object>> callback2 = new AsyncCallback<Dictionary<string, object>>(delegate(Dictionary<string, object> response)
			{
				if (callback != null)
				{
					callback.ResponseHandler(GetCancelResult(response));
				}
			}, delegate(BackendlessFault fault)
			{
				if (callback != null)
				{
					callback.ErrorHandler(fault);
				}
			});
			Invoker.InvokeAsync(Invoker.Api.MESSAGINGSERVICE_CANCEL, new object[2]
			{
				null,
				messageId
			}, callback2);
		}

		public Subscription Subscribe(AsyncCallback<List<Message>> callback)
		{
			return Subscribe(DEFAULT_CHANNEL_NAME, callback);
		}

		public Subscription Subscribe(int pollingInterval, AsyncCallback<List<Message>> callback)
		{
			return Subscribe(DEFAULT_CHANNEL_NAME, pollingInterval, callback);
		}

		public Subscription Subscribe(AsyncCallback<List<Message>> callback, SubscriptionOptions subscriptionOptions)
		{
			return Subscribe(DEFAULT_CHANNEL_NAME, callback, subscriptionOptions);
		}

		public Subscription Subscribe(int pollingInterval, AsyncCallback<List<Message>> callback, SubscriptionOptions subscriptionOptions)
		{
			return Subscribe(DEFAULT_CHANNEL_NAME, pollingInterval, callback, subscriptionOptions);
		}

		public Subscription Subscribe(string channelName, AsyncCallback<List<Message>> callback)
		{
			return Subscribe(channelName, 0, callback, new SubscriptionOptions());
		}

		public Subscription Subscribe(string channelName, int pollingInterval, AsyncCallback<List<Message>> callback)
		{
			return Subscribe(channelName, pollingInterval, callback, new SubscriptionOptions());
		}

		public Subscription Subscribe(string channelName, AsyncCallback<List<Message>> callback, SubscriptionOptions subscriptionOptions)
		{
			return Subscribe(channelName, 0, callback, subscriptionOptions);
		}

		public Subscription Subscribe(string channelName, int pollingInterval, AsyncCallback<List<Message>> callback, SubscriptionOptions subscriptionOptions)
		{
			checkChannelName(channelName);
			if (pollingInterval < 0)
			{
				throw new ArgumentException("Wrong polling interval");
			}
			Subscription subscription = subscribeForPollingAccess(channelName, subscriptionOptions);
			subscription.ChannelName = channelName;
			if (pollingInterval != 0)
			{
				subscription.PollingInterval = pollingInterval;
			}
			subscription.OnSubscribe(callback);
			return subscription;
		}

		public void Subscribe(int pollingInterval, AsyncCallback<List<Message>> callback, AsyncCallback<Subscription> subscriptionCallback)
		{
			Subscribe(DEFAULT_CHANNEL_NAME, pollingInterval, callback, null, subscriptionCallback);
		}

		public void Subscribe(AsyncCallback<List<Message>> callback, AsyncCallback<Subscription> subscriptionCallback)
		{
			Subscribe(DEFAULT_CHANNEL_NAME, 0, callback, null, subscriptionCallback);
		}

		public void Subscribe(AsyncCallback<List<Message>> callback, SubscriptionOptions subscriptionOptions, AsyncCallback<Subscription> subscriptionCallback)
		{
			Subscribe(DEFAULT_CHANNEL_NAME, 0, callback, subscriptionOptions, subscriptionCallback);
		}

		public void Subscribe(int pollingInterval, AsyncCallback<List<Message>> callback, SubscriptionOptions subscriptionOptions, AsyncCallback<Subscription> subscriptionCallback)
		{
			Subscribe(DEFAULT_CHANNEL_NAME, pollingInterval, callback, subscriptionOptions, subscriptionCallback);
		}

		public void Subscribe(string channelName, AsyncCallback<List<Message>> callback, AsyncCallback<Subscription> subscriptionCallback)
		{
			Subscribe(channelName, 0, callback, null, subscriptionCallback);
		}

		public void Subscribe(string channelName, int pollingInterval, AsyncCallback<List<Message>> callback, AsyncCallback<Subscription> subscriptionCallback)
		{
			Subscribe(channelName, pollingInterval, callback, null, subscriptionCallback);
		}

		public void Subscribe(string channelName, AsyncCallback<List<Message>> callback, SubscriptionOptions subscriptionOptions, AsyncCallback<Subscription> subscriptionCallback)
		{
			Subscribe(channelName, 0, callback, subscriptionOptions, subscriptionCallback);
		}

		public void Subscribe(string channelName, int pollingInterval, AsyncCallback<List<Message>> callback, SubscriptionOptions subscriptionOptions, AsyncCallback<Subscription> subscriptionCallback)
		{
			checkChannelName(channelName);
			if (pollingInterval < 0)
			{
				throw new ArgumentException("Wrong polling interval");
			}
			AsyncCallback<Subscription> callback2 = new AsyncCallback<Subscription>(delegate(Subscription r)
			{
				r.ChannelName = channelName;
				if (pollingInterval != 0)
				{
					r.PollingInterval = pollingInterval;
				}
				r.OnSubscribe(callback);
				if (subscriptionCallback != null)
				{
					subscriptionCallback.ResponseHandler(r);
				}
			}, delegate(BackendlessFault f)
			{
				if (subscriptionCallback != null)
				{
					subscriptionCallback.ErrorHandler(f);
					return;
				}
				throw new BackendlessException(f);
			});
			subscribeForPollingAccess(channelName, subscriptionOptions, callback2);
		}

		public List<Message> PollMessages(string channelName, string subscriptionId)
		{
			checkChannelName(channelName);
			if (string.IsNullOrEmpty(subscriptionId))
			{
				throw new ArgumentNullException("Subscription id cannot be null or empty.");
			}
			try
			{
				Dictionary<string, List<Message>> dictionary = Invoker.InvokeSync<Dictionary<string, List<Message>>>(Invoker.Api.MESSAGINGSERVICE_POLLMESSAGES, new object[3]
				{
					null,
					channelName,
					subscriptionId
				});
				if (dictionary != null && dictionary.ContainsKey("messages"))
				{
					return dictionary["messages"];
				}
				return new List<Message>();
				IL_0065:
				List<Message> result;
				return result;
			}
			catch (BackendlessException)
			{
				return new List<Message>();
				IL_0076:
				List<Message> result;
				return result;
			}
		}

		public void PollMessages(string channelName, string subscriptionId, AsyncCallback<List<Message>> callback)
		{
			checkChannelName(channelName);
			if (string.IsNullOrEmpty(subscriptionId))
			{
				throw new ArgumentNullException("Subscription id cannot be null or empty.");
			}
			AsyncCallback<Dictionary<string, List<Message>>> callback2 = new AsyncCallback<Dictionary<string, List<Message>>>(delegate(Dictionary<string, List<Message>> response)
			{
				List<Message> list = null;
				list = ((response == null || !response.ContainsKey("messages")) ? new List<Message>() : response["messages"]);
				if (callback != null)
				{
					callback.ResponseHandler(list);
				}
			}, delegate(BackendlessFault fault)
			{
				if (callback != null)
				{
					callback.ErrorHandler(fault);
				}
			});
			Invoker.InvokeAsync(Invoker.Api.MESSAGINGSERVICE_POLLMESSAGES, new object[3]
			{
				null,
				channelName,
				subscriptionId
			}, callback2);
		}

		public void SendTextEmail(string subject, string messageBody, List<string> recipients)
		{
			SendEmail(subject, new BodyParts(messageBody, null), recipients, new List<string>());
		}

		public void SendTextEmail(string subject, string messageBody, string recipient)
		{
			SendEmail(subject, new BodyParts(messageBody, null), new List<string>
			{
				recipient
			}, new List<string>());
		}

		public void SendHTMLEmail(string subject, string messageBody, List<string> recipients)
		{
			SendEmail(subject, new BodyParts(null, messageBody), recipients, new List<string>());
		}

		public void SendHTMLEmail(string subject, string messageBody, string recipient)
		{
			SendEmail(subject, new BodyParts(null, messageBody), new List<string>
			{
				recipient
			}, new List<string>());
		}

		public void SendEmail(string subject, BodyParts bodyParts, string recipient, List<string> attachments)
		{
			SendEmail(subject, bodyParts, new List<string>
			{
				recipient
			}, attachments);
		}

		public void SendEmail(string subject, BodyParts bodyParts, string recipient)
		{
			SendEmail(subject, bodyParts, new List<string>
			{
				recipient
			}, new List<string>());
		}

		public void SendEmail(string subject, BodyParts bodyParts, List<string> recipients, List<string> attachments)
		{
			if (subject == null)
			{
				throw new ArgumentNullException("Subject cannot be null");
			}
			if (bodyParts == null)
			{
				throw new ArgumentNullException("BodyParts cannot be null");
			}
			if (recipients == null || recipients.Count == 0)
			{
				throw new ArgumentNullException("Recipients cannot be empty");
			}
			if (attachments == null)
			{
				throw new ArgumentNullException("Attachments cannot be null");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("subject", subject);
			dictionary.Add("bodyparts", bodyParts);
			if (recipients.Count > 0)
			{
				dictionary.Add("to", recipients);
			}
			if (attachments.Count > 0)
			{
				dictionary.Add("attachment", attachments);
			}
			Invoker.InvokeSync<object>(Invoker.Api.MESSAGINGSERVICE_SENDEMAIL, new object[1]
			{
				dictionary
			});
		}

		public void SendTextEmail(string subject, string messageBody, List<string> recipients, AsyncCallback<object> responder)
		{
			SendEmail(subject, new BodyParts(messageBody, null), recipients, new List<string>(), responder);
		}

		public void SendTextEmail(string subject, string messageBody, string recipient, AsyncCallback<object> responder)
		{
			SendEmail(subject, new BodyParts(messageBody, null), new List<string>
			{
				recipient
			}, new List<string>(), responder);
		}

		public void SendHTMLEmail(string subject, string messageBody, List<string> recipients, AsyncCallback<object> responder)
		{
			SendEmail(subject, new BodyParts(null, messageBody), recipients, new List<string>(), responder);
		}

		public void SendHTMLEmail(string subject, string messageBody, string recipient, AsyncCallback<object> responder)
		{
			SendEmail(subject, new BodyParts(null, messageBody), new List<string>
			{
				recipient
			}, new List<string>(), responder);
		}

		public void SendEmail(string subject, BodyParts bodyParts, string recipient, List<string> attachments, AsyncCallback<object> responder)
		{
			SendEmail(subject, bodyParts, new List<string>
			{
				recipient
			}, attachments, responder);
		}

		public void SendEmail(string subject, BodyParts bodyParts, string recipient, AsyncCallback<object> responder)
		{
			SendEmail(subject, bodyParts, new List<string>
			{
				recipient
			}, new List<string>(), responder);
		}

		public void SendEmail(string subject, BodyParts bodyParts, List<string> recipients, List<string> attachments, AsyncCallback<object> responder)
		{
			if (subject == null)
			{
				throw new ArgumentNullException("Subject cannot be null");
			}
			if (bodyParts == null)
			{
				throw new ArgumentNullException("BodyParts cannot be null");
			}
			if (recipients == null || recipients.Count == 0)
			{
				throw new ArgumentNullException("Recipients cannot be empty");
			}
			if (attachments == null)
			{
				throw new ArgumentNullException("Attachments cannot be null");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("subject", subject);
			dictionary.Add("bodyparts", bodyParts);
			if (recipients.Count > 0)
			{
				dictionary.Add("to", recipients);
			}
			if (attachments.Count > 0)
			{
				dictionary.Add("attachment", attachments);
			}
			Invoker.InvokeAsync(Invoker.Api.MESSAGINGSERVICE_SENDEMAIL, new object[1]
			{
				dictionary
			}, responder);
		}

		private Subscription subscribeForPollingAccess(string channelName, SubscriptionOptions subscriptionOptions)
		{
			checkChannelName(channelName);
			if (subscriptionOptions == null)
			{
				subscriptionOptions = new SubscriptionOptions();
			}
			return Invoker.InvokeSync<Subscription>(Invoker.Api.MESSAGINGSERVICE_SUBSCRIBE, new object[2]
			{
				GetSubscribeRequestData(subscriptionOptions),
				channelName
			});
		}

		private void subscribeForPollingAccess(string channelName, SubscriptionOptions subscriptionOptions, AsyncCallback<Subscription> callback)
		{
			checkChannelName(channelName);
			if (subscriptionOptions == null)
			{
				subscriptionOptions = new SubscriptionOptions();
			}
			Invoker.InvokeAsync(Invoker.Api.MESSAGINGSERVICE_SUBSCRIBE, new object[2]
			{
				GetSubscribeRequestData(subscriptionOptions),
				channelName
			}, callback);
		}

		private void checkChannelName(string channelName)
		{
			if (string.IsNullOrEmpty(channelName))
			{
				throw new ArgumentNullException("Channel name cannot be null or empty.");
			}
		}

		private static Dictionary<string, object> GetPublishRequestData(object message, PublishOptions publishOptions, DeliveryOptions deliveryOptions)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("message", message);
			if (publishOptions != null)
			{
				if (!string.IsNullOrEmpty(publishOptions.PublisherId))
				{
					dictionary.Add("publisherId", publishOptions.PublisherId);
				}
				if (publishOptions.Headers != null && publishOptions.Headers.Count > 0)
				{
					dictionary.Add("headers", publishOptions.Headers);
				}
				if (!string.IsNullOrEmpty(publishOptions.Subtopic))
				{
					dictionary.Add("subtopic", publishOptions.Subtopic);
				}
			}
			if (deliveryOptions != null)
			{
				if (!string.IsNullOrEmpty(deliveryOptions.PushPolicy.ToString()))
				{
					dictionary.Add("pushPolicy", deliveryOptions.PushPolicy.ToString());
				}
				string text = string.Empty;
				if (deliveryOptions.PushBroadcast == 7)
				{
					text = "ALL";
				}
				else
				{
					if ((deliveryOptions.PushBroadcast & 1) > 0)
					{
						text = "IOS";
					}
					if ((deliveryOptions.PushBroadcast & 2) > 0)
					{
						if (!string.IsNullOrEmpty(text))
						{
							text += "|";
						}
						text = "ANDROID";
					}
					if ((deliveryOptions.PushBroadcast & 4) > 0)
					{
						if (!string.IsNullOrEmpty(text))
						{
							text += "|";
						}
						text = "WP";
					}
				}
				dictionary.Add("pushBroadcast", text);
				if (deliveryOptions.PushSinglecast != null && deliveryOptions.PushSinglecast.Count > 0)
				{
					dictionary.Add("pushSinglecast", deliveryOptions.PushSinglecast);
				}
				if (deliveryOptions.PublishAt.HasValue)
				{
					dictionary.Add("publishAt", deliveryOptions.PublishAt.Value.Ticks);
				}
				if (deliveryOptions.RepeatEvery > 0)
				{
					dictionary.Add("repeatEvery", deliveryOptions.RepeatEvery);
				}
				if (deliveryOptions.RepeatExpiresAt.HasValue)
				{
					dictionary.Add("repeatExpiresAt", deliveryOptions.RepeatExpiresAt.Value.Ticks);
				}
			}
			return dictionary;
		}

		private static bool GetCancelResult(Dictionary<string, object> response)
		{
			bool result = false;
			try
			{
				string text = (string)response["status"];
				if (text == null)
				{
					return result;
				}
				if (!text.Equals("CANCELLED"))
				{
					return result;
				}
				result = true;
				return result;
			}
			catch (System.Exception)
			{
				return result;
			}
		}

		private static Dictionary<string, object> GetSubscribeRequestData(SubscriptionOptions subscriptionOptions)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(subscriptionOptions.SubscriberId))
			{
				dictionary.Add("subscriberId", subscriptionOptions.SubscriberId);
			}
			if (!string.IsNullOrEmpty(subscriptionOptions.Subtopic))
			{
				dictionary.Add("subtopic", subscriptionOptions.Subtopic);
			}
			if (!string.IsNullOrEmpty(subscriptionOptions.Selector))
			{
				dictionary.Add("selector", subscriptionOptions.Selector);
			}
			return dictionary;
		}

		private static Dictionary<string, object> GetRegisterDeviceOnServerRequestData(DeviceRegistration deviceRegistration)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("deviceToken", deviceRegistration.DeviceToken);
			dictionary.Add("deviceId", deviceRegistration.DeviceId);
			dictionary.Add("os", deviceRegistration.Os);
			dictionary.Add("osVersion", deviceRegistration.OsVersion);
			if (deviceRegistration.Channels != null)
			{
				dictionary.Add("channels", deviceRegistration.Channels);
			}
			if (deviceRegistration.Expiration.HasValue)
			{
				dictionary.Add("expiration", deviceRegistration.Timestamp);
			}
			return dictionary;
		}

		private static string GetRegisterDeviceOnServerResult(Dictionary<string, object> response)
		{
			string result = null;
			try
			{
				result = (string)response["registrationId"];
				return result;
			}
			catch (System.Exception)
			{
				return result;
			}
		}

		private static string GetDeviceId()
		{
			return _deviceRegistration.DeviceId;
		}

		private static bool GetUnregisterDeviceOnServerResult(Dictionary<string, object> response)
		{
			bool result = false;
			try
			{
				result = (bool)response["result"];
				return result;
			}
			catch (System.Exception)
			{
				return result;
			}
		}
	}
}
