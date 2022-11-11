using Core.Data;
using Core.RaceChoice;
using UnityEngine;

namespace Core
{
    public class InitialLoadSetup : MonoBehaviour
    {
        [SerializeField] private ShipsData ShipsData;
        [SerializeField] private CameraData CameraData;
        
        [SerializeField] private RaceChoiceController RaceChoiceController;
        private void Start()
        {
            MoveCameraToChosenRaceShip();
            EnableInputForChosenRaceShips();
        }

        private void MoveCameraToChosenRaceShip()
        {
            for (int i = 0; i < RaceChoiceController.RaceTypesList.Count; i++)
            {
                if (RaceChoiceController.RaceTypesList[i] == RaceChoiceController.ChosenRace)
                {
                    Transform cameraRigTransform = CameraData.CameraTransform.parent.transform;
                    
                    cameraRigTransform.position = new Vector3(
                        ShipsData.data[i].shipsPositions[i].x,
                        cameraRigTransform.position.y,
                        ShipsData.data[i].shipsPositions[i].z);
                    
                    break;
                }
            }
        }

        private void EnableInputForChosenRaceShips()
        {
            
        }
    }
}