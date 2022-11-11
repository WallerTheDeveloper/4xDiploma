// using Core.Data;
// using Core.RaceChoice;
// using Races;
// using UnityEngine;
//
// namespace Core
// {
//     public class InitialGameSetup : MonoBehaviour
//     {
//
//         [SerializeField] 
//         private CameraData CameraData;
//
//         [SerializeField] 
//         private ShipsData ShipsData;
//         private void OnEnable()
//         {
//             RaceChoiceController.OnGameInit += MoveCameraToChosenRaceShip;
//         }
//         
//         private void MoveCameraToChosenRaceShip()
//         {
//             for (int i = 0; i < RaceChoiceController.RaceTypesList.Count; i++)
//             {
//                 if (RaceChoiceController.RaceTypesList[i] == RaceChoiceController.ChosenRace)
//                 {
//                     CameraData.CameraTransform.position = ShipsData.data[i].shipsPositions[i];
//                 }
//             }
//         }
//     }
// }
