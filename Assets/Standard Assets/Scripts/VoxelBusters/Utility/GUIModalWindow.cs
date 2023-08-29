using UnityEngine;

namespace VoxelBusters.Utility
{
	public class GUIModalWindow : MonoBehaviour
	{
		[SerializeField]
		private GUISkin m_uiSkin;

		private GUISkin m_oldSkin;

		private GUIScrollView m_rootScrollView;

		protected Rect m_windowRect = new Rect(0f, 0f, Screen.width, Screen.height);

		protected GUIScrollView RootScrollView => m_rootScrollView;

		public GUISkin UISkin
		{
			get
			{
				return m_uiSkin;
			}
			set
			{
				if (value != null)
				{
					m_uiSkin = UnityEngine.Object.Instantiate(value);
				}
			}
		}

		protected virtual void Awake()
		{
			UISkin = m_uiSkin;
		}

		protected virtual void Start()
		{
			m_rootScrollView = base.gameObject.AddComponent<GUIScrollView>();
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		private void OnGUI()
		{
			SetSkin();
			AdjustFontBasedOnScreen();
			AdjustWindowBasedOnScreen();
			m_windowRect = GUI.Window(GetInstanceID(), m_windowRect, GUIWindowBase, string.Empty);
			RestoreSkin();
		}

		private void GUIWindowBase(int _windowID)
		{
			OnGUIWindow();
		}

		protected virtual void OnGUIWindow()
		{
		}

		protected virtual void AdjustFontBasedOnScreen()
		{
			GUI.skin.box.fontSize = (int)((float)Screen.width * 0.03f);
			GUI.skin.button.fontSize = (int)((float)Screen.width * 0.03f);
			GUI.skin.label.fontSize = (int)((float)Screen.width * 0.03f);
			GUI.skin.toggle.fontSize = (int)((float)Screen.width * 0.03f);
		}

		protected virtual void AdjustWindowBasedOnScreen()
		{
			m_windowRect.width = Screen.width;
			m_windowRect.height = Screen.height;
		}

		protected void SetSkin()
		{
			m_oldSkin = GUI.skin;
			GUI.skin = UISkin;
		}

		protected void RestoreSkin()
		{
			GUI.skin = m_oldSkin;
		}

		protected float GetWindowWidth()
		{
			return m_windowRect.width;
		}

		protected float GetWindowHeight()
		{
			return m_windowRect.height;
		}
	}
}
