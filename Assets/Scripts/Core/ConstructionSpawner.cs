using PlayerInteractable.Constructions;
using UnityEngine;

namespace Core
{
    public class ConstructionSpawner : MonoBehaviour
    {
        public void SpawnShip(Vector3 randomPointAtMap, BasicShip currentShip, float shipsSpawnRadius, out Vector3 position)
        {
            float randomPointInRadiusX = (Random.insideUnitCircle.normalized * shipsSpawnRadius).x;
            float randomPointInRadiusZ = (Random.insideUnitCircle.normalized * shipsSpawnRadius).y;

            position = new Vector3(randomPointAtMap.x + randomPointInRadiusX, 0,
                randomPointAtMap.z + randomPointInRadiusZ);

            Instantiate(
                currentShip,
                position,
                Quaternion.identity);
        }
    }
}