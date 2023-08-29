using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace I2.Loc
{
	public static class PersistentStorage
	{
		public static bool Save(string fileName, string data)
		{
			try
			{
				string path = Application.persistentDataPath + "/" + fileName + ".loc";
				File.WriteAllText(path, data, Encoding.UTF8);
				return true;
				IL_0029:
				bool result;
				return result;
			}
			catch (Exception)
			{
				UnityEngine.Debug.LogError("Error saving file " + fileName);
				return false;
				IL_0046:
				bool result;
				return result;
			}
		}

		public static string Load(string fileName)
		{
			try
			{
				string path = Application.persistentDataPath + "/" + fileName + ".loc";
				return File.ReadAllText(path, Encoding.UTF8);
				IL_0027:
				string result;
				return result;
			}
			catch (Exception)
			{
				return null;
				IL_0034:
				string result;
				return result;
			}
		}

		public static void Delete(string fileName)
		{
			try
			{
				string path = Application.persistentDataPath + "/" + fileName + ".loc";
				File.Delete(path);
			}
			catch (Exception)
			{
			}
		}

		public static bool SaveFile(string fileName, string data)
		{
			try
			{
				File.WriteAllText(fileName, data, Encoding.UTF8);
				return true;
				IL_0013:
				bool result;
				return result;
			}
			catch (Exception)
			{
				UnityEngine.Debug.LogError("Error saving file " + fileName);
				return false;
				IL_0030:
				bool result;
				return result;
			}
		}

		public static string LoadFile(string fileName)
		{
			try
			{
				return File.ReadAllText(fileName, Encoding.UTF8);
				IL_0011:
				string result;
				return result;
			}
			catch (Exception)
			{
				return null;
				IL_001e:
				string result;
				return result;
			}
		}

		public static void DeleteFile(string fileName)
		{
			try
			{
				File.Delete(fileName);
			}
			catch (Exception)
			{
			}
		}

		public static bool HasFile(string fileName)
		{
			return File.Exists(fileName);
		}
	}
}
