using Core;
using UnityEngine;

namespace AI
{
    public class PatrolPathRandomSpawner : MonoBehaviour
    {
        [SerializeField] private PatrolPath PatrolPath;
        [SerializeField] private int numberOfWaypoints = 5;
        private PatrolPath currentParent;
        
        public PatrolPath GeneratePatrolPath(Vector3 position)
        {
            currentParent = Instantiate(
                PatrolPath, 
                position,
                Quaternion.identity,
                GameObject.FindGameObjectWithTag(Globals.Tags.PatrolWaysTag).transform);
            GenerateWaypoints(position);
            return currentParent;
        }
        
        private void GenerateWaypoints(Vector3 position)
        {
            for (int i = 0; i < numberOfWaypoints; i++)
            {
                PatrolPath.GenerateWayPointInRadius(position, currentParent);
            }
        }
    }
}
