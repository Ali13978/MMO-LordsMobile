using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public static class ComponentExtensions
	{
		public static T AddComponentIfNotFound<T>(this GameObject _gameObject) where T : Component
		{
			T val = _gameObject.GetComponent<T>();
			if ((Object)val == (Object)null)
			{
				val = _gameObject.AddComponent<T>();
			}
			return val;
		}

		public static T[] GetComponentsInChildren<T>(this Component _component, bool _includeParent, bool _includeInactive) where T : Component
		{
			T[] componentsInChildren = _component.GetComponentsInChildren<T>(_includeInactive);
			if (_includeParent)
			{
				return componentsInChildren;
			}
			List<T> list = new List<T>();
			T[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				T item = array[i];
				if (item.transform != _component.transform)
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}
	}
}
