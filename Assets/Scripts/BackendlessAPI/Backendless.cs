using BackendlessAPI.Caching;
using BackendlessAPI.Counters;
using BackendlessAPI.Engine;
using BackendlessAPI.Logging;
using BackendlessAPI.Service;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace BackendlessAPI
{
	public static class Backendless
	{
		public static string DEFAULT_URL;

		public static PersistenceService Persistence;

		public static PersistenceService Data;

		public static GeoService Geo;

		public static MessagingService Messaging;

		public static FileService Files;

		public static UserService UserService;

		public static Events Events;

		public static Cache Cache;

		public static CounterService Counters;

		public static LoggingService Logging;

		public static string Url
		{
			get;
			private set;
		}

		public static string AppId
		{
			get;
			private set;
		}

		public static string SecretKey
		{
			get;
			private set;
		}

		public static string VersionNum
		{
			get;
			private set;
		}

		static Backendless()
		{
			DEFAULT_URL = "https://api.backendless.com";
			Url = DEFAULT_URL;
			ServicePointManager.ServerCertificateValidationCallback = ((abc, X509Certificate, X509Chain, SslPolicyErrors) => true);
		}

		public static string getUrl()
		{
			return Url;
		}

		public static void setUrl(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				url = DEFAULT_URL;
			}
			Url = url;
		}

		public static void InitApp(string applicationId, string secretKey, string version)
		{
			if (string.IsNullOrEmpty(applicationId))
			{
				throw new ArgumentNullException("Application id cannot be null");
			}
			if (string.IsNullOrEmpty(secretKey))
			{
				throw new ArgumentNullException("Secret key cannot be null");
			}
			if (string.IsNullOrEmpty(version))
			{
				throw new ArgumentNullException("Version cannot be null");
			}
			AppId = applicationId;
			SecretKey = secretKey;
			VersionNum = version;
			Persistence = new PersistenceService();
			Data = Persistence;
			Geo = new GeoService();
			Messaging = new MessagingService();
			Files = new FileService();
			UserService = new UserService();
			Events = Events.GetInstance();
			Cache = Cache.GetInstance();
			Counters = CounterService.GetInstance();
			Logging = new LoggingService();
			HeadersManager.CleanHeaders();
		}
	}
}
