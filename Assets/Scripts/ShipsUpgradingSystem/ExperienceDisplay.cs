using System;
using Attributes;
using TMPro;
using UnityEngine;

namespace ShipsUpgradingSystem
{
    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _experienceAmount;
        [SerializeField] private TextMeshProUGUI _levelText;
        
        private BaseStats _baseStats;
        private Experience _experience;

        private void OnEnable()
        {
            Health.OnLevelChange += ChangeLevelText;
        }
        
        private void Start()
        {
            _experience = GetComponentInParent<Experience>();
            _baseStats = GetComponentInParent<BaseStats>();

            _experienceAmount.SetText(_experience.GetExperience().ToString());
        }
        private void ChangeLevelText()
        {
            _experienceAmount.SetText(_experience.GetExperience().ToString());
            _levelText.SetText(_baseStats.GetLevel() + " / 3");
        }
    }
}
