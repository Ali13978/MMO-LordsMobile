namespace UnityEngine.Purchasing
{
	[AddComponentMenu("")]
	public class DemoInventory : MonoBehaviour
	{
		public void Fulfill(string productId)
		{
			switch (productId)
			{
			case "100.gold.coins":
				UnityEngine.Debug.Log("You Got Money!");
				break;
			default:
				UnityEngine.Debug.Log($"Unrecognized productId \"{productId}\"");
				break;
			}
		}
	}
}
