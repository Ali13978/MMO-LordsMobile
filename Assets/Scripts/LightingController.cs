using UnityEngine;

public class LightingController : MonoBehaviour
{
	public Light keyLight;

	public Light fillLight;

	public Color humans;

	public Color humansFill;

	public Color goblins;

	public Color goblinsFill;

	public Color skeletons;

	public Color skeletonsFill;

	public Color wolves;

	public Color wolvesFill;

	public Color orcs;

	public Color orcsFill;

	public void InitializeLighting()
	{
		if (MainController.worldScreen == WorldScreen.Upgrade || MainController.worldScreen == WorldScreen.Defence)
		{
			keyLight.color = humans;
			keyLight.intensity = 0.9f;
			keyLight.transform.rotation = Quaternion.Euler(new Vector3(38.063f, -68.39001f, -8.48f));
			fillLight.color = humansFill;
			fillLight.intensity = 0.5f;
			fillLight.transform.rotation = Quaternion.Euler(new Vector3(19.606f, 43.882f, 192.78f));
			return;
		}
		switch (EnemyPrefsController.TerrainSelected)
		{
		case CityTerrain.Goblins:
			keyLight.intensity = 0.95f;
			keyLight.color = goblins;
			keyLight.transform.rotation = Quaternion.Euler(new Vector3(38.063f, 226.61f, -8.48f));
			fillLight.intensity = 0.5f;
			fillLight.color = goblinsFill;
			fillLight.transform.rotation = Quaternion.Euler(new Vector3(19.606f, 122.3f, 192.78f));
			break;
		case CityTerrain.Wolves:
			keyLight.intensity = 0.8f;
			keyLight.color = wolves;
			keyLight.transform.rotation = Quaternion.Euler(new Vector3(38.063f, 226.61f, -8.48f));
			fillLight.intensity = 0.38f;
			fillLight.color = wolvesFill;
			fillLight.transform.rotation = Quaternion.Euler(new Vector3(19.606f, 122.3f, 192.78f));
			break;
		case CityTerrain.Skeletons:
			keyLight.intensity = 0.8f;
			keyLight.color = skeletons;
			keyLight.transform.rotation = Quaternion.Euler(new Vector3(25.215f, 233.022f, 3.987f));
			fillLight.intensity = 0.59f;
			fillLight.color = skeletonsFill;
			fillLight.transform.rotation = Quaternion.Euler(new Vector3(19.606f, 122.3f, 192.78f));
			break;
		case CityTerrain.Orcs:
			keyLight.intensity = 1.16f;
			keyLight.color = orcs;
			keyLight.transform.rotation = Quaternion.Euler(new Vector3(38.063f, 226.61f, -8.48f));
			fillLight.intensity = 0.5f;
			fillLight.color = orcsFill;
			fillLight.transform.rotation = Quaternion.Euler(new Vector3(19.606f, 122.3f, 192.78f));
			break;
		}
	}
}
