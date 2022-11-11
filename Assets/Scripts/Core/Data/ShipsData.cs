using System;
using System.Collections.Generic;
using PlayerInteractable.Constructions;
using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "ShipsData", menuName = "My Scriptable Objects/Create Ships Data")]
    public class ShipsData : ScriptableObject
    {
        [System.Serializable]
        public class Data
        {
            public BasicShip[] shipTypes;
            public List<Vector3> shipsPositions = new List<Vector3>(3);
        }

        

        [SerializeField]
        public Data[] data;
    }
}
