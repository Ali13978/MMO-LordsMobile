namespace VoxelBusters.NativePlugins.Internal
{
	public interface IUtilityPlatform
	{
		void OpenStoreLink(string _applicationID);

		void SetApplicationIconBadgeNumber(int _badgeNumber);
	}
}
