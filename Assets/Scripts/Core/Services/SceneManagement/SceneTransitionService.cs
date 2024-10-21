using Core.SceneManagement;
using Cysharp.Threading.Tasks;
using Debugging;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Services.SceneManagement
{
    public class SceneTransitionService
    {
        private AsyncSceneLoader _sceneLoader = new();
        private SceneTransitionController _transitionController;

        [DebugKey(Key.A)]
        public void Test()
        {
            LoadScene("SceneTemplate").Forget();
        }

        [Inject]
        public void Construct(SceneTransitionController transitionController)
        {
            _transitionController = transitionController;
        }

        public async UniTask LoadScene(string sceneName)
        {
            await _transitionController.FadeOut(1f);
            await _sceneLoader.SceneTransitionAsync(sceneName);
            await _transitionController.FadeIn(1f);
        }
    }
}
