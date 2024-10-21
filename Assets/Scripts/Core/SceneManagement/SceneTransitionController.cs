using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.SceneManagement
{
    public class SceneTransitionController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup fadeCanvasGroup;
        [SerializeField] private Slider progressBar;


        private void Start()
        {
            FadeIn(1f).Forget();
        }

        public async UniTask FadeIn(float duration)
        {
            fadeCanvasGroup.alpha = 1f;
            await fadeCanvasGroup.DOFade(0f, duration).ToUniTask();
            fadeCanvasGroup.blocksRaycasts = false;
        }

        public async UniTask FadeOut(float duration)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.blocksRaycasts = true;
            await fadeCanvasGroup.DOFade(1f, duration).ToUniTask();
        }

        public void UpdateProgress(float progress)
        {
            if (progressBar != null)
            {
                progressBar.value = progress;
            }
        }
    }
}
