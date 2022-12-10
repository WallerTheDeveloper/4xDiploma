using System;
using UnityEngine;

namespace Core.Map
{
    public class DisplayIconBehaviour : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var parentObjectTag = gameObject.transform.parent.tag;
            if (parentObjectTag == Globals.Tags.inActiveObjectTag)
            {
                _spriteRenderer.enabled = false;
            }
            else
            {
                _spriteRenderer.enabled = true;
            }
        }
    }
}
