using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.AssetStoreProductUtility.Demo
{
	public class DemoGUIWindow : GUIModalWindow
	{
		protected const string kSubTitleStyle = "sub-title";

		protected override void OnGUIWindow()
		{
		}

		protected override void AdjustFontBasedOnScreen()
		{
			GUI.skin.box.fontSize = Mathf.Clamp((int)((float)Screen.width * 0.03f), 0, 36);
			GUI.skin.button.fontSize = Mathf.Clamp((int)((float)Screen.width * 0.03f), 0, 36);
			GUI.skin.label.fontSize = Mathf.Clamp((int)((float)Screen.width * 0.03f), 0, 36);
			GUI.skin.toggle.fontSize = Mathf.Clamp((int)((float)Screen.width * 0.03f), 0, 36);
			GUI.skin.textField.fontSize = Mathf.Clamp((int)((float)Screen.width * 0.03f), 20, 36);
			GUI.skin.GetStyle("sub-title").fontSize = Mathf.Clamp((int)((float)Screen.width * 0.03f), 0, 40);
		}
	}
}
