using UnityEngine;

public class UIBankUnlocked : MonoBehaviour
{
	private MainController mainController;

	private UIController uiController;

	private SfxUIController sfxUiController;

	private TutorialController tutorialController;

	public void Initialize(MainController mainController, UIController uiController, SfxUIController sfxUiController, TutorialController tutorialController)
	{
		this.mainController = mainController;
		this.uiController = uiController;
		this.sfxUiController = sfxUiController;
		this.tutorialController = tutorialController;
		Touch_Battle.IsWindowSmallOpen = true;
	}

	public void CloseWindow()
	{
		sfxUiController.PlaySound(SfxUI.ClickDefault);
		MoveCamera();
		Touch_Battle.IsWindowSmallOpen = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void MoveCamera()
	{
		tutorialController.SetGlow(13);
		Camera component = GameObject.FindGameObjectWithTag("CameraDefence").GetComponent<Camera>();
		GameObject gameObject = component.gameObject;
		Vector3 position = component.transform.position;
		LeanTween.move(gameObject, new Vector2(56f, position.y), 1f).setEase(LeanTweenType.linear).setIgnoreTimeScale(useUnScaledTime: true);
	}
}
