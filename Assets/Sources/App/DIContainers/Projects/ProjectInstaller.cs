using Sources.Frameworks.GameServices.SceneLoaderServices.Implementation;
using Sources.Frameworks.GameServices.SceneLoaderServices.Interfaces;
using Zenject;

namespace Sources.App.DIContainers.Projects
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
        }
    }
}