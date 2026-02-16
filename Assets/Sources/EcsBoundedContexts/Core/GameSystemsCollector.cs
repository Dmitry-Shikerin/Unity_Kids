using System.Collections.Generic;
using Leopotam.EcsProto;
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
			GameOverSystem gameOverSystem, //Order: 68 //Common
			TimerSystem timerSystem, //Order: 79 //Ability
			ActiveGameObjectSystem activeGameObjectSystem //Order: 84 //Common
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				gameOverSystem, //Common
				timerSystem, //Ability
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
