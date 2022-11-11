using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Movement;

namespace Control
{
    public class InputController : MonoBehaviour
    {
        private InputActionsAsset _inputActions;
        private bool _moveShouldContinue;
        private bool _moveHasStarted;
        private void Awake()
        {
            _inputActions = new InputActionsAsset();
        }
        
        private void OnEnable()
        {
            _inputActions.Enable();
        
            _inputActions.Player.ShipsControl.started += OnClick;
            _inputActions.Player.ShipsControl.performed += OnClick;
            _inputActions.Player.ShipsControl.canceled += OnClick;
        }
        
        private void OnDisable()
        {
            _inputActions.Player.ShipsControl.started -= OnClick;
            _inputActions.Player.ShipsControl.performed -= OnClick;
            _inputActions.Player.ShipsControl.canceled -= OnClick;
            
            _inputActions.Disable();
        }
        
        private void OnClick(InputAction.CallbackContext context)
        {
            switch (context.phase) {
                case InputActionPhase.Started:
                    OnClickStarted(context);
                    break;
                case InputActionPhase.Performed:
                    OnClickPerformed(context);
                    break;
                case InputActionPhase.Canceled:
                    OnClickCanceled(context);
                    break;
                case InputActionPhase.Disabled:
                    OnClickDisabled(context);
                    break;
                case InputActionPhase.Waiting:
                    OnClickWaiting(context);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            _moveHasStarted = true;
            if (_moveShouldContinue && _moveHasStarted)
            {
;               GetComponent<Mover>().InteractWithMovement();
            }
        }
        private void OnClickStarted(InputAction.CallbackContext context) {
            _moveShouldContinue = true;
            _moveHasStarted = false;
        }
        
        private void OnClickCanceled(InputAction.CallbackContext context)
        {
            _moveShouldContinue = false;
        }
        
        private void OnClickDisabled(InputAction.CallbackContext context)
        {
            //do nothing
        }
        
        private void OnClickWaiting(InputAction.CallbackContext context)
        {
            _moveShouldContinue = false;
            _moveHasStarted = false;
        }
    }
}
