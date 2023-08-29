namespace VoxelBusters.Utility
{
	public class InspectorButtonInfo
	{
		public string Name
		{
			get;
			private set;
		}

		public string ToolTip
		{
			get;
			private set;
		}

		public string InvokeMethod
		{
			get;
			private set;
		}

		private InspectorButtonInfo()
		{
		}

		public InspectorButtonInfo(string _buttonName, string _toolTip, string _invokeMethod)
		{
			Name = _buttonName;
			ToolTip = _toolTip;
			InvokeMethod = _invokeMethod;
		}
	}
}
