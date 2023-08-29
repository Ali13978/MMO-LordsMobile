using UnityEngine;
using UnityEngine.UI;

public class WindowLevelUp : MonoBehaviour
{
	public Text textUpgradeReceived;

	private string textUpgradeSoldier = "+1 ROMAN SOLDIER SLOT";

	private string textUpgradeMercenary = "+1 MERCENARY SOLDIER SLOT";

	private string textUpgradeCohort = "+1 SOLDIER PER SQUAD";

	private void Awake()
	{
		Touch_Battle.IsWindowBigOpen = true;
	}

	private void InitializeWindow(int _messageID)
	{
		switch (_messageID)
		{
		case 0:
			textUpgradeReceived.text = textUpgradeSoldier;
			break;
		case 1:
			textUpgradeReceived.text = textUpgradeMercenary;
			break;
		case 2:
			textUpgradeReceived.text = textUpgradeCohort;
			break;
		}
	}

	public void ButtonClose()
	{
		Touch_Battle.IsWindowBigOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
