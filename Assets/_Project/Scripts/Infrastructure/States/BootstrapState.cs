using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;
using _Project.Scripts.Infrastructure.Services.SaveLoad;
using _Project.Scripts.Infrastructure.Services.StaticData;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.States
{
    public class BootstrapState :
        IState
    {
        private const string Bootstrap = "Bootstrap";

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
            Debug.unityLogger.logEnabled = Debug.isDebugBuild;
            _sceneLoader.Load(Bootstrap, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>()));

            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IGameFactory>(),
                _services.Single<IPersistentProgressService>()));
            
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IGameStateMachine>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticData();
            _services.RegisterSingle(staticDataService);
        }
    }
}