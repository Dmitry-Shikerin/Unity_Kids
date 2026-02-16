using Cysharp.Threading.Tasks;

namespace Sources.Frameworks.GameServices.SceneLoaderServices.Interfaces
{
    public interface ISceneLoaderService
    {
        UniTask Load(string sceneName);
        UniTask Unload();
    }
}