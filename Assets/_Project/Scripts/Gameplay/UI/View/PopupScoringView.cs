using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.UI.View
{
    public class PopupScoringView : MonoBehaviour
    {
        [SerializeField] private Button _buttonGoMainMenu;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private CanvasGroup _bodyCanvasGroup;
        
        private Sequence _animation;

        [field: SerializeField] public RectTransform Body { get; private set; }
        
        public void ShowPopup(Vector2 targetBodyPosition, Vector2 startShift,int endValue)
        {
            gameObject.SetActive(true);

            KillCurrentAnimationIfActive();
            _scoreText.text = "0";
            _animation = DOTween.Sequence();

            _animation
                .AppendInterval(2f)
                .Append(_bodyCanvasGroup.DOFade(1, 1f).From(0))
                .Join(Body.DOAnchorPos(targetBodyPosition, 1f).From(startShift))
                .Append(_scoreText.DOCounter(0, endValue, 2))
                .Append(_buttonGoMainMenu.transform.DOScale(1, 0.5f).From(0).SetEase(Ease.OutBounce));
        }

        private bool InAnimation() =>
            _animation != null && _animation.active;

        private void KillCurrentAnimationIfActive()
        {
            if (InAnimation())
                _animation.Kill();
        }

        private void OnDestroy() => 
            KillCurrentAnimationIfActive();
    }
}