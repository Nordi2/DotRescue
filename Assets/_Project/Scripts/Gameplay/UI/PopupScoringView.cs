using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Buttons = NaughtyAttributes.ButtonAttribute;

namespace _Project.Scripts.Gameplay.UI
{
    public class PopupScoringView : MonoBehaviour
    {
        [SerializeField] private RectTransform _body;
        [SerializeField] private Button _buttonGoMainMenu;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private CanvasGroup _bodyCanvasGroup;
        
        private Vector2 _targetBodyPosition;
        private Vector2 _startShift;
        private Sequence _animation;
        
        private void Awake()
        {
            _targetBodyPosition = _body.anchoredPosition;
            _startShift = new Vector2(_targetBodyPosition.x, -Screen.height / 2);
        }
        
        [Buttons]
        public void ShowPopup()
        {
            gameObject.SetActive(true);

            KillCurrentAnimationIfActive();
            _scoreText.text = "0";
            _animation = DOTween.Sequence();

            _animation
                .Append(_bodyCanvasGroup.DOFade(1, 1f).From(0))
                .Join(_body.DOAnchorPos(_targetBodyPosition, 1f).From(_startShift))
                .Append(_scoreText.DOCounter(0,100,5))
                .Append(_buttonGoMainMenu.transform.DOScale(1, 0.5f).From(0).SetEase(Ease.OutBounce));
        }
        
        [Buttons]
        public void HidePopup()
        {
            KillCurrentAnimationIfActive();
            
            _animation = DOTween.Sequence();
            
            _animation
                .Append(_bodyCanvasGroup.DOFade(0,1f).From(1))
                .Join(_body.DOAnchorPos(_startShift,1f).From(_targetBodyPosition))
                .OnComplete(()=>gameObject.SetActive(false));
        }
        
        private bool InAnimation() => 
            _animation != null && _animation.active;
        
        private void KillCurrentAnimationIfActive()
        {
            if (InAnimation())
                _animation.Kill();
        }
    }
}