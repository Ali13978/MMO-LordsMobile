using UnityEngine;

namespace AudienceNetwork
{
	internal class AdLogger
	{
		private enum AdLogLevel
		{
			None,
			Notification,
			Error,
			Warning,
			Log,
			Debug,
			Verbose
		}

		private static AdLogLevel logLevel = AdLogLevel.Log;

		private static readonly string logPrefix = "Audience Network Unity ";

		internal static void Log(string message)
		{
			AdLogLevel adLogLevel = AdLogLevel.Log;
			if (logLevel >= adLogLevel)
			{
				UnityEngine.Debug.Log(logPrefix + levelAsString(adLogLevel) + message);
			}
		}

		internal static void LogWarning(string message)
		{
			AdLogLevel adLogLevel = AdLogLevel.Warning;
			if (logLevel >= adLogLevel)
			{
				UnityEngine.Debug.LogWarning(logPrefix + levelAsString(adLogLevel) + message);
			}
		}

		internal static void LogError(string message)
		{
			AdLogLevel adLogLevel = AdLogLevel.Error;
			if (logLevel >= adLogLevel)
			{
				UnityEngine.Debug.LogError(logPrefix + levelAsString(adLogLevel) + message);
			}
		}

		private static string levelAsString(AdLogLevel logLevel)
		{
			switch (logLevel)
			{
			case AdLogLevel.Notification:
				return string.Empty;
			case AdLogLevel.Error:
				return "<error>: ";
			case AdLogLevel.Warning:
				return "<warn>: ";
			case AdLogLevel.Log:
				return "<log>: ";
			case AdLogLevel.Debug:
				return "<debug>: ";
			case AdLogLevel.Verbose:
				return "<verbose>: ";
			default:
				return string.Empty;
			}
		}
	}
}
