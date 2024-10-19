using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Services.SceneTransition
{
    public class SceneTransitionController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _fadeCanvasGroup;
        [SerializeField] private Slider progressBar;

        public async UniTask FadeIn(float duration)
        {
            _fadeCanvasGroup.alpha = 1f;
            await _fadeCanvasGroup.DOFade(0f, duration).ToUniTask();
        }
    }
}
