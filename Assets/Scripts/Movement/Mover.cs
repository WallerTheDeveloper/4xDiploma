using System.Collections;
using Core;
using Ships;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody), typeof(Ship), typeof(ActionScheduler))]
    public class Mover: MonoBehaviour, IAction
    {
        private float _speed = 1f;
        private float _targetDistance = 1f; // how close to the target do you want to get?
        private float _rotateSpeed = 5f;
        private bool _isReachedDestination;
        
        private Quaternion rotation; 
        private Vector3 _currentTargetLocation;
        private static Ray GetMouseRay => Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        public void InteractWithMovement()
        {
            bool hasHit = Physics.Raycast(GetMouseRay, out var hit, Mathf.Infinity, Globals.PLANET_LAYER_MASK); //Raycasting only when object has layer name "Planet"
            if (hasHit)
            {
                PerformFlyAction(hit.point);
            }
        }

        public void PerformFlyAction(Vector3 destination)
        {
            if (TryGetComponent(out ActionScheduler actionScheduler))
            {
                actionScheduler.PerformAction(this);
            }
            _currentTargetLocation = destination;
            GetComponent<Ship>().Fly();
        }

        public void Cancel()
        {
            _speed = 0;
        }
        
        protected IEnumerator CalculateMovement()
        {
            while (Vector3.Distance(transform.position, _currentTargetLocation) > _targetDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _currentTargetLocation, _speed * Time.deltaTime);
                yield return null;
            }
        }
        
        protected IEnumerator SmoothRotate()
        {
            Vector3 direction = (_currentTargetLocation - transform.position).normalized;
            rotation = Quaternion.LookRotation(direction);
            while (rotation != transform.rotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}