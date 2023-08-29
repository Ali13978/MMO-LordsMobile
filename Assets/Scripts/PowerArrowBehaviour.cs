using UnityEngine;

public class PowerArrowBehaviour : MonoBehaviour
{
	public float timeToDestroy;

	private float arrowATK;

	private float speedMove = 15f;

	private bool isStucked;

	public AudioClip[] sfxHitSomething;

	private Transform myTransform;

	private BoxCollider myCollider;

	private Rigidbody myRigidbody;

	private AudioSource audioSource;

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

	public bool IsStucked => isStucked;

	private void Awake()
	{
		myTransform = base.gameObject.GetComponent<Transform>();
		myCollider = base.gameObject.GetComponent<BoxCollider>();
		myRigidbody = base.gameObject.GetComponent<Rigidbody>();
		audioSource = base.gameObject.GetComponent<AudioSource>();
	}

	private void Start()
	{
		arrowATK = ConfigPrefsController.generalPowerArrowsDamageBase * (float)PlayerPrefs.GetInt("playerLevel") + ConfigPrefsController.generalPowerArrowsDamagePerLevel * (float)PlayerPrefsController.GeneralTechPowers_ArchersShower;
	}

	private void Update()
	{
		if (!isStucked)
		{
			myTransform.position += myTransform.up * speedMove * Time.deltaTime;
			return;
		}
		timeToDestroy -= Time.deltaTime;
		if (timeToDestroy <= 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (isStucked || col.CompareTag("ArrowDefence") || col.CompareTag("JavalineDefence") || col.CompareTag("JavalineAttack") || col.CompareTag("JavalineGate") || ((MainController.worldScreen != WorldScreen.AttackStarted || !col.CompareTag("ArrowReceiverDefence")) && (MainController.worldScreen != WorldScreen.Defence || !col.CompareTag("ArrowReceiverAttack")) && !col.CompareTag("Floor")))
		{
			return;
		}
		if (PlayerPrefsController.isSfx && sfxHitSomething.Length > 0)
		{
			float num = UnityEngine.Random.Range(0f, 1f);
			int num2 = UnityEngine.Random.Range(0, sfxHitSomething.Length);
			audioSource.PlayOneShot(sfxHitSomething[num2], AudioPrefsController.volumeBattlePowerArrowHit * AudioPrefsController.volumeBattle * AudioPrefsController.volumeMaster);
		}
		isStucked = true;
		myTransform.SetParent(col.transform);
		UnityEngine.Object.Destroy(myCollider);
		UnityEngine.Object.Destroy(myRigidbody);
		if (col.CompareTag("ArrowReceiverDefence") || col.CompareTag("ArrowReceiverAttack"))
		{
			CohortSoldierArrowReceiver component = col.GetComponent<CohortSoldierArrowReceiver>();
			float num3 = arrowATK;
			if (component.soldierScript.CohortParent.type != CohortType.Siege)
			{
				num3 += arrowATK * ConfigPrefsController.bonusSmallAmmoVsSoldier;
			}
			component.Hitted(arrowATK);
		}
	}
}
