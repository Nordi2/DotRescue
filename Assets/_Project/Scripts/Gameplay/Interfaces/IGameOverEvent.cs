using System;

namespace _Project.Scripts.Gameplay.Interfaces
{
    public interface IGameOverEvent
    {
        event Action OnGameOver;
    }
}