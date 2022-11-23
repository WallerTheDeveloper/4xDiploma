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
        public RaceTypes ChosenRace { get; private set; } // eventually move to another script (RaceManager?)

        public static List<RaceTypes> RaceTypesList = 
            Enum.GetValues(typeof(RaceTypes))
                .Cast<RaceTypes>()
                .ToList(); // eventually move to another script (RaceManager?)

        private void OnEnable()
        {
            ChooseRaceMenuView.OnRaceChoice += HandleRaceChoice;
            Debug.Log("OnEnableRaceChoiceController");
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