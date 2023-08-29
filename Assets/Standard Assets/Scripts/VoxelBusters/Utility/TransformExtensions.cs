using UnityEngine;

namespace VoxelBusters.Utility
{
	public static class TransformExtensions
	{
		public static void AddChild(this Transform _parentTransform, GameObject _childGameObject, Vector3 _localPosition, Quaternion _localRotation, Vector3 _localScale)
		{
			Transform transform = _childGameObject.transform;
			transform.parent = _parentTransform;
			transform.localPosition = _localPosition;
			transform.localRotation = _localRotation;
			transform.localScale = _localScale;
		}

		public static void AddChild(this RectTransform _parentTransform, GameObject _childGameObject, Vector3 _localPosition, Quaternion _localRotation, Vector3 _localScale)
		{
			Transform transform = _childGameObject.transform;
			transform.SetParent(_parentTransform, worldPositionStays: false);
			transform.localPosition = _localPosition;
			transform.localRotation = _localRotation;
			transform.localScale = _localScale;
		}

		public static string GetPath(this Transform _transform)
		{
			if (_transform == null)
			{
				return null;
			}
			Transform parent = _transform.parent;
			if (parent == null)
			{
				return "/" + _transform.name;
			}
			return parent.GetPath() + "/" + _transform.name;
		}
	}
}
