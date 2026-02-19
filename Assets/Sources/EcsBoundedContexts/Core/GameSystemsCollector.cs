using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.BoundedContexts.Systems;
using Sources.BoundedContexts.Systems.GameBoards;
using Sources.BoundedContexts.Systems.Rectangles;
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
			GameBoardSystem gameBoardSystem, //Order: 19 //Common
			RectangleBeginDragSystem rectangleBeginDragSystem, //Order: 20 //Common
			RectangleEndDragSystem rectangleEndDragSystem, //Order: 21 //Common
			RectangleMoveSystem rectangleMoveSystem, //Order: 26 //Common
			SlotSystem slotSystem, //Order: 28 //Common
			RectangleDestroySystem rectangleDestroySystem, //Order: 30 //Common
			RectanglesDropSystem rectanglesDropSystem, //Order: 30 //Common
			ConsoleSystem consoleSystem, //Order: 32 //Common
			ActiveGameObjectSystem activeGameObjectSystem, //Order: 84 //Common
			SaveGameSystem saveGameSystem //Order: 100 //Common
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				gameInitializeSystem, //Common
				gameBoardSystem, //Common
				rectangleBeginDragSystem, //Common
				rectangleEndDragSystem, //Common
				rectangleMoveSystem, //Common
				slotSystem, //Common
				rectangleDestroySystem, //Common
				rectanglesDropSystem, //Common
				consoleSystem, //Common
				activeGameObjectSystem, //Common
				saveGameSystem, //Common
			};
		}

		public void AddSystems()
		{
			foreach (IProtoSystem system in _systems)
				_protoSystems.AddSystem(system);
		}
	}
}
