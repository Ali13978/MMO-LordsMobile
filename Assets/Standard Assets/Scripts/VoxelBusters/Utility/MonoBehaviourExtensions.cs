using UnityEngine;

namespace VoxelBusters.Utility
{
	public static class MonoBehaviourExtensions
	{
		private static bool isPaused;

		private static float timeScale = 1f;

		public static void PauseUnity(this MonoBehaviour _monoTarget)
		{
			if (!isPaused)
			{
				UnityEngine.Debug.LogWarning("[MonoBehaviourExtensions] Paused");
				isPaused = true;
				timeScale = Time.timeScale;
				Time.timeScale = 0f;
			}
		}

		public static void ResumeUnity(this MonoBehaviour _monoTarget)
		{
			if (isPaused)
			{
				UnityEngine.Debug.LogWarning("[MonoBehaviourExtensions] Resumed");
				isPaused = false;
				Time.timeScale = timeScale;
			}
		}
	}
}
