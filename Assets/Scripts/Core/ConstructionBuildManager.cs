using Core.Data;
using Core.RaceChoice;
using Core.ResourceGatheringSystem;
using Core.Spawners;
using Core.WorldGeneration;
using UI;
using UnityEngine;


namespace Core
{
    public class ConstructionBuildManager : MonoBehaviour
    {
        [SerializeField] private GamePanelUI _gamePanelUI;
        [SerializeField] private ShipsData _shipsData;
        
        [SerializeField] private int _anyShipPrice = 100;
        [SerializeField] private int _engineeringShipPrice = 300;
        [SerializeField] private int _exlopringShipPrice = 120;
        [SerializeField] private int _warshipPrice = 150;
        
        private RaceChoiceController _raceChoiceController;
        private ConstructionSpawner _constructionSpawner;
        public static ConstructionBuildManager Instance { get; private set; }
        
        public void Init(RaceChoiceController raceChoiceController, ConstructionSpawner constructionSpawner)
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }

            _raceChoiceController = raceChoiceController;
            _constructionSpawner = constructionSpawner;
        }
        public void BuildEngineeringShip() // event function
        {
            if (ResourceGatherManager.Instance.AlloysAmount < _anyShipPrice ||
                ResourceGatherManager.Instance.MineralsAmount < _engineeringShipPrice)
            {
                return;
            }
            
            const int engineeringShipNumberInList = 0;

            for (int i = 0; i < RaceChoiceController.RaceTypesList.Count; i++)
            {
                if (_raceChoiceController.ChosenRace == RaceChoiceController.RaceTypesList[i])
                {
                    _constructionSpawner.SpawnShipAtPoint(
                        _shipsData.data[i].ShipsPositions[0],
                        _shipsData.data[i].shipTypes[engineeringShipNumberInList],
                        WorldGenerator.ShipsSpawnRadius);
                    ResourceGatherManager.Instance.AlloysAmount -= _anyShipPrice;
                    ResourceGatherManager.Instance.MineralsAmount -= _engineeringShipPrice;
                    _gamePanelUI.UpdateAlloysText();
                    _gamePanelUI.UpdateMineralsText();
                }
            }
        }
        
        public void BuildExploringShip() // event function
        {
            if (ResourceGatherManager.Instance.AlloysAmount < _anyShipPrice ||
                ResourceGatherManager.Instance.RedCrystalsAmount < _exlopringShipPrice)
            {
                return;
            }
            const int exploringShipNumberInList = 1;
            
            for (int i = 0; i < RaceChoiceController.RaceTypesList.Count; i++)
            {
                if (_raceChoiceController.ChosenRace == RaceChoiceController.RaceTypesList[i])
                {
                    _constructionSpawner.SpawnShipAtPoint(
                        _shipsData.data[i].ShipsPositions[0], 
                        _shipsData.data[i].shipTypes[exploringShipNumberInList],
                        WorldGenerator.ShipsSpawnRadius);
                    ResourceGatherManager.Instance.AlloysAmount -= _anyShipPrice;
                    ResourceGatherManager.Instance.RedCrystalsAmount -= _exlopringShipPrice;
                    _gamePanelUI.UpdateAlloysText();
                    _gamePanelUI.UpdateRedCrystalsText();
                }
            }
        }
        
        public void BuildWarship() // event function
        {
            if (ResourceGatherManager.Instance.AlloysAmount < _anyShipPrice ||
                ResourceGatherManager.Instance.RocketGasAmount < _warshipPrice)
            {
                return;
            }   
            
            const int warshipNumberInList = 2;
            
            for (int i = 0; i < RaceChoiceController.RaceTypesList.Count; i++)
            {
                if (_raceChoiceController.ChosenRace == RaceChoiceController.RaceTypesList[i])
                {
                    _constructionSpawner.SpawnShipAtPoint(
                        _shipsData.data[i].ShipsPositions[0],
                        _shipsData.data[i].shipTypes[warshipNumberInList], 
                        WorldGenerator.ShipsSpawnRadius);
                    ResourceGatherManager.Instance.AlloysAmount -= _anyShipPrice;
                    ResourceGatherManager.Instance.RocketGasAmount -= _warshipPrice;
                    _gamePanelUI.UpdateAlloysText();
                    _gamePanelUI.UpdateRocketGasText();
                }
            }
        }
       
    }
}