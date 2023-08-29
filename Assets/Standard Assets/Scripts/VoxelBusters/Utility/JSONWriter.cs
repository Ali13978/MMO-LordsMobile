using System;
using System.Collections;
using System.Text;

namespace VoxelBusters.Utility
{
	public class JSONWriter
	{
		private const int kBufferLength = 512;

		internal StringBuilder StringBuilder
		{
			get;
			private set;
		}

		public JSONWriter(int _bufferLength = 512)
		{
			StringBuilder = new StringBuilder(_bufferLength);
		}

		public string Serialise(object _objectValue)
		{
			WriteObjectValue(_objectValue);
			return StringBuilder.ToString();
		}

		public void WriteObjectValue(object _objectVal)
		{
			if (_objectVal == null)
			{
				WriteNullValue();
				return;
			}
			Type type = _objectVal.GetType();
			if (type.IsPrimitive)
			{
				WritePrimitive(_objectVal);
			}
			else if (type.IsEnum)
			{
				WriteEnum(_objectVal);
			}
			else if (type.IsArray)
			{
				WriteArray(_objectVal as Array);
			}
			else if (_objectVal is IList)
			{
				WriteList(_objectVal as IList);
			}
			else if (_objectVal is IDictionary)
			{
				WriteDictionary(_objectVal as IDictionary);
			}
			else
			{
				WriteString(_objectVal.ToString());
			}
		}

		public void WriteDictionary(IDictionary _dict)
		{
			bool flag = true;
			IDictionaryEnumerator enumerator = _dict.GetEnumerator();
			StringBuilder.Append('{');
			while (enumerator.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					StringBuilder.Append(',');
				}
				WriteString(enumerator.Key.ToString());
				StringBuilder.Append(':');
				WriteObjectValue(enumerator.Value);
			}
			StringBuilder.Append('}');
		}

		public void WriteArray(Array _array)
		{
			StringBuilder.Append('[');
			switch (_array.Rank)
			{
			case 1:
			{
				int length3 = _array.Length;
				for (int k = 0; k < length3; k++)
				{
					if (k != 0)
					{
						StringBuilder.Append(',');
					}
					WriteObjectValue(_array.GetValue(k));
				}
				break;
			}
			case 2:
			{
				int length = _array.GetLength(0);
				int length2 = _array.GetLength(1);
				for (int i = 0; i < length; i++)
				{
					if (i != 0)
					{
						StringBuilder.Append(',');
					}
					StringBuilder.Append('[');
					for (int j = 0; j < length2; j++)
					{
						if (j != 0)
						{
							StringBuilder.Append(',');
						}
						WriteObjectValue(_array.GetValue(i, j));
					}
					StringBuilder.Append(']');
				}
				break;
			}
			}
			StringBuilder.Append(']');
		}

		public void WriteList(IList _list)
		{
			int count = _list.Count;
			StringBuilder.Append('[');
			for (int i = 0; i < count; i++)
			{
				if (i != 0)
				{
					StringBuilder.Append(',');
				}
				WriteObjectValue(_list[i]);
			}
			StringBuilder.Append(']');
		}

		public void WritePrimitive(object _value)
		{
			if (_value is bool)
			{
				if ((bool)_value)
				{
					StringBuilder.Append("true");
				}
				else
				{
					StringBuilder.Append("false");
				}
			}
			else if (_value is char)
			{
				StringBuilder.Append('"').Append((char)_value).Append('"');
			}
			else
			{
				StringBuilder.Append(_value);
			}
		}

		public void WriteEnum(object _value)
		{
			StringBuilder.Append((int)_value);
		}

		public void WriteNullValue()
		{
			StringBuilder.Append("null");
		}

		public void WriteString(string _stringVal)
		{
			int length = _stringVal.Length;
			int num = 0;
			StringBuilder.Append('"');
			while (num < length)
			{
				char c = _stringVal[num++];
				if (c == '"')
				{
					StringBuilder.Append('\\').Append('"');
				}
				else if (c == '\\')
				{
					StringBuilder.Append('\\').Append('\\');
				}
				else if (c == '/')
				{
					StringBuilder.Append('\\').Append('/');
				}
				else if (c == '\b')
				{
					StringBuilder.Append('\\').Append('b');
				}
				else if (c == '\f')
				{
					StringBuilder.Append('\\').Append('f');
				}
				else if (c == '\n')
				{
					StringBuilder.Append('\\').Append('n');
				}
				else if (c == '\r')
				{
					StringBuilder.Append('\\').Append('r');
				}
				else if (c == '\t')
				{
					StringBuilder.Append('\\').Append('t');
				}
				else if (c > '\u007f')
				{
					int num3 = c;
					string value = "\\u" + num3.ToString("x4");
					StringBuilder.Append(value);
				}
				else
				{
					StringBuilder.Append(c);
				}
			}
			StringBuilder.Append('"');
		}

		public void WriteDictionaryStart()
		{
			StringBuilder.Append('{');
		}

		public void WriteKeyValuePair(string _key, object _value, bool _appendSeperator = false)
		{
			WriteString(_key);
			StringBuilder.Append(':');
			WriteObjectValue(_value);
			if (_appendSeperator)
			{
				StringBuilder.Append(',');
			}
		}

		public void WriteKeyValuePairSeperator()
		{
			StringBuilder.Append(':');
		}

		public void WriteDictionaryEnd()
		{
			StringBuilder.Append('}');
		}

		public void WriteArrayStart()
		{
			StringBuilder.Append('[');
		}

		public void WriteArrayEnd()
		{
			StringBuilder.Append(']');
		}

		public void WriteElementSeperator()
		{
			StringBuilder.Append(',');
		}
	}
}
