using Sources.BoundedContexts.Systems;
using Sources.BoundedContexts.Systems.GameBoards;
using Sources.BoundedContexts.Systems.Rectangles;
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
			container.Bind<GameBoardSystem>().AsSingle();
			container.Bind<RectangleBeginDragSystem>().AsSingle();
			container.Bind<RectangleEndDragSystem>().AsSingle();
			container.Bind<RectangleMoveSystem>().AsSingle();
			container.Bind<SlotSystem>().AsSingle();
			container.Bind<RectangleDestroySystem>().AsSingle();
			container.Bind<RectanglesDropSystem>().AsSingle();
			container.Bind<ConsoleSystem>().AsSingle();
			container.Bind<ActiveGameObjectSystem>().AsSingle();
			container.Bind<SaveGameSystem>().AsSingle();

			//EventBuffer

			//Rectangle

		}
	}
}
