using System;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.Obstacle;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.Infrastructure.Services.StaticData;
using _Project.Scripts.StaticData;
using DebugToolsPlus;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Infrastructure.Factory
{
    public class GameFactory :
        IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameLoopService _gameLoopService;
        private readonly IInputService _inputService;

        public StorageScore StorageScore { get; private set; }

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

        public void CreateGameLoopController(IGameOverEvent gameOverEvent)
        {
            D.Log("CREATED INFRASTRUCTURE", "Gameloop_Controller", DColor.RED,true);
            
            GameLoopController gameLoopController = new GameLoopController(
                _inputService,
                _gameLoopService,
                gameOverEvent);

            _gameLoopService.AddDisposable(gameLoopController);
        }

        public GameObject CreateHud()
        {
            D.Log("CREATED WORLD OBJECT", "Hud", DColor.GREEN,true);
            
            GameObject hudPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.HudPath));
            
            SettingCanvas();
            ScoreConfig scoreConfig = _staticDataService.GetData<ScoreConfig>();
            
            TextScoreView textScoreView = hudPrefab.GetComponent<TextScoreView>();
            StorageScore = new StorageScore(scoreConfig.AddedScore, scoreConfig.InitialScore);

            ScorePresenter scorePresenter = new ScorePresenter(StorageScore, textScoreView);
            
            void SettingCanvas()
            {
                Canvas canvas = hudPrefab.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = Camera.main; 
            }

            _gameLoopService.AddListener(scorePresenter);
            _gameLoopService.AddListener(StorageScore);
            
            return hudPrefab;
        }

        public PlayerFacade CreatePlayer()
        {
            D.Log("CREATED WORLD OBJECT", "Player", DColor.GREEN,true);
            
            GameObject playerPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.PlayerPath));

            PlayerConfig config = _staticDataService.GetData<PlayerConfig>();
            PlayerView view = playerPrefab.GetComponentInChildren<PlayerView>();
            PlayerFacade facade = playerPrefab.GetComponentInChildren<PlayerFacade>();

            PlayerMovement movement = new PlayerMovement(config.RotationSpeed, view.PlayerPosition);
            SpawnDieEffect spawnDieEffect = new SpawnDieEffect(view.DeathVfx, view.PositionSkinPlayer);

            PlayerMoveController playerMoveController = new PlayerMoveController(_inputService, movement);

            facade.Construct(spawnDieEffect);

            _gameLoopService.AddListener(playerMoveController);

            return facade;
        }

        public GameObject CreateObstacle()
        {
            D.Log("CREATED WORLD OBJECT", "Obstacle", DColor.GREEN,true);
            
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

            return obstaclePrefab;
        }

        public GameObject CreatePlayArea()
        {
            D.Log("CREATED WORLD OBJECT", "Play_Area", DColor.GREEN,true);
            
            GameObject playAreaPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.PlayAreaPath));
            return playAreaPrefab;
        }
    }
}