using Core;
using Core.ResourceGatheringSystem;

namespace PlayerInteractable.SpaceObjects
{
    public class GasPlanet : Planet
    {
        public int RocketGasAmount { get; set; } = 80;
        protected override int Alloys { get; set; } = 20;

        public override void GatherResource()
        {
            if (Globals.Bools.hasReachedDestination)
            {
                ResourceGatherManager.Instance.AddRocketGas(RocketGasAmount);
                ResourceGatherManager.Instance.AddAlloys(Alloys);
                base.GatherResource();
            }
        }
    }
}