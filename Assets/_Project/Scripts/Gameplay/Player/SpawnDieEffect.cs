using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Player
{
    [UsedImplicitly]
    public class SpawnDieEffect
    {
        private readonly ParticleSystem _deathVfx;
        private readonly Transform _spawnPointVfx;

        public SpawnDieEffect(
            ParticleSystem deathVfx,
            Transform spawnPointVfx)
        {
            _deathVfx = deathVfx;
            _spawnPointVfx = spawnPointVfx;
        }

        public void SpawnDeathVfx() => 
            Object.Instantiate(_deathVfx, _spawnPointVfx.position, Quaternion.identity);
    }
}