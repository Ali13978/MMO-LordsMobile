using UnityEngine;

public class CohortSoldierAnimationActions : MonoBehaviour
{
	public CohortSoldier cohortSoldierScript;

	private void Awake()
	{
	}

	public void SpawnJavaline()
	{
		cohortSoldierScript.ShootJavaline();
	}

	public void HitEnemyMeelee()
	{
		cohortSoldierScript.HitEnemyMeelee();
	}
}
