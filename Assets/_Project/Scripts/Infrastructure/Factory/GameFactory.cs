using System.Collections.Generic;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.Obstacle;
using _Project.Scripts.Gameplay.Player;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;
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
        private const string GameLoop = "[GameLoopService]";

        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        private IGameLoopService _gameLoopService;
        private IInputService _inputService;
        
        public GameFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public List<ISavedProgress> ProgressWriters { get; } = new();
        public StorageScore StorageScore { get; private set; }

        public IInputService CreateInputService()
        {
            D.Log(GetType().Name.ToUpper(), D.FormatText("CREATED INFRASTRUCTURE: Input_Service", DColor.GREEN),
                DColor.YELLOW);

            _inputService = new InputService();

            return _inputService;
        }

        public IGameLoopService CreateGameLoopService()
        {
            D.Log(GetType().Name.ToUpper(), D.FormatText("CREATED INFRASTRUCTURE: Gameloop_Service", DColor.GREEN),
                DColor.YELLOW);

            _gameLoopService = new GameObject(GameLoop).AddComponent<GameLoopService>();

            return _gameLoopService;
        }

        public void CleanUp() => 
            ProgressWriters.Clear();

        public void CreateGameLoopController(IGameOverEvent gameOverEvent)
        {
            D.Log(GetType().Name, D.FormatText("CREATED INFRASTRUCTURE: Gameloop_Controller", DColor.GREEN),
                DColor.YELLOW);

            GameLoopController gameLoopController = new GameLoopController(
                _inputService,
                _gameLoopService,
                gameOverEvent);

            _gameLoopService.AddDisposable(gameLoopController);
        }

        public GameObject CreateHud()
        {
            D.Log(GetType().Name.ToUpper(), D.FormatText("CREATED WORLD OBJECT: Hud", DColor.GREEN), DColor.YELLOW);

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
            
            RegisterProgressWatchers(StorageScore);
            
            _gameLoopService.AddListener(scorePresenter);
            _gameLoopService.AddListener(StorageScore);

            return hudPrefab;
        }

        public PlayerFacade CreatePlayer()
        {
            D.Log(GetType().Name.ToUpper(), D.FormatText("CREATED WORLD OBJECT: Player", DColor.GREEN), DColor.YELLOW);

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
            D.Log(GetType().Name.ToUpper(), D.FormatText("CREATED WORLD OBJECT: Obstacle", DColor.GREEN),
                DColor.YELLOW);

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
            D.Log(GetType().Name.ToUpper(), D.FormatText("CREATED WORLD OBJECT: Play_Area", DColor.GREEN),
                DColor.YELLOW);

            GameObject playAreaPrefab = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.PlayAreaPath));
            return playAreaPrefab;
        }

        private void RegisterProgressWatchers(ISavedProgress progress) => 
            ProgressWriters.Add(progress);
    }
}