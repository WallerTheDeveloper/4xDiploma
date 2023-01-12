using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.UnitSelection
{
    public class UnitClick : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }
        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                RaycastHit hit;

                Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Globals.Layers.CONSTRUCTION_LAYER_MASK))
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                        Globals.Bools.IsOneUnitSelected = false;
                    }
                    else
                    {
                        UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                        Globals.Bools.IsOneUnitSelected = true;
                    }
                }
            }
        }
    }
}
