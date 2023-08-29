using BackendlessAPI.LitJson;

namespace BackendlessAPI.Property
{
	public class UserProperty : AbstractProperty
	{
		[JsonProperty("identity")]
		public bool IsIdentity
		{
			get;
			set;
		}
	}
}
