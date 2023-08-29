using UnityEngine;

namespace VoxelBusters.Utility
{
	public class RegexAttribute : PropertyAttribute
	{
		public readonly string pattern;

		public readonly string helpMessage;

		public RegexAttribute(string pattern, string helpMessage)
		{
			this.pattern = pattern;
			this.helpMessage = helpMessage;
		}
	}
}
