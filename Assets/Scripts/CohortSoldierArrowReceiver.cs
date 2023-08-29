using UnityEngine;

public class CohortSoldierArrowReceiver : MonoBehaviour
{
	public CohortSoldier soldierScript;

	private void Start()
	{
	}

	public void Hitted(float _damage)
	{
		soldierScript.Hitted(_damage, _isMelee: false);
	}
}
