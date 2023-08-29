using UnityEngine;

namespace I2.Loc
{
	[I2EditorInitialize]
	public class LocalizeTarget_UnityStandard_AudioSource : LocalizeTarget<AudioSource>
	{
		static LocalizeTarget_UnityStandard_AudioSource()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizeTargetDesc_Type<AudioSource, LocalizeTarget_UnityStandard_AudioSource> localizeTargetDesc_Type = new LocalizeTargetDesc_Type<AudioSource, LocalizeTarget_UnityStandard_AudioSource>();
			localizeTargetDesc_Type.Name = "AudioSource";
			localizeTargetDesc_Type.Priority = 100;
			LocalizationManager.RegisterTarget(localizeTargetDesc_Type);
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.AudioClip;
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
			primaryTerm = ((!mTarget.clip) ? string.Empty : mTarget.clip.name);
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			bool flag = (mTarget.isPlaying || mTarget.loop) && Application.isPlaying;
			AudioClip clip = mTarget.clip;
			AudioClip audioClip = cmp.FindTranslatedObject<AudioClip>(mainTranslation);
			if (clip != audioClip)
			{
				mTarget.clip = audioClip;
			}
			if (flag && (bool)mTarget.clip)
			{
				mTarget.Play();
			}
		}
	}
}
