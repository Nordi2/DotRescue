using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : 
        IService
    {
        public StorageScore StorageScore { get; }

        void CreateGameLoopController(PlayerFacade playerFacade);
        GameObject CreateHud();
        PlayerFacade CreatePlayer();
        GameObject CreateObstacle();
        GameObject CreatePlayArea();
    }
}