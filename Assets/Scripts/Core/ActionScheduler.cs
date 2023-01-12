using UnityEngine;

namespace Core
{
    public class ActionScheduler: MonoBehaviour // inherits from MonoBehaviour to be able to attach this script to an GameObject
    {
        private IAction _currentAction;

        public void PerformAction(IAction action)
        {
            if (_currentAction == action) return;

            if (_currentAction != null)
            {
                _currentAction.Cancel();
            }
            _currentAction = action;
        }
        public void CancelCurrentAction()
        {
            PerformAction(null);
        }
    }
}