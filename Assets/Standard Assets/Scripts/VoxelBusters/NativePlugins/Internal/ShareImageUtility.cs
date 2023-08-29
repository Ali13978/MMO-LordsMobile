using System.Collections;
using UnityEngine;
using VoxelBusters.DesignPatterns;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public abstract class ShareImageUtility
	{
		private DownloadTexture m_downloadTexture;

		private IEnumerator m_takeScreenShotCoroutine;

		protected bool ImageAsyncDownloadInProgress
		{
			get;
			set;
		}

		public abstract void AttachImage(Texture2D _texture);

		public void AttachScreenShot()
		{
			StopAsyncRequests();
			ImageAsyncDownloadInProgress = true;
			m_takeScreenShotCoroutine = TextureExtensions.TakeScreenshot(delegate(Texture2D _texture)
			{
				AttachImage(_texture);
				ImageAsyncDownloadInProgress = false;
			});
			SingletonPattern<NPBinding>.Instance.StartCoroutine(m_takeScreenShotCoroutine);
		}

		public void AttachImageAtPath(string _imagePath)
		{
			StopAsyncRequests();
			ImageAsyncDownloadInProgress = true;
			URL uRL = URL.FileURLWithPath(_imagePath);
			m_downloadTexture = new DownloadTexture(uRL, _isAsynchronous: true, _autoFixOrientation: false);
			m_downloadTexture.OnCompletion = delegate(Texture2D _texture, string _error)
			{
				AttachImage(_texture);
				ImageAsyncDownloadInProgress = false;
			};
			m_downloadTexture.StartRequest();
		}

		protected void StopAsyncRequests()
		{
			if (m_downloadTexture != null)
			{
				m_downloadTexture.Abort();
				m_downloadTexture = null;
			}
			if (m_takeScreenShotCoroutine != null)
			{
				SingletonPattern<NPBinding>.Instance.StopCoroutine(m_takeScreenShotCoroutine);
				m_takeScreenShotCoroutine = null;
			}
		}
	}
}
