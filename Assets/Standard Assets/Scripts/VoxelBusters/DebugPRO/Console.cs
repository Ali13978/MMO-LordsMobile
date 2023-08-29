using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using VoxelBusters.DebugPRO.Internal;

namespace VoxelBusters.DebugPRO
{
	public class Console : ScriptableObject
	{
		private const string kDefaultTag = "untagged";

		private const string kInstanceID = "vb-debug-pro-console-id";

		[SerializeField]
		private List<ConsoleLog> m_consoleLogsList = new List<ConsoleLog>();

		private List<ConsoleLog> m_displayableConsoleLogsList = new List<ConsoleLog>();

		[SerializeField]
		private List<ConsoleTag> m_consoleTags = new List<ConsoleTag>();

		[SerializeField]
		private bool m_clearOnPlay = true;

		[SerializeField]
		private bool m_errorPause;

		[SerializeField]
		private eConsoleLogType m_allowedLogTypes = (eConsoleLogType)255;

		[SerializeField]
		private bool m_showInfoLogs = true;

		[SerializeField]
		private bool m_showWarningLogs = true;

		[SerializeField]
		private bool m_showErrorLogs = true;

		private string m_infoLogsCounterStr;

		private int m_infoLogsCounter;

		private string m_warningLogsCounterStr;

		private int m_warningLogsCounter;

		private string m_errorLogsCounterStr;

		private int m_errorLogsCounter;

		private ConsoleLog m_selectedConsoleLog;

		private static Console instance;

		public static bool IsDebugMode => UnityEngine.Debug.isDebugBuild;

		private bool ShowInfoLogs
		{
			get
			{
				return m_showInfoLogs;
			}
			set
			{
				if (m_showInfoLogs != value)
				{
					m_showInfoLogs = value;
					if (value)
					{
						m_allowedLogTypes |= eConsoleLogType.INFO;
					}
					else
					{
						m_allowedLogTypes &= (eConsoleLogType)(-9);
					}
					RebuildDisplayableLogs();
				}
			}
		}

		private bool ShowWarningLogs
		{
			get
			{
				return m_showWarningLogs;
			}
			set
			{
				if (m_showWarningLogs != value)
				{
					m_showWarningLogs = value;
					if (value)
					{
						m_allowedLogTypes |= eConsoleLogType.WARNING;
					}
					else
					{
						m_allowedLogTypes &= (eConsoleLogType)(-5);
					}
					RebuildDisplayableLogs();
				}
			}
		}

		private bool ShowErrorLogs
		{
			get
			{
				return m_showErrorLogs;
			}
			set
			{
				if (m_showErrorLogs != value)
				{
					m_showErrorLogs = value;
					if (value)
					{
						m_allowedLogTypes |= (eConsoleLogType)19;
					}
					else
					{
						m_allowedLogTypes &= (eConsoleLogType)(-20);
					}
					RebuildDisplayableLogs();
				}
			}
		}

		public int InfoLogsCounter
		{
			get
			{
				return m_infoLogsCounter;
			}
			set
			{
				if (value != m_infoLogsCounter)
				{
					if (value > 999)
					{
						m_infoLogsCounterStr = "999+";
					}
					else
					{
						m_infoLogsCounterStr = value.ToString();
					}
				}
				m_infoLogsCounter = value;
			}
		}

		public int WarningLogsCounter
		{
			get
			{
				return m_warningLogsCounter;
			}
			set
			{
				if (value != m_warningLogsCounter)
				{
					if (value > 999)
					{
						m_warningLogsCounterStr = "999+";
					}
					else
					{
						m_warningLogsCounterStr = value.ToString();
					}
				}
				m_warningLogsCounter = value;
			}
		}

		public int ErrorLogsCounter
		{
			get
			{
				return m_errorLogsCounter;
			}
			set
			{
				if (value != m_errorLogsCounter)
				{
					if (value > 999)
					{
						m_errorLogsCounterStr = "999+";
					}
					else
					{
						m_errorLogsCounterStr = value.ToString();
					}
				}
				m_errorLogsCounter = value;
			}
		}

		public static Console Instance
		{
			get
			{
				if (instance == null)
				{
					instance = ScriptableObject.CreateInstance<Console>();
				}
				return instance;
			}
		}

		private Console()
		{
			m_infoLogsCounterStr = "0";
			m_warningLogsCounterStr = "0";
			m_errorLogsCounterStr = "0";
		}

		static Console()
		{
			UnityDebugUtility.LogCallback -= HandleUnityLog;
			UnityDebugUtility.LogCallback += HandleUnityLog;
		}

		private void OnEnable()
		{
			if (instance == null)
			{
				instance = this;
			}
			RebuildDisplayableLogs();
		}

		private void OnDisable()
		{
		}

		private static void HandleUnityLog(string _message, string _stackTrace, LogType _logType)
		{
			eConsoleLogType logType;
			switch (_logType)
			{
			case LogType.Log:
				logType = eConsoleLogType.INFO;
				break;
			case LogType.Warning:
				logType = eConsoleLogType.WARNING;
				break;
			case LogType.Error:
				logType = eConsoleLogType.ERROR;
				break;
			case LogType.Exception:
				logType = eConsoleLogType.EXCEPTION;
				break;
			default:
				logType = eConsoleLogType.ASSERT;
				break;
			}
			if (Instance != null)
			{
				Instance.Log("untagged", _message, logType, null);
			}
		}

		private void Clear()
		{
			m_consoleLogsList.Clear();
			m_consoleTags.Clear();
			RebuildDisplayableLogs();
		}

