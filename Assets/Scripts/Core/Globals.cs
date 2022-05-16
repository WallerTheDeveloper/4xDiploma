using System.Collections.Generic;
using Core.UnitSelection;

namespace Core
{
    public class Globals
    {
        public static int PLANET_LAYER_MASK = 1 << 6; // Layer name: Planet
        public static int SHIP_LAYER_MASK = 1 << 7; // Layer name: Ship
        public static List<UnitManager> SELECTED_UNITS = new List<UnitManager>();
    }
}