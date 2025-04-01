namespace _Project.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string BOOTSTRAP = "Bootstrap";
        private const string GAMEPLAY = "Gameplay";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(BOOTSTRAP,onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState,string>(GAMEPLAY);
        }

        private void RegisterServices()
        {

        }

        private void RegisterStaticData()
        {
        }
    }
}