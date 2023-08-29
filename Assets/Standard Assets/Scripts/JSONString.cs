namespace VoxelBusters.Utility.Internal
{
	public struct JSONString
	{
		public string Value
		{
			get;
			private set;
		}

		public bool IsNullOrEmpty
		{
			get;
			private set;
		}

		public int Length
		{
			get;
			private set;
		}

		public char this[int _index] => Value[_index];

		public JSONString(string _JSONString)
		{
			Value = _JSONString;
			IsNullOrEmpty = string.IsNullOrEmpty(_JSONString);
			Length = ((!IsNullOrEmpty) ? _JSONString.Length : 0);
		}
	}
}
