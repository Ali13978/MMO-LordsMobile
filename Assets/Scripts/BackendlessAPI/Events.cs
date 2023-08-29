using BackendlessAPI.Async;
using BackendlessAPI.Engine;
using BackendlessAPI.Exception;
using System.Collections;
using System.Collections.Generic;

namespace BackendlessAPI
{
	public class Events
	{
		private static readonly Events instance = new Events();

		public static Events GetInstance()
		{
			return instance;
		}

		public IDictionary Dispatch(string eventName, IDictionary eventArgs)
		{
			return Invoker.InvokeSync<Dictionary<string, object>>(Invoker.Api.EVENTS_DISPATCH, new object[2]
			{
				eventArgs,
				eventName
			});
		}

		public void Dispatch(string eventName, IDictionary eventArgs, AsyncCallback<IDictionary> callback)
		{
			AsyncCallback<Dictionary<string, object>> callback2 = new AsyncCallback<Dictionary<string, object>>(delegate(Dictionary<string, object> r)
			{
				if (callback != null)
				{
					callback.ResponseHandler(r);
				}
			}, delegate(BackendlessFault f)
			{
				if (callback != null)
				{
					callback.ErrorHandler(f);
					return;
				}
				throw new BackendlessException(f);
			});
			Invoker.InvokeAsync(Invoker.Api.EVENTS_DISPATCH, new object[2]
			{
				eventArgs,
				eventName
			}, callback2);
		}

		public T Dispatch<T>(string eventName, IDictionary eventArgs)
		{
			return Invoker.InvokeSync<T>(Invoker.Api.EVENTS_DISPATCH, new object[2]
			{
				eventArgs,
				eventName
			});
		}

		public void Dispatch<T>(string eventName, IDictionary eventArgs, AsyncCallback<T> callback)
		{
			AsyncCallback<T> callback2 = new AsyncCallback<T>(delegate(T r)
			{
				if (callback != null)
				{
					callback.ResponseHandler(r);
				}
			}, delegate(BackendlessFault f)
			{
				if (callback != null)
				{
					callback.ErrorHandler(f);
					return;
				}
				throw new BackendlessException(f);
			});
			Invoker.InvokeAsync(Invoker.Api.EVENTS_DISPATCH, new object[2]
			{
				eventArgs,
				eventName
			}, callback2);
		}
	}
}
