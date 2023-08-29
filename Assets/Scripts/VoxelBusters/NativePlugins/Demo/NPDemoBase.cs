using UnityEngine;
using VoxelBusters.AssetStoreProductUtility.Demo;

namespace VoxelBusters.NativePlugins.Demo
{
	public class NPDemoBase : DemoSubMenu
	{
		private const string kThingsToKnowTitle = "Things to know";

		private bool m_showThingsToKnow;

		private string m_featureInterfaceInfoText;

		private string[] m_additionalInfoTexts = new string[0];

		protected override void Start()
		{
			base.Start();
			m_showThingsToKnow = true;
			string name = base.gameObject.name;
			m_featureInterfaceInfoText = string.Format("NPBinding.{0} object provides the interface to access {1} functionalities.", name.Replace(" ", string.Empty), name);
		}

		protected override void OnGUIWindow()
		{
			base.OnGUIWindow();
			try
			{
				base.RootScrollView.BeginScrollView();
				if (!DisplayThingsToKnow())
				{
					DisplayFeatureFunctionalities();
				}
			}
			finally
			{
				base.RootScrollView.EndScrollView();
				if (m_showThingsToKnow)
				{
					GUILayout.FlexibleSpace();
				}
				else
				{
					DrawResults();
					DrawPopButton();
				}
			}
		}

		protected bool DisplayThingsToKnow()
		{
			if (m_showThingsToKnow)
			{
				GUILayout.Label("Things to know", "sub-title");
				if (m_featureInterfaceInfoText != null)
				{
					GUILayout.Box(m_featureInterfaceInfoText);
				}
				string[] additionalInfoTexts = m_additionalInfoTexts;
				foreach (string text in additionalInfoTexts)
				{
					GUILayout.Box(text);
				}
				if (GUILayout.Button("Okie great!"))
				{
					m_showThingsToKnow = false;
				}
				return true;
			}
			return false;
		}

		protected virtual void DisplayFeatureFunctionalities()
		{
		}

		protected void AddExtraInfoTexts(params string[] _infoTexts)
		{
			if (_infoTexts != null)
			{
				m_additionalInfoTexts = _infoTexts;
			}
		}

		protected void SetFeatureInterfaceInfoText(string _newText)
		{
			m_featureInterfaceInfoText = _newText;
		}
	}
}
