using Sources.Frameworks.GameServices.Loads.Services.Implementation;
using Sources.Frameworks.GameServices.Loads.Services.Implementation.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Prefabs.Implementation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Zenject;

namespace Sources.App.DIContainers.Common
{
    public class SaveLoadServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStorageService>().To<StorageService>().AsSingle();
            Container.Bind<IDataService>().To<PlayerPrefsDataService>().AsSingle();
            Container.Bind<IAssetCollector>().To<AssetCollector>().AsSingle();
            Container.Bind<IResourcesAssetLoader>().To<ResourcesAssetLoader>().AsSingle();
            Container.Bind<IAddressablesAssetLoader>().To<AddressablesAssetLoader>().AsSingle();
        }
    }
}