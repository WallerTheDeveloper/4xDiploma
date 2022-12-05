using UI;
using UnityEngine;

namespace Core.ResourceGatheringSystem
{
    public class ResourceGatherManager : MonoBehaviour
    {
        public int AlloysAmount;
        public int RedCrystalsAmount;
        public int RocketGasAmount;
        public int MineralsAmount;
        
        [SerializeField] private GamePanelUI _gamePanelUI;
        public static ResourceGatherManager Instance { get; private set; }
        private void Init()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Awake()
        {
            Init();
        }

        public void AddAlloys(int amount)
        {
            AlloysAmount += amount;
            _gamePanelUI.UpdateAlloysText();
        }
        public void AddRedCrystals(int amount)
        {
            RedCrystalsAmount += amount;
            _gamePanelUI.UpdateRedCrystalsText();
        }
        public void AddRocketGas(int amount)
        {
            RocketGasAmount += amount;
            _gamePanelUI.UpdateRocketGasText();
        }
        public void AddMinerals(int amount)
        {
            MineralsAmount += amount;
            _gamePanelUI.UpdateMineralsText();
        }
    }
}