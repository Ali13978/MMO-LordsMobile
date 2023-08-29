using UnityEngine;

namespace VoxelBusters.NativePlugins.Internal
{
	public class UtilityUnsupported : IUtilityPlatform
	{
		public void OpenStoreLink(string _applicationID)
		{
			UnityEngine.Debug.LogWarning("The operation could not be completed because the requested feature is not supported.");
		}

		public void SetApplicationIconBadgeNumber(int _badgeNumber)
		{
		}
	}
}
