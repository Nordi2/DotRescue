using System;

namespace _Project.Scripts.Infrastructure.Services.GameLoop
{
    public interface IGameLoopService :
        IService
    {
        void OnStartGame();
        void OnFinishGame();
        void AddListener(IGameListener listener);
        void AddDisposable(IDisposable disposable);
    }
}