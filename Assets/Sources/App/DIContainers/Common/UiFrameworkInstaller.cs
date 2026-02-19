using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Zenject;

namespace Sources.App.DIContainers.Common
{
    public class UiFrameworkInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();
        }
    }
}