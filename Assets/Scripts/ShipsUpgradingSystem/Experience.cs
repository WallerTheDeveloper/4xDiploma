using System;
using UnityEngine;

namespace ShipsUpgradingSystem
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0;
        public event Action onExperienceGained;

        public float GetExperience()
        {
            return experiencePoints;
        }
        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained?.Invoke();
        }
    }
} 