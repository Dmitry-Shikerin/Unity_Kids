using System;
using Cysharp.Threading.Tasks;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Controllers;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Zenject;

namespace Sources.BoundedContexts.Scenes.Infrastructure.Factories
{
    public class GameplaySceneFactory : ISceneFactory
    {
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _entityRepository;
        private readonly DiContainer _container;
        private readonly RootGameObject _rootGameObject;
        private readonly ICompositeAssetService _compositeAssetService;
        private readonly ISoundService _soundService;
        private readonly IEcsGameStartUp _ecsGameStartUp;
        private readonly ILocalizationService _localizationService;
        private readonly ICurtainView _curtainView;
        private readonly IUpdateService _updateService;

        public GameplaySceneFactory(
            IAssetCollector assetCollector,
            IEntityRepository entityRepository,
            DiContainer container,
            RootGameObject rootGameObject,
            ICompositeAssetService compositeAssetService,
            ISoundService soundService,
            IEcsGameStartUp ecsGameStartUp,
            ILocalizationService localizationService,
            ICurtainView curtainView,
            IUpdateService updateService)
        {
            _assetCollector = assetCollector;
            _entityRepository = entityRepository;
            _container = container;
            _rootGameObject = rootGameObject;
            _compositeAssetService = compositeAssetService;
            _soundService = soundService;
            _ecsGameStartUp = ecsGameStartUp;
            _localizationService = localizationService;
            _curtainView = curtainView;
            _updateService = updateService;
        }

        public UniTask<IScene> Create(object payload)
        {
            IScene gameplayScene = new GameplayScene(
                _assetCollector,
                _entityRepository,
                _container,
                _rootGameObject,
                _compositeAssetService,
                _soundService,
                _ecsGameStartUp,
                _localizationService,
                _curtainView,
                _updateService);

            return UniTask.FromResult(gameplayScene);
        }
    }
}