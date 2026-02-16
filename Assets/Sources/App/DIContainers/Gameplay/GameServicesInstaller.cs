using Sources.Frameworks.GameServices.Linecasts.Implementation;
using Sources.Frameworks.GameServices.Linecasts.Interfaces;
using Sources.Frameworks.GameServices.Overlaps.Implementation;
using Sources.Frameworks.GameServices.Overlaps.Interfaces;
using Sources.Frameworks.GameServices.Pauses;
using Sources.Frameworks.GameServices.Pauses.Impl;
using Sources.Frameworks.GameServices.UpdateServices.Implementation;
using Zenject;

namespace Sources.App.DIContainers.Gameplay
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IOverlapService>().To<OverlapService>().AsSingle();
            Container.Bind<ILinecastService>().To<LinecastService>().AsSingle();
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();
            Container.BindInterfacesTo<UpdateService>().AsSingle();
        }
    }
}