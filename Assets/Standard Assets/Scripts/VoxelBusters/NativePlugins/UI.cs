using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.DebugPRO;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class UI : MonoBehaviour
	{
		public delegate void AlertDialogCompletion(string _buttonPressed);

		public delegate void SingleFieldPromptCompletion(string _buttonPressed, string _inputText);

		public delegate void LoginPromptCompletion(string _buttonPressed, string _usernameText, string _passwordText);

		private const string kDefaultTextForButton = "Ok";

		private Dictionary<string, AlertDialogCompletion> m_alertDialogCallbackCollection = new Dictionary<string, AlertDialogCompletion>();

		protected SingleFieldPromptCompletion OnSingleFieldPromptClosed;

		protected LoginPromptCompletion OnLoginPromptClosed;

		private void AlertDialogClosed(string _jsonStr)
		{
			IDictionary dataDict = JSONUtility.FromJSON(_jsonStr) as IDictionary;
			ParseAlertDialogDismissedData(dataDict, out string _buttonPressed, out string _callerTag);
			Console.Log("Native Plugins", "[UI] Alert dialog closed, ButtonPressed=" + _buttonPressed);
			GetAlertDialogCallback(_callerTag)?.Invoke(_buttonPressed);
		}

		protected virtual void ParseAlertDialogDismissedData(IDictionary _dataDict, out string _buttonPressed, out string _callerTag)
		{
			_buttonPressed = null;
			_callerTag = null;
		}

		private string CacheAlertDialogCallback(AlertDialogCompletion _newCallback)
		{
			if (_newCallback != null)
			{
				string uUID = NPBinding.Utility.GetUUID();
				m_alertDialogCallbackCollection[uUID] = _newCallback;
				return uUID;
			}
			return string.Empty;
		}

		protected AlertDialogCompletion GetAlertDialogCallback(string _tag)
		{
			if (!string.IsNullOrEmpty(_tag) && m_alertDialogCallbackCollection.ContainsKey(_tag))
			{
				return m_alertDialogCallbackCollection[_tag];
			}
			return null;
		}

		public void ShowAlertDialogWithSingleButton(string _title, string _message, string _button, AlertDialogCompletion _onCompletion)
		{
			ShowAlertDialogWithMultipleButtons(_title, _message, new string[1]
			{
				_button
			}, _onCompletion);
		}

		public void ShowAlertDialogWithMultipleButtons(string _title, string _message, string[] _buttons, AlertDialogCompletion _onCompletion)
		{
			string callbackTag = CacheAlertDialogCallback(_onCompletion);
			ShowAlertDialogWithMultipleButtons(_title, _message, _buttons, callbackTag);
		}

		protected virtual void ShowAlertDialogWithMultipleButtons(string _title, string _message, string[] _buttons, string _callbackTag)
		{
			if (_buttons == null || _buttons.Length == 0)
			{
				_buttons = new string[1]
				{
					"Ok"
				};
			}
			else if (string.IsNullOrEmpty(_buttons[0]))
			{
				_buttons[0] = "Ok";
			}
		}

		private void SingleFieldPromptDialogClosed(string _jsonStr)
		{
			Console.Log("Native Plugins", "[UI] Single field prompt was dismissed");
			if (OnSingleFieldPromptClosed != null)
			{
				IDictionary dataDict = JSONUtility.FromJSON(_jsonStr) as IDictionary;
				ParseSingleFieldPromptClosedData(dataDict, out string _buttonPressed, out string _inputText);
				OnSingleFieldPromptClosed(_buttonPressed, _inputText);
			}
		}

		private void LoginPromptDialogClosed(string _jsonStr)
		{
			Console.Log("Native Plugins", "[UI] Login prompt was dismissed");
			if (OnLoginPromptClosed != null)
			{
				IDictionary dataDict = JSONUtility.FromJSON(_jsonStr) as IDictionary;
				ParseLoginPromptClosedData(dataDict, out string _buttonPressed, out string _usernameText, out string _passwordText);
				OnLoginPromptClosed(_buttonPressed, _usernameText, _passwordText);
			}
		}

		protected virtual void ParseSingleFieldPromptClosedData(IDictionary _dataDict, out string _buttonPressed, out string _inputText)
		{
			_buttonPressed = null;
			_inputText = null;
		}

		protected virtual void ParseLoginPromptClosedData(IDictionary _dataDict, out string _buttonPressed, out string _usernameText, out string _passwordText)
		{
			_buttonPressed = null;
			_usernameText = null;
			_passwordText = null;
		}

		public void ShowSingleFieldPromptDialogWithPlainText(string _title, string _message, string _placeholder, string[] _buttons, SingleFieldPromptCompletion _onCompletion)
		{
			ShowSingleFieldPromptDialog(_title, _message, _placeholder, _useSecureText: false, _buttons, _onCompletion);
		}

		public void ShowSingleFieldPromptDialogWithSecuredText(string _title, string _message, string _placeholder, string[] _buttons, SingleFieldPromptCompletion _onCompletion)
		{
			ShowSingleFieldPromptDialog(_title, _message, _placeholder, _useSecureText: true, _buttons, _onCompletion);
		}

		protected virtual void ShowSingleFieldPromptDialog(string _title, string _message, string _placeholder, bool _useSecureText, string[] _buttonsList, SingleFieldPromptCompletion _onCompletion)
		{
			OnSingleFieldPromptClosed = _onCompletion;
		}

		public virtual void ShowLoginPromptDialog(string _title, string _message, string _usernamePlaceHolder, string _passwordPlaceHolder, string[] _buttons, LoginPromptCompletion _onCompletion)
		{
			OnLoginPromptClosed = _onCompletion;
		}

		public virtual void ShowToast(string _message, eToastMessageLength _length)
		{
		}

		public virtual void SetPopoverPoint(Vector2 _position)
		{
		}

		public void SetPopoverPointAtLastTouchPosition()
		{
			Vector2 popoverPoint = Vector2.zero;
			if (UnityEngine.Input.touchCount > 0)
			{
				popoverPoint = UnityEngine.Input.GetTouch(0).position;
				popoverPoint.y = (float)Screen.height - popoverPoint.y;
			}
			SetPopoverPoint(popoverPoint);
		}
	}
}
