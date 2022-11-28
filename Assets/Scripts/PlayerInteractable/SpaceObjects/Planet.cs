 using Core;
 using UI;
 using UnityEngine;
 using UnityEngine.InputSystem;
 
 namespace PlayerInteractable.SpaceObjects
{
    public abstract class Planet: MonoBehaviour
    {
        [SerializeField] private PopUpMenu _popUpMenu;
        private Camera _camera;
        private Collider _planetCollider;
        private bool isClicked = true;
        
        public const int COLLIDER_RADIUS = 35;
        // public static Func<IEnumerator> OnButtonClick;
        private void Start()
        {
            _planetCollider = GetComponent<Collider>();
            _camera = Camera.main;
            
        }
        private void Update()
        {
            if (isClicked && Globals.Bools.isOneUnitSelected)
            {
                ActivatePopUpOnClick(true);
                isClicked = false;
            }
            else
            {
                ActivatePopUpOnClick(false);
                isClicked = true;
            }
        }
        public void GatherResource() // Editor event function 
        {
            if (Globals.Bools.hasReachedDestination)
            {
                _popUpMenu.ActivatePopUpMenu(false);
                print("Resource gathered!");
            }
        }
        public void BuildStarBaseInRadius() // Editor event function 
        {
            if (Globals.Bools.hasReachedDestination)
            {
                _popUpMenu.ActivatePopUpMenu(false);
                print("Star base built!");
            }
        }
        private void ActivatePopUpOnClick(bool isActive)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {   
                Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
    
                if (_planetCollider.Raycast(ray, out var hit, Mathf.Infinity))
                {
                    _popUpMenu.ActivatePopUpMenu(isActive);
                }
            }
        }
        public void DisableSpaceObjectMeshRenderer()
        {
            MeshRenderer childMesh = GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;
                    
            if (childMesh != null)
            {
                childMesh.enabled = false;
                gameObject.tag = Globals.Tags.inActiveObjectTag;
            }
        }
    }
}