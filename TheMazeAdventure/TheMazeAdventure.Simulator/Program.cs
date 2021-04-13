using System;
using TheMazeAdventure.Integration;
using Microsoft.Extensions.DependencyInjection;

namespace TheMazeAdventure.Simulator
{
	class Program
	{
		static void Main(string[] args)
		{
			var services = new ServiceCollection().AddSingleton<IMazeIntegration>(new MazeIntegration());
			var serviceProvider = services.BuildServiceProvider();

			IMazeIntegration mazeIntegration = serviceProvider.GetService<IMazeIntegration>();
			Console.WriteLine("Welcome Dungeon master, you can test your maze here");
			Console.WriteLine("Please provide a maze size, it should be greater than 1");
			int size = Convert.ToInt32(Console.ReadLine());
			mazeIntegration.BuildMaze(size);
			int entranceRoomId = mazeIntegration.GetEntranceRoom();
			Console.WriteLine("N,E,W,S will be used as which direction you want to go.");
			int nextRoomId = 0;
			NextMove:
			nextRoomId = MoveInDirectionIfValid(mazeIntegration, nextRoomId == 0 ? entranceRoomId : nextRoomId);
			if (!isGameEnded(mazeIntegration, nextRoomId))
			{
				goto NextMove;
			}
			Console.WriteLine("Press any key to exit..");
			Console.ReadKey();
		}

		static int MoveInDirectionIfValid(IMazeIntegration mazeIntegration, int currentRoomId)
		{
			Console.WriteLine("Please choose a direction for your next move, it can be one of 'N', 'E', 'W', 'S'");
			char direction = Convert.ToChar(Console.ReadLine());
			int nextRoomId = mazeIntegration.GetRoom(currentRoomId, direction) ?? currentRoomId;
			if (nextRoomId == currentRoomId)
			{
				Console.WriteLine("You have hit the wall, please choose any other direction");
				MoveInDirectionIfValid(mazeIntegration, currentRoomId);
			}
			Console.WriteLine(mazeIntegration.GetDescription(nextRoomId));
			return nextRoomId;
		}

		static bool isGameEnded(IMazeIntegration mazeIntegration, int currentRoomId)
		{
			if (mazeIntegration.CausesInjury(currentRoomId))
			{
				Console.WriteLine("You are dead, please restart");
				return true;
			}
			if (mazeIntegration.HasTreasure(currentRoomId))
			{
				Console.WriteLine("Congratulations on finding the treasure");
				return true;
			}
			return false;
		}
	}
}
