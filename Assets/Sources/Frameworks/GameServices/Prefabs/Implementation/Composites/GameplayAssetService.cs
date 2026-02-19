using System;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.Prefabs.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.Frameworks.GameServices.Prefabs.Implementation.Composites
{
    public class GameplayAssetService : CompositeAssetService
    {
        private readonly IAddressablesAssetLoader _addressablesAssetLoader;
        private readonly IAddressablesAssetLoader[] _assetServices;

        public GameplayAssetService(
            IAddressablesAssetLoader addressablesAssetLoader)
            : base(addressablesAssetLoader)
        {
            _addressablesAssetLoader = addressablesAssetLoader ??
                                       throw new ArgumentNullException(nameof(addressablesAssetLoader));
        }
        
        protected override async UniTask LoadByAddressableConfigAsync(string addressablesCollectorPath)
        {
            AddressablesAssetConfig config = await _addressablesAssetLoader.LoadAsset<AddressablesAssetConfig>(addressablesCollectorPath);
            
            await AddressalesLoad(config.RectangleConfig);
        }
    }
}