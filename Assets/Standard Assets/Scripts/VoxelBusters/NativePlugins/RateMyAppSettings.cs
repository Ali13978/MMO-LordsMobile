using System;
using UnityEngine;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class RateMyAppSettings
	{
		[Tooltip("If enabled, Rate My App feature will be active within your application.")]
		[SerializeField]
		private bool m_isEnabled;

		[SerializeField]
		[Tooltip("The text that appears in the prompt's title bar.")]
		private string m_title = "Rate My App";

		[SerializeField]
		[Tooltip("Descriptive message for the user.")]
		private string m_message = "If you enjoy using Native Plugin would you mind taking a moment to rate it? It wont take more than a minute. Thanks for your support";

		[Tooltip("The number of hours since first launch, after which user is prompted to rate the app.")]
		[SerializeField]
		private int m_showFirstPromptAfterHours = 2;

		[Tooltip("The number of hours since last time we showed the prompt, after which user is prompted to rate the app.")]
		[SerializeField]
		private int m_successivePromptAfterHours = 6;

		[Tooltip("The number of times the user must launch the app, after which user is prompted to rate the app.")]
		[SerializeField]
		private int m_successivePromptAfterLaunches = 5;

		[Tooltip("The button label for the button, that will send user to app review page.")]
		[SerializeField]
		private string m_rateItButtonText = "Rate It Now";

		[Tooltip("The button label for the button, that will remind user to review later.")]
		[SerializeField]
		private string m_remindMeLaterButtonText = "Remind Me Later";

		[Tooltip("The button label for the button, that rejects reviewing the app. Keep this field empty, if you don't wish to show this option.")]
		[SerializeField]
		private string m_dontAskButtonText = "No, Thanks";

		public bool IsEnabled
		{
			get
			{
				return m_isEnabled;
			}
			set
			{
				m_isEnabled = value;
			}
		}

		public string Title
		{
			get
			{
				return m_title;
			}
			set
			{
				m_title = value;
			}
		}

		public string Message
		{
			get
			{
				return m_message;
			}
			set
			{
				m_message = value;
			}
		}

		public int ShowFirstPromptAfterHours
		{
			get
			{
				return m_showFirstPromptAfterHours;
			}
			set
			{
				m_showFirstPromptAfterHours = value;
			}
		}

		public int SuccessivePromptAfterHours
		{
			get
			{
				return m_successivePromptAfterHours;
			}
			set
			{
				m_successivePromptAfterHours = value;
			}
		}

		public int SuccessivePromptAfterLaunches
		{
			get
			{
				return m_successivePromptAfterLaunches;
			}
			set
			{
				m_successivePromptAfterLaunches = value;
			}
		}

		public string RemindMeLaterButtonText
		{
			get
			{
				return m_remindMeLaterButtonText;
			}
			set
			{
				m_remindMeLaterButtonText = value;
			}
		}

		public string RateItButtonText
		{
			get
			{
				return m_rateItButtonText;
			}
			set
			{
				m_rateItButtonText = value;
			}
		}

		public string DontAskButtonText
		{
			get
			{
				return m_dontAskButtonText;
			}
			set
			{
				m_dontAskButtonText = value;
			}
		}
	}
}
