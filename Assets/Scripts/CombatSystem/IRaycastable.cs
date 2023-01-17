using Control;

namespace CombatSystem
{
    public interface IRaycastable
    {
        bool HandleRaycast(InputController callingController);
    }
}