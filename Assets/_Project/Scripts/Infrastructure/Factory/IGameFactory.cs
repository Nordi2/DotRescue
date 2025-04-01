using _Project.Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : 
        IService
    {
        GameObject CreateHud();
        GameObject CreatePlayer();
        GameObject CreateObstacle();
        GameObject CreatePlayArea();
    }
}