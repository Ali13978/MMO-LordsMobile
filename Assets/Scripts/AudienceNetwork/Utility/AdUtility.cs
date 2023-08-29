namespace AudienceNetwork.Utility
{
	public class AdUtility
	{
		internal static double width()
		{
			return AdUtilityBridge.Instance.width();
		}

		internal static double height()
		{
			return AdUtilityBridge.Instance.height();
		}

		internal static double convert(double deviceSize)
		{
			return AdUtilityBridge.Instance.convert(deviceSize);
		}

		internal static void prepare()
		{
			AdUtilityBridge.Instance.prepare();
		}
	}
}
