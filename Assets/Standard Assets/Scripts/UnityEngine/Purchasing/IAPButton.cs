using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Purchasing
{
	[RequireComponent(typeof(Button))]
	[AddComponentMenu("Unity IAP/IAP Button")]
	[HelpURL("https://docs.unity3d.com/Manual/UnityIAP.html")]
	public class IAPButton : MonoBehaviour
	{
		public enum ButtonType
		{
			Purchase,
			Restore
		}

		[Serializable]
		public class OnPurchaseCompletedEvent : UnityEvent<Product>
		{
		}

		[Serializable]
		public class OnPurchaseFailedEvent : UnityEvent<Product, PurchaseFailureReason>
		{
		}

		public class IAPButtonStoreManager : IStoreListener
		{
			private static IAPButtonStoreManager instance = new IAPButtonStoreManager();

			private ProductCatalog catalog;

			private List<IAPButton> activeButtons = new List<IAPButton>();

			private List<IAPListener> activeListeners = new List<IAPListener>();

			protected IStoreController controller;

			protected IExtensionProvider extensions;

			public static IAPButtonStoreManager Instance => instance;

			public IStoreController StoreController => controller;

			public IExtensionProvider ExtensionProvider => extensions;

			private IAPButtonStoreManager()
			{
				catalog = ProductCatalog.LoadDefaultCatalog();
				StandardPurchasingModule standardPurchasingModule = StandardPurchasingModule.Instance();
				standardPurchasingModule.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
				ConfigurationBuilder builder = ConfigurationBuilder.Instance(standardPurchasingModule);
				IAPConfigurationHelper.PopulateConfigurationBuilder(ref builder, catalog);
				UnityPurchasing.Initialize(this, builder);
			}

			public bool HasProductInCatalog(string productID)
			{
				foreach (ProductCatalogItem allProduct in catalog.allProducts)
				{
					if (allProduct.id == productID)
					{
						return true;
					}
				}
				return false;
			}

			public Product GetProduct(string productID)
			{
				if (controller != null && controller.products != null && !string.IsNullOrEmpty(productID))
				{
					return controller.products.WithID(productID);
				}
				return null;
			}

			public void AddButton(IAPButton button)
			{
				activeButtons.Add(button);
			}

			public void RemoveButton(IAPButton button)
			{
				activeButtons.Remove(button);
			}

			public void AddListener(IAPListener listener)
			{
				activeListeners.Add(listener);
			}

			public void RemoveListener(IAPListener listener)
			{
				activeListeners.Remove(listener);
			}

			public void InitiatePurchase(string productID)
			{
				if (controller == null)
				{
					UnityEngine.Debug.LogError("Purchase failed because Purchasing was not initialized correctly");
					foreach (IAPButton activeButton in activeButtons)
					{
						if (activeButton.productId == productID)
						{
							activeButton.OnPurchaseFailed(null, PurchaseFailureReason.PurchasingUnavailable);
						}
					}
				}
				else
				{
					controller.InitiatePurchase(productID);
				}
			}

			public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
			{
				this.controller = controller;
				this.extensions = extensions;
				foreach (IAPButton activeButton in activeButtons)
				{
					activeButton.UpdateText();
				}
			}

			public void OnInitializeFailed(InitializationFailureReason error)
			{
				UnityEngine.Debug.LogError($"Purchasing failed to initialize. Reason: {error.ToString()}");
			}

			public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
			{
				bool flag = false;
				bool flag2 = false;
				foreach (IAPButton activeButton in activeButtons)
				{
					if (activeButton.productId == e.purchasedProduct.definition.id)
					{
						if (activeButton.ProcessPurchase(e) == PurchaseProcessingResult.Complete)
						{
							flag = true;
						}
						flag2 = true;
					}
				}
				foreach (IAPListener activeListener in activeListeners)
				{
					if (activeListener.ProcessPurchase(e) == PurchaseProcessingResult.Complete)
					{
						flag = true;
					}
					flag2 = true;
				}
				if (!flag2)
				{
					UnityEngine.Debug.LogWarning("Purchase not correctly processed for product \"" + e.purchasedProduct.definition.id + "\". Add an active IAPButton to process this purchase, or add an IAPListener to receive any unhandled purchase events.");
				}
				return (!flag) ? PurchaseProcessingResult.Pending : PurchaseProcessingResult.Complete;
			}

			public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
			{
				bool flag = false;
				foreach (IAPButton activeButton in activeButtons)
				{
					if (activeButton.productId == product.definition.id)
					{
						activeButton.OnPurchaseFailed(product, reason);
						flag = true;
					}
				}
				foreach (IAPListener activeListener in activeListeners)
				{
					activeListener.OnPurchaseFailed(product, reason);
					flag = true;
				}
				if (flag)
				{
					UnityEngine.Debug.LogWarning("Failed purchase not correctly handled for product \"" + product.definition.id + "\". Add an active IAPButton to handle this failure, or add an IAPListener to receive any unhandled purchase failures.");
				}
			}
		}

		[HideInInspector]
		public string productId;

		[Tooltip("The type of this button, can be either a purchase or a restore button")]
		public ButtonType buttonType;

		[Tooltip("Consume the product immediately after a successful purchase")]
		public bool consumePurchase = true;

		[Tooltip("Event fired after a successful purchase of this product")]
		public OnPurchaseCompletedEvent onPurchaseComplete;

		[Tooltip("Event fired after a failed purchase of this product")]
		public OnPurchaseFailedEvent onPurchaseFailed;

		[Tooltip("[Optional] Displays the localized title from the app store")]
		public Text titleText;

		[Tooltip("[Optional] Displays the localized description from the app store")]
		public Text descriptionText;

		[Tooltip("[Optional] Displays the localized price from the app store")]
		public Text priceText;

		private void Start()
		{
			Button component = GetComponent<Button>();
			if (buttonType == ButtonType.Purchase)
			{
				if ((bool)component)
				{
					component.onClick.AddListener(PurchaseProduct);
				}
				if (string.IsNullOrEmpty(productId))
				{
					UnityEngine.Debug.LogError("IAPButton productId is empty");
				}
				if (!IAPButtonStoreManager.Instance.HasProductInCatalog(productId))
				{
					UnityEngine.Debug.LogWarning("The product catalog has no product with the ID \"" + productId + "\"");
				}
			}
			else if (buttonType == ButtonType.Restore && (bool)component)
			{
				component.onClick.AddListener(Restore);
			}
		}

		private void OnEnable()
		{
			if (buttonType == ButtonType.Purchase)
			{
				IAPButtonStoreManager.Instance.AddButton(this);
				UpdateText();
			}
		}

		private void OnDisable()
		{
			if (buttonType == ButtonType.Purchase)
			{
				IAPButtonStoreManager.Instance.RemoveButton(this);
			}
		}

		private void PurchaseProduct()
		{
			if (buttonType == ButtonType.Purchase)
			{
				UnityEngine.Debug.Log("IAPButton.PurchaseProduct() with product ID: " + productId);
				IAPButtonStoreManager.Instance.InitiatePurchase(productId);
			}
		}

		private void Restore()
		{
			if (buttonType == ButtonType.Restore)
			{
				if (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)
				{
					IAPButtonStoreManager.Instance.ExtensionProvider.GetExtension<IMicrosoftExtensions>().RestoreTransactions();
				}
				else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.tvOS)
				{
					IAPButtonStoreManager.Instance.ExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnTransactionsRestored);
				}
				else if (Application.platform == RuntimePlatform.Android && StandardPurchasingModule.Instance().appStore == AppStore.SamsungApps)
				{
					IAPButtonStoreManager.Instance.ExtensionProvider.GetExtension<ISamsungAppsExtensions>().RestoreTransactions(OnTransactionsRestored);
				}
				else if (Application.platform == RuntimePlatform.Android && StandardPurchasingModule.Instance().appStore == AppStore.CloudMoolah)
				{
					IAPButtonStoreManager.Instance.ExtensionProvider.GetExtension<IMoolahExtension>().RestoreTransactionID(delegate(RestoreTransactionIDState restoreTransactionIDState)
					{
						OnTransactionsRestored(restoreTransactionIDState != RestoreTransactionIDState.RestoreFailed && restoreTransactionIDState != RestoreTransactionIDState.NotKnown);
					});
				}
				else
				{
					UnityEngine.Debug.LogWarning(Application.platform.ToString() + " is not a supported platform for the Codeless IAP restore button");
				}
			}
		}

		private void OnTransactionsRestored(bool success)
		{
			UnityEngine.Debug.Log("Transactions restored: " + success);
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
		{
			UnityEngine.Debug.Log($"IAPButton.ProcessPurchase(PurchaseEventArgs {e} - {e.purchasedProduct.definition.id})");
			onPurchaseComplete.Invoke(e.purchasedProduct);
			return (!consumePurchase) ? PurchaseProcessingResult.Pending : PurchaseProcessingResult.Complete;
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
		{
			UnityEngine.Debug.Log($"IAPButton.OnPurchaseFailed(Product {product}, PurchaseFailureReason {reason})");
			onPurchaseFailed.Invoke(product, reason);
		}

		private void UpdateText()
		{
			Product product = IAPButtonStoreManager.Instance.GetProduct(productId);
			if (product != null)
			{
				if (titleText != null)
				{
					titleText.text = product.metadata.localizedTitle;
				}
				if (descriptionText != null)
				{
					descriptionText.text = product.metadata.localizedDescription;
				}
				if (priceText != null)
				{
					priceText.text = product.metadata.localizedPriceString;
				}
			}
		}
	}
}
