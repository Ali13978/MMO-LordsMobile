namespace VoxelBusters.NativePlugins.Internal
{
	public class NPObject
	{
		private string m_instaceID;

		private NPObject()
		{
		}

		public NPObject(NPObjectManager.eCollectionType _collectionType)
		{
			m_instaceID = InstanceIDGenerator.Create();
			NPObjectManager.AddNewObjectToCollection(this, _collectionType);
		}

		public string GetInstanceID()
		{
			return m_instaceID;
		}
	}
}
