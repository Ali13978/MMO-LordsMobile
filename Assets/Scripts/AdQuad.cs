using AudienceNetwork;
using UnityEngine;

[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class AdQuad : MonoBehaviour
{
	public AdManager adManager;

	public bool useIconImage;

	public bool useCoverImage;

	private bool adRendered;

	private void Start()
	{
		Renderer component = GetComponent<Renderer>();
		component.enabled = false;
		adRendered = false;
	}

	private void OnGUI()
	{
		NativeAd nativeAd = adManager.nativeAd;
		if ((bool)nativeAd && adManager.IsAdLoaded() && !adRendered)
		{
			Sprite sprite = null;
			if (useCoverImage)
			{
				sprite = nativeAd.CoverImage;
			}
			else if (useIconImage)
			{
				sprite = nativeAd.IconImage;
			}
			if ((bool)sprite)
			{
				MeshRenderer component = GetComponent<MeshRenderer>();
				Renderer component2 = GetComponent<Renderer>();
				component2.enabled = true;
				Texture2D texture = sprite.texture;
				Material material = new Material(Shader.Find("Sprites/Default"));
				material.color = Color.white;
				material.SetTexture("texture", texture);
				component.sharedMaterial = material;
				component2.material.mainTexture = texture;
				adRendered = true;
			}
		}
	}
}
