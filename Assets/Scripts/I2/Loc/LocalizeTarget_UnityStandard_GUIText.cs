using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	[I2EditorInitialize]
	public class LocalizeTarget_UnityStandard_GUIText : LocalizeTarget<Text>
	{
		private TextAnchor mAlignment_RTL = TextAnchor.MiddleRight;

		private TextAnchor mAlignment_LTR;
        
		private bool mAlignmentWasRTL;

		private bool mInitializeAlignment = true;

		static LocalizeTarget_UnityStandard_GUIText()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizeTargetDesc_Type<Text, LocalizeTarget_UnityStandard_GUIText> localizeTargetDesc_Type = new LocalizeTargetDesc_Type<Text, LocalizeTarget_UnityStandard_GUIText>();
			localizeTargetDesc_Type.Name = "GUIText";
			localizeTargetDesc_Type.Priority = 100;
			LocalizationManager.RegisterTarget(localizeTargetDesc_Type);
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Font;
		}

		public override bool CanUseSecondaryTerm()
		{
			return true;
		}

		public override bool AllowMainTermToBeRTL()
		{
			return true;
		}

		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!mTarget) ? null : mTarget.text);
			secondaryTerm = ((!string.IsNullOrEmpty(Secondary) || !(mTarget.font != null)) ? null : mTarget.font.name);
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Font secondaryTranslatedObj = cmp.GetSecondaryTranslatedObj<Font>(ref mainTranslation, ref secondaryTranslation);
			if (secondaryTranslatedObj != null && mTarget.font != secondaryTranslatedObj)
			{
				mTarget.font = secondaryTranslatedObj;
			}
			if (mInitializeAlignment)
			{
				mInitializeAlignment = false;
				mAlignment_LTR = (mAlignment_RTL = mTarget.alignment);
				if (LocalizationManager.IsRight2Left && mAlignment_RTL == TextAnchor.MiddleRight)
				{
					mAlignment_LTR = TextAnchor.MiddleLeft;
				}
				if (!LocalizationManager.IsRight2Left && mAlignment_LTR == TextAnchor.MiddleLeft)
				{
					mAlignment_RTL = TextAnchor.MiddleRight;
				}
			}
			if (mainTranslation != null && mTarget.text != mainTranslation)
			{
				if (cmp.CorrectAlignmentForRTL && mTarget.alignment != TextAnchor.MiddleCenter)
				{
					mTarget.alignment = ((!LocalizationManager.IsRight2Left) ? mAlignment_LTR : mAlignment_RTL);
				}
				mTarget.text = mainTranslation;
			}
		}
	}
}
