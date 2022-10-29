using System;
using UnityEngine;
using Core.Miscellaneous;
using PlayerInteractable.SpaceObjects;
using PlayerInteractable.Constructions;

using Random = UnityEngine.Random;

namespace Core.WorldGeneration
{

    [Serializable]
    public class PlanetTypes
    {
        public Planet planetPrefab;
        
        [Range(0f, 100f)] public float spawnProbability = 100f;
    }

    [Serializable]
    public class RaceShipsInfo
    {
        public BasicShip[] shipTypes;
    }
    
    public class WorldGenerator : MonoBehaviour
    {

        [Header("Values")]
        
        [SerializeField] 
        [Range(5f, 10f)] 
        private float shipsSpawnRadius = 5f;
        
        [SerializeField] 
        [Range(0.7f, 3f)] 
        private float planetsSpawnDensity = 1f;
        
        [Header("Objects")]
        
        [SerializeField]
        private PlanetTypes[] planetsType;
        
        [SerializeField]
        private RaceShipsInfo[] raceShipsInfo;
        
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
                SpawnPlanetWithProbability();
            }
        }
        private void SpawnPlanetWithProbability()
        {
            for (int i = 0; i < planetsType.Length; i++)
            {
                Vector3 randomPointAtMap = GenerateRandomPointAtMap();
                
                if (Physics.CheckSphere(randomPointAtMap, Planet.COLLIDER_RADIUS / planetsSpawnDensity)) continue;
                
                float rnd = Random.Range(0.1f, 100);

                if (rnd <= planetsType[i].spawnProbability)
                {
                    Planet planet = planetsType[i].planetPrefab;
                    Instantiate(
                            planet, 
                            randomPointAtMap, 
                            Quaternion.identity,
                            GameObject.FindGameObjectWithTag("Instantiated Planets").transform // to prevent editor littering
                        );
                    DisableSpaceObjectMeshRenderer(planet);
                }
            }
        }
        private void SpawnShipsInRandomPoints()
        {
            for (int i = 0; i < raceShipsInfo.Length; i++)
            {              
                Vector3 randomPointAtMap = GenerateRandomPointAtMap();
                
                for (int j = 0; j < raceShipsInfo[i].shipTypes.Length; j++)
                {
                    SpawnShip(randomPointAtMap, raceShipsInfo[i].shipTypes[j]);
                }
            }
        }
        private void SpawnShip(Vector3 randomPointAtMap, BasicShip currentShip)
        {
            float randomPointInRadiusX = (Random.insideUnitCircle.normalized * shipsSpawnRadius).x;
            float randomPointInRadiusZ = (Random.insideUnitCircle.normalized * shipsSpawnRadius).y;
                    
            Instantiate(
                currentShip,
                new Vector3(randomPointAtMap.x + randomPointInRadiusX, 0, randomPointAtMap.z + randomPointInRadiusZ),
                Quaternion.identity); 
        }

        private Vector3 GenerateRandomPointAtMap()
        {
            return new Vector3(
                Random.Range(MapSettings.minPositionX, MapSettings.maxPositionX),
                0f, 
                Random.Range(MapSettings.minPositionZ, MapSettings.maxPositionZ)
            );
        }
        private void DisableSpaceObjectMeshRenderer(Planet planet)
        {
            MeshRenderer childMesh = planet.GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;
                    
            if (childMesh != null)
            {
                childMesh.enabled = false;
                planet.gameObject.tag = Globals.inActiveObjectTag;
            }
        }

    }
}