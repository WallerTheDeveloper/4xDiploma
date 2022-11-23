using Core.Data;
using UnityEngine;
using Core.Miscellaneous;
using Core.RaceChoice;
using PlayerInteractable.SpaceObjects;
using PlayerInteractable.Constructions;
using UI.MenuUI;
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

        private PlanetsData _planetsData;
        private ShipsData _shipsData;
        private ShipsData _aiShipsData;
        private ConstructionSpawner _constructionSpawner;
        private RaceChoiceController _raceChoiceController;
        
        private const int SPAWN_ITERATIONS = 500;
        
        public void Init(ShipsData shipsData, ShipsData aiShipsData, ConstructionSpawner constructionSpawner, PlanetsData planetsData, RaceChoiceController raceChoiceController)
        {
            _shipsData = shipsData;
            _aiShipsData = aiShipsData;
            _constructionSpawner = constructionSpawner;
            _planetsData = planetsData;
            _raceChoiceController = raceChoiceController;
            
            SpawnShipsInRandomPoints();
            GeneratePlanetsInRandomPoints();
        }
        private void SpawnShipsInRandomPoints()
        {
            //Spawn Player driven ships
            for (int i = 0; i < _shipsData.data.Length; i++)
            {
                Vector3 randomPointAtMap = GenerateRandomPointAtMap();

                for (int j = 0; j < _shipsData.data[i].shipTypes.Length; j++)
                {
                    if (_shipsData.data[i].RaceTypes == _raceChoiceController.ChosenRace)
                    {
                        _constructionSpawner.SpawnShip(randomPointAtMap, _shipsData.data[i].shipTypes[j], shipsSpawnRadius, out Vector3 shipNewPosition);
                        _shipsData.data[j].ShipsPositions.Add(shipNewPosition);
                    }
                }
            }
            // Spawn AI driven ships
            for (int i = 0; i < _aiShipsData.data.Length; i++)
            {
                Vector3 randomPointAtMap = GenerateRandomPointAtMap();
                for (int j = 0; j < _aiShipsData.data[i].shipTypes.Length; j++)
                {
                    if (_aiShipsData.data[i].RaceTypes != _raceChoiceController.ChosenRace)
                    {
                        _constructionSpawner.SpawnShip(randomPointAtMap, _aiShipsData.data[i].shipTypes[j], shipsSpawnRadius, out Vector3 shipNewPosition);
                    }
                }
            }
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

                bool checkObjectsInRadius = Physics.CheckSphere(randomPointAtMap, Planet.COLLIDER_RADIUS / planetsSpawnDensity);
                
                if (checkObjectsInRadius)
                {
                    continue;
                }
                
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
                planet.gameObject.tag = Globals.Tags.inActiveObjectTag;
            }
        }

    }
}