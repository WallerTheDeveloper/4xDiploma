// using UnityEngine;
// using UnityEngine.InputSystem;
//
// public class ButtonHandler : MonoBehaviour
// {
//     [SerializeField] private GameObject _spawnShipObject;
//     [SerializeField] private float _spawnDistance = 10f;
//     private Camera _camera;
//     private Vector3 mouseWorldPosition;
//     private void Start()
//     {
//         _camera = Camera.main;
//     }
//
//     //Event function
//     public void SpawnShipMousePosition()
//     {
//         Instantiate(_spawnShipObject, mouseWorldPosition, Quaternion.identity);
//     }
//
//     private void Update()
//     {
//         Vector3 mousePosition = Mouse.current.position.ReadValue();
//         mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(
//             mousePosition.x, 
//             mousePosition.y,
//             _camera.nearClipPlane + _spawnDistance
//         ));
//         _spawnShipObject.transform.position = mouseWorldPosition;
//     }
// }
