using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class ChooseRaceMenuView : MenuView
    {
        [SerializeField] private Button _humanRaceButton;
        [SerializeField] private Button _fungoidRaceButton;
        [SerializeField] private Button _machineRaceButton;
        [SerializeField] private Button _backButton;

        private const byte GAME_SCENE_INDEX = 1;
        
        public override void Init()
        {
            _humanRaceButton.onClick.AddListener(() => LoadAsHumanRace());
            _fungoidRaceButton.onClick.AddListener(() => LoadAsFungoidRace());
            _machineRaceButton.onClick.AddListener(() => LoadAsMachineRace());
            _backButton.onClick.AddListener((() => MenuViewManager.MenuViewManagerInstance.ShowPrevious()));
        }

        private void LoadAsHumanRace()
        {
            SceneManager.LoadScene(GAME_SCENE_INDEX);
        }

        private void LoadAsFungoidRace()
        {
            SceneManager.LoadScene(GAME_SCENE_INDEX);

        }

        private void LoadAsMachineRace()
        {
            SceneManager.LoadScene(GAME_SCENE_INDEX);
        }
    }
}
