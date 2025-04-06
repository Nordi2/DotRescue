using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textScore;
        [SerializeField] private Button _statrGameButton;
        
        private UnityAction _action;

        public void ClickStartGameButton(UnityAction action)
        {
            _action = action;
            _statrGameButton.onClick.AddListener(_action);
        }

        public void UpdateTextScore(string newText) => 
            _textScore.text = newText;

        private void OnDestroy()
        {
            _statrGameButton.onClick.RemoveListener(_action);
        }
    }
}