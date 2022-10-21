using System.Collections.Generic;
using UnityEngine;
using Core.Miscellaneous;
using PlayerInteractable;
using Random = UnityEngine.Random;

namespace Core.WorldGeneration
{

    [System.Serializable]
    public class PlanetTypes
    {
        public string Name; // debug - remove before build
        
        public Planet planetPrefab;
        
        [Range(0f, 100f)] public float spawnProbability = 100f;
    }
    
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private PlanetTypes[] planetsType;

        [SerializeField] [Range(0.7f, 3f)] private float spawnDensity = 1f;
        
        private const int PLANET_SPAWN_ITERATIONS = 500;
        
        private void Start()
        {
            GeneratePlanetsInRandomPoints();
        }

        private void GeneratePlanetsInRandomPoints()
        {
        
            for (int i = 0; i < PLANET_SPAWN_ITERATIONS; i++)
            {
                
                Vector3 randomPlanetPointOnMap = GenerateRandomPoint();
        
                if (Physics.CheckSphere(randomPlanetPointOnMap, Planet.COLLIDER_RADIUS / spawnDensity)) continue;

                SpawnPlanetWithProbability(randomPlanetPointOnMap);
            }
        }

        private void SpawnPlanetWithProbability(Vector3 randomPointOnMap)
        {
            for (int i = 0; i < planetsType.Length; i++)
            {
                float rnd = Random.Range(0.1f, 100);

                if (rnd <= planetsType[i].spawnProbability)
                {
                    Instantiate(planetsType[i].planetPrefab, randomPointOnMap, Quaternion.identity);
                    Debug.Log("Name: " + planetsType[i].Name); // debug - remove before build
                }
            }
        }
        
        private Vector3 GenerateRandomPoint()
        {
            return new Vector3(
                Random.Range(MapSettings.minPositionX, MapSettings.maxPositionX),
                0f, 
                Random.Range(MapSettings.minPositionZ, MapSettings.maxPositionZ)
            );
        }
    }
}