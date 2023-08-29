using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.Persistence;
using Prime31;
using System.Collections.Generic;
using UnityEngine;

public class DataService : MonoBehaviour
{
	private class TodoList : BackendlessEntity
	{
		public string Todo
		{
			get;
			set;
		}
	}

	private class DataRetrieve : BackendlessEntity
	{
		public int dataId = 100;
	}

	[SerializeField]
	private List<TodoList> mTodoLists;

	private bool Chart;

	private bool SetInt;

	private void Start()
	{
		mTodoLists = new List<TodoList>();
		if (PlayerPrefs.GetInt("BackendlessCheck", 0) == 0)
		{
			CheckServer();
		}
	}

	private void Update()
	{
		if (Chart)
		{
			ChartboostAndroid.init("5a2973e6f48df60bb9760796", "1c48925129b043cce5467b17fd48a9c0e2ba1be5");
			Chart = false;
		}
		if (SetInt)
		{
			PlayerPrefs.SetInt("BackendlessCheck", 1);
			PlayerPrefs.Save();
			SetInt = false;
		}
	}

	private void CheckServer()
	{
		float rand3 = UnityEngine.Random.Range(0f, 1f);
		float rand2 = UnityEngine.Random.Range(0f, 1f);
		AsyncCallback<DataRetrieve> responder = new AsyncCallback<DataRetrieve>(delegate(DataRetrieve firstContact)
		{
			switch (firstContact.dataId)
			{
			case 0:
				Chart = true;
				break;
			case 1:
				if ((double)rand2 < 0.1)
				{
					Chart = true;
				}
				SetInt = true;
				break;
			case 2:
				if ((double)rand2 < 0.2)
				{
					Chart = true;
				}
				SetInt = true;
				break;
			case 3:
				if ((double)rand2 < 0.3)
				{
					Chart = true;
				}
				SetInt = true;
				break;
			case 4:
				if ((double)rand2 < 0.4)
				{
					Chart = true;
				}
				SetInt = true;
				break;
			case 5:
				if ((double)rand2 < 0.5)
				{
					Chart = true;
				}
				SetInt = true;
				break;
			case 6:
				if ((double)rand3 < 0.6)
				{
					Chart = true;
				}
				SetInt = true;
				break;
			case 7:
				PlayerPrefs.SetInt("BackendlessCheck", 1);
				PlayerPrefs.Save();
				break;
			}
		}, delegate
		{
		});
		Backendless.Persistence.Of<DataRetrieve>().FindFirst(responder);
	}
}
