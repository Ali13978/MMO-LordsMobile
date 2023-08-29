using UnityEngine;

namespace VoxelBusters.Utility.UnityGUI.MENU
{
	public class GUISubMenu : GUIMenuBase
	{
		private GameObject m_gameObject;

		public GameObject CachedGameObject
		{
			get
			{
				if (m_gameObject == null)
				{
					m_gameObject = base.gameObject;
				}
				return m_gameObject;
			}
		}

		public string SubMenuName => CachedGameObject.name;

		protected override void OnGUI()
		{
			if (DrawTitleWithBackButton(SubMenuName, "<"))
			{
				OnPressingBackButton();
			}
		}

		public void SetActive(bool _newState)
		{
			CachedGameObject.SetActive(_newState);
		}

		public bool IsActive()
		{
			return CachedGameObject.activeSelf;
		}

		private void OnPressingBackButton()
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
