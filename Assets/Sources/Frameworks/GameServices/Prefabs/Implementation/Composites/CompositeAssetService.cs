using System;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Sources.Frameworks.GameServices.Prefabs.Implementation.Composites
{
    public abstract class CompositeAssetService : ICompositeAssetService
    {
        private readonly IAddressablesAssetLoader _addressablesAssetLoader;
        private readonly IAddressablesAssetLoader[] _assetServices;

        public CompositeAssetService(
            IAddressablesAssetLoader addressablesAssetLoader)
        {
            _addressablesAssetLoader = addressablesAssetLoader ??
                                       throw new ArgumentNullException(nameof(addressablesAssetLoader));
        }

        public async UniTask LoadAsync(string assetCollectorPath, string addressablesCollectorPath)
        {
            await LoadByAddressableConfigAsync(addressablesCollectorPath);
        }
        
        protected abstract UniTask LoadByAddressableConfigAsync(string addressablesCollectorPath);

        protected async UniTask AddressalesLoad<T>(AssetReferenceT<T> assetReference)
            where T : Object =>
            await _addressablesAssetLoader.LoadAsset(assetReference);

        protected async UniTask AddressalesPrefabLoad<T>(AssetReferenceT<GameObject> assetReference) 
            where T : Object =>
            await _addressablesAssetLoader.LoadAsset<T>(assetReference);

        public void Release()
        {
            _addressablesAssetLoader.ReleaseAll();
        }
    }
}