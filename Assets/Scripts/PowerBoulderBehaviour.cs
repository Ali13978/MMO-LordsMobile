using UnityEngine;

public class PowerBoulderBehaviour : MonoBehaviour
{
	public int boulderIndex;

	public GameObject prefabExplosion;

	private bool isStucked;

	private float arrowATK;

	public GameObject particlesToDestroy;

	private GameObject tempExplosion;

	private Transform myTransform;

	private Rigidbody myRigidbody;

	private SphereCollider myCollider;

	public BouldersShower boulderShower;

	public SphereCollider mySphereCollider;

	private UpgradesController upgradesController;

	public float ArrowATK
	{
		get
		{
			return arrowATK;
		}
		set
		{
			arrowATK = value;
		}
	}

	private void Awake()
	{
		upgradesController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UpgradesController>();
		myTransform = base.gameObject.GetComponent<Transform>();
		myRigidbody = base.gameObject.GetComponent<Rigidbody>();
		myCollider = base.gameObject.GetComponent<SphereCollider>();
	}

	private void Start()
	{
		arrowATK = ConfigPrefsController.generalPowerBouldersDamageBase * (float)PlayerPrefs.GetInt("playerLevel") + ConfigPrefsController.generalPowerBouldersDamagePerLevel * (float)PlayerPrefsController.GeneralTechPowers_CatapultsShower;
	}

	private void OnTriggerEnter(Collider col)
	{
		if (!isStucked && col.gameObject.CompareTag("Floor"))
		{
			boulderShower.PlaySfxHit();
			CheckRadiusEffectCollision();
			isStucked = true;
			boulderShower.IsStucked[boulderIndex] = true;
			Transform transform = myTransform;
			Vector3 position = myTransform.position;
			float x = position.x;
			Vector3 position2 = myTransform.position;
			transform.position = new Vector3(x, 0f, position2.z);
			tempExplosion = (UnityEngine.Object.Instantiate(prefabExplosion, myTransform.position + new Vector3(0f, 0.025f, 0f), Quaternion.Euler(new Vector3(90f, 0f, 0f))) as GameObject);
			UnityEngine.Object.Destroy(tempExplosion, 2f);
			UnityEngine.Object.Destroy(particlesToDestroy);
			UnityEngine.Object.Destroy(myCollider);
			UnityEngine.Object.Destroy(myRigidbody);
		}
	}

	private void CheckRadiusEffectCollision()
	{
		for (int i = 0; i < upgradesController.CohortsEnemyArray.Count; i++)
		{
			upgradesController.CohortsEnemyArray[i].CheckRadiusCollision(mySphereCollider.bounds, arrowATK, _isBonusSiege: true);
		}
	}
}
