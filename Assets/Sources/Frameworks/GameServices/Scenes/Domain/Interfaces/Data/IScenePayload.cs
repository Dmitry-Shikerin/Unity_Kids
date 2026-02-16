namespace Sources.Frameworks.GameServices.Scenes.Domain.Interfaces.Data
{
    public interface IScenePayload
    {
        string SceneId { get; }
        bool CanLoad { get; }
        bool CanFromGameplay { get; }
    }
}