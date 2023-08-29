using UnityEngine;

namespace VoxelBusters.NativePlugins.Demo
{
	public class UtilityDemo : NPDemoBase
	{
		[SerializeField]
		private int m_applicationBadgeNumber = 2;

		protected override void Start()
		{
			base.Start();
			AddExtraInfoTexts("For using RateMyApp feature, you have to enable it in NPSettings->Utility Settings.");
		}

		protected override void DisplayFeatureFunctionalities()
		{
			base.DisplayFeatureFunctionalities();
			if (GUILayout.Button("Get UUID"))
			{
				string uUID = GetUUID();
				AddNewResult("New UUID = " + uUID + ".");
			}
			if (GUILayout.Button("Open Store Link"))
			{
				string storeIdentifier = NPSettings.Application.StoreIdentifier;
				AddNewResult("Opening store link with application id = " + storeIdentifier + ".");
				OpenStoreLink(storeIdentifier);
			}
			if (GUILayout.Button("Ask For Review Now"))
			{
				AskForReviewNow();
			}
			if (GUILayout.Button("Set Application Icon Badge Number"))
			{
				SetApplicationIconBadgeNumber();
			}
			if (GUILayout.Button("Get Bundle Version"))
			{
				AddNewResult("Application's bundle version is " + NPBinding.Utility.GetBundleVersion() + ".");
			}
			if (GUILayout.Button("Get Bundle Identifier"))
			{
				AddNewResult("Application's bundle identifier is " + NPBinding.Utility.GetBundleIdentifier() + ".");
			}
		}

		private string GetUUID()
		{
			return NPBinding.Utility.GetUUID();
		}

		private void OpenStoreLink(string _applicationID)
		{
			NPBinding.Utility.OpenStoreLink(_applicationID);
		}

		private void AskForReviewNow()
		{
			if (NPSettings.Utility.RateMyApp.IsEnabled)
			{
				NPBinding.Utility.RateMyApp.AskForReviewNow();
			}
			else
			{
				AddNewResult("Enable RateMyApp feature in NPSettings.");
			}
		}

		private void SetApplicationIconBadgeNumber()
		{
			NPBinding.Utility.SetApplicationIconBadgeNumber(m_applicationBadgeNumber);
		}
	}
}
