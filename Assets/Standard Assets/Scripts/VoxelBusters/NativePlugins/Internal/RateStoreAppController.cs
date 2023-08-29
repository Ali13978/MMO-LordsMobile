using System.Collections;

namespace VoxelBusters.NativePlugins.Internal
{
	public class RateStoreAppController : IRateMyAppController
	{
		private const string kIsFirstTimeLaunch = "np-is-first-time-launch";

		private const string kVersionLastRated = "np-version-last-rated";

		private const string kShowPromptAfter = "np-show-prompt-after";

		private const string kPromptLastShown = "np-prompt-last-shown";

		private const string kDontShow = "np-dont-show";

		private const string kAppUsageCount = "np-app-usage-count";

		public string GetKeyNameIsFirstTimeLaunch()
		{
			return "np-is-first-time-launch";
		}

		public string GetKeyNameVersionLastRated()
		{
			return "np-version-last-rated";
		}

		public string GetKeyNameShowPromptAfter()
		{
			return "np-show-prompt-after";
		}

		public string GetKeyNamePromptLastShown()
		{
			return "np-prompt-last-shown";
		}

		public string GetKeyNameDontShow()
		{
			return "np-dont-show";
		}

		public string GetKeyNameAppUsageCount()
		{
			return "np-app-usage-count";
		}

		public void ExecuteRoutine(IEnumerator _routine)
		{
			NPBinding.Utility.StartCoroutine(_routine);
		}

		public void ShowDialog(string _title, string _message, string[] _buttons, UI.AlertDialogCompletion _onCompletion)
		{
			NPBinding.UI.ShowAlertDialogWithMultipleButtons(_title, _message, _buttons, _onCompletion);
		}

		public void OnPressingRemindMeLaterButton()
		{
		}

		public void OnPressingRateItButton()
		{
			NPBinding.Utility.OpenStoreLink(NPSettings.Application.StoreIdentifier);
		}

		public void OnPressingDontShowButton()
		{
		}
	}
}
