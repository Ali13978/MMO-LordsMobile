using UnityEngine;
using UnityEngine.UI;

public class RewardAnimation : MonoBehaviour
{
	public Text textReward;

	public RectTransform myTransform;

	private void Awake()
	{
		myTransform.localScale = new Vector3(0.5f, 0.5f, 0.05f);
	}

	public void SetData(int _moneyReward)
	{
		textReward.text = _moneyReward.ToString("###,###,##0");
		LeanTween.scale(myTransform, new Vector3(1f, 1f, 1f), 1.5f).setEase(LeanTweenType.easeInOutQuad).setIgnoreTimeScale(useUnScaledTime: true)
			.setOnComplete(FinishedStepA);
	}

	public void SetData(string _moneyReward)
	{
		textReward.text = _moneyReward;
		LeanTween.scale(myTransform, new Vector3(1f, 1f, 1f), 1.5f).setEase(LeanTweenType.easeInOutQuad).setIgnoreTimeScale(useUnScaledTime: true)
			.setOnComplete(FinishedStepA);
	}

	private void FinishedStepA()
	{
		float time = 0.75f;
		RectTransform component = textReward.GetComponent<RectTransform>();
		LeanTween.colorText(component, new Color(1f, 1f, 1f, 0f), time).setIgnoreTimeScale(useUnScaledTime: true).setOnComplete(FinishedStepB);
		LeanTween.color(myTransform, new Color(1f, 1f, 1f, 0f), time).setIgnoreTimeScale(useUnScaledTime: true);
	}

	private void FinishedStepB()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
