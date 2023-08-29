using UnityEngine;

public class UIBankTutorial : MonoBehaviour
{
	private TutorialController tutorialController;

	private UIController uiController;

	private BankController bankController;

	private SfxUIController sfxUIController;

	private UpgradesController upgradesController;

	private StationEngine stationEngine;

	public void Initialize(StationEngine stationEngine, TutorialController tutorialController, UIController uiController, BankController bankController, SfxUIController sfxUIController, UpgradesController upgradesController)
	{
		this.tutorialController = tutorialController;
		this.uiController = uiController;
		this.bankController = bankController;
		this.sfxUIController = sfxUIController;
		this.upgradesController = upgradesController;
		this.stationEngine = stationEngine;
		Touch_Battle.IsWindowSmallOpen = true;
	}

	public void ConfirmWindow()
	{
		sfxUIController.PlaySound(SfxUI.ClickDefault);
		PlayerPrefs.SetInt("bankTutorialDone", 1);
		Touch_Battle.IsWindowSmallOpen = false;
		tutorialController.ClearGlow();
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/CanvasBank")) as GameObject;
		UIBankMenu component = gameObject.GetComponent<UIBankMenu>();
		component.Initialize(stationEngine, bankController, uiController, sfxUIController, upgradesController, tutorialController);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ConfirmWindow();
		}
	}
}
