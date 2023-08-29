using System.IO;
using UnityEngine;

namespace VoxelBusters.NativePlugins.Demo
{
	public class Utility : MonoBehaviour
	{
		private void Start()
		{
			UnityEngine.ScreenCapture.CaptureScreenshot("Screenshot.png");
		}

		public static string GetScreenshotPath()
		{
			return Path.Combine(Application.persistentDataPath, "Screenshot.png");
		}
	}
}
