using System.Collections;
using System.Collections.Generic;
using AI;
using Control;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody), typeof(ActionScheduler))]
    public class Mover: MonoBehaviour, IAction
    {
        private float _speed = 10f;
        private float _targetDistance = 2f; // how close to the target do you want to get?
        private float _rotateSpeed = 5f;
        
        private Quaternion _rotation;
        // private Transform _currentTargetLocation;
        private Vector3 _currentTargetLocation;
        private static Ray GetMouseRay => Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        public void InteractWithMovement()
        {
            StopAllCoroutines();
            bool hasHit = Physics.Raycast(GetMouseRay, out var hit, Mathf.Infinity, Globals.Layers.PLANET_LAYER_MASK); //Raycasting only when object has layer name "Planet"
            if (hasHit && hit.transform.CompareTag(Globals.Tags.ActiveObjectTag))
            {
                PerformFlyAction(hit.transform.position);
            }
        }
        public void PerformFlyAction(Vector3 destination)
        {
            if (TryGetComponent(out ActionScheduler actionScheduler))
            {
                actionScheduler.PerformAction(this);
            }
            _currentTargetLocation = destination;

            Fly();
        }
        public void Cancel()
        {
            _speed = 0;
        }
        private void Fly()
        {
            StartCoroutine(SmoothRotate());
            StartCoroutine(CalculateMovement());
            if (TryGetComponent(out InputController inputController))
            {
                return;
            }
            StopAllCoroutines();
        }
        private IEnumerator CalculateMovement()
        {
            Globals.Bools.HasReachedDestination = false;
            while (Vector3.Distance(transform.position, _currentTargetLocation) > _targetDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _currentTargetLocation, _speed * Time.deltaTime);
                yield return null;
            }
            Globals.Bools.HasReachedDestination = true;
        }
        private IEnumerator SmoothRotate()
        {
            Vector3 direction = (_currentTargetLocation - transform.position).normalized;
            
            _rotation = Quaternion.LookRotation(direction);

            while (_rotation != transform.rotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, _rotateSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}