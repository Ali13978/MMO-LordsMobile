using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using VoxelBusters.Utility.Internal;

namespace VoxelBusters.Utility
{
	public class JSONReader
	{
		public JSONString JSONString
		{
			get;
			private set;
		}

		private JSONReader()
		{
		}

		public JSONReader(string _inputJSONString)
		{
			JSONString = new JSONString(_inputJSONString);
		}

		public object Deserialise()
		{
			if (JSONString.IsNullOrEmpty)
			{
				return null;
			}
			int _index = 0;
			return ReadValue(ref _index);
		}

		public object Deserialise(ref int _errorIndex)
		{
			if (JSONString.IsNullOrEmpty)
			{
				return null;
			}
			int _index = 0;
			object result = ReadValue(ref _index);
			if (_index != JSONString.Length)
			{
				_errorIndex = _index;
			}
			else
			{
				_errorIndex = -1;
			}
			return result;
		}

		public object ReadValue(ref int _index)
		{
			RemoveWhiteSpace(ref _index);
			switch (LookAhead(_index))
			{
			case eJSONToken.CURLY_OPEN:
				return ReadObject(ref _index);
			case eJSONToken.SQUARED_OPEN:
				return ReadArray(ref _index);
			case eJSONToken.STRING:
				return ReadString(ref _index);
			case eJSONToken.NUMBER:
				return ReadNumber(ref _index);
			case eJSONToken.NULL:
				_index += 4;
				return null;
			case eJSONToken.TRUE:
				_index += 4;
				return true;
			case eJSONToken.FALSE:
				_index += 5;
				return false;
			default:
				UnityEngine.Debug.LogError($"[JSON] Parse error at index ={_index}");
				return null;
			}
		}

		public object ReadObject(ref int _index)
		{
			IDictionary dictionary = new Dictionary<string, object>();
			bool flag = false;
			_index++;
			while (!flag)
			{
				switch (LookAhead(_index))
				{
				case eJSONToken.NONE:
					UnityEngine.Debug.LogError($"[JSON] Parse error at index ={_index}");
					return null;
				case eJSONToken.CURLY_CLOSE:
					NextToken(ref _index);
					flag = true;
					continue;
				}
				string _key;
				object _value;
				int num = ReadKeyValuePair(ref _index, out _key, out _value);
				if (num != -1)
				{
					dictionary[_key] = _value;
					switch (LookAhead(_index))
					{
					case eJSONToken.COMMA:
						NextToken(ref _index);
						break;
					case eJSONToken.CURLY_CLOSE:
						NextToken(ref _index);
						flag = true;
						break;
					default:
						UnityEngine.Debug.LogError($"[JSON] Parse error at index ={_index}");
						return null;
					}
				}
			}
			return dictionary;
		}

		public int ReadKeyValuePair(ref int _index, out string _key, out object _value)
		{
			_key = null;
			_value = null;
			_key = (ReadValue(ref _index) as string);
			if (_key == null)
			{
				UnityEngine.Debug.LogError($"[JSON] Parse error at index ={_index}");
				return -1;
			}
			if (NextToken(ref _index) != eJSONToken.COLON)
			{
				UnityEngine.Debug.LogError($"[JSON] Parse error at index ={_index}");
				return -1;
			}
			_value = ReadValue(ref _index);
			return 0;
		}

		public object ReadArray(ref int _index)
		{
			IList list = new List<object>();
			bool flag = false;
			_index++;
			while (!flag)
			{
				switch (LookAhead(_index))
				{
				case eJSONToken.NONE:
					UnityEngine.Debug.LogError($"[JSON] Parse error at index ={_index}");
					return null;
				case eJSONToken.SQUARED_CLOSE:
					NextToken(ref _index);
					flag = true;
					continue;
				}
				ReadArrayElement(ref _index, out object _element);
				list.Add(_element);
				switch (LookAhead(_index))
				{
				case eJSONToken.COMMA:
					NextToken(ref _index);
					break;
				case eJSONToken.SQUARED_CLOSE:
					NextToken(ref _index);
					flag = true;
					break;
				default:
					UnityEngine.Debug.LogError($"[JSON] Parse error at index ={_index}");
					return null;
				}
			}
			return list;
		}

