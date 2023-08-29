using UnityEngine;

public class ArrowsShower : MonoBehaviour
{
	public float timeToDestroy;

	public GameObject[] arrowsArray;

	private void Awake()
	{
	}

	private void Update()
	{
		timeToDestroy -= Time.deltaTime;
		if (timeToDestroy <= 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
