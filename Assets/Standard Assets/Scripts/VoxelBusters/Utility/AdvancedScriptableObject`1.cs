using UnityEngine;

namespace VoxelBusters.Utility
{
	public abstract class AdvancedScriptableObject<T> : ScriptableObject where T : ScriptableObject
	{
		private static T instance;

		protected static string assetContainerFolderName = "VoxelBusters";

		public static T Instance
		{
			get
			{
				if ((Object)instance == (Object)null)
				{
					instance = GetAsset(typeof(T).Name);
				}
				return instance;
			}
		}

		protected virtual void Reset()
		{
		}

		protected virtual void OnEnable()
		{
			if ((Object)instance == (Object)null)
			{
				instance = (this as T);
			}
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnDestroy()
		{
		}

		public virtual void Save()
		{
		}

		private static string GetPathRelativeToResources(string _assetName)
		{
			return (assetContainerFolderName != null) ? $"{assetContainerFolderName}/{_assetName}" : _assetName;
		}

		private static string GetAssetPath(string _assetName)
		{
			return "Assets/Resources/" + GetPathRelativeToResources(_assetName) + ".asset";
		}

		public static T GetAsset(string _assetName)
		{
			string pathRelativeToResources = GetPathRelativeToResources(_assetName);
			return Resources.Load<T>(pathRelativeToResources);
		}
	}
}
