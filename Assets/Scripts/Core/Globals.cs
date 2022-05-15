using Ships;

namespace Core
{
    public class Globals
    {
        public static int PLANET_LAYER_MASK = 1 << 6; // Layer name: Planet

        public static ShipData[] SHIP_DATA =
        {
            new ShipData("Ship", 100),
        };
    }
}