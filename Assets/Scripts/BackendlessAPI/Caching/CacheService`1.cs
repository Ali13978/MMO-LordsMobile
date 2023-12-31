using BackendlessAPI.Async;

namespace BackendlessAPI.Caching
{
	internal class CacheService<T> : ICache<T>
	{
		private string key;

		public CacheService(T type, string key)
		{
			this.key = key;
		}

		public void Put(T value, AsyncCallback<object> callback)
		{
			Cache.GetInstance().Put(key, value, callback);
		}

		public void Put(T value, int expire, AsyncCallback<object> callback)
		{
			Cache.GetInstance().Put(key, value, expire, callback);
		}

		public void Put(T value)
		{
			Cache.GetInstance().Put(key, value);
		}

		public void Put(T value, int expire)
		{
			Cache.GetInstance().Put(key, value, expire);
		}

		public void Get(AsyncCallback<T> callback)
		{
			Cache.GetInstance().Get(key, callback);
		}

		public T Get()
		{
			return Cache.GetInstance().Get<T>(key);
		}

		public void Contains(AsyncCallback<bool> callback)
		{
			Cache.GetInstance().Contains(key, callback);
		}

		public bool Contains()
		{
			return Cache.GetInstance().Contains(key);
		}

		public void ExpireIn(int seconds, AsyncCallback<object> callback)
		{
			Cache.GetInstance().ExpireIn(key, seconds, callback);
		}

		public void ExpireIn(int seconds)
		{
			Cache.GetInstance().ExpireIn(key, seconds);
		}

		public void ExpireAt(int seconds, AsyncCallback<object> callback)
		{
			Cache.GetInstance().ExpireAt(key, seconds, callback);
		}

		public void ExpireAt(int seconds)
		{
			Cache.GetInstance().ExpireAt(key, seconds);
		}

		public void Delete(AsyncCallback<object> callback)
		{
			Cache.GetInstance().Delete(key, callback);
		}

		public void Delete()
		{
			Cache.GetInstance().Delete(key);
		}
	}
}
