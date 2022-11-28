using UnityEngine;
using Core.Spawners;

namespace Core.WorldGeneration
{ 
    public class WorldGenerator : MonoBehaviour
    {
        [Range(5f, 10f)] 
        public static float ShipsSpawnRadius = 5f;
        
        [Range(0.7f, 3f)] 
        public static float PlanetsSpawnDensity = 1f;
        
        private ConstructionSpawner _constructionSpawner;
        private SpaceObjectsSpawner _spaceObjectsSpawner;
        
        private const int SPAWN_ITERATIONS = 500;
        
        public void Init(ConstructionSpawner constructionSpawner, SpaceObjectsSpawner spaceObjectsSpawner)
        {
            _constructionSpawner = constructionSpawner;
            _spaceObjectsSpawner = spaceObjectsSpawner;
            GenerateShipInRandomPoints();
            GeneratePlanetsInRandomPoints();
        }
        private void GenerateShipInRandomPoints()
        {
            _constructionSpawner.SpawnPlayerDrivenShips();
            _constructionSpawner.SpawnAIDrivenShips();
        }
        private void GeneratePlanetsInRandomPoints()
        {
            for (int i = 0; i < SPAWN_ITERATIONS; i++)
            {
                _spaceObjectsSpawner.SpawnPlanetWithProbability();
            }
        }
    }
}