using UnityEngine;

namespace I2.Loc
{
	[I2EditorInitialize]
	public class LocalizeTarget_UnityStandard_SpriteRenderer : LocalizeTarget<SpriteRenderer>
	{
		static LocalizeTarget_UnityStandard_SpriteRenderer()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizeTargetDesc_Type<SpriteRenderer, LocalizeTarget_UnityStandard_SpriteRenderer> localizeTargetDesc_Type = new LocalizeTargetDesc_Type<SpriteRenderer, LocalizeTarget_UnityStandard_SpriteRenderer>();
			localizeTargetDesc_Type.Name = "SpriteRenderer";
			localizeTargetDesc_Type.Priority = 100;
			LocalizationManager.RegisterTarget(localizeTargetDesc_Type);
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Sprite;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!(mTarget.sprite != null)) ? string.Empty : mTarget.sprite.name);
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Sprite sprite = mTarget.sprite;
			if (sprite == null || sprite.name != mainTranslation)
			{
				mTarget.sprite = cmp.FindTranslatedObject<Sprite>(mainTranslation);
			}
		}
	}
}
