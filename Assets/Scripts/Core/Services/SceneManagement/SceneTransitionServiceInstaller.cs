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

            // Инстанцируем префаб как корневой объект в сцене
            var controllerInstance = Instantiate(sceneTransitionControllerPrefab);
            GameObject.DontDestroyOnLoad(controllerInstance); // Применяем DontDestroyOnLoad для корневого объекта

            // Получаем компонент SceneTransitionController из инстанцированного объекта
            var controllerComponent = controllerInstance.GetComponent<SceneTransitionController>();

            if (controllerComponent == null)
            {
                Debug.LogError("SceneTransitionController не найден на загруженном префабе.");
                return;
            }

            // Привязываем компонент SceneTransitionController
            Container.Bind<SceneTransitionController>().FromInstance(controllerComponent).AsSingle().NonLazy();
            Container.Bind<SceneTransitionService>().AsSingle().NonLazy();
        }
    }
}
