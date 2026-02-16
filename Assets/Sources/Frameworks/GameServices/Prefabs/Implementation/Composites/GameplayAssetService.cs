using System;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.EntityPools.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.Frameworks.GameServices.Prefabs.Implementation.Composites
{
    public class GameplayAssetService : CompositeAssetService
    {
        private readonly IAddressablesAssetLoader _addressablesAssetLoader;
        private readonly IResourcesAssetLoader _resourcesAssetLoader;
        private readonly IAddressablesAssetLoader[] _assetServices;

        public GameplayAssetService(
            IAddressablesAssetLoader addressablesAssetLoader,
            IResourcesAssetLoader resourcesAssetLoader)
            : base(
                addressablesAssetLoader,
                resourcesAssetLoader)
        {
            _addressablesAssetLoader = addressablesAssetLoader ??
                                       throw new ArgumentNullException(nameof(addressablesAssetLoader));
            _resourcesAssetLoader = resourcesAssetLoader ??
                                    throw new ArgumentNullException(nameof(resourcesAssetLoader));
        }
        
        protected override async UniTask LoadByResourcesConfigAsync(string assetCollectorPath)
        {
            if (string.IsNullOrEmpty(assetCollectorPath))
                return;

            if (string.IsNullOrWhiteSpace(assetCollectorPath))
                return;
        }

        protected override async UniTask LoadByResourcesFoldersAsync()
        {
            await UniTask.WhenAll
            (
                _resourcesAssetLoader.LoadAsset<PoolManagerCollector>(ResourcesPrefabPath.PoolManagerCollector)
            );
        }
        
        protected override async UniTask LoadByAddressableConfigAsync(string addressablesCollectorPath)
        {
            AddressablesAssetConfig config = await _addressablesAssetLoader.LoadAsset<AddressablesAssetConfig>(addressablesCollectorPath);
            
            //await AddressalesLoad(config.UiConfig);
            
            //Prefabs
            //await AddressalesPrefabLoad<CharacterMeleeModule>(config.CharacterMeleeModule);
        }
    }
}