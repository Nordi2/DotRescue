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
        private readonly List<IDisposable> _listDisposables = new();

        public void AddListener(IGameListener listener) =>
            CheckTypeAndAddInList(listener);

        public void AddDisposable(IDisposable disposable) =>
            _listDisposables.Add(disposable);

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

        private void OnDestroy()
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