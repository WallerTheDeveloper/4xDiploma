using Attributes;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private float weaponRange = 1f; 
        [SerializeField] Projectile projectile = null;
        public void LaunchProjectile(Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, instigator.transform.position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}