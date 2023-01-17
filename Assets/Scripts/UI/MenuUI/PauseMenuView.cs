using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Loading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class PauseMenuView : MenuView
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        private TaskCompletionSource<bool> _taskCompletion;
        private Canvas _canvas;
        public event Action QuitGame;

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                OnEscapeButtonClicked();
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
            var isConfirmed = await AwaitForDecision();
        }
        
        
        private void OnAcceptExit()
        {
            SceneManager.LoadScene(0);
        }

        private void OnAcceptContinue()
        {
            _taskCompletion.SetResult(false);
        }
    }
}
