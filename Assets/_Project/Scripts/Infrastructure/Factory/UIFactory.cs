using _Project.Scripts.Gameplay.PauseText;
using _Project.Scripts.Gameplay.UI;
using _Project.Scripts.Infrastructure.AssetManagement;
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

        private IInputService _inputService;
        private IAssetProvider _assetProvider;

        private Transform _uiRoot;

        public UIFactory(
            IAssetProvider assetProvider,
            IInputService inputService)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
        }

        public PopupScoringView CreatePopupScoring()
        {
            GameObject popupScoring = Object.Instantiate(_assetProvider.LoadAsset(PopupScoringPath), _uiRoot);

            return popupScoring.GetComponent<PopupScoringView>();
        }

        public PauseTextView CreateInitialPauseText()
        {
            GameObject uiText = Object.Instantiate(_assetProvider.LoadAsset(InitialPauseTextPath), _uiRoot);

            PauseTextView view = uiText.GetComponent<PauseTextView>();
            PauseTextPresenter presenter = new PauseTextPresenter(view, _inputService);

            return view;
        }

        public void CreateUIRoot() =>
            _uiRoot = Object.Instantiate(_assetProvider.LoadAsset(UIRootPath).transform);
    }
}