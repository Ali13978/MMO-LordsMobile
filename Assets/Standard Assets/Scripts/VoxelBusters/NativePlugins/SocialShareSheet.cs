using VoxelBusters.DebugPRO;

namespace VoxelBusters.NativePlugins
{
	public class SocialShareSheet : ShareSheet
	{
		public override eShareOptions[] ExcludedShareOptions
		{
			get
			{
				return base.ExcludedShareOptions;
			}
			set
			{
				Console.LogWarning("Native Plugins", "[Sharing] Not allowed.");
			}
		}

		public SocialShareSheet()
		{
			base.ExcludedShareOptions = new eShareOptions[3]
			{
				eShareOptions.WHATSAPP,
				eShareOptions.MAIL,
				eShareOptions.MESSAGE
			};
		}
	}
}
