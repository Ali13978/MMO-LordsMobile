using UnityEngine;

namespace VoxelBusters.DebugPRO
{
	public class UnityDebugUtility : ScriptableObject
	{
		private static UnityDebugUtility instance;

		public static event Application.LogCallback LogCallback;

		static UnityDebugUtility()
		{
		}

		private static void EditorUpdate()
		{
			if (instance == null)
			{
				instance = ScriptableObject.CreateInstance<UnityDebugUtility>();
			}
			instance.hideFlags = HideFlags.HideAndDontSave;
			Application.logMessageReceived -= instance.HandleLog;
			Application.logMessageReceived += instance.HandleLog;
		}

		private void HandleLog(string _message, string _stackTrace, LogType _logType)
		{
			if (UnityDebugUtility.LogCallback != null)
			{
				UnityDebugUtility.LogCallback(_message, _stackTrace, _logType);
			}
		}
	}
}
