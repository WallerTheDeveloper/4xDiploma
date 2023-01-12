using UnityEngine;

namespace Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _targetToDestroy = null;
        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (_targetToDestroy != null)
                {
                    Destroy(_targetToDestroy, 5);
                }
                else Destroy(gameObject, 5);
            }     
        }
    }
}