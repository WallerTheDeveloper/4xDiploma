using UnityEngine;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class MainMenuView : MenuView
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _exitButton;
        public override void Init()
        {   
            _newGameButton.onClick.AddListener(() => MenuViewManager.ShowView<ChooseRaceMenuView>());
            _optionsButton.onClick.AddListener(() => MenuViewManager.ShowView<OptionsMenuView>());
            _exitButton.onClick.AddListener(() => Application.Quit());
        }
    }
}