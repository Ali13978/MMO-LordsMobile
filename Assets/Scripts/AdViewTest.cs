using AudienceNetwork;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdViewTest : MonoBehaviour
{
	private AdView adView;

	private void Awake()
	{
		AdView adView = this.adView = new AdView("YOUR_PLACEMENT_ID", AdSize.BANNER_HEIGHT_50);
		this.adView.Register(base.gameObject);
		this.adView.AdViewDidLoad = delegate
		{
			UnityEngine.Debug.Log("Ad view loaded.");
			this.adView.Show(100.0);
		};
		adView.AdViewDidFailWithError = delegate(string error)
		{
			UnityEngine.Debug.Log("Ad view failed to load with error: " + error);
		};
		adView.AdViewWillLogImpression = delegate
		{
			UnityEngine.Debug.Log("Ad view logged impression.");
		};
		adView.AdViewDidClick = delegate
		{
			UnityEngine.Debug.Log("Ad view clicked.");
		};
		adView.LoadAd();
	}

	private void OnDestroy()
	{
		if ((bool)adView)
		{
			adView.Dispose();
		}
		UnityEngine.Debug.Log("AdViewTest was destroyed!");
	}

	public void NextScene()
	{
		SceneManager.LoadScene("NativeAdScene");
	}
}
