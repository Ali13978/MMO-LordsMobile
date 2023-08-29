using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Store;
using UnityEngine.UI;

[AddComponentMenu("Unity IAP/Demo")]
public class IAPDemo : MonoBehaviour, IStoreListener
{
	[Serializable]
	public class UnityChannelPurchaseError
	{
		public string error;

		public UnityChannelPurchaseInfo purchaseInfo;
	}

	[Serializable]
	public class UnityChannelPurchaseInfo
	{
		public string productCode;

		public string gameOrderId;

		public string orderQueryToken;
	}

	private class UnityChannelLoginHandler : ILoginListener
	{
		internal Action initializeSucceededAction;

		internal Action<string> initializeFailedAction;

		internal Action<UserInfo> loginSucceededAction;

		internal Action<string> loginFailedAction;

		public void OnInitialized()
		{
			initializeSucceededAction();
		}

		public void OnInitializeFailed(string message)
		{
			initializeFailedAction(message);
		}

		public void OnLogin(UserInfo userInfo)
		{
			loginSucceededAction(userInfo);
		}

		public void OnLoginFailed(string message)
		{
			loginFailedAction(message);
		}
	}

	private IStoreController m_Controller;

	private IAppleExtensions m_AppleExtensions;

	private IMoolahExtension m_MoolahExtensions;

	private ISamsungAppsExtensions m_SamsungExtensions;

	private IMicrosoftExtensions m_MicrosoftExtensions;

	private IUnityChannelExtensions m_UnityChannelExtensions;

	private bool m_IsGooglePlayStoreSelected;

	private bool m_IsSamsungAppsStoreSelected;

	private bool m_IsCloudMoolahStoreSelected;

	private bool m_IsUnityChannelSelected;

	private string m_LastTransactionID;

	private bool m_IsLoggedIn;

	private UnityChannelLoginHandler unityChannelLoginHandler;

	private bool m_FetchReceiptPayloadOnPurchase;

	private bool m_PurchaseInProgress;

	private Dictionary<string, IAPDemoProductUI> m_ProductUIs = new Dictionary<string, IAPDemoProductUI>();

	public GameObject productUITemplate;

	public RectTransform contentRect;

	public Button restoreButton;

	public Button loginButton;

	public Button validateButton;

