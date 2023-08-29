using System;

namespace ExifLibrary
{
	[Flags]
	public enum Flash : ushort
	{
		FlashDidNotFire = 0x0,
		StrobeReturnLightNotDetected = 0x4,
		StrobeReturnLightDetected = 0x2,
		FlashFired = 0x1,
		CompulsoryFlashMode = 0x8,
		AutoMode = 0x10,
		NoFlashFunction = 0x20,
		RedEyeReductionMode = 0x40
	}
}
