using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.UnitSelection
{
    public class UnitDrag : MonoBehaviour
    {
        private bool _isDragging = false;
        private Color _greenColor = new Color(0.5f, 1f, 0.4f, 0.2f);
        private Vector2 _dragStartPosition;

        void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _isDragging = true;
                _dragStartPosition = Mouse.current.position.ReadValue();
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                _isDragging = false;
            }

            if (_isDragging && _dragStartPosition != Mouse.current.position.ReadValue())
            {
                SelectUnits();
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                UnitSelections.Instance.DeselectAll();
                Globals.Bools.isOneUnitSelected = false;
            }
        }

        private void SelectUnits()
        {
            Bounds selectionBounds = Utilities.GetViewportBounds(Camera.main, _dragStartPosition, Mouse.current.position.ReadValue());
            bool inBounds;
            
            foreach (var unit in UnitSelections.Instance.unitsList)
            {
                inBounds = selectionBounds.Contains(Camera.main.WorldToViewportPoint(unit.transform.position));
                
                if (inBounds)
                {
                    UnitSelections.Instance.DragSelect(unit);
                }
            }
        }
        private void OnGUI()
        {
            if (_isDragging)
            {
                var rect = Utilities.GetScreenRect(_dragStartPosition, Mouse.current.position.ReadValue());
                Utilities.DrawScreenRect(rect, _greenColor);
                Utilities.DrawScreenRectBorder(rect, 1, Color.green);
            }
        }
    }

}