	public Text versionText;

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		m_Controller = controller;
		m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
		m_SamsungExtensions = extensions.GetExtension<ISamsungAppsExtensions>();
		m_MoolahExtensions = extensions.GetExtension<IMoolahExtension>();
		m_MicrosoftExtensions = extensions.GetExtension<IMicrosoftExtensions>();
		m_UnityChannelExtensions = extensions.GetExtension<IUnityChannelExtensions>();
		InitUI(controller.products.all);
		m_AppleExtensions.RegisterPurchaseDeferredListener(OnDeferred);
		UnityEngine.Debug.Log("Available items:");
		Product[] all = controller.products.all;
		foreach (Product product in all)
		{
			if (product.availableToPurchase)
			{
				UnityEngine.Debug.Log(string.Join(" - ", new string[7]
				{
					product.metadata.localizedTitle,
					product.metadata.localizedDescription,
					product.metadata.isoCurrencyCode,
					product.metadata.localizedPrice.ToString(),
					product.metadata.localizedPriceString,
					product.transactionID,
					product.receipt
				}));
			}
		}
		AddProductUIs(m_Controller.products.all);
		LogProductDefinitions();
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	{
		UnityEngine.Debug.Log("Purchase OK: " + e.purchasedProduct.definition.id);
		UnityEngine.Debug.Log("Receipt: " + e.purchasedProduct.receipt);
		m_LastTransactionID = e.purchasedProduct.transactionID;
		m_PurchaseInProgress = false;
		if (m_IsUnityChannelSelected)
		{
			UnifiedReceipt unifiedReceipt = JsonUtility.FromJson<UnifiedReceipt>(e.purchasedProduct.receipt);
			if (unifiedReceipt != null && !string.IsNullOrEmpty(unifiedReceipt.Payload))
			{
				UnityChannelPurchaseReceipt unityChannelPurchaseReceipt = JsonUtility.FromJson<UnityChannelPurchaseReceipt>(unifiedReceipt.Payload);
				UnityEngine.Debug.LogFormat("UnityChannel receipt: storeSpecificId = {0}, transactionId = {1}, orderQueryToken = {2}", unityChannelPurchaseReceipt.storeSpecificId, unityChannelPurchaseReceipt.transactionId, unityChannelPurchaseReceipt.orderQueryToken);
			}
		}
		UpdateProductUI(e.purchasedProduct);
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product item, PurchaseFailureReason r)
	{
		UnityEngine.Debug.Log("Purchase failed: " + item.definition.id);
		UnityEngine.Debug.Log(r);
		if (m_IsUnityChannelSelected)
		{
			string lastPurchaseError = m_UnityChannelExtensions.GetLastPurchaseError();
			UnityChannelPurchaseError unityChannelPurchaseError = JsonUtility.FromJson<UnityChannelPurchaseError>(lastPurchaseError);
			if (unityChannelPurchaseError != null && unityChannelPurchaseError.purchaseInfo != null)
			{
				UnityChannelPurchaseInfo purchaseInfo = unityChannelPurchaseError.purchaseInfo;
				UnityEngine.Debug.LogFormat("UnityChannel purchaseInfo: productCode = {0}, gameOrderId = {1}, orderQueryToken = {2}", purchaseInfo.productCode, purchaseInfo.gameOrderId, purchaseInfo.orderQueryToken);
			}
			if (r == PurchaseFailureReason.Unknown && unityChannelPurchaseError != null && unityChannelPurchaseError.error != null && unityChannelPurchaseError.error.Equals("DuplicateTransaction"))
			{
				UnityEngine.Debug.Log("Duplicate transaction detected, unlock this item");
			}
		}
		m_PurchaseInProgress = false;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		UnityEngine.Debug.Log("Billing failed to initialize!");
		switch (error)
		{
		case InitializationFailureReason.AppNotKnown:
			UnityEngine.Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");
			break;
		case InitializationFailureReason.PurchasingUnavailable:
			UnityEngine.Debug.Log("Billing disabled!");
			break;
		case InitializationFailureReason.NoProductsAvailable:
			UnityEngine.Debug.Log("No products available for purchase!");
			break;
		}
	}

