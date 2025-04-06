using System;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class ScoreData
    {
        public int Score;

        public ScoreData(int score)
        {
            Score = score;
        }
    }
}