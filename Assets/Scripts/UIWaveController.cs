using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveController : MonoBehaviour
{
	public RectTransform[] transformSpawn;

	public Image[] objectsSpawn;

	public Image enemyWaveImage;

	public Image wallWaveImage;

	public Sprite[] enemyWaveIcons;

	public Sprite playerWaveIcon;

	public GameObject[] objectsBanner;

	public RectTransform[] transformBanner;

	public Text textBar;

	public Image imageParent;

	public RectTransform transformParent;

	private GameObject[] iconsArray;

	private int miniWaveIndex;

	private Faction barFaction;

	private BarType barType;

	private UnitType[] unitTypeA;

	private UnitType[] unitTypeB;

	private UnitType[] unitTypeC;

	private bool isSecondLegionPresent;

	private bool isThirdLegionPresent;

	private float[] timeRomanInvasion;

	private WaveController waveController;

	private void Awake()
	{
		waveController = base.gameObject.GetComponent<WaveController>();
		timeRomanInvasion = new float[ConfigPrefsController.waveRomanEachTime.Length];
		timeRomanInvasion[0] = ConfigPrefsController.waveRomanEachTime[2];
		timeRomanInvasion[1] = ConfigPrefsController.waveRomanEachTime[1];
		timeRomanInvasion[2] = ConfigPrefsController.waveRomanEachTime[0];
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (miniWaveIndex >= 3)
		{
			return;
		}
		if (barType == BarType.Army)
		{
			if (waveController.WaveTimeActual < waveController.TimeMiniWaves[miniWaveIndex])
			{
				ShowMiniWave(_changeMiniWave: true);
			}
		}
		else if (barType == BarType.Invasion && MainController.worldScreen == WorldScreen.AttackStarted && waveController.WaveTimeActual < timeRomanInvasion[miniWaveIndex])
		{
			ShowMiniWave(_changeMiniWave: true);
		}
	}

	private void ResetBar()
	{
		miniWaveIndex = 0;
		RectTransform rectTransform = transformParent;
		Vector2 anchoredPosition = transformParent.anchoredPosition;
		rectTransform.anchoredPosition = new Vector2(-12f, anchoredPosition.y);
		imageParent.enabled = false;
		for (int i = 0; i < objectsSpawn.Length; i++)
		{
			objectsSpawn[i].enabled = false;
		}
		for (int j = 0; j < objectsBanner.Length; j++)
		{
			objectsBanner[j].SetActive(value: false);
		}
		if (iconsArray != null)
		{
			for (int k = 0; k < iconsArray.Length; k++)
			{
				UnityEngine.Object.Destroy(iconsArray[k]);
			}
			iconsArray = null;
		}
	}

	public void SetPhaseInitial()
	{
		ResetBar();
		textBar.text = ScriptLocalization.Get("NORMAL/wave") + " " + PlayerPrefs.GetInt("playerWave").ToString("###,###,##0");
	}

	public void SetPhaseAction(BarType _barType, Faction _faction, UnitType[] _unitTypeA, UnitType[] _unitTypeB, UnitType[] _unitTypeC)
	{
		ResetBar();
		barType = _barType;
		barFaction = _faction;
		unitTypeA = _unitTypeA;
		unitTypeB = _unitTypeB;
		unitTypeC = _unitTypeC;
		switch (_barType)
		{
		case BarType.Invasion:
			textBar.text = ScriptLocalization.Get("NORMAL/reinforcements");
			break;
		case BarType.Horde:
			textBar.text = ScriptLocalization.Get("NORMAL/wave") + " " + PlayerPrefs.GetInt("playerWave").ToString("###,###,##0");
			break;
		}
		if (_barType == BarType.Army || _barType == BarType.Elephants)
		{
			imageParent.enabled = true;
		}
		switch (_barType)
		{
		case BarType.Army:
			for (int j = 0; j < objectsSpawn.Length; j++)
			{
				objectsSpawn[j].enabled = true;
			}
			break;
		case BarType.Invasion:
		{
			GameObject gameObject = GameObject.Find("WaveRoman");
			WaveRoman component = gameObject.GetComponent<WaveRoman>();
			for (int i = 0; i < component.UnitTypeSelected.Length; i++)
			{
				if (i >= 6 && i <= 11 && component.UnitTypeSelected[i] != 0)
				{
					isSecondLegionPresent = true;
				}
				else if (i >= 12 && component.UnitTypeSelected[i] != 0)
				{
					isThirdLegionPresent = true;
				}
			}
			objectsBanner[0].SetActive(value: true);
			if (isSecondLegionPresent)
			{
				objectsBanner[1].SetActive(value: true);
			}
			if (isThirdLegionPresent)
			{
				objectsBanner[2].SetActive(value: true);
			}
			break;
		}
		}
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			ShowEnemyIconInvade();
		}
		else if (MainController.worldScreen == WorldScreen.Upgrade || MainController.worldScreen == WorldScreen.Defence)
		{
			ShowEnemyIcon(barFaction);
		}
		ShowMiniWave(_changeMiniWave: false);
	}

	public void ShowEnemyIcon(Faction enemyFactionToShow)
	{
		switch (enemyFactionToShow)
		{
		case Faction.Goblins:
			enemyWaveImage.sprite = enemyWaveIcons[0];
			break;
		case Faction.Skeletons:
			enemyWaveImage.sprite = enemyWaveIcons[1];
			break;
		case Faction.Wolves:
			enemyWaveImage.sprite = enemyWaveIcons[2];
			break;
		case Faction.Orcs:
			enemyWaveImage.sprite = enemyWaveIcons[3];
			break;
		}
	}

	public void ShowEnemyIconInvade()
	{
		enemyWaveImage.sprite = playerWaveIcon;
		switch (EnemyPrefsController.TerrainSelected)
		{
		case CityTerrain.Goblins:
			wallWaveImage.sprite = enemyWaveIcons[0];
			break;
		case CityTerrain.Skeletons:
			wallWaveImage.sprite = enemyWaveIcons[1];
			break;
		case CityTerrain.Wolves:
			wallWaveImage.sprite = enemyWaveIcons[2];
			break;
		case CityTerrain.Orcs:
			wallWaveImage.sprite = enemyWaveIcons[3];
			break;
		}
	}

	public void ShowMiniWave(bool _changeMiniWave)
	{
		if (_changeMiniWave && (barType == BarType.Army || barType == BarType.Invasion))
		{
			miniWaveIndex++;
		}
		if (!_changeMiniWave && barType == BarType.Army)
		{
			RectTransform rectTransform = transformParent;
			Vector2 anchoredPosition = transformParent.anchoredPosition;
			rectTransform.anchoredPosition = new Vector2(40f, anchoredPosition.y);
		}
		if (iconsArray != null)
		{
			for (int i = 0; i < iconsArray.Length; i++)
			{
				UnityEngine.Object.Destroy(iconsArray[i]);
			}
			iconsArray = null;
		}
		if (barType == BarType.Army || barType == BarType.Horde)
		{
			UnitType[] array = null;
			if (miniWaveIndex < 3)
			{
				switch (miniWaveIndex)
				{
				case 0:
					iconsArray = new GameObject[unitTypeA.Length];
					array = unitTypeA;
					break;
				case 1:
					iconsArray = new GameObject[unitTypeB.Length];
					array = unitTypeB;
					break;
				case 2:
					iconsArray = new GameObject[unitTypeC.Length];
					array = unitTypeC;
					break;
				}
				if (barType == BarType.Army || barType == BarType.Invasion)
				{
					for (int j = 0; j < objectsSpawn.Length; j++)
					{
						if (j != miniWaveIndex)
						{
							if (j < miniWaveIndex)
							{
								objectsSpawn[j].enabled = false;
							}
							else if (j <= miniWaveIndex)
							{
							}
						}
					}
				}
				if (barType == BarType.Army || barType == BarType.Elephants || barType == BarType.Horde)
				{
					imageParent.enabled = true;
					for (int k = 0; k < array.Length; k++)
					{
						string text = "Canvas/WaveIcons/WaveIcon_";
						switch (barFaction)
						{
						case Faction.Goblins:
							text += "Italian_";
							break;
						case Faction.Wolves:
							text += "Iberian_";
							break;
						case Faction.Skeletons:
							text += "Gaul_";
							break;
						case Faction.Orcs:
							text += "Carthaginian_";
							break;
						}
						switch (array[k])
						{
						case UnitType.Sword:
							text += "Melee";
							break;
						case UnitType.Spear:
							text += "Spear";
							break;
						case UnitType.Mounted:
							text += "Mounted";
							break;
						case UnitType.Archer:
							text += "Ranged";
							break;
						case UnitType.Siege:
							text += "Siege";
							break;
						}
						iconsArray[k] = (UnityEngine.Object.Instantiate(Resources.Load(text)) as GameObject);
						RectTransform component = iconsArray[k].GetComponent<RectTransform>();
						component.SetParent(transformParent);
						if (array.Length < 3)
						{
							component.anchoredPosition = new Vector2(22f + (float)k * -44f, -6f);
							transformParent.sizeDelta = new Vector2(96f, 60f);
						}
						else if (array.Length >= 3)
						{
							component.anchoredPosition = new Vector2(50f + (float)k * -48f, -6f);
							transformParent.sizeDelta = new Vector2(150f, 60f);
						}
						component.localScale = new Vector3(1f, 1f, 1f);
					}
				}
				if (!_changeMiniWave || miniWaveIndex >= 3)
				{
					return;
				}
				switch (miniWaveIndex)
				{
				case 0:
				{
					RectTransform rectTrans4 = transformParent;
					Vector2 anchoredPosition5 = transformParent.anchoredPosition;
					LeanTween.move(rectTrans4, new Vector2(40f, anchoredPosition5.y), 1f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true);
					break;
				}
				case 1:
				{
					RectTransform rectTrans3 = transformParent;
					Vector2 anchoredPosition4 = transformParent.anchoredPosition;
					LeanTween.move(rectTrans3, new Vector2(-12f, anchoredPosition4.y), 1f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true);
					break;
				}
				case 2:
					if (array.Length >= 3)
					{
						RectTransform rectTrans = transformParent;
						Vector2 anchoredPosition2 = transformParent.anchoredPosition;
						LeanTween.move(rectTrans, new Vector2(-91f, anchoredPosition2.y), 1f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true);
					}
					else
					{
						RectTransform rectTrans2 = transformParent;
						Vector2 anchoredPosition3 = transformParent.anchoredPosition;
						LeanTween.move(rectTrans2, new Vector2(-64.5f, anchoredPosition3.y), 1f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true);
					}
					break;
				}
			}
			else
			{
				imageParent.enabled = false;
				for (int l = 0; l < objectsSpawn.Length; l++)
				{
					objectsSpawn[l].enabled = false;
				}
			}
		}
		else if (barType == BarType.Elephants)
		{
			iconsArray = new GameObject[1];
			string path = "Canvas/WaveIcons/WaveIcon_Carthaginian_Elephant";
			iconsArray[0] = (UnityEngine.Object.Instantiate(Resources.Load(path)) as GameObject);
			RectTransform component2 = iconsArray[0].GetComponent<RectTransform>();
			component2.SetParent(transformParent);
			component2.anchoredPosition = new Vector2(0f, -6f);
			component2.localScale = new Vector3(1f, 1f, 1f);
			transformParent.sizeDelta = new Vector2(60f, 60f);
		}
		else
		{
			if (barType != BarType.Invasion)
			{
				return;
			}
			if (miniWaveIndex < 4)
			{
				for (int m = 0; m < objectsBanner.Length; m++)
				{
					if (m < miniWaveIndex)
					{
						LeanTween.scale(transformBanner[m], transformBanner[m].localScale * 1.5f, 0.5f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true)
							.setLoopPingPong(1);
						LeanTween.move(transformBanner[m], transformBanner[m].anchoredPosition + new Vector2(0f, 150f), 1f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true)
							.setDelay(1f);
					}
				}
			}
			else
			{
				for (int n = 0; n < objectsBanner.Length; n++)
				{
					objectsBanner[n].SetActive(value: false);
				}
			}
		}
	}
}
