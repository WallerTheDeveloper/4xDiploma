using System;
using System.Collections.Generic;
using Core;
using Core.Data;
using Core.Miscellaneous;
using Movement;
using UnityEngine;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspitionTime = 1f;
        public PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 1f;
        // private Fighter _fighter;
        // private GameObject _player;
        // private Health _health;
        LazyValue<Vector3> _guardPosition;

        private Mover _mover;
        
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;
        
        private void Awake()
        {
            // _fighter = GetComponent<Fighter>();
            // _health = GetComponent<Health>();
            // _player = GameObject.FindWithTag("Player");
            _mover = GetComponent<Mover>();
            _guardPosition = new LazyValue<Vector3>(GetGuardPosition);
        }

        private void Start()
        {
            _guardPosition.ForceInit();
        }

        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Update()
        {
            // if (_health.IsDead()) return;

            // if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            // {
            //     AttackBehaviour();
            // }
            // else if (timeSinceLastSawPlayer < suspitionTime)
            // {
            //     SuspicionBehaviour();
            // }
            if (timeSinceLastSawPlayer < suspitionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _guardPosition.value;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                _mover.PerformFlyAction(nextPosition);
            }
        }
        
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }
        
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }
        
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        // private void AttackBehaviour()
        // {
        //     timeSinceLastSawPlayer = 0;
        //     _fighter.Attack(_player);
        // }
        //
        // private bool InAttackRangeOfPlayer()
        // {
        //     float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        //     return distanceToPlayer < chaseDistance;
        // }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}