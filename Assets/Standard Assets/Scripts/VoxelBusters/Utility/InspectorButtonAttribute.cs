using UnityEngine;

namespace VoxelBusters.Utility
{
	public class InspectorButtonAttribute : PropertyAttribute
	{
		public InspectorButtonInfo[] Buttons
		{
			get;
			private set;
		}

		public eInspectorButtonPosition Position
		{
			get;
			private set;
		}

		private InspectorButtonAttribute()
		{
			Buttons = new InspectorButtonInfo[0];
			Position = eInspectorButtonPosition.BOTTOM;
		}

		public InspectorButtonAttribute(eInspectorButtonPosition _position, string _buttonName, string _toolTip, string _invokeMethod)
		{
			Buttons = new InspectorButtonInfo[1]
			{
				new InspectorButtonInfo(_buttonName, _toolTip, _invokeMethod)
			};
			Position = _position;
		}

		public InspectorButtonAttribute(eInspectorButtonPosition _position, params string[] _buttonInfoList)
		{
			int num = _buttonInfoList.Length;
			InspectorButtonInfo[] array = new InspectorButtonInfo[num];
			for (int i = 0; i < num; i++)
			{
				string[] array2 = _buttonInfoList[i].Split(';');
				array[i] = new InspectorButtonInfo(array2[0], array2[1], array2[2]);
			}
			Buttons = array;
			Position = _position;
		}
	}
}
