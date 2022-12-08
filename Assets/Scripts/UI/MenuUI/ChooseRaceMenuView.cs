using System;
using System.Collections.Generic;
using Loading;
using Races;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class ChooseRaceMenuView : MenuView
    {
        [SerializeField] private Button _humanRaceButton;
        [SerializeField] private Button _fungoidRaceButton;
        [SerializeField] private Button _machineRaceButton;
        [SerializeField] private Button _backButton;
        public static event Action<RaceTypes> OnRaceChoice;
        public override void Init()
        {
            _humanRaceButton.onClick.AddListener(() => LoadAsHumanRace());
            _fungoidRaceButton.onClick.AddListener(() => LoadAsFungoidRace());
            _machineRaceButton.onClick.AddListener(() => LoadAsMachineRace());
            _backButton.onClick.AddListener((() => MenuViewManager.MenuViewManagerInstance.ShowPrevious()));
        }
        private void LoadNewGame()
        {
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new NewGameLoadingOperation());
            ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
        }
        private void LoadAsHumanRace()
        {
            OnRaceChoice?.Invoke(RaceTypes.HumanRace);
            LoadNewGame();
        }
        private void LoadAsFungoidRace()
        {
            OnRaceChoice?.Invoke(RaceTypes.FungoidRace);
            LoadNewGame();
        }
        private void LoadAsMachineRace()
        {
            OnRaceChoice?.Invoke(RaceTypes.MachineRace);
            LoadNewGame();
        }
    }
}
