using System.ComponentModel;

namespace TheMazeAdventure.DataContracts.Enum
{
	public enum TrapType
	{
		//Future: these texts can come from a resource file.
		[Description("You are safe for now stay alert.")]
		None = 0,
		[Description("Wild Animals of forest have caught your scent, you are being attacked.")]
		Forest = 1,
		[Description("You are stuck into the marsh and have begun to drown.")]
		Marsh = 2,
		[Description("Your water levels are low, you have started to dehydrate.")]
		Desert = 3,
		[Description("Rocks have started tumbling, run for your life.")]
		Hills = 4,
		[Description("You have awoken the mighty smaug, say your last prayers.")]
		Dragon = 5,
	}
}
