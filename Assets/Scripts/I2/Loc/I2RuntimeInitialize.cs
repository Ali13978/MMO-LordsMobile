using System;
using UnityEngine;

namespace I2.Loc
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class I2RuntimeInitialize : RuntimeInitializeOnLoadMethodAttribute
	{
		public I2RuntimeInitialize()
			: base(RuntimeInitializeLoadType.BeforeSceneLoad)
		{
		}
	}
}
