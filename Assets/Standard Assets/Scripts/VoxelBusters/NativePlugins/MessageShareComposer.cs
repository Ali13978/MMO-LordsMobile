namespace VoxelBusters.NativePlugins
{
	public class MessageShareComposer : IShareView
	{
		public string Body
		{
			get;
			set;
		}

		public string[] ToRecipients
		{
			get;
			set;
		}

		public bool IsReadyToShowView => true;
	}
}
