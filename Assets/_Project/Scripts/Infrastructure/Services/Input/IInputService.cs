using System;

namespace _Project.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : 
        IService
    {
        event Action OnClickLeftMouseButton;
    }
}