using UnityEngine;

namespace VoxelBusters.Utility
{
	public class PopupAttribute : PropertyAttribute
	{
		public string[] options;

		public PopupAttribute(params string[] _options)
		{
			options = _options;
		}
	}
}
