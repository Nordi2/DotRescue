using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.States;
using DebugToolsPlus;

namespace _Project.Scripts.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activateState;

        public GameStateMachine(SceneLoader sceneLoader , LoadingCurtain curtain , AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(MainMenuState)] = new MainMenuState(curtain,sceneLoader,services.Single<IUIFactory>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this,sceneLoader,curtain,services.Single<IGameFactory>(),services.Single<IUIFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            D.LogWarning("ENTER STATE",  D.FormatText(typeof(TState).Name,DColor.CYAN),DColor.YELLOW);
            
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            D.LogWarning("ENTER STATE",  D.FormatText(typeof(TState).Name,DColor.CYAN),DColor.YELLOW);
            
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activateState?.Exit();

            TState state = GetState<TState>();
            _activateState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}