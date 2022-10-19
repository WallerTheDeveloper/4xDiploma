using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class PauseMenuView : MenuView
    {
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _mainMenuButton;

        private const byte MAIN_MENU_SCENE_INDEX = 0;
        public override void Init()
        {
            _mainMenuButton.onClick.AddListener(() => ExitToMainMenu());
            _optionsButton.onClick.AddListener(() => MenuViewManager.ShowView<OptionsMenuView>());
        }

        private void ExitToMainMenu()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE_INDEX);
            // Destroy(GameObject.Find());
        }
    }
}
