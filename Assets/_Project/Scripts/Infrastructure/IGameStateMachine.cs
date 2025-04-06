using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.States;

namespace _Project.Scripts.Infrastructure
{
    public interface IGameStateMachine :
        IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}