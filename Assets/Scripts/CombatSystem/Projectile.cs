using Attributes;
using UnityEngine;

namespace CombatSystem
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1;
        [SerializeField] private GameObject hitEffect = null;
        GameObject instigator = null;
        Health target;
        private float damage = 0;
    
        private void Start()
        {
            transform.LookAt(target.transform.position);
        }
        
        void Update()
        {
            if (target == null) return;
            if (!target.IsDead())
            {
                transform.LookAt(target.transform.position);
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);      
        }
    
        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.instigator = instigator;
            this.damage = damage;
        }
       
        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Health>() != target) return;
            if(target.IsDead()) return;
            target.TakeDamage(instigator, damage);
            
            if (hitEffect != null)
            {
                // Instantiate(hitEffect, GetAimLocation(), transform.rotation);
                Instantiate(hitEffect, target.transform.position, transform.rotation);

            }
            Destroy(gameObject); 
        }
    }
}