using UnityEngine;

public class WaveRoman : MonoBehaviour
{
	private Faction[] unitsFactionSelected = new Faction[18];

	private ArmyType[] unitTypeSelected = new ArmyType[18];

	private int[] unitIndexSelected = new int[18];

	private int[] unitLevelSelected = new int[18];

	public Faction[] UnitsFactionSelected => unitsFactionSelected;

	public ArmyType[] UnitTypeSelected => unitTypeSelected;

	public int[] UnitIndexSelected => unitIndexSelected;

	public int[] UnitLevelSelected => unitLevelSelected;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void SetData(Faction[] _unitsFactionSelected, ArmyType[] _unitTypeSelected, int[] _unitIndexSelected, int[] _unitLevelSelected)
	{
		unitsFactionSelected = _unitsFactionSelected;
		unitTypeSelected = _unitTypeSelected;
		unitIndexSelected = _unitIndexSelected;
		unitLevelSelected = _unitLevelSelected;
	}
}
