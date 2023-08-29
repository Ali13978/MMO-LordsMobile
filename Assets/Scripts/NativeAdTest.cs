using AudienceNetwork;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
public class NativeAdTest : MonoBehaviour
{
	private NativeAd nativeAd;

	[Header("Text:")]
	public Text title;

	public Text socialContext;

	public Text status;

	[Header("Images:")]
	public Image coverImage;

	public Image iconImage;

	[Header("Buttons:")]
	public Text callToAction;

	public Button callToActionButton;

	private void Awake()
	{
		Log("Native ad ready to load.");
	}

	private void OnGUI()
	{
		if (nativeAd != null && nativeAd.CoverImage != null)
		{
			coverImage.sprite = nativeAd.CoverImage;
		}
		if (nativeAd != null && nativeAd.IconImage != null)
		{
			iconImage.sprite = nativeAd.IconImage;
		}
	}

	private void OnDestroy()
	{
		if ((bool)nativeAd)
		{
			nativeAd.Dispose();
		}
		UnityEngine.Debug.Log("NativeAdTest was destroyed!");
	}

	public void LoadAd()
	{
		NativeAd nativeAd = new NativeAd("YOUR_PLACEMENT_ID");
		this.nativeAd = nativeAd;
		nativeAd.RegisterGameObjectForImpression(base.gameObject, new Button[1]
		{
			callToActionButton
		});
		nativeAd.NativeAdDidLoad = delegate
		{
			Log("Native ad loaded.");
			UnityEngine.Debug.Log("Loading images...");
			StartCoroutine(nativeAd.LoadIconImage(nativeAd.IconImageURL));
			StartCoroutine(nativeAd.LoadCoverImage(nativeAd.CoverImageURL));
			UnityEngine.Debug.Log("Images loaded.");
			title.text = nativeAd.Title;
			socialContext.text = nativeAd.SocialContext;
			callToAction.text = nativeAd.CallToAction;
		};
		nativeAd.NativeAdDidFailWithError = delegate(string error)
		{
			Log("Native ad failed to load with error: " + error);
		};
		nativeAd.NativeAdWillLogImpression = delegate
		{
			Log("Native ad logged impression.");
		};
		nativeAd.NativeAdDidClick = delegate
		{
			Log("Native ad clicked.");
		};
		nativeAd.LoadAd();
		Log("Native ad loading...");
	}

	private void Log(string s)
	{
		status.text = s;
		UnityEngine.Debug.Log(s);
	}

	public void NextScene()
	{
		SceneManager.LoadScene("RewardedVideoAdScene");
	}
}
