using System;
using System.Collections.Generic;
using Attributes;
using Control;
using UnityEngine;

namespace Core.UnitSelection
{
    public class UnitSelections : MonoBehaviour
    {
        public List<GameObject> unitsList = new List<GameObject>();
        public List<GameObject> unitsSelected = new List<GameObject>();
        public static UnitSelections Instance { get; private set; }
        public void Init()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        public void Select(GameObject unitToSelect)
        {
            unitsSelected.Add(unitToSelect);
            ActivateSelectionCircle(unitToSelect, true);
        }
        
        public void ClickSelect(GameObject unitToAdd)
        {
            DeselectAll();
            Select(unitToAdd);
            ActivateMovement(unitToAdd,true);
        }
        
        public void ShiftClickSelect(GameObject unitToAdd)
        {
            if (!unitsSelected.Contains(unitToAdd))
            {
                Select(unitToAdd);
                ActivateMovement(unitToAdd,true);
            }
            else
            {
                ActivateMovement(unitToAdd, false);
                Deselect(unitToAdd);
            }
        }
        
        public void DragSelect(GameObject unitToAdd)
        {
            if (!unitsSelected.Contains(unitToAdd))
            {
                unitsSelected.Add(unitToAdd);
                ActivateSelectionCircle(unitToAdd, true);
                ActivateMovement( unitToAdd, true);
            }
        }
        public void DeselectAll()
        {
            foreach (var unit in unitsSelected)
            {
                ActivateMovement(unit,false);
                ActivateSelectionCircle(unit, false);
            }
            unitsSelected.Clear();
        }
        public void Deselect(GameObject unitToDeselect)
        {
            ActivateSelectionCircle(unitToDeselect, false);
            unitsSelected.Remove(unitToDeselect);
        }
        private void ActivateSelectionCircle(GameObject unit, bool isActive)
        {
            foreach (Transform child in unit.transform)
            {
                if (child.tag == Globals.Tags.ActivateSelection)
                {
                    // unit.transform.GetChild(1).gameObject.SetActive(isActive);
                    child.gameObject.SetActive(isActive);
                }
            }
        }
        private void ActivateMovement(GameObject unit, bool isEnabled)
        {
            unit.GetComponent<InputController>().enabled = isEnabled;
        }
    }
}