using UnityEngine;

namespace VoxelBusters.Utility
{
	public static class GameObjectExtensions
	{
		public static GameObject CreateChild(this GameObject _parentGameObject, string _childName, Vector3 _localPosition, Quaternion _localRotation, Vector3 _localScale)
		{
			GameObject gameObject = new GameObject(_childName);
			_parentGameObject.AddChild(gameObject, _localPosition, _localRotation, _localScale);
			return gameObject;
		}

		public static void AddChild(this GameObject _parentGameObject, GameObject _childGameObject, Vector3 _localPosition, Quaternion _localRotation, Vector3 _localScale)
		{
			_parentGameObject.transform.AddChild(_childGameObject, _localPosition, _localRotation, _localScale);
		}

		public static T AddInvisibleComponent<T>(this GameObject _gameObject) where T : MonoBehaviour
		{
			T result = _gameObject.AddComponent<T>();
			result.hideFlags = HideFlags.HideInInspector;
			return result;
		}

		public static string GetPath(this GameObject _gameObject)
		{
			if (_gameObject == null)
			{
				return null;
			}
			return _gameObject.transform.GetPath();
		}
	}
}
