using UnityEngine;

namespace VoxelBusters.Utility.UnityGUI.MENU
{
	public class GUIMainMenu : GUIMenuBase
	{
		private const int kButtonsPerColumn = 5;

		private GUISubMenu[] m_subMenuList;

		private GUISubMenu m_activeSubMenu;

		private bool m_showingMainMenu;

		protected virtual void Start()
		{
			m_subMenuList = GetComponentsInChildren<GUISubMenu>();
			m_showingMainMenu = true;
			for (int i = 0; i < m_subMenuList.Length; i++)
			{
				m_subMenuList[i].SetActive(_newState: false);
			}
		}

		private void Update()
		{
			if (!m_showingMainMenu)
			{
				m_showingMainMenu = !m_activeSubMenu.IsActive();
			}
		}

		protected override void OnGUI()
		{
			if (m_showingMainMenu)
			{
				DrawMainMenu();
			}
		}

		protected virtual void DrawMainMenu()
		{
			DrawTitle("Main Menu");
			int num = m_subMenuList.Length;
			int num2 = num / 5 + ((num % 5 != 0) ? 1 : 0);
			BeginButtonLayout(num2);
			DrawSubMenuColumn(m_subMenuList, 0, 5);
			DrawSubMenuColumn(m_subMenuList, 5, num);
			EndButtonLayout();
		}

		private void DrawSubMenuColumn(GUISubMenu[] _subMenuList, int _startIndex, int _endIndex)
		{
			int num = _subMenuList.Length;
			GUILayout.BeginVertical();
			for (int i = _startIndex; i < _endIndex && i < num; i++)
			{
				GUISubMenu gUISubMenu = _subMenuList[i];
				if (DrawButton(gUISubMenu.SubMenuName))
				{
					m_activeSubMenu = gUISubMenu;
					m_activeSubMenu.SetActive(_newState: true);
					m_showingMainMenu = false;
				}
			}
			GUILayout.EndVertical();
		}
	}
}
