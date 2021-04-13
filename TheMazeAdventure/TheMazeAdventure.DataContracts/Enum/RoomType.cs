using System.ComponentModel;

namespace TheMazeAdventure.DataContracts.Enum
{
	public enum RoomType
	{
		//Future: these texts can come from a resource file.		
		[Description("You are looking at a heap of leaves and feel slightly elated, Stay alert the forest is filled with wild animals.")]
		Forest = 0,
		[Description("You are looking at shollow waters, but be aware where you set your foot.")]
		Marsh = 1,
		[Description("You can see nothing but sand everywhere, make sure you stay hydrated.")]
		Desert = 2,
		[Description("You see big big hills and rocks all over, beware these rocks can tumble anytime.")]
		Hills = 3,
		[Description("You are looking at a fire breathing dragon, its sleeping for now but a slightest sound can wake him up.")]
		Dragon = 4,
		[Description("This is the starting point of the maze.")]
		Entrance = 5,
		[Description("Congratulations!, Your efforts have been rewarded, the treasure is all yours.")]
		Treasure = 6
	}
}
