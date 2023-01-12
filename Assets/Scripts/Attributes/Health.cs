using Core;
using Core.Miscellaneous;
using ShipsUpgradingSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float regenerationPercentage = 70;
        // [SerializeField] private UnityEvent takeDamage;
        
        private bool is_dead = false;
        private LazyValue<float> _healthPoints;
        // private void Awake() {
        //     _healthPoints = new LazyValue<float>(GetInitialHealth);
        // }

        // private float GetInitialHealth()
        // {
        //     return GetComponent<BaseStats>().GetStat(Stat.Health);
        // }

        private void Start()
        {
            _healthPoints.ForceInit();
        }

        // private void OnEnable()
        // {
        //     GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        // }
        //
        // private void OnDisable()
        // {
        //     GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        // }
        //
        public bool IsDead()
        {
            return is_dead;
        }
        //
        // public float GetHealthPoints()
        // {
        //     return _healthPoints.value;
        // }
        //
        // public float GetMaxHealthPoints()
        // {
        //     return GetComponent<BaseStats>().GetStat(Stat.Health);
        // }
        //
        // public float GetPercentage()
        // {
        //     return 100 * (_healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
        // }
        //
        // public void TakeDamage(GameObject instigator, float damage)
        // {
        //     print(gameObject.name + " took damage: " + damage);
        //     _healthPoints.value = Mathf.Max(_healthPoints.value - damage, 0);
        //     if (_healthPoints.value == 0)
        //     {
        //         Die();
        //         AwardExperience(instigator);
        //     }
        //     else
        //     {
        //         takeDamage.Invoke();
        //     }
        // }
        //
        // private void Die()
        // {
        //     if (is_dead) return;
        //     is_dead = true;
        //     GetComponent<Animator>().SetTrigger("die");
        //     GetComponent<ActionScheduler>().CancelCurrentAction();
        // }
        //
        // private void AwardExperience(GameObject instigator)
        // {
        //     Experience experience = instigator.GetComponent<Experience>();
        //     if (experience == null) return;
        //
        //     experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        // }

        // private void RegenerateHealth()
        // {
        //     float regenerateHP = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
        //     _healthPoints.value = Mathf.Max(_healthPoints.value, regenerateHP);
        // }
    }
}