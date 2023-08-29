using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingInvasionAnimation : MonoBehaviour
{
	public Image imageBackground;

	public Text textLoading;

	private RectTransform transformTextLoading;

	public RectTransform transformTitle;

	public Color initialColorBackground;

	public Color finalColorBackground;

	private Vector2 finalPositionText;

	private Vector2 finalPositionTitle;

	private float timeWait;

	private float timeFlag = 2f;

	private void Awake()
	{
		transformTextLoading = textLoading.GetComponent<RectTransform>();
		imageBackground.color = initialColorBackground;
		finalPositionTitle = transformTitle.anchoredPosition;
		finalPositionText = transformTextLoading.anchoredPosition;
		transformTitle.anchoredPosition = new Vector2(0f, 450f);
		transformTextLoading.anchoredPosition = new Vector2(0f, -350f);
	}

	private void Start()
	{
		MenuAnimation();
	}

	private void MenuAnimation()
	{
		float time = 0.75f;
		LeanTween.move(transformTextLoading, finalPositionText, time).setEase(LeanTweenType.easeOutSine).setIgnoreTimeScale(useUnScaledTime: true);
		LeanTween.move(transformTitle, finalPositionTitle, time).setEase(LeanTweenType.easeOutSine).setIgnoreTimeScale(useUnScaledTime: true);
		LeanTween.color(imageBackground.rectTransform, finalColorBackground, time).setEase(LeanTweenType.easeInOutSine).setOnComplete(StartLoading)
			.setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void StartLoading()
	{
		StartCoroutine(AsynchronousLoad());
	}

	private IEnumerator AsynchronousLoad()
	{
		AsyncOperation ao = SceneManager.LoadSceneAsync("MainScene");
		ao.allowSceneActivation = false;
		while (!ao.isDone || timeWait < timeFlag)
		{
			if (timeWait < timeFlag)
			{
				timeWait += Time.deltaTime;
				if (timeWait >= timeFlag)
				{
					ao.allowSceneActivation = true;
				}
			}
			yield return null;
		}
	}
}
