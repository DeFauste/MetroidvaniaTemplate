using Core.SceneManagement;
using UnityEngine;
using Zenject;

namespace Core.Services.SceneManagement
{
    public class SceneTransitionServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("SceneTransitionServiceInstaller: InstallBindings called");

            var sceneTransitionControllerPrefab = Resources.Load<GameObject>("SceneTransitionController");

            if (sceneTransitionControllerPrefab == null)
            {
                Debug.LogError("Не удалось загрузить SceneTransitionController из Resources.");
                return;
            }

            Debug.Log("SceneTransitionServiceInstaller: Prefab loaded successfully");

            var controllerInstance = Container.InstantiatePrefabForComponent<SceneTransitionController>(sceneTransitionControllerPrefab);
            GameObject.DontDestroyOnLoad(controllerInstance.gameObject);

            Container.Bind<SceneTransitionController>().FromInstance(controllerInstance).AsSingle().NonLazy();
            Container.Bind<SceneTransitionService>().AsSingle();
        }
    }
}
