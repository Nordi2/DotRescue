using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _canvasGroup
                .DOFade(0, 0.5f)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}