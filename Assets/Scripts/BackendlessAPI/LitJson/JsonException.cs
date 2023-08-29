using System;

namespace BackendlessAPI.LitJson
{
	public class JsonException : ApplicationException
	{
		public JsonException()
		{
		}

		internal JsonException(ParserToken token)
			: base($"Invalid token '{token}' in input string")
		{
		}

		internal JsonException(ParserToken token, System.Exception inner_exception)
			: base($"Invalid token '{token}' in input string", inner_exception)
		{
		}

		internal JsonException(int c)
			: base($"Invalid character '{(char)c}' in input string")
		{
		}

		internal JsonException(int c, System.Exception inner_exception)
			: base($"Invalid character '{(char)c}' in input string", inner_exception)
		{
		}

		public JsonException(string message)
			: base(message)
		{
		}

		public JsonException(string message, System.Exception inner_exception)
			: base(message, inner_exception)
		{
		}
	}
}
