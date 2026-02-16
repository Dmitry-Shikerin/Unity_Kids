using Sources.BoundedContexts.Systems;
using Sources.EcsBoundedContexts.GameOvers.Infrastructure.Controllers;
using Sources.EcsBoundedContexts.Timers.Infrastructure;
using Sources.EcsBoundedContexts.GameObjects.Controllers;
using Zenject;

namespace Sources.EcsBoundedContexts.Core
{
	public static class GameSystemsInstaller
	{
		public static void InstallBindings(DiContainer container)
		{
			//Default

			//Common
			container.Bind<GameInitializeSystem>().AsSingle();
			container.Bind<GameOverSystem>().AsSingle();
			container.Bind<TimerSystem>().AsSingle();
			container.Bind<ActiveGameObjectSystem>().AsSingle();

			//EventBuffer

			//Rectangle

		}
	}
}
