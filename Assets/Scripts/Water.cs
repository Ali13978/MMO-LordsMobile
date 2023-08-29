using UnityEngine;

public class Water : MonoBehaviour
{
	public int materialIndex;

	public Vector2 uvAnimationRate = new Vector2(0.5f, 0f);

	public string textureName = "_MainTex";

	private Vector2 uvOffset = Vector2.zero;

	private Renderer myRenderer;

	private void Awake()
	{
		myRenderer = base.gameObject.GetComponent<Renderer>();
	}

	private void Update()
	{
		uvOffset += uvAnimationRate * Time.unscaledDeltaTime;
		if (myRenderer.enabled)
		{
			myRenderer.sharedMaterials[materialIndex].SetTextureOffset(textureName, uvOffset);
		}
	}
}
