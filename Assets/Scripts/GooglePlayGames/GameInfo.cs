namespace GooglePlayGames
{
	public static class GameInfo
	{
		private const string UnescapedApplicationId = "APP_ID";

		private const string UnescapedIosClientId = "IOS_CLIENTID";

		private const string UnescapedWebClientId = "WEB_CLIENTID";

		private const string UnescapedNearbyServiceId = "NEARBY_SERVICE_ID";

		public const string ApplicationId = "156932771452";

		public const string IosClientId = "";

		public const string WebClientId = "";

		public const string NearbyConnectionServiceId = "";

		public static bool ApplicationIdInitialized()
		{
			return !string.IsNullOrEmpty("156932771452") && !"156932771452".Equals(ToEscapedToken("APP_ID"));
		}

		public static bool IosClientIdInitialized()
		{
			return !string.IsNullOrEmpty(string.Empty) && !string.Empty.Equals(ToEscapedToken("IOS_CLIENTID"));
		}

		public static bool WebClientIdInitialized()
		{
			return !string.IsNullOrEmpty(string.Empty) && !string.Empty.Equals(ToEscapedToken("WEB_CLIENTID"));
		}

		public static bool NearbyConnectionsInitialized()
		{
			return !string.IsNullOrEmpty(string.Empty) && !string.Empty.Equals(ToEscapedToken("NEARBY_SERVICE_ID"));
		}

		private static string ToEscapedToken(string token)
		{
			return $"__{token}__";
		}
	}
}
