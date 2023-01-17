using System;
using System.Collections;
using CombatSystem;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Movement;
using PlayerInteractable.SpaceObjects;

namespace Control
{
    public class InputController : MonoBehaviour
    {
        private InputActionsAsset _inputActions;
        private bool _moveShouldContinue;
        private bool _moveHasStarted;
        private Mover _mover;
        
        private void Awake()
        {
            _mover = GetComponent<Mover>();
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

        private void Update()
        {
            InteractWithComponent();
            if (_moveShouldContinue && _moveHasStarted)
            {
                _mover.InteractWithMovement();
            }
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
            // if (_moveShouldContinue && _moveHasStarted)
            // {
            //     _mover.InteractWithMovement();
            // }
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
        
        private void InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (var hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (var raycastable in raycastables)
                {
                    raycastable.HandleRaycast(this); 
                } 
            }
        }
        private RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
    }
}
