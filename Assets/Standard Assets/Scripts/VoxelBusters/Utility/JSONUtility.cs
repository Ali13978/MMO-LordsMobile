namespace VoxelBusters.Utility
{
	public static class JSONUtility
	{
		public static string ToJSON(object _object)
		{
			JSONWriter jSONWriter = new JSONWriter();
			return jSONWriter.Serialise(_object);
		}

		public static bool IsNull(string _jsonStr)
		{
			return _jsonStr.Equals("null");
		}

		public static object FromJSON(string _inputJSONString)
		{
			JSONReader jSONReader = new JSONReader(_inputJSONString);
			return jSONReader.Deserialise();
		}

		public static object FromJSON(string _inputJSONString, ref int _errorIndex)
		{
			JSONReader jSONReader = new JSONReader(_inputJSONString);
			return jSONReader.Deserialise(ref _errorIndex);
		}
	}
}
