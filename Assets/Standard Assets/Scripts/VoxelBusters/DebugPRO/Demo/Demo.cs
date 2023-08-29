using UnityEngine;

namespace VoxelBusters.DebugPRO.Demo
{
	public class Demo : MonoBehaviour
	{
		private void Start()
		{
			PrintLogs();
		}

		private void PrintLogs(Console _p = null)
		{
			UnityEngine.Debug.Log("[Unity] message1");
			UnityEngine.Debug.Log("[Unity] message2");
			UnityEngine.Debug.Log("[Unity] message3");
			UnityEngine.Debug.Log("[Unity] message4");
			Console.Log("tag1", "[DebugPRO] message1");
			Console.Log("tag2", "[DebugPRO] message2");
			Console.Log("tag3", "[DebugPRO] message3");
			Console.Log("tag4", "[DebugPRO] message4");
		}
	}
}
