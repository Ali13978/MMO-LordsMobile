using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public class Plist : Dictionary<string, object>
	{
		private Plist()
		{
		}

		public static Plist LoadPlistAtPath(string _filePath)
		{
			if (!File.Exists(_filePath))
			{
				UnityEngine.Debug.LogError("[Plist] Load failed as file doesnt exist, Path=" + _filePath);
				return null;
			}
			string text = File.ReadAllText(_filePath);
			Plist plist = new Plist();
			plist.ParsePlistText(text);
			return plist;
		}

		public static Plist Load(string _plistTextContents)
		{
			Plist plist = new Plist();
			plist.ParsePlistText(_plistTextContents);
			return plist;
		}

		private void ParsePlistText(string _text)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(_text);
			XmlNode lastChild = xmlDocument.LastChild;
			XmlNode firstChild = lastChild.FirstChild;
			IDictionary _parsedDict = this;
			if (firstChild != null)
			{
				ParseDictionaryNode(firstChild, ref _parsedDict);
			}
		}

		private object ParseValueNode(XmlNode _valNode)
		{
			switch (_valNode.Name)
			{
			case "true":
				return true;
			case "false":
				return false;
			case "integer":
				return int.Parse(_valNode.InnerText);
			case "real":
				return float.Parse(_valNode.InnerText);
			case "string":
				return _valNode.InnerText;
			case "dict":
			{
				IDictionary _parsedDict = new Dictionary<string, object>();
				ParseDictionaryNode(_valNode, ref _parsedDict);
				return _parsedDict;
			}
			case "array":
			{
				IList _parsedList = new List<object>();
				ParseListNode(_valNode, ref _parsedList);
				return _parsedList;
			}
			default:
				return null;
			}
		}

		private void ParseListNode(XmlNode _listNode, ref IList _parsedList)
		{
			foreach (XmlNode childNode in _listNode.ChildNodes)
			{
				_parsedList.Add(ParseValueNode(childNode));
			}
		}

		private void ParseDictionaryNode(XmlNode _dictNode, ref IDictionary _parsedDict)
		{
			int count = _dictNode.ChildNodes.Count;
			for (int i = 0; i < count; i += 2)
			{
				XmlNode xmlNode = _dictNode.ChildNodes.Item(i);
				XmlNode valNode = _dictNode.ChildNodes.Item(i + 1);
				_parsedDict[xmlNode.InnerText] = ParseValueNode(valNode);
			}
		}

		public object GetKeyPathValue(string _keyPath)
		{
			if (string.IsNullOrEmpty(_keyPath))
			{
				return this;
			}
			object obj = this;
			string[] array = _keyPath.Split('/');
			int num = array.Length;
			try
			{
				for (int i = 0; i < num; i++)
				{
					string key = array[i];
					IDictionary dictionary = obj as IDictionary;
					if (dictionary == null || !dictionary.Contains(key))
					{
						throw new KeyNotFoundException();
					}
					obj = dictionary[key];
				}
				return obj;
				IL_0071:
				object result;
				return result;
			}
			catch (KeyNotFoundException ex)
			{
				UnityEngine.Debug.LogWarning("[Plist] " + ex.Message + " KeyPath : " + _keyPath);
				return null;
				IL_009c:
				object result;
				return result;
			}
		}

		public void AddValue(string _keyPath, object _newValue)
		{
			if (_keyPath == null)
			{
				_keyPath = string.Empty;
			}
			string[] array = _keyPath.Split('/');
			string text = array[array.Length - 1];
			string keyPath = _keyPath.Substring(0, _keyPath.Length - text.Length).TrimEnd('/');
			IDictionary dictionary = GetKeyPathValue(keyPath) as IDictionary;
			if (dictionary != null)
			{
				dictionary[text] = _newValue;
			}
		}

		public void Save(string _saveToPath)
		{
			using (StreamWriter streamWriter = new StreamWriter(_saveToPath))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
					xmlWriterSettings.Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
					xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
					xmlWriterSettings.Indent = true;
					using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
					{
						xmlWriter.WriteStartDocument();
						xmlWriter.WriteDocType("plist", "-//Apple Computer//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/Plist-1.0.dtd", null);
						xmlWriter.WriteStartElement("plist");
						xmlWriter.WriteAttributeString("version", "1.0");
						WriteXMLDictionaryNode(xmlWriter, this);
						xmlWriter.WriteEndElement();
						xmlWriter.WriteEndDocument();
						xmlWriter.Flush();
						xmlWriter.Close();
						string @string = Encoding.UTF8.GetString(memoryStream.ToArray());
						streamWriter.Write(@string);
					}
				}
			}
		}

		private void WriteXMLNode(XmlWriter _xmlWriter, object _value)
		{
			if (_value is bool)
			{
				WriteXMLBoolNode(_xmlWriter, (bool)_value);
			}
			else if (_value is int || _value is long)
			{
				WriteXMLIntegerNode(_xmlWriter, (int)_value);
			}
			else if (_value is string)
			{
				WriteXMLStringNode(_xmlWriter, _value as string);
			}
			else if (_value is IList)
			{
				WriteXMLListNode(_xmlWriter, _value as IList);
			}
			else if (_value is IDictionary)
			{
				WriteXMLDictionaryNode(_xmlWriter, _value as IDictionary);
			}
		}

		private void WriteXMLBoolNode(XmlWriter _xmlWriter, bool _value)
		{
			_xmlWriter.WriteElementString(_value.ToString().ToLower(), string.Empty);
		}

		private void WriteXMLIntegerNode(XmlWriter _xmlWriter, int _value)
		{
			_xmlWriter.WriteElementString("integer", _value.ToString(NumberFormatInfo.InvariantInfo));
		}

		private void WriteXMLStringNode(XmlWriter _xmlWriter, string _value)
		{
			_xmlWriter.WriteElementString("string", _value);
		}

		private void WriteXMLListNode(XmlWriter _xmlWriter, IList _listValue)
		{
			if (_listValue == null)
			{
				_listValue = new List<object>();
			}
			_xmlWriter.WriteStartElement("array");
			foreach (object item in _listValue)
			{
				WriteXMLNode(_xmlWriter, item);
			}
			_xmlWriter.WriteEndElement();
		}

		private void WriteXMLDictionaryNode(XmlWriter _xmlWriter, IDictionary _dictValue)
		{
			if (_dictValue == null)
			{
				_dictValue = new Dictionary<string, object>();
			}
			_xmlWriter.WriteStartElement("dict");
			foreach (object key in _dictValue.Keys)
			{
				_xmlWriter.WriteElementString("key", key.ToString());
				WriteXMLNode(_xmlWriter, _dictValue[key]);
			}
			_xmlWriter.WriteEndElement();
		}
	}
}
