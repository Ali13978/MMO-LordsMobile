using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	[I2EditorInitialize]
	public class LocalizeTarget_UnityUI_RawImage : LocalizeTarget<RawImage>
	{
		static LocalizeTarget_UnityUI_RawImage()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizeTargetDesc_Type<RawImage, LocalizeTarget_UnityUI_RawImage> localizeTargetDesc_Type = new LocalizeTargetDesc_Type<RawImage, LocalizeTarget_UnityUI_RawImage>();
			localizeTargetDesc_Type.Name = "RawImage";
			localizeTargetDesc_Type.Priority = 100;
			LocalizationManager.RegisterTarget(localizeTargetDesc_Type);
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Texture;
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
			primaryTerm = ((!mTarget.mainTexture) ? string.Empty : mTarget.mainTexture.name);
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Texture texture = mTarget.texture;
			if (texture == null || texture.name != mainTranslation)
			{
				mTarget.texture = cmp.FindTranslatedObject<Texture>(mainTranslation);
			}
		}
	}
}
