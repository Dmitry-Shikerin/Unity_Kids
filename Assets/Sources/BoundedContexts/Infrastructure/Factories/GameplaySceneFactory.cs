using Cysharp.Threading.Tasks;
using Sources.BoundedContexts.Controllers;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.BoundedContexts.Infrastructure.Factories
{
    public class GameplaySceneFactory : ISceneFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICompositeAssetService _compositeAssetService;
        private readonly IEcsGameStartUp _ecsGameStartUp;
        private readonly ILocalizationService _localizationService;
        private readonly IUpdateService _updateService;

        public GameplaySceneFactory(
            IEntityRepository entityRepository,
            ICompositeAssetService compositeAssetService,
            IEcsGameStartUp ecsGameStartUp,
            ILocalizationService localizationService,
            IUpdateService updateService)
        {
            _entityRepository = entityRepository;
            _compositeAssetService = compositeAssetService;
            _ecsGameStartUp = ecsGameStartUp;
            _localizationService = localizationService;
            _updateService = updateService;
        }

        public UniTask<IScene> Create(object payload)
        {
            IScene gameplayScene = new GameplayScene(
                _entityRepository,
                _compositeAssetService,
                _ecsGameStartUp,
                _localizationService,
                _updateService);

            return UniTask.FromResult(gameplayScene);
        }
    }
}