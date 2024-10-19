using Cysharp.Threading.Tasks;
using Game.Services.Debugging;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Services.SceneTransition
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
