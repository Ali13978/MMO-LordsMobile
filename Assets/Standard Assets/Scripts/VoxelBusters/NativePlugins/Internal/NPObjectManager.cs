using System.Collections.Generic;

namespace VoxelBusters.NativePlugins.Internal
{
	public class NPObjectManager
	{
		public enum eCollectionType
		{
			GAME_SERVICES
		}

		private static Dictionary<string, NPObject> gameServicesObjectCollection = new Dictionary<string, NPObject>();

		public static void AddNewObjectToCollection(NPObject _newObject, eCollectionType _collectionType)
		{
			Dictionary<string, NPObject> dictionary = null;
			if (_collectionType == eCollectionType.GAME_SERVICES)
			{
				dictionary = gameServicesObjectCollection;
			}
			dictionary.Add(_newObject.GetInstanceID(), _newObject);
		}

		public static T GetObjectWithInstanceID<T>(string _instanceID, eCollectionType _collectionType) where T : NPObject
		{
			Dictionary<string, NPObject> dictionary = null;
			if (_collectionType == eCollectionType.GAME_SERVICES)
			{
				dictionary = gameServicesObjectCollection;
			}
			dictionary.TryGetValue(_instanceID, out NPObject value);
			return (T)value;
		}
	}
}
