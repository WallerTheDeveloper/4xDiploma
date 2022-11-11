using System;
using System.Collections.Generic;
using System.Linq;
using UI.MenuUI;
using UnityEngine;
using Races;

namespace Core.RaceChoice
{
    [CreateAssetMenu(fileName = "RaceChoiceController", menuName = "My Scriptable Objects/Create RaceChoiceController")]
    public class RaceChoiceController : ScriptableObject
    {

        public static event Action OnGameInit;
        public RaceTypes ChosenRace { get; private set; }

        public static List<RaceTypes> RaceTypesList = 
            Enum.GetValues(typeof(RaceTypes))
                .Cast<RaceTypes>()
                .ToList();
        private void OnEnable()
        {
            ChooseRaceMenuView.OnRaceChoice += HandleRaceChoice;
        }

        private void HandleRaceChoice(RaceTypes race)
        {
            switch (race)
            {
                case RaceTypes.MachineRace:
                {  
                    Debug.Log("Machine choice!");
                    ChosenRace = RaceTypes.MachineRace;
                    break;
                }
                case RaceTypes.FungoidRace:
                {
                    Debug.Log("Fungoid choice!");
                    ChosenRace = RaceTypes.FungoidRace;
                    break;
                }
                case RaceTypes.HumanRace:
                {
                    Debug.Log("Human choice!");
                    ChosenRace = RaceTypes.HumanRace;
                    break;
                }
            }
        }
    }
}