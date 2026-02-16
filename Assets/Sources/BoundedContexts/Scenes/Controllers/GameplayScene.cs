using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Common.Extansions.Colliders;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;
using Zenject;

namespace Sources.BoundedContexts.Scenes.Controllers
{
    public class GameplayScene : IScene
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

        public GameplayScene(
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

        public async void Enter(object payload = null)
        {
            await _compositeAssetService.LoadAsync(ResourcesPrefabPath.ResourcesAssetsConfig, AddressablesPrefabPath.AddressablesAssetConfig);
            ColliderExt.Construct(_entityRepository);
            _localizationService.Translate();
            _ecsGameStartUp.Initialize();
            _soundService.Initialize();
            _soundService.Play(SoundDatabaseName.Music, SoundName.GameplayBackgroundMusic);
            await _curtainView.HideAsync();
        }

        public void Exit()
        {
            _soundService.Stop(SoundName.GameplayBackgroundMusic);
            _soundService.Destroy();
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