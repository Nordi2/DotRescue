using System;
using _Project.Scripts.Gameplay.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerFacade : MonoBehaviour,
        IDieble,IGameOverEvent
    {
        public event Action OnGameOver;

        private SpawnDieEffect _spawnDieEffect;


        public void Construct(SpawnDieEffect spawnDieEffect)
        {
            _spawnDieEffect = spawnDieEffect;
        }

        public void Die()
        {
            _spawnDieEffect.SpawnDeathVfx();
            OnGameOver?.Invoke();
            gameObject.SetActive(false);
        }

#if UNITY_EDITOR

        [Button]
        public void Respawn() =>
            gameObject.SetActive(true);
#endif
    }
}