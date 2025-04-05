namespace _Project.Scripts.Infrastructure.States
{
    public class MainMenuState :
        IState
    {
        private GameStateMachine _stateMachine;

        public MainMenuState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}