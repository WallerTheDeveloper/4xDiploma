using PlayerInteractable.SpaceObjects;
using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "PlanetsData", menuName = "My Scriptable Objects/Create Planets Data")]
    public class PlanetsData : ScriptableObject
    {
        [System.Serializable]
        public struct Data
        {
            public Planet planetPrefab;
            [Range(0f, 100f)] public float spawnProbability;
        }
        [SerializeField]
        public Data[] data;

    }
}