using System.Collections;

namespace VoxelBusters.Utility
{
	public static class JSONParserExtensions
	{
		public static string ToJSON(this IDictionary _dictionary)
		{
			string text = JSONUtility.ToJSON(_dictionary);
			return (!JSONUtility.IsNull(text)) ? text : null;
		}

		public static string ToJSON(this IList _list)
		{
			string text = JSONUtility.ToJSON(_list);
			return (!JSONUtility.IsNull(text)) ? text : null;
		}
	}
}
