using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace Game.Services.SceneTransition
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
        }

        public async UniTask FadeOut(float duration)
        {
            fadeCanvasGroup.alpha = 0f;
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
