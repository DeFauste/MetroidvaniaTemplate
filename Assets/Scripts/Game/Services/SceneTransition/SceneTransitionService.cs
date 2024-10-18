using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Services.SceneTransition
{
    public class SceneTransitionService
    {
        private readonly SceneLoader _sceneLoader;
        private readonly CanvasGroup _transitionCanvasGroup;  // Используется для затемнения
        private readonly Image _loadingImage;  // Используется для отображения экрана загрузки

        // Конструктор принимает зависимости через Zenject
        public SceneTransitionService(SceneLoader sceneLoader, CanvasGroup transitionCanvasGroup, Image loadingImage)
        {
            _sceneLoader = sceneLoader;
            _transitionCanvasGroup = transitionCanvasGroup;
            _loadingImage = loadingImage;
        }

        // Метод для выполнения перехода между сценами
        public async UniTask TransitionToScene(string sceneName, float fadeDuration = 1f)
        {
            // 1. Показываем анимацию затемнения
            await FadeOut(fadeDuration);

            // 2. Загружаем сцену асинхронно с отображением прогресса
            var progressObservable = await _sceneLoader.LoadSceneAsync(sceneName);

            // Подписываемся на обновление прогресса загрузки через R3
            progressObservable.Subscribe(progress =>
            {
                // Например, обновляем заполнение картинки загрузки
                _loadingImage.fillAmount = progress;
            },
            error =>
            {
                // Обрабатываем возможную ошибку загрузки
                Debug.LogError($"Error during scene loading: {error}");
            },
            onCompleted  =>
            {
                // Когда загрузка завершена
                Debug.Log("Scene loading complete.");
            });

            // 3. Плавное появление новой сцены
            await FadeIn(fadeDuration);
        }

        // Метод для затемнения экрана
        private async UniTask FadeOut(float duration)
        {
            _transitionCanvasGroup.gameObject.SetActive(true);  // Активируем Canvas

            float timeElapsed = 0;
            while (timeElapsed < duration)
            {
                _transitionCanvasGroup.alpha = Mathf.Lerp(0, 1, timeElapsed / duration);  // Линейная интерполяция альфа-канала
                timeElapsed += Time.deltaTime;
                await UniTask.Yield();
            }

            _transitionCanvasGroup.alpha = 1;
        }

        // Метод для появления новой сцены
        private async UniTask FadeIn(float duration)
        {
            float timeElapsed = 0;
            while (timeElapsed < duration)
            {
                _transitionCanvasGroup.alpha = Mathf.Lerp(1, 0, timeElapsed / duration);  // Линейная интерполяция альфа-канала
                timeElapsed += Time.deltaTime;
                await UniTask.Yield();
            }

            _transitionCanvasGroup.alpha = 0;
            _transitionCanvasGroup.gameObject.SetActive(false);  // Деактивируем Canvas после завершения анимации
        }
    }
}
