using System.IO;

namespace VoxelBusters.Utility
{
	public class FileOperations
	{
		public static void Delete(string _filePath)
		{
			File.Delete(_filePath);
		}

		public static void Move(string _sourcePath, string _destinationPath)
		{
			File.Move(_sourcePath, _destinationPath);
		}

		public static bool Exists(string _filePath)
		{
			return File.Exists(_filePath);
		}

		public static byte[] ReadAllBytes(string _filePath)
		{
			return File.ReadAllBytes(_filePath);
		}

		public static void WriteAllBytes(string _filePath, byte[] _bytes)
		{
			File.WriteAllBytes(_filePath, _bytes);
		}

		public static StreamWriter CreateText(string _filePath)
		{
			return File.CreateText(_filePath);
		}

		public static string ReadAllText(string _filePath)
		{
			return File.ReadAllText(_filePath);
		}

		public static void WriteAllText(string _filePath, string _contents)
		{
			File.WriteAllText(_filePath, _contents);
		}

		public static void Rename(string _filePath, string _newFileName)
		{
			string fileName = Path.GetFileName(_filePath);
			string text = _filePath.Replace(fileName, _newFileName);
			if (File.Exists(_filePath))
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Move(_filePath, text);
			}
		}
	}
}
