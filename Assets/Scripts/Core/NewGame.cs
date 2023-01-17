using System;
using System.Collections.Generic;
using AI;
using Cinemachine;
using Core.Data;
using Core.RaceChoice;
using Core.Spawners;
using Core.UnitSelection;
using Core.WorldGeneration;
using Loading;
using UI.MenuUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

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

        [SerializeField] private PatrolPathRandomSpawner _patrolPathRandomSpawner;
        
        private int _numberOfFrames = 0;
        
        private int _numberOfFrameToInitGame = 5;
        public string SceneName => Globals.Scenes.NEW_GAME;
        // public string SceneName => Globals.Scenes.MAIN_MENU;


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
            _constructionSpawner.Init(_shipsData, _aiShipsData, ProjectContext.Instance.RaceChoiceController, _patrolPathRandomSpawner);
            _spaceObjectsSpawner.Init(_planetsData);
            _worldGenerator.Init(_constructionSpawner, _spaceObjectsSpawner);
            _unitSelections.Init();
            _constructionBuildManager.Init(ProjectContext.Instance.RaceChoiceController, _constructionSpawner);
        }
        public void Init()
        {
        }
        private void BasicGameConfiguration()
        {
            MoveCameraToChosenRaceShip();
        }
        
        public void MoveCameraToChosenRaceShip()
        {
            for (int i = 0; i < _shipsData.data.Length; i++)
            {
                if (_shipsData.data[i].RaceTypes == ProjectContext.Instance.RaceChoiceController.ChosenRace)
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
        private void ResetData()
        {
            foreach (var data in _shipsData.data)
            {
                data.ShipsPositions = new List<Vector3>();

            }

            foreach (var aidata in _aiShipsData.data)
            {
                aidata.ShipsPositions = new List<Vector3>();
            }
        }
        
        private void OnDestroy()
        {
            ResetData();
        }
    }
}