using UnityEngine;
using UnityEngine.UI;

public class UIBankIcon : MonoBehaviour
{
	public Image imageBankBackground;

	public Image imageButtonBankStatus;

	public Image imageButtonBankFill;

	public Sprite[] spriteButtonBankStatus;

	public Sprite[] spriteButtonBankBackground;

	public BankController bankController;

	public void UpdateIconStatus()
	{
		if (!bankController.IsReady())
		{
			return;
		}
		if (bankController.GetRewardAccumulated() > 0)
		{
			imageButtonBankStatus.sprite = spriteButtonBankStatus[1];
			imageBankBackground.sprite = spriteButtonBankBackground[0];
			float num = bankController.GetMaxReward();
			float num2 = bankController.GetRewardAccumulated();
			imageButtonBankFill.fillAmount = num2 / num;
			if (num2 >= num)
			{
				imageBankBackground.sprite = spriteButtonBankBackground[1];
			}
		}
		else
		{
			imageButtonBankStatus.sprite = spriteButtonBankStatus[0];
			imageBankBackground.sprite = spriteButtonBankBackground[0];
			imageButtonBankFill.fillAmount = 0f;
		}
	}

	public void AnimationPayReward()
	{
		UpdateIconStatus();
		LeanTween.value(base.gameObject, imageButtonBankFill.fillAmount, 0f, 0.5f).setOnUpdate(delegate(float val)
		{
			imageButtonBankFill.fillAmount = val;
		}).setIgnoreTimeScale(useUnScaledTime: true)
			.setOnComplete(AnimationPayRewardFinished);
	}

	private void AnimationPayRewardFinished()
	{
		UpdateIconStatus();
		UIController.instance.UpdateUIUpgrade();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (MainController.worldScreen == WorldScreen.Upgrade && bankController.IsReady() && !bankController.IsChecked)
		{
			UpdateIconStatus();
			bankController.IsChecked = true;
		}
	}
}
