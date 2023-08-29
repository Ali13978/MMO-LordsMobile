using System.Collections;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public abstract class Request
	{
		private class SurrogateMonoBehaviour : MonoBehaviour
		{
		}

		private static MonoBehaviour surrogateMonobehaviour;

		public bool IsAsynchronous
		{
			get;
			private set;
		}

		public URL URL
		{
			get;
			private set;
		}

		protected WWW WWWObject
		{
			get;
			set;
		}

		private Request()
		{
		}

		protected Request(URL _URL, bool _isAsynchronous)
		{
			URL = _URL;
			IsAsynchronous = _isAsynchronous;
		}

		public void StartRequest()
		{
			if (WWWObject == null || string.IsNullOrEmpty(URL.URLString))
			{
				UnityEngine.Debug.LogError("[WebRequest] Request data is invalid.");
				DidFailStartRequestWithError("The operation could not be completed because request data is invalid.");
			}
			else if (IsAsynchronous)
			{
				if (surrogateMonobehaviour == null)
				{
					GameObject gameObject = new GameObject();
					gameObject.hideFlags = HideFlags.HideInHierarchy;
					surrogateMonobehaviour = gameObject.AddComponent<SurrogateMonoBehaviour>();
					Object.DontDestroyOnLoad(gameObject);
				}
				surrogateMonobehaviour.StartCoroutine(StartAsynchronousRequest());
			}
			else
			{
				while (!WWWObject.isDone)
				{
				}
				OnFetchingResponse();
			}
		}

		private IEnumerator StartAsynchronousRequest()
		{
			while (!WWWObject.isDone)
			{
				yield return null;
			}
			OnFetchingResponse();
		}

		public void Abort()
		{
			if (WWWObject != null && !WWWObject.isDone)
			{
				WWWObject.Dispose();
			}
		}

		protected abstract void OnFetchingResponse();

		protected abstract void DidFailStartRequestWithError(string _error);
	}
}
