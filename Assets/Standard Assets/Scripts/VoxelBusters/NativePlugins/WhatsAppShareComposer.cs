using UnityEngine;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	public class WhatsAppShareComposer : ShareImageUtility, IShareView
	{
		public string Text
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
