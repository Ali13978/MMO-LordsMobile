using UnityEngine;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	public class SocialShareComposerBase : ShareImageUtility, IShareView
	{
		public eSocialServiceType ServiceType
		{
			get;
			private set;
		}

		public string Text
		{
			get;
			set;
		}

		public string URL
		{
			get;
			set;
		}

		public byte[] ImageData
		{
			get;
			private set;
		}

		public bool IsReadyToShowView => !base.ImageAsyncDownloadInProgress;

		private SocialShareComposerBase()
		{
		}

		protected SocialShareComposerBase(eSocialServiceType _serviceType)
		{
			ServiceType = _serviceType;
		}

		public override void AttachImage(Texture2D _texture)
		{
			if (_texture != null)
			{
				ImageData = _texture.EncodeToPNG();
			}
			else
			{
				ImageData = null;
			}
		}
	}
}
