using BackendlessAPI.Async;
using BackendlessAPI.Engine;

namespace BackendlessAPI.Caching
{
	public class Cache
	{
		private static readonly Cache instance = new Cache();

		private Cache()
		{
		}

		public static Cache GetInstance()
		{
			return instance;
		}

		public ICache<T> With<T>(string key, T type)
		{
			return new CacheService<T>(type, key);
		}

		public void Put(string key, object obj, int expire, AsyncCallback<object> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.CACHESERVICE_PUT, new object[3]
			{
				obj,
				key,
				expire
			}, callback);
		}

		public void Put(string key, object obj, AsyncCallback<object> callback)
		{
			Put(key, obj, 0, callback);
		}

		public void Put(string key, object obj)
		{
			Put(key, obj, 0);
		}

		public void Put(string key, object obj, int expire)
		{
			Invoker.InvokeSync<object>(Invoker.Api.CACHESERVICE_PUT, new object[3]
			{
				obj,
				key,
				expire
			});
		}

		public T Get<T>(string key)
		{
			return Invoker.InvokeSync<T>(Invoker.Api.CACHESERVICE_GET, new object[2]
			{
				null,
				key
			});
		}

		public void Get<T>(string key, AsyncCallback<T> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.CACHESERVICE_GET, new object[2]
			{
				null,
				key
			}, callback);
		}

		public bool Contains(string key)
		{
			return Invoker.InvokeSync<bool>(Invoker.Api.CACHESERVICE_CONTAINS, new object[2]
			{
				null,
				key
			});
		}

		public void Contains(string key, AsyncCallback<bool> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.CACHESERVICE_CONTAINS, new object[2]
			{
				null,
				key
			}, callback);
		}

		public void ExpireIn(string key, int seconds)
		{
			Invoker.InvokeSync<object>(Invoker.Api.CACHESERVICE_EXPIREIN, new object[3]
			{
				null,
				key,
				seconds
			});
		}

		public void ExpireIn(string key, int seconds, AsyncCallback<object> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.CACHESERVICE_EXPIREIN, new object[3]
			{
				null,
				key,
				seconds
			}, callback);
		}

		public void ExpireAt(string key, int timestamp)
		{
			Invoker.InvokeSync<object>(Invoker.Api.CACHESERVICE_EXPIREAT, new object[3]
			{
				null,
				key,
				timestamp
			});
		}

		public void ExpireAt(string key, int timestamp, AsyncCallback<object> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.CACHESERVICE_EXPIREAT, new object[3]
			{
				null,
				key,
				timestamp
			}, callback);
		}

		public void Delete(string key)
		{
			Invoker.InvokeSync<object>(Invoker.Api.CACHESERVICE_DELETE, new object[2]
			{
				null,
				key
			});
		}

		public void Delete(string key, AsyncCallback<object> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.CACHESERVICE_DELETE, new object[2]
			{
				null,
				key
			}, callback);
		}
	}
}
