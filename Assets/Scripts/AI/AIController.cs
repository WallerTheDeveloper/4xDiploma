using Attributes;
using CombatSystem;
using Core;
using Core.Miscellaneous;
using Movement;
using UnityEngine;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspitionTime = 1f;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 1f;
        public PatrolPath patrolPath;

        private Fighter _fighter;
        private GameObject[] _playerTargetFleet;
        private Health _health;
        LazyValue<Vector3> _guardPosition;
        
        private Mover _mover;
        
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;

        int currentWaypointIndex = 0;
        private void Awake()
        {
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _playerTargetFleet = GameObject.FindGameObjectsWithTag(Globals.Tags.SelectableUnitTag);
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
            if (_health.IsDead()) return;
            
            if (TryGetComponent(out Fighter fighter))
            {
                if (InAttackRangeOfPlayer() && _fighter.CanAttack(FindClosestTarget()))
                {
                    AttackBehaviour();
                }
                else if (timeSinceLastSawPlayer < suspitionTime)
                {
                    SuspicionBehaviour();
                }
                else
                {
                    PatrolBehaviour();
                }
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private GameObject FindClosestTarget()
        {
            float distanceToClosestEnemy = Mathf.Infinity;
            GameObject closestTarget = null;

            foreach (var currentTarget in _playerTargetFleet)
            {
                if (currentTarget == null) continue;
                float distanceToTarget = (currentTarget.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToTarget < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToTarget;
                    closestTarget = currentTarget;
                }
            }

            return closestTarget;
        }
        
        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        public void PatrolBehaviour()
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

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            _fighter.Attack(FindClosestTarget());
        }
        
        private bool InAttackRangeOfPlayer()
        {
            if (FindClosestTarget() == null) return false;
            float distanceToPlayer = Vector3.Distance(FindClosestTarget().transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}