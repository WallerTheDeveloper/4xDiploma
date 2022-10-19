using System;
using Attributes;
using Cinemachine;
using Core.Miscellaneous;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

namespace Control
{
    public class CameraController : MonoBehaviour, IBoundarySensitive
    {
        private InputActionsAsset cameraActions;
        private InputAction movement;
        private Transform _cameraTransform;

        [SerializeField]
        private float maxSpeed = 5f;
        private float speed;
        [SerializeField]
        private float acceleration = 10f;
        [SerializeField]
        private float damping = 15f;

        [SerializeField]
        private float stepSize = 2f;
        [SerializeField]
        private float zoomDampening = 7.5f;
        [SerializeField]
        private float minHeight = 5f;
        [SerializeField]
        private float maxHeight = 50f;
        [SerializeField]
        private float zoomSpeed = 2f;

        [SerializeField]
        private float maxRotationSpeed = 1f;

        [SerializeField]
        [Range(0f,0.1f)]
        private float edgeTolerance = 0.05f;

        [SerializeField] private bool useEdgeScreen = true;
        
        //value set in various functions 
        //used to update the position of the camera base object.
        private Vector3 targetPosition;

        private float zoomHeight;

        //used to track and maintain velocity w/o a rigidbody
        private Vector3 horizontalVelocity;
        private Vector3 lastPosition;

        //tracks where the dragging action started
        Vector3 startDrag;

        private void Awake()
        {
            cameraActions = new InputActionsAsset();
            _cameraTransform = GetComponentInChildren<CinemachineVirtualCamera>().transform;
        }

        private void OnEnable()
        {
            zoomHeight = _cameraTransform.localPosition.y;
            _cameraTransform.LookAt(this.transform);

            lastPosition = this.transform.position;

            movement = cameraActions.Camera.Movement;
            cameraActions.Camera.RotateCamera.performed += RotateCamera;
            cameraActions.Camera.ZoomCamera.performed += ZoomCamera;
            cameraActions.Camera.Enable();
        }

        private void OnDisable()
        {
            cameraActions.Camera.RotateCamera.performed -= RotateCamera;
            cameraActions.Camera.ZoomCamera.performed -= ZoomCamera;
            cameraActions.Camera.Disable();
        }

        private void Update()
        {
            //inputs
            GetKeyboardMovement();
            
            if(useEdgeScreen)
                CheckMouseAtScreenEdge();
                
            DragCamera();
    
            //move base and camera objects
            UpdateVelocity();
            UpdateBasePosition();
            UpdateCameraPosition();
            
            DeclareBoundaries();
        }
        
        public void DeclareBoundaries()
        {
            //Map Boundaries = 35 x 35
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, MapSettings.minPositionX, MapSettings.maxPositionX),
                0f,
                Mathf.Clamp(transform.position.z, MapSettings.minPositionZ, MapSettings.maxPositionZ)
            );
        }

        private void UpdateVelocity()
        {
            horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
            horizontalVelocity.y = 0f;
            lastPosition = this.transform.position;
        }

        private void GetKeyboardMovement()
        {
            Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight()
                        + movement.ReadValue<Vector2>().y * GetCameraForward();

            inputValue = inputValue.normalized;

            if (inputValue.sqrMagnitude > 0.1f)
                targetPosition += inputValue;
        }

        private void DragCamera()
        {
            if (!Mouse.current.middleButton.isPressed)
                return;

            //create plane to raycast to
            Plane plane = new Plane(Vector3.up, Vector3.zero);
        
            if(plane.Raycast(GetMouseRay(), out float distance))
            {
                if (Mouse.current.middleButton.wasPressedThisFrame)
                    startDrag = GetMouseRay().GetPoint(distance);
                else
                    targetPosition += startDrag - GetMouseRay().GetPoint(distance);
            }
        }

        private void CheckMouseAtScreenEdge()
        {
            //mouse position is in pixels
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 moveDirection = Vector3.zero;

            //horizontal scrolling
            if (mousePosition.x < edgeTolerance * Screen.width)
                moveDirection += -GetCameraRight();
            else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
                moveDirection += GetCameraRight();

            //vertical scrolling
            if (mousePosition.y < edgeTolerance * Screen.height)
                moveDirection += -GetCameraForward();
            else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
                moveDirection += GetCameraForward();

            targetPosition += moveDirection;
        }

        private void UpdateBasePosition()
        {
            if (targetPosition.sqrMagnitude > 0.1f)
            {
                //create a ramp up or acceleration
                speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
                transform.position += targetPosition * speed * Time.deltaTime;
            }
            else
            {
                //create smooth slow down
                horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
                transform.position += horizontalVelocity * Time.deltaTime;
            }

            //reset for next frame
            targetPosition = Vector3.zero;
        }

        private void ZoomCamera(InputAction.CallbackContext obj)
        {
            float inputValue = -obj.ReadValue<Vector2>().y / 100f;

            if (Mathf.Abs(inputValue) > 0.1f)
            {
                zoomHeight = _cameraTransform.localPosition.y + inputValue * stepSize;

                if (zoomHeight < minHeight)
                    zoomHeight = minHeight;
                else if (zoomHeight > maxHeight)
                    zoomHeight = maxHeight;
            }
        }

        private void UpdateCameraPosition()
        {
            //set zoom target
             Vector3 zoomTarget = new Vector3(_cameraTransform.localPosition.x, zoomHeight, _cameraTransform.localPosition.z);
            //add vector for forward/backward zoom
            zoomTarget -= zoomSpeed * (zoomHeight - _cameraTransform.localPosition.y) * Vector3.forward;

            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
            _cameraTransform.LookAt(this.transform);
        }
     
        private void RotateCamera(InputAction.CallbackContext obj)
        {
            if (!Mouse.current.rightButton.isPressed)
                return;

            float inputValue = obj.ReadValue<Vector2>().x;
            transform.rotation = Quaternion.Euler(0f, inputValue * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
        }

        //gets the horizontal forward vector of the camera
        private Vector3 GetCameraForward()
        {
            Vector3 forward = _cameraTransform.forward;
            forward.y = 0f;
            return forward;
        }

        //gets the horizontal right vector of the camera
        private Vector3 GetCameraRight()
        {
            Vector3 right = _cameraTransform.right;
            right.y = 0f;
            return right;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
    }
}
