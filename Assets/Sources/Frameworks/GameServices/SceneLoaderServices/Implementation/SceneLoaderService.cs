using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.SceneLoaderServices.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Frameworks.GameServices.SceneLoaderServices.Implementation
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public async UniTask Load(string sceneName) =>
            await SceneManager.LoadSceneAsync(sceneName);

        public UniTask Unload() =>
            UniTask.CompletedTask;
    }
}