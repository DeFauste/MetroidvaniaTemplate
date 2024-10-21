using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SceneManagement
{
    public class AsyncSceneLoader
    {
        private const string IntermediateEmptySceneName = "Boot";

        private bool _isOperationInProgress;

        /// <summary>
        /// Async scene loading by name.
        /// </summary>
        /// <param name="sceneName">Loading scene name</param>
        /// <param name="progress">An object to track download progress</param>
        /// <param name="mode">Scene loading mode</param>
        /// <param name="activateImmediately">Specifies whether the scene should be activated immediately after loading.</param>
        public async UniTask LoadSceneAsync(string sceneName, IProgress<float> progress = null, LoadSceneMode mode = LoadSceneMode.Single, bool activateImmediately = true)
        {
            if (!CanProceedWithOperation(sceneName)) return;

            _isOperationInProgress = true;

            try
            {
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, mode);

                if (activateImmediately)
                {
                    while (!asyncOperation.isDone)
                    {
                        // Normalization progress from 0 to 1
                        float progressValue = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                        progress?.Report(progressValue);
                        await UniTask.Yield();
                    }
                }
                else
                {
                    asyncOperation.allowSceneActivation = false;

                    while (asyncOperation.progress < 0.9f)
                    {
                        float progressValue = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                        progress?.Report(progressValue);
                        await UniTask.Yield();
                    }

                    progress?.Report(1f);

                    // Scene activation
                    asyncOperation.allowSceneActivation = true;

                    // Waiting for the scene to fully activate
                    await UniTask.WaitUntil(() => asyncOperation.isDone);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при загрузке сцены {sceneName}: {ex}");
                throw;
            }
            finally
            {
                _isOperationInProgress = false;
            }
        }

        /// <summary>
        /// Async scene unload by name.
        /// </summary>
        /// <param name="sceneName">Unload scene name</param>
        public async UniTask UnloadSceneAsync(string sceneName)
        {
            if (!CanProceedWithOperation(sceneName)) return;

            _isOperationInProgress = true;

            try
            {
                AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
                while (!asyncOperation.isDone)
                {
                    await UniTask.Yield();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error when unloading scene {sceneName}: {ex}");
                throw;
            }
            finally
            {
                _isOperationInProgress = false;
            }
        }

        /// <summary>
        /// Switches scenes using an empty scene in between for a safe transition.
        /// </summary>
        /// <param name="sceneName">Loading scene name.</param>
        /// <param name="progress">An object to track download progress</param>
        public async UniTask SceneTransitionAsync(string sceneName, IProgress<float> progress = null)
        {
            if (!CanProceedWithOperation(sceneName)) return;

            try
            {
                // Step 1: Load empty boot scene
                await LoadSceneAsync(IntermediateEmptySceneName, progress: null, LoadSceneMode.Single, activateImmediately: true);
                await UniTask.Yield();

                // Step 2: Freeing up resources
                await Resources.UnloadUnusedAssets();
                System.GC.Collect();

                // Step 3: Load new scene with progress
                await LoadSceneAsync(sceneName, progress, LoadSceneMode.Single, activateImmediately: true);
            }
            finally
            {
                _isOperationInProgress = false;
            }
        }

        /// <summary>
        /// Checks whether an operation can be performed.
        /// </summary>
        /// <param name="sceneName">Name of the scene to check</param>
        /// <returns>Returns true if the operation can be performed; otherwise false</returns>
        private bool CanProceedWithOperation(string sceneName)
        {
            if (_isOperationInProgress)
            {
                Debug.LogWarning("Another operation is already in progress");
                return false;
            }

            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("Scene Name is empty");
                return false;
            }

            return true;
        }
    }
}
