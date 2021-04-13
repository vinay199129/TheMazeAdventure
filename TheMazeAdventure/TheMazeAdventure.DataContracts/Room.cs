using TheMazeAdventure.DataContracts.Enum;

namespace TheMazeAdventure.DataContracts
{
	public class Room
	{
		public int Id { get; set; }
		public Coordinate coordinate { get; set; }
		public bool HasTreasure { get; set; }
		public bool CausesInjury { get; set; }
		public RoomType Type { get; set; }
		public TrapType Trap { get; set; }
		public string Description { get; set; }
	}
}