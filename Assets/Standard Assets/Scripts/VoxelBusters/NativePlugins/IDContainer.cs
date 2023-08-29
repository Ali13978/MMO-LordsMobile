using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class IDContainer
	{
		[SerializeField]
		private string m_globalID;

		[SerializeField]
		private PlatformValue[] m_platformIDs;

		public string GlobalID => m_globalID;

		public PlatformValue[] PlatformIDs => m_platformIDs;

		private IDContainer()
		{
		}

		public IDContainer(string _globalID, params PlatformValue[] _platformIDs)
		{
			m_globalID = _globalID;
			m_platformIDs = _platformIDs;
		}

		public bool EqualsGlobalID(string _identifier)
		{
			return string.Equals(m_globalID, _identifier);
		}

		public bool EqualsCurrentPlatformID(string _identifier)
		{
			PlatformValue currentPlatformValue = PlatformValueHelper.GetCurrentPlatformValue(m_platformIDs);
			if (currentPlatformValue == null)
			{
				return false;
			}
			return string.Equals(currentPlatformValue.Value, _identifier);
		}
	}
}
