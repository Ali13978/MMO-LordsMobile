using System.Runtime.InteropServices;

namespace VoxelBusters.Utility
{
	public class AndroidManifestGenerator
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct Feature
		{
			public string Name
			{
				get;
				private set;
			}

			public bool Required
			{
				get;
				private set;
			}

			public Feature(string _name, bool _required)
			{
				Name = _name;
				Required = _required;
			}
		}
	}
}
