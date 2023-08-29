using BackendlessAPI.Async;

namespace BackendlessAPI.Counters
{
	internal class AtomicImpl<T> : IAtomic<T>
	{
		private string counterName;

		public AtomicImpl(string counterName)
		{
			this.counterName = counterName;
		}

		public void Reset()
		{
			CounterService.GetInstance().Reset(counterName);
		}

		public void Reset(AsyncCallback<object> callback)
		{
			CounterService.GetInstance().Reset(counterName, callback);
		}

		public T Get()
		{
			return CounterService.GetInstance().Get<T>(counterName);
		}

		public void Get(AsyncCallback<T> callback)
		{
			CounterService.GetInstance().Get(counterName, callback);
		}

		public T GetAndIncrement()
		{
			return CounterService.GetInstance().GetAndIncrement<T>(counterName);
		}

		public void GetAndIncrement(AsyncCallback<T> callback)
		{
			CounterService.GetInstance().GetAndIncrement(counterName, callback);
		}

		public T IncrementAndGet()
		{
			return CounterService.GetInstance().IncrementAndGet<T>(counterName);
		}

		public void IncrementAndGet(AsyncCallback<T> callback)
		{
			CounterService.GetInstance().IncrementAndGet(counterName, callback);
		}

		public T GetAndDecrement()
		{
			return CounterService.GetInstance().GetAndDecrement<T>(counterName);
		}

		public void GetAndDecrement(AsyncCallback<T> callback)
		{
			CounterService.GetInstance().GetAndDecrement(counterName, callback);
		}

		public T DecrementAndGet()
		{
			return CounterService.GetInstance().DecrementAndGet<T>(counterName);
		}

		public void DecrementAndGet(AsyncCallback<T> callback)
		{
			CounterService.GetInstance().DecrementAndGet(counterName, callback);
		}

		public T AddAndGet(long value)
		{
			return CounterService.GetInstance().AddAndGet<T>(counterName, value);
		}

		public void AddAndGet(long value, AsyncCallback<T> callback)
		{
			CounterService.GetInstance().AddAndGet(counterName, value, callback);
		}

		public T GetAndAdd(long value)
		{
			return CounterService.GetInstance().GetAndAdd<T>(counterName, value);
		}

		public void GetAndAdd(long value, AsyncCallback<T> callback)
		{
			CounterService.GetInstance().GetAndAdd(counterName, value, callback);
		}

		public bool CompareAndSet(long expected, long updated)
		{
			return CounterService.GetInstance().CompareAndSet(counterName, expected, updated);
		}

		public void CompareAndSet(long expected, long updated, AsyncCallback<bool> callback)
		{
			CounterService.GetInstance().CompareAndSet(counterName, expected, updated, callback);
		}
	}
}
