using System.Collections;
using System.Collections.Generic;

namespace VoxelBusters.Utility
{
	public static class GenericsExtension
	{
		public static object[] ToArray(this IEnumerator _enumerator)
		{
			if (_enumerator == null)
			{
				return null;
			}
			List<object> list = new List<object>();
			while (_enumerator.MoveNext())
			{
				list.Add(_enumerator.Current);
			}
			return list.ToArray();
		}

		public static object[] ToArray(this IList _listObject)
		{
			if (_listObject == null)
			{
				return null;
			}
			int count = _listObject.Count;
			object[] array = new object[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = _listObject[i];
			}
			return array;
		}

		public static object[] ToArray(this ICollection _collection)
		{
			if (_collection == null)
			{
				return null;
			}
			IEnumerator enumerator = _collection.GetEnumerator();
			int count = _collection.Count;
			object[] array = new object[count];
			int num = 0;
			while (enumerator.MoveNext())
			{
				array[num++] = enumerator.Current;
			}
			return array;
		}
	}
}
