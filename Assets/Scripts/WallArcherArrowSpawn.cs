using UnityEngine;

public class WallArcherArrowSpawn : MonoBehaviour
{
	public WallArcher wallArcherScript;

	private void Start()
	{
	}

	public void SpawnArrow()
	{
		wallArcherScript.SpawnArrow();
	}
}
