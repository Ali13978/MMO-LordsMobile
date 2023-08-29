using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Demo
{
	public class AddressBookDemo : NPDisabledFeatureDemo
	{
		private float m_eachColumnWidth;

		private float m_eachRowHeight = 150f;

		private int m_maxContactsToRender = 50;

		private AddressBookContact[] m_contactsInfo;

		private Texture[] m_contactPictures;

		private GUIScrollView m_contactsScrollView;
	}
}
