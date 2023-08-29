using AudienceNetwork;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterstitialAdTest : MonoBehaviour
{
	private InterstitialAd interstitialAd;

	private bool isLoaded;

	public Text statusLabel;

	public void LoadInterstitial()
	{
		statusLabel.text = "Loading interstitial ad...";
		InterstitialAd interstitialAd = this.interstitialAd = new InterstitialAd("YOUR_PLACEMENT_ID");
		this.interstitialAd.Register(base.gameObject);
		this.interstitialAd.InterstitialAdDidLoad = delegate
		{
			UnityEngine.Debug.Log("Interstitial ad loaded.");
			isLoaded = true;
			statusLabel.text = "Ad loaded. Click show to present!";
		};
		interstitialAd.InterstitialAdDidFailWithError = delegate(string error)
		{
			UnityEngine.Debug.Log("Interstitial ad failed to load with error: " + error);
			statusLabel.text = "Interstitial ad failed to load. Check console for details.";
		};
		interstitialAd.InterstitialAdWillLogImpression = delegate
		{
			UnityEngine.Debug.Log("Interstitial ad logged impression.");
		};
		interstitialAd.InterstitialAdDidClick = delegate
		{
			UnityEngine.Debug.Log("Interstitial ad clicked.");
		};
		this.interstitialAd.LoadAd();
	}

	public void ShowInterstitial()
	{
		if (isLoaded)
		{
			interstitialAd.Show();
			isLoaded = false;
			statusLabel.text = string.Empty;
		}
		else
		{
			statusLabel.text = "Ad not loaded. Click load to request an ad.";
		}
	}

	private void OnDestroy()
	{
		if (interstitialAd != null)
		{
			interstitialAd.Dispose();
		}
		UnityEngine.Debug.Log("InterstitialAdTest was destroyed!");
	}

	public void NextScene()
	{
		SceneManager.LoadScene("AdViewScene");
	}
}
