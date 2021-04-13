using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheMazeAdventure.Integration;
using Microsoft.Extensions.DependencyInjection;

namespace TheMazeAdventure.UnitTests
{
	[TestClass]
	public class MazeIntegrationTests
	{
		[TestMethod]
		[ExpectedException(typeof(Exception), "Invalid Size provided")]
		public void MinimumMazeSize()
		{
			//arrange
			var services = new ServiceCollection().AddSingleton<IMazeIntegration>(new MazeIntegration());
			var serviceProvider = services.BuildServiceProvider();
			IMazeIntegration mazeIntegration = serviceProvider.GetService<IMazeIntegration>();

			//act
			mazeIntegration.BuildMaze(1);
		}

		[TestMethod]
		
		public void Maze_Walls()
		{
			//arrange
			var services = new ServiceCollection().AddSingleton<IMazeIntegration>(new MazeIntegration());
			var serviceProvider = services.BuildServiceProvider();
			IMazeIntegration mazeIntegration = serviceProvider.GetService<IMazeIntegration>();

			//we are entering from north at the entrance room
			//if we try to go south from here we hit a wall
			//act
			mazeIntegration.BuildMaze(3);
			int entranceRoomId = mazeIntegration.GetEntranceRoom();

			//assert
			Assert.IsNull(mazeIntegration.GetRoom(entranceRoomId, 'S'));
		}
	}
}
