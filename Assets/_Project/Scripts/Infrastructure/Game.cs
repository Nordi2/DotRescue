using _Project.Scripts.Infrastructure.Services;

namespace _Project.Scripts.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner,LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner),curtain,AllServices.Container);
        }
    }
}