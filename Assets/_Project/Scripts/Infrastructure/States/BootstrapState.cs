using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.Infrastructure.Services.StaticData;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.States
{
    public class BootstrapState :
        IState
    {
        private const string Bootstrap = "Bootstrap";
        private const string Gameplay = "Gameplay";
        private const string GameLoop = "[GameLoopService]";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Bootstrap, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(Gameplay);
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            IGameLoopService gameLoopService = RegisterGameLoop();
            IInputService inputService = new InputService();
            
            _services.RegisterSingle(gameLoopService);
            _services.RegisterSingle(inputService);
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IGameLoopService>(),
                _services.Single<IInputService>()));
            
            gameLoopService.AddListener(inputService);

            _services.RegisterSingle(inputService);
            _services.RegisterSingle(gameLoopService);
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticData();
            _services.RegisterSingle(staticDataService);
        }

        private IGameLoopService RegisterGameLoop()
        {
            GameLoopService gameLoopService = new GameObject(GameLoop).AddComponent<GameLoopService>();
            Object.DontDestroyOnLoad(gameLoopService.gameObject);
            return gameLoopService;
        }
    }
}