using System.Collections.Generic;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.PauseText;
using _Project.Scripts.Gameplay.PopupScoring;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using _Project.Scripts.Infrastructure.Services.PersistentProgress;
using _Project.Scripts.MainMenu;
using DebugToolsPlus;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Infrastructure.Factory
{
    public class UIFactory :
        IUIFactory
    {
        private const string SortingLayerUI = "UI";
        
        private readonly IAssetProvider _assetProvider;
        private readonly IGameStateMachine _stateMachine;

        private Transform _uiRoot;

        public UIFactory(
            IAssetProvider assetProvider,
            IGameStateMachine stateMachine)
        {
            _assetProvider = assetProvider;
            _stateMachine = stateMachine;
        }

        public List<ILoadProgress> LoadProgresses { get; } = new();

        public PopupScoringView CreatePopupScoring(IGameOverEvent gameOverEvent, StorageScore storageScore,IGameLoopService gameLoopService)
        {
            D.Log(GetType().Name, D.FormatText("CREATED UI: Popup_Scoring",DColor.GREEN),DColor.YELLOW);

            GameObject popupScoring = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.PopupScoringPath), _uiRoot);

            var view = popupScoring.GetComponent<PopupScoringView>();
            PopupScoringPresenter presenter = new PopupScoringPresenter(view, gameOverEvent, storageScore,_stateMachine);

            gameLoopService.AddListener(presenter);

            return popupScoring.GetComponent<PopupScoringView>();
        }

        public PauseTextView CreateInitialPauseText(IInputService inputService,IGameLoopService gameLoopService)
        {
            D.Log(GetType().Name, D.FormatText("CREATED UI: Pause_Text",DColor.GREEN),DColor.YELLOW);

            GameObject uiText = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.InitialPauseTextPath), _uiRoot);

            PauseTextView view = uiText.GetComponent<PauseTextView>();
            PauseTextPresenter presenter = new PauseTextPresenter(view, inputService);

            gameLoopService.AddListener(presenter);

            return view;
        }

        public void CreateMainMenu()
        {
            D.Log(GetType().Name, D.FormatText("CREATED UI: Main_Menu",DColor.GREEN),DColor.YELLOW);

            GameObject mainMenu = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.MainMenuPath), _uiRoot);

            MainMenuView view = mainMenu.GetComponent<MainMenuView>();
            MainMenuPresenter presenter = new MainMenuPresenter(view, _stateMachine);
            
            LoadProgresses.Add(presenter);
        }

        public void CreateUIRoot()
        {
            D.Log(GetType().Name, D.FormatText("CREATED UI: UI_Root",DColor.GREEN),DColor.YELLOW);
            
            _uiRoot = Object.Instantiate(_assetProvider.LoadAsset(AssetPath.UIRootPath).transform);
            
            SettingCanvas();
            
            void SettingCanvas()
            {
                Canvas canvas = _uiRoot.GetComponent<Canvas>();
                
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = Camera.main;
                canvas.sortingLayerName = SortingLayerUI;
            }
        }

        public void CleanUp()
        {
            LoadProgresses.Clear();
        }
    }
}