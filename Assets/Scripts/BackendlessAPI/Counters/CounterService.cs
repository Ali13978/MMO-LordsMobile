using BackendlessAPI.Async;
using BackendlessAPI.Engine;
using BackendlessAPI.Exception;
using System;

namespace BackendlessAPI.Counters
{
	public class CounterService
	{
		private static readonly CounterService instance = new CounterService();

		private CounterService()
		{
		}

		public static CounterService GetInstance()
		{
			return instance;
		}

		public IAtomic<T> Of<T>(string counterName)
		{
			return new AtomicImpl<T>(counterName);
		}

		public void Reset(string counterName)
		{
			Invoker.InvokeSync<object>(Invoker.Api.COUNTERSERVICE_RESET, new object[2]
			{
				null,
				counterName
			});
		}

		public void Reset(string counterName, AsyncCallback<object> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.COUNTERSERVICE_RESET, new object[2]
			{
				null,
				counterName
			}, callback);
		}

		public int Get(string counterName)
		{
			return Get<int>(counterName);
		}

		public T Get<T>(string counterName)
		{
			string value = Invoker.InvokeSync<string>(Invoker.Api.COUNTERSERVICE_GET, new object[2]
			{
				null,
				counterName
			});
			return (T)Convert.ChangeType(value, typeof(T));
		}

		public void Get<T>(string counterName, AsyncCallback<T> callback)
		{
			AsyncCallback<string> callback2 = new AsyncCallback<string>(delegate(string r)
			{
				if (callback != null)
				{
					callback.ResponseHandler((T)Convert.ChangeType(r, typeof(T)));
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
			Invoker.InvokeAsync(Invoker.Api.COUNTERSERVICE_GET, new object[2]
			{
				null,
				counterName
			}, callback2);
		}

		public int GetAndIncrement(string counterName)
		{
			return GetAndIncrement<int>(counterName);
		}

		public T GetAndIncrement<T>(string counterName)
		{
			string value = Invoker.InvokeSync<string>(Invoker.Api.COUNTERSERVICE_GET_INC, new object[2]
			{
				null,
				counterName
			});
			return (T)Convert.ChangeType(value, typeof(T));
		}

		public void GetAndIncrement<T>(string counterName, AsyncCallback<T> callback)
		{
			AsyncCallback<string> callback2 = new AsyncCallback<string>(delegate(string r)
			{
				if (callback != null)
				{
					callback.ResponseHandler((T)Convert.ChangeType(r, typeof(T)));
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
			Invoker.InvokeAsync(Invoker.Api.COUNTERSERVICE_GET_INC, new object[2]
			{
				null,
				counterName
			}, callback2);
		}

		public int IncrementAndGet(string counterName)
		{
			return IncrementAndGet<int>(counterName);
		}

		public T IncrementAndGet<T>(string counterName)
		{
			string value = Invoker.InvokeSync<string>(Invoker.Api.COUNTERSERVICE_INC_GET, new object[2]
			{
				null,
				counterName
			});
			return (T)Convert.ChangeType(value, typeof(T));
		}

		public void IncrementAndGet<T>(string counterName, AsyncCallback<T> callback)
		{
			AsyncCallback<string> callback2 = new AsyncCallback<string>(delegate(string r)
			{
				if (callback != null)
				{
					callback.ResponseHandler((T)Convert.ChangeType(r, typeof(T)));
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
			Invoker.InvokeAsync(Invoker.Api.COUNTERSERVICE_INC_GET, new object[2]
			{
				null,
				counterName
			}, callback2);
		}

		public int GetAndDecrement(string counterName)
		{
			return GetAndDecrement<int>(counterName);
		}

		public T GetAndDecrement<T>(string counterName)
		{
			string value = Invoker.InvokeSync<string>(Invoker.Api.COUNTERSERVICE_GET_DEC, new object[2]
			{
				null,
				counterName
			});
			return (T)Convert.ChangeType(value, typeof(T));
		}

		public void GetAndDecrement<T>(string counterName, AsyncCallback<T> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.COUNTERSERVICE_GET_DEC, new object[2]
			{
				null,
				counterName
			}, callback);
		}

		public int DecrementAndGet(string counterName)
		{
			return DecrementAndGet<int>(counterName);
		}

		public T DecrementAndGet<T>(string counterName)
		{
			string value = Invoker.InvokeSync<string>(Invoker.Api.COUNTERSERVICE_DEC_GET, new object[2]
			{
				null,
				counterName
			});
			return (T)Convert.ChangeType(value, typeof(T));
		}

		public void DecrementAndGet<T>(string counterName, AsyncCallback<T> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.COUNTERSERVICE_DEC_GET, new object[2]
			{
				null,
				counterName
			}, callback);
		}

		public int AddAndGet(string counterName, long value)
		{
			return AddAndGet<int>(counterName, value);
		}

		public T AddAndGet<T>(string counterName, long value)
		{
			string value2 = Invoker.InvokeSync<string>(Invoker.Api.COUNTERSERVICE_ADD_GET, new object[3]
			{
				null,
				counterName,
				value
			});
			return (T)Convert.ChangeType(value2, typeof(T));
		}

		public void AddAndGet<T>(string counterName, long value, AsyncCallback<T> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.COUNTERSERVICE_ADD_GET, new object[3]
			{
				null,
				counterName,
				value
			}, callback);
		}

		public int GetAndAdd(string counterName, long value)
		{
			return GetAndAdd<int>(counterName, value);
		}

		public T GetAndAdd<T>(string counterName, long value)
		{
			string value2 = Invoker.InvokeSync<string>(Invoker.Api.COUNTERSERVICE_GET_ADD, new object[3]
			{
				null,
				counterName,
				value
			});
			return (T)Convert.ChangeType(value2, typeof(T));
		}

		public void GetAndAdd<T>(string counterName, long value, AsyncCallback<T> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.COUNTERSERVICE_GET_ADD, new object[3]
			{
				null,
				counterName,
				value
			}, callback);
		}

		public bool CompareAndSet(string counterName, long expected, long updated)
		{
			string value = Invoker.InvokeSync<string>(Invoker.Api.COUNTERSERVICE_COM_SET, new object[4]
			{
				null,
				counterName,
				expected,
				updated
			});
			return Convert.ToBoolean(value);
		}

		public void CompareAndSet(string counterName, long expected, long updated, AsyncCallback<bool> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.COUNTERSERVICE_COM_SET, new object[4]
			{
				null,
				counterName,
				expected,
				updated
			}, callback);
		}
	}
}
