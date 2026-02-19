using Sources.Frameworks.GameServices.UpdateServices.Implementation;
using Zenject;

namespace Sources.App.DIContainers.Gameplay
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UpdateService>().AsSingle();
        }
    }
}