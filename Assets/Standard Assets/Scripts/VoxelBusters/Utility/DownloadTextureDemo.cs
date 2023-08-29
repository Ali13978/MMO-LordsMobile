using UnityEngine;

namespace VoxelBusters.Utility
{
	public class DownloadTextureDemo : MonoBehaviour
	{
		[SerializeField]
		private string m_URLString;

		[SerializeField]
		private MeshRenderer m_renderer;

		public void StartDownload()
		{
			URL uRL = (!m_URLString.StartsWith("http")) ? URL.FileURLWithPath(m_URLString) : URL.URLWithString(m_URLString);
			DownloadTexture downloadTexture = new DownloadTexture(uRL, _isAsynchronous: true, _autoFixOrientation: true);
			downloadTexture.OnCompletion = delegate(Texture2D _texture, string _error)
			{
				UnityEngine.Debug.Log($"[DownloadTextureDemo] Texture download completed. Error= {_error.GetPrintableString()}.");
				if (_texture != null)
				{
					m_renderer.sharedMaterial.mainTexture = _texture;
				}
			};
			downloadTexture.StartRequest();
		}
	}
}
