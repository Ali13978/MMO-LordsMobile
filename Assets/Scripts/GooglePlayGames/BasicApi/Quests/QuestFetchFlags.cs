using System;

namespace GooglePlayGames.BasicApi.Quests
{
	[Flags]
	public enum QuestFetchFlags
	{
		Upcoming = 0x1,
		Open = 0x2,
		Accepted = 0x4,
		Completed = 0x8,
		CompletedNotClaimed = 0x10,
		Expired = 0x20,
		EndingSoon = 0x40,
		Failed = 0x80,
		All = -1
	}
}
