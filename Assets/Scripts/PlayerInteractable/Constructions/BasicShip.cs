using Attributes;
using Core;
using Movement;
using UnityEngine;

namespace PlayerInteractable.Constructions
{
    public abstract class BasicShip : Mover, IFlyable
    {
        public void Fly()
        {
            Globals.isFlyTriggered = true;
            StartCoroutine(SmoothRotate());
            StartCoroutine(CalculateMovement());
        }
    }
}