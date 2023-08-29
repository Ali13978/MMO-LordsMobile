using System.Collections;
using UnityEngine;
using VoxelBusters.DebugPRO;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class UIAndroid : UI
	{
		private class Native
		{
			internal class Class
			{
				internal const string NAME = "com.voxelbusters.nativeplugins.features.ui.UiHandler";
			}

			internal class Methods
			{
				internal const string SHOW_ALERT_DIALOG = "showAlertDialogWithMultipleButtons";

				internal const string SHOW_SINGLE_FIELD_PROMPT = "showSingleFieldPromptDialog";

				internal const string SHOW_LOGIN_PROMPT = "showLoginPromptDialog";

				internal const string SHOW_TOAST = "showToast";
			}
		}

		private AndroidJavaObject Plugin
		{
			get;
			set;
		}

		private UIAndroid()
		{
			Plugin = AndroidPluginUtility.GetSingletonInstance("com.voxelbusters.nativeplugins.features.ui.UiHandler");
		}

		protected override void ParseAlertDialogDismissedData(IDictionary _dataDict, out string _buttonPressed, out string _callerTag)
		{
			_buttonPressed = (_dataDict["button-pressed"] as string);
			_callerTag = (_dataDict["caller"] as string);
		}

		protected override void ShowAlertDialogWithMultipleButtons(string _title, string _message, string[] _buttonsList, string _callbackTag)
		{
			base.ShowAlertDialogWithMultipleButtons(_title, _message, _buttonsList, _callbackTag);
			Plugin.Call("showAlertDialogWithMultipleButtons", _title, _message, _buttonsList.ToJSON(), _callbackTag);
		}

		protected override void ParseSingleFieldPromptClosedData(IDictionary _dataDict, out string _buttonPressed, out string _inputText)
		{
			_buttonPressed = (_dataDict["button-pressed"] as string);
			_inputText = (_dataDict["input"] as string);
		}

		protected override void ParseLoginPromptClosedData(IDictionary _dataDict, out string _buttonPressed, out string _usernameText, out string _passwordText)
		{
			_buttonPressed = (_dataDict["button-pressed"] as string);
			_usernameText = (_dataDict["username"] as string);
			_passwordText = (_dataDict["password"] as string);
		}

		protected override void ShowSingleFieldPromptDialog(string _title, string _message, string _placeholder, bool _useSecureText, string[] _buttonsList, SingleFieldPromptCompletion _onCompletion)
		{
			base.ShowSingleFieldPromptDialog(_title, _message, _placeholder, _useSecureText, _buttonsList, _onCompletion);
			Plugin.Call("showSingleFieldPromptDialog", _title, _message, _placeholder, _useSecureText, _buttonsList.ToJSON());
		}

		public override void ShowLoginPromptDialog(string _title, string _message, string _placeholder1, string _placeholder2, string[] _buttonsList, LoginPromptCompletion _onCompletion)
		{
			base.ShowLoginPromptDialog(_title, _message, _placeholder1, _placeholder2, _buttonsList, _onCompletion);
			Plugin.Call("showLoginPromptDialog", _title, _message, _placeholder1, _placeholder2, _buttonsList.ToJSON());
		}

		public override void ShowToast(string _message, eToastMessageLength _length)
		{
			Plugin.Call("showToast", _message, (_length != 0) ? "LONG" : "SHORT");
		}

		public override void SetPopoverPoint(Vector2 _position)
		{
			Console.LogWarning("Native Plugins", "The operation could not be completed because the requested feature is supported only on iOS platform.");
		}
	}
}
