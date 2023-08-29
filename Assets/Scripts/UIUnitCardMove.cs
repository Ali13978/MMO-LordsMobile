using UnityEngine;
using UnityEngine.UI;

public class UIUnitCardMove : MonoBehaviour
{
	public Image imageUnit;

	private ArmyType selectedType;

	private ArmyHeroType selectedHero;

	private ArmySoldierType selectedSoldier;

	private ArmyMercenaryType selectedMercenary;

	public void SetData(Sprite _imageUnit, ArmyType _selectedType, int _selectedIndex)
	{
		imageUnit.sprite = _imageUnit;
		selectedType = _selectedType;
		switch (_selectedType)
		{
		case ArmyType.Heroe:
			selectedHero = (ArmyHeroType)_selectedIndex;
			break;
		case ArmyType.Soldier:
			selectedSoldier = (ArmySoldierType)_selectedIndex;
			break;
		case ArmyType.Mercenary:
			selectedMercenary = (ArmyMercenaryType)_selectedIndex;
			break;
		}
	}
}
