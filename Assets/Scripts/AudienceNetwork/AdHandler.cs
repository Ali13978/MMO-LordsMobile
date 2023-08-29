using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudienceNetwork
{
	public class AdHandler : MonoBehaviour
	{
		private static readonly Queue<Action> executeOnMainThreadQueue = new Queue<Action>();

		public void executeOnMainThread(Action action)
		{
			executeOnMainThreadQueue.Enqueue(action);
		}

		private void Update()
		{
			while (executeOnMainThreadQueue.Count > 0)
			{
				executeOnMainThreadQueue.Dequeue()();
			}
		}

		public void removeFromParent()
		{
			UnityEngine.Object.Destroy(this);
		}
	}
}
