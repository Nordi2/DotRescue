using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.UI
{
    public class PauseTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Ease _ease;
        
        private Tween _tween;

        [Button]
        public void StartAnimation()
        {
            _tween = _text
                .DOFade(0, 1)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(_ease);
        }

        [Button]
        public void StopAnimation()
        {
            if (_tween is not null)
                _tween.Kill();
            
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_tween is not null)
                _tween.Kill();
        }
    }
}