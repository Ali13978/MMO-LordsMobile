using System;
using System.IO;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public static class IOExtensions
	{
		public static void Destroy(string _path)
		{
			FileAttributes attributes = File.GetAttributes(_path);
			if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
			{
				Directory.Delete(_path, recursive: true);
			}
			else
			{
				File.Delete(_path);
			}
		}

		public static string MakeRelativePath(this string _fromPath, string _toPath)
		{
			Uri fromUri = null;
			if (_fromPath != null)
			{
				fromUri = new Uri(_fromPath);
			}
			return fromUri.MakeRelativePath(_toPath);
		}

		public static string MakeRelativePath(this Uri _fromUri, string _toPath)
		{
			if (_fromUri == null)
			{
				throw new ArgumentNullException("_fromUri");
			}
			if (_toPath == null)
			{
				throw new ArgumentNullException("_toPath");
			}
			Uri uri = new Uri(_toPath);
			if (_fromUri.Scheme != uri.Scheme)
			{
				return _toPath;
			}
			Uri uri2 = _fromUri.MakeRelativeUri(uri);
			string text = Uri.UnescapeDataString(uri2.ToString());
			if (uri.Scheme.Equals("file"))
			{
				text = text.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			}
			return text;
		}

		public static bool AssignPermissionRecursively(string _directoryPath, FileAttributes _attribute)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(_directoryPath);
			if (!directoryInfo.Exists)
			{
				UnityEngine.Debug.LogWarning("[IOExtensions] The operation could not be completed because directory doesn't exist.");
				return false;
			}
			return directoryInfo.AssignPermissionRecursively(_attribute);
		}

		public static bool AssignPermissionRecursively(this DirectoryInfo _directoryInfo, FileAttributes _attribute)
		{
			_directoryInfo.Attributes |= _attribute;
			FileInfo[] files = _directoryInfo.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				fileInfo.Attributes |= _attribute;
			}
			DirectoryInfo[] directories = _directoryInfo.GetDirectories();
			foreach (DirectoryInfo directoryInfo in directories)
			{
				directoryInfo.AssignPermissionRecursively(_attribute);
			}
			return true;
		}

		public static void CopyFilesRecursively(string _sourceDirectory, string _destinationDirectory, bool _excludeMetaFiles = true, bool _deleteDestinationFolderIfExists = true)
		{
			DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(_sourceDirectory);
			DirectoryInfo destinationDirectoryInfo = new DirectoryInfo(_destinationDirectory);
			CopyFilesRecursively(sourceDirectoryInfo, destinationDirectoryInfo, _excludeMetaFiles, _deleteDestinationFolderIfExists);
		}

		public static void CopyFilesRecursively(DirectoryInfo _sourceDirectoryInfo, DirectoryInfo _destinationDirectoryInfo, bool _excludeMetaFiles = true, bool _deleteDestinationFolderIfExists = true)
		{
			if (!_sourceDirectoryInfo.Exists)
			{
				throw new DirectoryNotFoundException($"The operation could not be completed because directory does not exist. Path: {_sourceDirectoryInfo.FullName}.");
			}
			if (_deleteDestinationFolderIfExists && _destinationDirectoryInfo.Exists)
			{
				_destinationDirectoryInfo.Delete(recursive: true);
			}
			_destinationDirectoryInfo.Create();
			FileInfo[] files = _sourceDirectoryInfo.GetFiles();
			if (_excludeMetaFiles)
			{
				FileInfo[] array = files;
				foreach (FileInfo fileInfo in array)
				{
					if (!(fileInfo.Extension == ".meta"))
					{
						CopyFile(fileInfo, Path.Combine(_destinationDirectoryInfo.FullName, fileInfo.Name));
					}
				}
			}
			else
			{
				FileInfo[] array2 = files;
				foreach (FileInfo fileInfo2 in array2)
				{
					CopyFile(fileInfo2, Path.Combine(_destinationDirectoryInfo.FullName, fileInfo2.Name));
				}
			}
			DirectoryInfo[] directories = _sourceDirectoryInfo.GetDirectories();
			DirectoryInfo[] array3 = directories;
			foreach (DirectoryInfo directoryInfo in array3)
			{
				CopyFilesRecursively(directoryInfo, new DirectoryInfo(Path.Combine(_destinationDirectoryInfo.FullName, directoryInfo.Name)), _excludeMetaFiles);
			}
		}

		public static void CopyFile(string _sourceFilePath, string _destinationFilePath, bool _overwrite = true)
		{
			CopyFile(new FileInfo(_sourceFilePath), _destinationFilePath, _overwrite);
		}

		public static void CopyFile(FileInfo _sourceFileInfo, string _destinationFilePath, bool _overwrite = true)
		{
			if (!_sourceFileInfo.Exists)
			{
				UnityEngine.Debug.LogWarning("[IOExtensions] The operation could not be completed because file doesn't exist.");
				return;
			}
			FileAttributes attributes = _sourceFileInfo.Attributes;
			_sourceFileInfo.Attributes = FileAttributes.Normal;
			_sourceFileInfo.CopyTo(_destinationFilePath, _overwrite);
			_sourceFileInfo.Attributes = attributes;
		}
	}
}
