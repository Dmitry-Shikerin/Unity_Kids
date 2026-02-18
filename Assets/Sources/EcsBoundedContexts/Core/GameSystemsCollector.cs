using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.BoundedContexts.Systems;
using Sources.BoundedContexts.Systems.Rectangles;
using Sources.BoundedContexts.Systems.GameBoards;
using Sources.EcsBoundedContexts.GameOvers.Infrastructure.Controllers;
using Sources.EcsBoundedContexts.Timers.Infrastructure;
using Sources.EcsBoundedContexts.GameObjects.Controllers;

namespace Sources.EcsBoundedContexts.Core
{
	public class GameSystemsCollector : ISystemsCollector
	{
		private readonly ProtoSystems _protoSystems;
		private readonly IEnumerable<IProtoSystem> _systems;

		public GameSystemsCollector(
			ProtoSystems protoSystems,
			GameInitializeSystem gameInitializeSystem, //Order: 5 //Common
			RectangleBeginDragSystem rectangleBeginDragSystem, //Order: 20 //Common
			RectangleEndDragSystem rectangleEndDragSystem, //Order: 21 //Common
			GameBoardSystem gameBoardSystem, //Order: 24 //Common
			RectangleMoveSystem rectangleMoveSystem, //Order: 26 //Common
			SlotSystem slotSystem, //Order: 28 //Common
			RectangleDestroySystem rectangleDestroySystem, //Order: 30 //Common
			RectanglesDropSystem rectanglesDropSystem, //Order: 30 //Common
			GameOverSystem gameOverSystem, //Order: 68 //Common
			TimerSystem timerSystem, //Order: 79 //Common
			ActiveGameObjectSystem activeGameObjectSystem //Order: 84 //Common
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				gameInitializeSystem, //Common
				rectangleBeginDragSystem, //Common
				rectangleEndDragSystem, //Common
				gameBoardSystem, //Common
				rectangleMoveSystem, //Common
				slotSystem, //Common
				rectangleDestroySystem, //Common
				rectanglesDropSystem, //Common
				gameOverSystem, //Common
				timerSystem, //Common
				activeGameObjectSystem, //Common
			};
		}

		public void AddSystems()
		{
			foreach (IProtoSystem system in _systems)
				_protoSystems.AddSystem(system);
		}
	}
}
