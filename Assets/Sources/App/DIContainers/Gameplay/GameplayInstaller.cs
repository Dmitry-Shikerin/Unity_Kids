using Sirenix.OdinInspector;
using Sources.BoundedContexts.Infrastructure.Factories;
using Sources.BoundedContexts.Presentation;
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
        [Required] [SerializeField] private HudView _hud;
        
        public override void InstallBindings()
        {
            Container.Bind<HudView>().FromInstance(_hud).AsSingle();
            
            Container.Bind<ISceneFactory>().To<GameplaySceneFactory>().AsSingle();
            Container.Bind<ICompositeAssetService>().To<GameplayAssetService>().AsSingle();
            
            Container.Bind<ISystemsCollector>().To<GameSystemsCollector>().AsSingle();
            GameSystemsInstaller.InstallBindings(Container);
        }
    }
}