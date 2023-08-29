using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowInfo : MonoBehaviour
{
	public Text textTitle;

	public Text textName;

	public Image imageSoldier;

	public Sprite[] spriteHero;

	public Sprite[] spriteMercenary;

	private string stringTitleHero = "NEW CHAMPION UNLOCKED!";

	private string stringTitleMercenary = "NEW MERCENARY UNLOCKED!";

	private Vector2[] positionHero = new Vector2[4]
	{
		new Vector2(3.68f, 50.8f),
		new Vector2(-21.09f, 57.9f),
		new Vector2(14f, 57f),
		new Vector2(14f, 57f)
	};

	private Vector2[] positionMercenary = new Vector2[9]
	{
		new Vector2(3.2f, 55.93f),
		new Vector2(3.2f, 55.93f),
		new Vector2(3.2f, 55.93f),
		new Vector2(3.2f, 55.93f),
		new Vector2(3.2f, 55.93f),
		new Vector2(3.2f, 55.93f),
		new Vector2(3.2f, 55.93f),
		new Vector2(3.2f, 55.93f),
		new Vector2(3.2f, 55.93f)
	};

	private SfxUIController sfxUiController;

	private MapController mapController;

	private void Awake()
	{
		Touch_Map.IsWindowOpen = true;
		stringTitleHero = ScriptLocalization.Get("TUTORIAL/unlock_subtitle_hero").ToUpper();
		stringTitleMercenary = ScriptLocalization.Get("TUTORIAL/unlock_subtitle_mercenary").ToUpper();
		sfxUiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SfxUIController>();
		mapController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapController>();
	}

	private void Start()
	{
		sfxUiController.PlaySound(SfxUI.CharacterUnlock);
	}

	public void SetCharacterData(CityCharacter _armyType, int _characterIndex)
	{
		switch (_armyType)
		{
		case CityCharacter.Hero:
			textTitle.text = stringTitleHero;
			textName.text = ConfigPrefsController.unitsStats_Hero_Name[_characterIndex];
			imageSoldier.sprite = spriteHero[_characterIndex];
			imageSoldier.rectTransform.anchoredPosition = positionHero[_characterIndex];
			break;
		case CityCharacter.Mercenary:
			textTitle.text = stringTitleMercenary;
			textName.text = ConfigPrefsController.unitsStats_Mercenary_Name[_characterIndex];
			imageSoldier.sprite = spriteMercenary[_characterIndex];
			imageSoldier.rectTransform.anchoredPosition = positionMercenary[_characterIndex];
			break;
		}
	}

	public void ButtonPressClose()
	{
		sfxUiController.PlaySound(SfxUI.ClickClose);
		Touch_Map.IsWindowOpen = false;
		mapController.UpdateMapStatus(_isTutorial: false);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
