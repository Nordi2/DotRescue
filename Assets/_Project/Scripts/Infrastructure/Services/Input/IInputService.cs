using System;
using _Project.Scripts.Infrastructure.Services.GameLoop;

namespace _Project.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : 
        IGameUpdateListener,
        IService
    {
        event Action OnClickLeftMouseButton;
    }
}