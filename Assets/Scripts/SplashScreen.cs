using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
	public Text textLoading;

	public Image imageLoading;

	private int step;

	private string[] stepText = new string[4]
	{
		"LOADING",
		"LOADING.",
		"LOADING..",
		"LOADING..."
	};

	private float timeStepFlag = 0.25f;

	private float timeStep;

	private void Awake()
	{
		textLoading.text = stepText[step];
		timeStep = timeStepFlag;
	}

	private void Start()
	{
		imageLoading.color = Color.white;
		StartCoroutine(AsynchronousLoad("MainScene"));
	}

	private void Update()
	{
		timeStep -= Time.deltaTime;
		if (timeStep <= 0f)
		{
			step++;
			if (step >= stepText.Length)
			{
				step = 0;
			}
			timeStep = timeStepFlag;
			textLoading.text = stepText[step];
		}
	}

	private IEnumerator AsynchronousLoad(string scene)
	{
		yield return null;
		AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
		ao.allowSceneActivation = true;
	}
}
