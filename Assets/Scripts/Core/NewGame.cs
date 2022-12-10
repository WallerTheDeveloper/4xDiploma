using System.Collections.Generic;
using Cinemachine;
using Core.Data;
using Core.RaceChoice;
using Core.Spawners;
using Core.UnitSelection;
using Core.WorldGeneration;
using Loading;
using UI.MenuUI;
using UnityEngine;

namespace Core
{
    public class NewGame : MonoBehaviour, ICleanUp
    {
        [SerializeField] private ShipsData _shipsData;
        
        [SerializeField] private ShipsData _aiShipsData;

        [SerializeField] private PlanetsData _planetsData;
        
        [SerializeField] private CameraData _cameraData;
        
        // [SerializeField] private RaceChoiceController _raceChoiceController;
        
        [SerializeField] private PauseMenuView _pauseMenuView;
        
        [SerializeField] private WorldGenerator _worldGenerator;

        [SerializeField] private ConstructionSpawner _constructionSpawner;

        [SerializeField] private ConstructionBuildManager _constructionBuildManager;
        
        [SerializeField] private SpaceObjectsSpawner _spaceObjectsSpawner;

        [SerializeField] private UnitSelections _unitSelections;
        
        private int _numberOfFrames = 0;
        
        private int _numberOfFrameToInitGame = 5;
        public string SceneName => Globals.Scenes.NEW_GAME;
        
        private void OnApplicationQuit()
        {
            ResetPositionsOnQuit();
        }

        private void Update()
        {
            if (_numberOfFrames <= _numberOfFrameToInitGame)
            {
                BasicGameConfiguration();
                _numberOfFrames++;
            }
        }

        private void Awake()
        {
            _constructionSpawner.Init(_shipsData, _aiShipsData, ProjectContext.Instance.RaceChoiceController);
            _spaceObjectsSpawner.Init(_planetsData);
            _worldGenerator.Init(_constructionSpawner, _spaceObjectsSpawner);
            _unitSelections.Init();
            _constructionBuildManager.Init(ProjectContext.Instance.RaceChoiceController, _constructionSpawner);
        }
        public void Init()
        {
            _pauseMenuView.QuitGame += GoToMainMenu;
        }
        private void BasicGameConfiguration()
        {
            MoveCameraToChosenRaceShip();
        }
        public void MoveCameraToChosenRaceShip()
        {
            for (int i = 0; i < RaceChoiceController.RaceTypesList.Count; i++)
            {
                if (RaceChoiceController.RaceTypesList[i] == ProjectContext.Instance.RaceChoiceController.ChosenRace)
                {
                    Transform cameraRigTransform = _cameraData.CameraTransform.parent.transform;
                    
                    cameraRigTransform.position = new Vector3(
                        _shipsData.data[i].ShipsPositions[0].x,
                        cameraRigTransform.position.y,
                        _shipsData.data[i].ShipsPositions[0].z
                    );
                    break;
                }
            }
        }
        private void GoToMainMenu()
        {
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new ClearGameOperation(this));
            ProjectContext.Instance.LoadingScreenProvider.LoadAndDestroy(operations);
        }
        private void ResetPositionsOnQuit()
        {
            foreach (var data in _shipsData.data)
            {
                data.ShipsPositions = new List<Vector3>();
            } 
        }
    }
}