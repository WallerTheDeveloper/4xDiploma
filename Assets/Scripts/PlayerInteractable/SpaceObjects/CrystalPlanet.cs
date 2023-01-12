using Core;
using Core.ResourceGatheringSystem;

namespace PlayerInteractable.SpaceObjects
{
    public class CrystalPlanet : Planet
    {
        public int CrystalsAmount { get; set; } = 70;
        protected override int Alloys { get; set; } = 50;

        public override void GatherResource()
        {
            if (Globals.Bools.HasReachedDestination)
            {
                ResourceGatherManager.Instance.AddRedCrystals(CrystalsAmount);
                ResourceGatherManager.Instance.AddAlloys(Alloys);
                base.GatherResource();
            }
        }
    }
}
