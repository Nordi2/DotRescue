using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Services.GameLoop
{
    public class GameLoopService : MonoBehaviour,
        IGameLoopService
    {
        private readonly List<IGameUpdateListener> _listUpdateable = new();
        private readonly List<IGameFixedListener> _listFixedUpdateable = new();
        private readonly List<IGameStartListener> _listStarteable = new();
        private readonly List<IGameFinishListener> _listFinishable = new();
        private readonly List<IDisposable> _listDisposables = new();
        
        public void AddListener(IGameListener listener) =>
            CheckTypeAndAddInList(listener);

        public void AddDisposable(IDisposable disposable) =>
            _listDisposables.Add(disposable);

        public void OnStartGame()
        {
            foreach (IGameStartListener startListener in _listStarteable)
                startListener.StartGame();
        }

        public void OnFinishGame()
        {
            foreach (IGameFinishListener finishListener in _listFinishable)
                finishListener.FinishGame();
            
            Dispose();
        }
        
        private void Update()
        {
            foreach (IGameUpdateListener updatable in _listUpdateable)
                updatable.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            foreach (IGameFixedListener fixedUpdatable in _listFixedUpdateable)
                fixedUpdatable.FixedUpdate(Time.fixedDeltaTime);
        }

        private void OnDestroy() => 
            Dispose();

        private void Dispose()
        {
            foreach (IDisposable disposable in _listDisposables)
                disposable.Dispose();
        }

        private void CheckTypeAndAddInList(IGameListener listener)
        {
            if (CheckType(listener, out IGameUpdateListener updateListener))
                _listUpdateable.Add(updateListener);

            if (CheckType(listener, out IGameFixedListener fixedUpdateListener))
                _listFixedUpdateable.Add(fixedUpdateListener);
            
            if (CheckType(listener, out IGameStartListener startListener))
                _listStarteable.Add(startListener);
            
            if (CheckType(listener, out IGameFinishListener finishListener))
                _listFinishable.Add(finishListener);
        }

        private bool CheckType<T>(IGameListener listener, out T newListener)
        {
            if (listener is T gameListener)
            {
                newListener = gameListener;
                return true;
            }

            newListener = default;
            return false;
        }
    }
}