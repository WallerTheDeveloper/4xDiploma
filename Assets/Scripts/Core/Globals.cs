namespace Core
{
    public class Globals
    {
        public class Bools
        {
            public static bool isFlyTriggered = false;
            public static bool isUnitsDeselected = false;
            public static bool isOneUnitSelected = false;
            public static bool hasReachedDestination = false;
        }

        public class Layers
        {
            public static int PLANET_LAYER_MASK = 1 << 6; // Layer name: Planet
            public static int CONSTRUCTION_LAYER_MASK = 1 << 7; // Layer name: Construction
        }
        public class Tags
        {
            public const string activeObjectTag = "Active Object";
            public const string inActiveObjectTag = "Inactive Object";
        }
        public class Scenes
        {
            public const string MAIN_MENU = "Main Menu Scene";
            public const string NEW_GAME = "Game scene";
        }
        
    }
}