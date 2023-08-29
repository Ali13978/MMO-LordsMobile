using BackendlessAPI.Async;

namespace BackendlessAPI.Counters
{
	public interface IAtomic<T>
	{
		void Reset();

		void Reset(AsyncCallback<object> responder);

		T Get();

		void Get(AsyncCallback<T> responder);

		T GetAndIncrement();

		void GetAndIncrement(AsyncCallback<T> responder);

		T IncrementAndGet();

		void IncrementAndGet(AsyncCallback<T> responder);

		T GetAndDecrement();

		void GetAndDecrement(AsyncCallback<T> responder);

		T DecrementAndGet();

		void DecrementAndGet(AsyncCallback<T> responder);

		T AddAndGet(long value);

		void AddAndGet(long value, AsyncCallback<T> responder);

		T GetAndAdd(long value);

		void GetAndAdd(long value, AsyncCallback<T> responder);

		bool CompareAndSet(long expected, long updated);

		void CompareAndSet(long expected, long updated, AsyncCallback<bool> responder);
	}
}
