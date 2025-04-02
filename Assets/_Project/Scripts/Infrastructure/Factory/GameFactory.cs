using _Project_Test.Scripts;
using _Project.Scripts.Gameplay.Obstacle;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
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
        private readonly IGameLoopService _gameLoopService;
        private readonly IInputService _inputService;

        public GameFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IGameLoopService gameLoopService,
            IInputService inputService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _gameLoopService = gameLoopService;
            _inputService = inputService;
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
            PlayerFacade facade = playerPrefab.GetComponentInChildren<PlayerFacade>();
            PlayerMovement movement = new PlayerMovement(config.RotationSpeed, facade.PlayerTransform);

            PlayerMoveController playerMoveController = new PlayerMoveController(_inputService, movement);

            _gameLoopService.AddListener(playerMoveController);
            _gameLoopService.AddDisposable(playerMoveController);
            
            return playerPrefab;
        }

        public GameObject CreateObstacle()
        {
            GameObject obstaclePrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.ObstaclePath));
            
            ObstacleConfig config = _staticDataService.GetData<ObstacleConfig>();
            ObstacleFacade facade = obstaclePrefab.GetComponentInChildren<ObstacleFacade>();
            ObstacleMovement movement = new ObstacleMovement(facade.ObstacleTransform);
            ObstacleStatsTimeSwitcher obstacleStatsTimeSwitcher = new ObstacleStatsTimeSwitcher();

            ObstacleMoveController moveController = new ObstacleMoveController(
                movement,
                obstacleStatsTimeSwitcher,
                config);
            
            _gameLoopService.AddListener(moveController);
            _gameLoopService.AddDisposable(moveController);
            
            return obstaclePrefab;
        }

        public GameObject CreatePlayArea()
        {
            GameObject playAreaPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.PlayAreaPath));
            return playAreaPrefab;
        }
    }
}