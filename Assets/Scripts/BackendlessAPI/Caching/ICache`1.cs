using BackendlessAPI.Async;

namespace BackendlessAPI.Caching
{
	public interface ICache<T>
	{
		void Put(T value, AsyncCallback<object> callback);

		void Put(T value, int expire, AsyncCallback<object> callback);

		void Put(T value);

		void Put(T value, int expire);

		void Get(AsyncCallback<T> callback);

		T Get();

		void Contains(AsyncCallback<bool> callback);

		bool Contains();

		void ExpireIn(int seconds, AsyncCallback<object> callback);

		void ExpireIn(int seconds);

		void ExpireAt(int seconds, AsyncCallback<object> callback);

		void ExpireAt(int seconds);

		void Delete(AsyncCallback<object> callback);

		void Delete();
	}
}
