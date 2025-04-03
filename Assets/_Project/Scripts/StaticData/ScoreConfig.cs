using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.StaticData
{
    [CreateAssetMenu(
        fileName = "ScoreConfig",
        menuName = "Configs/ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {
        [field: SerializeField, MinValue(0)] public int InitialScore { get; private set; }
        [field: SerializeField, MinValue(0.1f)] public float AddedScore { get; private set; }
    }
}