		private void RebuildDisplayableLogs()
		{
			m_displayableConsoleLogsList.Clear();
			m_selectedConsoleLog = default(ConsoleLog);
			InfoLogsCounter = 0;
			WarningLogsCounter = 0;
			ErrorLogsCounter = 0;
			int count = m_consoleLogsList.Count;
			for (int i = 0; i < count; i++)
			{
				ConsoleLog consoleLog = m_consoleLogsList[i];
				AddToDisplayableLogList(consoleLog);
			}
		}

		private bool AddToDisplayableLogList(ConsoleLog _consoleLog)
		{
			int tagID = _consoleLog.TagID;
			ConsoleTag consoleTag = m_consoleTags[tagID];
			if (!consoleTag.IsActive)
			{
				return false;
			}
			if (_consoleLog.Type == eConsoleLogType.INFO)
			{
				InfoLogsCounter++;
			}
			else if (_consoleLog.Type == eConsoleLogType.WARNING)
			{
				WarningLogsCounter++;
			}
			else
			{
				ErrorLogsCounter++;
			}
			if ((_consoleLog.Type & m_allowedLogTypes) == (eConsoleLogType)0)
			{
				return false;
			}
			m_displayableConsoleLogsList.Add(_consoleLog);
			return true;
		}

		private ConsoleTag GetConsoleTag(string _tagName)
		{
			if (m_consoleTags.Count == 0)
			{
				return null;
			}
			return m_consoleTags.FirstOrDefault((ConsoleTag _consoleTag) => _consoleTag.Name.Equals(_tagName));
		}

		private int GetIndexOfConsoleTag(string _tagName)
		{
			if (m_consoleTags.Count == 0)
			{
				return -1;
			}
			return m_consoleTags.FindIndex((ConsoleTag _consoleTag) => _consoleTag.Name.Equals(_tagName));
		}

		private bool IgnoreConsoleLog(string _tagName)
		{
			if (!IsDebugMode)
			{
				return true;
			}
			if (string.IsNullOrEmpty(_tagName))
			{
				_tagName = "untagged";
			}
			ConsoleTag consoleTag = GetConsoleTag(_tagName);
			if (consoleTag != null && consoleTag.Ignore)
			{
				return true;
			}
			return false;
		}

		private void Log(string _tagName, object _message, eConsoleLogType _logType, UnityEngine.Object _context)
		{
			if (!IgnoreConsoleLog(_tagName))
			{
				int num = GetIndexOfConsoleTag(_tagName);
				if (num == -1)
				{
					m_consoleTags.Add(new ConsoleTag(_tagName));
					num = m_consoleTags.Count - 1;
				}
				ConsoleTag consoleTag = m_consoleTags[num];
				if (!consoleTag.Ignore)
				{
					int logID = m_consoleLogsList.Count + 1;
                    ConsoleLog log = new ConsoleLog();
                    log.SetValuesConsoleLog(logID, num, _message.ToString(), _logType, _context);
					NativeBinding.Log(log);
				}
			}
		}

		public static void ClearConsole()
		{
			if (!(Instance == null))
			{
				Instance.Clear();
			}
		}

		public static void AddIgnoreTag(string _tag)
		{
			if (!(Instance == null))
			{
				ConsoleTag consoleTag = Instance.GetConsoleTag(_tag);
				if (consoleTag == null)
				{
					ConsoleTag consoleTag2 = new ConsoleTag(_tag);
					Instance.m_consoleTags.Add(consoleTag2);
					consoleTag = consoleTag2;
				}
				consoleTag.Ignore = true;
			}
		}

		public static void RemoveIgnoreTag(string _tag)
		{
			if (!(Instance == null))
			{
				ConsoleTag consoleTag = Instance.GetConsoleTag(_tag);
				if (consoleTag != null)
				{
					consoleTag.Ignore = false;
				}
			}
		}

		public static void DrawLine(string _tag, Vector3 _start, Vector3 _end, [Optional] Color _color, float _duration = 0f, bool _depthTest = true)
		{
			if (!(Instance == null) && !Instance.IgnoreConsoleLog(_tag))
			{
				UnityEngine.Debug.DrawLine(_start, _end, _color, _duration, _depthTest);
			}
		}

		public static void DrawRay(string _tag, Vector3 _start, Vector3 _direction, [Optional] Color _color, float _duration = 0f, bool _depthTest = true)
		{
			if (!(Instance == null) && !Instance.IgnoreConsoleLog(_tag))
			{
				UnityEngine.Debug.DrawRay(_start, _direction, _color, _duration, _depthTest);
			}
		}

		public static void Log(string _tag, object _message, UnityEngine.Object _context = null)
		{
			if (!(Instance == null))
			{
				Instance.Log(_tag, _message, eConsoleLogType.INFO, _context);
			}
		}

		public static void LogWarning(string _tag, object _message, UnityEngine.Object _context = null)
		{
			if (!(Instance == null))
			{
				Instance.Log(_tag, _message, eConsoleLogType.WARNING, _context);
			}
		}

		public static void LogError(string _tag, object _message, UnityEngine.Object _context = null)
		{
			if (!(Instance == null))
			{
				Instance.Log(_tag, _message, eConsoleLogType.ERROR, _context);
			}
		}

		public static void LogException(string _tag, Exception _exception, UnityEngine.Object _context = null)
		{
			if (!(Instance == null))
			{
				Instance.Log(_tag, _exception, eConsoleLogType.EXCEPTION, _context);
			}
		}
	}
}
