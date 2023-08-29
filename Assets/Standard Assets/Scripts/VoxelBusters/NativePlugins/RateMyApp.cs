using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class RateMyApp
	{
		private RateMyAppSettings m_rateMyAppSettings;

		private IRateMyAppController m_controller;

		public IRateMyAppDelegate Delegate
		{
			private get;
			set;
		}

		public RateMyApp(IRateMyAppController _controller)
			: this(NPSettings.Utility.RateMyApp, _controller)
		{
		}

		public RateMyApp(RateMyAppSettings _settings, IRateMyAppController _controller)
		{
			m_rateMyAppSettings = _settings;
			m_controller = _controller;
			MarkIfLaunchIsFirstTime();
		}

		public void AskForReview()
		{
			if (CanAskForReview())
			{
				m_controller.ExecuteRoutine(ShowDialogRoutine());
			}
		}

		public void AskForReviewNow()
		{
			ShowDialog();
		}

		public void RecordAppLaunch()
		{
			int value = PlayerPrefs.GetInt(m_controller.GetKeyNameAppUsageCount(), 0) + 1;
			PlayerPrefs.SetInt(m_controller.GetKeyNameAppUsageCount(), value);
			PlayerPrefs.Save();
		}

		private void MarkIfLaunchIsFirstTime()
		{
			bool flag = PlayerPrefs.GetInt(m_controller.GetKeyNameShowPromptAfter(), -1) == -1 || IsFirstTimeLaunch();
			PlayerPrefs.SetInt(m_controller.GetKeyNameIsFirstTimeLaunch(), flag ? 1 : 0);
		}

		private int GetAppUsageCount()
		{
			return PlayerPrefs.GetInt(m_controller.GetKeyNameAppUsageCount(), 0);
		}

		private bool IsFirstTimeLaunch()
		{
			return PlayerPrefs.GetInt(m_controller.GetKeyNameIsFirstTimeLaunch(), 0) == 1;
		}

		private bool CanAskForReview()
		{
			try
			{
				if (PlayerPrefs.GetInt(m_controller.GetKeyNameDontShow(), 0) == 1)
				{
					return false;
				}
				string @string = PlayerPrefs.GetString(m_controller.GetKeyNameVersionLastRated());
				if (!string.IsNullOrEmpty(@string))
				{
					string bundleVersion = PlayerSettings.GetBundleVersion();
					if (bundleVersion.CompareTo(@string) <= 0)
					{
						return false;
					}
				}
				DateTime utcNow = DateTime.UtcNow;
				int num = PlayerPrefs.GetInt(m_controller.GetKeyNameShowPromptAfter(), -1);
				if (num == -1)
				{
					num = m_rateMyAppSettings.ShowFirstPromptAfterHours;
					PlayerPrefs.SetInt(m_controller.GetKeyNameShowPromptAfter(), m_rateMyAppSettings.ShowFirstPromptAfterHours);
					PlayerPrefs.SetString(m_controller.GetKeyNamePromptLastShown(), utcNow.ToString());
				}
				string string2 = PlayerPrefs.GetString(m_controller.GetKeyNamePromptLastShown());
				DateTime d = DateTime.Parse(string2);
				int num2 = (int)(utcNow - d).TotalHours;
				int appUsageCount = GetAppUsageCount();
				if (num > num2)
				{
					return false;
				}
				if (!IsFirstTimeLaunch() && appUsageCount <= m_rateMyAppSettings.SuccessivePromptAfterLaunches)
				{
					return false;
				}
				PlayerPrefs.SetInt(m_controller.GetKeyNameIsFirstTimeLaunch(), 0);
				PlayerPrefs.SetInt(m_controller.GetKeyNameAppUsageCount(), 0);
				PlayerPrefs.SetString(m_controller.GetKeyNamePromptLastShown(), utcNow.ToString());
				return true;
				IL_0160:
				bool result;
				return result;
			}
			finally
			{
				PlayerPrefs.Save();
			}
		}

		private IEnumerator ShowDialogRoutine()
		{
			if (Delegate != null)
			{
				while (!Delegate.CanShowRateMyAppDialog())
				{
					yield return new WaitForSeconds(1f);
				}
			}
			ShowDialog();
		}

		private void ShowDialog()
		{
			if (Delegate != null)
			{
				Delegate.OnBeforeShowingRateMyAppDialog();
			}
			List<string> list = new List<string>();
			list.Add(m_rateMyAppSettings.RateItButtonText);
			list.Add(m_rateMyAppSettings.RemindMeLaterButtonText);
			if (!string.IsNullOrEmpty(m_rateMyAppSettings.DontAskButtonText))
			{
				list.Add(m_rateMyAppSettings.DontAskButtonText);
			}
			m_controller.ShowDialog(m_rateMyAppSettings.Title, m_rateMyAppSettings.Message, list.ToArray(), delegate(string _buttonName)
			{
				if (_buttonName.Equals(m_rateMyAppSettings.RemindMeLaterButtonText))
				{
					OnPressingRemindMeLaterButton();
				}
				else if (_buttonName.Equals(m_rateMyAppSettings.RateItButtonText))
				{
					OnPressingRateItButton();
				}
				else
				{
					OnPressingDontShowButton();
				}
				PlayerPrefs.Save();
			});
		}

		private void OnPressingRemindMeLaterButton()
		{
			PlayerPrefs.SetInt(m_controller.GetKeyNameShowPromptAfter(), m_rateMyAppSettings.SuccessivePromptAfterHours);
			m_controller.OnPressingRemindMeLaterButton();
		}

		private void OnPressingRateItButton()
		{
			string bundleVersion = PlayerSettings.GetBundleVersion();
			PlayerPrefs.SetString(m_controller.GetKeyNameVersionLastRated(), bundleVersion);
			m_controller.OnPressingRateItButton();
		}

		private void OnPressingDontShowButton()
		{
			PlayerPrefs.SetInt(m_controller.GetKeyNameDontShow(), 1);
			m_controller.OnPressingDontShowButton();
		}
	}
}
