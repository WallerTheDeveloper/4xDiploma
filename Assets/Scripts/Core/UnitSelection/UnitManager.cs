using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UnitSelection
{
    public class UnitManager: MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _selectionCircle;
        
        private void Start()
        {
            addPhysicsRaycaster();
        }

        public void OnPointerDown(PointerEventData eventData) // OnMouseDown alternative in new input system (only works with UI, but also with GameObjects if you add Physics Raycaster to Camera)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Globals.isClickedOnSingleUnit = true;
                SelectUnit();
            }
        }
        
        void addPhysicsRaycaster() // TODO: Move to another file! Name it PhysicsRaycasterProvider 
        {
            PhysicsRaycaster physicsRaycaster = FindObjectOfType<PhysicsRaycaster>();
            if (physicsRaycaster == null)
            {
                Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
            }
        }
      
        public void SelectUnit()
        {
            if (Globals.SELECTED_UNITS.Contains(this)) return;
            Globals.isClickedOnSingleUnit = false;
            Globals.SELECTED_UNITS.Add(this);
            _selectionCircle.SetActive(true);
        }
        public void DeselectUnit()
        {
            if (!Globals.SELECTED_UNITS.Contains(this)) return;

            Globals.SELECTED_UNITS.Remove(this);
            
            _selectionCircle.SetActive(false);
        }
    }
}