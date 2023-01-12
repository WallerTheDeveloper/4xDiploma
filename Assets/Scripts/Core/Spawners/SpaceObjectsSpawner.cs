using Cinemachine;
using Core.Data;
using Core.WorldGeneration;
using PlayerInteractable.SpaceObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Spawners
{
    public class SpaceObjectsSpawner : MonoBehaviour
    {
        private PlanetsData _planetsData;
        public void Init(PlanetsData planetsData)
        {
            _planetsData = planetsData;
        }
        public void SpawnPlanetWithProbability()
        {
            for (int i = 0; i < _planetsData.data.Length; i++)
            {
                Vector3 randomPointAtMap = RandomPointGenerator.GenerateRandomPointAtMap();

                bool checkObjectsInRadius = 
                    Physics.CheckSphere(randomPointAtMap, Planet.COLLIDER_RADIUS / WorldGenerator.PlanetsSpawnDensity);
                
                if (checkObjectsInRadius)
                {
                    continue;
                }
                
                float rnd = Random.Range(0.1f, 100);

                if (rnd <= _planetsData.data[i].spawnProbability)
                {
                    SpawnSpaceObjectAtRandomPoint(_planetsData.data[i].planetPrefab, randomPointAtMap);
                }
            }
        }
        public void SpawnSpaceObjectAtRandomPoint(Planet planet, Vector3 pointAtMap)
        {
            Instantiate(
                planet, 
                pointAtMap, 
                Quaternion.identity,
                GameObject.FindGameObjectWithTag(Globals.Tags.InstantiatedObjectsTag).transform // to prevent editor littering
            );
            planet.DisableSpaceObjectMeshRenderer();
        }
    }
}