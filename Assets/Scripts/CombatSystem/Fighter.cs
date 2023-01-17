using System;
using Attributes;
using Core;
using Core.Miscellaneous;
using Movement;
using ShipsUpgradingSystem;
using UnityEngine;

namespace CombatSystem
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;

        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] Weapon defaultWeapon = null;
        LazyValue<Weapon> currentWeapon;
        private Mover _mover; 
    
        private void Awake() {
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
            _mover = GetComponent<Mover>();
        }
        private void Start()
        {
            currentWeapon.ForceInit();
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            
            if (!GetIsInRange())
            {
                _mover.MoveTo(target.transform.position);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }
        private Weapon SetupDefaultWeapon()
        {
            return defaultWeapon;
        }
        
        private void AttackBehaviour()    
        {
            print("AttackBeh!!!");
            transform.LookAt(target.transform);
            float damage = GetComponent<BaseStats>().GetBaseStat(Stat.Damage);
            if (timeSinceLastAttack > timeBetweenAttacks) 
            {
                currentWeapon.value.LaunchProjectile(target, gameObject, damage);
                timeSinceLastAttack = 0;
            }
        }

        private bool GetIsInRange() 
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.GetRange();
        }
        
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
            // return targetToTest != null;
        }
        
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().PerformAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        
        public void Cancel()
        {
            // target = null;
            // GetComponent<Mover>().Cancel();
        }
    }
}
