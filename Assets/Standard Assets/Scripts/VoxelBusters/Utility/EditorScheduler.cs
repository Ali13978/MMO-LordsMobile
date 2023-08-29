using System;

namespace VoxelBusters.Utility
{
	public class EditorScheduler
	{
		public delegate void CallbackFunction();

		public static event CallbackFunction ScheduleUpdate;

		static EditorScheduler()
		{
		}

		private static void Update()
		{
			if (EditorScheduler.ScheduleUpdate == null)
			{
				return;
			}
			Delegate[] invocationList = EditorScheduler.ScheduleUpdate.GetInvocationList();
			int num = invocationList.Length;
			for (int i = 0; i < num; i++)
			{
				if ((object)invocationList[i] != null)
				{
					invocationList[i].Method.Invoke(null, null);
				}
			}
		}
	}
}
