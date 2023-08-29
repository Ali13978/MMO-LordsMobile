using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUnitCard : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IScrollHandler
{
	private Image imageBackground;

	public Image imageCard;

	public Text textTitleCard;

	public Text textCostCard;

	private Faction selectedFaction;

	private ArmyType selectedType;

	private ArmyHeroType selectedHero;

	private ArmySoldierType selectedSoldier;

	private ArmyMercenaryType selectedMercenary;

	private int indexFormation;

	private int costCard;

	private int levelCard;

	private UIWindowAttack uiWindowAttack;

	private ScrollRect mainScroll;

	private float timeToStartCardDrag = 0.4f;

	private float timeToStartCardDragCounter;

	private float timeToMakeClick = 0.2f;

	private float timeToMakeClickCounter;

	private bool isDragging;

	private bool isPointerDown;

	public Faction SelectedFaction => selectedFaction;

	public ArmyType SelectedType => selectedType;

	public ArmyHeroType SelectedHero => selectedHero;

	public ArmySoldierType SelectedSoldier => selectedSoldier;

	public ArmyMercenaryType SelectedMercenary => selectedMercenary;

	public int IndexFormation => indexFormation;

	public int CostCard => costCard;

	public int LevelCard => levelCard;

	public bool IsDragging
	{
		get
		{
			return isDragging;
		}
		set
		{
			isDragging = value;
		}
	}

	public bool IsPointerDown
	{
		get
		{
			return isPointerDown;
		}
		set
		{
			isPointerDown = value;
		}
	}

	private void Awake()
	{
		imageBackground = base.gameObject.GetComponent<Image>();
		uiWindowAttack = GameObject.FindGameObjectWithTag("MenuInvasion").GetComponent<UIWindowAttack>();
		mainScroll = GameObject.FindGameObjectWithTag("InvasionScroll").GetComponent<ScrollRect>();
	}

	public void SetData(Sprite _imageCard, string _textTitle, int _costCard, Faction _selectedFaction, ArmyType _selectedType, int _selectedIndex, int _levelCard, int _indexFormation)
	{
		imageCard.sprite = _imageCard;
		textTitleCard.text = _textTitle;
		costCard = _costCard;
		textCostCard.text = costCard.ToString("###,###,##0");
		levelCard = _levelCard;
		indexFormation = _indexFormation;
		selectedFaction = _selectedFaction;
		selectedType = _selectedType;
		switch (_selectedType)
		{
		case ArmyType.Heroe:
			selectedHero = (ArmyHeroType)_selectedIndex;
			break;
		case ArmyType.Soldier:
			selectedSoldier = (ArmySoldierType)_selectedIndex;
			break;
		case ArmyType.Mercenary:
			selectedMercenary = (ArmyMercenaryType)_selectedIndex;
			break;
		}
	}

	private void StartDrag()
	{
		isDragging = true;
		int selectedIndex = 0;
		switch (selectedType)
		{
		case ArmyType.Heroe:
			selectedIndex = (int)selectedHero;
			break;
		case ArmyType.Mercenary:
			selectedIndex = (int)selectedMercenary;
			break;
		case ArmyType.Soldier:
			selectedIndex = (int)selectedSoldier;
			break;
		}
		uiWindowAttack.StartDragCard(imageCard.sprite, selectedType, selectedIndex);
	}

	private void FinishDrag()
	{
		isDragging = false;
		int selectedIndex = 0;
		switch (selectedType)
		{
		case ArmyType.Heroe:
			selectedIndex = (int)selectedHero;
			break;
		case ArmyType.Mercenary:
			selectedIndex = (int)selectedMercenary;
			break;
		case ArmyType.Soldier:
			selectedIndex = (int)selectedSoldier;
			break;
		}
		uiWindowAttack.ReleaseCard(imageCard.sprite, selectedFaction, selectedType, selectedIndex, costCard, levelCard, indexFormation);
	}

	private void Update()
	{
		if (isPointerDown)
		{
			timeToMakeClickCounter += Time.deltaTime;
		}
		else
		{
			timeToMakeClickCounter = 0f;
		}
		if (isDragging)
		{
			if (!isPointerDown)
			{
				timeToStartCardDragCounter = 0f;
				FinishDrag();
			}
		}
		else if (isPointerDown)
		{
			timeToStartCardDragCounter += Time.deltaTime;
			if (timeToStartCardDragCounter >= timeToStartCardDrag)
			{
				StartDrag();
			}
		}
		else
		{
			timeToStartCardDragCounter = 0f;
		}
	}

	public void PointerClickAdd()
	{
		if (!isDragging && !isPointerDown)
		{
			int selectedIndex = 0;
			switch (selectedType)
			{
			case ArmyType.Heroe:
				selectedIndex = (int)selectedHero;
				break;
			case ArmyType.Soldier:
				selectedIndex = (int)selectedSoldier;
				break;
			case ArmyType.Mercenary:
				selectedIndex = (int)selectedMercenary;
				break;
			}
			uiWindowAttack.ReleaseCard(imageCard.sprite, selectedFaction, selectedType, selectedIndex, costCard, levelCard, indexFormation, -2);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isPointerDown = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (timeToMakeClickCounter <= timeToMakeClick && timeToMakeClickCounter != 0f)
		{
			PointerClickAdd();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPointerDown = false;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isDragging)
		{
			isPointerDown = false;
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!isDragging)
		{
			mainScroll.OnBeginDrag(eventData);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!isDragging)
		{
			mainScroll.OnDrag(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!isDragging)
		{
			mainScroll.OnEndDrag(eventData);
		}
	}

	public void OnScroll(PointerEventData data)
	{
		if (!isDragging)
		{
			mainScroll.OnScroll(data);
		}
	}
}
