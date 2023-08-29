using BackendlessAPI.LitJson;

namespace BackendlessAPI.Property
{
	public class ObjectProperty : AbstractProperty
	{
		[JsonProperty("relatedTable")]
		public string RelatedTable
		{
			get;
			set;
		}

		[JsonProperty("customRegex")]
		public string CustomRegex
		{
			get;
			set;
		}

		[JsonProperty("primaryKey")]
		public bool PrimaryKey
		{
			get;
			set;
		}

		public ObjectProperty()
		{
		}

		public ObjectProperty(string name)
		{
			base.Name = name;
		}

		public ObjectProperty(string name, DateTypeEnum type, bool required)
		{
			base.Name = name;
			base.Type = type;
			base.IsRequired = required;
		}
	}
}
