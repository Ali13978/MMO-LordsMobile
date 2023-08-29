namespace VoxelBusters.NativePlugins
{
	public interface IIdentifierContainer
	{
		string GlobalID
		{
			get;
			set;
		}

		PlatformValue[] PlatformIDs
		{
			get;
			set;
		}
	}
}
