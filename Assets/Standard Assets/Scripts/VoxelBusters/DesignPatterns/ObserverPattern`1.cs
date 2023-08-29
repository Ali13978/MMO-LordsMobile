using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.DesignPatterns
{
	public class ObserverPattern<T> : SingletonPattern<T> where T : MonoBehaviour
	{
		private List<IObserver> m_observers = new List<IObserver>();

		public void AddObserver(IObserver _observer)
		{
			if (!m_observers.Contains(_observer))
			{
				UnityEngine.Debug.Log($"[ObserverPattern] Adding new observer= {_observer.ToString()}.");
				m_observers.Add(_observer);
			}
		}

		public void RemoveObserver(IObserver _observer)
		{
			if (m_observers.Contains(_observer))
			{
				UnityEngine.Debug.Log($"[ObserverPattern] Removing observer= {_observer.ToString()}.");
				m_observers.Remove(_observer);
			}
		}

		public virtual void NotifyObservers(string _key, ArrayList _data)
		{
			UnityEngine.Debug.Log($"[ObserverPattern] {ToString()} is notifying observers with key {_key}.");
			for (int i = 0; i < m_observers.Count; i++)
			{
				m_observers[i].OnPropertyChange(_key, _data);
			}
		}
	}
}
