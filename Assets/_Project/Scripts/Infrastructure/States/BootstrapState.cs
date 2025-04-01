using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.Services.StaticData;

namespace _Project.Scripts.Infrastructure.States
{
    public class BootstrapState :
        IState
    {
        private const string BOOTSTRAP = "Bootstrap";
        private const string GAMEPLAY = "Gameplay";

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
            _sceneLoader.Load(BOOTSTRAP, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(GAMEPLAY);
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticData();
            _services.RegisterSingle(staticDataService);
        }
    }
}