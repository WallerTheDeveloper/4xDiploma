using AI;
using Core.Data;
using Core.RaceChoice;
using Core.WorldGeneration;
using PlayerInteractable.Constructions;
using UnityEngine;

namespace Core.Spawners
{
    public class ConstructionSpawner : MonoBehaviour
    {
        private ShipsData _shipsData;
        private ShipsData _aiShipsData;
        private RaceChoiceController _raceChoiceController;
        private PatrolPathRandomSpawner _patrolPathRandomSpawner;
        private BasicShip _currentShip;
        public void Init(ShipsData shipsData, ShipsData aiShipsData, RaceChoiceController raceChoiceController, PatrolPathRandomSpawner patrolPathRandomSpawner)
        {
            _shipsData = shipsData;
            _aiShipsData = aiShipsData;
            _raceChoiceController = raceChoiceController;
            _patrolPathRandomSpawner = patrolPathRandomSpawner;
        }
        public void SpawnPlayerDrivenShips()
        {
            for (int i = 0; i < _shipsData.data.Length; i++)
            {
                if (_shipsData.data[i].RaceTypes != _raceChoiceController.ChosenRace)
                {
                    continue;
                }
                Vector3 randomPointAtMap = RandomPointGenerator.GenerateRandomPointAtMap();
                
                for (int j = 0; j < _shipsData.data[i].shipTypes.Length; j++)
                {
                    SpawnShipAtPoint(randomPointAtMap, _shipsData.data[i].shipTypes[j], WorldGenerator.ShipsSpawnRadius, out Vector3 shipNewPosition);
                    _shipsData.data[i].ShipsPositions.Add(shipNewPosition);
                }
            }
        }
        public void SpawnAIDrivenShips()
        {
            // Spawn AI driven ships
            for (int i = 0; i < _aiShipsData.data.Length; i++)
            {
                if (_aiShipsData.data[i].RaceTypes == _raceChoiceController.ChosenRace)
                {
                    continue;
                }

                Vector3 randomPointAtMap = RandomPointGenerator.GenerateRandomPointAtMap();
                for (int j = 0; j < _aiShipsData.data[i].shipTypes.Length; j++)
                {
                    SpawnShipAtPoint(randomPointAtMap, _aiShipsData.data[i].shipTypes[j],
                        WorldGenerator.ShipsSpawnRadius, out Vector3 AIshipNewPosition);
                    _aiShipsData.data[i].ShipsPositions.Add(AIshipNewPosition);
                    var currentShipPatrolPath = _patrolPathRandomSpawner.GeneratePatrolPath(AIshipNewPosition);
                    _currentShip.GetComponent<AIController>().patrolPath = currentShipPatrolPath;
                }
            }
        }
        public void SpawnShipAtPoint(Vector3 pointAtMap, BasicShip currentShip, float shipsSpawnRadius, out Vector3 position)
        {
            float randomPointInRadiusX = (Random.insideUnitCircle.normalized * shipsSpawnRadius).x;
            float randomPointInRadiusZ = (Random.insideUnitCircle.normalized * shipsSpawnRadius).y;

            position = new Vector3(pointAtMap.x + randomPointInRadiusX, 0,
                pointAtMap.z + randomPointInRadiusZ);
    
            _currentShip = Instantiate(
                currentShip,
                position,
                Quaternion.identity);
        }
        public void SpawnShipAtPoint(Vector3 pointAtMap, BasicShip currentShip, float shipsSpawnRadius)
        {
            Vector3 position;
            float randomPointInRadiusX = (Random.insideUnitCircle.normalized * shipsSpawnRadius).x;
            float randomPointInRadiusZ = (Random.insideUnitCircle.normalized * shipsSpawnRadius).y;

            position = new Vector3(pointAtMap.x + randomPointInRadiusX, 0,
                pointAtMap.z + randomPointInRadiusZ);

            Instantiate(
                currentShip,
                position,
                Quaternion.identity);
        }
    }
}