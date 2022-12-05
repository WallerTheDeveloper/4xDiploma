using Core.ResourceGatheringSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class GamePanelUI : MonoBehaviour
    {
        public TextMeshProUGUI AlloysAmountText;
        public TextMeshProUGUI RedCrystalsAmountText;
        public TextMeshProUGUI RocketGasAmountText;
        public TextMeshProUGUI MineralsAmountText;

        public Button BuildEngineeringShipButton;
        public Button BuildWarshipButton;
        public Button BuildExploringShipButton;

        public UnityEvent OnLackOfResources;

        public void UpdateAlloysText()
        {
            AlloysAmountText.SetText(ResourceGatherManager.Instance.AlloysAmount.ToString());
        }

        public void UpdateRedCrystalsText()
        {
            RedCrystalsAmountText.SetText(ResourceGatherManager.Instance.RedCrystalsAmount.ToString());
        }

        public void UpdateRocketGasText()
        {
            RocketGasAmountText.SetText(ResourceGatherManager.Instance.RocketGasAmount.ToString());
        }

        public void UpdateMineralsText()
        {
            MineralsAmountText.SetText(ResourceGatherManager.Instance.MineralsAmount.ToString());
        }
    }
}