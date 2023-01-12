// using System.Collections.Generic;
// using Attributes;
// using Core;
// using Core.Miscellaneous;
// using Movement;
// using UnityEngine;
//
// using ShipsUpgradingSystem;
//
// namespace RPG.Combat
// {
//     public class Fighter : MonoBehaviour, IAction
//     {
//         private Health target;
//         private float timeSinceLastAttack = Mathf.Infinity;
//         
//         
//         [SerializeField] private float timeBetweenAttacks = 1f;
//         [SerializeField] Transform rightHandTransform = null;
//         [SerializeField] Transform leftHandTransform = null;
//         // [SerializeField] Weapon defaultWeapon = null;
//         // LazyValue<Weapon> currentWeapon;
//
//         private void Awake() {
//             // currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
//         }
//         private void Start()
//         {
//             // currentWeapon.ForceInit();
//         }
//         private void Update()
//         {
//             timeSinceLastAttack += Time.deltaTime;
//             if (target == null) return;
//             if (target.IsDead()) return;
//             
//             if (!GetIsInRange())
//             {
//                 // GetComponent<Mover>().MoveTo(target.transform.position);
//             }
//             else
//             {
//                 GetComponent<Mover>().Cancel();
//                 AttackBehaviour();
//             }
//         }
//         // private Weapon SetupDefaultWeapon()
//         // {
//             // AttachWeapon(defaultWeapon);
//             // return defaultWeapon;
//         // }
//         public Health GetTarget()
//         {
//             return target;
//         }
//         // public void EquipWeapon(Weapon weapon)
//         // {
//             // currentWeapon.value = weapon;
//             // AttachWeapon(weapon);
//         // }
//
//         // private void AttachWeapon(Weapon weapon)
//         // {
//             // Animator animator = GetComponent<Animator>();
//             // weapon.Spawn(rightHandTransform, leftHandTransform, animator);
//         // }
//         private void AttackBehaviour()    
//         {
//             transform.LookAt(target.transform);
//             if (timeSinceLastAttack > timeBetweenAttacks) 
//             {
//                 TriggerAttack();
//                 timeSinceLastAttack = 0;
//             }
//         }
//         private void TriggerAttack()
//         {
//             GetComponent<Animator>().ResetTrigger("stopAttack");
//             GetComponent<Animator>().SetTrigger("attack");
//         }
//         public bool CanAttack(GameObject combatTarget)
//         {
//             if (combatTarget == null) { return false; }
//             Health targetToTest = combatTarget.GetComponent<Health>();
//             return targetToTest != null && !targetToTest.IsDead();
//         }
//         
//         public void Attack(GameObject combatTarget)    
//         {
//             // GetComponent<ActionScheduler>().StartAction(this);
//             target = combatTarget.GetComponent<Health>();
//         }
//         
//         // Animation Event
//         void Hit()
//         {
//             if (target == null) { return; }
//
//             float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
//             
//             if (currentWeapon.value.HasProjectile())
//             {
//                 currentWeapon.value.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
//             }
//             else
//             {
//                 target.TakeDamage(gameObject, damage);
//             }
//         }    
//         
//         void Shoot()
//         {
//             Hit();
//         }
//         private bool GetIsInRange()
//         {
//             return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.GetRange();
//         }
//
//         public void Cancel()
//         {
//             StopAttack();
//             target = null;
//             GetComponent<Mover>().Cancel();
//         }
//         
//         private void StopAttack()
//         {
//             GetComponent<Animator>().ResetTrigger("attack");
//             GetComponent<Animator>().SetTrigger("stopAttack");
//         }
//         
//         public IEnumerable<float> GetAdditiveModifiers(Stat stat)
//         {
//             if (stat == Stat.Damage)
//             {
//                 yield return currentWeapon.value.GetDamage();
//             }
//         }
//
//         public IEnumerable<float> GetPercentageModifiers(Stat stat)
//         {
//             if (stat == Stat.Damage)
//             {
//                 yield return currentWeapon.value.GetPercentageBonus();
//             }
//         }
//
//     }
// }
