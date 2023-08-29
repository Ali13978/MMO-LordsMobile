using System.Collections.Generic;

namespace BackendlessAPI.Engine
{
	internal class HeadersEnum
	{
		public static readonly HeadersEnum USER_TOKEN_KEY = new HeadersEnum("user-token");

		public static readonly HeadersEnum LOGGED_IN_KEY = new HeadersEnum("logged-in");

		public static readonly HeadersEnum SESSION_TIME_OUT_KEY = new HeadersEnum("session-time-out");

		public static readonly HeadersEnum APP_ID_NAME = new HeadersEnum("application-id");

		public static readonly HeadersEnum SECRET_KEY_NAME = new HeadersEnum("secret-key");

		public static readonly HeadersEnum APP_TYPE_NAME = new HeadersEnum("application-type");

		public static readonly HeadersEnum API_VERSION = new HeadersEnum("api-version");

		private readonly string name;

		public static IEnumerable<HeadersEnum> Values
		{
			get
			{
				yield return USER_TOKEN_KEY;
				yield return LOGGED_IN_KEY;
				yield return SESSION_TIME_OUT_KEY;
				yield return APP_ID_NAME;
				yield return SECRET_KEY_NAME;
				yield return APP_TYPE_NAME;
				yield return API_VERSION;
			}
		}

		public string Header => name;

		private HeadersEnum(string name)
		{
			this.name = name;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
