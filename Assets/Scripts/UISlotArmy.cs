using UnityEngine;
using UnityEngine.UI;

public class UISlotArmy : MonoBehaviour
{
	public int slotIndex;

	public Image imageSoldier;

	public Text textCostSoldier;

	private void Awake()
	{
		RemoveUnit();
	}

	public void AddUnit(Sprite _imageSoldier, string _textCost)
	{
		imageSoldier.sprite = _imageSoldier;
		imageSoldier.color = new Color(1f, 1f, 1f, 1f);
		textCostSoldier.text = _textCost;
	}

	public void RemoveUnit()
	{
		imageSoldier.sprite = null;
		imageSoldier.color = new Color(0f, 0f, 0f, 0f);
		textCostSoldier.text = string.Empty;
	}
}
