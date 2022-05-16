using Ships;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.UnitSelection
{
    public class UnitsSelection: MonoBehaviour
    {
        private bool _isDragging = false;
        private Vector2 _dragStartPosition;
        private Color _greenColor = new Color(0.5f, 1f, 0.4f, 0.2f);
        private void Update()
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
                SelectUnitsInDraggingBox();
            }
        }

        private void SelectUnitsInDraggingBox()
        {
            Bounds selectionBounds = Utilities.GetViewportBounds(Camera.main, _dragStartPosition, Mouse.current.position.ReadValue());
            Ship[] selectableUnits = FindObjectsOfType<Ship>();

            bool inBounds;

            foreach (Ship unit in selectableUnits)
            {
                inBounds = selectionBounds.Contains(Camera.main.WorldToViewportPoint(unit.transform.position));

                if (inBounds)
                {
                    unit.GetComponent<UnitManager>().SelectUnit();
                }
                else
                {
                    unit.GetComponent<UnitManager>().DeselectUnit();
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