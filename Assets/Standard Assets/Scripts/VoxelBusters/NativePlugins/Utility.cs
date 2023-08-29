using System;
using System.Collections;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class Utility : MonoBehaviour
	{
		private IUtilityPlatform m_platform;

		public RateMyApp RateMyApp
		{
			get;
			private set;
		}

		private void Awake()
		{
			m_platform = CreatePlatformSpecificObject();
			if (NPSettings.Utility.RateMyApp.IsEnabled)
			{
				RateMyApp = new RateMyApp(NPSettings.Utility.RateMyApp, new RateStoreAppController());
				RateMyApp.RecordAppLaunch();
			}
		}

		private IEnumerator Start()
		{
			yield return new WaitForEndOfFrame();
			if (RateMyApp != null)
			{
				RateMyApp.AskForReview();
			}
		}

		private void OnApplicationPause(bool _isPaused)
		{
			if (!_isPaused && RateMyApp != null)
			{
				RateMyApp.AskForReview();
			}
		}

		public string GetUUID()
		{
			return Guid.NewGuid().ToString();
		}

		public void OpenStoreLink(params PlatformValue[] _storeIdentifiers)
		{
			PlatformValue currentPlatformValue = PlatformValueHelper.GetCurrentPlatformValue(_storeIdentifiers);
			if (currentPlatformValue != null)
			{
				OpenStoreLink(currentPlatformValue.Value);
			}
		}

		public void OpenStoreLink(string _applicationID)
		{
			m_platform.OpenStoreLink(_applicationID);
		}

		public void SetApplicationIconBadgeNumber(int _badgeNumber)
		{
			m_platform.SetApplicationIconBadgeNumber(_badgeNumber);
		}

		public string GetBundleVersion()
		{
			return PlayerSettings.GetBundleVersion();
		}

		public string GetBundleIdentifier()
		{
			return PlayerSettings.GetBundleIdentifier();
		}

		private IUtilityPlatform CreatePlatformSpecificObject()
		{
			return new UtilityAndroid();
		}
	}
}
