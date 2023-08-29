using UnityEngine;

public class BrokenDoorController : MonoBehaviour
{
	public Rigidbody[] rigidBodiesArray = new Rigidbody[2];

	public float forceExplosionFrom;

	public float forceExplosionTo;

	private void Start()
	{
		for (int i = 0; i < rigidBodiesArray.Length; i++)
		{
			float x = UnityEngine.Random.Range(forceExplosionFrom, forceExplosionTo);
			rigidBodiesArray[i].AddForce(new Vector3(x, 0f, 0f));
		}
	}
}
