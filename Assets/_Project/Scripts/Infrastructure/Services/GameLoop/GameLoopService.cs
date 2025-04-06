using System;
using System.Collections.Generic;
using DebugToolsPlus;
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
                
        public void AddListener(IGameListener listener)
        {
            CheckTypeAndAddInList(listener);
        }

        public void AddDisposable(IDisposable disposable) =>
            _listDisposables.Add(disposable);

        public void OnStartGame()
        {
            D.Log(GetType().Name.ToUpper(),"START-GAME",DColor.MAGENTA,true);
            
            foreach (IGameStartListener startListener in _listStarteable)
                startListener.StartGame();
        }

        public void OnFinishGame()
        {
            D.Log(GetType().Name.ToUpper(),"FINISH-GAME",DColor.MAGENTA,true);
            
            foreach (IGameFinishListener finishListener in _listFinishable)
                finishListener.FinishGame();
            
            Dispose();
        }
        
        private void Update()
        {
           D.Log(GetType().Name.ToUpper(),"UPDATE",DColor.MAGENTA,true);
           
            foreach (IGameUpdateListener updatable in _listUpdateable)
                updatable.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            D.Log(GetType().Name.ToUpper(),"FIXED-UPDATE",DColor.MAGENTA,true);
            
            foreach (IGameFixedListener fixedUpdatable in _listFixedUpdateable)
                fixedUpdatable.FixedUpdate(Time.fixedDeltaTime);
        }

        private void OnDestroy() => 
            Dispose();

        private void Dispose()
        {
            D.Log(GetType().Name.ToUpper(),"DISPOSE",DColor.MAGENTA,true);
            
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