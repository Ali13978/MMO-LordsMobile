using UnityEngine;

public class SimpleAnimationDestroy : MonoBehaviour
{
	private bool isShown;

	public void DestroyMe()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void AnimationEnd()
	{
		UIController component = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
		component.ShowBoosterReload();
		if (PlayerPrefs.GetInt("playerWave") == ConfigPrefsController.waveFreeItem && PlayerPrefs.GetInt("moneyBoost") <= 0 && !isShown)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Windows/INAPPS/CanvasFreeItem")) as GameObject;
			FreeItemController component2 = gameObject.GetComponent<FreeItemController>();
			component2.Initialize(component);
			isShown = true;
		}
	}
}
