using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.Factory;
using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;
using _Project.Scripts.Infrastructure.Services.SaveLoad;
using _Project.Scripts.Infrastructure.States;
using DebugToolsPlus;

namespace _Project.Scripts.Infrastructure
{
    public class GameStateMachine :
        IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activateState;

        public GameStateMachine(SceneLoader sceneLoader , LoadingCurtain curtain , AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(
                    this,
                    sceneLoader,
                    services),
                
                [typeof(LoadProgressState)] = new LoadProgressState(
                    this,
                    services.Single<ISaveLoadService>(),
                    services.Single<IPersistentProgressService>()),
                
                [typeof(MainMenuState)] = new MainMenuState(
                    curtain,
                    sceneLoader,
                    services.Single<IUIFactory>(),
                    services.Single<IPersistentProgressService>()),
                
                [typeof(LoadLevelState)] = new LoadLevelState(
                    this,
                    sceneLoader,
                    curtain,
                    services.Single<IGameFactory>(),
                    services.Single<IUIFactory>()),
                
                [typeof(GameLoopState)] = new GameLoopState(
                    this,
                    services.Single<ISaveLoadService>())
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            D.LogWarning(GetType().Name.ToUpper(), D.FormatText($"Enter State {typeof(TState).Name}",DColor.RED),DColor.YELLOW);
            
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            D.LogWarning(GetType().Name.ToUpper(), D.FormatText($"Enter State {typeof(TState).Name}",DColor.RED),DColor.YELLOW);
            
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