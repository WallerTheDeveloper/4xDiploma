using System;
using UnityEngine;
using Core.Miscellaneous;
using PlayerInteractable;
using Ships;
using Random = UnityEngine.Random;

namespace Core.WorldGeneration
{

    [Serializable]
    public class PlanetTypes
    {
        // public string Name; // debug - remove before build
        
        public Planet planetPrefab;
        
        [Range(0f, 100f)] public float spawnProbability = 100f;
    }

    [Serializable]
    public class ShipTypes
    {
        public BasicShip[] shipPrefab;
    }
    
    public class WorldGenerator : MonoBehaviour
    {        
        [SerializeField] [Range(5f, 10f)] private float shipsSpawnRadius = 5f;
        
        [SerializeField] [Range(0.7f, 3f)] private float spawnDensity = 1f;

        [SerializeField] private PlanetTypes[] planetsType;

        [SerializeField] private ShipTypes[] shipTypes;

        private const int SPAWN_ITERATIONS = 500;
        
        private void Start()
        {
            SpawnShipsInRandomPoints();
            GeneratePlanetsInRandomPoints();
        }

        private void GeneratePlanetsInRandomPoints()
        {
        
            for (int i = 0; i < SPAWN_ITERATIONS; i++)
            {
                
                Vector3 randomPointAtMap = GenerateRandomPointAtMap();
                
        
                if (Physics.CheckSphere(randomPointAtMap, Planet.COLLIDER_RADIUS / spawnDensity)) continue;

                SpawnPlanetWithProbability(randomPointAtMap);
            }
        }

        private void SpawnPlanetWithProbability(Vector3 randomPointOnMap)
        {
            for (int i = 0; i < planetsType.Length; i++)
            {
                float rnd = Random.Range(0.1f, 100);

                if (rnd <= planetsType[i].spawnProbability)
                {
                    Instantiate(
                            planetsType[i].planetPrefab, 
                            randomPointOnMap, 
                            Quaternion.identity,
                            GameObject.FindGameObjectWithTag("Instantiated Planets").transform // to prevent editor littering
                        );
                    // Debug.Log("Name: " + planetsType[i].Name); // debug - remove before build
                }
            }
        }
        
        private void SpawnShipsInRandomPoints()
        {
            for (int i = 0; i < shipTypes.Length; i++)
            {              
                Vector3 randomPointAtMap = GenerateRandomPointAtMap();
                
                for (int j = 0; j < shipTypes[i].shipPrefab.Length; j++)
                {
                    BasicShip currentShip = shipTypes[i].shipPrefab[j];
                    float randomPointInRadiusX = (Random.insideUnitCircle.normalized * shipsSpawnRadius).x;
                    float randomPointInRadiusZ = (Random.insideUnitCircle.normalized * shipsSpawnRadius).y;
                    
                    Instantiate(
                        currentShip,
                        new Vector3(randomPointAtMap.x + randomPointInRadiusX, 0, randomPointAtMap.z + randomPointInRadiusZ),
                        Quaternion.identity);
                }
            }
        }
        
        private Vector3 GenerateRandomPointAtMap()
        {
            return new Vector3(
                Random.Range(MapSettings.minPositionX, MapSettings.maxPositionX),
                0f, 
                Random.Range(MapSettings.minPositionZ, MapSettings.maxPositionZ)
            );
        }
    }
}