	public void Awake()
	{
		StandardPurchasingModule standardPurchasingModule = StandardPurchasingModule.Instance();
		standardPurchasingModule.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
		ConfigurationBuilder builder = ConfigurationBuilder.Instance(standardPurchasingModule);
		builder.Configure<IMicrosoftConfiguration>().useMockBillingSystem = false;
		m_IsGooglePlayStoreSelected = (Application.platform == RuntimePlatform.Android && standardPurchasingModule.appStore == AppStore.GooglePlay);
		builder.Configure<IMoolahConfiguration>().appKey = "d93f4564c41d463ed3d3cd207594ee1b";
		builder.Configure<IMoolahConfiguration>().hashKey = "cc";
		builder.Configure<IMoolahConfiguration>().SetMode(CloudMoolahMode.AlwaysSucceed);
		m_IsCloudMoolahStoreSelected = (Application.platform == RuntimePlatform.Android && standardPurchasingModule.appStore == AppStore.CloudMoolah);
		m_IsUnityChannelSelected = (Application.platform == RuntimePlatform.Android && standardPurchasingModule.appStore == AppStore.XiaomiMiPay);
		builder.Configure<IUnityChannelConfiguration>().fetchReceiptPayloadOnPurchase = m_FetchReceiptPayloadOnPurchase;
		ProductCatalog productCatalog = ProductCatalog.LoadDefaultCatalog();
		foreach (ProductCatalogItem allProduct in productCatalog.allProducts)
		{
			if (allProduct.allStoreIDs.Count > 0)
			{
				IDs ds = new IDs();
				foreach (StoreID allStoreID in allProduct.allStoreIDs)
				{
					ds.Add(allStoreID.id, allStoreID.store);
				}
				builder.AddProduct(allProduct.id, allProduct.type, ds);
			}
			else
			{
				builder.AddProduct(allProduct.id, allProduct.type);
			}
		}
		builder.AddProduct("100.gold.coins", ProductType.Consumable, new IDs
		{
			{
				"100.gold.coins.mac",
				"MacAppStore"
			},
			{
				"000000596586",
				"TizenStore"
			},
			{
				"com.ff",
				"MoolahAppStore"
			}
		});
		builder.AddProduct("500.gold.coins", ProductType.Consumable, new IDs
		{
			{
				"500.gold.coins.mac",
				"MacAppStore"
			},
			{
				"000000596581",
				"TizenStore"
			},
			{
				"com.ee",
				"MoolahAppStore"
			}
		});
		builder.AddProduct("sword", ProductType.NonConsumable, new IDs
		{
			{
				"sword.mac",
				"MacAppStore"
			},
			{
				"000000596583",
				"TizenStore"
			}
		});
		builder.AddProduct("subscription", ProductType.Subscription, new IDs
		{
			{
				"subscription.mac",
				"MacAppStore"
			}
		});
		builder.Configure<IAmazonConfiguration>().WriteSandboxJSON(builder.products);
		builder.Configure<ISamsungAppsConfiguration>().SetMode(SamsungAppsMode.AlwaysSucceed);
		m_IsSamsungAppsStoreSelected = (Application.platform == RuntimePlatform.Android && standardPurchasingModule.appStore == AppStore.SamsungApps);
		builder.Configure<ITizenStoreConfiguration>().SetGroupId("100000085616");
		Action initializeUnityIap = delegate
		{
			UnityPurchasing.Initialize(this, builder);
		};
		if (!m_IsUnityChannelSelected)
		{
			initializeUnityIap();
			return;
		}
		AppInfo appInfo = new AppInfo();
		appInfo.appId = "abc123appId";
		appInfo.appKey = "efg456appKey";
		appInfo.clientId = "hij789clientId";
		appInfo.clientKey = "klm012clientKey";
		appInfo.debug = false;
		unityChannelLoginHandler = new UnityChannelLoginHandler();
		unityChannelLoginHandler.initializeFailedAction = delegate(string message)
		{
			UnityEngine.Debug.LogError("Failed to initialize and login to UnityChannel: " + message);
		};
		unityChannelLoginHandler.initializeSucceededAction = delegate
		{
			initializeUnityIap();
		};
		StoreService.Initialize(appInfo, unityChannelLoginHandler);
	}

	private void OnTransactionsRestored(bool success)
	{
		UnityEngine.Debug.Log("Transactions restored.");
	}

	private void OnDeferred(Product item)
	{
		UnityEngine.Debug.Log("Purchase deferred: " + item.definition.id);
	}

	private void InitUI(IEnumerable<Product> items)
	{
		restoreButton.gameObject.SetActive(NeedRestoreButton());
		loginButton.gameObject.SetActive(NeedLoginButton());
		validateButton.gameObject.SetActive(NeedValidateButton());
		ClearProductUIs();
		restoreButton.onClick.AddListener(RestoreButtonClick);
		loginButton.onClick.AddListener(LoginButtonClick);
		validateButton.onClick.AddListener(ValidateButtonClick);
		versionText.text = "Unity version: " + Application.unityVersion + "\nIAP version: 1.17.0";
	}

	public void PurchaseButtonClick(string productID)
	{
		if (m_PurchaseInProgress)
		{
			UnityEngine.Debug.Log("Please wait, purchase in progress");
			return;
		}
		if (m_Controller == null)
		{
			UnityEngine.Debug.LogError("Purchasing is not initialized");
			return;
		}
		if (m_Controller.products.WithID(productID) == null)
		{
			UnityEngine.Debug.LogError("No product has id " + productID);
			return;
		}
		if (NeedLoginButton() && !m_IsLoggedIn)
		{
			UnityEngine.Debug.LogWarning("Purchase notifications will not be forwarded server-to-server. Login incomplete.");
		}
		m_PurchaseInProgress = true;
		m_Controller.InitiatePurchase(m_Controller.products.WithID(productID), "aDemoDeveloperPayload");
	}

