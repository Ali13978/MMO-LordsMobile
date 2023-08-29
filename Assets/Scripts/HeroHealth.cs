using UnityEngine;

public class HeroHealth : MonoBehaviour
{
	private Transform healthBarBackgroundTransform;

	public Transform healthBarFrontTransform;

	public SpriteRenderer healthBarFrontRenderer;

	public Transform heroTransform;

	public CohortSoldier mySoldier;

	public Transform[] transformHealthExample;

	private float initialHP;

	private Vector3 initialPosition;

	private void Awake()
	{
		healthBarFrontRenderer = healthBarFrontTransform.GetComponent<SpriteRenderer>();
		initialHP = mySoldier.statsHP;
		healthBarBackgroundTransform = base.gameObject.GetComponent<Transform>();
		if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
		{
			healthBarBackgroundTransform.localPosition = transformHealthExample[1].localPosition;
			healthBarBackgroundTransform.localRotation = Quaternion.Euler(new Vector3(0f, 20f, 0f));
		}
		else if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
		{
			healthBarBackgroundTransform.localPosition = transformHealthExample[0].localPosition;
			if (mySoldier.CohortParent.type == CohortType.Siege)
			{
				healthBarBackgroundTransform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			}
			else
			{
				healthBarBackgroundTransform.localRotation = Quaternion.Euler(new Vector3(0f, 340f, 0f));
			}
		}
		initialPosition = healthBarBackgroundTransform.localPosition;
	}

	private void Start()
	{
		if ((mySoldier.CohortParent.stance == CohortStance.Attacker && (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)) || (mySoldier.CohortParent.stance == CohortStance.Defender && (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)))
		{
			healthBarFrontRenderer.color = Color.green;
		}
		else
		{
			healthBarFrontRenderer.color = Color.red;
		}
		initialHP = mySoldier.statsHP;
	}

	private void Update()
	{
		if (mySoldier.StatsHP > 0f)
		{
			if (MainController.worldScreen == WorldScreen.Attack || MainController.worldScreen == WorldScreen.AttackStarted)
			{
				healthBarBackgroundTransform.position = heroTransform.position - new Vector3(initialPosition.x, 0f - initialPosition.y, initialPosition.z);
			}
			else if (MainController.worldScreen == WorldScreen.Defence || MainController.worldScreen == WorldScreen.Upgrade)
			{
				healthBarBackgroundTransform.position = heroTransform.position + initialPosition;
			}
			healthBarFrontTransform.localScale = new Vector3(Mathf.Lerp(0f, 0.94f, mySoldier.StatsHP / initialHP), 0.6f, 1f);
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
