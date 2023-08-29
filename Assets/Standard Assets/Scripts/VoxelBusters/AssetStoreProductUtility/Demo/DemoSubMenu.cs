using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.AssetStoreProductUtility.Demo
{
	public class DemoSubMenu : DemoGUIWindow
	{
		private List<string> m_results = new List<string>();

		private GUIScrollView m_resultsScrollView;

		protected override void Start()
		{
			base.Start();
			m_resultsScrollView = base.gameObject.AddComponent<GUIScrollView>();
		}

		protected virtual void DrawPopButton(string _popTitle = "Back")
		{
			if (GUILayout.Button(_popTitle))
			{
				base.gameObject.SetActive(value: false);
			}
		}

		protected override void OnGUIWindow()
		{
			base.OnGUIWindow();
			GUILayout.Box(base.name);
		}

		protected void AppendResult(string _result)
		{
			m_results.Add(_result);
		}

		protected void AddNewResult(string _result)
		{
			m_results.Clear();
			m_results.Add(_result);
		}

		protected void DrawResults()
		{
			GUILayout.FlexibleSpace();
			if (m_results.Count <= 0)
			{
				return;
			}
			m_resultsScrollView.BeginScrollView(base.UISkin.window, GUILayout.MinHeight((float)Screen.height * 0.3f));
			for (int i = 0; i < m_results.Count; i++)
			{
				string text = m_results[i];
				if (i == 0)
				{
					GUILayout.Box(text);
				}
				else
				{
					GUILayout.Label(text);
				}
			}
			GUILayout.FlexibleSpace();
			m_resultsScrollView.EndScrollView();
		}
	}
}
