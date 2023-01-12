using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipsUpgradingSystem
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        Dictionary<ShipType, Dictionary<Stat, float[]>> lookupTable = null;
        public float GetStat(Stat stat, ShipType shipType, int level)
        {
            BuildLookup();

            float[] levels = lookupTable[shipType][stat];

            if (levels.Length < level)
            {
                return 0;
            }

            return levels[level - 1];
        }

        public int GetLevels(Stat stat, ShipType shipType)
        {
            BuildLookup();

            float[] levels = lookupTable[shipType][stat];
            return levels.Length;
        }
        
        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<ShipType, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }
                lookupTable[progressionClass.ShipType] = statLookupTable;
            }
        }
        
        [System.Serializable]
        class ProgressionCharacterClass
        {
            public ShipType ShipType;
            public ProgressionStat[] stats;
        }
        
        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}