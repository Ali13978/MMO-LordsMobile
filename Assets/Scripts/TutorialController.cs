using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
	private string pathTutorials = "Canvas/Tutorial/CanvasTutorial_";

	public GameObject butttonGeneral;

	public GameObject butttonTech;

	public GameObject butttonMap;

	public GameObject butttonWave;

	public GameObject menuUpgrades;

	public GameObject buttonUpgradesWall;

	public GameObject buttonUpgradesArcher;

	public GameObject buttonUpgradesTowers;

	public GameObject buttonUpgradesCatapults;

	public Button buttonButtonUpgradesWall;

	public Image buttonImageUpgradesWall;

	public Button buttonButtonUpgradesArcher;

	public Image buttonImageUpgradesArcher;

	public Button buttonButtonUpgradesTower;

	public Image buttonImageUpgradesTower;

	public Button buttonButtonUpgradesWave;

	public Image buttonImageUpgradesWave;

	public Button buttonButtonGeneral;

	public Image buttonImageGeneral;

	public Button buttonButtonTech;

	public Image buttonImageTech;

	public GameObject buttonAmmoTower;

	public GameObject buttonBackMap;

	public GameObject buttonCancelAttackWindow;

	public Button buttonDefenceMelee;

	[Header("GLOW")]
	public Image glowUpgrade_nextWave;

	public Image glowUpgrade_walls;

	public Image glowUpgrade_archers;

	public Image glowUpgrade_units;

	public Image glowUpgrade_map;

	public Image glowUpgrade_general;

	public Image glowSpeed;

	public Image glowSpawnMelee;

	public Image glowTower;

	private Image glowBank;

	public GameObject bankGlow;

	private GameObject objectTutorial;

	private UIController uiController;

	private Touch_Battle touchBattle;

	private UIUnitsTechnology uiUnitsScript;

	private UIGeneralTechnology uiGeneralScript;

	private void Awake()
	{
		if (SceneManager.GetActiveScene().name == "MainScene")
		{
			uiController = base.gameObject.GetComponent<UIController>();
			touchBattle = base.gameObject.GetComponent<Touch_Battle>();
		}
	}

	private void Start()
	{
		if ((MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade) && !PlayerPrefsController.forceTutorialDone && SceneManager.GetActiveScene().name == "MainScene" && !PlayerPrefsController.tutorialSteps[0])
		{
			if (PlayerPrefs.HasKey("selectedLanguage"))
			{
				ActivateStep(0);
			}
			else
			{
				Object.Instantiate(Resources.Load("Windows/CanvasLanguage"));
			}
		}
		if (!PlayerPrefs.HasKey("selectedLanguage") && PlayerPrefsController.tutorialSteps[26])
		{
			Object.Instantiate(Resources.Load("Windows/CanvasLanguage"));
		}
	}

	public void ActivateStep(int _stepIndex)
	{
		if (PlayerPrefsController.forceTutorialDone)
		{
			return;
		}
		switch (_stepIndex)
		{
		case 0:
			Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()));
			butttonGeneral.SetActive(value: false);
			butttonMap.SetActive(value: false);
			butttonTech.SetActive(value: false);
			butttonWave.SetActive(value: false);
			menuUpgrades.SetActive(value: false);
			break;
		case 1:
			butttonWave.SetActive(value: true);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			ClearGlow();
			SetGlow(0);
			break;
		case 2:
			PlayerPrefsController.tutorialSteps[0] = true;
			PlayerPrefsController.tutorialSteps[1] = true;
			UnityEngine.Object.Destroy(objectTutorial);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			Time.timeScale = 0f;
			ClearGlow();
			SetGlow(11);
			break;
		case 3:
			PlayerPrefsController.tutorialSteps[2] = true;
			Time.timeScale = 1f;
			if (objectTutorial != null)
			{
				UnityEngine.Object.Destroy(objectTutorial);
				objectTutorial = null;
			}
			ClearGlow();
			break;
		case 4:
			PlayerPrefsController.tutorialSteps[3] = true;
			butttonGeneral.SetActive(value: false);
			butttonMap.SetActive(value: false);
			butttonTech.SetActive(value: false);
			butttonWave.SetActive(value: false);
			menuUpgrades.SetActive(value: false);
			Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()));
			break;
		case 5:
			PlayerPrefsController.tutorialSteps[4] = true;
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			butttonGeneral.SetActive(value: false);
			butttonMap.SetActive(value: false);
			butttonTech.SetActive(value: false);
			butttonWave.SetActive(value: false);
			menuUpgrades.SetActive(value: true);
			buttonUpgradesTowers.SetActive(value: false);
			buttonUpgradesCatapults.SetActive(value: false);
			ClearGlow();
			SetGlow(1);
			SetGlow(2);
			break;
		case 6:
			PlayerPrefsController.tutorialSteps[5] = true;
			UnityEngine.Object.Destroy(objectTutorial);
			butttonGeneral.SetActive(value: false);
			butttonMap.SetActive(value: false);
			butttonTech.SetActive(value: false);
			butttonWave.SetActive(value: true);
			menuUpgrades.SetActive(value: true);
			buttonUpgradesTowers.SetActive(value: false);
			buttonUpgradesCatapults.SetActive(value: false);
			ClearGlow();
			SetGlow(0);
			break;
		case 7:
		{
			PlayerPrefsController.tutorialSteps[6] = true;
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			LeanTween.cancel(butttonWave);
			RectTransform component = butttonWave.GetComponent<RectTransform>();
			component.localScale = new Vector3(1f, 1f, 1f);
			butttonGeneral.SetActive(value: false);
			butttonMap.SetActive(value: false);
			butttonTech.SetActive(value: true);
			butttonWave.SetActive(value: false);
			menuUpgrades.SetActive(value: false);
			buttonUpgradesTowers.SetActive(value: false);
			buttonUpgradesCatapults.SetActive(value: false);
			ClearGlow();
			SetGlow(3);
			break;
		}
		case 8:
			PlayerPrefsController.tutorialSteps[7] = true;
			UnityEngine.Object.Destroy(objectTutorial);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			PlayerPrefsController.SaveJustMoney(ConfigPrefsController.unitStats_Roman_Melee_Price_Base[0]);
			uiUnitsScript = GameObject.FindGameObjectWithTag("Canvas_Units").GetComponent<UIUnitsTechnology>();
			uiUnitsScript.UpdateDescription();
			uiUnitsScript.tutorialButtonClose.SetActive(value: false);
			ClearGlow();
			SetGlow(6);
			break;
		case 9:
			PlayerPrefsController.tutorialSteps[8] = true;
			PlayerPrefsController.tutorialSteps[9] = true;
			UnityEngine.Object.Destroy(objectTutorial);
			uiUnitsScript = GameObject.FindGameObjectWithTag("Canvas_Units").GetComponent<UIUnitsTechnology>();
			uiUnitsScript.tutorialButtonClose.SetActive(value: true);
			ClearGlow();
			SetGlow(7);
			break;
		case 10:
		{
			menuUpgrades.SetActive(value: false);
			butttonTech.SetActive(value: false);
			butttonWave.SetActive(value: false);
			uiUnitsScript = GameObject.FindGameObjectWithTag("Canvas_Units").GetComponent<UIUnitsTechnology>();
			LeanTween.cancel(uiUnitsScript.tutorialButtonClose);
			RectTransform component = uiUnitsScript.tutorialButtonClose.GetComponent<RectTransform>();
			component.localScale = new Vector3(1f, 1f, 1f);
			Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()));
			ClearGlow();
			break;
		}
		case 11:
			PlayerPrefsController.tutorialSteps[10] = true;
			butttonGeneral.SetActive(value: false);
			butttonMap.SetActive(value: false);
			butttonTech.SetActive(value: true);
			butttonWave.SetActive(value: true);
			menuUpgrades.SetActive(value: true);
			buttonUpgradesTowers.SetActive(value: false);
			buttonUpgradesCatapults.SetActive(value: false);
			uiController.UpdateUIUpgrade();
			ClearGlow();
			SetGlow(0);
			break;
		case 12:
		{
			PlayerPrefsController.tutorialSteps[11] = true;
			LeanTween.cancel(butttonWave);
			RectTransform component = butttonWave.GetComponent<RectTransform>();
			component.localScale = new Vector3(1f, 1f, 1f);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			butttonGeneral.SetActive(value: true);
			butttonMap.SetActive(value: false);
			butttonTech.SetActive(value: false);
			butttonWave.SetActive(value: false);
			menuUpgrades.SetActive(value: false);
			ClearGlow();
			SetGlow(5);
			break;
		}
		case 13:
			PlayerPrefsController.tutorialSteps[12] = true;
			uiGeneralScript = GameObject.FindGameObjectWithTag("Canvas_General").GetComponent<UIGeneralTechnology>();
			for (int k = 0; k < uiGeneralScript.tutorialButtonsArray.Length; k++)
			{
				uiGeneralScript.tutorialButtonsArray[k].interactable = false;
			}
			for (int l = 0; l < uiGeneralScript.tutorialImagesArray.Length; l++)
			{
				uiGeneralScript.tutorialImagesArray[l].color = new Color(0.4f, 0.4f, 0.4f);
			}
			UnityEngine.Object.Destroy(objectTutorial);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			uiGeneralScript.tutorialButtonClose.SetActive(value: false);
			ClearGlow();
			SetGlow(8);
			break;
		case 14:
			PlayerPrefsController.tutorialSteps[13] = true;
			uiGeneralScript = GameObject.FindGameObjectWithTag("Canvas_General").GetComponent<UIGeneralTechnology>();
			for (int i = 0; i < uiGeneralScript.tutorialButtonsArray.Length; i++)
			{
				uiGeneralScript.tutorialButtonsArray[i].interactable = true;
			}
			for (int j = 0; j < uiGeneralScript.tutorialImagesArray.Length; j++)
			{
				uiGeneralScript.tutorialImagesArray[j].color = new Color(1f, 1f, 1f);
			}
			UnityEngine.Object.Destroy(objectTutorial);
			uiGeneralScript.tutorialButtonClose.SetActive(value: true);
			ClearGlow();
			SetGlow(9);
			break;
		case 15:
			PlayerPrefsController.tutorialSteps[14] = true;
			buttonButtonUpgradesArcher.interactable = false;
			buttonImageUpgradesArcher.color = new Color(0.4f, 0.4f, 0.4f);
			buttonButtonGeneral.interactable = false;
			buttonImageGeneral.color = new Color(0.4f, 0.4f, 0.4f);
			uiGeneralScript = GameObject.FindGameObjectWithTag("Canvas_General").GetComponent<UIGeneralTechnology>();
			LeanTween.cancel(uiGeneralScript.tutorialButtonClose);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			PlayerPrefsController.SaveJustMoney(PlayerPrefsController.upgradeWallPrices[2] + PlayerPrefsController.upgradeWallPrices[3] + PlayerPrefsController.upgradeTowerAmmoSmallPrices[1] + 1);
			uiController.UpdateUIUpgrade();
			ClearGlow();
			SetGlow(1);
			break;
		case 16:
			PlayerPrefsController.tutorialSteps[15] = true;
			buttonButtonUpgradesArcher.interactable = true;
			buttonImageUpgradesArcher.color = new Color(1f, 1f, 1f);
			buttonButtonGeneral.interactable = true;
			buttonImageGeneral.color = new Color(1f, 1f, 1f);
			UnityEngine.Object.Destroy(objectTutorial);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			butttonGeneral.SetActive(value: false);
			butttonMap.SetActive(value: false);
			butttonTech.SetActive(value: false);
			butttonWave.SetActive(value: false);
			menuUpgrades.SetActive(value: false);
			ClearGlow();
			SetGlow(12);
			break;
		case 17:
			PlayerPrefsController.tutorialSteps[16] = true;
			UnityEngine.Object.Destroy(objectTutorial);
			LeanTween.cancel(buttonAmmoTower);
			ClearGlow();
			break;
		case 18:
			PlayerPrefsController.tutorialSteps[17] = true;
			uiController.UpdateUIUpgrade();
			butttonGeneral.SetActive(value: true);
			butttonMap.SetActive(value: false);
			butttonTech.SetActive(value: true);
			butttonWave.SetActive(value: true);
			menuUpgrades.SetActive(value: true);
			ClearGlow();
			SetGlow(0);
			break;
		case 19:
		{
			PlayerPrefsController.tutorialSteps[18] = true;
			butttonGeneral.SetActive(value: true);
			butttonMap.SetActive(value: true);
			butttonTech.SetActive(value: true);
			butttonWave.SetActive(value: true);
			menuUpgrades.SetActive(value: true);
			buttonButtonUpgradesWall.interactable = false;
			buttonImageUpgradesWall.color = new Color(0.4f, 0.4f, 0.4f);
			buttonButtonUpgradesArcher.interactable = false;
			buttonImageUpgradesArcher.color = new Color(0.4f, 0.4f, 0.4f);
			buttonButtonUpgradesTower.interactable = false;
			buttonImageUpgradesTower.color = new Color(0.4f, 0.4f, 0.4f);
			buttonButtonGeneral.interactable = false;
			buttonImageGeneral.color = new Color(0.4f, 0.4f, 0.4f);
			buttonButtonTech.interactable = false;
			buttonImageTech.color = new Color(0.4f, 0.4f, 0.4f);
			buttonButtonUpgradesWave.interactable = false;
			buttonImageUpgradesWave.color = new Color(0.4f, 0.4f, 0.4f);
			LeanTween.cancel(butttonWave);
			RectTransform component = butttonWave.GetComponent<RectTransform>();
			component.localScale = new Vector3(1f, 1f, 1f);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			ClearGlow();
			SetGlow(4);
			break;
		}
		case 20:
			PlayerPrefsController.tutorialSteps[19] = true;
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			buttonBackMap.SetActive(value: false);
			break;
		case 21:
			PlayerPrefsController.tutorialSteps[20] = true;
			buttonCancelAttackWindow.SetActive(value: false);
			UnityEngine.Object.Destroy(objectTutorial);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			break;
		case 22:
			PlayerPrefsController.tutorialSteps[21] = true;
			UnityEngine.Object.Destroy(objectTutorial);
			break;
		case 23:
			PlayerPrefsController.tutorialSteps[22] = true;
			PlayerPrefsController.SaveTutorial();
			break;
		case 24:
			PlayerPrefsController.tutorialSteps[23] = true;
			buttonBackMap.SetActive(value: false);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			break;
		case 25:
			PlayerPrefsController.tutorialSteps[24] = true;
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			break;
		case 26:
			PlayerPrefsController.tutorialSteps[25] = true;
			UnityEngine.Object.Destroy(objectTutorial);
			break;
		case 27:
			PlayerPrefsController.tutorialSteps[26] = true;
			PlayerPrefsController.SaveTutorial();
			buttonBackMap.SetActive(value: true);
			objectTutorial = UnityEngine.Object.Instantiate(Resources.Load(pathTutorials + _stepIndex.ToString()) as GameObject);
			break;
		}
	}

	public int GetActualIndex()
	{
		int num = 0;
		if (!PlayerPrefsController.forceTutorialDone)
		{
			for (int i = 0; i < PlayerPrefsController.tutorialSteps.Length && PlayerPrefsController.tutorialSteps[i]; i++)
			{
				num++;
			}
		}
		else
		{
			num = -1;
		}
		return num;
	}

	public void SetGlow(int _glowIndex)
	{
		float time = 0.4f;
		LeanTweenType ease = LeanTweenType.easeInOutSine;
		switch (_glowIndex)
		{
		case 0:
			LeanTween.cancel(glowUpgrade_nextWave.gameObject);
			glowUpgrade_nextWave.color = Color.white;
			glowUpgrade_nextWave.enabled = true;
			LeanTween.color(glowUpgrade_nextWave.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 1:
			LeanTween.cancel(glowUpgrade_walls.gameObject);
			glowUpgrade_walls.color = Color.white;
			glowUpgrade_walls.enabled = true;
			LeanTween.color(glowUpgrade_walls.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 2:
			LeanTween.cancel(glowUpgrade_archers.gameObject);
			glowUpgrade_archers.color = Color.white;
			glowUpgrade_archers.enabled = true;
			LeanTween.color(glowUpgrade_archers.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 3:
			LeanTween.cancel(glowUpgrade_units.gameObject);
			glowUpgrade_units.color = Color.white;
			glowUpgrade_units.enabled = true;
			LeanTween.color(glowUpgrade_units.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 4:
			LeanTween.cancel(glowUpgrade_map.gameObject);
			glowUpgrade_map.color = Color.white;
			glowUpgrade_map.enabled = true;
			LeanTween.color(glowUpgrade_map.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 5:
			LeanTween.cancel(glowUpgrade_general.gameObject);
			glowUpgrade_general.color = Color.white;
			glowUpgrade_general.enabled = true;
			LeanTween.color(glowUpgrade_general.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 6:
			uiUnitsScript = GameObject.FindGameObjectWithTag("Canvas_Units").GetComponent<UIUnitsTechnology>();
			LeanTween.cancel(uiUnitsScript.tutorialGlowUpgrade.gameObject);
			uiUnitsScript.tutorialGlowUpgrade.color = Color.white;
			uiUnitsScript.tutorialGlowUpgrade.enabled = true;
			LeanTween.color(uiUnitsScript.tutorialGlowUpgrade.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 7:
			uiUnitsScript = GameObject.FindGameObjectWithTag("Canvas_Units").GetComponent<UIUnitsTechnology>();
			LeanTween.cancel(uiUnitsScript.tutorialGlowClose.gameObject);
			uiUnitsScript.tutorialGlowClose.color = Color.white;
			uiUnitsScript.tutorialGlowClose.enabled = true;
			LeanTween.color(uiUnitsScript.tutorialGlowClose.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 8:
			uiGeneralScript = GameObject.FindGameObjectWithTag("Canvas_General").GetComponent<UIGeneralTechnology>();
			LeanTween.cancel(uiGeneralScript.tutorialGlowUpgrade.gameObject);
			uiGeneralScript.tutorialGlowUpgrade.color = Color.white;
			uiGeneralScript.tutorialGlowUpgrade.enabled = true;
			LeanTween.color(uiGeneralScript.tutorialGlowUpgrade.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 9:
			uiGeneralScript = GameObject.FindGameObjectWithTag("Canvas_General").GetComponent<UIGeneralTechnology>();
			LeanTween.cancel(uiGeneralScript.tutorialGlowClose.gameObject);
			uiGeneralScript.tutorialGlowClose.color = Color.white;
			uiGeneralScript.tutorialGlowClose.enabled = true;
			LeanTween.color(uiGeneralScript.tutorialGlowClose.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 10:
			LeanTween.cancel(glowSpeed.gameObject);
			glowSpeed.color = Color.white;
			glowSpeed.enabled = true;
			LeanTween.color(glowSpeed.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 11:
			LeanTween.cancel(glowSpawnMelee.gameObject);
			glowSpawnMelee.color = Color.white;
			glowSpawnMelee.enabled = true;
			LeanTween.color(glowSpawnMelee.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 12:
			LeanTween.cancel(glowTower.gameObject);
			glowTower.color = Color.white;
			glowTower.enabled = true;
			LeanTween.color(glowTower.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		case 13:
			bankGlow.SetActive(value: true);
			break;
		case 14:
			glowBank = GameObject.FindGameObjectWithTag("Canvas_Bank").GetComponent<UIBankMenu>().glowUpgrade;
			LeanTween.cancel(glowBank.gameObject);
			glowBank.color = Color.white;
			glowBank.enabled = true;
			LeanTween.color(glowBank.GetComponent<RectTransform>(), new Color(1f, 1f, 1f, 0.25f), time).setEase(ease).setLoopPingPong()
				.setIgnoreTimeScale(useUnScaledTime: true);
			break;
		}
	}

	public void ClearGlow()
	{
		if (glowUpgrade_nextWave != null)
		{
			glowUpgrade_nextWave.enabled = false;
		}
		if (glowUpgrade_walls != null)
		{
			glowUpgrade_walls.enabled = false;
		}
		if (glowUpgrade_archers != null)
		{
			glowUpgrade_archers.enabled = false;
		}
		if (glowUpgrade_units != null)
		{
			glowUpgrade_units.enabled = false;
		}
		if (glowUpgrade_map != null)
		{
			glowUpgrade_map.enabled = false;
		}
		if (glowUpgrade_general != null)
		{
			glowUpgrade_general.enabled = false;
		}
		if (glowSpeed != null)
		{
			glowSpeed.enabled = false;
		}
		if (glowSpawnMelee != null)
		{
			glowSpawnMelee.enabled = false;
		}
		if (glowTower != null)
		{
			glowTower.enabled = false;
		}
		if (glowBank != null)
		{
			glowBank.enabled = false;
		}
		if (bankGlow != null)
		{
			bankGlow.SetActive(value: false);
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas_Units");
		if (gameObject != null)
		{
			UIUnitsTechnology component = gameObject.GetComponent<UIUnitsTechnology>();
			component.tutorialGlowClose.enabled = false;
			component.tutorialGlowUpgrade.enabled = false;
		}
		gameObject = null;
		gameObject = GameObject.FindGameObjectWithTag("Canvas_General");
		if (gameObject != null)
		{
			UIGeneralTechnology component2 = gameObject.GetComponent<UIGeneralTechnology>();
			component2.tutorialGlowClose.enabled = false;
			component2.tutorialGlowUpgrade.enabled = false;
		}
	}
}
