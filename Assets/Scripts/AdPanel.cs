using AudienceNetwork;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
public class AdPanel : MonoBehaviour
{
	public AdManager adManager;

	[Header("Text:")]
	public Text title;

	public Text socialContext;

	[Header("Images:")]
	public Image coverImage;

	public Image iconImage;

	[Header("Buttons:")]
	public Text callToAction;

	public Button callToActionButton;

	private bool adIconContentFilled;

	private bool adCoverContentFilled;

	private bool adTextContentFilled;

	private void Awake()
	{
		adIconContentFilled = false;
		adCoverContentFilled = false;
		adTextContentFilled = false;
	}

	private void Update()
	{
		NativeAd nativeAd = adManager.nativeAd;
		if (adManager.IsAdLoaded() && nativeAd != null)
		{
			if (nativeAd.CoverImage != null && !adCoverContentFilled)
			{
				adCoverContentFilled = true;
				coverImage.sprite = nativeAd.CoverImage;
			}
			if (nativeAd.IconImage != null && !adIconContentFilled)
			{
				adIconContentFilled = true;
				iconImage.sprite = nativeAd.IconImage;
			}
			if (!adTextContentFilled)
			{
				adTextContentFilled = true;
				title.text = nativeAd.Title;
				socialContext.text = nativeAd.SocialContext;
				callToAction.text = nativeAd.CallToAction;
			}
		}
	}

	public void registerGameObjectForImpression()
	{
		NativeAd nativeAd = adManager.nativeAd;
		if (nativeAd != null && base.gameObject.GetComponent<NativeAdHandler>() == null)
		{
			nativeAd.RegisterGameObjectForImpression(base.gameObject, new Button[1]
			{
				callToActionButton
			});
		}
	}
}
