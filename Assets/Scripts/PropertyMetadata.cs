using System;
using System.Reflection;

namespace BackendlessAPI.LitJson
{
	internal struct PropertyMetadata
	{
		public MemberInfo Info;

		public bool IsField;

		public Type Type;
	}
}
