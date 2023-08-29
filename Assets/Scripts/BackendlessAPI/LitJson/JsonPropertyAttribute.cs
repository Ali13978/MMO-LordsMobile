using System;

namespace BackendlessAPI.LitJson
{
	public sealed class JsonPropertyAttribute : Attribute
	{
		public string PropertyName
		{
			get;
			set;
		}

		public JsonPropertyAttribute(string propertyName)
		{
			PropertyName = propertyName;
		}
	}
}
