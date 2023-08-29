namespace ExifLibrary
{
	public enum MeteringMode : ushort
	{
		Unknown = 0,
		Average = 1,
		CenterWeightedAverage = 2,
		Spot = 3,
		MultiSpot = 4,
		Pattern = 5,
		Partial = 6,
		Other = 0xFF
	}
}
