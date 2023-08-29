using UnityEngine;

[ExecuteInEditMode]
public class SpriteOutline : MonoBehaviour
{
	public Color color = Color.white;

	private SpriteRenderer spriteRenderer;

	private void OnEnable()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		UpdateOutline(outline: true);
	}

	private void OnDisable()
	{
		UpdateOutline(outline: false);
	}

	private void Update()
	{
		UpdateOutline(outline: true);
	}

	private void UpdateOutline(bool outline)
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		spriteRenderer.GetPropertyBlock(materialPropertyBlock);
		materialPropertyBlock.SetFloat("_Outline", (!outline) ? 0f : 1f);
		materialPropertyBlock.SetColor("_OutlineColor", color);
		spriteRenderer.SetPropertyBlock(materialPropertyBlock);
	}
}
