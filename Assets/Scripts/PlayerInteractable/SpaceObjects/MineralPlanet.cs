using Core;
using Core.ResourceGatheringSystem;

namespace PlayerInteractable.SpaceObjects
{
    public class MineralPlanet : Planet
    {
        public int MineralsAmount { get; set; } = 90;
        protected override int Alloys { get; set; } = 30;
        public override void GatherResource()
        {
            if (Globals.Bools.hasReachedDestination)
            {
                ResourceGatherManager.Instance.AddMinerals(MineralsAmount);
                ResourceGatherManager.Instance.AddAlloys(Alloys);
                base.GatherResource();
            }
        }
    }
}