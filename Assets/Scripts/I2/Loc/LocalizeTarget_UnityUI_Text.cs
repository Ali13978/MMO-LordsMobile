using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	[I2EditorInitialize]
	public class LocalizeTarget_UnityUI_Text : LocalizeTarget<Text>
	{
		private TextAnchor mAlignment_RTL = TextAnchor.UpperRight;

		private TextAnchor mAlignment_LTR;

		private bool mAlignmentWasRTL;

		private bool mInitializeAlignment = true;

		static LocalizeTarget_UnityUI_Text()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizeTargetDesc_Type<Text, LocalizeTarget_UnityUI_Text> localizeTargetDesc_Type = new LocalizeTargetDesc_Type<Text, LocalizeTarget_UnityUI_Text>();
			localizeTargetDesc_Type.Name = "Text";
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
			secondaryTerm = ((!(mTarget.font != null)) ? string.Empty : mTarget.font.name);
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Font secondaryTranslatedObj = cmp.GetSecondaryTranslatedObj<Font>(ref mainTranslation, ref secondaryTranslation);
			if (secondaryTranslatedObj != null && secondaryTranslatedObj != mTarget.font)
			{
				mTarget.font = secondaryTranslatedObj;
			}
			if (mInitializeAlignment)
			{
				mInitializeAlignment = false;
				mAlignmentWasRTL = LocalizationManager.IsRight2Left;
				InitAlignment(mAlignmentWasRTL, mTarget.alignment, out mAlignment_LTR, out mAlignment_RTL);
			}
			else
			{
				InitAlignment(mAlignmentWasRTL, mTarget.alignment, out TextAnchor alignLTR, out TextAnchor alignRTL);
				if ((mAlignmentWasRTL && mAlignment_RTL != alignRTL) || (!mAlignmentWasRTL && mAlignment_LTR != alignLTR))
				{
					mAlignment_LTR = alignLTR;
					mAlignment_RTL = alignRTL;
				}
				mAlignmentWasRTL = LocalizationManager.IsRight2Left;
			}
			if (mainTranslation != null && mTarget.text != mainTranslation)
			{
				if (cmp.CorrectAlignmentForRTL)
				{
					mTarget.alignment = ((!LocalizationManager.IsRight2Left) ? mAlignment_LTR : mAlignment_RTL);
				}
				mTarget.text = mainTranslation;
				mTarget.SetVerticesDirty();
			}
		}

		private void InitAlignment(bool isRTL, TextAnchor alignment, out TextAnchor alignLTR, out TextAnchor alignRTL)
		{
			alignLTR = (alignRTL = alignment);
			if (isRTL)
			{
				switch (alignment)
				{
				case TextAnchor.UpperCenter:
				case TextAnchor.MiddleCenter:
				case TextAnchor.LowerCenter:
					break;
				case TextAnchor.UpperRight:
					alignLTR = TextAnchor.UpperLeft;
					break;
				case TextAnchor.MiddleRight:
					alignLTR = TextAnchor.MiddleLeft;
					break;
				case TextAnchor.LowerRight:
					alignLTR = TextAnchor.LowerLeft;
					break;
				case TextAnchor.UpperLeft:
					alignLTR = TextAnchor.UpperRight;
					break;
				case TextAnchor.MiddleLeft:
					alignLTR = TextAnchor.MiddleRight;
					break;
				case TextAnchor.LowerLeft:
					alignLTR = TextAnchor.LowerRight;
					break;
				}
			}
			else
			{
				switch (alignment)
				{
				case TextAnchor.UpperCenter:
				case TextAnchor.MiddleCenter:
				case TextAnchor.LowerCenter:
					break;
				case TextAnchor.UpperRight:
					alignRTL = TextAnchor.UpperLeft;
					break;
				case TextAnchor.MiddleRight:
					alignRTL = TextAnchor.MiddleLeft;
					break;
				case TextAnchor.LowerRight:
					alignRTL = TextAnchor.LowerLeft;
					break;
				case TextAnchor.UpperLeft:
					alignRTL = TextAnchor.UpperRight;
					break;
				case TextAnchor.MiddleLeft:
					alignRTL = TextAnchor.MiddleRight;
					break;
				case TextAnchor.LowerLeft:
					alignRTL = TextAnchor.LowerRight;
					break;
				}
			}
		}
	}
}
