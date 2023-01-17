using System;
using Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CombatSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        private Health _health;
        private Slider _healthSlider;

        private void OnEnable()
        {
            Health.OnHealthChange += ChangeHealth;
        }

        private void OnDisable()
        {
            Health.OnHealthChange -= ChangeHealth;
        }

        private void Awake()
        {
            _health = GetComponentInParent<Health>();
            _healthSlider = GetComponent<Slider>();
        }

        private void Start()
        {
            _healthSlider.maxValue = _health.GetHealthPoints();
            ChangeHealth();
        }

        private void ChangeHealth()
        {
            _amountText.SetText(_health.GetHealthPoints() + " / " + _healthSlider.maxValue);
            _healthSlider.value = _health.GetHealthPoints();
        }
    }
}