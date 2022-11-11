using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "My Scriptable Objects/Create Camera Data")]
    public class CameraData : ScriptableObject
    {
        [SerializeField]
        public float maxSpeed = 5f;
        
        [SerializeField]
        public float acceleration = 10f;
        
        [SerializeField]
        public float damping = 15f;

        [SerializeField]
        public float stepSize = 2f;
        
        [SerializeField]
        public float zoomDampening = 7.5f;
        
        [SerializeField]
        public float minHeight = 5f;
        
        [SerializeField]
        public float maxHeight = 50f;
        
        [SerializeField]
        public float zoomSpeed = 2f;

        [SerializeField]
        public float maxRotationSpeed = 1f;

        [SerializeField]
        [Range(0f, 0.1f)]
        public float edgeTolerance = 0.05f;

        [SerializeField] public bool useEdgeScreen = true;
        
        public Transform CameraTransform { get; set; }
    }
}