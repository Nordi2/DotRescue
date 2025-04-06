using System;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public ScoreData ScoreData;

        public PlayerProgress(int score)
        {
            ScoreData = new ScoreData(score);
        }
    }
}