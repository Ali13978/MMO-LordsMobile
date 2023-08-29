using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CityIcon : MonoBehaviour
{
	private int cityIndex;

	public CityTerrain cityTerrain;

	public Faction cityFaction;

	public int cityStrength;

	public bool isConquered;

	public bool isVisible;

	public CityCharacter cityCharacter;

	public int cityCharacterIndex;

	public GameObject[] prefabCharacterIcon = new GameObject[2];

	public GameObject[] heroIcon = new GameObject[4];

	public Sprite[] spriteIcon;

	public Sprite[] spriteUpgrade;

	public CityIcon[] citiesConnected;

	public Text labelText;

	public SpriteRenderer labelRenderer;

	public SpriteRenderer[] rendererUpgrade;

	private BoxCollider myCollider;

	public SpriteRenderer bannerRenderer;

	private Transform bannerTransform;

	private Transform myTransform;

	private UIMapController uiMapController;

	private TutorialController tutorialController;

	public int CityIndex
	{
		get
		{
			return cityIndex;
		}
		set
		{
			cityIndex = value;
		}
	}

	public Transform MyTransform
	{
		get
		{
			return myTransform;
		}
		set
		{
			myTransform = value;
		}
	}

	private void Start()
	{
	}

	private void OnMouseUpAsButton()
	{
		if (!Touch_Map.IsWindowOpen && !IsPointerOverUIObject() && (tutorialController.GetActualIndex() != 25 || cityIndex == 1))
		{
			UIMapController.CitySelected = cityIndex;
			if (isConquered)
			{
				uiMapController.ButtonCityUpgrade(this);
			}
			else
			{
				EnemyPrefsController.SetLevel(cityIndex, cityFaction, cityStrength, cityCharacter, cityCharacterIndex, cityTerrain);
				uiMapController.ButtonCityAttack();
			}
			if (tutorialController.GetActualIndex() == 25 && cityIndex == 1)
			{
				tutorialController.ActivateStep(26);
			}
		}
	}

	private bool IsPointerOverUIObject()
	{
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		PointerEventData pointerEventData2 = pointerEventData;
		Vector3 mousePosition = UnityEngine.Input.mousePosition;
		float x = mousePosition.x;
		Vector3 mousePosition2 = UnityEngine.Input.mousePosition;
		pointerEventData2.position = new Vector2(x, mousePosition2.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, list);
		return list.Count > 0;
	}

	public void InitializeComponents()
	{
		bannerTransform = bannerRenderer.GetComponent<Transform>();
		myTransform = base.gameObject.GetComponent<Transform>();
		GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");
		uiMapController = gameObject.GetComponent<UIMapController>();
		tutorialController = gameObject.GetComponent<TutorialController>();
		myCollider = base.gameObject.GetComponent<BoxCollider>();
	}

	public void SetStatus()
	{
		if (isVisible)
		{
			bannerRenderer.enabled = true;
			if (cityIndex != 0)
			{
				if (isConquered)
				{
					bannerRenderer.sprite = spriteIcon[0];
					labelText.text = string.Empty;
					labelRenderer.gameObject.SetActive(value: false);
					if (PlayerPrefsController.CitiesLevel[CityIndex] < 4)
					{
						bannerTransform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
						bannerTransform.localPosition = Vector3.zero;
						for (int i = 0; i < rendererUpgrade.Length; i++)
						{
							rendererUpgrade[i].enabled = true;
							if (i <= PlayerPrefsController.CitiesLevel[CityIndex])
							{
								rendererUpgrade[i].sprite = spriteUpgrade[1];
							}
						}
					}
					else
					{
						bannerTransform.localScale = new Vector3(1f, 1f, 1f);
						bannerTransform.localPosition = new Vector3(0f, 0.16f, 0f);
						for (int j = 0; j < rendererUpgrade.Length; j++)
						{
							rendererUpgrade[j].enabled = false;
						}
					}
				}
				else
				{
					bannerRenderer.sprite = spriteIcon[(int)cityFaction];
					labelText.text = cityStrength.ToString();
					for (int k = 0; k < rendererUpgrade.Length; k++)
					{
						rendererUpgrade[k].enabled = false;
					}
				}
			}
			else
			{
				labelText.text = string.Empty;
				labelRenderer.gameObject.SetActive(value: false);
				for (int l = 0; l < rendererUpgrade.Length; l++)
				{
					rendererUpgrade[l].enabled = false;
				}
			}
			if (PlayerPrefsController.CitiesConquered[cityIndex] || cityCharacter == CityCharacter.None)
			{
				return;
			}
			GameObject gameObject = null;
			if (cityCharacter == CityCharacter.Hero)
			{
				switch (cityCharacterIndex)
				{
				case 0:
					gameObject = UnityEngine.Object.Instantiate(heroIcon[0]);
					break;
				case 1:
					gameObject = UnityEngine.Object.Instantiate(heroIcon[1]);
					break;
				case 2:
					gameObject = UnityEngine.Object.Instantiate(heroIcon[2]);
					break;
				case 3:
					gameObject = UnityEngine.Object.Instantiate(heroIcon[3]);
					break;
				}
			}
			else if (cityCharacter == CityCharacter.Mercenary)
			{
				gameObject = UnityEngine.Object.Instantiate(prefabCharacterIcon[1]);
			}
			Transform component = gameObject.GetComponent<Transform>();
			component.SetParent(myTransform);
			component.localPosition = new Vector3(0f, 0.7f, 0f);
			component.localScale = new Vector3(1.25f, 1.25f, 1.25f);
			LeanTween.move(component.gameObject, component.position + new Vector3(0f, 0.075f, 0f), 1f).setEase(LeanTweenType.easeInOutSine).setIgnoreTimeScale(useUnScaledTime: true)
				.setLoopPingPong();
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
