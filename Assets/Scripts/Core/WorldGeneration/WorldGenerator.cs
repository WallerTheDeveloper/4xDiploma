using System;
using System.Collections.Generic;
using Core.Data;
using UnityEngine;
using Core.Miscellaneous;
using PlayerInteractable.SpaceObjects;
using PlayerInteractable.Constructions;

using Random = UnityEngine.Random;

namespace Core.WorldGeneration
{ 
    public class WorldGenerator : MonoBehaviour // TODO: ShipFactory, PlanetsFactory
    {
        [SerializeField] 
        [Range(5f, 10f)] 
        private float shipsSpawnRadius = 5f;
        
        [SerializeField] 
        [Range(0.7f, 3f)] 
        private float planetsSpawnDensity = 1f;

        [SerializeField]
        private PlanetsData _planetsData;
        
        [SerializeField]
        private ShipsData _shipsData;
        
        private const int SPAWN_ITERATIONS = 500;
       
        private void Awake()
        {
            SpawnShipsInRandomPoints();
            GeneratePlanetsInRandomPoints();
            Debug.Log("World generated!");
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
            for (int i = 0; i < _planetsData.data.Length; i++)
            {
                Vector3 randomPointAtMap = GenerateRandomPointAtMap();
                
                if (Physics.CheckSphere(randomPointAtMap, Planet.COLLIDER_RADIUS / planetsSpawnDensity)) continue;
                
                float rnd = Random.Range(0.1f, 100);

                if (rnd <= _planetsData.data[i].spawnProbability)
                {
                    Planet planet = _planetsData.data[i].planetPrefab;
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
            for (int i = 0; i < _shipsData.data.Length; i++)
            {
                Vector3 randomPointAtMap = GenerateRandomPointAtMap();
                
                for (int j = 0; j < _shipsData.data[i].shipTypes.Length; j++)
                {
                    SpawnShip(randomPointAtMap, _shipsData.data[i].shipTypes[j], j);
                }
            }
        }
        private void SpawnShip(Vector3 randomPointAtMap, BasicShip currentShip, int shipNumber)
        {
            float randomPointInRadiusX = (Random.insideUnitCircle.normalized * shipsSpawnRadius).x;
            float randomPointInRadiusZ = (Random.insideUnitCircle.normalized * shipsSpawnRadius).y;
            
            Vector3 position = new Vector3(randomPointAtMap.x + randomPointInRadiusX, 0, randomPointAtMap.z + randomPointInRadiusZ);
            
            Instantiate(
                currentShip,
                position,
                Quaternion.identity);

            if (_shipsData.data[shipNumber].shipsPositions.Count >= 3)
            {
                _shipsData.data[shipNumber].shipsPositions = new List<Vector3>();
            }
            _shipsData.data[shipNumber].shipsPositions.Add(position);
            
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