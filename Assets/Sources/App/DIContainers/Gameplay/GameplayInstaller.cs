using Sirenix.OdinInspector;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Prefabs.Implementation.Composites;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using UnityEngine;
using Zenject;

namespace Sources.App.DIContainers.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [Required] [SerializeField] private RootGameObject _rootGameObject;
        
        public override void InstallBindings()
        {
            Container.Bind().FromInstance(_rootGameObject).AsSingle();
            
            Container.Bind<ISceneFactory>().To<GameplaySceneFactory>().AsSingle();
            Container.Bind<ICompositeAssetService>().To<GameplayAssetService>().AsSingle();
            
            //ECS
            Container.Bind<ISystemsCollector>().To<GameSystemsCollector>().AsSingle();
            GameSystemsInstaller.InstallBindings(Container);
        }
    }
}