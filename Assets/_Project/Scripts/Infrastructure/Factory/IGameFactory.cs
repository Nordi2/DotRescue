using System.Collections.Generic;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Infrastructure.Services;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : 
        IService
    {
        List<ISavedProgress> ProgressWriters { get; }
        public StorageScore StorageScore { get; }
        void CleanUp();
        void CreateGameLoopController(IGameOverEvent gameOverEvent);
        GameObject CreateHud();
        PlayerFacade CreatePlayer();
        GameObject CreateObstacle();
        GameObject CreatePlayArea();
        IInputService CreateInputService();
        IGameLoopService CreateGameLoopService();
    }
}