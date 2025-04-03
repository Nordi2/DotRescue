using UnityEngine;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _deathVfx;
        [SerializeField] private Transform _positionSkinPlayer;

        public ParticleSystem DeathVfx => _deathVfx;
        public Transform PlayerPosition => transform;
        public Transform PositionSkinPlayer => _positionSkinPlayer;
    }
}