using Attributes;
using Control;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(InputController callingController)
        {
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }

            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                print("Combat target");
                callingController.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
    }
}