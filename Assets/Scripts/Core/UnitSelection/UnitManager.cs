using System;
using System.Collections.Generic;
using Ships;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace Core.UnitSelection
{
    public class UnitManager: MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _selectionCircle;

        public void OnPointerDown(PointerEventData eventData) // OnMouseDown alternative in new input system
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                SelectSingleUnit();
            }
        }
        
        public void SelectUnit()
        {
            if (Globals.SELECTED_UNITS.Contains((this))) return;

            Globals.SELECTED_UNITS.Add(this);

            _selectionCircle.SetActive(true);
        }

        public void SelectSingleUnit()
        {
            Ray _ray;
            RaycastHit _raycastHit;
            _ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(_ray, out _raycastHit, Mathf.Infinity, Globals.SHIP_LAYER_MASK))
            {
                Globals.SELECTED_UNITS.Add(this);
                _selectionCircle.SetActive(true);
            }
        }

        public void DeselectUnit()
        {
            if (!Globals.SELECTED_UNITS.Contains(this)) return;

            Globals.SELECTED_UNITS.Remove(this);
            
            _selectionCircle.SetActive(false);
        }
        
    }
}