		public void ReadArrayElement(ref int _index, out object _element)
		{
			_element = ReadValue(ref _index);
		}

		public string ReadString(ref int _index)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			_index++;
			while (!flag && _index != JSONString.Length)
			{
				char c = JSONString[_index++];
				switch (c)
				{
				case '"':
					flag = true;
					continue;
				case '\\':
					break;
				default:
					stringBuilder.Append(c);
					continue;
				}
				if (_index == JSONString.Length)
				{
					break;
				}
				switch (JSONString[_index++])
				{
				case '"':
					stringBuilder.Append('"');
					continue;
				case '\\':
					stringBuilder.Append('\\');
					continue;
				case '/':
					stringBuilder.Append('/');
					continue;
				case 'b':
					stringBuilder.Append('\b');
					continue;
				case 'f':
					stringBuilder.Append('\f');
					continue;
				case 'n':
					stringBuilder.Append('\n');
					continue;
				case 'r':
					stringBuilder.Append('\r');
					continue;
				case 't':
					stringBuilder.Append('\t');
					continue;
				case 'u':
					break;
				default:
					continue;
				}
				int num = JSONString.Length - _index;
				if (num >= 4)
				{
					string s = JSONString.Value.Substring(_index, 4);
					char value = (char)int.Parse(s, NumberStyles.HexNumber);
					stringBuilder.Append(value);
					_index += 4;
					continue;
				}
				break;
			}
			if (!flag)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		public object ReadNumber(ref int _index)
		{
			int num = _index;
			bool flag = false;
			while (!flag)
			{
				if ("0123456789+-.eE".IndexOf(JSONString[num]) != -1)
				{
					num++;
					if (num >= JSONString.Length)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			int length = num - _index;
			string s = JSONString.Value.Substring(_index, length);
			_index = num;
			if (long.TryParse(s, out long result))
			{
				return result;
			}
			if (double.TryParse(s, out double result2))
			{
				return result2;
			}
			return null;
		}

		public eJSONToken LookAhead(int _index)
		{
			int _index2 = _index;
			return NextToken(ref _index2);
		}

		public eJSONToken NextToken(ref int _index)
		{
			if (_index == JSONString.Length)
			{
				return eJSONToken.NONE;
			}
			RemoveWhiteSpace(ref _index);
			switch (JSONString[_index++])
			{
			case '{':
				return eJSONToken.CURLY_OPEN;
			case '}':
				return eJSONToken.CURLY_CLOSE;
			case '[':
				return eJSONToken.SQUARED_OPEN;
			case ']':
				return eJSONToken.SQUARED_CLOSE;
			case ':':
				return eJSONToken.COLON;
			case ',':
				return eJSONToken.COMMA;
			case '"':
				return eJSONToken.STRING;
			case '-':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return eJSONToken.NUMBER;
			default:
				_index--;
				if (_index + 4 < JSONString.Length && JSONString[_index] == 'n' && JSONString[_index + 1] == 'u' && JSONString[_index + 2] == 'l' && JSONString[_index + 3] == 'l')
				{
					_index += 4;
					return eJSONToken.NULL;
				}
				if (_index + 4 < JSONString.Length && JSONString[_index] == 't' && JSONString[_index + 1] == 'r' && JSONString[_index + 2] == 'u' && JSONString[_index + 3] == 'e')
				{
					_index += 4;
					return eJSONToken.TRUE;
				}
				if (_index + 5 < JSONString.Length && JSONString[_index] == 'f' && JSONString[_index + 1] == 'a' && JSONString[_index + 2] == 'l' && JSONString[_index + 3] == 's' && JSONString[_index + 4] == 'e')
				{
					_index += 5;
					return eJSONToken.FALSE;
				}
				return eJSONToken.NONE;
			}
		}

		private void RemoveWhiteSpace(ref int _index)
		{
			int length = JSONString.Length;
			while (_index < length)
			{
				char value = JSONString[_index];
				if (" \n\t\r".IndexOf(value) != -1)
				{
					_index++;
					continue;
				}
				break;
			}
		}
	}
}