	public void RestoreButtonClick()
	{
		if (m_IsCloudMoolahStoreSelected)
		{
			if (!m_IsLoggedIn)
			{
				UnityEngine.Debug.LogError("CloudMoolah purchase restoration aborted. Login incomplete.");
			}
			else
			{
				m_MoolahExtensions.RestoreTransactionID(delegate(RestoreTransactionIDState restoreTransactionIDState)
				{
					UnityEngine.Debug.Log("restoreTransactionIDState = " + restoreTransactionIDState.ToString());
					bool success = restoreTransactionIDState != RestoreTransactionIDState.RestoreFailed && restoreTransactionIDState != RestoreTransactionIDState.NotKnown;
					OnTransactionsRestored(success);
				});
			}
		}
		else if (m_IsSamsungAppsStoreSelected)
		{
			m_SamsungExtensions.RestoreTransactions(OnTransactionsRestored);
		}
		else if (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)
		{
			m_MicrosoftExtensions.RestoreTransactions();
		}
		else
		{
			m_AppleExtensions.RestoreTransactions(OnTransactionsRestored);
		}
	}

	public void LoginButtonClick()
	{
		if (!m_IsUnityChannelSelected)
		{
			UnityEngine.Debug.Log("Login is only required for the Xiaomi store");
			return;
		}
		unityChannelLoginHandler.loginSucceededAction = delegate(UserInfo userInfo)
		{
			m_IsLoggedIn = true;
			UnityEngine.Debug.LogFormat("Succeeded logging into UnityChannel. channel {0}, userId {1}, userLoginToken {2} ", userInfo.channel, userInfo.userId, userInfo.userLoginToken);
		};
		unityChannelLoginHandler.loginFailedAction = delegate(string message)
		{
			m_IsLoggedIn = false;
			UnityEngine.Debug.LogError("Failed logging into UnityChannel. " + message);
		};
		StoreService.Login(unityChannelLoginHandler);
	}

	public void ValidateButtonClick()
	{
		if (!m_IsUnityChannelSelected)
		{
			UnityEngine.Debug.Log("Remote purchase validation is only supported for the Xiaomi store");
			return;
		}
		string txId = m_LastTransactionID;
		m_UnityChannelExtensions.ValidateReceipt(txId, delegate(bool success, string signData, string signature)
		{
			UnityEngine.Debug.LogFormat("ValidateReceipt transactionId {0}, success {1}, signData {2}, signature {3}", txId, success, signData, signature);
		});
	}

	private void ClearProductUIs()
	{
		foreach (KeyValuePair<string, IAPDemoProductUI> productUI in m_ProductUIs)
		{
			UnityEngine.Object.Destroy(productUI.Value.gameObject);
		}
		m_ProductUIs.Clear();
	}

	private void AddProductUIs(Product[] products)
	{
		ClearProductUIs();
		RectTransform component = productUITemplate.GetComponent<RectTransform>();
		float height = component.rect.height;
		Vector3 vector = component.localPosition;
		contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)products.Length * height);
		foreach (Product product in products)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(productUITemplate.gameObject);
			gameObject.transform.SetParent(productUITemplate.transform.parent, worldPositionStays: false);
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.localPosition = vector;
			vector += Vector3.down * height;
			gameObject.SetActive(value: true);
			IAPDemoProductUI component3 = gameObject.GetComponent<IAPDemoProductUI>();
			component3.SetProduct(product, PurchaseButtonClick);
			m_ProductUIs[product.definition.id] = component3;
		}
	}

	private void UpdateProductUI(Product p)
	{
		if (m_ProductUIs.ContainsKey(p.definition.id))
		{
			m_ProductUIs[p.definition.id].SetProduct(p, PurchaseButtonClick);
		}
	}

	private void UpdateProductPendingUI(Product p, int secondsRemaining)
	{
		if (m_ProductUIs.ContainsKey(p.definition.id))
		{
			m_ProductUIs[p.definition.id].SetPendingTime(secondsRemaining);
		}
	}

	private bool NeedRestoreButton()
	{
		return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.tvOS || Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM || m_IsSamsungAppsStoreSelected || m_IsCloudMoolahStoreSelected;
	}

	private bool NeedLoginButton()
	{
		return m_IsUnityChannelSelected;
	}

	private bool NeedValidateButton()
	{
		return m_IsUnityChannelSelected;
	}

	private void LogProductDefinitions()
	{
		Product[] all = m_Controller.products.all;
		Product[] array = all;
		foreach (Product product in array)
		{
			UnityEngine.Debug.Log($"id: {product.definition.id}\nstore-specific id: {product.definition.storeSpecificId}\ntype: {product.definition.type.ToString()}\n");
		}
	}
}
