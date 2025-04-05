using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.UI.View
{
    public class TextScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        public void UpdateText(string newText) => 
            _scoreText.text = newText;
    }
}