using System;
using Core;
using UnityEngine;

namespace PlayerInteractable.SpaceObjects
{
    public class Star : Planet
    {
        public static event Action<Transform> OnPopUpMenuButtonClicked;
        public void OnBuildButtonClick() // Editor event function 
        {
            if (Globals.Bools.hasReachedDestination)
            {
                PlanetActionsManager.Instance.ClickedObjects.Add(this);
                _popUpMenu.ActivatePopUpMenu(false);
                OnPopUpMenuButtonClicked?.Invoke(this.transform);
            }
        }
    }
}
