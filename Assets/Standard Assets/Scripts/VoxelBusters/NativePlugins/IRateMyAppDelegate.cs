namespace VoxelBusters.NativePlugins
{
	public interface IRateMyAppDelegate
	{
		bool CanShowRateMyAppDialog();

		void OnBeforeShowingRateMyAppDialog();
	}
}
