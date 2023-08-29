using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

public class StationEngineIAP : MonoBehaviour, IStoreListener
{
	private const string keyPrefsCurrency = "stationEngine_IapCurrency";

	private const string keyPrefsPrice = "stationEngine_IapPrice";

	private static IStoreController m_StoreController;

	private static IExtensionProvider m_StoreExtensionProvider;

	private StationEngine stationEngine;

	private bool isDebugEnabled;

	private string[] namesIAP;

	private string[] currencyIAP;

	private string[] pricesIAP;

	private string[] skuIAP;

	private StationEngine.ComponentStatus actualStatus;

	public void Initialize(StationEngine stationEngine, string secKey, string[] namesIAP, string[] skusIAP, bool isDebugEnabled)
	{
		if (!IsInitialized() && actualStatus != StationEngine.ComponentStatus.INITIALIZING)
		{
			actualStatus = StationEngine.ComponentStatus.INITIALIZING;
			this.stationEngine = stationEngine;
			this.namesIAP = namesIAP;
			skuIAP = skusIAP;
			this.isDebugEnabled = isDebugEnabled;
			if (this.isDebugEnabled)
			{
				stationEngine.PostDebugInfo("UNITY IAP - Initializing");
			}
			SetDefaultPrices();
			InitializeUnityPurchasing();
		}
	}

	public StationEngine.ComponentStatus GetStatus()
	{
		return actualStatus;
	}

	public void SetStatusTimeOut()
	{
		actualStatus = StationEngine.ComponentStatus.TIME_OUT;
	}

	public void PurchaseIAP(int skuIndex)
	{
		if (IsInitialized())
		{
			Product product = m_StoreController.products.WithStoreSpecificID(skuIAP[skuIndex]);
			if (product != null && product.availableToPurchase)
			{
				if (isDebugEnabled)
				{
					stationEngine.PostDebugInfo($"Purchasing product asychronously: '{product.definition.id}'");
				}
				m_StoreController.InitiatePurchase(product);
			}
			else
			{
				stationEngine.PostDebugError("UNITY IAP - FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			stationEngine.PostDebugError("UNITY IAP - FAIL. Not initialized.");
		}
	}

	public string GetPrice(int IAPIndex)
	{
		return pricesIAP[IAPIndex];
	}

	public void SetPrices()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("UNITY IAP - Setting prices");
		}
		currencyIAP = new string[base.name.Length];
		pricesIAP = new string[base.name.Length];
		if (!CreateBuilder().products.Any())
		{
			return;
		}
		ProductDefinition[] array = CreateBuilder().products.ToArray();
		for (int i = 0; i < namesIAP.Length; i++)
		{
			if (array.Any())
			{
				string id = array[i].id;
				Product product = m_StoreController.products.WithStoreSpecificID(skuIAP[i]);
				ProductMetadata metadata = product.metadata;
				currencyIAP[i] = metadata.isoCurrencyCode;
				pricesIAP[i] = metadata.localizedPriceString;
				PlayerPrefs.SetString("stationEngine_IapCurrency_" + i.ToString(), currencyIAP[i]);
				PlayerPrefs.SetString("stationEngine_IapPrice_" + i.ToString(), pricesIAP[i]);
				PlayerPrefs.Save();
			}
		}
	}

	private void SetDefaultPrices()
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("UNITY IAP - Setting default prices");
		}
		currencyIAP = new string[base.name.Length];
		pricesIAP = new string[base.name.Length];
		for (int i = 0; i < namesIAP.Length; i++)
		{
			if (PlayerPrefs.HasKey("stationEngine_IapPrice_" + i.ToString()))
			{
				currencyIAP[i] = PlayerPrefs.GetString("stationEngine_IapCurrency_" + i.ToString());
				pricesIAP[i] = PlayerPrefs.GetString("stationEngine_IapPrice_" + i.ToString());
			}
			else
			{
				currencyIAP[i] = string.Empty;
				pricesIAP[i] = string.Empty;
			}
		}
	}

	private void ConsumeIAP(int productIndex)
	{
		if (isDebugEnabled)
		{
			stationEngine.PostDebugInfo("UNITY IAP - Consuming IAP index: " + productIndex.ToString());
		}
		PlayerPrefs.SetInt("playerRubies", PlayerPrefs.GetInt("playerRubies") + ConfigPrefsController.storeRubyPackAmount[productIndex]);
		PlayerPrefs.SetInt("vipUser", 1);
		PlayerPrefs.Save();
		if (StoreUIController.instance != null)
		{
			StoreUIController.instance.UpdateWindow();
		}
		if (UIController.instance != null)
		{
			UIController.instance.UpdateUIUpgrade();
		}
	}

	private void InitializeUnityPurchasing()
	{
		if (!IsInitialized())
		{
			UnityPurchasing.Initialize(this, CreateBuilder());
		}
	}

	private ConfigurationBuilder CreateBuilder()
	{
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		HashSet<ProductDefinition> hashSet = new HashSet<ProductDefinition>();
		for (int i = 0; i < skuIAP.Length; i++)
		{
			hashSet.Add(new ProductDefinition(namesIAP[i], skuIAP[i], ProductType.Consumable));
		}
		ReadOnlyCollection<ProductDefinition> products = new ReadOnlyCollection<ProductDefinition>(hashSet.ToList());
		configurationBuilder.AddProducts(products);
		return configurationBuilder;
	}

	private bool IsInitialized()
	{
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		stationEngine.PostDebugInfo("OnInitialized: PASS");
		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
		actualStatus = StationEngine.ComponentStatus.INITIALIZED;
		SetPrices();
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		stationEngine.PostDebugInfo("OnInitializeFailed InitializationFailureReason:" + error);
		actualStatus = StationEngine.ComponentStatus.ERROR;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		stationEngine.PostDebugInfo($"OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}");
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		string storeSpecificId = args.purchasedProduct.definition.storeSpecificId;
		int productIndex = 0;
		for (int i = 0; i < skuIAP.Length; i++)
		{
			if (skuIAP[i] == storeSpecificId)
			{
				productIndex = i;
			}
		}
		ConsumeIAP(productIndex);
		return PurchaseProcessingResult.Complete;
	}
}
