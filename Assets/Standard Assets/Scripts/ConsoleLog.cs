using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace VoxelBusters.DebugPRO.Internal
{
	[Serializable]
	public struct ConsoleLog
	{
		[SerializeField]
		private int m_ID;

		[SerializeField]
		private int m_tagID;

		[SerializeField]
		private string m_message;

		[SerializeField]
		private eConsoleLogType m_type;

		[SerializeField]
		private string m_callStack;

		[SerializeField]
		private string m_description;

		[SerializeField]
		private string m_callerFileName;

		[SerializeField]
		private int m_callerFileLineNumber;

		public int ID
		{
			get
			{
				return m_ID;
			}
			private set
			{
				m_ID = value;
			}
		}

		public int TagID
		{
			get
			{
				return m_tagID;
			}
			private set
			{
				m_tagID = value;
			}
		}

		public string Message
		{
			get
			{
				return m_message;
			}
			private set
			{
				m_message = value;
			}
		}

		public eConsoleLogType Type
		{
			get
			{
				return m_type;
			}
			private set
			{
				m_type = value;
			}
		}

		public UnityEngine.Object Context
		{
			get;
			private set;
		}

		public string CallStack
		{
			get
			{
				return m_callStack;
			}
			private set
			{
				m_callStack = value;
			}
		}

		public string Description
		{
			get
			{
				return m_description;
			}
			private set
			{
				m_description = value;
			}
		}

		//public ConsoleLog(int _logID, int _tagID, string _message, eConsoleLogType _type, UnityEngine.Object _context)
		//{
  //          CallStack = "";
  //          Description = "";
  //          ID = _logID;
  //          TagID = _tagID;
  //          Message = _message;
  //          Type = _type;
  //          Context = _context;
  //          ExtractStackTraceDescription();
  //      }

        public void SetValuesConsoleLog(int _logID, int _tagID, string _message, eConsoleLogType _type, UnityEngine.Object _context)
        {
            ID = _logID;
            TagID = _tagID;
            Message = _message;
            Type = _type;
            Context = _context;
            ExtractStackTraceDescription();
        }

        private void ExtractStackTraceDescription()
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				StackTrace stackTrace = new StackTrace(fNeedFileInfo: true);
				int frameCount = stackTrace.FrameCount;
				string fullPath = Path.GetFullPath(Application.dataPath + "/../");
				int num = 0;
				for (num = 0; num < frameCount; num++)
				{
					StackFrame frame = stackTrace.GetFrame(num);
					MethodBase method = frame.GetMethod();
					string fullName = method.DeclaringType.FullName;
					if (!IsInternalCall(fullName))
					{
						break;
					}
				}
				int num2 = num;
				for (; num < frameCount; num++)
				{
					StackFrame frame2 = stackTrace.GetFrame(num);
					MethodBase method2 = frame2.GetMethod();
					string name = method2.DeclaringType.Name;
					stringBuilder.AppendFormat("{0}:{1}", name, method2.ToString());
					string fileName = frame2.GetFileName();
					if (fileName != null)
					{
						string arg = fileName.Substring(fullPath.Length);
						stringBuilder.AppendFormat("(at {0}:{1})\n", arg, frame2.GetFileLineNumber());
					}
				}
				CallStack = stringBuilder.ToString().TrimEnd('\n');
				Description = $"{Message}\n{CallStack}";
				if (num2 < frameCount)
				{
					StackFrame frame3 = stackTrace.GetFrame(num2);
					m_callerFileName = frame3.GetFileName();
					m_callerFileLineNumber = frame3.GetFileLineNumber();
				}
				else
				{
					m_callerFileName = null;
					m_callerFileLineNumber = 0;
				}
			}
			catch
			{
				Description = Message;
				CallStack = string.Empty;
				m_callerFileName = null;
				m_callerFileLineNumber = 0;
			}
		}

		private static bool IsInternalCall(string _name)
		{
			return _name.StartsWith("UnityEditor.") || _name.StartsWith("UnityEngine.") || _name.StartsWith("System.") || _name.StartsWith("UnityScript.Lang.") || _name.StartsWith("Boo.Lang.") || _name.StartsWith("VoxelBusters.DebugPRO");
		}

		public bool IsValid()
		{
			return ID > 0;
		}

		public bool Equals(ConsoleLog _log)
		{
			return ID == _log.ID;
		}

		public void OnSelect()
		{
		}

		public void OnPress()
		{
		}
	}
}
