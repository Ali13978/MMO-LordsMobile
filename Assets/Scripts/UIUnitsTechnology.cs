using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitsTechnology : MonoBehaviour
{
	public UpgradeBar[] upgradeBarsMelee;

	public UpgradeBar[] upgradeBarsSpear;

	public UpgradeBar[] upgradeBarsMounted;

	public UpgradeBar[] upgradeBarsRanged;

	public UpgradeBar[] upgradeBarsSiege;

	public GameObject[] meleeProgressArrow;

	public GameObject[] spearProgressArrow;

	public GameObject[] rangedProgressArrow;

	public GameObject[] mountedProgressArrow;

	public GameObject[] siegeProgressArrow;

	public Image attackStat;

	public Image healthStat;

	public Image defenceStat;

	public Light light;

	public Button[] buttonUnitSpear;

	public Button[] buttonUnitMelee;

	public Button[] buttonUnitRanged;

	public Button[] buttonUnitMounted;

	public Button[] buttonUnitSiege;

	public Image[] imageUnitSpear;

	public Image[] imageUnitMelee;

	public Image[] imageUnitRanged;

	public Image[] imageUnitMounted;

	public Image[] imageUnitSiege;

	public Image imageConnectionStartMounted;

	public Image imageConnectionStartMelee;

	public Image imageConnectionStartRanged;

	public Image imageConnectionStartSiege;

	public Image[] imageConnectionSpear;

	public Image[] imageConnectionMelee;

	public Image[] imageConnectionRanged;

	public Image[] imageConnectionMounted;

	public Image[] imageConnectionSiege;

	public Image descriptionImage;

	public Color colorCardCompleted;

	public Color colorUncompleted;

	public Color colorCompleted;

	private float[] connectionValues = new float[4]
	{
		0.05f,
		0.4f,
		0.7f,
		1f
	};

	public Color colorCardUnavailable;

	public Text textTitle;

	public Button buttonUpgrade;

	public Text textSpecialA;

	public Text textSpecialB;

	public GameObject panelStats;

	public Color colorStatsNormal;

	public Color colorStatsBetter;

	public Text textStatsHP_Old;

	public Text textStatsHP_New;

	public Text textStatsATK_Old;

	public Text textStatsATK_New;

	public Text textStatsDEF_Old;

	public Text textStatsDEF_New;

	public Text textPrice;

	private string[] unitsSpearTitle = new string[7]
	{
		"PEASANT",
		"PEASANT SPEARMAN",
		"TOWN WATCH",
		"VIGILES",
		"RORARII",
		"TRIARII",
		"HOPLITE AUXILIA"
	};

	private string[] unitsMeleeTitle = new string[10]
	{
		"PEASANT SWORDMAN",
		"PEASANT MILITIA",
		"MILITIA",
		"EARLY HASTATI",
		"HASTATI",
		"PRINCIPES",
		"EARLY LEGIONARY",
		"LEGIONARY",
		"LEGIONARY FIRST",
		"PRAETORIAN"
	};

	private string[] unitsRangedTitle = new string[5]
	{
		"PEASANT SKIRMISHER",
		"LEVES",
		"VELITES",
		"JAVALINMAN AUXILIA",
		"PELTASTS AUXILIA"
	};

	private string[] unitsMountedTitle = new string[4]
	{
		"EQUITES",
		"CAVALRY",
		"LEGIONARY CAVALRY",
		"PRAETORIAN CAVALRY"
	};

	private string[] unitsSiegeTitle = new string[4]
	{
		"BATTERING RAM",
		"ADV. BATTERING RAM",
		"HEAVY BATTERING RAM",
		"TESTUDO BATTERING RAM"
	};

	private string unitsSpearStrong = "Strong vs. Cavalry";

	private string unitsSpearWeak = "Weak vs. Ranged";

	private string unitsMeleeStrong = "No Weakness";

	private string unitsMeleeWeak = "No Advantage";

	private string unitsRangedStrong = "Strong vs. Spearman";

	private string unitsRangedWeak = "Weak vs. Cavalry";

	private string unitsMountedStrong = "Strong vs. Ranged";

	private string unitsMountedWeak = "Weak vs. Spearman";

	private string unitsSiegeStrong = "Strong vs. Wall Gates";

	private string unitsSiegeWeak = string.Empty;

	public Sprite[] spriteCardsMelee;

	public Sprite[] spriteCardsSpear;

	public Sprite[] spriteCardsRanged;

	public Sprite[] spriteCardsMounted;

	public Sprite[] spriteCardsSiege;

	public RectTransform[] meleeTransform;

	public RectTransform[] spearTransform;

	public RectTransform[] rangedTransform;

	public RectTransform[] cavalryTransform;

	public RectTransform[] siegeTransform;

	private GameObject selectedObject;

	public GameObject coinObject;

	public GameObject tutorialButtonClose;

	public Image tutorialGlowClose;

	public Image tutorialGlowUpgrade;

	private TutorialController tutorialController;

	private SfxUIController sfxUiController;

	private AchievementsController achievementsController;

	private UIController uiController;

	private StationEngine stationEngine;

	private UnitType unitTypeSelected;

	private int cardSelected;

	private void Awake()
	{
	}

	public void Initialize(StationEngine stationEngine, UIController uiController, AchievementsController achievementsController, TutorialController tutorialController, SfxUIController sfxUiController)
	{
		this.uiController = uiController;
		this.achievementsController = achievementsController;
		this.tutorialController = tutorialController;
		this.sfxUiController = sfxUiController;
		this.stationEngine = stationEngine;
		Touch_Battle.IsWindowBigOpen = true;
		unitsSpearTitle[0] = ScriptLocalization.Get("UNITS/spear0").ToUpper();
		unitsSpearTitle[1] = ScriptLocalization.Get("UNITS/spear1").ToUpper();
		unitsSpearTitle[2] = ScriptLocalization.Get("UNITS/spear2").ToUpper();
		unitsSpearTitle[3] = ScriptLocalization.Get("UNITS/spear3").ToUpper();
		unitsSpearTitle[4] = ScriptLocalization.Get("UNITS/spear4").ToUpper();
		unitsSpearTitle[5] = ScriptLocalization.Get("UNITS/spear5").ToUpper();
		unitsMeleeTitle[0] = ScriptLocalization.Get("UNITS/melee0").ToUpper();
		unitsMeleeTitle[1] = ScriptLocalization.Get("UNITS/melee1").ToUpper();
		unitsMeleeTitle[2] = ScriptLocalization.Get("UNITS/melee2").ToUpper();
		unitsMeleeTitle[3] = ScriptLocalization.Get("UNITS/melee3").ToUpper();
		unitsMeleeTitle[4] = ScriptLocalization.Get("UNITS/melee4").ToUpper();
		unitsMeleeTitle[5] = ScriptLocalization.Get("UNITS/melee5").ToUpper();
		unitsMeleeTitle[6] = ScriptLocalization.Get("UNITS/melee6").ToUpper();
		unitsMeleeTitle[7] = ScriptLocalization.Get("UNITS/melee7").ToUpper();
		unitsMeleeTitle[8] = ScriptLocalization.Get("UNITS/melee8").ToUpper();
		unitsMeleeTitle[9] = ScriptLocalization.Get("UNITS/melee9").ToUpper();
		unitsRangedTitle[0] = ScriptLocalization.Get("UNITS/ranged0").ToUpper();
		unitsRangedTitle[1] = ScriptLocalization.Get("UNITS/ranged1").ToUpper();
		unitsRangedTitle[2] = ScriptLocalization.Get("UNITS/ranged2").ToUpper();
		unitsRangedTitle[3] = ScriptLocalization.Get("UNITS/ranged3").ToUpper();
		unitsRangedTitle[4] = ScriptLocalization.Get("UNITS/ranged4").ToUpper();
		unitsMountedTitle[0] = ScriptLocalization.Get("UNITS/mounted0").ToUpper();
		unitsMountedTitle[1] = ScriptLocalization.Get("UNITS/mounted1").ToUpper();
		unitsMountedTitle[2] = ScriptLocalization.Get("UNITS/mounted2").ToUpper();
		unitsMountedTitle[3] = ScriptLocalization.Get("UNITS/mounted3").ToUpper();
		unitsSiegeTitle[0] = ScriptLocalization.Get("UNITS/siege0").ToUpper();
		unitsSiegeTitle[1] = ScriptLocalization.Get("UNITS/siege1").ToUpper();
		unitsSiegeTitle[2] = ScriptLocalization.Get("UNITS/siege2").ToUpper();
		unitsSiegeTitle[3] = ScriptLocalization.Get("UNITS/siege3").ToUpper();
		unitsSpearStrong = ScriptLocalization.Get("UNITS/spearstrong").ToUpper();
		unitsSpearWeak = ScriptLocalization.Get("UNITS/spearweak").ToUpper();
		unitsMeleeStrong = ScriptLocalization.Get("UNITS/meleestrong").ToUpper();
		unitsMeleeWeak = ScriptLocalization.Get("UNITS/meleeweak").ToUpper();
		unitsRangedStrong = ScriptLocalization.Get("UNITS/rangedstrong").ToUpper();
		unitsRangedWeak = ScriptLocalization.Get("UNITS/rangedweak").ToUpper();
		unitsMountedStrong = ScriptLocalization.Get("UNITS/mountedstrong").ToUpper();
		unitsMountedWeak = ScriptLocalization.Get("UNITS/mountedweak").ToUpper();
		unitsSiegeStrong = ScriptLocalization.Get("UNITS/siegestrong").ToUpper();
		cardSelected = -1;
		textTitle.text = string.Empty;
		textSpecialA.text = string.Empty;
		textSpecialB.text = string.Empty;
		textPrice.text = string.Empty;
		buttonUpgrade.interactable = false;
		UpdateWindow();
		UpdateDescription();
		if (PlayerPrefsController.UnitsTechMelee[0] < 3)
		{
			ButtonCardMelee(0);
		}
		else
		{
			sfxUiController.PlaySound(SfxUI.ClickDefault);
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && !UIWindowTutorial.WindowOpen)
		{
			ButtonClose();
		}
	}

	public void UpdateWindow()
	{
		if (ProgressArrowAnimation.instance != null)
		{
			ProgressArrowAnimation.instance.RefeshAnimation();
		}
		if (PlayerPrefsController.UnitsTechMelee[0] == 3)
		{
			buttonUnitSpear[0].interactable = true;
			upgradeBarsSpear[0].gameObject.SetActive(value: true);
			imageConnectionStartMelee.color = colorCompleted;
			meleeProgressArrow[1].gameObject.SetActive(value: false);
		}
		else
		{
			buttonUnitSpear[0].interactable = false;
			upgradeBarsSpear[0].gameObject.SetActive(value: false);
			imageConnectionStartMelee.color = colorUncompleted;
			meleeProgressArrow[1].gameObject.SetActive(value: true);
			spearProgressArrow[0].gameObject.SetActive(value: true);
		}
		if (PlayerPrefsController.UnitsTechSpear[1] == 3)
		{
			buttonUnitRanged[0].interactable = true;
			upgradeBarsRanged[0].gameObject.SetActive(value: true);
			imageConnectionStartRanged.color = colorCompleted;
		}
		else
		{
			buttonUnitRanged[0].interactable = false;
			upgradeBarsRanged[0].gameObject.SetActive(value: false);
			imageConnectionStartRanged.color = colorUncompleted;
			if (PlayerPrefsController.UnitsTechSpear[0] == 3)
			{
				rangedProgressArrow[0].gameObject.SetActive(value: true);
			}
		}
		if (PlayerPrefsController.UnitsTechSpear[2] == 3)
		{
			buttonUnitMounted[0].interactable = true;
			imageConnectionStartMounted.color = colorCompleted;
			upgradeBarsMounted[0].gameObject.SetActive(value: true);
		}
		else
		{
			buttonUnitMounted[0].interactable = false;
			imageConnectionStartMounted.color = colorUncompleted;
			upgradeBarsMounted[0].gameObject.SetActive(value: false);
			if (PlayerPrefsController.UnitsTechSpear[1] == 3)
			{
				mountedProgressArrow[0].gameObject.SetActive(value: true);
			}
		}
		if (PlayerPrefsController.UnitsTechRanged[1] == 3)
		{
			buttonUnitSiege[0].interactable = true;
			upgradeBarsSiege[0].gameObject.SetActive(value: true);
			imageConnectionStartSiege.color = colorCompleted;
		}
		else
		{
			buttonUnitSiege[0].interactable = false;
			upgradeBarsSiege[0].gameObject.SetActive(value: false);
			imageConnectionStartSiege.color = colorUncompleted;
			if (PlayerPrefsController.UnitsTechRanged[0] == 3)
			{
				siegeProgressArrow[0].gameObject.SetActive(value: true);
			}
		}
		for (int i = 0; i < PlayerPrefsController.UnitsTechSpear.Length; i++)
		{
			upgradeBarsSpear[0].TurnSlotOn(PlayerPrefsController.UnitsTechSpear[i]);
			if (PlayerPrefsController.UnitsTechSpear[0] == 3)
			{
				upgradeBarsSpear[0].gameObject.SetActive(value: false);
			}
			if (i > 0)
			{
				if (i + 1 < PlayerPrefsController.UnitsTechSpear.Length)
				{
					if (PlayerPrefsController.UnitsTechSpear[i] == 3)
					{
						spearProgressArrow[i + 1].gameObject.SetActive(value: false);
					}
					else
					{
						spearProgressArrow[i + 1].gameObject.SetActive(value: true);
					}
					if (PlayerPrefsController.UnitsTechSpear[i - 1] == 3)
					{
						spearProgressArrow[i + 1].gameObject.SetActive(value: true);
						spearProgressArrow[i].gameObject.SetActive(value: false);
					}
					else
					{
						spearProgressArrow[i + 1].gameObject.SetActive(value: false);
					}
				}
				if (i >= PlayerPrefsController.UnitsTechSpear.Length - 1 && PlayerPrefsController.UnitsTechSpear[i - 1] == 3)
				{
					spearProgressArrow[PlayerPrefsController.UnitsTechSpear.Length - 1].gameObject.SetActive(value: false);
				}
			}
			else if (PlayerPrefsController.UnitsTechSpear[0] != 3 && PlayerPrefsController.UnitsTechMelee[0] == 3)
			{
				spearProgressArrow[0].gameObject.SetActive(value: false);
				spearProgressArrow[1].gameObject.SetActive(value: true);
			}
		}
		for (int j = 1; j < PlayerPrefsController.UnitsTechSpear.Length; j++)
		{
			upgradeBarsSpear[j].TurnSlotOn(PlayerPrefsController.UnitsTechSpear[j]);
			upgradeBarsSpear[j].gameObject.SetActive(value: true);
			if (PlayerPrefsController.UnitsTechSpear[0] == 3)
			{
				upgradeBarsSpear[0].gameObject.SetActive(value: false);
			}
			if (j > 0)
			{
				if (PlayerPrefsController.UnitsTechSpear[j] == 3)
				{
					upgradeBarsSpear[j].gameObject.SetActive(value: false);
				}
				else if (PlayerPrefsController.UnitsTechSpear[j - 1] == 3)
				{
					buttonUnitSpear[j].interactable = true;
					upgradeBarsSpear[j - 1].gameObject.SetActive(value: false);
					upgradeBarsSpear[j].gameObject.SetActive(value: true);
				}
				else
				{
					upgradeBarsSpear[j].gameObject.SetActive(value: false);
					buttonUnitSpear[j].interactable = false;
				}
			}
		}
		for (int k = 0; k < PlayerPrefsController.UnitsTechMelee.Length; k++)
		{
			upgradeBarsMelee[0].TurnSlotOn(PlayerPrefsController.UnitsTechMelee[k]);
			if (PlayerPrefsController.UnitsTechMelee[0] == 3)
			{
				upgradeBarsMelee[0].gameObject.SetActive(value: false);
			}
		}
		for (int l = 1; l < PlayerPrefsController.UnitsTechMelee.Length; l++)
		{
			upgradeBarsMelee[l].TurnSlotOn(PlayerPrefsController.UnitsTechMelee[l]);
			upgradeBarsMelee[l].gameObject.SetActive(value: true);
			if (PlayerPrefsController.UnitsTechMelee[0] == 3)
			{
				upgradeBarsMelee[0].gameObject.SetActive(value: false);
			}
			if (PlayerPrefsController.UnitsTechMelee[l] == 3)
			{
				upgradeBarsMelee[l].gameObject.SetActive(value: false);
			}
			else if (PlayerPrefsController.UnitsTechMelee[l - 1] == 3)
			{
				buttonUnitMelee[l].interactable = true;
				upgradeBarsMelee[l - 1].gameObject.SetActive(value: false);
				upgradeBarsMelee[l].gameObject.SetActive(value: true);
			}
			else
			{
				upgradeBarsMelee[l].gameObject.SetActive(value: false);
				buttonUnitMelee[l].interactable = false;
			}
			if (l + 1 < PlayerPrefsController.UnitsTechMelee.Length)
			{
				if (PlayerPrefsController.UnitsTechMelee[l] == 3)
				{
					meleeProgressArrow[l + 1].gameObject.SetActive(value: false);
				}
				else
				{
					meleeProgressArrow[l + 1].gameObject.SetActive(value: true);
				}
				if (PlayerPrefsController.UnitsTechMelee[l - 1] == 3)
				{
					meleeProgressArrow[l + 1].gameObject.SetActive(value: true);
					meleeProgressArrow[l].gameObject.SetActive(value: false);
				}
				else
				{
					meleeProgressArrow[l + 1].gameObject.SetActive(value: false);
				}
			}
			if (l >= PlayerPrefsController.UnitsTechMelee.Length - 1 && PlayerPrefsController.UnitsTechMelee[l - 1] == 3)
			{
				meleeProgressArrow[PlayerPrefsController.UnitsTechMelee.Length - 1].gameObject.SetActive(value: false);
			}
		}
		for (int m = 0; m < PlayerPrefsController.UnitsTechRanged.Length; m++)
		{
			upgradeBarsRanged[0].TurnSlotOn(PlayerPrefsController.UnitsTechRanged[m]);
			if (PlayerPrefsController.UnitsTechRanged[0] == 3)
			{
				upgradeBarsRanged[0].gameObject.SetActive(value: false);
			}
			if (m > 0)
			{
				if (m + 1 < PlayerPrefsController.UnitsTechRanged.Length)
				{
					if (PlayerPrefsController.UnitsTechRanged[m] == 3)
					{
						rangedProgressArrow[m + 1].gameObject.SetActive(value: false);
					}
					else
					{
						rangedProgressArrow[m + 1].gameObject.SetActive(value: true);
					}
					if (PlayerPrefsController.UnitsTechRanged[m - 1] == 3)
					{
						rangedProgressArrow[m + 1].gameObject.SetActive(value: true);
						rangedProgressArrow[m].gameObject.SetActive(value: false);
					}
					else
					{
						rangedProgressArrow[m + 1].gameObject.SetActive(value: false);
					}
				}
				if (m >= PlayerPrefsController.UnitsTechRanged.Length - 1 && PlayerPrefsController.UnitsTechRanged[m - 1] == 3)
				{
					rangedProgressArrow[PlayerPrefsController.UnitsTechRanged.Length - 1].gameObject.SetActive(value: false);
				}
			}
			else if (PlayerPrefsController.UnitsTechRanged[0] != 3 && PlayerPrefsController.UnitsTechSpear[1] == 3)
			{
				rangedProgressArrow[0].gameObject.SetActive(value: false);
				rangedProgressArrow[1].gameObject.SetActive(value: true);
			}
		}
		for (int n = 1; n < PlayerPrefsController.UnitsTechRanged.Length; n++)
		{
			upgradeBarsRanged[n].TurnSlotOn(PlayerPrefsController.UnitsTechRanged[n]);
			upgradeBarsRanged[n].gameObject.SetActive(value: true);
			if (PlayerPrefsController.UnitsTechRanged[0] == 3)
			{
				upgradeBarsRanged[0].gameObject.SetActive(value: false);
			}
			if (n > 0)
			{
				if (PlayerPrefsController.UnitsTechRanged[n] == 3)
				{
					upgradeBarsRanged[n].gameObject.SetActive(value: false);
				}
				else if (PlayerPrefsController.UnitsTechRanged[n - 1] == 3)
				{
					upgradeBarsRanged[n - 1].gameObject.SetActive(value: false);
					upgradeBarsRanged[n].gameObject.SetActive(value: true);
					buttonUnitRanged[n].interactable = true;
				}
				else
				{
					upgradeBarsRanged[n].gameObject.SetActive(value: false);
					buttonUnitRanged[n].interactable = false;
				}
			}
			if (n + 1 < PlayerPrefsController.UnitsTechRanged.Length)
			{
				if (PlayerPrefsController.UnitsTechRanged[n] == 3)
				{
					imageConnectionRanged[n].color = colorCompleted;
				}
				else
				{
					imageConnectionRanged[n].color = colorUncompleted;
				}
			}
		}
		for (int num = 0; num < PlayerPrefsController.UnitsTechMounted.Length; num++)
		{
			upgradeBarsMounted[0].TurnSlotOn(PlayerPrefsController.UnitsTechMounted[num]);
			if (PlayerPrefsController.UnitsTechMounted[0] == 3)
			{
				upgradeBarsMounted[0].gameObject.SetActive(value: false);
			}
			if (num > 0)
			{
				if (num + 1 < PlayerPrefsController.UnitsTechMounted.Length)
				{
					if (PlayerPrefsController.UnitsTechMounted[num] == 3)
					{
						mountedProgressArrow[num + 1].gameObject.SetActive(value: false);
					}
					else
					{
						mountedProgressArrow[num + 1].gameObject.SetActive(value: true);
					}
					if (PlayerPrefsController.UnitsTechMounted[num - 1] == 3)
					{
						mountedProgressArrow[num + 1].gameObject.SetActive(value: true);
						mountedProgressArrow[num].gameObject.SetActive(value: false);
					}
					else
					{
						mountedProgressArrow[num + 1].gameObject.SetActive(value: false);
					}
				}
				if (num >= PlayerPrefsController.UnitsTechMounted.Length - 1 && PlayerPrefsController.UnitsTechMounted[num - 1] == 3)
				{
					mountedProgressArrow[PlayerPrefsController.UnitsTechMounted.Length - 1].gameObject.SetActive(value: false);
				}
			}
			else if (PlayerPrefsController.UnitsTechMounted[0] != 3 && PlayerPrefsController.UnitsTechSpear[2] == 3)
			{
				mountedProgressArrow[0].gameObject.SetActive(value: false);
				mountedProgressArrow[1].gameObject.SetActive(value: true);
			}
		}
		for (int num2 = 1; num2 < PlayerPrefsController.UnitsTechMounted.Length; num2++)
		{
			upgradeBarsMounted[num2].TurnSlotOn(PlayerPrefsController.UnitsTechMounted[num2]);
			upgradeBarsMounted[num2].gameObject.SetActive(value: true);
			if (num2 > 0)
			{
				if (PlayerPrefsController.UnitsTechMounted[num2] == 3)
				{
					upgradeBarsMounted[num2].gameObject.SetActive(value: false);
				}
				else if (PlayerPrefsController.UnitsTechMounted[num2 - 1] == 3)
				{
					upgradeBarsMounted[num2 - 1].gameObject.SetActive(value: false);
					upgradeBarsMounted[num2].gameObject.SetActive(value: true);
					buttonUnitMounted[num2].interactable = true;
				}
				else
				{
					upgradeBarsMounted[num2].gameObject.SetActive(value: false);
					buttonUnitMounted[num2].interactable = false;
				}
			}
			if (num2 + 1 < PlayerPrefsController.UnitsTechMounted.Length)
			{
				if (PlayerPrefsController.UnitsTechMounted[num2] == 3)
				{
					imageConnectionMounted[num2].color = colorCompleted;
				}
				else
				{
					imageConnectionMounted[num2].color = colorUncompleted;
				}
			}
		}
		for (int num3 = 0; num3 < PlayerPrefsController.UnitsTechSiege.Length; num3++)
		{
			upgradeBarsSiege[0].TurnSlotOn(PlayerPrefsController.UnitsTechSiege[num3]);
			if (PlayerPrefsController.UnitsTechSiege[0] == 3)
			{
				upgradeBarsSiege[0].gameObject.SetActive(value: false);
			}
			if (num3 > 0)
			{
				if (num3 + 1 < PlayerPrefsController.UnitsTechSiege.Length)
				{
					if (PlayerPrefsController.UnitsTechSiege[num3] == 3)
					{
						siegeProgressArrow[num3 + 1].gameObject.SetActive(value: false);
					}
					else
					{
						siegeProgressArrow[num3 + 1].gameObject.SetActive(value: true);
					}
					if (PlayerPrefsController.UnitsTechSiege[num3 - 1] == 3)
					{
						siegeProgressArrow[num3 + 1].gameObject.SetActive(value: true);
						siegeProgressArrow[num3].gameObject.SetActive(value: false);
					}
					else
					{
						siegeProgressArrow[num3 + 1].gameObject.SetActive(value: false);
					}
				}
				if (num3 >= PlayerPrefsController.UnitsTechSiege.Length - 1 && PlayerPrefsController.UnitsTechSiege[num3 - 1] == 3)
				{
					siegeProgressArrow[PlayerPrefsController.UnitsTechSiege.Length - 1].gameObject.SetActive(value: false);
				}
			}
			else if (PlayerPrefsController.UnitsTechSiege[0] != 3 && PlayerPrefsController.UnitsTechRanged[1] == 3)
			{
				siegeProgressArrow[0].gameObject.SetActive(value: false);
				siegeProgressArrow[1].gameObject.SetActive(value: true);
			}
		}
		for (int num4 = 1; num4 < PlayerPrefsController.UnitsTechSiege.Length; num4++)
		{
			upgradeBarsSiege[num4].TurnSlotOn(PlayerPrefsController.UnitsTechSiege[num4]);
			upgradeBarsSiege[num4].gameObject.SetActive(value: true);
			if (num4 > 0)
			{
				if (PlayerPrefsController.UnitsTechSiege[num4] == 3)
				{
					upgradeBarsSiege[num4].gameObject.SetActive(value: false);
				}
				else if (PlayerPrefsController.UnitsTechSiege[num4 - 1] == 3)
				{
					upgradeBarsSiege[num4 - 1].gameObject.SetActive(value: false);
					upgradeBarsSiege[num4].gameObject.SetActive(value: true);
					buttonUnitSiege[num4].interactable = true;
				}
				else
				{
					upgradeBarsSiege[num4].gameObject.SetActive(value: false);
					buttonUnitSiege[num4].interactable = false;
				}
			}
			if (num4 + 1 < PlayerPrefsController.UnitsTechSiege.Length)
			{
				if (PlayerPrefsController.UnitsTechSiege[num4] == 3)
				{
					imageConnectionSiege[num4].color = colorCompleted;
				}
				else
				{
					imageConnectionSiege[num4].color = colorUncompleted;
				}
			}
		}
		for (int num5 = 0; num5 < imageUnitMelee.Length; num5++)
		{
			if (num5 > 0)
			{
				if (PlayerPrefsController.UnitsTechMelee[num5 - 1] == 3)
				{
					imageUnitMelee[num5].sprite = spriteCardsMelee[num5];
				}
			}
			else
			{
				imageUnitMelee[num5].sprite = spriteCardsMelee[num5];
			}
		}
		for (int num6 = 0; num6 < imageUnitSpear.Length; num6++)
		{
			if (num6 > 0)
			{
				if (PlayerPrefsController.UnitsTechSpear[num6 - 1] == 3)
				{
					imageUnitSpear[num6].sprite = spriteCardsSpear[num6];
				}
			}
			else if (PlayerPrefsController.UnitsTechMelee[0] == 3)
			{
				imageUnitSpear[num6].sprite = spriteCardsSpear[num6];
			}
		}
		for (int num7 = 0; num7 < imageUnitRanged.Length; num7++)
		{
			if (num7 > 0)
			{
				if (PlayerPrefsController.UnitsTechRanged[num7 - 1] == 3)
				{
					imageUnitRanged[num7].sprite = spriteCardsRanged[num7];
				}
			}
			else if (PlayerPrefsController.UnitsTechSpear[1] == 3)
			{
				imageUnitRanged[num7].sprite = spriteCardsRanged[num7];
			}
		}
		for (int num8 = 0; num8 < imageUnitMounted.Length; num8++)
		{
			if (num8 > 0)
			{
				if (PlayerPrefsController.UnitsTechMounted[num8 - 1] == 3)
				{
					imageUnitMounted[num8].sprite = spriteCardsMounted[num8];
				}
			}
			else if (PlayerPrefsController.UnitsTechSpear[2] == 3)
			{
				imageUnitMounted[num8].sprite = spriteCardsMounted[num8];
			}
		}
		for (int num9 = 0; num9 < imageUnitSiege.Length; num9++)
		{
			if (num9 > 0)
			{
				if (PlayerPrefsController.UnitsTechSiege[num9 - 1] == 3)
				{
					imageUnitSiege[num9].sprite = spriteCardsSiege[num9];
				}
			}
			else if (PlayerPrefsController.UnitsTechRanged[1] == 3)
			{
				imageUnitSiege[num9].sprite = spriteCardsSiege[num9];
			}
		}
		float num10 = 1f;
		for (int num11 = 0; num11 < buttonUnitMelee.Length; num11++)
		{
			LeanTween.cancel(buttonUnitMelee[num11].gameObject);
			buttonUnitMelee[num11].transform.localScale = new Vector3(1f, 1f, 1f);
			if (buttonUnitMelee[num11].IsInteractable() && PlayerPrefsController.UnitsTechMelee[num11] < 3)
			{
				imageUnitMelee[num11].color = Color.white;
			}
			else if (PlayerPrefsController.UnitsTechMelee[num11] == 3)
			{
				imageUnitMelee[num11].color = colorCardCompleted;
			}
			else
			{
				imageUnitMelee[num11].color = colorCardUnavailable;
			}
		}
		for (int num12 = 0; num12 < buttonUnitRanged.Length; num12++)
		{
			LeanTween.cancel(buttonUnitRanged[num12].gameObject);
			buttonUnitRanged[num12].transform.localScale = new Vector3(1f, 1f, 1f);
			if (buttonUnitRanged[num12].IsInteractable() && PlayerPrefsController.UnitsTechRanged[num12] < 3)
			{
				imageUnitRanged[num12].color = Color.white;
			}
			else if (PlayerPrefsController.UnitsTechRanged[num12] == 3)
			{
				imageUnitRanged[num12].color = colorCardCompleted;
			}
			else
			{
				imageUnitRanged[num12].color = colorCardUnavailable;
			}
		}
		for (int num13 = 0; num13 < buttonUnitSpear.Length; num13++)
		{
			LeanTween.cancel(buttonUnitSpear[num13].gameObject);
			buttonUnitSpear[num13].transform.localScale = new Vector3(1f, 1f, 1f);
			if (buttonUnitSpear[num13].IsInteractable() && PlayerPrefsController.UnitsTechSpear[num13] < 3)
			{
				imageUnitSpear[num13].color = Color.white;
			}
			else if (PlayerPrefsController.UnitsTechSpear[num13] == 3)
			{
				imageUnitSpear[num13].color = colorCardCompleted;
			}
			else
			{
				imageUnitSpear[num13].color = colorCardUnavailable;
			}
		}
		for (int num14 = 0; num14 < buttonUnitMounted.Length; num14++)
		{
			LeanTween.cancel(buttonUnitMounted[num14].gameObject);
			buttonUnitMounted[num14].transform.localScale = new Vector3(1f, 1f, 1f);
			if (buttonUnitMounted[num14].IsInteractable() && PlayerPrefsController.UnitsTechMounted[num14] < 3)
			{
				imageUnitMounted[num14].color = Color.white;
			}
			else if (PlayerPrefsController.UnitsTechMounted[num14] == 3)
			{
				imageUnitMounted[num14].color = colorCardCompleted;
			}
			else
			{
				imageUnitMounted[num14].color = colorCardUnavailable;
			}
		}
		for (int num15 = 0; num15 < buttonUnitSiege.Length; num15++)
		{
			LeanTween.cancel(buttonUnitSiege[num15].gameObject);
			buttonUnitSiege[num15].transform.localScale = new Vector3(1f, 1f, 1f);
			if (buttonUnitSiege[num15].IsInteractable() && PlayerPrefsController.UnitsTechSiege[num15] < 3)
			{
				imageUnitSiege[num15].color = Color.white;
			}
			else if (PlayerPrefsController.UnitsTechSiege[num15] == 3)
			{
				imageUnitSiege[num15].color = colorCardCompleted;
			}
			else
			{
				imageUnitSiege[num15].color = colorCardUnavailable;
			}
		}
	}

	public void ButtonCardSpear(int _cardIndex)
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		cardSelected = _cardIndex;
		unitTypeSelected = UnitType.Spear;
		UpdateWindow();
		UpdateDescription();
		if (selectedObject != null)
		{
			UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
		}
		descriptionImage.sprite = imageUnitSpear[_cardIndex].sprite;
		selectedObject = (UnityEngine.Object.Instantiate(Resources.Load("UI/WindowUnits/SelectorUnits"), spearTransform[_cardIndex].position, Quaternion.Euler(Vector3.zero)) as GameObject);
		RectTransform component = selectedObject.GetComponent<RectTransform>();
		component.SetParent(spearTransform[_cardIndex]);
		component.localScale = new Vector3(1f, 1f, 1f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = new Vector2(6f, 6f);
	}

	public void ButtonCardRanged(int _cardIndex)
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		cardSelected = _cardIndex;
		unitTypeSelected = UnitType.Archer;
		UpdateWindow();
		UpdateDescription();
		if (selectedObject != null)
		{
			UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
		}
		descriptionImage.sprite = imageUnitRanged[_cardIndex].sprite;
		selectedObject = (UnityEngine.Object.Instantiate(Resources.Load("UI/WindowUnits/SelectorUnits"), rangedTransform[_cardIndex].position, Quaternion.Euler(Vector3.zero)) as GameObject);
		RectTransform component = selectedObject.GetComponent<RectTransform>();
		component.SetParent(rangedTransform[_cardIndex]);
		component.localScale = new Vector3(1f, 1f, 1f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = new Vector2(6f, 6f);
	}

	public void ButtonCardMounted(int _cardIndex)
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		cardSelected = _cardIndex;
		unitTypeSelected = UnitType.Mounted;
		UpdateWindow();
		UpdateDescription();
		if (selectedObject != null)
		{
			UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
		}
		descriptionImage.sprite = imageUnitMounted[_cardIndex].sprite;
		selectedObject = (UnityEngine.Object.Instantiate(Resources.Load("UI/WindowUnits/SelectorUnits"), cavalryTransform[_cardIndex].position, Quaternion.Euler(Vector3.zero)) as GameObject);
		RectTransform component = selectedObject.GetComponent<RectTransform>();
		component.SetParent(cavalryTransform[_cardIndex]);
		component.localScale = new Vector3(1f, 1f, 1f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = new Vector2(6f, 6f);
	}

	public void ButtonCardMelee(int _cardIndex)
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		cardSelected = _cardIndex;
		unitTypeSelected = UnitType.Sword;
		UpdateWindow();
		UpdateDescription();
		if (selectedObject != null)
		{
			UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
		}
		descriptionImage.sprite = imageUnitMelee[_cardIndex].sprite;
		selectedObject = (UnityEngine.Object.Instantiate(Resources.Load("UI/WindowUnits/SelectorUnits"), meleeTransform[_cardIndex].position, Quaternion.Euler(Vector3.zero)) as GameObject);
		RectTransform component = selectedObject.GetComponent<RectTransform>();
		component.SetParent(meleeTransform[_cardIndex]);
		component.localScale = new Vector3(1f, 1f, 1f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = new Vector2(6f, 6f);
	}

	public void ButtonCardSiege(int _cardIndex)
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		cardSelected = _cardIndex;
		unitTypeSelected = UnitType.Siege;
		UpdateWindow();
		UpdateDescription();
		if (selectedObject != null)
		{
			UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
		}
		descriptionImage.sprite = imageUnitSiege[_cardIndex].sprite;
		selectedObject = (UnityEngine.Object.Instantiate(Resources.Load("UI/WindowUnits/SelectorUnits"), siegeTransform[_cardIndex].position, Quaternion.Euler(Vector3.zero)) as GameObject);
		RectTransform component = selectedObject.GetComponent<RectTransform>();
		component.SetParent(siegeTransform[_cardIndex]);
		component.localScale = new Vector3(1f, 1f, 1f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = new Vector2(6f, 6f);
	}

	public void UpdateDescription()
	{
		int num = 0;
		if (cardSelected >= 0)
		{
			coinObject.SetActive(value: true);
			switch (unitTypeSelected)
			{
			case UnitType.Spear:
			{
				textTitle.text = unitsSpearTitle[cardSelected] + "\n" + (PlayerPrefsController.UnitsTechSpear[cardSelected] + 1).ToString() + " / 4";
				if (PlayerPrefsController.UnitsTechSpear[cardSelected] < 3)
				{
					num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.Spear, cardSelected);
					textPrice.text = num.ToString("###,###,##0");
				}
				else
				{
					textPrice.text = string.Empty;
				}
				textSpecialA.text = unitsSpearStrong;
				textSpecialB.text = unitsSpearWeak;
				float num2 = (int)ConfigPrefsController.GetUnitsRomanHealth(CohortType.Mixed, CohortSubType.Spear, cardSelected, PlayerPrefsController.UnitsTechSpear[cardSelected]);
				float num3 = (int)ConfigPrefsController.GetUnitsRomanAttack(CohortType.Mixed, CohortSubType.Spear, cardSelected, PlayerPrefsController.UnitsTechSpear[cardSelected]);
				float num4 = (int)ConfigPrefsController.GetUnitsRomanDefence(CohortType.Mixed, CohortSubType.Spear, cardSelected, PlayerPrefsController.UnitsTechSpear[cardSelected]);
				textStatsHP_Old.text = num2.ToString();
				textStatsATK_Old.text = num3.ToString();
				textStatsDEF_Old.text = num4.ToString();
				if (PlayerPrefsController.UnitsTechSpear[cardSelected] < 3)
				{
					float unitsRomanHealth = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Mixed, CohortSubType.Spear, cardSelected, PlayerPrefsController.UnitsTechSpear[cardSelected] + 1);
					float unitsRomanAttack = ConfigPrefsController.GetUnitsRomanAttack(CohortType.Mixed, CohortSubType.Spear, cardSelected, PlayerPrefsController.UnitsTechSpear[cardSelected] + 1);
					float unitsRomanDefence = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Mixed, CohortSubType.Spear, cardSelected, PlayerPrefsController.UnitsTechSpear[cardSelected] + 1);
					if (PlayerPrefs.GetFloat("playerMoney") >= (float)num)
					{
						buttonUpgrade.interactable = true;
					}
					else
					{
						buttonUpgrade.interactable = false;
					}
					textStatsHP_New.text = unitsRomanHealth.ToString();
					textStatsATK_New.text = unitsRomanAttack.ToString();
					textStatsDEF_New.text = unitsRomanDefence.ToString();
					if (unitsRomanHealth > num2)
					{
						textStatsHP_New.color = colorStatsBetter;
					}
					else
					{
						textStatsHP_New.color = colorStatsNormal;
					}
					if (unitsRomanAttack > num3)
					{
						textStatsATK_New.color = colorStatsBetter;
					}
					else
					{
						textStatsATK_New.color = colorStatsNormal;
					}
					if (unitsRomanDefence > num4)
					{
						textStatsDEF_New.color = colorStatsBetter;
					}
					else
					{
						textStatsDEF_New.color = colorStatsNormal;
					}
				}
				else
				{
					buttonUpgrade.interactable = false;
					textStatsHP_New.text = string.Empty;
					textStatsATK_New.text = string.Empty;
					textStatsDEF_New.text = string.Empty;
				}
				UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechSpear);
				break;
			}
			case UnitType.Archer:
			{
				textTitle.text = unitsRangedTitle[cardSelected] + "\n" + (PlayerPrefsController.UnitsTechRanged[cardSelected] + 1).ToString() + " / 4";
				if (PlayerPrefsController.UnitsTechRanged[cardSelected] < 3)
				{
					num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Ranged, CohortSubType.Javaline, cardSelected);
					textPrice.text = num.ToString("###,###,##0");
				}
				else
				{
					textPrice.text = string.Empty;
				}
				textSpecialA.text = unitsRangedStrong;
				textSpecialB.text = unitsRangedWeak;
				float num2 = (int)ConfigPrefsController.GetUnitsRomanHealth(CohortType.Ranged, CohortSubType.Javaline, cardSelected, PlayerPrefsController.UnitsTechRanged[cardSelected]);
				float num3 = (int)ConfigPrefsController.GetUnitsRomanRangedAttack(CohortType.Ranged, CohortSubType.Javaline, cardSelected, PlayerPrefsController.UnitsTechRanged[cardSelected]);
				float num4 = (int)ConfigPrefsController.GetUnitsRomanDefence(CohortType.Ranged, CohortSubType.Javaline, cardSelected, PlayerPrefsController.UnitsTechRanged[cardSelected]);
				textStatsHP_Old.text = num2.ToString();
				textStatsATK_Old.text = num3.ToString();
				textStatsDEF_Old.text = num4.ToString();
				if (PlayerPrefsController.UnitsTechRanged[cardSelected] < 3)
				{
					float unitsRomanHealth = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Ranged, CohortSubType.Javaline, cardSelected, PlayerPrefsController.UnitsTechRanged[cardSelected] + 1);
					float unitsRomanAttack = ConfigPrefsController.GetUnitsRomanRangedAttack(CohortType.Ranged, CohortSubType.Javaline, cardSelected, PlayerPrefsController.UnitsTechRanged[cardSelected] + 1);
					float unitsRomanDefence = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Ranged, CohortSubType.Javaline, cardSelected, PlayerPrefsController.UnitsTechRanged[cardSelected] + 1);
					if (PlayerPrefs.GetFloat("playerMoney") >= (float)num)
					{
						buttonUpgrade.interactable = true;
					}
					else
					{
						buttonUpgrade.interactable = false;
					}
					textStatsHP_New.text = unitsRomanHealth.ToString();
					textStatsATK_New.text = unitsRomanAttack.ToString();
					textStatsDEF_New.text = unitsRomanDefence.ToString();
					if (unitsRomanHealth > num2)
					{
						textStatsHP_New.color = colorStatsBetter;
					}
					else
					{
						textStatsHP_New.color = colorStatsNormal;
					}
					if (unitsRomanAttack > num3)
					{
						textStatsATK_New.color = colorStatsBetter;
					}
					else
					{
						textStatsATK_New.color = colorStatsNormal;
					}
					if (unitsRomanDefence > num4)
					{
						textStatsDEF_New.color = colorStatsBetter;
					}
					else
					{
						textStatsDEF_New.color = colorStatsNormal;
					}
				}
				else
				{
					buttonUpgrade.interactable = false;
					textStatsHP_New.text = string.Empty;
					textStatsATK_New.text = string.Empty;
					textStatsDEF_New.text = string.Empty;
				}
				UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechRanged);
				break;
			}
			case UnitType.Mounted:
			{
				textTitle.text = unitsMountedTitle[cardSelected] + "\n" + (PlayerPrefsController.UnitsTechMounted[cardSelected] + 1).ToString() + " / 4";
				if (PlayerPrefsController.UnitsTechMounted[cardSelected] < 3)
				{
					num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.Mounted, cardSelected);
					textPrice.text = num.ToString("###,###,##0");
				}
				else
				{
					textPrice.text = string.Empty;
				}
				textSpecialA.text = unitsMountedStrong;
				textSpecialB.text = unitsMountedWeak;
				float num2 = (int)ConfigPrefsController.GetUnitsRomanHealth(CohortType.Mixed, CohortSubType.Mounted, cardSelected, PlayerPrefsController.UnitsTechMounted[cardSelected]);
				float num3 = (int)ConfigPrefsController.GetUnitsRomanAttack(CohortType.Mixed, CohortSubType.Mounted, cardSelected, PlayerPrefsController.UnitsTechMounted[cardSelected]);
				float num4 = (int)ConfigPrefsController.GetUnitsRomanDefence(CohortType.Mixed, CohortSubType.Mounted, cardSelected, PlayerPrefsController.UnitsTechMounted[cardSelected]);
				textStatsHP_Old.text = num2.ToString();
				textStatsATK_Old.text = num3.ToString();
				textStatsDEF_Old.text = num4.ToString();
				if (PlayerPrefsController.UnitsTechMounted[cardSelected] < 3)
				{
					float unitsRomanHealth = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Mixed, CohortSubType.Mounted, cardSelected, PlayerPrefsController.UnitsTechMounted[cardSelected] + 1);
					float unitsRomanAttack = ConfigPrefsController.GetUnitsRomanAttack(CohortType.Mixed, CohortSubType.Mounted, cardSelected, PlayerPrefsController.UnitsTechMounted[cardSelected] + 1);
					float unitsRomanDefence = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Mixed, CohortSubType.Mounted, cardSelected, PlayerPrefsController.UnitsTechMounted[cardSelected] + 1);
					if (PlayerPrefs.GetFloat("playerMoney") >= (float)num)
					{
						buttonUpgrade.interactable = true;
					}
					else
					{
						buttonUpgrade.interactable = false;
					}
					textStatsHP_New.text = unitsRomanHealth.ToString();
					textStatsATK_New.text = unitsRomanAttack.ToString();
					textStatsDEF_New.text = unitsRomanDefence.ToString();
					if (unitsRomanHealth > num2)
					{
						textStatsHP_New.color = colorStatsBetter;
					}
					else
					{
						textStatsHP_New.color = colorStatsNormal;
					}
					if (unitsRomanAttack > num3)
					{
						textStatsATK_New.color = colorStatsBetter;
					}
					else
					{
						textStatsATK_New.color = colorStatsNormal;
					}
					if (unitsRomanDefence > num4)
					{
						textStatsDEF_New.color = colorStatsBetter;
					}
					else
					{
						textStatsDEF_New.color = colorStatsNormal;
					}
				}
				else
				{
					buttonUpgrade.interactable = false;
					textStatsHP_New.text = string.Empty;
					textStatsATK_New.text = string.Empty;
					textStatsDEF_New.text = string.Empty;
				}
				UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechMounted);
				break;
			}
			case UnitType.Sword:
			{
				textTitle.text = unitsMeleeTitle[cardSelected] + "\n" + (PlayerPrefsController.UnitsTechMelee[cardSelected] + 1).ToString() + " / 4";
				if (PlayerPrefsController.UnitsTechMelee[cardSelected] < 3)
				{
					num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.None, cardSelected);
					textPrice.text = num.ToString("###,###,##0");
				}
				else
				{
					textPrice.text = string.Empty;
				}
				textSpecialA.text = unitsMeleeStrong;
				textSpecialB.text = unitsMeleeWeak;
				float num2 = (int)ConfigPrefsController.GetUnitsRomanHealth(CohortType.Mixed, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechMelee[cardSelected]);
				float num3 = (int)ConfigPrefsController.GetUnitsRomanAttack(CohortType.Mixed, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechMelee[cardSelected]);
				float num4 = (int)ConfigPrefsController.GetUnitsRomanDefence(CohortType.Mixed, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechMelee[cardSelected]);
				textStatsHP_Old.text = num2.ToString();
				textStatsATK_Old.text = num3.ToString();
				textStatsDEF_Old.text = num4.ToString();
				if (PlayerPrefsController.UnitsTechMelee[cardSelected] < 3)
				{
					float unitsRomanHealth = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Mixed, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechMelee[cardSelected] + 1);
					float unitsRomanAttack = ConfigPrefsController.GetUnitsRomanAttack(CohortType.Mixed, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechMelee[cardSelected] + 1);
					float unitsRomanDefence = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Mixed, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechMelee[cardSelected] + 1);
					if (PlayerPrefs.GetFloat("playerMoney") >= (float)num)
					{
						buttonUpgrade.interactable = true;
					}
					else
					{
						buttonUpgrade.interactable = false;
					}
					textStatsHP_New.text = unitsRomanHealth.ToString();
					textStatsATK_New.text = unitsRomanAttack.ToString();
					textStatsDEF_New.text = unitsRomanDefence.ToString();
					if (unitsRomanHealth > num2)
					{
						textStatsHP_New.color = colorStatsBetter;
					}
					else
					{
						textStatsHP_New.color = colorStatsNormal;
					}
					if (unitsRomanAttack > num3)
					{
						textStatsATK_New.color = colorStatsBetter;
					}
					else
					{
						textStatsATK_New.color = colorStatsNormal;
					}
					if (unitsRomanDefence > num4)
					{
						textStatsDEF_New.color = colorStatsBetter;
					}
					else
					{
						textStatsDEF_New.color = colorStatsNormal;
					}
				}
				else
				{
					buttonUpgrade.interactable = false;
					textStatsHP_New.text = string.Empty;
					textStatsATK_New.text = string.Empty;
					textStatsDEF_New.text = string.Empty;
				}
				UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechMelee);
				break;
			}
			case UnitType.Siege:
			{
				textTitle.text = unitsSiegeTitle[cardSelected] + "\n" + (PlayerPrefsController.UnitsTechSiege[cardSelected] + 1).ToString() + " / 4";
				if (PlayerPrefsController.UnitsTechSiege[cardSelected] < 3)
				{
					num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Siege, CohortSubType.None, cardSelected);
					textPrice.text = num.ToString("###,###,##0");
				}
				else
				{
					textPrice.text = string.Empty;
				}
				textSpecialA.text = unitsSiegeStrong;
				textSpecialB.text = unitsSiegeWeak;
				float num2 = (int)ConfigPrefsController.GetUnitsRomanHealth(CohortType.Siege, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechSiege[cardSelected]);
				float num3 = (int)ConfigPrefsController.GetUnitsRomanAttack(CohortType.Siege, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechSiege[cardSelected]);
				float num4 = (int)ConfigPrefsController.GetUnitsRomanDefence(CohortType.Siege, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechSiege[cardSelected]);
				textStatsHP_Old.text = num2.ToString();
				textStatsATK_Old.text = num3.ToString();
				textStatsDEF_Old.text = num4.ToString();
				if (PlayerPrefsController.UnitsTechSiege[cardSelected] < 3)
				{
					float unitsRomanHealth = ConfigPrefsController.GetUnitsRomanHealth(CohortType.Siege, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechSiege[cardSelected] + 1);
					float unitsRomanAttack = ConfigPrefsController.GetUnitsRomanAttack(CohortType.Siege, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechSiege[cardSelected] + 1);
					float unitsRomanDefence = ConfigPrefsController.GetUnitsRomanDefence(CohortType.Siege, CohortSubType.None, cardSelected, PlayerPrefsController.UnitsTechSiege[cardSelected] + 1);
					if (PlayerPrefs.GetFloat("playerMoney") >= (float)num)
					{
						buttonUpgrade.interactable = true;
					}
					else
					{
						buttonUpgrade.interactable = false;
					}
					textStatsHP_New.text = unitsRomanHealth.ToString();
					textStatsATK_New.text = unitsRomanAttack.ToString();
					textStatsDEF_New.text = unitsRomanDefence.ToString();
					if (unitsRomanHealth > num2)
					{
						textStatsHP_New.color = colorStatsBetter;
					}
					else
					{
						textStatsHP_New.color = colorStatsNormal;
					}
					if (unitsRomanAttack > num3)
					{
						textStatsATK_New.color = colorStatsBetter;
					}
					else
					{
						textStatsATK_New.color = colorStatsNormal;
					}
					if (unitsRomanDefence > num4)
					{
						textStatsDEF_New.color = colorStatsBetter;
					}
					else
					{
						textStatsDEF_New.color = colorStatsNormal;
					}
				}
				else
				{
					buttonUpgrade.interactable = false;
					textStatsHP_New.text = string.Empty;
					textStatsATK_New.text = string.Empty;
					textStatsDEF_New.text = string.Empty;
				}
				UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechSiege);
				break;
			}
			}
		}
		else
		{
			textTitle.text = string.Empty;
			buttonUpgrade.interactable = false;
			coinObject.SetActive(value: false);
			textStatsHP_Old.text = string.Empty;
			textStatsATK_Old.text = string.Empty;
			textStatsDEF_Old.text = string.Empty;
		}
	}

	public void ButtonClose()
	{
		if (PlayerPrefsController.tutorialSteps[8])
		{
			sfxUiController.PlaySound(SfxUI.ClickDefault);
			uiController.BackFromUpgradeWindow();
			Touch_Battle.IsWindowBigOpen = false;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void ButtonUpgrade()
	{
		sfxUiController.PlaySound(SfxUI.ClickBuy);
		string empty = string.Empty;
		int num = 0;
		switch (unitTypeSelected)
		{
		case UnitType.Spear:
			num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.Spear, cardSelected);
			UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechSpear);
			break;
		case UnitType.Archer:
			num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Ranged, CohortSubType.Javaline, cardSelected);
			UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechRanged);
			break;
		case UnitType.Mounted:
			num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.Mounted, cardSelected);
			UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechMounted);
			break;
		case UnitType.Sword:
			num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Mixed, CohortSubType.None, cardSelected);
			UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechMelee);
			break;
		case UnitType.Siege:
			num = ConfigPrefsController.GetUpgradeUnitPrice(CohortType.Siege, CohortSubType.None, cardSelected);
			UpdateUnitsBars(cardSelected, PlayerPrefsController.UnitsTechSiege);
			break;
		}
		if (PlayerPrefs.GetFloat("playerMoney") >= (float)num)
		{
			PlayerPrefsController.SaveJustMoney(PlayerPrefs.GetFloat("playerMoney") - (float)num);
			switch (unitTypeSelected)
			{
			case UnitType.Spear:
				PlayerPrefsController.UnitsTechSpear[cardSelected]++;
				empty = FormatString(PlayerPrefsController.UnitsTechSpear[cardSelected], cardSelected);
				stationEngine.SendAnalyticSelectContent("Militia_Farmer", empty);
				stationEngine.SendAnalyticSpendVirtualCurrency("Militia_Farmer " + empty, "[GOLD]", num.ToString());
				break;
			case UnitType.Archer:
				PlayerPrefsController.UnitsTechRanged[cardSelected]++;
				empty = FormatString(PlayerPrefsController.UnitsTechRanged[cardSelected], cardSelected);
				stationEngine.SendAnalyticSelectContent("Javelin_Militia", empty);
				stationEngine.SendAnalyticSpendVirtualCurrency("Javelin_Militia " + empty, "[GOLD]", num.ToString());
				break;
			case UnitType.Mounted:
				PlayerPrefsController.UnitsTechMounted[cardSelected]++;
				empty = FormatString(PlayerPrefsController.UnitsTechMounted[cardSelected], cardSelected);
				stationEngine.SendAnalyticSelectContent("Scout", empty);
				stationEngine.SendAnalyticSpendVirtualCurrency("Scout " + empty, "[GOLD]", num.ToString());
				break;
			case UnitType.Sword:
				PlayerPrefsController.UnitsTechMelee[cardSelected]++;
				empty = FormatString(PlayerPrefsController.UnitsTechMelee[cardSelected], cardSelected);
				stationEngine.SendAnalyticSelectContent("Militia", empty);
				stationEngine.SendAnalyticSpendVirtualCurrency("Militia " + empty, "[GOLD]", num.ToString());
				break;
			case UnitType.Siege:
				PlayerPrefsController.UnitsTechSiege[cardSelected]++;
				empty = FormatString(PlayerPrefsController.UnitsTechSiege[cardSelected], cardSelected);
				stationEngine.SendAnalyticSelectContent("Battering_Ram", empty);
				stationEngine.SendAnalyticSpendVirtualCurrency("Battering_Ram " + empty, "[GOLD]", num.ToString());
				break;
			}
			PlayerPrefsController.SaveBoughtUnitTech(PlayerPrefs.GetFloat("playerMoney"), PlayerPrefs.GetInt("playerExpPoints"), PlayerPrefs.GetInt("playerWave"));
			UpdateWindow();
			UpdateDescription();
			if (tutorialController.GetActualIndex() == 8 && PlayerPrefsController.UnitsTechMelee[0] >= 1)
			{
				tutorialController.ActivateStep(9);
			}
			achievementsController.CheckAchievementsWave();
		}
	}

	public void ButtonInfo()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		Object.Instantiate(Resources.Load("Canvas/CanvasInfoBonusWeapon"));
	}

	private void UpdateUnitsBars(int lvlSelected, int[] arrayUnitTech)
	{
		int num = arrayUnitTech.Length * 3;
		int num2 = 0;
		for (int i = 0; i < arrayUnitTech.Length; i++)
		{
			if (i <= lvlSelected)
			{
				num2 += arrayUnitTech[i];
			}
			if (arrayUnitTech[i] < 3)
			{
				break;
			}
		}
		if (arrayUnitTech[lvlSelected] == 3 && lvlSelected < arrayUnitTech.Length - 1)
		{
			num2--;
		}
		uiController.UpdateLevelBars(attackStat, num2, num);
		uiController.UpdateLevelBars(defenceStat, num2, num);
		uiController.UpdateLevelBars(healthStat, num2, num);
	}

	public string FormatString(int value, int index)
	{
		return string.Format((index >= 10) ? "[0{1}] - [{0}]" : "[00{1}] - [{0}] ", value, index);
	}
}
