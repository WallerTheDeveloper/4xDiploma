using System;
using Core;
using PlayerInteractable.Constructions;
using UnityEngine;

namespace PlayerInteractable.SpaceObjects
{
    public class Star : Planet
    {
        // public static event Action<Transform> OnPopUpMenuButtonClicked;
        private bool isButtonClicked;
        public void OnBuildButtonClick() // Editor event function 
        {
            // if (Globals.Bools.hasReachedDestination)
            // {
            //     PlanetActionsManager.Instance.ClickedObjects.Add(this);
            //     _popUpMenu.ActivatePopUpMenu(false);
            //     OnPopUpMenuButtonClicked?.Invoke(this.transform);
            // }
            isButtonClicked = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == 7)
            {
                print("Layer");
                if (isButtonClicked)
                {
                    print("Button clicked");

                    var constructionGameObject = other.gameObject;
                    if (constructionGameObject.TryGetComponent(out EngineeringShip engineeringShip))
                    {
                        PlanetActionsManager.Instance.ClickedObjects.Add(this);
                        _popUpMenu.ActivatePopUpMenu(false);
                        engineeringShip.BuildStarBaseInRadius(this.transform);
                        isButtonClicked = false;
                    }   
                }
            }
        }
    }
}
