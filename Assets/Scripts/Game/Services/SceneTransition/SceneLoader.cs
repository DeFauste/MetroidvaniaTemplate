using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Services.SceneTransition
{
    public class SceneLoader
    {
        public UniTask<Observable<float>> LoadSceneAsync(string sceneName)
        {
            var loadingProgress = new Subject<float>();

            return UniTask.Create(async () =>
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
                operation!.allowSceneActivation = false;

                while (operation.progress < 0.9f)
                {
                    loadingProgress.OnNext(operation.progress);
                    await UniTask.Yield();
                }

                loadingProgress.OnNext(1.0f);

                operation.allowSceneActivation = true;
                loadingProgress.OnCompleted();

                return loadingProgress.AsObservable();
            });
        }
    }
}
