using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Gameplay.PauseText;
using _Project.Scripts.Gameplay.PopupScoring;
using _Project.Scripts.Gameplay.Score;
using _Project.Scripts.Gameplay.UI.View;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Infrastructure.Services.GameLoop;
using _Project.Scripts.Infrastructure.Services.Input;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Factory
{
    public class UIFactory :
        IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private const string InitialPauseTextPath = "UI/InitialPause";
        private const string PopupScoringPath = "UI/Popup_Scoring";

        private readonly IInputService _inputService;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameLoopService _gameLoopService;
        
        private Transform _uiRoot;

        public UIFactory(
            IAssetProvider assetProvider,
            IInputService inputService,
            IGameLoopService gameLoopService)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
            _gameLoopService = gameLoopService;
        }

        public PopupScoringView CreatePopupScoring(IGameOverEvent gameOverEvent, StorageScore storageScore)
        {
            GameObject popupScoring = Object.Instantiate(_assetProvider.LoadAsset(PopupScoringPath), _uiRoot);

            var view = popupScoring.GetComponent<PopupScoringView>();
            PopupScoringPresenter presenter = new PopupScoringPresenter(view, gameOverEvent, storageScore);

            _gameLoopService.AddListener(presenter);
            
            return popupScoring.GetComponent<PopupScoringView>();
        }

        public PauseTextView CreateInitialPauseText()
        {
            GameObject uiText = Object.Instantiate(_assetProvider.LoadAsset(InitialPauseTextPath), _uiRoot);

            PauseTextView view = uiText.GetComponent<PauseTextView>();
            PauseTextPresenter presenter = new PauseTextPresenter(view, _inputService);
            
            _gameLoopService.AddListener(presenter);
            
            return view;
        }

        public void CreateUIRoot() =>
            _uiRoot = Object.Instantiate(_assetProvider.LoadAsset(UIRootPath).transform);
    }
}