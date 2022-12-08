using System.Collections.ObjectModel;
using System.Linq;
using PlayerInteractable.SpaceObjects;
using UI;
using UnityEngine;

namespace Core
{
    public class PlanetActionsManager : MonoBehaviour
    {
        public ObservableCollection<Planet> ClickedObjects = new ObservableCollection<Planet>();

        public static PlanetActionsManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
            ClickedObjects.CollectionChanged += RestrictActionForClickedPlanet;
        }

        public void RestrictActionForClickedPlanet(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) // if new Planet has been added to list action
            {
                var lastItem = ClickedObjects.Last();
                var clickedPlanetPopUpMenu = lastItem.gameObject.GetComponentInChildren<PopUpMenu>();
                Destroy(clickedPlanetPopUpMenu.gameObject);
            }
        }
    }
}

