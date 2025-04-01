using _Project.Scripts.Infrastructure.AssetManagement;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factory
{
    public class GameFactory :
        IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHud()
        {
            GameObject hudPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.HudPath));
            return hudPrefab;
        }

        public GameObject CreatePlayer()
        {
            GameObject playerPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.PlayerPath));
            return playerPrefab;
        }

        public GameObject CreateObstacle()
        {
            GameObject obstaclePrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.ObstaclePath));
            return obstaclePrefab;
        }

        public GameObject CreatePlayArea()
        {
            GameObject playAreaPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.PlayAreaPath));
            return playAreaPrefab;
        }
    }
}