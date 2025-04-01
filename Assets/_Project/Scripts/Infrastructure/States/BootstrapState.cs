using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Services;
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
        private const string InputService = "[InputService]";

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
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>()));
            _services.RegisterSingle(RegisterInputService());
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticData();
            _services.RegisterSingle(staticDataService);
        }

        private IInputService RegisterInputService()
        {
            InputService inputService = new GameObject(InputService).AddComponent<InputService>();
            Object.DontDestroyOnLoad(inputService.gameObject);
            return inputService;
        }
    }
}