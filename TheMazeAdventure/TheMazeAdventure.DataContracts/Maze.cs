using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMazeAdventure.DataContracts
{
	public class Maze
	{
		public int size { get; set; }
		public Room[,] Rooms { get; set; }
		public Room Entrance { get; set; }
	}
}
