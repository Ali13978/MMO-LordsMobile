using UnityEngine;

namespace VoxelBusters.Utility
{
	public class ExecuteOnValueChangeAttribute : PropertyAttribute
	{
		public string InvokeMethod
		{
			get;
			private set;
		}

		private ExecuteOnValueChangeAttribute()
		{
		}

		public ExecuteOnValueChangeAttribute(string _method)
		{
			InvokeMethod = _method;
		}
	}
}
