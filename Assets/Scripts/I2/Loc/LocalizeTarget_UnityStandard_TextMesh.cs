using UnityEngine;

namespace I2.Loc
{
	[I2EditorInitialize]
	public class LocalizeTarget_UnityStandard_TextMesh : LocalizeTarget<TextMesh>
	{
		private TextAlignment mAlignment_RTL = TextAlignment.Right;

		private TextAlignment mAlignment_LTR;

		private bool mAlignmentWasRTL;

		private bool mInitializeAlignment = true;

		static LocalizeTarget_UnityStandard_TextMesh()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizeTargetDesc_Type<TextMesh, LocalizeTarget_UnityStandard_TextMesh> localizeTargetDesc_Type = new LocalizeTargetDesc_Type<TextMesh, LocalizeTarget_UnityStandard_TextMesh>();
			localizeTargetDesc_Type.Name = "TextMesh";
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
				if (LocalizationManager.IsRight2Left && mAlignment_RTL == TextAlignment.Right)
				{
					mAlignment_LTR = TextAlignment.Left;
				}
				if (!LocalizationManager.IsRight2Left && mAlignment_LTR == TextAlignment.Left)
				{
					mAlignment_RTL = TextAlignment.Right;
				}
			}
			if (mainTranslation != null && mTarget.text != mainTranslation)
			{
				if (cmp.CorrectAlignmentForRTL && mTarget.alignment != TextAlignment.Center)
				{
					mTarget.alignment = ((!LocalizationManager.IsRight2Left) ? mAlignment_LTR : mAlignment_RTL);
				}
				mTarget.font.RequestCharactersInTexture(mainTranslation);
				mTarget.text = mainTranslation;
			}
		}
	}
}
