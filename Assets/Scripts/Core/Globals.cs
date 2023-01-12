namespace Core
{
    public class Globals
    {
        public class Bools
        {
            public static bool IsOneUnitSelected = false;
            public static bool HasReachedDestination = false;
        }

        public class Layers
        {
            public static int PLANET_LAYER_MASK = 1 << 6; // Layer name: Planet
            public static int CONSTRUCTION_LAYER_MASK = 1 << 7; // Layer name: Construction
            public static int CONSTRUCTION_LAYER = 7;
        }
        public class Tags
        {
            public const string ActiveObjectTag = "Active Object";
            public const string InActiveObjectTag = "Inactive Object";
            public const string PatrolWaysTag = "Patrol Ways";
            public const string PatrolPathTag = "Patrol Path";
            public const string InstantiatedObjectsTag = "Instantiated Planets";
            public const string AIUnitTag = "AI Unit";
        }
        public class Scenes
        {
            public const string MAIN_MENU = "Main Menu Scene";
            public const string NEW_GAME = "Game scene";
        }
        
    }
}