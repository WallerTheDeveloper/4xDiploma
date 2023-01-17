using System;
using Core;
using Core.Miscellaneous;
using Core.UnitSelection;
using ShipsUpgradingSystem;
using UnityEngine;

namespace Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private GameObject explodeAfterDeadEffect;
        private bool is_dead = false;
        private LazyValue<float> _healthPoints;
        
        public static event Action OnHealthChange;
        public static event Action OnLevelChange;

        private void Awake() {
            _healthPoints = new LazyValue<float>(GetInitialHealth);
        }
        
        private void Start()
        {
            _healthPoints.ForceInit();
        }
        
        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetBaseStat(Stat.Health);
        }
        
        public bool IsDead()
        {
            return is_dead;
        }

        public float GetHealthPoints()
        {
            return _healthPoints.value;
        }
        
        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            _healthPoints.value = Mathf.Max(_healthPoints.value - damage, 0);
            OnHealthChange?.Invoke();
            if (_healthPoints.value == 0)
            {
                Die();
                print("dead");
                AwardExperience(instigator);
            }
        }
        
        private void Die()
        {
            if (is_dead) return;
            is_dead = true;
            GameObject effect = Instantiate(explodeAfterDeadEffect, transform.position, Quaternion.identity);
            GetComponent<ActionScheduler>().CancelCurrentAction();
            UnitSelections.Instance.unitsSelected.Remove(gameObject);
            Destroy(gameObject);
            Destroy(effect, 5f);
        }
        
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
        
            experience.GainExperience(GetComponent<BaseStats>().GetBaseStat(Stat.ExperienceReward));
            OnLevelChange?.Invoke();
        }
    }
}
