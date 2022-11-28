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
        
        public void Init(ShipsData shipsData, ShipsData aiShipsData, RaceChoiceController raceChoiceController)
        {
            _shipsData = shipsData;
            _aiShipsData = aiShipsData;
            _raceChoiceController = raceChoiceController; 
        }
        public void SpawnPlayerDrivenShips()
        {
            for (int i = 0; i < _shipsData.data.Length; i++)
            {
                Vector3 randomPointAtMap = RandomPointGenerator.GenerateRandomPointAtMap();

                for (int j = 0; j < _shipsData.data[i].shipTypes.Length; j++)
                {
                    if (_shipsData.data[i].RaceTypes == _raceChoiceController.ChosenRace)
                    {
                        SpawnShip(randomPointAtMap, _shipsData.data[i].shipTypes[j], WorldGenerator.ShipsSpawnRadius, out Vector3 shipNewPosition);
                        _shipsData.data[j].ShipsPositions.Add(shipNewPosition);
                    }
                }
            }
        }
        public void SpawnAIDrivenShips()
        {
            // Spawn AI driven ships
            for (int i = 0; i < _aiShipsData.data.Length; i++)
            {
                Vector3 randomPointAtMap = RandomPointGenerator.GenerateRandomPointAtMap();
                for (int j = 0; j < _aiShipsData.data[i].shipTypes.Length; j++)
                {
                    if (_aiShipsData.data[i].RaceTypes != _raceChoiceController.ChosenRace)
                    {
                        SpawnShip(randomPointAtMap, _aiShipsData.data[i].shipTypes[j], WorldGenerator.ShipsSpawnRadius, out Vector3 shipNewPosition);
                    }
                }
            }
        }
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