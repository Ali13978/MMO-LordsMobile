using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
{
	public Image[] slots;

	public void TurnSlotOn(int unitLevel)
	{
		for (int i = 0; i < unitLevel; i++)
		{
			slots[i].gameObject.SetActive(value: true);
		}
	}

	public void TurnSlotOff(int unitLevel)
	{
		for (int i = 0; i < unitLevel; i++)
		{
			slots[i].gameObject.SetActive(value: false);
		}
	}
}
