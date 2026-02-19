using Sources.EcsBoundedContexts.Common.Extansions.Colliders;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Zenject;

namespace Sources.BoundedContexts.Controllers
{
    public class GameplayScene : IScene
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICompositeAssetService _compositeAssetService;
        private readonly IEcsGameStartUp _ecsGameStartUp;
        private readonly ILocalizationService _localizationService;
        private readonly IUpdateService _updateService;

        public GameplayScene(
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

        public async void Enter(object payload = null)
        {
            await _compositeAssetService.LoadAsync(ResourcesPrefabPath.ResourcesAssetsConfig, AddressablesPrefabPath.AddressablesAssetConfig);
            ColliderExt.Construct(_entityRepository);
            _localizationService.Translate(LocalizationLanguage.Russian);
            _ecsGameStartUp.Initialize();
        }

        public void Exit()
        {
            _ecsGameStartUp.Destroy();
            _compositeAssetService.Release();
        }

        public void Update(float deltaTime)
        {
            _updateService.Update(deltaTime);
            _ecsGameStartUp.Update(deltaTime);
        }

        public void UpdateLate(float deltaTime)
        {
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }
    }
}