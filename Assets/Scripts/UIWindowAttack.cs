using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIWindowAttack : MonoBehaviour
{
	public WaveRoman waveRoman;

	private RectTransform myTransform;

	public RectTransform contentUnitsTransform;

	public Text textTotalCost;

	public Button buttonAttack;

	public UISlotArmy[] slotArmyArray;

	private GameObject[] slotArmyObjectArray;

	public Sprite[] spriteUnitsHeroArray = new Sprite[4];

	public Sprite[] spriteUnitsSoldierMeleeArray = new Sprite[4];

	public Sprite[] spriteUnitsSoldierSpearArray = new Sprite[4];

	public Sprite[] spriteUnitsSoldierRangedArray = new Sprite[4];

	public Sprite[] spriteUnitsSoldierMountedArray = new Sprite[4];

	public Sprite[] spriteUnitsSoldierSiegeArray = new Sprite[4];

	public Sprite[] spriteUnitsMercenaryArray = new Sprite[9];

	public Button buttonsSoldiersType;

	public Button buttonsHeroesType;

	public Button buttonsMercsType;

	public GameObject[] glow;

	public Image backgroundSoldiers;

	public Image iconSoldiers;

	public Image backgroundHero;

	public Image iconHero;

	public Image backgroundMercs;

	public Image iconMercs;

	public Image scrollBackground;

	private Faction[] unitsFactionSelected = new Faction[18];

	private ArmyType[] unitTypeSelected = new ArmyType[18];

	private int[] unitIndexSelected = new int[18];

	private int[] unitsCostSelected = new int[18];

	private int[] unitsLevelSelected = new int[18];

	private int[] unitsIndexFormation = new int[18];

	private List<UIUnitCard> unitCardsArray = new List<UIUnitCard>();

	private List<GameObject> unitsObjectList = new List<GameObject>();

	private List<Transform> unitsTransformList = new List<Transform>();

	private int totalUnitsCost;

	private bool isAnyFirstLegion;

	private bool isAnySecondLegion;

	private GameObject cardMovingObject;

	private RectTransform cardMovingTransform;

	private ArmyType scrollViewSelected;

	private TutorialController tutorialController;

	private SfxUIController sfxUIController;

	private bool isDragging;

	private void Awake()
	{
		myTransform = base.gameObject.GetComponent<RectTransform>();
		tutorialController = GameObject.FindGameObjectWithTag("GameController").GetComponent<TutorialController>();
		sfxUIController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SfxUIController>();
		slotArmyObjectArray = new GameObject[slotArmyArray.Length];
		for (int i = 0; i < slotArmyArray.Length; i++)
		{
			slotArmyObjectArray[i] = slotArmyArray[i].gameObject;
		}
		for (int j = 0; j < unitIndexSelected.Length; j++)
		{
			unitIndexSelected[j] = -1;
			unitsIndexFormation[j] = -1;
		}
	}

	private void Start()
	{
	}

	public void StartWindow()
	{
		scrollViewSelected = ArmyType.Mercenary;
		UpdateScrollView();
		LoadFormation();
		scrollViewSelected = ArmyType.Heroe;
		UpdateScrollView();
		LoadFormation();
		scrollViewSelected = ArmyType.Soldier;
		UpdateScrollView();
		LoadFormation();
	}

	public void ResetWindow()
	{
		if (cardMovingObject != null)
		{
			UnityEngine.Object.Destroy(cardMovingObject);
		}
		for (int i = 0; i < slotArmyArray.Length; i++)
		{
			slotArmyArray[i].RemoveUnit();
		}
		if (unitsObjectList != null)
		{
			for (int j = 0; j < unitsObjectList.Count; j++)
			{
				UnityEngine.Object.Destroy(unitsObjectList[j]);
			}
		}
		unitsFactionSelected = null;
		unitTypeSelected = null;
		unitIndexSelected = null;
		unitsCostSelected = null;
		unitsLevelSelected = null;
		unitsIndexFormation = null;
		unitCardsArray.Clear();
		unitsObjectList.Clear();
		unitsTransformList.Clear();
		totalUnitsCost = 0;
		cardMovingTransform = null;
		unitsFactionSelected = new Faction[18];
		unitTypeSelected = new ArmyType[18];
		unitIndexSelected = new int[18];
		unitsCostSelected = new int[18];
		unitsLevelSelected = new int[18];
		unitsIndexFormation = new int[18];
		for (int k = 0; k < unitIndexSelected.Length; k++)
		{
			unitIndexSelected[k] = -1;
			unitsIndexFormation[k] = -1;
		}
	}

	private void Update()
	{
		if (isDragging)
		{
			cardMovingTransform.position = UnityEngine.Input.mousePosition;
		}
	}

	public void ScrollingList()
	{
		if (unitCardsArray.Count > 0)
		{
			for (int i = 0; i < unitCardsArray.Count; i++)
			{
				unitCardsArray[i].IsDragging = false;
				unitCardsArray[i].IsPointerDown = false;
			}
		}
	}

	private void LoadFormation()
	{
		if (PlayerPrefsController.LoadAttackFormation(0, 0) < 0)
		{
			return;
		}
		for (int i = 0; i < unitsFactionSelected.Length; i++)
		{
			ArmyType armyType = (ArmyType)PlayerPrefsController.LoadAttackFormation(0, i);
			int num = PlayerPrefsController.LoadAttackFormation(1, i);
			if (armyType == ArmyType.None)
			{
				continue;
			}
			for (int j = 0; j < unitCardsArray.Count; j++)
			{
				if (unitCardsArray[j].IndexFormation == num && unitCardsArray[j].SelectedType == armyType)
				{
					int selectedIndex = -1;
					switch (unitCardsArray[j].SelectedType)
					{
					case ArmyType.Heroe:
						selectedIndex = (int)unitCardsArray[j].SelectedHero;
						break;
					case ArmyType.Soldier:
						selectedIndex = (int)unitCardsArray[j].SelectedSoldier;
						break;
					case ArmyType.Mercenary:
						selectedIndex = (int)unitCardsArray[j].SelectedMercenary;
						break;
					}
					ReleaseCard(unitCardsArray[j].imageCard.sprite, unitCardsArray[j].SelectedFaction, unitCardsArray[j].SelectedType, selectedIndex, unitCardsArray[j].CostCard, unitCardsArray[j].LevelCard, unitCardsArray[j].IndexFormation, i);
					break;
				}
			}
		}
	}

	private void UpdateScrollView()
	{
		for (int i = 0; i < unitsObjectList.Count; i++)
		{
			UnityEngine.Object.Destroy(unitsObjectList[i]);
		}
		unitsObjectList.Clear();
		unitsTransformList.Clear();
		isAnyFirstLegion = false;
		isAnySecondLegion = false;
		for (int j = 0; j < 6; j++)
		{
			if (unitTypeSelected[j] != 0)
			{
				isAnyFirstLegion = true;
				break;
			}
		}
		if (isAnyFirstLegion)
		{
			for (int k = 6; k < 12; k++)
			{
				if (unitTypeSelected[k] != 0)
				{
					isAnySecondLegion = true;
					break;
				}
			}
		}
		for (int l = 6; l < slotArmyArray.Length; l++)
		{
			if (((l >= 6 && l < 12 && !isAnyFirstLegion) || (l >= 12 && !isAnySecondLegion)) && unitTypeSelected[l] != 0)
			{
				ClickSlotRemove(l);
			}
		}
		int num = 0;
		int num2 = 1;
		int num3 = 0;
		for (int m = 0; m < PlayerPrefsController.HeroeLvl.Length; m++)
		{
			if (PlayerPrefsController.HeroeLvl[m] >= 1)
			{
				num++;
			}
		}
		if (PlayerPrefsController.UnitsTechMelee[0] >= 3)
		{
			num2++;
		}
		if (PlayerPrefsController.UnitsTechSpear[1] >= 3)
		{
			num2++;
		}
		if (PlayerPrefsController.UnitsTechSpear[2] >= 3)
		{
			num2++;
		}
		if (PlayerPrefsController.UnitsTechRanged[1] >= 3)
		{
			num2++;
		}
		for (int n = 0; n < PlayerPrefsController.UnitsTechMercenary.Length; n++)
		{
			if (PlayerPrefsController.UnitsTechMercenary[n])
			{
				num3++;
			}
		}
		for (int num4 = 0; num4 < unitTypeSelected.Length; num4++)
		{
			switch (unitTypeSelected[num4])
			{
			case ArmyType.Heroe:
				num--;
				break;
			case ArmyType.Mercenary:
				num3--;
				break;
			case ArmyType.Soldier:
				num2--;
				break;
			}
		}
		if (num > 0)
		{
			UpdateButton(buttonsHeroesType, isInteractable: true, iconHero, backgroundHero);
		}
		else
		{
			UpdateButton(buttonsHeroesType, isInteractable: false, iconHero, backgroundHero);
		}
		if (num2 > 0)
		{
			UpdateButton(buttonsSoldiersType, isInteractable: true, iconSoldiers, backgroundSoldiers);
		}
		else
		{
			UpdateButton(buttonsSoldiersType, isInteractable: false, iconSoldiers, backgroundSoldiers);
		}
		if (num3 > 0)
		{
			UpdateButton(buttonsMercsType, isInteractable: true, iconMercs, backgroundMercs);
		}
		else
		{
			UpdateButton(buttonsMercsType, isInteractable: false, iconMercs, backgroundMercs);
		}
		if (scrollViewSelected == ArmyType.None)
		{
			scrollViewSelected = ArmyType.Heroe;
		}
		if (scrollViewSelected == ArmyType.Heroe && num <= 0)
		{
			if (num2 > 0)
			{
				scrollViewSelected = ArmyType.Soldier;
			}
			else if (num3 > 0)
			{
				scrollViewSelected = ArmyType.Mercenary;
			}
			else
			{
				scrollViewSelected = ArmyType.None;
			}
		}
		else if (scrollViewSelected == ArmyType.Soldier && num2 <= 0)
		{
			if (num3 > 0)
			{
				scrollViewSelected = ArmyType.Mercenary;
			}
			else if (num > 0)
			{
				scrollViewSelected = ArmyType.Heroe;
			}
			else
			{
				scrollViewSelected = ArmyType.None;
			}
		}
		else if (scrollViewSelected == ArmyType.Mercenary && num3 <= 0)
		{
			if (num > 0)
			{
				scrollViewSelected = ArmyType.Heroe;
			}
			else if (num2 > 0)
			{
				scrollViewSelected = ArmyType.Soldier;
			}
			else
			{
				scrollViewSelected = ArmyType.None;
			}
		}
		float num5 = 5f;
		Vector3 a = new Vector3(0f, 0f, 0f);
		unitCardsArray.Clear();
		if (scrollViewSelected == ArmyType.Heroe)
		{
			UpdateButtonSiblingIndex(buttonsHeroesType, buttonsMercsType, buttonsSoldiersType);
			glow[0].SetActive(value: false);
			glow[1].SetActive(value: true);
			glow[2].SetActive(value: false);
			int num6 = 0;
			for (int num7 = 0; num7 < 4; num7++)
			{
				if (PlayerPrefsController.HeroeLvl[num7] < 1)
				{
					continue;
				}
				int num8 = 0;
				switch (num7)
				{
				case 0:
					num8 = 3;
					break;
				case 1:
					num8 = 0;
					break;
				case 2:
					num8 = 2;
					break;
				case 3:
					num8 = 1;
					break;
				}
				bool flag = false;
				for (int num9 = 0; num9 < unitTypeSelected.Length; num9++)
				{
					if (unitTypeSelected[num9] == ArmyType.Heroe && unitIndexSelected[num9] == num8)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/Unit_item")) as GameObject;
					RectTransform component = gameObject.GetComponent<RectTransform>();
					component.SetParent(contentUnitsTransform);
					component.position = new Vector3(0f, 0f);
					component.localScale = new Vector3(1f, 1f, 1f);
					Vector2 sizeDelta = component.sizeDelta;
					float num10 = sizeDelta.y + 5f;
					num5 += num10;
					component.anchoredPosition3D = a - new Vector3(0f, 5f + num10 * (float)num6, 0f);
					int costCard = (int)(ConfigPrefsController.unitStats_Hero_Cost * (float)EnemyPrefsController.LevelIndexSelected);
					UIUnitCard component2 = gameObject.GetComponent<UIUnitCard>();
					component2.SetData(spriteUnitsHeroArray[num7], ConfigPrefsController.unitsStats_Hero_Name[num7], costCard, Faction.Humans, ArmyType.Heroe, num8, 0, num7);
					unitCardsArray.Add(component2);
					unitsObjectList.Add(gameObject);
					unitsTransformList.Add(component);
					num6++;
				}
			}
		}
		else if (scrollViewSelected == ArmyType.Soldier)
		{
			UpdateButtonSiblingIndex(buttonsSoldiersType, buttonsMercsType, buttonsHeroesType);
			glow[0].SetActive(value: true);
			glow[1].SetActive(value: false);
			glow[2].SetActive(value: false);
			int num11 = 0;
			for (int num12 = 0; num12 < 5; num12++)
			{
				if (num12 != 2 && (num12 != 0 || PlayerPrefsController.UnitsTechSpear[1] < 3) && (num12 != 1 || PlayerPrefsController.UnitsTechMelee[0] < 3) && (num12 != 3 || PlayerPrefsController.UnitsTechSpear[2] < 3) && (num12 != 4 || PlayerPrefsController.UnitsTechRanged[1] < 3))
				{
					continue;
				}
				bool flag2 = false;
				for (int num13 = 0; num13 < unitTypeSelected.Length; num13++)
				{
					if (unitTypeSelected[num13] == ArmyType.Soldier && unitIndexSelected[num13] == num12)
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					continue;
				}
				GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("UI/Unit_item")) as GameObject;
				RectTransform component3 = gameObject2.GetComponent<RectTransform>();
				component3.SetParent(contentUnitsTransform);
				component3.position = new Vector3(0f, 0f);
				component3.localScale = new Vector3(1f, 1f, 1f);
				Vector2 sizeDelta2 = component3.sizeDelta;
				float num14 = sizeDelta2.y + 5f;
				num5 += num14;
				component3.anchoredPosition3D = a - new Vector3(0f, 5f + num14 * (float)num11, 0f);
				int num15 = 0;
				int num16 = 0;
				UIUnitCard component4 = gameObject2.GetComponent<UIUnitCard>();
				switch (num12)
				{
				case 0:
					for (int num19 = PlayerPrefsController.UnitsTechRanged.Length - 1; num19 >= 0; num19--)
					{
						if (PlayerPrefsController.UnitsTechRanged[num19] >= 3)
						{
							num15 = num19 + 1;
							break;
						}
					}
					if (num15 >= PlayerPrefsController.UnitsTechRanged.Length)
					{
						num15 = PlayerPrefsController.UnitsTechRanged.Length - 1;
					}
					num16 = (int)(ConfigPrefsController.unitStats_Roman_Ranged_Cost * (float)EnemyPrefsController.LevelIndexSelected);
					component4.SetData(spriteUnitsSoldierRangedArray[num15], ConfigPrefsController.unitsStats_Soldier_Name[num12], num16, Faction.Humans, ArmyType.Soldier, num12, num15, num12);
					break;
				case 1:
					for (int num20 = PlayerPrefsController.UnitsTechSpear.Length - 1; num20 >= 0; num20--)
					{
						if (PlayerPrefsController.UnitsTechSpear[num20] >= 3)
						{
							num15 = num20 + 1;
							break;
						}
					}
					if (num15 >= PlayerPrefsController.UnitsTechSpear.Length)
					{
						num15 = PlayerPrefsController.UnitsTechSpear.Length - 1;
					}
					num16 = (int)(ConfigPrefsController.unitStats_Roman_Spear_Cost * (float)EnemyPrefsController.LevelIndexSelected);
					component4.SetData(spriteUnitsSoldierSpearArray[num15], ConfigPrefsController.unitsStats_Soldier_Name[num12], num16, Faction.Humans, ArmyType.Soldier, num12, num15, num12);
					break;
				case 2:
					for (int num21 = PlayerPrefsController.UnitsTechMelee.Length - 1; num21 >= 0; num21--)
					{
						if (PlayerPrefsController.UnitsTechMelee[num21] >= 3)
						{
							num15 = num21 + 1;
							break;
						}
					}
					if (num15 >= PlayerPrefsController.UnitsTechMelee.Length)
					{
						num15 = PlayerPrefsController.UnitsTechMelee.Length - 1;
					}
					num16 = (int)(ConfigPrefsController.unitStats_Roman_Melee_Cost * (float)EnemyPrefsController.LevelIndexSelected);
					component4.SetData(spriteUnitsSoldierMeleeArray[num15], ConfigPrefsController.unitsStats_Soldier_Name[num12], num16, Faction.Humans, ArmyType.Soldier, num12, num15, num12);
					break;
				case 3:
					for (int num18 = PlayerPrefsController.UnitsTechMounted.Length - 1; num18 >= 0; num18--)
					{
						if (PlayerPrefsController.UnitsTechMounted[num18] >= 3)
						{
							num15 = num18 + 1;
							break;
						}
					}
					if (num15 >= PlayerPrefsController.UnitsTechMounted.Length)
					{
						num15 = PlayerPrefsController.UnitsTechMounted.Length - 1;
					}
					num16 = (int)(ConfigPrefsController.unitStats_Roman_Mounted_Cost * (float)EnemyPrefsController.LevelIndexSelected);
					component4.SetData(spriteUnitsSoldierMountedArray[num15], ConfigPrefsController.unitsStats_Soldier_Name[num12], num16, Faction.Humans, ArmyType.Soldier, num12, num15, num12);
					break;
				case 4:
					for (int num17 = PlayerPrefsController.UnitsTechSiege.Length - 1; num17 >= 0; num17--)
					{
						if (PlayerPrefsController.UnitsTechSiege[num17] >= 3)
						{
							num15 = num17 + 1;
							break;
						}
					}
					if (num15 >= PlayerPrefsController.UnitsTechSiege.Length)
					{
						num15 = PlayerPrefsController.UnitsTechSiege.Length - 1;
					}
					num16 = (int)(ConfigPrefsController.unitStats_Roman_Siege_Cost * (float)EnemyPrefsController.LevelIndexSelected);
					component4.SetData(spriteUnitsSoldierSiegeArray[num15], ConfigPrefsController.unitsStats_Soldier_Name[num12], num16, Faction.Humans, ArmyType.Soldier, num12, num15, num12);
					break;
				}
				unitCardsArray.Add(component4);
				unitsObjectList.Add(gameObject2);
				unitsTransformList.Add(component3);
				num11++;
			}
		}
		else if (scrollViewSelected == ArmyType.Mercenary)
		{
			UpdateButtonSiblingIndex(buttonsMercsType, buttonsSoldiersType, buttonsHeroesType);
			glow[0].SetActive(value: false);
			glow[1].SetActive(value: false);
			glow[2].SetActive(value: true);
			int num22 = 0;
			for (int num23 = 0; num23 < PlayerPrefsController.UnitsTechMercenary.Length; num23++)
			{
				if (!PlayerPrefsController.UnitsTechMercenary[num23])
				{
					continue;
				}
				int num24 = 0;
				switch (num23)
				{
				case 0:
				case 5:
					num24 = 0;
					break;
				case 3:
				case 7:
					num24 = 1;
					break;
				case 2:
				case 4:
				case 8:
					num24 = 2;
					break;
				case 1:
				case 6:
					num24 = 3;
					break;
				}
				Faction faction = Faction.Humans;
				switch (num23)
				{
				case 0:
				case 1:
				case 2:
					faction = Faction.Goblins;
					break;
				case 3:
				case 4:
					faction = Faction.Skeletons;
					break;
				case 5:
				case 6:
					faction = Faction.Wolves;
					break;
				case 7:
				case 8:
					faction = Faction.Orcs;
					break;
				}
				bool flag3 = false;
				for (int num25 = 0; num25 < unitTypeSelected.Length; num25++)
				{
					if (unitTypeSelected[num25] == ArmyType.Mercenary && unitsFactionSelected[num25] == faction && unitIndexSelected[num25] == num24)
					{
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					GameObject gameObject3 = UnityEngine.Object.Instantiate(Resources.Load("UI/Unit_item")) as GameObject;
					RectTransform component5 = gameObject3.GetComponent<RectTransform>();
					component5.SetParent(contentUnitsTransform);
					component5.position = new Vector3(0f, 0f);
					component5.localScale = new Vector3(1f, 1f, 1f);
					Vector2 sizeDelta3 = component5.sizeDelta;
					float num26 = sizeDelta3.y + 5f;
					num5 += num26;
					component5.anchoredPosition3D = a - new Vector3(0f, 5f + num26 * (float)num22, 0f);
					int num27 = 0;
					UIUnitCard component6 = gameObject3.GetComponent<UIUnitCard>();
					switch (num23)
					{
					case 0:
						num27 = (int)(ConfigPrefsController.unitStats_Italian_Ranged_Cost * (float)EnemyPrefsController.LevelIndexSelected);
						num27 -= (int)((float)num27 * ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost);
						component6.SetData(spriteUnitsMercenaryArray[num23], ConfigPrefsController.unitsStats_Mercenary_Name[num23], num27, Faction.Goblins, ArmyType.Mercenary, num24, 1, num23);
						break;
					case 1:
						num27 = (int)(ConfigPrefsController.unitStats_Italian_Mounted_Cost * (float)EnemyPrefsController.LevelIndexSelected);
						num27 -= (int)((float)num27 * ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost);
						component6.SetData(spriteUnitsMercenaryArray[num23], ConfigPrefsController.unitsStats_Mercenary_Name[num23], num27, Faction.Goblins, ArmyType.Mercenary, num24, 1, num23);
						break;
					case 2:
						num27 = (int)(ConfigPrefsController.unitStats_Italian_Melee_Cost * (float)EnemyPrefsController.LevelIndexSelected);
						num27 -= (int)((float)num27 * ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost);
						component6.SetData(spriteUnitsMercenaryArray[num23], ConfigPrefsController.unitsStats_Mercenary_Name[num23], num27, Faction.Goblins, ArmyType.Mercenary, num24, 1, num23);
						break;
					case 3:
						num27 = (int)(ConfigPrefsController.unitStats_Gaul_Spear_Cost * (float)EnemyPrefsController.LevelIndexSelected);
						num27 -= (int)((float)num27 * ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost);
						component6.SetData(spriteUnitsMercenaryArray[num23], ConfigPrefsController.unitsStats_Mercenary_Name[num23], num27, Faction.Skeletons, ArmyType.Mercenary, num24, 1, num23);
						break;
					case 4:
						num27 = (int)(ConfigPrefsController.unitStats_Gaul_Melee_Cost * (float)EnemyPrefsController.LevelIndexSelected);
						num27 -= (int)((float)num27 * ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost);
						component6.SetData(spriteUnitsMercenaryArray[num23], ConfigPrefsController.unitsStats_Mercenary_Name[num23], num27, Faction.Skeletons, ArmyType.Mercenary, num24, 1, num23);
						break;
					case 5:
						num27 = (int)(ConfigPrefsController.unitStats_Iberian_Ranged_Cost * (float)EnemyPrefsController.LevelIndexSelected);
						num27 -= (int)((float)num27 * ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost);
						component6.SetData(spriteUnitsMercenaryArray[num23], ConfigPrefsController.unitsStats_Mercenary_Name[num23], num27, Faction.Wolves, ArmyType.Mercenary, num24, 1, num23);
						break;
					case 6:
						num27 = (int)(ConfigPrefsController.unitStats_Iberian_Mounted_Cost * (float)EnemyPrefsController.LevelIndexSelected);
						num27 -= (int)((float)num27 * ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost);
						component6.SetData(spriteUnitsMercenaryArray[num23], ConfigPrefsController.unitsStats_Mercenary_Name[num23], num27, Faction.Wolves, ArmyType.Mercenary, num24, 1, num23);
						break;
					case 7:
						num27 = (int)(ConfigPrefsController.unitStats_Carthaginian_Spear_Cost * (float)EnemyPrefsController.LevelIndexSelected);
						num27 -= (int)((float)num27 * ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost);
						component6.SetData(spriteUnitsMercenaryArray[num23], ConfigPrefsController.unitsStats_Mercenary_Name[num23], num27, Faction.Orcs, ArmyType.Mercenary, num24, 1, num23);
						break;
					case 8:
						num27 = (int)(ConfigPrefsController.unitStats_Carthaginian_Melee_Cost * (float)EnemyPrefsController.LevelIndexSelected);
						num27 -= (int)((float)num27 * ConfigPrefsController.generalArmyMercenaryCostPerLevel * (float)PlayerPrefsController.GeneralTechArmy_MercenaryCost);
						component6.SetData(spriteUnitsMercenaryArray[num23], ConfigPrefsController.unitsStats_Mercenary_Name[num23], num27, Faction.Orcs, ArmyType.Mercenary, num24, 1, num23);
						break;
					}
					unitCardsArray.Add(component6);
					unitsObjectList.Add(gameObject3);
					unitsTransformList.Add(component5);
					num22++;
				}
			}
		}
		contentUnitsTransform.sizeDelta = new Vector2(0f, num5);
		totalUnitsCost = 0;
		for (int num28 = 0; num28 < slotArmyArray.Length; num28++)
		{
			totalUnitsCost += unitsCostSelected[num28];
		}
		textTotalCost.text = totalUnitsCost.ToString("###,###,##0");
		if ((float)totalUnitsCost <= PlayerPrefs.GetFloat("playerMoney") && isAnyFirstLegion)
		{
			buttonAttack.interactable = true;
		}
		else
		{
			buttonAttack.interactable = false;
		}
		for (int num29 = 6; num29 < slotArmyObjectArray.Length; num29++)
		{
			if (num29 >= 6 && num29 < 12 && isAnyFirstLegion)
			{
				slotArmyObjectArray[num29].SetActive(value: true);
			}
			else if (num29 >= 12 && isAnySecondLegion)
			{
				slotArmyObjectArray[num29].SetActive(value: true);
			}
			else
			{
				slotArmyObjectArray[num29].SetActive(value: false);
			}
		}
	}

	public void ButtonUnitsHeroes()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		scrollViewSelected = ArmyType.Heroe;
		UpdateScrollView();
	}

	public void ButtonUnitsSoldiers()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		scrollViewSelected = ArmyType.Soldier;
		UpdateScrollView();
	}

	public void ButtonUnitsMercenaries()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		scrollViewSelected = ArmyType.Mercenary;
		UpdateScrollView();
	}

	public void ButtonStartAttack()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		if (tutorialController.GetActualIndex() == 22)
		{
			tutorialController.ActivateStep(23);
		}
		PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)totalUnitsCost);
		waveRoman.SetData(unitsFactionSelected, unitTypeSelected, unitIndexSelected, unitsLevelSelected);
		PlayerPrefsController.SaveStartInvasion(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"), unitTypeSelected, unitsIndexFormation);
		MainController.worldScreen = WorldScreen.Attack;
		MusicController component = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
		Object.Instantiate(Resources.Load("Canvas/CanvasLoadingInvasionNew"));
	}

	public void ClickSlotRemove(int _slotIndex)
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		slotArmyArray[_slotIndex].RemoveUnit();
		unitTypeSelected[_slotIndex] = ArmyType.None;
		unitIndexSelected[_slotIndex] = -1;
		unitsCostSelected[_slotIndex] = 0;
		unitsLevelSelected[_slotIndex] = 0;
		unitsIndexFormation[_slotIndex] = -1;
		UpdateScrollView();
	}

	public void StartDragCard(Sprite _imageSprite, ArmyType _selectedType, int _selectedIndex)
	{
		if (tutorialController.GetActualIndex() == 21)
		{
			tutorialController.ActivateStep(22);
		}
		cardMovingObject = (UnityEngine.Object.Instantiate(Resources.Load("UI/Unit_Item_Move")) as GameObject);
		cardMovingTransform = cardMovingObject.GetComponent<RectTransform>();
		cardMovingTransform.SetParent(myTransform);
		cardMovingTransform.localScale = new Vector3(1f, 1f, 1f);
		cardMovingTransform.position = UnityEngine.Input.mousePosition;
		UIUnitCardMove component = cardMovingObject.GetComponent<UIUnitCardMove>();
		component.SetData(_imageSprite, _selectedType, _selectedIndex);
		isDragging = true;
	}

	public void ReleaseCard(Sprite _imageSprite, Faction _selectedFaction, ArmyType _selectedType, int _selectedIndex, int _priceUnit, int _levelUnit, int _formationIndex, int autoAddIndex = -1)
	{
		isDragging = false;
		UnityEngine.Object.Destroy(cardMovingObject);
		cardMovingObject = null;
		cardMovingTransform = null;
		switch (autoAddIndex)
		{
		case -1:
		{
			sfxUIController.PlaySound(SfxUI.ClickDefault);
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			pointerEventData.position = UnityEngine.Input.mousePosition;
			List<RaycastResult> list = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerEventData, list);
			if (list.Count <= 0)
			{
				break;
			}
			int num2 = 0;
			while (true)
			{
				if (num2 < list.Count)
				{
					if (list[num2].gameObject.CompareTag("InvasionArmyBox"))
					{
						break;
					}
					num2++;
					continue;
				}
				return;
			}
			AddCardToArmy(list[num2].gameObject, _imageSprite, _selectedFaction, _selectedType, _priceUnit, _selectedIndex, _levelUnit, _formationIndex);
			break;
		}
		case -2:
		{
			sfxUIController.PlaySound(SfxUI.ClickDefault);
			int num = 0;
			while (true)
			{
				if (num < unitTypeSelected.Length)
				{
					if (unitTypeSelected[num] == ArmyType.None)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			AddCardToArmy(slotArmyArray[num].gameObject, _imageSprite, _selectedFaction, _selectedType, _priceUnit, _selectedIndex, _levelUnit, _formationIndex);
			break;
		}
		default:
			AddCardToArmy(slotArmyArray[autoAddIndex].gameObject, _imageSprite, _selectedFaction, _selectedType, _priceUnit, _selectedIndex, _levelUnit, _formationIndex);
			break;
		}
	}

	private void AddCardToArmy(GameObject _armySlotObject, Sprite _imageSprite, Faction _selectedFaction, ArmyType _selectedType, int _priceUnit, int _selectedIndex, int _selectedLevel, int _formationIndex)
	{
		UISlotArmy component = _armySlotObject.GetComponent<UISlotArmy>();
		if (tutorialController.GetActualIndex() == 21)
		{
			tutorialController.ActivateStep(22);
		}
		unitsFactionSelected[component.slotIndex] = _selectedFaction;
		unitTypeSelected[component.slotIndex] = _selectedType;
		unitIndexSelected[component.slotIndex] = _selectedIndex;
		unitsCostSelected[component.slotIndex] = _priceUnit;
		unitsLevelSelected[component.slotIndex] = _selectedLevel;
		unitsIndexFormation[component.slotIndex] = _formationIndex;
		int num = _priceUnit;
		component.AddUnit(_imageSprite, num.ToString("###,###,##0"));
		UpdateScrollView();
	}

	public void UpdateJustAttackButton()
	{
		if ((float)totalUnitsCost <= PlayerPrefs.GetFloat("playerMoney") && isAnyFirstLegion)
		{
			buttonAttack.interactable = true;
		}
		else
		{
			buttonAttack.interactable = false;
		}
	}

	public void UpdateButton(Button button, bool isInteractable, Image icon, Image background)
	{
		button.interactable = isInteractable;
		if (!isInteractable)
		{
			icon.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 76);
			background.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 76);
		}
		else
		{
			icon.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			background.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
	}

	private void UpdateButtonSiblingIndex(Button button1, Button button2, Button button3)
	{
		RectTransform component = button1.GetComponent<RectTransform>();
		RectTransform component2 = button2.GetComponent<RectTransform>();
		RectTransform component3 = button3.GetComponent<RectTransform>();
		RectTransform component4 = scrollBackground.GetComponent<RectTransform>();
		component2.SetSiblingIndex(0);
		component3.SetSiblingIndex(1);
		component4.SetSiblingIndex(2);
		component.SetSiblingIndex(3);
	}
}
