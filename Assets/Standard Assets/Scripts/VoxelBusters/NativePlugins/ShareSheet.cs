using UnityEngine;
using VoxelBusters.NativePlugins.Internal;

namespace VoxelBusters.NativePlugins
{
	public class ShareSheet : ShareImageUtility, IShareView
	{
		private eShareOptions[] m_excludedShareOptions;

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

		public virtual eShareOptions[] ExcludedShareOptions
		{
			get
			{
				return m_excludedShareOptions;
			}
			set
			{
				m_excludedShareOptions = value;
			}
		}

		public bool IsReadyToShowView => !base.ImageAsyncDownloadInProgress;

		public ShareSheet()
		{
			Text = null;
			URL = null;
			ImageData = null;
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
