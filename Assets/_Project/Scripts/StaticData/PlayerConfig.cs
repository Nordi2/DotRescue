using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.StaticData
{
    [CreateAssetMenu(
        fileName = "PlayerConfig",
        menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, MinValue(1f)] public float RotationSpeed { get; private set; } = 125f;
    }
}