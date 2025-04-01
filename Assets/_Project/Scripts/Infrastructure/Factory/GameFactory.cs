using _Project_Test.Scripts;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Services.StaticData;
using _Project.Scripts.StaticData;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factory
{
    public class GameFactory :
        IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        
        public GameFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public GameObject CreateHud()
        {
            GameObject hudPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.HudPath));
            return hudPrefab;
        }

        public GameObject CreatePlayer()
        {
            GameObject playerPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.PlayerPath));

            PlayerConfig config = _staticDataService.GetData<PlayerConfig>();

            Player player = playerPrefab.GetComponentInChildren<Player>();
            player._rotateSpeed = config.RotationSpeed;
            
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