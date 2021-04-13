using System;
using System.Collections.Generic;
using System.Linq;
using TheMazeAdventure.DataContracts;
using TheMazeAdventure.DataContracts.Enum;
using TheMazeAdventure.Utils;

namespace TheMazeAdventure.Integration
{
	public class MazeIntegration : IMazeIntegration
	{
		Random _random;
		Maze _maze = null;
		public void BuildMaze(int size)
		{
			if (size <= 1)
			{
				Console.WriteLine("Please provide value greater than 1, an exception will be thrown");
				throw new Exception("Invalid Size provided");
			}

			//Lets start with a 2*2 maze and for directions, lets take entry direction as north
			//As our maze is a 2d array, lets assume entry room will be one from first row.

			//Initial SetUp
			_random = new Random();
			int roomIdSeed = 1;
			_maze = new Maze();
			_maze.size = size;
			Console.WriteLine($"A new maze of size {size} * {size} has been created");
			_maze.Rooms = new Room[size, size];


			//Lets decide on the starting room first
			Coordinate entranceRoomCoordinate = CreateEntranceRoom(size);

			//lets set the treasure room now, eg for a 3*3 maze it can be on row 1 and 2 assuming a 0 based index
			Coordinate treasureRoomCoordinate = CreateTreasureRoom(size);


			//Create the maze
			for (int row = 0; row < size; row++)
			{
				for (int column = 0; column < size; column++)
				{
					if (row == entranceRoomCoordinate.Row && column == entranceRoomCoordinate.Column)
					{
						_maze.Entrance = _maze.Rooms[row, column] = new Room() { Id = roomIdSeed, Type = RoomType.Entrance, coordinate = new Coordinate() { Row = row, Column = column } };
						Console.WriteLine($"Id of entrance room will be {roomIdSeed}");
						roomIdSeed++;
						continue;
					}
					if (row == treasureRoomCoordinate.Row && column == treasureRoomCoordinate.Column)
					{
						_maze.Rooms[row, column] = new Room() { Id = roomIdSeed, Type = RoomType.Treasure, HasTreasure = true, coordinate = new Coordinate() { Row = row, Column = column } };
						Console.WriteLine($"Id of treasure room will be {roomIdSeed}");
						roomIdSeed++;
						continue;
					}
					int randomRoomType = _random.Next(0, 4);
					_maze.Rooms[row, column] = new Room() { Id = roomIdSeed, Type = (RoomType)randomRoomType, coordinate = new Coordinate() { Row = row, Column = column }, CausesInjury = RoomUtils.CausesInjury((RoomType)randomRoomType) };
					roomIdSeed++;
				}
			}
			Console.WriteLine($"Maze has been created successfully");
		}

		public bool CausesInjury(int roomId)
		{
			return GetRoomById(roomId).CausesInjury;
		}

		public string GetDescription(int roomId)
		{
			var room = GetRoomById(roomId);
			Console.WriteLine($"You have reached to room with id {roomId}, Coordinate of this room are {GetCoordinateDescription(room.coordinate)}");
			return RoomUtils.GetRoomDescription(room.CausesInjury, room.Type);
		}

		public int GetEntranceRoom()
		{
			Console.WriteLine($"An Adventurer has entered the maze");
			return _maze.Entrance.Id;
		}

		public int? GetRoom(int roomId, char direction)
		{
			//Here are the movement combinations
			//if you are on the first row, you cant move west
			//if you are on the last row, you can not move north
			//if you are on the first col you can not move south
			//if you are on the last col you can not move east

			//more precisely coordinate wise you can not move below the 0 and and size-1 in row/col coordinates
			//the input chars will come in "N", "E", "W" , "S"

			//N Means row to the TOP
			//S means row to the Bottom
			//E Means column to the Right
			//W means column to the left

			//Get the current room coordinates
			Room currentRoom = GetRoomsInMaze().Where(x => x.Id == roomId).FirstOrDefault();
			Coordinate currentRoomCoordinate = currentRoom.coordinate;
			Coordinate expectedRoomCoordinate = new Coordinate() { Row = currentRoomCoordinate.Row, Column = currentRoomCoordinate.Column };
			switch (direction)
			{
				case 'N':
					expectedRoomCoordinate.Row = currentRoomCoordinate.Row + 1;
					return GetRoomIdByCoordinates(expectedRoomCoordinate);
				case 'S':
					expectedRoomCoordinate.Row = currentRoomCoordinate.Row - 1;
					return GetRoomIdByCoordinates(expectedRoomCoordinate);
				case 'E':
					expectedRoomCoordinate.Column = currentRoomCoordinate.Column + 1;
					return GetRoomIdByCoordinates(expectedRoomCoordinate);
				case 'W':
					expectedRoomCoordinate.Column = currentRoomCoordinate.Column - 1;
					return GetRoomIdByCoordinates(expectedRoomCoordinate);
				default:
					return null;
			}
		}

		public bool HasTreasure(int roomId)
		{
			return GetRoomById(roomId).HasTreasure;
		}

		private Coordinate CreateTreasureRoom(int size)
		{
			Coordinate treasureRoomCoordinate = new Coordinate();
			treasureRoomCoordinate.Row = _random.Next(1, size - 1);
			treasureRoomCoordinate.Column = _random.Next(0, size - 1);
			Console.WriteLine($"Coordinate of treasure room are {GetCoordinateDescription(treasureRoomCoordinate)}");
			return treasureRoomCoordinate;
		}

		private Coordinate CreateEntranceRoom(int size)
		{
			Coordinate entranceRoomCoordinate = new Coordinate();
			entranceRoomCoordinate.Row = 0; //This will always be 0
			entranceRoomCoordinate.Column = _random.Next(0, size - 1);
			Console.WriteLine($"Coordinate of entrance room are {GetCoordinateDescription(entranceRoomCoordinate)}");
			return entranceRoomCoordinate;
		}

		private List<Room> GetRoomsInMaze()
		{
			return _maze?.Rooms.Cast<Room>().ToList() ?? new List<Room>();
		}

		private Room GetRoomById(int roomId)
		{
			return GetRoomsInMaze().Where(x => x.Id == roomId).FirstOrDefault();
		}

		private int? GetRoomIdByCoordinates(Coordinate roomCoordinate)
		{
			return (GetRoomsInMaze().Where(x => x.coordinate.Row == roomCoordinate.Row && x.coordinate.Column == roomCoordinate.Column).FirstOrDefault()?.Id) ?? null;
		}

		private string GetCoordinateDescription(Coordinate coordinate)
		{
			return $" Row: {coordinate.Row}, Column:{coordinate.Column} ";
		}
	}
}
