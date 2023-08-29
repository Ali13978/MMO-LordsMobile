using BackendlessAPI.LitJson;

namespace BackendlessAPI.Property
{
	public abstract class AbstractProperty
	{
		[JsonProperty("name")]
		public string Name
		{
			get;
			set;
		}

		[JsonProperty("required")]
		public bool IsRequired
		{
			get;
			set;
		}

		[JsonProperty("selected")]
		public bool IsSelected
		{
			get;
			set;
		}

		[JsonProperty("type")]
		public DateTypeEnum Type
		{
			get;
			set;
		}

		[JsonProperty("defaultValue")]
		public object DefaultValue
		{
			get;
			set;
		}
	}
}
