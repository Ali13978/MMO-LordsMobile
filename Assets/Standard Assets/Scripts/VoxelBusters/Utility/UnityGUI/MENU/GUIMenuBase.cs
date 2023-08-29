using UnityEngine;

namespace VoxelBusters.Utility.UnityGUI.MENU
{
	public class GUIMenuBase : MonoBehaviour
	{
		public const float kButtonHeight = 40f;

		public const float kBackButtonWidth = 40f;

		public const float kBackButtonHeight = 40f;

		private Rect m_titleLayoutNormalisedRect = new Rect(0.05f, 0f, 0.9f, 0.2f);

		private Rect m_buttonLayoutNormalisedRect = new Rect(0.05f, 0.2f, 0.9f, 0.8f);

		private Rect m_resultLayoutNormalisedRect = new Rect(0f, 0.8f, 1f, 1f);

		private float m_buttonColumnCount = 2f;

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnGUI()
		{
		}

		public void DrawTitle(string _title)
		{
			DrawTitleWithBackButton(_title, null);
		}

		public bool DrawTitleWithBackButton(string _title, string _button)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(_button))
			{
				GUIStyle gUIStyle = new GUIStyle(GUI.skin.button);
				gUIStyle.alignment = TextAnchor.MiddleCenter;
				if (GUI.Button(new Rect(10f, 5f, 40f, 40f), _button, gUIStyle))
				{
					result = true;
				}
			}
			GUILayout.BeginArea(GetLayoutRect(m_titleLayoutNormalisedRect));
			GUIStyle gUIStyle2 = new GUIStyle(GUI.skin.label);
			gUIStyle2.alignment = TextAnchor.UpperCenter;
			gUIStyle2.fontSize = 20;
			GUILayout.Space(10f);
			GUILayout.Label(_title, gUIStyle2);
			GUILayout.EndArea();
			return result;
		}

		public void BeginButtonLayout(float _columnCount = 2f, float _normalisedHeight = 0.8f)
		{
			m_buttonLayoutNormalisedRect.height = _normalisedHeight;
			m_buttonColumnCount = _columnCount;
			GUILayout.BeginArea(GetLayoutRect(m_buttonLayoutNormalisedRect));
			GUILayout.BeginHorizontal();
		}

		public void EndButtonLayout()
		{
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		public bool DrawButton(string _buttonName)
		{
			if (GUILayout.Button(_buttonName, GUILayout.MinHeight(40f), GUILayout.MaxWidth((float)Screen.width * m_buttonLayoutNormalisedRect.xMax / m_buttonColumnCount)))
			{
				return true;
			}
			return false;
		}

		public void DrawResultLayout(string _result, float _normalisedHeight = 0.2f)
		{
			m_resultLayoutNormalisedRect.height = _normalisedHeight;
			if (m_resultLayoutNormalisedRect.height != 0f)
			{
				GUIStyle style = new GUIStyle(GUI.skin.textArea);
				GUI.Label(GetLayoutRect(m_resultLayoutNormalisedRect), _result, style);
			}
		}

		private Rect GetLayoutRect(Rect _normalisedRect)
		{
			Vector2 vector = new Vector2(Screen.width, Screen.height);
			return new Rect(vector.x * _normalisedRect.xMin, vector.y * _normalisedRect.yMin, vector.x * _normalisedRect.xMax, vector.y * _normalisedRect.yMax);
		}
	}
}
