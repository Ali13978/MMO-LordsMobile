using System;

namespace VoxelBusters.NativePlugins.Internal
{
	public class InstanceIDGenerator
	{
		public static string Create()
		{
			string text = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
			text = text.Replace('/', '_');
			return text.Substring(0, 22);
		}
	}
}
