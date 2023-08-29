using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public class NotifyNPSettingsOnValueChangeAttribute : ExecuteOnValueChangeAttribute
	{
		public NotifyNPSettingsOnValueChangeAttribute()
			: base("OnPropertyModified")
		{
		}
	}
}
