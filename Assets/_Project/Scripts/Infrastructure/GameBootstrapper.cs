using _Project.Scripts.Infrastructure.States;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour,
        ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}