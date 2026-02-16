using Sources.Frameworks.GameServices.Scenes.Domain.Interfaces.Data;

namespace Sources.Frameworks.GameServices.Scenes.Infrastructure.Views.Interfaces
{
    public interface ISceneViewFactory
    {
        public void Create(IScenePayload payload);
    }
}