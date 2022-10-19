using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.Miscellaneous;
using PlayerInteractable;
using Random = UnityEngine.Random;

namespace Core.WorldGeneration
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private Planet[] planetsType;
            
        [SerializeField]
        [Range(0.7f, 3f)]
        private float spawnDensity = 1f;
            
        private List<Planet> _instantiatedPlanets;

        private readonly int[] _spawnWeight = Enumerable.Range(1, 100).ToArray();
        private readonly int PLANET_SPAWN_ITERATIONS = 2000;
        
        private void Start()
        {
            _instantiatedPlanets = new List<Planet>();
            GeneratePlanetsInRandomPoints();
        }
        
        
        private void GeneratePlanetsInRandomPoints()
        {
            for (int i = 0; i < PLANET_SPAWN_ITERATIONS; i++)
            {
                int randomPlanet = Random.Range(0, planetsType.Length);

                Vector3 randomPlanetPointOnMap = GenerateRandomPoint();
                
                if (Physics.CheckSphere(randomPlanetPointOnMap, Planet.COLLIDER_RADIUS / spawnDensity)) continue;

                var planet = Instantiate(planetsType[randomPlanet], randomPlanetPointOnMap, Quaternion.identity);
                _instantiatedPlanets.Add(planet);
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