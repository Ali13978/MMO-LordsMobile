using BackendlessAPI;
using UnityEngine;

public class BackendlessPlugin : MonoBehaviour
{
	public enum SERVER
	{
		BACKENDLESS,
		GMO_MBAAS
	}

	[SerializeField]
	private SERVER Server;

	[SerializeField]
	private string applicationId;

	[SerializeField]
	private string RestSecretKey;

	[SerializeField]
	private string version;

	private void Awake()
	{
		Object.DontDestroyOnLoad(this);
		if (Server == SERVER.GMO_MBAAS)
		{
			Backendless.setUrl("https://api.gmo-mbaas.com");
		}
		else
		{
			Backendless.setUrl("https://api.backendless.com");
		}
		Backendless.InitApp(applicationId, RestSecretKey, version);
	}
}
