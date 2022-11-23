using System;
using System.Threading.Tasks;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class PauseMenuView : MenuView
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        private TaskCompletionSource<bool> _taskCompletion;
        private Canvas _canvas;
        private bool _mainMenuOpened = false;
        public event Action QuitGame;

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame && Globals.Bools.isUnitsDeselected) // TODO: Exit not working
            {
                OnEscapeButtonClicked();
                print("Clicked");
            }
        }

        public override void Init()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
            _continueButton.onClick.AddListener(OnAcceptContinue);
            _exitButton.onClick.AddListener(OnAcceptExit);
        }
        public async Task<bool> AwaitForDecision()
        {
            _canvas.enabled = true;
            _taskCompletion = new TaskCompletionSource<bool>();
            var result = await _taskCompletion.Task;
            _canvas.enabled = false;
            return result;
        }
        private async void OnEscapeButtonClicked()
        {
            OnPauseClicked(true);
            var isConfirmed = await AwaitForDecision();
            OnPauseClicked(false);
            if (isConfirmed)
            {
                QuitGame?.Invoke();
            }
        }
        private void OnPauseClicked(bool isPaused)
        {
            // ProjectContext.Instance.PauseManager.SetPaused(isPaused);
        }
        private void OnDestroy()
        {
            // _pauseToggle.ValueChanged -= OnPauseClicked;
        }
        
        private void OnAcceptExit()
        {
            _taskCompletion.SetResult(true);
        }

        private void OnAcceptContinue()
        {
            _taskCompletion.SetResult(false);
        }
    }
}
