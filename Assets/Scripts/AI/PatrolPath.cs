using Core;
using UnityEngine;

namespace AI
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private GameObject _wayPointGameObject;
        const float waypointGizmoRadius = 0.3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }
        
        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
        
        public void GenerateWayPointInRadius(Vector3 position, PatrolPath currentParent)
        {
            float spawnRadius = 60f;
            float randomPointInRadiusX = (Random.insideUnitCircle.normalized * spawnRadius).x;
            float randomPointInRadiusZ = (Random.insideUnitCircle.normalized * spawnRadius).y;
            
            Vector3 patrolPathPosition = new Vector3(position.x + randomPointInRadiusX, 0,
                position.z + randomPointInRadiusZ);
            Instantiate(
                _wayPointGameObject,
                patrolPathPosition,
                Quaternion.identity,
                currentParent.transform);   
        }
    }   
}