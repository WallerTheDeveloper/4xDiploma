using Attributes;
using Core;
using Movement;
using UnityEngine;

namespace PlayerInteractable.Constructions
{
    public abstract class BasicShip : Mover, IFlyable
    {
        // [HideInInspector]
        // public Vector3 PositionInWorld { get; set; }

        public void Fly()
        {
            Globals.isFlyTriggered = true;
            StartCoroutine(SmoothRotate());
            StartCoroutine(CalculateMovement());
        }
    }
}