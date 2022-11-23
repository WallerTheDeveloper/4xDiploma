using System.Collections.Generic;
using System.IO;
using PlayerInteractable.Constructions;
using Races;
using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "ShipsData", menuName = "My Scriptable Objects/Create Ships Data")]
    public class ShipsData : ScriptableObject
    {
        [System.Serializable]
        public class Data
        {
            public RaceTypes RaceTypes;
            public BasicShip[] shipTypes;
            public List<Vector3> ShipsPositions = new List<Vector3>();
        }
        
        [SerializeField]
        public Data[] data;
    }
}
