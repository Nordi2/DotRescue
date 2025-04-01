using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        private void Awake()
        {
            GameBootstrapper gameBootstrap = FindObjectOfType<GameBootstrapper>();

            if (gameBootstrap is null)
                Instantiate(_gameBootstrapper);
        }
    }
}