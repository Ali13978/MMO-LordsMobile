using System.Collections;
using UnityEngine;

namespace VoxelBusters.Utility.Demo
{
	public class JSONDemo : MonoBehaviour
	{
		private string m_JSONObject = string.Empty;

		private string m_result = string.Empty;

		private ArrayList m_GUIButtons = new ArrayList(new string[1]
		{
			"JSON --> C# Object"
		});

		private void OnGUI()
		{
			m_JSONObject = GUIExtensions.TextArea(m_JSONObject, new Rect(0.05f, 0.01f, 0.9f, 0.44f));
			GUIExtensions.Buttons(m_GUIButtons, OnGUIButtonPressed, new Rect(0.05f, 0.45f, 0.9f, 0.1f));
			GUIExtensions.TextArea(m_result, new Rect(0.05f, 0.55f, 0.9f, 0.44f));
		}

		private void OnGUIButtonPressed(string _buttonName)
		{
			int _errorIndex = 0;
			object obj = JSONUtility.FromJSON(m_JSONObject, ref _errorIndex);
			if (_errorIndex == -1)
			{
				m_result = "Value=" + JSONUtility.ToJSON(obj) + "\nType=" + obj.GetType();
			}
			else
			{
				m_result = "Something went wrong!!! Value=NULL. \nError index =" + _errorIndex + "\nSubstring =" + m_JSONObject.Substring(_errorIndex);
			}
		}
	}
}
