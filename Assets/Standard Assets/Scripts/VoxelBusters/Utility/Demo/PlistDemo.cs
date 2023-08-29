using System.Collections;
using UnityEngine;

namespace VoxelBusters.Utility.Demo
{
	public class PlistDemo : MonoBehaviour
	{
		private Plist m_plist;

		private string m_input = "input";

		private string m_keyPath = "keypath";

		private string m_result = "result";

		private ArrayList m_GUIButtons = new ArrayList(new string[5]
		{
			"LoadPlistAtPath",
			"LoadPlistText",
			"GetKeyPathValue",
			"AddValue",
			"Save"
		});

		private void OnGUI()
		{
			m_input = GUIExtensions.TextArea(m_input, new Rect(0.05f, 0.01f, 0.9f, 0.39f));
			m_keyPath = GUIExtensions.TextArea(m_keyPath, new Rect(0.05f, 0.4f, 0.9f, 0.05f));
			GUIExtensions.Buttons(m_GUIButtons, OnGUIButtonPressed, new Rect(0.05f, 0.45f, 0.9f, 0.25f));
			GUIExtensions.TextArea(m_result, new Rect(0.05f, 0.7f, 0.9f, 0.29f));
		}

		private void OnGUIButtonPressed(string _buttonName)
		{
			if (_buttonName == "LoadPlistAtPath")
			{
				LoadPlistAtPath();
			}
			else if (_buttonName == "LoadPlistText")
			{
				LoadPlistText();
			}
			else if (_buttonName == "GetKeyPathValue")
			{
				GetKeyPathValue();
			}
			else if (_buttonName == "AddValue")
			{
				AddValue();
			}
			else if (_buttonName == "Save")
			{
				Save();
			}
		}

		private void LoadPlistAtPath()
		{
			if (string.IsNullOrEmpty(m_input))
			{
				m_result = "Failed to load plist";
				return;
			}
			m_plist = Plist.LoadPlistAtPath(m_input);
			m_result = "Plist=" + JSONUtility.ToJSON(m_plist);
		}

		private void LoadPlistText()
		{
			if (string.IsNullOrEmpty(m_input))
			{
				m_result = "Failed to load plist";
				return;
			}
			m_plist = Plist.Load(m_input);
			m_result = "Plist=" + JSONUtility.ToJSON(m_plist);
		}

		private void GetKeyPathValue()
		{
			if (m_plist == null)
			{
				m_result = "Please load the plist before calling its API's";
				return;
			}
			object keyPathValue = m_plist.GetKeyPathValue(m_keyPath);
			m_result = "Keypath=" + m_keyPath + "\nValue=" + keyPathValue;
		}

		private void AddValue()
		{
			if (m_plist == null)
			{
				m_result = "Please load the plist before calling its API's";
				return;
			}
			if (string.IsNullOrEmpty(m_input))
			{
				m_result = "Failed to add value";
				return;
			}
			object obj = JSONUtility.FromJSON(m_input);
			if (obj != null)
			{
				m_plist.AddValue(m_keyPath, obj);
				m_result = "Plist=" + JSONUtility.ToJSON(m_plist);
			}
			else
			{
				m_plist.AddValue(m_keyPath, m_input);
				m_result = "Plist=" + JSONUtility.ToJSON(m_plist);
			}
		}

		private void Save()
		{
			if (m_plist == null)
			{
				m_result = "Please load the plist before calling its API's";
			}
			else if (string.IsNullOrEmpty(m_input))
			{
				m_result = "Input cant be null/empty";
			}
			else
			{
				m_plist.Save(m_input);
			}
		}
	}
}
