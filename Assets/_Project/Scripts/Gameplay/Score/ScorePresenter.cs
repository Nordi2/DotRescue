using System.Globalization;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.Services.GameLoop;

namespace _Project.Scripts.Gameplay.Score
{
    public class ScorePresenter : 
        IGameStartListener,
        IGameFinishListener
    {
        private StorageScore _storageScore;
        private TextScoreView _textScoreView;

        public ScorePresenter(
            StorageScore storageScore,
            TextScoreView textScoreView)
        {
            _storageScore = storageScore;
            _textScoreView = textScoreView;
            
            _textScoreView.UpdateText(_storageScore.CurrentScore.ToString(CultureInfo.CurrentCulture));
        }
        
        void IGameStartListener.StartGame()
        {
            _storageScore.OnScoreChanged += UpdateView;
        }

        void IGameFinishListener.FinishGame()
        {
            _storageScore.OnScoreChanged -= UpdateView;    
        }

        private void UpdateView(int currentScore) =>
            _textScoreView.UpdateText(currentScore.ToString());
    }
}