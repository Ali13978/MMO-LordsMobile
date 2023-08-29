using System;
using UnityEngine;

namespace VoxelBusters.DebugPRO.Internal
{
	[Serializable]
	public class ConsoleTag
	{
		[SerializeField]
		private string m_name;

		[SerializeField]
		private bool m_isActive;

		[SerializeField]
		private bool m_ignore;

		public string Name
		{
			get
			{
				return m_name;
			}
			set
			{
				m_name = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return m_isActive;
			}
			set
			{
				m_isActive = value;
			}
		}

		public bool Ignore
		{
			get
			{
				return m_ignore;
			}
			set
			{
				m_ignore = value;
			}
		}

		private ConsoleTag()
		{
		}

		public ConsoleTag(string _tagName, bool _isActive = true, bool _ignore = false)
		{
			Name = _tagName;
			IsActive = _isActive;
			Ignore = _ignore;
		}
	}
}
